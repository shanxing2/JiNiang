Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports ShanXingTech
Imports ShanXingTech.Net2
Imports ShanXingTech.Text2
Imports 姬娘插件.Events

Public Class LightLiver
#Region "枚举"
    Enum ApiPath
#Disable Warning ide1006
        update
        startLive
        stopLive
#Enable Warning ide1006
    End Enum
#End Region

#Region "字段区"
    Private m_HttpHeadersParam As Dictionary(Of String, String)
    Private ReadOnly m_Referer As String = "http://link.bilibili.com/p/center/index"
    Private ReadOnly m_RoomApiBaseUrl As String = "https://api.live.bilibili.com/room/v1/Room/"
    Private ReadOnly m_MessagePattern As String = ",""msg"":""(.*?)"","
#End Region

#Region "属性区"
    Property Room As LiveRoom
    ReadOnly Property Platform As String = "pc"
    Property Token As String

#End Region

#Region "常量区"
	Public Const AreaSeparator As String = " · "
#End Region

#Region "构造函数区"

	Sub New(ByVal room As LiveRoom, ByVal token As String)
        Me.Room = room
        Me.Token = token

        m_HttpHeadersParam = New Dictionary(Of String, String) From {
            {"User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:55.0) Gecko/20100101 Firefox/55.0"},
            {"Connection", "keep-alive"},
            {"Cache-Control", "no-cache"},
            {"Accept-Encoding", "gzip, deflate"}
        }
    End Sub
#End Region

#Region "函数区"
    '''' <summary>
    '''' 执行Post请求
    '''' </summary>
    '''' <param name="url"></param>
    '''' <param name="postdata"></param>
    '''' <returns></returns>
    'Private Async Function DoPostAsync(ByVal url As String, ByVal postdata As String, ByVal encoding As Text.Encoding, ByVal referer As String) As Task(Of HttpResponse)
    '    m_HttpHeadersParam("Referer") = referer
    '    Return Await HttpAsync.TryPostAsync(url, m_HttpHeadersParam, postdata, encoding, 3)
    'End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="apiPath">需要执行的API</param>
    ''' <param name="kvps">需要修改的参数键值对。若没有需要修改的参数，可不需要传入</param>
    ''' <returns></returns>
    Private Async Function UpdateInternalAsync(ByVal apiPath As ApiPath， ParamArray ByVal kvps As KeyValuePair(Of String, String)()) As Task(Of (Success As Boolean, Message As String, Result As String))
        Dim url = m_RoomApiBaseUrl & apiPath.ToString()
        Dim postData = String.Empty
        If kvps.Length = 0 Then
            postData = $"room_id={Room.RealId}&csrf_token={Token}"
        ElseIf kvps.Length = 1 Then
            postData = $"room_id={Room.RealId}&csrf_token={Token}&{kvps(0).Key.ToLowerInvariant}={kvps(0).Value}"
        Else
            Dim kvpBuilder = StringBuilderCache.Acquire(180)
            For Each kvp In kvps
                If kvp.Key Is Nothing Then Continue For
                kvpBuilder.Append("&").Append(kvp.Key.ToLowerInvariant).Append("=").Append(kvp.Value)
            Next
            Dim tempKVString = StringBuilderCache.GetStringAndReleaseBuilder(kvpBuilder)
            postData = $"room_id={Room.RealId}&csrf_token={Token}{tempKVString}"
        End If

        Return Await BilibiliApi.DoApiPostAsync(url, postData, m_Referer)
    End Function


    Public Async Function UpdateAreaAsync(ByVal area_Id As String) As Task(Of (Success As Boolean, Message As String, Result As String))
        Return Await UpdateInternalAsync(ApiPath.update， New KeyValuePair(Of String, String)(NameOf(area_Id), area_Id))
    End Function

    Public Async Function UpdateTitleAsync(ByVal title As String) As Task(Of (Success As Boolean, Message As String, Result As String))
        ' 设置房间标题之后开播
        If title.Length = 0 Then
            Return (False, "你是耍流氓嘛???还没有输入标题呢~" & RandomEmoji.Shock, "")
        End If

        Dim changeRst = Await UpdateInternalAsync(ApiPath.update, New KeyValuePair(Of String, String)(NameOf(title), title))
        If changeRst.Success Then
            ' 保存设置成功的标题到数据库
            Room.Title = title
            Await TryStoreCurrentTitleAsync(Room.RealId, Room.Title)
            Return (True, changeRst.Message, changeRst.Result)
        Else
            Return (False, "失败," & changeRst.Message, "")
        End If
    End Function

    ''' <summary>
    ''' 开播
    ''' </summary>
    ''' <param name="areaId"></param>
    ''' <param name="roomTitle"></param>
    ''' <returns></returns>
    Public Async Function StartLiveAsync(ByVal areaId As String, ByVal roomTitle As String) As Task(Of (Success As Boolean, Message As String, NewestStatus As LiveStatus))
        If roomTitle <> Room.Title Then
            ' 设置房间标题之后开播
            Dim changeRst = Await UpdateTitleAsync(roomTitle)
            If Not changeRst.Success Then
                Return (False, changeRst.Message, LiveStatus.UnKnown)
            End If
        End If

        Dim UpdateRst = Await UpdateInternalAsync(ApiPath.startLive,
                                         New KeyValuePair(Of String, String)(NameOf(Platform), Platform),
                                         New KeyValuePair(Of String, String)("area_v2", areaId)
                                         )

        '{"code":0,"msg":"","message":"","data":{"change":1,"status":"ROUND"}}
        Dim statusString = UpdateRst.Result.GetFirstMatchValue("""status"":""(\w+)""")
        Dim status As LiveStatus
        System.Enum.TryParse(statusString, True, status)

        Return (UpdateRst.Success, UpdateRst.Message, status)
    End Function

    ''' <summary>
    ''' 关播
    ''' </summary>
    ''' <returns></returns>
    Public Async Function StopLiveAsync() As Task(Of (Success As Boolean, Message As String, NewestStatus As LiveStatus))
        Dim updateRst = Await UpdateInternalAsync(ApiPath.stopLive,
                                         New KeyValuePair(Of String, String)(NameOf(Platform), Platform)
                                         )

        Dim statusString = updateRst.Result.GetFirstMatchValue("""status"":""(\w+)""")
        Dim status As LiveStatus
        System.Enum.TryParse(statusString, True, status)

        Return (updateRst.Success, updateRst.Message, status)
    End Function

    ''' <summary>
    ''' 获取最近选择过的分区
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetChoosedAreaAsync() As Task(Of AreaInfo())
        Dim getRst = Await BilibiliApi.GetChoosedAreaAsync
        Dim area As AreaInfo() = {}
        Dim index As Integer

        If getRst.Success Then
            Dim json = getRst.Message
            ' {"code":0,"msg":"success","message":"success","data":[{"id":"27","name":"学习","parent_id":"1","parent_name":"娱乐","act_flag":0},{"id":"86","name":"英雄联盟","parent_id":"2","parent_name":"游戏","act_flag":0},{"id":"161","name":"催眠电台","parent_id":"1","parent_name":"娱乐","act_flag":0}]}
            Dim pattern = """id"":""(\d+)"",""name"":""(\S*?)"",""parent_id"":""(\d+)"",""parent_name"":""(\S*?)"""
            Dim matches = Regex.Matches(json, pattern, RegexOptions.IgnoreCase Or RegexOptions.Compiled)
            For Each match As Match In matches
                ReDim Preserve area(index)
                Dim ar As New AreaInfo With {
                    .Id = match.Groups(1).Value,
                    .Name = match.Groups(2).Value,
                    .ParentId = match.Groups(3).Value,
                    .ParentName = match.Groups(4).Value
                }
                area(index) = ar
                index += 1
            Next
        End If

        Return area
    End Function

    ''' <summary>
    ''' 获取分区表
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetAreaListAsync() As Task(Of Dictionary(Of Integer, AreaInfo()))
        Dim getRst = Await BilibiliApi.GetAreaListAsync()
        Dim areas As New Dictionary(Of Integer, AreaInfo())
        If getRst.Success Then
            Dim json = getRst.Message
            'Dim jObjectDic = TryCast(JsSerializer.DeserializeObject(json), Dictionary(Of String, Object))
            Dim jObject = MSJsSerializer.DeserializeObject(json)
            If jObject Is Nothing Then
                Return areas
            End If
            Dim areaEntity = MSJsSerializer.Deserialize(Of LiveEntity.Root)(json)

            If areaEntity?.code <> 0 Then
                Return areas
            End If

            For Each dt In areaEntity.data
                Dim index As Integer = 0
                Dim areaArr(dt.list.Count - 1) As AreaInfo
                For Each ar In dt.list
                    Dim area As New AreaInfo With {
                        .Id = ar.id,
                        .Name = ar.name,
                        .ParentId = ar.parent_id,
                        .ParentName = ar.parent_name,
                        .PinYin = ar.pinyin
                    }
                    areaArr(index) = area
                    index += 1
                Next

                areas.Add(areaArr(0).ParentId.ToIntegerOfCulture, areaArr)
            Next
        End If

        Return areas
    End Function

	''' <summary>
	''' 返回 “父分区 · 子分区” 格式字符串
	''' </summary>
	''' <param name="area"></param>
	''' <returns></returns>
	Public Function GetAreaFullName(ByVal area As AreaInfo) As String
		Dim areaName = If(area.Name = area.ParentName,
			area.Name,
			area.ParentName & AreaSeparator & area.Name)

		Return areaName
	End Function

	Private Async Function TryStoreCurrentTitleAsync(ByVal roomId As String, ByVal title As String) As Task
        Try
            ' 无则添加，有则更新
            Dim sql = $"INSERT OR REPLACE INTO RoomUsedTitle (id,RoomId,Title,LastUseTimestamp) VALUES ((SELECT id FROM RoomUsedTitle WHERE roomId = {roomId} AND title = '{title}'),{roomId},'{title}',{Date.Now.ToTimeStampString(TimePrecision.Second)});"
            Await IO2.Database.SQLiteHelper.ExecuteNonQueryAsync(sql, 0)
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try
    End Function


    ''' <summary>
    ''' 获取使用过的Top n个标题
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetUsedTitleAsync() As Task(Of Object())
        Dim roomTitles As Object() = {}

        Try
            Dim sql = String.Empty
            If DanmuEntry.Configment.DisplayUesdTitleCount = 0 Then
                sql = $"select Title from RoomUsedTitle where roomid = {Room.RealId};"
            Else
                sql = $"select Title from RoomUsedTitle where roomid = {Room.RealId} order by LastUseTimestamp desc limit 0, {DanmuEntry.Configment.DisplayUesdTitleCount.ToStringOfCulture};"
            End If

            Dim index As Integer
            Using reader = Await IO2.Database.SQLiteHelper.GetDataReaderAsync(sql)
                While Await reader?.ReadAsync
                    ReDim Preserve roomTitles(index)
                    roomTitles(index) = reader(0)
                    index += 1
                End While
            End Using
        Catch ex As Exception
            Logger.WriteLine(ex)
        Finally
            If roomTitles.Length = 0 Then
                ReDim roomTitles(0)
                roomTitles(0) = "今天没吃药，感觉自己萌萌哒~"
            End If
        End Try

        Return roomTitles
    End Function

    ''' <summary>
    ''' 获取Up直播间直播流信息,观众无权限
    ''' </summary>
    ''' <returns></returns>
    Public Async Function GetLiveStreamAsync() As Task(Of (Success As Boolean, LiveStream As StreamInfo))
        Dim funcRst As Boolean
        Dim liveStream As New StreamInfo

        Try
            Dim getRst = Await BilibiliApi.GetLiveStreamAsync
            funcRst = getRst.Success

            If funcRst Then
                Dim json = getRst.Message
                ' 成功 返回具体信息
                ' {"code":0,"msg":"ok","message":"ok","data":{"rtmp":{"addr":"rtmp://qn.live-send.acg.tv/live-qn/","code":"?streamname=xxx&key=yyy"},"stream_line":[{"name":"默认线路","src":70,"cdn_name":"qn","checked":1}]}}

                Dim pattern = """addr"":""(\S*?)"",""code"":""(\S*?)"""
                Dim match = Regex.Match(json, pattern, RegexOptions.IgnoreCase Or RegexOptions.Compiled)
                If match.Success Then
                    liveStream.RtmpAddress = match.Groups(1).Value
                    liveStream.RtmpCode = match.Groups(2).Value.TryUnescape
                End If
            End If
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try

        Return (funcRst, liveStream)
    End Function
#End Region
End Class
