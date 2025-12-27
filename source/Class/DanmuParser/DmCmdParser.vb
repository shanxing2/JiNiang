Imports System.Text
Imports ShanXingTech
Imports ShanXingTech.Text2
Imports 姬娘插件.Events
Imports 姬娘插件.Utils

''' <summary>
''' 解析服务器推送过来的各种CMD命令 <see cref=" DmCmd"/>
''' </summary>
Public Class DmCmdParser
    Implements IDisposable

#Region "事件区"
    ''' <summary>
    ''' 弹幕内容改变事件 有弹幕更新时触发
    ''' </summary>
    Public Event ChatHistoryChanged As EventHandler(Of DanmuChangedEventArgs)
    ''' <summary>
    ''' 直播间信息更新
    ''' </summary>
    Public Event LiveRoomInfoChanged As EventHandler(Of LiveRoomInfoChangedEventArgs)
    ''' <summary>
    ''' 小时榜排名改变事件 更新时触发
    ''' </summary>
    Public Event HourRankChanged As EventHandler(Of HourRankChangedEventArgs)
    ''' <summary>
    ''' 在线人数改变事件 
    ''' </summary>
    Public Event OnlineChanged As EventHandler(Of OnlineChangedEventArgs)
    ''' <summary>
    ''' 关注人数改变事件
    ''' </summary>
    Public Event AttentionCountChanged As EventHandler(Of AttentionCountChangedEventArgs)
    ''' <summary>
    ''' 粉丝增加事件
    ''' </summary>
    Public Event AttentionIncreased As EventHandler(Of AttentionIncreasedEventArgs)
    ''' <summary>
    ''' 直播状态改变事件
    ''' </summary>
    Public Event LiveStatusChanged As EventHandler(Of LiveStatusChangedEventArgs)
    ''' <summary>
    ''' 禁言状态改变事件
    ''' </summary>
    Public Event DanmuSendEnabledChanged As EventHandler(Of DanmuSendEnabledEventArgs)
    ''' <summary>
    '''银瓜子投喂事件 有投喂时触发
    ''' </summary>
    Public Event SilverFed As EventHandler(Of FedEventArgs)
    ''' <summary>
    '''金瓜子投喂事件 有投喂时触发
    ''' </summary>
    Public Event GoldFed As EventHandler(Of FedEventArgs)
    ''' <summary>
    '''抽奖开始事件 有抽奖时触发
    ''' </summary>
    Public Event RaffleStart As EventHandler(Of FedEventArgs)
    ''' <summary>
    ''' 检查勋章升级，需要检查时（投喂礼物）触发
    ''' </summary>
    Public Event MedalUpgradeChecking As EventHandler(Of FedEventArgs)
    ''' <summary>
    ''' 领取勋章时发生
    ''' </summary>
    Public Event MedalGained As EventHandler(Of FedEventArgs)

    ''' <summary>
    ''' 老爷(年费/月费)进入直播间时触发
    ''' </summary>
    Public Event VipEntered As EventHandler(Of VipEnteredEventArgs)
    ''' <summary>
    ''' 守护（舰长、提督、总督）进入直播间时触发
    ''' </summary>
    Public Event GuardEntered As EventHandler(Of GuardEnteredEventArgs)
    ''' <summary>
    ''' 观众进入直播间时触发
    ''' </summary>
    Public Event ViewerEntered As EventHandler(Of ViewerEnteredEventArgs)
    ''' <summary>
    ''' 接收系统消息事件
    ''' </summary>
    Public Event SystemMessageChanged As EventHandler(Of SystemMessageChangedEventArgs)
    ''' <summary>
    ''' 进入直播间成功事件 进入直播间成功时触发
    ''' </summary>
    Public Event JoinLiveRoomSucceeded As EventHandler(Of JoinLiveRoomSucceededEventArgs)
    '''' <summary>
    '''' 接收到服务器推送过来的信息事件（接收到所有信息（包括弹幕、送礼、人气等）都会引发此事件）
    '''' </summary>
    'Public Shared Event Received As EventHandler(Of DanmuReceivedEventArgs)
    ''' <summary>
    ''' 直播间观众被禁言时触发
    ''' </summary>
    Public Event RoomViewerBlocked As EventHandler(Of RoomViewerBlockedEventArgs)
    Public Event RoomViewerUnBlocked As EventHandler(Of RoomViewerBlockedEventArgs)

    ''' <summary>
    ''' 直播间观众被任命为管理员时触发
    ''' </summary>
    Public Shared Event RoomAdminAppointed As EventHandler(Of RoomAdminAppointedEventArgs)
    ''' <summary>
    ''' 直播间标题/分区改变时触发
    ''' </summary>
    Public Shared Event RoomChanged As EventHandler(Of RoomChangedEventArgs)

#End Region

#Region "字段区"
    Private ReadOnly m_DanmuBuilder As StringBuilder
    Private m_Danmu As String
    Private ReadOnly m_LogDic As Dictionary(Of String, String)
#End Region
#Region "属性区"
    Public Property DanmuFormatDic As Dictionary(Of String, DanmuFormatEntity.FormatInfo)
#End Region

#Region "构造函数区"
    ''' <summary>
    ''' 这个无参数的构造函数只应在测试的时候使用
    ''' </summary>
    Sub New()

    End Sub

    Sub New(ByVal danmuFormatDic As Dictionary(Of String, DanmuFormatEntity.FormatInfo))
        Me.DanmuFormatDic = danmuFormatDic
        m_DanmuBuilder = StringBuilderCache.Acquire(108)
        m_LogDic = New Dictionary(Of String, String)
        'm_TcpPacketReceivedBC = New Concurrent.BlockingCollection(Of DanmuReceivedEventArgs)
        'Task.Run(AddressOf TryPushReceivedLoop)
    End Sub
#End Region

#Region "IDisposable Support"
    ' 要检测冗余调用
    Private disposed As Boolean = False

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        ' 窗体内的控件调用Close或者Dispose方法时，isDisposed2的值为True
        If disposed Then Return

        ' TODO: 释放托管资源(托管对象)。
        If disposing Then

        End If

        ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
        ' TODO: 将大型字段设置为 null。

        disposed = True
    End Sub

    ' TODO: 仅当以上 Dispose(disposing As Boolean)拥有用于释放未托管资源的代码时才替代 Finalize()。
    Protected Overrides Sub Finalize()
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(False)
        MyBase.Finalize()
    End Sub

    ' Visual Basic 添加此代码以正确实现可释放模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(True)
        ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#Region "函数区"
    ''' <summary>
    ''' 非B站协议产生的弹幕事件中转
    ''' </summary>
    ''' <param name="eventName"></param>
    ''' <param name="EventArgs"></param>
    Public Shared Sub TransferDanmuEvent(Of T)(ByVal eventName As String, ByVal EventArgs As T)

        DanmuEventTransfer.Raise(eventName, EventArgs)
        'RaiseEvent RoomViewerUnBlocked(Nothing, EventArgs)
    End Sub

    ''' <summary>
    ''' 直播状态改变
    ''' </summary>
    ''' <param name="e"></param>
    Public Sub OnLiveStatusChanged(ByVal e As LiveStatusChangedEventArgs)
        RaiseEvent LiveStatusChanged(Nothing, e)
    End Sub

    ''' <summary>
    ''' 加入直播间成功
    ''' </summary>
    ''' <param name="e"></param>
    Public Sub OnJoinLiveRoomSucceeded(ByVal e As JoinLiveRoomSucceededEventArgs)
        RaiseEvent JoinLiveRoomSucceeded(Nothing, e)
    End Sub

    ''' <summary>
    ''' 有新的弹幕信息
    ''' </summary>
    ''' <param name="e"></param>
    Public Sub OnChatHistoryChanged(ByVal e As DanmuChangedEventArgs)
        RaiseEvent ChatHistoryChanged(Nothing, e)
    End Sub

    ''' <summary>
    ''' 人气值变化
    ''' </summary>
    ''' <param name="e"></param>
    Public Sub OnOnlineChanged(ByVal e As OnlineChangedEventArgs)
        RaiseEvent OnlineChanged(Nothing, e)
    End Sub

    ''' <summary>
    ''' 解析Cmd,所有Cmd请看 <see cref="DmCmd"/>
    ''' </summary>
    ''' <param name="cmdJson"></param>
    ''' <param name="userId">当前登录用户Id</param>
    ''' <param name="upId">Up Id</param>
    Public Sub ParseCmd(ByRef cmdJson As String, ByVal userId As String, ByVal upId As String)
        Dim cmd As DmCmd

        Try
            ' 因为接收到的信息复杂多变，没有固定的格式，所以就不用实体类来反序列化了 20180812
            Dim jDic = TryCast(MSJsSerializer.DeserializeObject(cmdJson), Dictionary(Of String, Object))
            If jDic Is Nothing Then
                Return
            End If

            Dim cmdValue As Object
#Disable Warning BC42030 ' 在为变量赋值之前，变量已被引用传递
            If jDic.TryGetValue("cmd", cmdValue) Then
#Enable Warning BC42030 ' 在为变量赋值之前，变量已被引用传递
                ' DANMU_MSG:4:0:2:2:2:0  20200604 B站调整
                If Not System.Enum.TryParse(cmdValue.ToString, True, cmd) AndAlso
                    "DANMU_MSG:4:0:2:2:2:0" <> cmdValue.ToString Then
                    TryAddUnKnownDmCmd(cmdValue.ToString, cmdJson)

                    Return
                End If
            Else
                Logger.WriteLine($"无CMD数据:{Environment.NewLine}{cmdJson}")
                Return
            End If

            'Debug.Print(ShanXingTech.Logger.MakeDebugString("接收到信息: " & body & Environment.NewLine))

            Select Case cmd
                Case DmCmd.DANMU_MSG
                    DANMU_MSG(userId, upId, jDic)
                Case DmCmd.SEND_GIFT
                    SEND_GIFT(jDic)
                Case DmCmd.COMBO_SEND
                    COMBO_SEND(jDic)
                Case DmCmd.COMBO_END
                    COMBO_END(jDic)
                Case DmCmd.GUARD_BUY
                    GUARD_BUY(jDic)
                Case DmCmd.SPECIAL_GIFT
                    SPECIAL_GIFT(jDic)
                Case DmCmd.TV_START
                    TV_START(jDic)
                Case DmCmd.RAFFLE_START
                    RAFFLE_START(jDic)
                Case DmCmd.RAFFLE_END, DmCmd.TV_END
                    RAFFLE_END(jDic)
                Case DmCmd.EVENT_CMD
                    EVENT_CMD(jDic)
                Case DmCmd.WISH_BOTTLE
                    WISH_BOTTLE(jDic)
                Case DmCmd.ROOM_RANK
                    ROOM_RANK(jDic)
                Case DmCmd.SYS_MSG
                    SYS_MSG(jDic, cmdJson)
                Case DmCmd.SYS_GIFT
                    SYS_GIFT(jDic, cmdJson)
                Case DmCmd.GUARD_MSG
                    GUARD_MSG(jDic, cmdJson)
                Case DmCmd.GUARD_LOTTERY_START
                    GUARD_LOTTERY_START(jDic)
                Case DmCmd.PREPARING
                    PREPARING()
                Case DmCmd.LIVE
                    LIVE()
                Case DmCmd.ROOM_SILENT_OFF
                    ROOM_SILENT_OFF()
                Case DmCmd.ROOM_SILENT_ON
                    ROOM_SILENT_ON(jDic)
                Case DmCmd.CUT_OFF
                    CUT_OFF(jDic)
                Case DmCmd.WELCOME
                    WELCOME(jDic)
                Case DmCmd.WELCOME_GUARD
                    WELCOME_GUARD(jDic)
                Case DmCmd.ENTRY_EFFECT
                    ENTRY_EFFECT(jDic)
                Case DmCmd.ROOM_BLOCK_MSG
                    ROOM_BLOCK_MSG(jDic)
                Case DmCmd.ROOM_ADMINS
                    ROOM_ADMINS(jDic)
                Case DmCmd.NOTICE_MSG
                    NOTICE_MSG(jDic)
                Case DmCmd.WARNING
                    WARNING(jDic)
                Case DmCmd.ACTIVITY_EVENT
                    ACTIVITY_EVENT(jDic)
                Case DmCmd.Room_Admin_Entrance
                    Room_Admin_Entrance(jDic)
                Case DmCmd.USER_TOAST_MSG
#Region "PK包未解析"
                    ' 包信息 .\需求相关\弹幕json\PK包.txt
                Case DmCmd.PK_MIC_END, DmCmd.PK_MATCH, DmCmd.PK_PRE, DmCmd.PK_START, DmCmd.PK_END, DmCmd.PK_PROCESS, DmCmd.PK_SETTLE, DmCmd.PK_CLICK_AGAIN, DmCmd.PK_AGAIN, DmCmd.PK_INVITE_INIT, DmCmd.PK_INVITE_SWITCH_CLOSE, DmCmd.PK_INVITE_SWITCH_OPEN, DmCmd.PK_INVITE_CANCEL, DmCmd.PK_BATTLE_START, DmCmd.PK_BATTLE_END, DmCmd.PK_BATTLE_CRIT, DmCmd.PK_BATTLE_ENTRANCE, DmCmd.PK_BATTLE_PRE, DmCmd.PK_BATTLE_PROCESS, DmCmd.PK_BATTLE_SETTLE, DmCmd.PK_BATTLE_SETTLE_USER, DmCmd.PK_CLICK_AGAIN, DmCmd.PK_LOTTERY_START


#End Region
                Case DmCmd.CHANGE_ROOM_INFO

                Case DmCmd.ROOM_REAL_TIME_MESSAGE_UPDATE

                Case DmCmd.ACTIVITY_MATCH_GIFT

                Case DmCmd.LOL_ACTIVITY

                Case DmCmd.NOTICE_MSG_H5

                Case DmCmd.DAILY_QUEST_NEWDAY

                Case DmCmd.ACTIVITY_BANNER_UPDATE

                Case DmCmd.ACTIVITY_BANNER_UPDATE_V2

                Case DmCmd.ACTIVITY_BANNER_UPDATE_BLS

                Case DmCmd.ROOM_CHANGE
                    ROOM_CHANGE(jDic)
                Case DmCmd.ACTIVITY_BANNER_RED_NOTICE_CLOSE
                Case DmCmd.HOUR_RANK_AWARDS
                Case DmCmd.new_anchor_reward,
                    DmCmd.ANCHOR_LOT_START,
                    DmCmd.ANCHOR_LOT_END,
                    DmCmd.ANCHOR_LOT_AWARD，
                     DmCmd.ANCHOR_NORMAL_NOTIFY
                Case DmCmd.HOT_ROOM_NOTIFY
                Case DmCmd.MATCH_TEAM_GIFT_RANK
                Case DmCmd.ROOM_SKIN_MSG
                Case DmCmd.SUPER_CHAT_MESSAGE
                Case DmCmd.SUPER_CHAT_MESSAGE_JPN
                Case DmCmd.WEEK_STAR_CLOCK
                Case DmCmd.STOP_LIVE_ROOM_LIST
                Case DmCmd.ONLINE_RANK_COUNT
                Case DmCmd.ONLINE_RANK_V2
                Case DmCmd.MESSAGEBOX_USER_GAIN_MEDAL
                    MESSAGEBOX_USER_GAIN_MEDAL(jDic)
                Case DmCmd.INTERACT_WORD
                    INTERACT_WORD(jDic)
                Case DmCmd.POPULARITY_RED_POCKET_START
                    POPULARITY_RED_POCKET_START(jDic)
                Case Else
                    TryAddUnKnownDmCmd(cmd, cmdJson)
                    Return
            End Select
        Catch ex As Exception
            TryAddUnKnownDmCmd(cmd, cmdJson)
        End Try

        'Debug.Print(Logger.MakeDebugString( $"弹幕:{cmd.ToString} {m_Danmu}{Environment.NewLine}"))
    End Sub

    Private Sub TryAddUnKnownDmCmd(ByVal dmCmd As DmCmd, ByVal cmdJson As String)
        TryAddUnKnownDmCmd(dmCmd.ToStringOfCulture, cmdJson)
    End Sub

    Private Sub TryAddUnKnownDmCmd(ByVal dmCmd As String, ByVal cmdJson As String)
        If m_LogDic.ContainsKey(dmCmd) Then Return

        Logger.WriteLine($"后台无此CMD类型，暂时无法解析:{Environment.NewLine}{cmdJson}")
        m_LogDic.Add(dmCmd, cmdJson)
    End Sub


#Region "具体CMD命令解析区"
    Private Sub DANMU_MSG(ByVal userId As String, ByVal upId As String, ByRef jDic As Dictionary(Of String, Object))
        m_DanmuBuilder.Clear()
        Dim ar1 = DirectCast(jDic("info")(0), Array)
        Dim ar2 = DirectCast(jDic("info")(2), Array)
        Dim ar3 = DirectCast(jDic("info")(3), Array)
        Dim ar4 = DirectCast(jDic("info")(4), Array)

        ' 每一组 空格分割
        ' "{总督/提督/舰长} {月费或者年费或者无} {房管或观众} {粉丝勋章 勋章等级} {活动头衔} {等级}{播主或自己或者观众昵称} {enviroment.newline}{时间}{enviroment.newline}{字幕内容}"
        m_DanmuBuilder.Append(If(3 = CInt(jDic("info")(7)), "[舰长] ", If(2 = CInt(jDic("info")(7)), "[提督] ", If(1 = CInt(jDic("info")(7)), "[总督] ", ""))))
        m_DanmuBuilder.Append(If(1 = CInt(ar2(4)), "[年] ", If(1 = CInt(ar2(3)), "[月] ", "")))
        'm_DanmuBuilder.Append(If(1 = CInt(ar2(2)), If(userId = CStr(ar2(0)), "", If(upId = CStr(ar2(0)), "", "[房管] ")), ""))
        m_DanmuBuilder.Append(If(1 = CInt(ar2(2)) AndAlso upId <> CStr(ar2(0)), "[房管] ", ""))
        m_DanmuBuilder.Append(If(ar3.Length = 0, "", "[" & ar3(1) & " " & ar3(0) & "] "))
        m_DanmuBuilder.Append("[UL ").Append(ar4(0)).Append("] ")
        ' 因为做span元素响应click时间有点麻烦，咱也不是搞前端的，所以先用button代替吧（默认span元素不响应click事件）
        Dim timeStamp = ar1(4).ToString
        Dim precision = TimePrecision.Second
        If timeStamp.Length = 13 Then
            precision = TimePrecision.Millisecond
        End If
        Dim dateTime = CLng(timeStamp).ToTimeStampString(precision, "yyyy-MM-dd HH:mm:ss")
        Dim danmu As Object
        Dim imgDanmu = ar1(13)
        If TypeOf imgDanmu Is String Then
            danmu = jDic("info")(1)
        Else
            Dim originalHeight = CInt(imgDanmu("height"))
            Dim originalWidth = CInt(imgDanmu("width"))
            Dim showWidth = (originalWidth * 30) / originalHeight
            danmu = $"<img src=""{imgDanmu("url")}""  height=""30"" width=""{showWidth}""/>"
        End If

        Dim checkInfo = DirectCast(jDic("info")(9), Dictionary(Of String, Object))
        ' 根据角色配置用户昵称显示样式
        If userId = CStr(ar2(0)) Then
            m_DanmuBuilder.AppendFormat(Common.UpOrOwnDanmuTemplete, "[自己]", dateTime, danmu)
        ElseIf userId = CStr(ar2(0)) Then
            m_DanmuBuilder.AppendFormat(Common.UpOrOwnDanmuTemplete, "[播主]", dateTime, danmu)
        Else
            m_DanmuBuilder.AppendFormat(Common.ViewerNickDanmuTemplete, ar2(0), checkInfo("ts"), checkInfo("ct"), ar2(1), dateTime, danmu)
        End If
        m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)
        m_Danmu = m_Danmu.HtmlDecode

        RaiseEvent ChatHistoryChanged(Nothing, New DanmuChangedEventArgs(m_Danmu))
    End Sub

    Private Sub SEND_GIFT(ByRef jDic As Dictionary(Of String, Object))
        ' {"cmd":"SEND_GIFT","data":{"giftName":"\u8fa3\u6761","num":10,"uname":"\u60f3\u9000\u5708\u7684\u4eba","face":"http:\/\/i1.hdslb.com\/bfs\/face\/6fae093aa8ca760c438383c4a60d0ad5971bc09f.jpg","guard_level":0,"rcost":266478202,"uid":17601387,"top_list":[],"timestamp":1550834560,"giftId":1,"giftType":0,"action":"\u5582\u98df","super":0,"super_gift_num":0,"price":100,"rnd":"923999176","newMedal":0,"newTitle":0,"medal":[],"title":"","beatId":"","biz_source":"live","metadata":"","remain":0,"gold":0,"silver":0,"eventScore":0,"eventNum":0,"smalltv_msg":[],"specialGift":null,"notice_msg":[],"capsule":null,"addFollow":0,"effect_block":1,"coin_type":"silver","total_coin":1000,"effect":0,"tag_image":"","user_count":0}}
        m_DanmuBuilder.Clear()
        m_DanmuBuilder.Append("蟹蟹 ").Append(jDic("data")("uname")).Append(" ").Append(jDic("data")("action")).Append(jDic("data")("giftName"))

        m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)
        Dim uname = CStr(jDic("data")("uname"))
        Dim fedEventArgs = New FedEventArgs(m_Danmu, CStr(jDic("data")("uid")), CStr(jDic("data")("uname"))， CInt(jDic("data")("num")), CInt(jDic("data")("price")), GiftUnit.Count)
        If "silver" = CStr(jDic("data")("coin_type")) Then
            RaiseEvent SilverFed(Nothing, fedEventArgs)
        Else
            RaiseEvent GoldFed(Nothing, fedEventArgs)
        End If
        RaiseEvent MedalUpgradeChecking(Nothing, fedEventArgs)
    End Sub

    Private Sub COMBO_SEND(ByRef jDic As Dictionary(Of String, Object))
        m_DanmuBuilder.Clear()

        ' {"cmd":"COMBO_SEND","data":{"uid":5189624,"uname":"纸涩","combo_num":9,"gift_name":"233","gift_id":8,"action":"赠送"}}
        m_DanmuBuilder.Append("蟹蟹 ").Append(jDic("data")("uname")).Append(" ").Append(jDic("data")("action")).Append(jDic("data")("gift_name"))
        m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)
        RaiseEvent GoldFed(Nothing, New FedEventArgs(m_Danmu, CInt(jDic("data")("combo_num")), GiftUnit.Combo))
    End Sub

    Private Sub COMBO_END(ByRef jDic As Dictionary(Of String, Object))
        'm_DanmuBuilder.Clear()
        ' 貌似官方推送的组合信息不太准确，所以不再响应
        ' n组礼物 {"cmd":"COMBO_END","data":{"uname":"21KGKG","r_uname":"扎双马尾的丧尸","combo_num":100,"price":100,"gift_name":"吃瓜","gift_id":20004,"start_time":1532071702,"end_time":1532071723,"guard_level":0}}
        'm_DanmuBuilder.Append("蟹蟹 ").Append(jDic("data")("uname")).Append(" 赠送").Append(jDic("data")("gift_name")).Append(" 共").Append(jDic("data")("combo_num")).Append("组")
        'm_Danmu= StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)
        'RaiseEvent GoldFeedChanged(Nothing, New GoldFeedChangedEventArgs(m_Danmu))
    End Sub

    Private Sub GUARD_BUY(ByRef jDic As Dictionary(Of String, Object))
        m_DanmuBuilder.Clear()
        ' 上船 总督/提督/舰长
        m_DanmuBuilder.Append(jDic("data")("username")).Append(" 购买 ").Append(If(3 = CInt(jDic("data")("guard_level")), "舰长", If(2 = CInt(jDic("data")("guard_level")), "提督", "总督"))).Append("船票")
        m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)
        RaiseEvent GoldFed(Nothing, New FedEventArgs(m_Danmu, CStr(jDic("data")("uid")), CStr(jDic("data")("username")), CInt(jDic("data")("num")), CInt(jDic("data")("price")), GiftUnit.Count))
    End Sub

    Private Sub SPECIAL_GIFT(ByRef jDic As Dictionary(Of String, Object))
        m_DanmuBuilder.Clear()
        ' 节奏风暴 节奏风暴最大持续时间为90秒，90秒内 前100的观众可以通过点击节奏风暴，发送节奏弹幕，发完会提前结束
        ' 接着收到 {"cmd":"SPECIAL_GIFT","data":{"39":{"id":313332,"action":"end"}}}
        If "end".Equals(CStr(jDic("data")("39")("action")), StringComparison.OrdinalIgnoreCase) Then
            m_Danmu = "节奏风暴结束"
        Else
            m_DanmuBuilder.Append("节奏风暴 ").Append(" X").Append(jDic("data")("39")("num")).Append(" 内容 ").Append(jDic("data")("39")("content")).Append(" ").Append(jDic("data")("39")("action"))
            m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)
        End If

        RaiseEvent ChatHistoryChanged(Nothing, New DanmuChangedEventArgs(m_Danmu))
    End Sub

    Private Sub TV_START(ByRef jDic As Dictionary(Of String, Object))
        ' 小电视飞船抽奖
        m_Danmu = CStr(jDic("data")("title")) & "开始 蟹蟹 " & CStr(jDic("data")("from"))
        RaiseEvent RaffleStart(Nothing, New FedEventArgs(m_Danmu, -1, GiftUnit.None))
    End Sub

    Private Sub RAFFLE_START(ByRef jDic As Dictionary(Of String, Object))
        m_DanmuBuilder.Clear()
        ' 摩天大楼 抽奖开始
        ' 摩天大楼 是送给当前直播间的 
        ' DmCmd.SYS_MSG，DmCmd.SYS_GIFT 类型的消息 是送给其他直播间的
        m_DanmuBuilder.Append(jDic("data")("title")).Append("开始, 心中默念主播名字可让人品爆发, 蟹蟹 ").Append(jDic("data")("from")).Append(" 的赠送 ")
        m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)
        RaiseEvent RaffleStart(Nothing, New FedEventArgs(m_Danmu, -1, GiftUnit.None))
    End Sub

    Private Sub RAFFLE_END(ByRef jDic As Dictionary(Of String, Object))
        m_DanmuBuilder.Clear()
        ' "mobileTips":"恭喜 白阿姨家的路人 获得2.3333w银瓜子"
        ' "msg":"恭喜<%白阿姨家的路人%>获得大奖<%2.3333w银瓜子%>, 感谢<%耀眼de天堂%>的赠送"
        m_DanmuBuilder.Append(jDic("data")("mobileTips")).Append(", 蟹蟹 ").Append(jDic("data")("from")).Append(" 的赠送")
        m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)
        RaiseEvent RaffleStart(Nothing, New FedEventArgs(m_Danmu, -1, GiftUnit.None))
    End Sub

    Private Sub TV_END(ByRef jDic As Dictionary(Of String, Object))
        RAFFLE_END(jDic)
    End Sub

    Private Sub EVENT_CMD(ByRef jDic As Dictionary(Of String, Object))
        ' {"roomid":48499,"cmd":"EVENT_CMD","data":{"event_type":"GIFT-44837","event_img":"http:\/\/i0.hdslb.com\/bfs\/live\/9a87ac24fd7660c50af7f5269b8425397e34bdb0.png"}}
        ' 不知如何解析
    End Sub

    Private Sub WISH_BOTTLE(ByRef jDic As Dictionary(Of String, Object))
        m_DanmuBuilder.Clear()
        Dim data As DanmuEntity.WISH_BOTTLE.Data
        Try
            data = MSJsSerializer.ConvertToType(Of DanmuEntity.WISH_BOTTLE.Data)(jDic("data"))
            m_DanmuBuilder.Append("许愿瓶：").Append(data.wish.content).Append(" ").Append(data.action).Append(" 进度：").Append(data.wish.wish_progress).Append("|").Append(data.wish.wish_limit)
        Catch ex As Exception
        End Try
#Disable Warning BC42104 ' 在为变量赋值之前，变量已被使用
        If data Is Nothing Then
#Enable Warning BC42104 ' 在为变量赋值之前，变量已被使用
            Dim data2 = MSJsSerializer.ConvertToType(Of DanmuEntity.WISH_BOTTLE.Data2)(jDic("data"))
            m_DanmuBuilder.Append("许愿瓶：").Append(data2.wish.content).Append(" ").Append(data2.action).Append(" 进度：").Append(data2.wish.wish_progress).Append("|").Append(data2.wish.wish_limit)
        End If
        m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)
        RaiseEvent GoldFed(Nothing, New FedEventArgs(m_Danmu, -1, GiftUnit.None))
    End Sub

    Private Sub ROOM_RANK(ByRef jDic As Dictionary(Of String, Object))
        ' 小时榜发生变化（或升或降 或有或无）
        m_Danmu = CStr(jDic("data")("rank_desc"))
        RaiseEvent HourRankChanged(Nothing, New HourRankChangedEventArgs(m_Danmu))
    End Sub

    Private Sub SYS_MSG(ByRef jDic As Dictionary(Of String, Object), ByVal body As String)
        If jDic.ContainsKey("msg_text") Then
            m_Danmu = CStr(jDic("msg_text"))
        ElseIf jDic.ContainsKey("msg") Then
            m_Danmu = CStr(jDic("msg"))
        ElseIf jDic.ContainsKey("message") Then
            m_Danmu = CStr(jDic("message"))
        Else
            m_Danmu = body
        End If
        m_Danmu = m_Danmu.Replace(":?", "")
        RaiseEvent SystemMessageChanged(Nothing, New SystemMessageChangedEventArgs(m_Danmu))
    End Sub

    Private Sub SYS_GIFT(ByRef jDic As Dictionary(Of String, Object), ByVal body As String)
        SYS_MSG(jDic, body)
    End Sub

    Private Sub GUARD_MSG(ByRef jDic As Dictionary(Of String, Object), ByVal body As String)
        m_Danmu = If(jDic.ContainsKey("msg"), CStr(jDic("msg")), body)
        m_Danmu = m_Danmu.Replace(":?", "")
        RaiseEvent SystemMessageChanged(Nothing, New SystemMessageChangedEventArgs(m_Danmu))
    End Sub

    Private Sub GUARD_LOTTERY_START(ByRef jDic As Dictionary(Of String, Object))
        m_Danmu = CStr(jDic("data")("lottery")("thank_text"))
        RaiseEvent SystemMessageChanged(Nothing, New SystemMessageChangedEventArgs(m_Danmu))
    End Sub

    Private Sub PREPARING()
        m_Danmu = If(DanmuFormatDic(DmCmd.PREPARING.ToString).CustomStyle, DanmuFormatDic(DmCmd.PREPARING.ToString).DefaultStyle)
        RaiseEvent LiveStatusChanged(Nothing, New LiveStatusChangedEventArgs(LiveStatus.Round, m_Danmu))
    End Sub

    Private Sub LIVE()
        m_Danmu = If(DanmuFormatDic(DmCmd.LIVE.ToString).CustomStyle, DanmuFormatDic(DmCmd.LIVE.ToString).DefaultStyle)
        RaiseEvent LiveStatusChanged(Nothing, New LiveStatusChangedEventArgs(LiveStatus.Live, m_Danmu))
    End Sub

    Private Sub ROOM_SILENT_OFF()
        m_Danmu = "主播取消了房间禁言 " & RandomEmoji.Happy
        RaiseEvent DanmuSendEnabledChanged(Nothing, New DanmuSendEnabledEventArgs(True, m_Danmu))
    End Sub

    Private Sub ROOM_SILENT_ON(ByRef jDic As Dictionary(Of String, Object))
        m_DanmuBuilder.Clear()

        Dim type = jDic("data")("type").ToString
        If "member" = type Then
            m_DanmuBuilder.Append("全场遭到禁言袭击，解除时间：")
        ElseIf "level" = type Then
            m_DanmuBuilder.Append("用户等级 UL.").Append(jDic("data")("level")).Append(" 以下的用户遭到禁言袭击，解除时间：")
        Else
            ' 勋章 没有 暂不实现
        End If
        Dim limitSecond = CInt(jDic("data")("second"))
        If -1 = limitSecond Then
            m_DanmuBuilder.Append("无")
        Else
            m_DanmuBuilder.Append(CInt(jDic("data")("second")).ToTimeStampString())
        End If
        m_DanmuBuilder.Append(" "c).Append(RandomEmoji.Helpless)
        m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)
        RaiseEvent DanmuSendEnabledChanged(Nothing, New DanmuSendEnabledEventArgs(False, m_Danmu))
    End Sub

    Private Sub CUT_OFF(ByRef jDic As Dictionary(Of String, Object))
        ' {"cmd":"CUT_OFF","msg":"\u7981\u6b62\u76f4\u64ad\u8fdd\u6cd5\u8f6f\u4ef6","roomid":48499}
        m_Danmu = CStr(jDic("msg"))
        RaiseEvent LiveStatusChanged(Nothing, New LiveStatusChangedEventArgs(LiveStatus.CutOff, m_Danmu))
    End Sub

    Private Sub WELCOME(ByRef jDic As Dictionary(Of String, Object))
        m_DanmuBuilder.Clear()

        Dim data = MSJsSerializer.ConvertToType(Of DanmuEntity.WELCOME.Data)(jDic("data"))
        m_DanmuBuilder.Append("欢迎 ").Append(data.uname).Append(" ").Append(If(1 = CInt(data.vip), "老爷 ", If(1 = CInt(data.svip), "年费老爷 ", ""))).Append("，空调已开放")
        m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)
        Dim level = If(1 = CInt(data.vip), VipLevel.Vip, If(1 = CInt(data.svip), VipLevel.SVip, VipLevel.None))
        RaiseEvent VipEntered(Nothing, New VipEnteredEventArgs(level, m_Danmu))
    End Sub

    Private Sub WELCOME_GUARD(ByRef jDic As Dictionary(Of String, Object))
        m_DanmuBuilder.Clear()

        ' 舰长大人/提督大人/总督大人
        Dim data = MSJsSerializer.ConvertToType(Of DanmuEntity.WELCOME_GUARD.Data)(jDic("data"))
        Dim level = If(1 = CInt(data.guard_level), GuardLevel.总督, If(2 = CInt(data.guard_level), GuardLevel.提督, GuardLevel.舰长))
        m_DanmuBuilder.Append("欢迎 ").Append(level.ToString).Append("大人 ").Append(data.username).Append(" ").Append("，空调已开放")
        m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)
        RaiseEvent GuardEntered(Nothing, New GuardEnteredEventArgs(level, m_Danmu))
    End Sub

    Private Sub ENTRY_EFFECT(ByRef jDic As Dictionary(Of String, Object))
        ' 上面 DmCmd.WELCOME_GUARD 有部分守护进入直播间的信息没有推送，原因暂不清楚
        'RaiseEvent ChatHistoryChanged(Nothing, New DanmuChangedEventArgs With {.m_Danmu = CStr(jDic("data")("copy_writing"))})
    End Sub

    Private Sub ROOM_BLOCK_MSG(ByRef jDic As Dictionary(Of String, Object))
        ' 被禁言
        m_Danmu = $"用户 {jDic("uname")} 已被管理员禁言"
        RaiseEvent RoomViewerBlocked(Nothing, New RoomViewerBlockedEventArgs(m_Danmu))
    End Sub

    Private Sub ROOM_ADMINS(ByRef jDic As Dictionary(Of String, Object))
        ' 管理员变化 不需要解析反馈？
        ' {"cmd":"ROOM_ADMINS","uids":[92646702,177836752,267794245,10359365,13784846,10809617]}
        'm_Danmu = String.Join(",", TryCast(jDic("uids"), Object()))
        'RaiseEvent ChatHistoryChanged(Nothing, New ChatHistoryChangedEventArgs With {.m_Danmu = m_Danmu})
    End Sub

    Private Sub NOTICE_MSG(ByRef jDic As Dictionary(Of String, Object))
        m_DanmuBuilder.Clear()
        '(5): {[msg_common, 娱乐区广播: <%1320一只幺yao家の弱鸡%>送给<%一只幺yao%>1个小金人， 点击前往TA的房间去抽奖吧]}
        '   (6): {[msg_self, 娱乐区广播: <%1320一只幺yao家の弱鸡%>送给<%一只幺yao%>1个小金人， 快来抽奖吧]}
        m_DanmuBuilder.Append(jDic("msg_common"))
        m_DanmuBuilder.TryRemove("%"c)
        m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)
        RaiseEvent SystemMessageChanged(Nothing, New SystemMessageChangedEventArgs(m_Danmu))
    End Sub

    Private Sub WARNING(ByRef jDic As Dictionary(Of String, Object))
        ' {"cmd":"WARNING","msg":"\u8fdd\u53cd\u76f4\u64ad\u5206\u533a\u89c4\u8303\uff0c\u8bf7\u7acb\u5373\u66f4\u6362\u81f3\u6620\u8bc4\u9986","roomid":7399897}
        m_Danmu = jDic("msg").ToString
        RaiseEvent SystemMessageChanged(Nothing, New SystemMessageChangedEventArgs(m_Danmu))
    End Sub

    Private Sub ACTIVITY_EVENT(ByRef jDic As Dictionary(Of String, Object))
        m_DanmuBuilder.Clear()
        m_DanmuBuilder.Append("活动：").Append(jDic("data")("keyword")).Append(" ").Append(" 进度：").Append(jDic("data")("progress")).Append("|").Append(jDic("data")("limit"))
        m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)
        RaiseEvent SystemMessageChanged(Nothing, New SystemMessageChangedEventArgs(m_Danmu))
    End Sub

    Private Sub Room_Admin_Entrance(ByRef jDic As Dictionary(Of String, Object))
        m_Danmu = "用户Id " & CStr(jDic("uid")) & " 已被主播设置为管理员"
        RaiseEvent RoomAdminAppointed(Nothing, New RoomAdminAppointedEventArgs(m_Danmu))
    End Sub

    Private Sub USER_TOAST_MSG(ByRef jDic As Dictionary(Of String, Object))
        ' "cmd":"USER_TOAST_MSG","data":{"op_type":1,"uid":11860409,"username":"一成不变是千雪","guard_level":3,"is_show":0}}

    End Sub

	Private Sub ROOM_REAL_TIME_MESSAGE_UPDATE(ByRef jDic As Dictionary(Of String, Object))
		''{"cmd":"ROOM_REAL_TIME_MESSAGE_UPDATE","data":{"roomid":4236342,"fans":476}}
		RaiseEvent AttentionCountChanged(Nothing, New AttentionCountChangedEventArgs(CInt(jDic("data")("fans"))))
	End Sub

    Private Sub ROOM_CHANGE(ByRef jDic As Dictionary(Of String, Object))
        Dim events = New RoomChangedEventArgs With {
            .Title = jDic("data")("title").ToString,
            .AreaId = CInt(jDic("data")("area_id")),
            .ParentAreaId = CInt(jDic("data")("parent_area_id")),
            .AreaName = jDic("data")("area_name").ToString,
        .ParentAreaName = jDic("data")("parent_area_name").ToString
        }
        RaiseEvent RoomChanged(Nothing, events)
    End Sub

    Private Sub MESSAGEBOX_USER_GAIN_MEDAL(ByRef jDic As Dictionary(Of String, Object))
        m_Danmu = $"靓仔 {jDic("data")("fan_name")} 获得❀{jDic("data")("medal_name")}❀勋章"
        Dim fedEventArgs = New FedEventArgs(m_Danmu, CStr(jDic("data")("uid")), CStr(jDic("data")("fan_name"))， -1, -1, GiftUnit.None)

        RaiseEvent GoldFed(Nothing, fedEventArgs)
        RaiseEvent MedalGained(Nothing, fedEventArgs)
    End Sub

    Private Sub INTERACT_WORD(ByRef jDic As Dictionary(Of String, Object))
        m_Danmu = $"{jDic("data")("uname")} ＿|￣|瞄"

        RaiseEvent ViewerEntered(Nothing, New ViewerEnteredEventArgs(m_Danmu))
    End Sub

    Private Sub POPULARITY_RED_POCKET_START(ByRef jDic As Dictionary(Of String, Object))
        m_Danmu = $"靓仔 {jDic("data")("sender_name")} 发的红包开枪啦~ 结束时间：{CLng(jDic("data")("end_time")).ToTimeStampString(TimePrecision.Second, "HH:mm:ss")}"

        Dim fedEventArgs = New FedEventArgs(m_Danmu, CStr(jDic("data")("sender_uid")), CStr(jDic("data")("sender_name"))， -1, CInt(jDic("data")("total_price")), GiftUnit.None)

        RaiseEvent GoldFed(Nothing, fedEventArgs)
    End Sub
#End Region

#End Region



End Class
