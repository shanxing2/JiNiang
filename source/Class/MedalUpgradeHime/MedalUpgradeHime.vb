Imports 姬娘插件.Events
Imports ShanXingTech
Imports System.Text.RegularExpressions
Imports ShanXingTech.Text2

Public NotInheritable Class MedalUpgradeHime
    ''' <summary>
    ''' 从数据库中获取缓存的勋章信息
    ''' </summary>
    ''' <param name="upUid">播主的用户Id</param>
    ''' <returns></returns>
    Public Shared Async Function GetMedalFromDbAsync(ByVal upUid As Integer) As Task(Of MedalInfo)
        Dim medal As New MedalInfo(upUid)
        If medal.Detail Is Nothing Then
            medal.Detail = New Concurrent.ConcurrentStack(Of MedalInfo.Datail)
        End If
        Dim sql = $"SELECT UpUserId,UpUserNick,Detail FROM Medal where UpUserId = {upUid.ToStringOfCulture}"
        Using reader = Await IO2.Database.SQLiteHelper.GetDataReaderAsync(sql)
            While Await reader.ReadAsync
                medal.UpUnick = CStr(reader(1))
                Dim detail = If(DBNull.Value.Equals(reader(2)), String.Empty, CStr(reader(2)))
                If detail.IsNullOrEmpty OrElse "[]" = detail Then Exit While

                ' list 不能直接转为 Concurrent.ConcurrentStack
                Dim detailArr = MSJsSerializer.Deserialize(Of MedalInfo.Datail())(detail)
                If detailArr.Length = 0 Then Exit While
                medal.Detail.PushRange(detailArr)
                Exit While
            End While
        End Using

        'medal.Detail.Push(New MedalInfo.Datail With {.ViewerUid = 11, .ViewerUnick = "11", .Level = 11})
        'medal.Detail.Push(New MedalInfo.Datail With {.ViewerUid = 22, .ViewerUnick = "22", .Level = 22})
        'medal.Detail.TryPop(New MedalInfo.Datail)

        Return medal
    End Function

    ''' <summary>
    ''' 尝试检测（<paramref name="fedInfo"/>中的观众）勋章升级
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="upUid"></param>
    ''' <param name="fedInfo"></param>
    ''' <returns></returns>
    Public Shared Async Function TryCheckMedalUpgradeAsync(Of T As FedEventArgs)(ByVal upUid As String, fedInfo As T) As Task(Of (Upgraded As Boolean, Danmu As String))
        If fedInfo.ViewerUid.IsNullOrEmpty OrElse fedInfo.ViewerUnick.IsNullOrEmpty Then
            Return (False, Nothing)
        End If

        ' 从网络获取勋章信息
        Dim getRst = Await BilibiliApi.GetMedalInfoAsync(fedInfo.ViewerUid, upUid)
        If Not getRst.Success Then
            IO2.Writer.WriteText($".\medal_{Now.ToString("yyyyMMdd")}.txt", $"从网络获取勋章信息失败,{NameOf(upUid)}:{upUid} {NameOf(fedInfo.ViewerUid)}:{fedInfo.ViewerUid} {NameOf(fedInfo.ViewerUnick)}:{fedInfo.ViewerUnick}", IO.FileMode.Append, IO2.CodePage.UTF8)
            Return (False, Nothing)
        End If

        ' 没有勋章 {"code":0,"msg":"","message":"","data":[]}
        If Regex.IsMatch(getRst.Message, """data"":\[\]", RegexOptions.Compiled Or RegexOptions.IgnoreCase Or RegexOptions.RightToLeft) Then
            Return (False, Nothing)
        End If

        Dim root = MSJsSerializer.Deserialize(Of ViewerMedalEntity.Root)(getRst.Message)
        If root Is Nothing Then
            Return (False, Nothing)
        End If

        Dim danmu As String = Nothing
        Dim upgraded As Boolean
        Dim levelFromCache = -1
        ' 升级后有剩余记到 +1级
        If root.data.today_intimacy > root.data.intimacy OrElse
            If(fedInfo.Price = 100, fedInfo.Count, CInt((fedInfo.Count * fedInfo.Price) / 100)) > root.data.intimacy Then
            ' 从本地数据库读取用户勋章信息（如有）
            levelFromCache = GetMedalLevelFromCache(fedInfo.ViewerUid)
            ' 同级表示已经提示过
            If levelFromCache > 0 Then
                upgraded = root.data.level > levelFromCache
                If upgraded Then
                    danmu = MakeMedalUpgradeDanmu(fedInfo.ViewerUnick, root.data.medal_name, root.data.level)
                    TryAddOrUpdateMedal(fedInfo.ViewerUid, fedInfo.ViewerUnick, root.data.level)
#If DEBUG Then

                Else
                    IO2.Writer.WriteText($".\medal_{Now.ToString("yyyyMMdd")}.txt",
                                         $"已经提示过升级,{NameOf(upUid)}:{upUid} {NameOf(fedInfo.ViewerUid)}:{fedInfo.ViewerUid} {NameOf(fedInfo.ViewerUnick)}:{fedInfo.ViewerUnick} {NameOf(root.data.level)}:{root.data.level}  {NameOf(fedInfo.Count)}:{fedInfo.Count}  {NameOf(fedInfo.Price)}:{fedInfo.Price}",
                                         IO.FileMode.Append,
                                         IO2.CodePage.UTF8)
#End If
                End If

                Return (upgraded, danmu)
            Else
                TryAddOrUpdateMedal(fedInfo.ViewerUid, fedInfo.ViewerUnick, root.data.level)
                Return (False, Nothing)
            End If
        End If

        ' 从本地数据库读取用户勋章信息（如有）
        If levelFromCache = -1 Then
            levelFromCache = GetMedalLevelFromCache(fedInfo.ViewerUid)
        End If
        ' 利用等级判断升级
        upgraded = levelFromCache > 0 AndAlso root.data.level > levelFromCache
        If upgraded Then
            danmu = MakeMedalUpgradeDanmu(fedInfo.ViewerUnick, root.data.medal_name, root.data.level)
        End If

        TryAddOrUpdateMedal(fedInfo.ViewerUid, fedInfo.ViewerUnick, root.data.level)

        Return (upgraded, danmu)
    End Function

    Private Shared Function MakeMedalUpgradeDanmu(ByVal viewerUname As String, ByVal medalName As String, ByVal medalLevel As Integer) As String
        Dim m_DanmuBuilder = StringBuilderCache.Acquire(50)
        m_DanmuBuilder.AppendFormat("恭喜 {0} 的勋章[{1}]升级到{2}级~", viewerUname, medalName, medalLevel.ToStringOfCulture)
        Dim m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)

#If DEBUG Then
        IO2.Writer.WriteText($".\medal_{Now.ToString("yyyyMMdd")}.txt", m_Danmu, IO.FileMode.Append, IO2.CodePage.UTF8)
#End If

        Return m_Danmu
    End Function

    Private Shared Function GetMedalLevelFromCache(ByVal viewerUid As String) As Integer
        Dim levelFromCache As Integer
        If DanmuEntry.User.ViewRoom.Medal.Detail.Count = 0 Then
            levelFromCache = 0
        Else
            Dim medalFromCache = DanmuEntry.User.ViewRoom.Medal.Detail.FirstOrDefault(Function(m) viewerUid.ToIntegerOfCulture = m.ViewerUid)

            levelFromCache = If(medalFromCache Is Nothing, 0, medalFromCache.Level)
        End If

        Return levelFromCache
    End Function

    Private Shared Sub TryAddOrUpdateMedal(ByVal viewerUid As String, ByVal viewerUname As String, ByVal levelFromNet As Integer)
        Dim medal = DanmuEntry.User.ViewRoom.Medal.Detail.FirstOrDefault(Function(m) viewerUid.ToIntegerOfCulture = m.ViewerUid)
        If medal Is Nothing Then
            medal = New MedalInfo.Datail With {
                .ViewerUid = viewerUid.ToIntegerOfCulture,
                .ViewerUnick = viewerUname,
                .Level = levelFromNet
            }
            DanmuEntry.User.ViewRoom.Medal.Detail.Push(medal)
        Else
            If medal.Level <> levelFromNet Then medal.Level = levelFromNet
        End If
    End Sub
End Class
