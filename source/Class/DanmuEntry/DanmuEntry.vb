Imports System.ComponentModel
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.Script.Serialization
Imports ShanXingTech
Imports ShanXingTech.ExtensionFunc
Imports ShanXingTech.Net2
Imports ShanXingTech.Text2
Imports 姬娘插件.Events
Imports 姬娘.Events

Public NotInheritable Class DanmuEntry
    'Inherits ApiRequest

#Region "枚举"

#End Region

#Region "属性区"
    Public Shared ReadOnly Property User As UserInfo

    'Public Shared Property CurrentUserId As Integer
    Public Shared Property Cts As Threading.CancellationTokenSource
    Public Shared Property Ct As Threading.CancellationToken
    Public Shared Property Configment As ConfigEntity
    Public Shared Property IsClosed As Boolean

#End Region

#Region "字段区"
    Private Shared m_LastMessage As String
    Private Shared Property m_CreateDbSql As String
    Private Shared ReadOnly m_MessagePattern As String = ",""msg"":""(.*?)"","
    Private Shared WithEvents m_LoginManager As LoginManager
#End Region


#Region "事件区"
    ''' <summary>
    ''' 直播间信息改变事件
    ''' </summary>
    Public Shared Event LiveRoomInfoChanged As EventHandler(Of LiveRoomInfoChangedEventArgs)
    ''' <summary>
    ''' 粉丝增加事件
    ''' </summary>
    Public Shared Event AttentionIncreased As EventHandler(Of AttentionIncreasedEventArgs)
    ''' <summary>
    ''' 直播状态改变事件
    ''' </summary>
    Public Shared Event LiveStatusChanged As EventHandler(Of LiveStatusChangedEventArgs)
    ''' <summary>
    ''' 已确认选择了某个用户，可以使用用户设置来配置mainform
    ''' </summary>
    Public Shared Event UserEnsured As EventHandler(Of UserEnsuredEventArgs)
    ''' <summary>
    ''' 获取直播间信息完成，可以初始化弹幕发送组件等
    ''' </summary>
    Public Shared Event RoomRealIdEnsured As EventHandler(Of EventArgs)
#End Region

#Region "构造函数区"
    ''' <summary>
    ''' 类构造函数
    ''' 类之内的任意一个静态方法第一次调用时调用此构造函数
    ''' 而且程序生命周期内仅调用一次
    ''' </summary>
    Shared Sub New()
        User = New UserInfo
        m_LoginManager = New LoginManager(User)
        Configment = New ConfigEntity

        BilibiliApi.Init(User)
    End Sub

    'Sub New()

    '    '
    'End Sub
#End Region


#Region "函数区"
    ''' <summary>
    ''' 入口开启
    ''' </summary>
    Protected Friend Shared Sub Open()
        '在此过程做一些外部环境初始化
        IO2.Database.SQLiteHelper.Init(MakeDbFileName)

        Dim fileName = ".\res\CreateDb.sql"
        Dim fileFullPath = IO.Path.GetFullPath(fileName)
        If Not IO.File.Exists(fileFullPath) Then
            Throw New IO.FileNotFoundException("获取建库语句文件失败")
        End If
        m_CreateDbSql = IO2.Reader.ReadFile(fileFullPath, Encoding.UTF8)

        If m_CreateDbSql.IsNullOrEmpty Then
            Throw New ArgumentException("获取建库语句失败")
        End If

        IO2.Database.SQLiteHelper.CreateTable(m_CreateDbSql)
        DatabaseUpgrader.TryUpgrade()
    End Sub

    ''' <summary>
    ''' 生成数据库文件名
    ''' </summary>
    ''' <returns></returns>
    Private Shared Function MakeDbFileName() As String
        Dim database As String
        ' Release模式把数据库放到exe目录下方便移动
#If DEBUG Then
        ' C:\ProgramData\ShanXingTech\姬娘\DanmuHime.db
        database = IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Application.CompanyName, Application.ProductName, "DanmuHime.db")

#Else
        ' .\db\DanmuHime.db
        database = IO.Path.Combine(Application.StartupPath, "db\DanmuHime.db")
#End If

        Return database
    End Function

    ''' <summary>
    ''' 入口关闭
    ''' </summary>
    ''' <param name="danmuControl"></param>
    Protected Friend Shared Sub Close(ByRef danmuControl As DanmuControl)
        Dim sb = StringBuilderCache.AcquireSuper(2048)

        Dim danmuElements = If(danmuControl?.DanmuDiv?.Children.Count = 0, Nothing, danmuControl?.DanmuDiv?.Children)
        sb.Append(MakeStoreLiveRoomInfoSQL(danmuElements))
        If sb.Length > 0 Then sb.AppendLine()

        sb.Append(MakeStoreUserInfoSQL())
        If sb.Length > 0 Then sb.AppendLine()

        sb.Append(MakeStorePersonalConfigSQL())
        If sb.Length > 0 Then sb.AppendLine()

        sb.Append(MakeStoreMedalConfigSQL())
        If sb.Length > 0 Then sb.AppendLine()

        If sb.Length = 0 Then
            Debug.Print(Logger.MakeDebugString("无数据需要保存到数据库"))
        Else
            Dim sql = StringBuilderCache.GetStringAndReleaseBuilderSuper(sb)
            IO2.Database.SQLiteHelper.ExecuteNonQuery(sql)

            Debug.Print(Logger.MakeDebugString("数据保存到数据库成功"))
        End If

        IsClosed = True
    End Sub

    ''' <summary>
    ''' 获取真正的直播间Id
    ''' </summary>
    ''' <param name="shortRoomId">直播间短Id</param>
    ''' <returns></returns>
    Private Shared Async Function GetRoomRealIdAsync(ByVal shortRoomId As String) As Task(Of (Success As Boolean, RoomShortId As Integer, RoomRealId As Integer))
        Dim getRst = Await GetRoomRealIdFromDBAsync(shortRoomId)
        Return If(getRst.Success, getRst,
            Await BilibiliApi.GetRoomRealIdFromNetworkAsync(shortRoomId))
    End Function

    ''' <summary>
    ''' 获取真正的直播间Id
    ''' </summary>
    ''' <param name="shortRoomId">直播间短Id</param>
    ''' <returns></returns>
    Private Shared Async Function GetRoomRealIdFromDBAsync(ByVal shortRoomId As String) As Task(Of (Success As Boolean, RoomShortId As Integer, RoomRealId As Integer))
        Dim shortId As Integer
        Dim realId As Integer
        Dim funcRst As Boolean

        Dim sql = $"select ShortId,RealId from ViewedRoomInfo where RealId = '{shortRoomId}' or ShortId = '{shortRoomId}'"
        Using reader = Await IO2.Database.SQLiteHelper.GetDataReaderAsync(sql)
            funcRst = Await reader?.ReadAsync
            If funcRst Then
                shortId = If(DBNull.Value.Equals(reader(0)), -1, CInt(reader(0)))
                realId = If(DBNull.Value.Equals(reader(1)), -1, CInt(reader(1)))
            End If
        End Using

        Return (funcRst, shortId, realId)
    End Function

    ''' <summary>
    ''' 获取当前用户正在观看的直播间相关信息 获取到的结果会有延时
    ''' </summary>
    ''' <returns></returns>
    Private Shared Async Function GetViewRoomInfoAsync(ByVal roomId As String) As Task(Of (Success As Boolean, Room As LiveRoom))
        Dim room As New LiveRoom With {.RealId = roomId}
        Dim funcRst = Await InternalGetViewRoomInfoAsync(room)
        Return (funcRst, room)
    End Function

    ''' <summary>
    ''' 获取当前用户正在观看的直播间相关信息 获取到的结果会有延时
    ''' </summary>
    ''' <returns></returns>
    Private Shared Async Function InternalGetViewRoomInfoAsync(ByVal viewRoom As LiveRoom) As Task(Of Boolean)
        Dim succeed = Await FillViewRoomInfoAsync(viewRoom)
        If Not succeed Then Return False

        Dim getServerConfigTask = GetServerConfigAsync(viewRoom)
        Dim getAnchorInRoomInfoTask = GetAnchorInRoomAsync(viewRoom.RealId)
        Dim getAreaRankTask = GetAreaRankAsync(viewRoom.RealId)
        Dim getHourRankTask = GetHourRankAsync(viewRoom.UserId, viewRoom.RealId)

        Await Task.WhenAll(getServerConfigTask,
                           getAnchorInRoomInfoTask,
                           getAreaRankTask,
                           getHourRankTask
                           )

        viewRoom.UserNick = getAnchorInRoomInfoTask.Result.RoomOwnerNick
        viewRoom.AnchorInRoom = getAnchorInRoomInfoTask.Result.AnchorInRoom
        viewRoom.AreaRank = getAreaRankTask.Result.AreaRank
        viewRoom.HourRank = getHourRankTask.Result.HourRank

        Dim funcRst = getAreaRankTask.Result.Success

        Return funcRst
    End Function

    ''' <summary>
    ''' 获取当前用户正在观看的直播间相关信息 获取到的结果会有延时
    ''' </summary>
    ''' <returns></returns>
    Private Shared Async Function FillViewRoomInfoAsync(ByVal viewRoom As LiveRoom) As Task(Of Boolean)
        ' 获取直播间信息不需要带cookie
        Dim funcRst As Boolean

        Try
            Dim getRst = Await BilibiliApi.GetViewRoomInfoAsync(viewRoom.RealId)
            Dim json = getRst.Message
            funcRst = getRst.Success

            If funcRst Then
                ' 成功 返回具体信息
                ' online 12 在线人数
#Region "反序列化"
                Dim root = MSJsSerializer.Deserialize(Of RoomEntity.Root)(json)
                If root Is Nothing Then
                    Exit Try
                End If

                With viewRoom
                    Dim room = root.data
                    .ShortId = room.short_id.ToStringOfCulture
                    .RealId = room.room_id.ToStringOfCulture
                    .HotWords = room.hot_words.ToArray
                    .UserId = room.uid.ToStringOfCulture
                    .Title = room.title
                    .Attention = room.attention
                    .Online = room.online
                    .Status = room.live_status
                    .Area = New AreaInfo With {
                        .Id = room.area_id.ToStringOfCulture,
                        .Name = room.area_name,
                        .ParentId = room.parent_area_id.ToStringOfCulture,
                        .ParentName = room.parent_area_name
                    }
                End With
#End Region
            End If
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try

        Return funcRst
    End Function

    Private Overloads Shared Async Function GetServerConfigAsync(ByVal room As LiveRoom) As Task
        Try
            room.Server = Await TryGetServerConfigInternalAsync(room.RealId)
            room.Guard = Await GetGuardInfoAsync(room)
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try
    End Function

    ''' <summary>
    ''' 获取弹幕服务器配置信息
    ''' </summary>
    ''' <param name="readRoomId"></param>
    ''' <returns>返回服务器配置信息，获取不到的话返回默认的 （dm_ws_port:2244,dm_wss_port:443,dm_port:2243,dm_server:broadcastlv.chat.bilibili.com）</returns>
    Private Shared Async Function TryGetServerConfigInternalAsync(ByVal readRoomId As String) As Task(Of ServerInfo)
        Dim json = Await BilibiliApi.GetServerConfigAsync(readRoomId)
        Try
            If json.Length = 0 Then Exit Try
            Dim root = MSJsSerializer.Deserialize(Of DanmuServerInfoEntity.Root)(json)
            If root Is Nothing Then Exit Try

            Dim server As New ServerInfo With {
                .DmWsPort = root.data.host_server_list(0).ws_port,
                .DmWsspPort = root.data.host_server_list(0).wss_port,
                .DmPort = root.data.port,
                .DmHost = root.data.host,
                .DmServerList = root.data.server_list(0).host,
                .DmHostList = root.data.host_server_list(0).host
            }

            Return server
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try

        Dim server2 As New ServerInfo With {
                .DmWsPort = 2244,
                .DmWsspPort = 443,
                .DmPort = 2243,
                .DmHost = "broadcastlv.chat.bilibili.com",
                .DmServerList = "broadcastlv.chat.bilibili.com",
                .DmHostList = "broadcastlv.chat.bilibili.com"
            }

        Return server2
    End Function

    ''' <summary>
    ''' 获取当前用户于当前直播间的信息（弹幕长度、颜色、守护信息）
    ''' </summary>
    ''' <param name="room"></param>
    ''' <returns>返回服务器配置信息，获取不到的话返回默认的(msg_color:16777215,msg_length:20)</returns>
    Private Shared Async Function GetGuardInfoAsync(ByVal room As LiveRoom) As Task(Of GuardInfo)
        Dim json = Await BilibiliApi.GetInfoByUserAsync(room.RealId)
        Try
            If json.Length = 0 Then Exit Try
            Dim root = MSJsSerializer.Deserialize(Of RoomViewerInfoEntity.Root)(json)
            If root Is Nothing Then Exit Try

            ' 未登录
            If root.code = -101 Then Exit Try

            room.IsAdmin = root.data.badge.is_room_admin
            ' ######################################################################
            ' .Level 属性暂时不清楚怎么获取， 先默认为常人
            ' ######################################################################
            Dim guard As New GuardInfo With {
                .DanmuColorDec = root.data.property.danmu.color,
                .DanmuFontSize = 25,
                .DanmuMaxLength = root.data.property.danmu.length,
                .Level = GuardLevel.常人
            }

            Return guard
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try

        Dim guard2 As New GuardInfo With {
                .DanmuColorDec = 16777215,
                .DanmuFontSize = 25,
                .DanmuMaxLength = 20,
                .Level = GuardLevel.常人
            }
        Return guard2
    End Function

    ''' <summary>
    ''' 获取直播间积分信息(主播积分、SAN等)
    ''' </summary>
    ''' <returns></returns>
    Private Shared Async Function GetAnchorInRoomAsync(ByVal roomRealId As String) As Task(Of (Success As Boolean, RoomOwnerNick As String, AnchorInRoom As AnchorInRoomInfo))
        ' 获取直播间信息不需要带cookie
        Dim funcRst As Boolean
        Dim anchorInRoom As New AnchorInRoomInfo
        Dim nick = String.Empty

        Try
            Dim getRst = Await BilibiliApi.GetAnchorInRoomAsync(roomRealId)
            Dim json = getRst.Message
            funcRst = getRst.Success

            If funcRst Then
                ' 成功 返回具体信息

#Region "反序列化"
                Dim root = MSJsSerializer.Deserialize(Of AnchorInRoomEntity.Root)(json)
                If root Is Nothing Then
                    Exit Try
                End If

                Dim anchorData = root.data
                If anchorData Is Nothing Then
                    Logger.WriteLine(json)
                    Exit Try
                End If
                nick = anchorData.info.uname
                anchorInRoom.AnchorScore = anchorData.level.anchor_score
                anchorInRoom.San = anchorData.san
#End Region
            End If
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try

        Return (funcRst, nick, anchorInRoom)
    End Function

    ''' <summary>
    ''' 获取直播间分区排名
    ''' </summary>
    ''' <returns></returns>
    Private Shared Async Function GetAreaRankAsync(ByVal roomRealId As String) As Task(Of (Success As Boolean, AreaRank As String))
        ' 获取直播间信息不需要带cookie
        Dim funcRst As Boolean
        Dim areaRank = String.Empty

        Try
            Dim getRst = Await BilibiliApi.GetAreaRankAsync(roomRealId)
            Dim json = getRst.Message
            funcRst = getRst.Success

            If funcRst Then
                ' 成功 返回具体信息
                '{"code"0,"msg":"OK","message":"OK","data":{"areaRank":{"index":0,"rank":">1000"}}}
                '{"code":0,"msg":"OK","message":"OK","data":{"areaRank":{"index":1,"rank":"39"}}}
                Dim pattern = """rank"":""(>?\d+)"""
                areaRank = GetFirstMatchValue(json, pattern)
            End If
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try

        Return (funcRst, areaRank)
    End Function

    ''' <summary>
    ''' 获取直播间小时榜排名
    ''' </summary>
    ''' <param name="roomUserId"></param>
    ''' <param name="roomAreaId"></param>
    ''' <returns></returns>
    Private Shared Async Function GetHourRankAsync(ByVal roomUserId As String, ByVal roomAreaId As String) As Task(Of (Success As Boolean, HourRank As String))
        ' 获取直播间信息不需要带cookie
        Dim funcRst As Boolean
        Dim hourRank = String.Empty

        Try
            Dim getRst = Await BilibiliApi.GetHourRankAsync(roomUserId, roomAreaId)
            Dim json = getRst.Message
            funcRst = getRst.Success

            If funcRst Then
                ' 成功 返回具体信息
                ' {"code":0,"msg":"success","message":"success","data":{"score":0,"rank":0,"uid":52155851,"uname":"小母猪和她胖头鱼哥哥","face":"https://i2.hdslb.com/bfs/face/074fe049c7d4bc47e3a6ae89906af5d0246a5ea1.jpg","personal_verify":-1,"best_assist":[],"unit":"金瓜子","distance_text":"距上榜还差","prescore":1,"last_hour_desc":"","self":0}}
                Dim pattern = """rank"":(>?\d+)"
                hourRank = GetFirstMatchValue(json, pattern)

                If "0" = hourRank Then
                    hourRank = String.Empty
                End If
            End If
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try

        Return (funcRst, hourRank)
    End Function

    ''' <summary>
    ''' 获取粉丝列表
    ''' </summary>
    ''' <param name="userId">用户Id</param>
    ''' <param name="pageNo">页数</param>
    ''' <param name="lastAttentionTime">最新一个粉丝关注的时间</param>
    ''' <param name="pageSize">页码</param>
    ''' <returns></returns>
    Private Shared Async Function GetFansAsyncInternal(ByVal userId As String, ByVal pageNo As Integer, ByVal lastAttentionTime As Integer, Optional ByVal pageSize As Integer = 20) As Task(Of (Success As Boolean, LastAttentionTime As Integer, Fans As Fans()))
        Dim funcRst As Boolean
        Dim fans As Fans() = {}

        Try
            Dim getRst = Await BilibiliApi.GetFansAsync(userId, pageNo, lastAttentionTime, pageSize)
            Dim json = getRst.Message
            funcRst = getRst.Success

            ' {"code":22007,"message":"限制只访问前5页","ttl":1}

            If funcRst Then
                ' 成功 返回具体信息

                ' {"mid":粉丝Id(用户Id),"attribute":0,"mtime":1529071061,"tag":null,"special":0,"uname":"昵称"
                Dim pattern = """mid"":(\d+),""attribute"":\d,""mtime"":(\d{10}),""tag"":\w+,""special"":\d,""uname"":""(.*?)"""
                Dim matches = Regex.Matches(json, pattern, RegexOptions.IgnoreCase Or RegexOptions.Compiled)

                Dim index As Integer
                For Each match As Match In matches
                    Dim attentionTime = match.Groups(2).Value.ToIntegerOfCulture

                    ' 只返回新关注的粉丝
                    If attentionTime <= lastAttentionTime Then
                        Exit For
                    End If

                    ReDim Preserve fans(index)

                    fans(index) = New Fans With {
                        .Id = match.Groups(1).Value.ToIntegerOfCulture,
                        .Name = match.Groups(3).Value,
                        .AttentionTime = attentionTime
                    }

                    index += 1
                Next

                If fans.Length > 0 Then
                    lastAttentionTime = fans(fans.Length - 1).AttentionTime
                End If
            End If
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try

        Return (funcRst, lastAttentionTime, fans)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="connectMode"></param>
    ''' <returns></returns>
    Protected Friend Shared Async Function UpdateRoomInfoAsync(ByVal connectMode As ConnectMode) As Task
        Dim succeed = Await UpdateRoomInfoAsync(User.ViewRoom)
        If Not succeed Then
            User.ViewRoom.Status = LiveStatus.UnKnown
            RaiseEvent LiveRoomInfoChanged(Nothing, New LiveRoomInfoChangedEventArgs(User.ViewRoom))
            Debug.Print(Logger.MakeDebugString("获取直播间信息失败"))
            Return
        End If

        RaiseEvent LiveRoomInfoChanged(Nothing, New LiveRoomInfoChangedEventArgs(User.ViewRoom))

#If DEBUG Then
        ' debug 模式 并且 未登录的话 那就不需要去获取新关注粉丝信息了
        If Not User.IsLogined Then
            Return
        End If
#End If

        If Configment.EnabledAttentionHime Then
            ' 直接把现时当做是最后一个粉丝关注时间即只提示软件运行之后关注的新粉丝，最后一个粉丝关注时间不再存库，避免首次使用软件时出现大量提示（前五页的粉丝）
            If 0 = User.ViewRoom.LastAttentionTimestamp Then
                User.ViewRoom.LastAttentionTimestamp = Date.Now.ToTimeStampString(TimePrecision.Second).ToIntegerOfCulture
            End If
            'User.ViewRoom.LastAttentionTimestamp = 1562913057
            Dim getFansRst = Await GetFansAsync(User.ViewRoom.UserId, User.ViewRoom.LastAttentionTimestamp)
            User.ViewRoom.LastAttentionTimestamp = getFansRst.LastAttentionTime

            Dim fans = getFansRst.Fans
            If fans.Length = 1 Then
                RaiseEvent AttentionIncreased(Nothing, New AttentionIncreasedEventArgs(fans(0)))
            ElseIf fans.Length > 1 Then
                For Each fan In fans
                    ' 循环中，如果用户取消了 粉丝姬那就不再继续引发事件
                    If Configment.EnabledAttentionHime Then Exit For
                    RaiseEvent AttentionIncreased(Nothing, New AttentionIncreasedEventArgs(fan))
                Next
            End If
        End If
    End Function

    ''' <summary>
    ''' 获取当前用户正在观看的直播间相关信息 获取到的结果会有延时
    ''' </summary>
    ''' <returns></returns>
    Private Shared Async Function UpdateRoomInfoAsync(ByVal viewRoom As LiveRoom) As Task(Of Boolean)
        Return Await InternalGetViewRoomInfoAsync(viewRoom)
    End Function

    ''' <summary>
    ''' 获取新粉丝
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <param name="lastAttentionTime"></param>
    ''' <returns></returns>
    Private Shared Async Function GetFansAsync(ByVal userId As String, ByVal lastAttentionTime As Integer) As Task(Of (LastAttentionTime As Integer, Fans As Fans()))
        Dim fans As Fans() = {}

        Dim pageSize = 20
        Dim pageNo = 1
        Dim totalFans = 0

        While True
            Dim getFansRst = Await GetFansAsyncInternal(userId, pageNo, lastAttentionTime, pageSize)

            If getFansRst.Success AndAlso getFansRst.Fans.Length > 0 Then
                Dim tempFans = getFansRst.Fans

                ReDim Preserve fans(tempFans.Length + fans.Length - 1)
                Array.Copy(tempFans, 0, fans, totalFans, tempFans.Length)

                totalFans += tempFans.Length

                Dim tempLastAttentionTime = tempFans(0).AttentionTime
                ' 如果此页最后一个关注粉丝关注的时间还是大于 lastAttentionTime ，说明下一页可能还有新关注的粉丝
                If tempLastAttentionTime > lastAttentionTime AndAlso getFansRst.Fans.Length >= pageSize Then
                    pageNo += 1
                Else
                    Exit While
                End If
            Else
                Exit While
            End If
        End While

        If fans.Length = 0 Then
            Return (lastAttentionTime, fans)
        Else
            lastAttentionTime = fans(0).AttentionTime

            Return (lastAttentionTime, fans.Reverse.ToArray)
        End If
    End Function

    ''' <summary>
    ''' 获取弹幕内容 格式为: "{播主或观众}{月费或者年费或者无}{粉丝勋章 等级}{等级}{昵称}  {enviroment.newline}{时间}{enviroment.newline}{字幕内容}{enviroment.newline}"
    ''' </summary>
    ''' <returns>Message 返回格式化之后的弹幕内容，如果需要原始字幕自己解析请调用 <seealso cref="BilibiliApi.GetDanmuJsonAsync()"/></returns>
    Protected Friend Shared Async Function GetDanmuAsync() As Task(Of (Success As Boolean, Message As String, NeedChangeChatHistory As Boolean))
        Dim damuJson = Await BilibiliApi.GetDanmuJsonAsync()
        Dim parseRst = ParseDanmu(damuJson)
        Dim message = parseRst.Message
        Dim needChangeChatHistory As Boolean

        ' 如果当日没有弹幕，那就加载上一次软件退出时保存的弹幕
        ' 如果还是没有，那就直接返回 空
        If message.Length = 0 Then
            message = GetCacheDanmu()
        End If

        ' 有弹幕 并且 最后一次读取到的弹幕 <> 上次的弹幕  需要更新弹幕
        If message.Length > 0 AndAlso Not message.Equals(m_LastMessage, StringComparison.OrdinalIgnoreCase) Then
            m_LastMessage = message
            needChangeChatHistory = True
        Else
            needChangeChatHistory = False
        End If

        Return (parseRst.Success, message, needChangeChatHistory)
    End Function

    ''' <summary>
    ''' 解析弹幕内容
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function ParseDanmu(ByVal danmuJson As String) As (Success As Boolean, Message As String)
        Dim message = String.Empty
        Dim funcRst As Boolean
        Dim needChangeChatHistory As Boolean

        Try
            funcRst = danmuJson.IndexOf("""code"":0") > -1

            ' 获取失败，返回空
            If Not funcRst Then
                Return (False, String.Empty)
            Else
                ' 成功 返回具体信息
                ' {"text":"\u7206\u70b8\u7684\u65f6\u5019\u53ef\u4ee5\u622a\u5c4f\u79c1\u53d1\u6211\u5417\uff08\u597d\u56f0\u554a\uff09","uid":176771693,"nickname":"\u662f\u4e0d\u4e00\u554a","uname_color":"","timeline":"2018-06-01 23:44:13","isadmin":0,"vip":0,"svip":0,"medal":[],"title":[],"user_level":[11,0,6406234,">50000"],"rank":10000,"teamid":0,"rnd":-1215288790,"user_title":"","guard_level":0,"activity_info":{"uname_color":""}}
                ' isadmin 为0 表示一般用户， isadmin 为1表示up或者房管？
                ' vip 应该是 老爷， svip应该是年费老爷
#Region "反序列化获取信息"
                Dim jObject = MSJsSerializer.DeserializeObject(danmuJson)
                If jObject Is Nothing Then
                    Return (False, String.Empty)
                End If

                Dim danmus = MSJsSerializer.ConvertToType(Of DanmuEntity.HttpDanmu.Room())(jObject("data")("room"))
                Dim sb = StringBuilderCache.AcquireSuper(720)
                For Each dm In danmus
                    ' 每一组 空格分割
                    ' "{总督/提督/舰长} {月费或者年费或者无} {房管或观众} {粉丝勋章 勋章等级} {活动头衔} {等级}{播主或自己或者观众昵称} {enviroment.newline}{时间}{enviroment.newline}{字幕内容}"
                    sb.Append(If(3 = dm.guard_level, "[舰长] ", If(2 = dm.guard_level, "[提督] ", If(1 = dm.guard_level, "[总督] ", ""))))
                    sb.Append(If(dm.isadmin = 1, If(User.ViewRoom.UserNick = dm.nickname, "", "[房管] "), ""))
                    sb.Append(If(dm.svip = 1, "[年]", If(dm.vip = 1, "[月]", "")))
                    sb.Append(If(dm.medal.Count = 0, "", "[" & dm.medal(1) & " " & dm.medal(0) & "]"))
                    sb.Append("[UL ").Append(dm.user_level(0)).Append("] ")
                    If User.Nick = dm.nickname Then
                        sb.AppendFormat(Common.UpOrOwnDanmuTemplete, "[自己]", dm.timeline, dm.text)
                    ElseIf User.ViewRoom.UserNick = dm.nickname Then
                        sb.AppendFormat(Common.UpOrOwnDanmuTemplete, "[播主]", dm.timeline, dm.text)
                    Else
                        sb.AppendFormat(Common.ViewerDanmuTemplete, dm.uid, dm.check_info.ts, dm.check_info.ct, dm.nickname, dm.timeline, dm.text)
                    End If
                    sb.Append("<br/><br/>")
                Next
                ' 去掉最后两个 换行
                Dim twoBrLength = "<br/><br/>".Length
                If sb.Substring(sb.Length - twoBrLength, twoBrLength) = "<br/><br/>" Then
                    sb.Remove(sb.Length - twoBrLength, twoBrLength)
                End If

                message = StringBuilderCache.GetStringAndReleaseBuilderSuper(sb)

                ' 记录最后一条信息的发言人 时间 以及内容  以分辨是否有弹幕需要更新
                ' 如果有弹幕， 那就 并且最后一条弹幕 <> 上次获取的最后一条弹幕   需要更新弹幕
                Dim lastDanmu = danmus.LastOrDefault
                If lastDanmu IsNot Nothing Then
                    Dim lastMessage = $"{If(lastDanmu.isadmin = 1, If(User.ViewRoom.UserNick = lastDanmu.nickname, "", "[房管]"), "")}{lastDanmu.nickname}  {lastDanmu.timeline} {lastDanmu.text}"
                    ' 如果弹幕内容不同，则设置需要更新的标记以及记录最新的弹幕内容
                    needChangeChatHistory = Not lastMessage.Equals(m_LastMessage, StringComparison.OrdinalIgnoreCase)
                    If needChangeChatHistory Then
                        m_LastMessage = lastMessage
                    End If
                End If
#End Region

                Return (True, message)
            End If
        Catch ex As Exception
            Logger.WriteLine(ex, danmuJson,,,)
        End Try

        Return (funcRst, message)
    End Function

    ''' <summary>
    ''' 读取上次退出时保存的缓存弹幕
    ''' </summary>
    ''' <returns></returns>
    Private Shared Function GetCacheDanmu() As String
        Dim sql = $"select DanmuTop100 from ViewedRoomInfo where RealId = {If(User.ViewRoom.RealId.IsNullOrEmpty, "-1", User.ViewRoom.RealId)}"
        Dim danmuCacheObj = IO2.Database.SQLiteHelper.GetFirst(sql)
        If danmuCacheObj Is Nothing Then Return String.Empty

        Dim danmuCache = danmuCacheObj.ToString.FromHexString(True)
        Return danmuCache
    End Function

    ''' <summary>
    ''' 保存当前弹幕到缓存文件以备下次打开时读取
    ''' </summary>
    ''' <param name="danmuElements"></param>
    Private Shared Function GetCurrentDanmuTop100(ByVal danmuElements As HtmlElementCollection) As String
        If danmuElements Is Nothing Then Return String.Empty

        Dim message = String.Empty

        Try
            Dim brPair = "<br/><br/>"
            ' 最多缓存后一百条信息
            Dim cacheNum As Integer = If(danmuElements.Count < 100, danmuElements.Count, 100)
            Dim sb = StringBuilderCache.AcquireSuper(720)
            For index = 0 To cacheNum - 1
                sb.Append(danmuElements(index).InnerHtml).Append(brPair)
            Next

            ' 后面如果有 两对 <br/><br/> 则删除一对
            Dim findString = "<br><br>" & vbLf & brPair
            If findString = sb.Substring(sb.Length - findString.Length, findString.Length) Then
                sb.Remove(sb.Length - (vbLf & brPair).Length, (vbLf & brPair).Length)
            End If
            findString = "<br><br><br/><br/>"
            If findString = sb.Substring(sb.Length - findString.Length, findString.Length) Then
                sb.Remove(sb.Length - brPair.Length, brPair.Length)
            End If

            message = StringBuilderCache.GetStringAndReleaseBuilderSuper(sb)
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try

        Return message
    End Function

    ''' <summary>
    ''' 保存观看的直播间信息到数据库以备下次打开时读取
    ''' </summary>
    Private Shared Function MakeStoreLiveRoomInfoSQL(ByVal danmuElements As HtmlElementCollection) As String
        Dim message = GetCurrentDanmuTop100(danmuElements)
        Return MakeStoreLiveRoomInfoSQLInternal(message)
    End Function

    Private Shared Function MakeStoreLiveRoomInfoSQLInternal(ByVal message As String) As String
        If User.ViewRoom Is Nothing Then Return String.Empty

        Dim msgHex = If(message.IsNullOrEmpty,
            $"(select DanmuTop100 from ViewedRoomInfo where RealId = {User.ViewRoom.RealId})",
            $"'{message.ToHexString(UpperLowerCase.Lower, True)}'")

        Dim sql = $"INSERT
                    OR REPLACE INTO ViewedRoomInfo (
	                    RealId,
                        ShortId,
                        UpName,
                        lastAttentionCount,
                        DanmuTop100,
                        LastViewedTimestamp
                    )
                    VALUES
                        (
                        {User.ViewRoom.RealId},
                        {User.ViewRoom.ShortId},
                        '{User.ViewRoom.UserNick}',
                        {User.ViewRoom.Attention.ToStringOfCulture},
                        {msgHex},
                        {Date.Now.ToTimeStampString(TimePrecision.Second)}
                        );"

        Return sql
    End Function


    Private Shared Function MakeStoreUserInfoSQL() As String
        If User.Cookies Is Nothing OrElse
            User.Id Is Nothing Then
            Return String.Empty
        End If

        Dim cookiesKvp = If(User.StoreCookies, User.Cookies.EncryptCookies, String.Empty)

        Dim sql = $"INSERT
                    OR REPLACE INTO UserInfo (
	                    Id,
	                    Nick,
	                    Cookies,
	                    SignDate,
	                    SignRewards,
	                    StoreCookies,
	                    LastViewedRoomRealId,
                        RoomShortId,
                        RoomRealId
                    )
                    VALUES
	                    (
		                {User.Id},
		                '{User.Nick}',
		                '{cookiesKvp}',
		                '{User.SignDate}',
		                '{User.SignRewards}',
		                {CInt(User.StoreCookies)},
		                {User.ViewedRoomId},
                        {User.RoomShortId},
                        {User.RoomRealId}
	                    );"

        Return sql
    End Function

    ''' <summary>
    ''' 获取个性化配置
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <param name="viewRoomId"></param>
    ''' <returns></returns>
    Protected Friend Shared Async Function GetPersonglConfigAsync(ByVal userId As String, ByVal viewRoomId As String) As Task
        Dim sql = $"select Personal from Config where UserId = {If(userId.IsNullOrEmpty, "-1", userId)} and ViewedRoomId = {If(viewRoomId.IsNullOrEmpty, "-1", viewRoomId)} "
        Using reader = Await IO2.Database.SQLiteHelper.GetDataReaderAsync(sql)
            If reader?.Read Then
                Configment = MSJsSerializer.Deserialize(Of ConfigEntity)(reader(0).ToString)
            End If
        End Using

        If Configment Is Nothing Then
            Configment = New ConfigEntity
        End If
    End Function

    Private Shared Function MakeStorePersonalConfigSQL() As String
        'User.Id = "52155851"
        If User.Id.IsNullOrEmpty Then Return String.Empty

        Dim sql = $"INSERT
                    OR REPLACE INTO Config (
	                    UserId,
                        ViewedRoomId,
	                    Personal
                    )
                    VALUES
	                    (
		                {User.Id},
                        {If(User.ViewRoom Is Nothing, "-1", User.ViewRoom.RealId)},
		                '{MSJsSerializer.Serialize(Configment)}'
	                    );"

        Return sql
    End Function

    Private Shared Function MakeStoreMedalConfigSQL() As String
        If User.ViewRoom?.Medal Is Nothing Then Return String.Empty

        Dim sql = $"INSERT
                    OR REPLACE INTO Medal (
	                    UpUserId,
                        UpUserNick,
	                    Detail
                    )
                    VALUES
	                    (
		                {User.ViewRoom.UserId},
                        '{User.ViewRoom.UserNick}',
		                '{User.ViewRoom.Medal.Detail.Serialize}'
	                    );"

        Return sql
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Protected Friend Shared Async Function TryLoginAsync() As Task(Of LoginResult)
        Open()
        Return Await m_LoginManager.TryLoginAsync()
    End Function

    ''' <summary>
    ''' 配置各种姬入口
    ''' </summary>
    ''' <returns></returns>
    Protected Friend Shared Async Function ConfigureHimesAsync() As Task
        Dim funcRst As Boolean

        Try
            Debug.Print(Logger.MakeDebugString("配置 DanmuEntry 开始"))
            If LoginManager.NotLoginUserId = User.Id Then
                ConfigureHimesByVisitorAsync()
            ElseIf User.RoomShortId <= 0 AndAlso User.RoomRealId <= 0 Then
                Dim rst = Await ConfigureHimesByUserAsync()
                If Not rst Then Exit Try
            End If

            Dim tsk = Await GetRoomRealIdAsync(User.ViewedRoomId.ToStringOfCulture)
            Dim viewRoomRealId = tsk.RoomRealId
            If viewRoomRealId <= 0 Then
                funcRst = False
                Return
            End If
            ' 如果现在浏览的直播间Id跟用现在登录用户的信息去获取到的直播间Id（短Id或者真实Id）一样，
            ' 表示现在登录的用户进入自己的直播间
            ' 否则就是作为观众去观看别人的直播
            User.Role = If(User.ViewedRoomId = User.RoomShortId OrElse viewRoomRealId = User.RoomRealId,
                UserRole.Uper,
                If(LoginManager.NotLoginUserId = User.Id, UserRole.Visitor, UserRole.Viewer))

            ' 获取当前观看的直播间信息
            Dim getRoomInfoRst = Await GetViewRoomInfoAsync(viewRoomRealId.ToStringOfCulture)
            User.ViewRoom = getRoomInfoRst.Room
            funcRst = getRoomInfoRst.Success
            If funcRst Then
                If User.ViewedRoomId <> viewRoomRealId Then
                    User.ViewedRoomId = viewRoomRealId
                End If
            Else
                Return
            End If

            RaiseEvent RoomRealIdEnsured(Nothing, New EventArgs())
        Catch ex As Exception
            Logger.WriteLine(ex)
        Finally
            Debug.Print(Logger.MakeDebugString($"配置 DanmuEntry {If(funcRst, "成功", "失败")}"))
        End Try
    End Function

    ''' <summary>
    ''' 通过用户（已登录）
    ''' </summary>
    Private Shared Async Function ConfigureHimesByUserAsync() As Task(Of Boolean)
        ' 获取当前登录用户信息
        Dim getRst = Await BilibiliApi.GetCurrentUserInfoAsync
        Dim json = getRst.Message
        Dim funcRst = getRst.Success

        If Not funcRst Then Return False
        'room_id 5096
        'short_id    388

        ' "uid":52155851,"uname":"\u5c0f\u6bcd\u732a\u4e0e90\u540e\u5927\u53d4"
        ' "roomid":"4236342"
        Dim pattern = """uid"":(\d+),""uname"":""(.*?)"".*?""roomid"":""?(\d+)""?"
        Dim match = Regex.Match(json, pattern, RegexOptions.IgnoreCase Or RegexOptions.Compiled)
        If Not match.Success Then
            Return False
        End If

        User.Id = match.Groups(1).Value
        User.Nick = match.Groups(2).Value.TryUnescape
        User.RoomShortId = match.Groups(3).Value.ToIntegerOfCulture

        Dim taskGetRealRoomId = Await GetRoomRealIdAsync(User.RoomShortId.ToStringOfCulture)
        User.RoomRealId = taskGetRealRoomId.RoomRealId

        Return True
    End Function

    ''' <summary>
    ''' 通过游客（未登录）配置himes
    ''' </summary>
    Private Shared Sub ConfigureHimesByVisitorAsync()
        User.Nick = "游客"

    End Sub

    ''' <summary>
    ''' 尝试签到
    ''' </summary>
    ''' <returns>如果已经签到，那么就直接返回,否则执行签到操作</returns>
    Protected Friend Shared Async Function TrySignAsync() As Task
        ' 如果已经签到或者是用户设置为不自动签到，那就直接返回
        If Not Configment.SignAuto OrElse User.IsSigned Then Return

        Dim getRst = Await GetSignInfoAsync()
        If getRst.Status = 1 Then
            ' 已经签到那就记住签到信息
            User.SignDate = Date.Now.Date
            User.SignRewards = getRst.SignRewards
            Return
        End If

        getRst = Await SignInternalAsync()
        If getRst.Status = 1 Then
            ' 已经签到那就记住签到信息
            User.SignDate = Date.Now.Date
            User.SignRewards = getRst.SignRewards
        End If
    End Function

    ''' <summary>
    ''' 获取签到状态
    ''' </summary>
    ''' <returns>已签到返回 1，未签到返回 0，未知返回 -1</returns>
    Private Shared Async Function GetSignInfoAsync() As Task(Of (Status As Integer, SignRewards As String))
        Dim getRst = Await BilibiliApi.GetSignInfoAsync
        Dim json = getRst.Message
        Dim funcRst = getRst.Success

        Dim status = -1
        Dim signRewards = String.Empty

        If funcRst Then
            ' {"code":0,"msg":"ok","message":"ok","data":{"text":"","specialText":"","status":0,"allDays":31,"curMonth":7,"curYear":2018,"curDay":9,"curDate":"2018-7-9","hadSignDays":6,"newTask":0,"signDaysList":[1,4,5,6,7,8],"signBonusDaysList":[7]}}
            Dim pattern = """text"":""(.*?)"",""specialText"":""(.*?)"",""status"":(\d)"
            Dim match = Regex.Match(json, pattern, RegexOptions.IgnoreCase Or RegexOptions.Compiled)
            If Not match.Success Then Return (-1, "")

            Dim todayRewards = match.Groups(1).Value
            Dim futureRewards = match.Groups(2).Value

            signRewards = "今日收获：" & todayRewards & If(futureRewards.Length > 0, Environment.NewLine & futureRewards, "")
            status = match.Groups(3).Value.ToIntegerOfCulture
        End If

        Return (status, signRewards)
    End Function

    ''' <summary>
    ''' 签到
    ''' </summary>
    ''' <returns></returns>
    Private Shared Async Function SignInternalAsync() As Task(Of (Status As Integer, SignRewards As String))
        Dim opRst = Await BilibiliApi.SignAsync
        Dim json = opRst.Message
        Dim funcRst = opRst.Success

        Dim status = -1
        Dim signRewards = String.Empty

        If funcRst Then
            ' {"code":0,"msg":"OK","message":"OK","data":{"text":"3000用户经验值,2辣条","specialText":"(再签到4天可以获得3天月费老爷)","allDays":31,"hadSignDays":6,"isBonusDay":0}}
            Dim pattern = """message"":""(.*?)"",""data"":{""text"":""(.*?)"",""specialText"":""(.*?)"""
            Dim match = Regex.Match(json, pattern, RegexOptions.IgnoreCase Or RegexOptions.Compiled)
            If Not match.Success Then Return (-1, "")

            Dim message = match.Groups(1).Value
            If "ok".Equals(message, StringComparison.OrdinalIgnoreCase) Then
                Dim todayRewards = match.Groups(2).Value
                Dim futureRewards = match.Groups(3).Value

                signRewards = "今日收获：" & todayRewards & Environment.NewLine & futureRewards
                status = 1
            Else
                status = 0
            End If
        End If

        Return (status, signRewards)
    End Function

    ''' <summary>
    ''' 尝试领取双端观看直播奖励
    ''' </summary>
    ''' <returns>如果已经领取，那么就直接返回,否则执行领取操作</returns>
    Protected Friend Shared Async Function TryReceiveDoubleWatchAwardAsync() As Task
        ' 如果已经领取或者是用户设置为不自动领取，那就直接返回
        If Not Configment.ReceiveDoubleWatchAwardAuto OrElse User.IsReceivedDoubleWatchAward Then Return

        Dim status = Await GetDoubleWatchStatusAsync()
        If 0 = status OrElse 2 = status Then
            User.IsReceivedDoubleWatchAward = True
            Return
        End If

        Dim receiveRst = Await BilibiliApi.ReceivedDoubleWatchAsync()
        User.IsReceivedDoubleWatchAward = receiveRst
    End Function

    ''' <summary>
    ''' 获取领取双端观看直播奖励状态
    ''' </summary>
    ''' <returns>已领取返回 2，未可领取返回 0，可领取但未领取返回1，未知返回 -1</returns>
    Private Shared Async Function GetDoubleWatchStatusAsync() As Task(Of Integer)
        Dim getRst = Await BilibiliApi.GetDoubleWatchStatusAsync
        Dim json = getRst.Message
        Dim funcRst = getRst.Success

        Dim status = -1

        If funcRst Then
            ' "double_watch_info":{"task_id":"double_watch_task","status":2,"web_watch":1,"mobile_watch":1
            Dim pattern = """task_id"":""double_watch_task"",""status"":(\d)"
            Dim match = Regex.Match(json, pattern, RegexOptions.IgnoreCase Or RegexOptions.Compiled)
            If Not match.Success Then Return -1

            status = match.Groups(1).Value.ToIntegerOfCulture
        End If

        Return status
    End Function

    Protected Friend Shared Async Function GetFansNickAsync(ByVal userId As String) As Task(Of String)
        Dim funcRst = String.Empty

        Try
            Dim postRst = Await BilibiliApi.GetMemberInfoAsync(userId)
            If Not postRst.Success Then Exit Try
            Dim json = postRst.Message

            ' "mid":44053783,"name":"\u516e\u4e36\u53f6"
            Dim pattern = """name"":""((?:\\\w+)+)"""
            Dim match = Regex.Match(json, pattern, RegexOptions.IgnoreCase Or RegexOptions.Compiled)
            If Not match.Success Then Exit Try

            funcRst = match.Groups(1).Value.TryUnescape
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try

        Return funcRst
    End Function

    Private Shared Async Sub m_LoginManager_UserEnsuredAsync(sender As Object, e As LoginManager.UserEnsuredEventArgs) Handles m_LoginManager.UserEnsured
        Await GetPersonglConfigAsync(e.UserId, e.ViewRoomId)
        RaiseEvent UserEnsured(sender, New Events.UserEnsuredEventArgs(Configment.MainForm, e.LoginResult))
    End Sub

    ''' <summary>
    ''' 直播间信息发生改变
    ''' </summary>
    Public Shared Sub OnLiveRoomInfoChanged()
        RaiseEvent LiveRoomInfoChanged(Nothing, New LiveRoomInfoChangedEventArgs(User.ViewRoom))
    End Sub

    ''' <summary>
    ''' 添加备注
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    Public Shared Function AddRemark(ByVal userId As String, ByVal userName As String, ByVal remark As String) As Boolean
        ' 重复则不再插入或者更新
        Dim sql = $"INSERT
                OR REPLACE INTO ViewerRemark (UserId, UserNick, Remark) SELECT
	                {userId.ToIntegerOfCulture},
	                '{userName}',
	                '{remark}'
                WHERE
	                NOT EXISTS (
		                SELECT
			                UserNick
		                FROM
			                ViewerRemark
		                WHERE
			                UserId = {userId.ToIntegerOfCulture}
		                AND UserNick = '{userName}'
		                AND Remark = '{remark}'
	                )"
        Return IO2.Database.SQLiteHelper.ExecuteNonQuery(sql)
    End Function

    ''' <summary>
    ''' 删除备注
    ''' </summary>
    ''' <param name="userId"></param>
    ''' <returns></returns>
    Public Shared Function DeleteRemark(ByVal userId As String) As Boolean
        Dim sql = "delete from viewerRemark where userId = " & userId
        Return IO2.Database.SQLiteHelper.ExecuteNonQuery(sql)
    End Function

    ''' <summary>
    ''' 根据用户Id或者用户昵称获取用户信息（用户Id跟用户昵称）,viewerId可有一个为 String.Empty
    ''' </summary>
    ''' <param name="viewerId"></param>
    ''' <param name="viewerIdOrViewerName"></param>
    ''' <returns></returns>
    Public Shared Async Function GetUserInfoAsync(ByVal viewerId As String, ByVal viewerIdOrViewerName As String) As Task(Of (Uid As String, Uname As String))
        ' 用用户输入的uidOrUname去获取uid和uname
        ' 1.假如全部是数字，那就先假设是用户id，用用户id去查看用户信息，如果查到匹配信息，那就判定为用户id，否则假设为昵称，同上
        ' 2.非全数字的时候，直接当做是用户昵称去查。
        ' a.为了速度， 只查第一页， 找不到完全匹配的信息,提示用户输入更全的信息；
        ' b.查到匹配，再获取uid
        If viewerId.Length > 0 Then
            Return Await InternalGetUserIfnoAsync(viewerId)
        ElseIf Regex.IsMatch(viewerIdOrViewerName, "^\d+$") Then
            Return Await InternalGetUserIfnoAsync(viewerIdOrViewerName)
        Else
            ' 只搜索第一页
            Dim getRst = Await BilibiliApi.SearchUserAsync(viewerIdOrViewerName)
            If getRst.Success Then
                ' 必须完全匹配昵称才算正确结果
                'Dim pattern = $"""uname"":""(?<uname>{viewerIdOrViewerName}|{Regex.Escape(viewerIdOrViewerName.TryToUnicode(UpperLowerCase.Lower, True, "\u"))})"",.*?""mid"":(?<mid>\d+)"
                'Return ParseUserInfo(pattern, getRst.Message)
                Try
                    Dim root = MSJsSerializer.Deserialize(Of UserSearchResultEntity.Root)(getRst.Message)
                    If root Is Nothing Then Return (String.Empty, String.Empty)
                    If root.data.numResults = 0 Then Return (String.Empty, String.Empty)
                    For Each r In root.data.result
                        If viewerIdOrViewerName = r.uname Then
                            Return (r.mid.ToStringOfCulture, r.uname)
                        End If
                    Next
                Catch ex As Exception
                    Logger.WriteLine(ex)
                End Try
            End If

            Return (String.Empty, String.Empty)
        End If
    End Function

    Private Shared Async Function InternalGetUserIfnoAsync(ByVal viewerId As String) As Task(Of (Uid As String, Uname As String))
        Dim getRst = Await BilibiliApi.GetMemberInfoAsync(viewerId)
        If getRst.Success Then
            Try
                Dim root = MSJsSerializer.Deserialize(Of UserInfoEntity.Root)(getRst.Message)
                If root Is Nothing Then Return (String.Empty, String.Empty)
                'Dim pattern = """mid"":(?<mid>\d+),""name"":""(?<uname>.*?)"""
                'Return ParseUserInfo(pattern, getRst.Message)
                Return (root.data.mid.ToStringOfCulture, root.data.name)
            Catch ex As Exception
                Logger.WriteLine(ex)
            End Try
        End If

        Return (String.Empty, String.Empty)
    End Function

    Private Shared Function ParseUserInfo(ByVal pattern As String, ByVal html As String) As (Uid As String, Uname As String)
        Dim match = Regex.Match(html, pattern, RegexOptions.IgnoreCase Or RegexOptions.Compiled)
        Return If(match.Success,
            (match.Groups("mid").Value, match.Groups("uname").Value.TryUnescape),
            (String.Empty, String.Empty))
    End Function

#End Region
End Class
