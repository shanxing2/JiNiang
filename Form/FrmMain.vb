Imports System.Text.RegularExpressions
Imports ShanXingTech
Imports ShanXingTech.Text2
Imports 姬娘
Imports 姬娘.Events
Imports 姬娘插件
Imports 姬娘插件.Events



Public Class FrmMain
#Region "条件编译常量"
    '  ##############################
    ' debug 模式的时候，把 True 改为 False,非 debug 模式 设置为 True
    '  ##############################
#Const CLICKONCE = True
#End Region

#Region "字段区"
    Public WithEvents DmTcpClient As TcpListener
    Public WithEvents DanmuParser As OpCodeParser
    'Private spanGiftFeed As HtmlElement

    Private m_LoginResult As LoginResult
    Private m_StartLiveSettingForm As FrmLiveSetting
    Private m_SettingControl As SettingControl
    Private tpStartLiveSetting As TabPage

#End Region

#Region "常量区"
    ' 明文：神即道, 道法自然, 如来|闪星网络信息科技 ShanXingTech Q2287190283
    ' 算法：古典密码中的有密钥换位密码 密钥：ShanXingTech
    Public Const ShanXingTechQ2287190283 = "神闪X7,SQB道信T2道网N9来A2D如H2C然技HA即星I1|N8E法息E8,络G0自科C3"


#End Region

#Region "属性区"
    Public Property DanmuControl As DanmuControl
    Public ReadOnly Property SettingControl As SettingControl
        Get
            If m_SettingControl Is Nothing Then
                m_SettingControl = New SettingControl(Me, Me.tpSetting)
                AddHandler m_SettingControl.MedalUpgradeHimeChanged, AddressOf DanmuParser_MedalUpgradeHimeChanged
            End If

            If Not Me.tpSetting.Controls.Contains(m_SettingControl) Then
                Me.tpSetting.Controls.Add(m_SettingControl)
            End If

            Return m_SettingControl
        End Get
    End Property

    Public Property LiveRoomControl As LiveRoomInfoControl
#End Region

#Region "构造函数区"
    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub
#End Region


    Private Async Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
#If Not DEBUG Then
        ' 检测到被附加到其他进程(比如od?)调试时，强制退出
        If Win32API.IsDebuggerPresent Then
            Me.Visible = False
            MessageBox.Show(ShanXingTechQ2287190283, "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Environment.Exit(0)
            Return
        End If
#End If

        If Deployment.Application.ApplicationDeployment.IsNetworkDeployed Then
            Me.Text = $"{My.Settings.APPName}   V{Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()}"
        Else
            Me.Text = $"{My.Settings.APPName}   V{Application.ProductVersion}"
        End If

#Disable Warning BC42358 ' 在调用完成之前，会继续执行当前方法，原因是此调用不处于等待状态

        Await TryLoginAsync()
#Enable Warning BC42358 ' 在调用完成之前，会继续执行当前方法，原因是此调用不处于等待状态
    End Sub


    Private Async Sub FrmMain_FormClosingAsync(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' ###################### 注意 ######################
        ' # 如果实现了IDisposble接口，则必须在 过程Protected Overrides Sub Dispose(disposing As Boolean)的
        ' # 最后调用 MyBase.Dispose(Disposing),否则无法关闭窗体
        ' # 20180806
        ' ###################### 注意 ######################
        If m_LoginResult = LoginResult.Yes Then
            ' 没使用过开播设置窗体，说明不是用的这个进程开播，或者是非此直播间的Up
            ' 以上两种情况都不需要此进程处理关播
            Dim isLiving = m_StartLiveSettingForm IsNot Nothing
            Dim stopLive As Boolean
            ' 使用过开播设置窗体并且是处于直播状态，说明一定是此直播间的Up，需要提示是否关播
            If isLiving AndAlso DanmuEntry.User.ViewRoom.Status = LiveStatus.Live Then
                If MessageBox.Show("直播中，是否关播???", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    stopLive = True
                End If
            Else
                If MessageBox.Show("确定要退出软件嘛 (╬ﾟдﾟ)▄︻┻┳═一", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.Cancel Then
                    e.Cancel = True
                    Return
                End If
            End If

            Me.Visible = False
            Try
                ConfigureEvents(False)
                DmTcpClient?.Close()
                If stopLive Then
                    Await m_StartLiveSettingForm.OnStopLiveAsync()
                End If
                DanmuEntry.Close(Me.DanmuControl)
            Catch ex As Exception
                Me.Visible = True
            End Try
        ElseIf m_LoginResult = LoginResult.NotLogin Then
            If MessageBox.Show("确定要退出软件嘛 (╬ﾟдﾟ)▄︻┻┳═一", "温馨提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.Cancel Then
                e.Cancel = True
                Return
            End If

            Me.Visible = False
            Try
                ConfigureEvents(False)
                DmTcpClient?.Close()
                DanmuEntry.Close(Me.DanmuControl)
            Catch ex As Exception
                Me.Visible = True
            End Try
        ElseIf m_LoginResult = LoginResult.UserOnly Then
            DanmuEntry.Close(Me.DanmuControl)
        End If
    End Sub

#Region "IDisposable Support"
    ' 要检测冗余调用
    Dim isDisposed2 As Boolean = False

    ''' <summary>
    ''' 重写Dispose 以清理非托管资源
    ''' </summary>
    ''' <param name="disposing"></param>
    Protected Overrides Sub Dispose(disposing As Boolean)
        ' 窗体内的控件调用Close或者Dispose方法时，isDisposed2的值为True
        If isDisposed2 Then Return

        Try
            ' TODO: 释放托管资源(托管对象)。
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                    components = Nothing
                End If
            End If

            ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
            ' TODO: 将大型字段设置为 null。

            If DmTcpClient IsNot Nothing Then
                DmTcpClient.Dispose()
                DmTcpClient = Nothing
            End If

            If DanmuParser IsNot Nothing Then
                DanmuParser.Dispose()
                DanmuParser = Nothing
            End If

            If ToolTip1 IsNot Nothing Then
                ToolTip1.Dispose()
                ToolTip1 = Nothing
            End If

            If m_SettingControl IsNot Nothing Then
                RemoveHandler m_SettingControl.MedalUpgradeHimeChanged, AddressOf DanmuParser_MedalUpgradeHimeChanged
            End If

            isDisposed2 = True
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '' NOTE: Leave out the finalizer altogether if this class doesn't   
    '' own unmanaged resources itself, but leave the other methods  
    '' exactly as they are.   
    'Protected Overrides Sub Finalize()
    '    Try
    '        ' Finalizer calls Dispose(false)  
    '        Dispose(False)
    '    Finally
    '        MyBase.Finalize()
    '    End Try
    'End Sub
#End Region

    ''' <summary>
    ‘’‘
    ''' </summary>
    Private Async Function TryLoginAsync() As Task
        Try
            AddHandler DanmuEntry.UserEnsured, AddressOf UserEnsuredTask
            AddHandler DanmuEntry.RoomRealIdEnsured, AddressOf RoomRealIdEnsuredTask

            Me.Invoke(Sub() LoadPlugins())

            ' 尽可能利用登录的空余时间初始化各种器
            Dim initTcpTask = InitTcpAsync()
            Dim initDanmuSenderTask = InitDanmuSenderAsync()
            Dim loginTask = DanmuEntry.TryLoginAsync()
            Await Task.WhenAll(InitDanmuSenderAsync, initTcpTask, loginTask)
            m_LoginResult = loginTask.Result
            If LoginResult.Yes = m_LoginResult OrElse
                LoginResult.NotLogin = m_LoginResult Then
                Debug.Print(Logger.MakeDebugString("登录成功"))
                Windows2.DrawTipsTask(Me, "等一分钟.gif    " & RandomEmoji.Helpless, 3000, True, False)

                Me.BeginInvoke(Sub() ConofigureSettingControlByPersonal())
                Dim succeed = Await ConfigureHimesAsync()
                If succeed Then
                    EnabledHimes()
                    ' 登录成功之后 获取最新弹幕
#Disable Warning bc42358
                    UpdateChatHistoryAsync()
#Enable Warning bc42358
                End If
            ElseIf m_LoginResult = LoginResult.Cancel Then
                Me.Close()
            ElseIf m_LoginResult = LoginResult.UserOnly Then
                ' 仅仅是加载个人设置
                Me.BeginInvoke(Sub() ConofigureSettingControlByPersonal())
            Else
                Windows2.DrawTipsTask(Me, "登录失败" & RandomEmoji.Sad, 1000, False, False)
            End If
        Catch ex As Exception
            Logger.WriteLine(ex)
        Finally
            RemoveHandler DanmuEntry.UserEnsured, AddressOf UserEnsuredTask
            RemoveHandler DanmuEntry.RoomRealIdEnsured, AddressOf RoomRealIdEnsuredTask
        End Try
    End Function

#Region "通讯初始化 用户配置与相应UI 处理"
    Private Async Function ConfigureHimesAsync() As Task(Of Boolean)
        Await DanmuEntry.ConfigureHimesAsync

        If DanmuEntry.User.ViewRoom Is Nothing OrElse
            DanmuEntry.User.ViewRoom.RealId.IsNullOrEmpty Then
            Windows2.DrawTipsTask(Me, "获取直播间Id失败" & RandomEmoji.Sad, 1000, False, False)
            DanmuControl.Enabled = False
            Return False
        End If

        If Not DanmuEntry.User.IsLogined Then Return False

        DanmuParser_MedalUpgradeHimeChanged(Nothing, New SettingControl.MedalUpgradeHimeEnsuredEventArgs(DanmuEntry.Configment.EnabledMedalUpgradeHime, DanmuEntry.User.ViewRoom.UserId.ToIntegerOfCulture))

        ' 如果是进入自己的直播间，那就不再显示用户昵称了
        If DanmuEntry.User.Id = DanmuEntry.User.ViewRoom.UserId Then
            If tpStartLiveSetting Is Nothing Then
                tpStartLiveSetting = New TabPage With {
                    .BackColor = SystemColors.Control,
                    .Location = New Point(4, 22),
                    .Name = NameOf(tpStartLiveSetting),
                    .TabIndex = 1,
                    .Text = "快速开播"
                }
                ' 377, 434
            End If
            TabControl1.TabPages.Insert(tpLiveRoom.TabIndex + 1, tpStartLiveSetting)

            LiveRoomControl.lblRoomOwnerNick.Text = DanmuEntry.User.ViewRoom.UserNick()
            LiveRoomControl.tlpLiveRoomInfoRow2.Controls.Remove(LiveRoomControl.lblJoinRoom)
            LiveRoomControl.tlpLiveRoomInfoRow2.Controls.Remove(LiveRoomControl.lblUserNick)
            LiveRoomControl.tlpLiveRoomInfoRow2.ColumnCount -= 2
            LiveRoomControl.tlpLiveRoomInfoRow2.ColumnStyles(LiveRoomControl.tlpLiveRoomInfoRow2.ColumnCount - 1).SizeType = SizeType.Absolute
            LiveRoomControl.tlpLiveRoomInfoRow2.ColumnStyles(LiveRoomControl.tlpLiveRoomInfoRow2.ColumnCount - 1).Width = 1.0F
        Else
            LiveRoomControl.lblUserNick.Text = DanmuEntry.User.Nick()
            LiveRoomControl.lblRoomOwnerNick.Text = DanmuEntry.User.ViewRoom.UserNick()
        End If

        DanmuControl.cmbDanmuInput.Items.Clear()
        DanmuControl.cmbDanmuInput.Items.AddRange(DanmuEntry.User.ViewRoom.HotWords)

        Me.tpLiveRoom.Controls.Add(LiveRoomControl)
        Me.tpLiveRoom.Controls.Add(DanmuControl)

        Return True
    End Function

    Private Async Function InitTcpAsync() As Task
        Await Task.Run(
                Sub()
                    If DanmuEntry.Configment.ConnectionMode = ConnectMode.Tcp AndAlso
                    DmTcpClient Is Nothing Then
                        DmTcpClient = New TcpListener()
                    End If
                End Sub)
    End Function

    Private Async Function InitDanmuSenderAsync() As Task
        Await Task.Run(Sub() DanmuControl.Create())
    End Function

    ''' <summary>
    ''' 配置各种姬
    ''' </summary>
    Private Sub EnabledHimes()
        ' 根据配置信息选择连接弹幕服务器的方式
        Select Case DanmuEntry.Configment.ConnectionMode
            Case ConnectMode.Tcp
                DanmuParser = New OpCodeParser(DanmuEntry.User.Id,
                                             DanmuEntry.User.ViewRoom.UserId,
                                             DanmuEntry.Configment.DanmuFormat.FormatDic)

                ConfigureEvents(True)

#Disable Warning BC42358 ' 在调用完成之前，会继续执行当前方法，原因是此调用不处于等待状态
                DmTcpClient.ConnectAsync(CInt(DanmuEntry.User.ViewRoom.RealId()),
                                                   DanmuEntry.User.Id,
                                                   DanmuEntry.User.ViewRoom.UserId,
                                                   DanmuEntry.User.ViewRoom.Server.DmHost,
                                                   DanmuEntry.User.ViewRoom.Server.DmPort)
#Enable Warning BC42358
            Case ConnectMode.Wss
            Case ConnectMode.Ws
            Case Else
        End Select
    End Sub

    Private Sub ReConnect()
        ' 根据配置信息选择连接弹幕服务器的方式
        Select Case DanmuEntry.Configment.ConnectionMode
            Case ConnectMode.Tcp
#Disable Warning BC42358 ' 在调用完成之前，会继续执行当前方法，原因是此调用不处于等待状态
                DmTcpClient.ConnectAsync(CInt(DanmuEntry.User.ViewRoom.RealId()),
                                                   DanmuEntry.User.Id,
                                                   DanmuEntry.User.ViewRoom.UserId,
                                                   DanmuEntry.User.ViewRoom.Server.DmHost,
                                                   DanmuEntry.User.ViewRoom.Server.DmPort)
            Case ConnectMode.Wss
            Case ConnectMode.Ws
            Case Else
        End Select
    End Sub

    ''' <summary>
    ''' 获取弹幕信息并显示到文本框
    ''' </summary>
    ''' <returns></returns>
    Private Async Function UpdateChatHistoryAsync() As Task
        Dim getRst = Await DanmuEntry.GetDanmuAsync()
        If getRst.NeedChangeChatHistory Then
            DanmuControl.UpdateChatHistory(getRst.Message)
        End If
    End Function

    Public Sub ConfigureEvents(ByVal enabledEvents As Boolean)
        ' 此处只需要配置默认注册的事件;
        ' 如果之前已经手动配置过， 那么也注册由弹幕服务器推送而产生的事件
        If enabledEvents Then
            If DanmuParser IsNot Nothing Then
                AddHandler DanmuEntry.LiveRoomInfoChanged, AddressOf dmClient_LiveRoomInfoChanged
                AddHandler DanmuEntry.LiveStatusChanged, AddressOf DmHttpClient_LiveStatusChanged

                If DanmuEntry.Configment.EnabledThanksHime Then
                    AddHandler DanmuParser.SilverFed, AddressOf DanmuParser_SilverFed
                    AddHandler DanmuParser.GoldFed, AddressOf DanmuParser_GoldFed
                End If
                If DanmuEntry.Configment.EnabledMedalUpgradeHime Then
                    AddHandler DanmuParser.MedalUpgradeChecking, AddressOf DanmuParser_MedalUpgradeChecking
                End If
                If DanmuEntry.Configment.EnabledWelcomeHime Then
                    AddHandler DanmuParser.VipEntered, AddressOf DanmuParser_VipEntered
                    AddHandler DanmuParser.GuardEntered, AddressOf DanmuParser_GuardEntered
                End If
                If DanmuEntry.Configment.EnabledSystemMessageHime Then
                    AddHandler DanmuParser.SystemMessageChanged, AddressOf DanmuParser_SystemMessageChanged
                End If
            End If

            If DanmuEntry.Configment.ConnectionMode = ConnectMode.Tcp Then
                If DanmuEntry.Configment.EnabledAttentionHime Then
                    AddHandler DanmuEntry.AttentionIncreased, AddressOf DanmuParser_AttentionIncreased
                    AddHandler DanmuParser.AttentionIncreased, AddressOf DanmuParser_AttentionIncreased
                End If
            Else

            End If
        Else
            RemoveHandler DanmuEntry.LiveRoomInfoChanged, AddressOf dmClient_LiveRoomInfoChanged
            RemoveHandler DanmuEntry.LiveStatusChanged, AddressOf DmHttpClient_LiveStatusChanged

            If DanmuParser Is Nothing Then Return

            If DanmuEntry.Configment.EnabledThanksHime Then
                RemoveHandler DanmuParser.SilverFed, AddressOf DanmuParser_SilverFed
                RemoveHandler DanmuParser.GoldFed, AddressOf DanmuParser_GoldFed
            End If
            If DanmuEntry.Configment.EnabledMedalUpgradeHime Then
                RemoveHandler DanmuParser.MedalUpgradeChecking, AddressOf DanmuParser_MedalUpgradeChecking
            End If
            If DanmuEntry.Configment.EnabledWelcomeHime Then
                RemoveHandler DanmuParser.VipEntered, AddressOf DanmuParser_VipEntered
                RemoveHandler DanmuParser.GuardEntered, AddressOf DanmuParser_GuardEntered
            End If
            If DanmuEntry.Configment.EnabledSystemMessageHime Then
                RemoveHandler DanmuParser.SystemMessageChanged, AddressOf DanmuParser_SystemMessageChanged
            End If

            If DanmuEntry.Configment.ConnectionMode = ConnectMode.Tcp Then
                If DanmuEntry.Configment.EnabledAttentionHime Then
                    RemoveHandler DanmuEntry.AttentionIncreased, AddressOf DanmuParser_AttentionIncreased
                    RemoveHandler DanmuParser.AttentionIncreased, AddressOf DanmuParser_AttentionIncreased
                End If
            Else

            End If
        End If
    End Sub

    Private Sub ConofigureSettingControlByPersonal()
        Me.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        With SettingControl
            .SuspendLayout()

            If DanmuEntry.Configment.ConnectionMode = ConnectMode.Tcp Then
                .rdbtnTcpMode.Checked = True
            ElseIf DanmuEntry.Configment.ConnectionMode = ConnectMode.Wss Then
                .rdbtnWssMode.Checked = True
            ElseIf DanmuEntry.Configment.ConnectionMode = ConnectMode.Ws Then
                .rdbtnWsMode.Checked = True
            Else
                .rdbtnHttpMode.Checked = True
            End If

            .pnlHimesEnablerContainer.Enabled = Not (m_LoginResult = LoginResult.NotLogin)

            .chkFlashWindow.Checked = DanmuEntry.Configment.FlashWindowWhileReceiveDanmu
            .chkEnabledThanksHime.Checked = DanmuEntry.Configment.EnabledThanksHime
            .chkEnabledWelcomeHime.Checked = DanmuEntry.Configment.EnabledWelcomeHime
            .chkEnabledAttentionHime.Checked = DanmuEntry.Configment.EnabledAttentionHime
            .chkEnabledSystemMessageHime.Checked = DanmuEntry.Configment.EnabledSystemMessageHime
            .chkSignAuto.Checked = DanmuEntry.Configment.SignAuto
            .chkReceiveDoubleWatchAwardAuto.Checked = DanmuEntry.Configment.ReceiveDoubleWatchAwardAuto
            .chkEnabledMedalUpgradeHime.Checked = DanmuEntry.Configment.EnabledMedalUpgradeHime

            .nudHeartbeatInterval.Value = DanmuEntry.Configment.HeartbeatInterval

            If DanmuEntry.Configment.ThanksHime.DanmuRepeatitiveHandle = DanmuRepeatOptions.Block Then
                .rdbtnBlockRepeatitive.Checked = True
            ElseIf DanmuEntry.Configment.ThanksHime.DanmuRepeatitiveHandle = DanmuRepeatOptions.Merge Then
                .rdbtnMergeRepeatitive.Checked = True
            End If

            .ResumeLayout(False)
        End With
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    ''' <summary>
    ''' 已确认选择了某个用户
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub UserEnsuredTask(sender As Object, e As UserEnsuredEventArgs)
        Me.tpLiveRoom.SuspendLayout()

        SettingControl.trbFormOpacity.Value = e.MainForm.Opacity
        Me.Opacity = SettingControl.trbFormOpacity.Value / 100
        SettingControl.lblMainFormOpacity.Text = Me.Opacity.ToString("P0")

        If e.MainForm.Location.IsEmpty OrElse
            e.MainForm.Size.IsEmpty Then
            ' 第一次运行居中显示
            Me.StartPosition = FormStartPosition.CenterScreen
            e.MainForm.Location = Me.Location
            e.MainForm.Size = Me.Size
        Else
            Me.Location = e.MainForm.Location
            Me.Size = e.MainForm.Size
        End If

#If CLICKONCE OrElse Not DEBUG Then
        ' 不能在构造函数中设置窗体的 TopMost 属性，因为窗体句柄还没有设置，会导致无法执行到置顶代码（）
        ' TopMost 属性源码 https://referencesource.microsoft.com/#System.Windows.Forms/winforms/Managed/System/WinForms/Form.cs,2d8f4b4dd33cce53
        'Me.Activate()
        Me.TopMost = e.MainForm.TopMost
#Else
        ' 为调试方便，调试模式时默认不置顶
        Me.TopMost = False
#End If
        If e.LoginResult = LoginResult.Yes Then
#If CLICKONCE OrElse Not DEBUG Then
            LiveRoomControl.chkTopMost.Checked = e.MainForm.TopMost
#Else
        ' 为调试方便，调试模式时默认不置顶
        LiveRoomControl.chkTopMost.Checked = False
#End If
        End If

        ' ##################开发人员调试专区##################
        ' Id 请更改为开发者的B站用户Id
        IAmDeveloper(DanmuEntry.User.Id = "52155851")
        ' ##################开发人员调试专区##################

        Me.tpLiveRoom.ResumeLayout(False)
    End Sub

    Private Sub RoomRealIdEnsuredTask(sender As Object, e As EventArgs)
        LiveRoomControl.Init(DanmuEntry.User, New Point(0, 0), Me.tpLiveRoom.Size, Me.Handle)
        DanmuControl.Init(DanmuEntry.User, DanmuEntry.Configment.ThanksHime, New Point(0, LiveRoomControl.Height), Me.tpLiveRoom.Size)
    End Sub

    Private Sub IAmDeveloper(ByVal enabled As Boolean)
        If DanmuControl IsNot Nothing Then
            DanmuControl.btnSendTest.Visible = enabled
        End If

        'With m_StartLiveSettingForm
        '    .chkAutoPushStream.Visible = enabled
        '    .picCapturePushButton.Visible = enabled
        '    .lblPushStreamText.Visible = enabled
        '    .lblPushStreamResult.Visible = enabled
        'End With

        With m_SettingControl
            .nudBrokeSilenceInterval.Visible = enabled
            .Label1.Visible = enabled
            .chkOpenSilverBoxAuto.Visible = enabled
            .rdbtnWssMode.Enabled = enabled
            .rdbtnWsMode.Enabled = enabled
            .rdbtnHttpMode.Enabled = enabled
        End With

        If enabled Then
            Dim tpFunctionTest As New TabPage
            With tpFunctionTest
                .Name = NameOf(tpFunctionTest)
                .TabIndex = TabControl1.TabCount + 1
                .Text = NameOf(tpFunctionTest)
                .UseVisualStyleBackColor = True
            End With
            Dim funcTest As New FunctionTestControl(Me, tpFunctionTest)
            tpFunctionTest.Controls.Add(funcTest)
            TabControl1.Controls.Add(tpFunctionTest)
        End If
    End Sub

    Private Sub LoadPlugins()
        ' 预加载，登录成功之后，添加到主窗体上
        LiveRoomControl = New LiveRoomInfoControl()
        DanmuControl = New DanmuControl()
    End Sub

    Private Sub DanmuParser_JoinLiveRoomSucceeded(sender As Object, e As JoinLiveRoomSucceededEventArgs) Handles DanmuParser.JoinLiveRoomSucceeded
        DanmuControl.UpdateJoinedLiveRoomTimestamp(e.Timestamp)
    End Sub

    Private Sub DmTcpClient_HeartBeatSentCompleted(sender As Object, e As TcpListener.HeartBeatSendCompletedEventArgs) Handles DmTcpClient.HeartBeatSendCompleted
        DanmuEntry.UpdateRoomInfoAsync(e.ConnectMode)
        DanmuEntry.TrySignAsync()
        DanmuEntry.TryReceiveDoubleWatchAwardAsync()
    End Sub


#Region "处理弹幕信息接收事件"
    Private Sub DmHttpClient_OnlineChanged(sender As Object, e As OnlineChangedEventArgs) Handles DanmuParser.OnlineChanged
        LiveRoomControl.UpdateLabelText(LiveRoomControl.lblOnline, "人气 " & e.Online.ToStringOfCulture)
    End Sub

    Private Sub dmClient_AttentionCountChanged(sender As Object, e As AttentionCountChangedEventArgs) Handles DanmuParser.AttentionCountChanged
        LiveRoomControl.UpdateLabelText(LiveRoomControl.lblAttention, "粉丝 " & e.AttentionCount.ToStringOfCulture)
    End Sub

    Private Sub dmClient_LiveRoomInfoChanged(sender As Object, e As LiveRoomInfoChangedEventArgs) Handles DanmuParser.LiveRoomInfoChanged
        Try
            LiveRoomControl.UpdateLiveRoomInfo(e.Room)

            ' 从网页获取而不是服务器推送的直播间状态，不需要附加状态文本
            DmHttpClient_LiveStatusChanged(Nothing, New LiveStatusChangedEventArgs(e.Room.Status))
        Catch ex As Exception
            If e.Room Is Nothing Then
                Logger.WriteLine("e.Room Is Nothing")
            End If

            Logger.WriteLine(ex)
        End Try
    End Sub

    Private Sub dmClient_HourRankChanged(sender As Object, e As HourRankChangedEventArgs) Handles DanmuParser.HourRankChanged
        LiveRoomControl.UpdateLabelText(LiveRoomControl.lblHourRank, e.Danmu)
    End Sub

    Private Sub DanmuParser_LiveStatusChanged(sender As Object, e As LiveStatusChangedEventArgs) Handles DanmuParser.LiveStatusChanged
        OnLiveStatusChange(e)
    End Sub

    Private Sub DmHttpClient_LiveStatusChanged(sender As Object, e As LiveStatusChangedEventArgs) Handles DmTcpClient.LiveStatusChanged
        OnLiveStatusChange(e)
    End Sub

    Private Sub OnLiveStatusChange(ByVal e As LiveStatusChangedEventArgs)
        If LiveRoomControl.lblLiveStatus.InvokeRequired Then
            ' 关播推送顺序是 推送轮播（如果有）——推送关播——获取到轮播状态
            Me?.BeginInvoke(Sub() LiveStatusChangedAction(e))
        Else
            LiveStatusChangedAction(e)
        End If

        If e.Danmu IsNot Nothing Then
            DanmuControl.TrySendDanmuByEventTask(e.Danmu, DanmuType.LiveStatusChange)
        End If

        If LiveStatus.Break = e.Status Then
            ReConnect()
        End If
    End Sub

    Private Sub LiveStatusChangedAction(ByVal e As LiveStatusChangedEventArgs)
        ' 字体颜色 label不能设置形状，所以只能是字符表示 ●
        ' 有 绿——在播 灰——下播 黄——轮播 红——中断 黑——被管理员切断
        Dim color As Color
        Select Case e.Status
            Case LiveStatus.Live
                color = Color.LimeGreen
            Case LiveStatus.Round
                color = Color.Yellow
            Case LiveStatus.Break
                color = Color.Red
            Case LiveStatus.CutOff
                color = Color.Black
            Case Else
                color = Color.Gray
        End Select

        LiveRoomControl.lblLiveStatus.ForeColor = color
        DanmuEntry.User.ViewRoom.Status = e.Status
        m_StartLiveSettingForm?.OnLiveStatusChanged()
        DanmuControl.LiveStatusChanged(e.Status)
    End Sub

    Public Sub DanmuParser_AttentionIncreased(sender As Object, e As AttentionIncreasedEventArgs)
        If Not DanmuEntry.Configment.EnabledAttentionHime Then Return

        DanmuControl.TrySendDanmuByEventTask(e.Fans.Name & " 大佬 加1", DanmuType.AttentionIncrease) ' ♡+1
        ' do something else
    End Sub

    Private Sub DanmuParser_ChatHistoryChanged(sender As Object, e As DanmuChangedEventArgs) Handles DanmuParser.ChatHistoryChanged
        DanmuControl.UpdateChatHistory(e.Danmu)
    End Sub

    Public Sub DanmuParser_SilverFed(sender As Object, e As FedEventArgs)
        DanmuControl.TrySendDanmuByEventTask(e.Danmu, DanmuType.SilverFeed, e.Count, e.Unit)
    End Sub

    Public Sub DanmuParser_GoldFed(sender As Object, e As FedEventArgs)
        DanmuControl.TrySendDanmuByEventTask(e.Danmu, DanmuType.GoldFeed, e.Count, e.Unit)
    End Sub

    Public Async Sub DanmuParser_MedalUpgradeChecking(sender As Object, e As FedEventArgs)
        Await TryCheckMedalUpgradeAsync(DanmuEntry.User.ViewRoom.UserId, e)
    End Sub

    Public Async Sub DanmuParser_MedalUpgradeHimeChanged(sender As Object, e As SettingControl.MedalUpgradeHimeEnsuredEventArgs)
        DanmuEntry.User.ViewRoom.Medal = If(e.Enabled,
            Await DanmuEntry.GetMedalFromDbAsync(e.UpId),
            Nothing)
    End Sub

    ''' <summary>
    ''' 尝试检测（<paramref name="fedInfo"/>中的观众）勋章升级
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="upUid"></param>
    ''' <param name="fedInfo"></param>
    ''' <returns></returns>
    Private Async Function TryCheckMedalUpgradeAsync(Of T As FedEventArgs)(ByVal upUid As String, fedInfo As T) As Task
        If fedInfo.ViewerUid.IsNullOrEmpty OrElse fedInfo.ViewerUnick.IsNullOrEmpty Then
            Return
        End If

        ' 从网络获取勋章信息
        Dim getRst = Await BilibiliApi.GetMedalInfoAsync(fedInfo.ViewerUid, upUid)
        If Not getRst.Success Then
            IO2.Writer.WriteText($".\medal_{Now.ToString("yyyyMMdd")}.txt", $"从网络获取勋章信息失败,{NameOf(upUid)}:{upUid} {NameOf(fedInfo.ViewerUid)}:{fedInfo.ViewerUid} {NameOf(fedInfo.ViewerUnick)}:{fedInfo.ViewerUnick}", IO.FileMode.Append, IO2.CodePage.UTF8)
            Return
        End If

        ' 没有勋章 {"code":0,"msg":"","message":"","data":[]}
        If Regex.IsMatch(getRst.Message, """data"":\[\]", RegexOptions.Compiled Or RegexOptions.IgnoreCase Or RegexOptions.RightToLeft) Then
            Return
        End If

        Dim root = MSJsSerializer.Deserialize(Of ViewerMedalEntity.Root)(getRst.Message)
        If root Is Nothing Then
            Return
        End If

        Dim levelFromCache = -1
        ' 升级后有剩余记到 +1级
        If root.data.today_intimacy > root.data.intimacy OrElse
            If(fedInfo.Price = 100, fedInfo.Count, CInt((fedInfo.Count * fedInfo.Price) / 100)) > root.data.intimacy Then
            ' 从本地数据库读取用户勋章信息（如有）
            levelFromCache = GetMedalLevelFromCache(fedInfo.ViewerUid)
            ' 同级表示已经提示过
            If levelFromCache > 0 Then
                If root.data.level > levelFromCache Then
                    MakeMedalUpgradeDanmu(fedInfo.ViewerUnick, root.data.medal_name, root.data.level)
                    TryAddOrUpdateMedal(fedInfo.ViewerUid, fedInfo.ViewerUnick, root.data.level)
#If DEBUG Then

                Else
                    IO2.Writer.WriteText($".\medal_{Now.ToString("yyyyMMdd")}.txt", $"已经提示过升级,{NameOf(upUid)}:{upUid} {NameOf(fedInfo.ViewerUid)}:{fedInfo.ViewerUid} {NameOf(fedInfo.ViewerUnick)}:{fedInfo.ViewerUnick} {NameOf(root.data.level)}:{root.data.level.ToString}  {NameOf(fedInfo.Count)}:{fedInfo.Count.ToString}  {NameOf(fedInfo.Price)}:{fedInfo.Price.ToString}", IO.FileMode.Append, IO2.CodePage.UTF8)
#End If
                End If

                Return
            Else
                TryAddOrUpdateMedal(fedInfo.ViewerUid, fedInfo.ViewerUnick, root.data.level)
                Return
            End If
        End If

        ' 从本地数据库读取用户勋章信息（如有）
        If levelFromCache = -1 Then
            levelFromCache = GetMedalLevelFromCache(fedInfo.ViewerUid)
        End If
        ' 利用等级判断升级
        If levelFromCache > 0 AndAlso root.data.level > levelFromCache Then
            MakeMedalUpgradeDanmu(fedInfo.ViewerUnick, root.data.medal_name, root.data.level)
        End If

        TryAddOrUpdateMedal(fedInfo.ViewerUid, fedInfo.ViewerUnick, root.data.level)
    End Function

    Private Function MakeMedalUpgradeDanmu(ByVal viewerUname As String, ByVal medalName As String, ByVal medalLevel As Integer) As String
        Dim m_DanmuBuilder = StringBuilderCache.Acquire(50)
        m_DanmuBuilder.AppendFormat("恭喜 {0} 的勋章[{1}]升级到{2}级！", viewerUname, medalName, medalLevel.ToStringOfCulture)
        Dim m_Danmu = StringBuilderCache.GetStringAndReleaseBuilder(m_DanmuBuilder)

#If DEBUG Then
        IO2.Writer.WriteText($".\medal_{Now.ToString("yyyyMMdd")}.txt", m_Danmu, IO.FileMode.Append, IO2.CodePage.UTF8)
#End If

        Return m_Danmu
    End Function

    Private Function GetMedalLevelFromCache(ByVal viewerUid As String) As Integer
        Dim levelFromCache As Integer
        If DanmuEntry.User.ViewRoom.Medal.Detail.Count = 0 Then
            levelFromCache = 0
        Else
            Dim medalFromCache = DanmuEntry.User.ViewRoom.Medal.Detail.First(Function(m) viewerUid.ToIntegerOfCulture = m.ViewerUid)

            levelFromCache = If(medalFromCache Is Nothing, 0, medalFromCache.Level)
        End If

        Return levelFromCache
    End Function

    Private Sub TryAddOrUpdateMedal(ByVal viewerUid As String, ByVal viewerUname As String, ByVal levelFromNet As Integer)
        Dim medal = DanmuEntry.User.ViewRoom.Medal.Detail.First(Function(m) viewerUid.ToIntegerOfCulture = m.ViewerUid)
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

    Public Sub DanmuParser_SystemMessageChanged(sender As Object, e As SystemMessageChangedEventArgs)
        DanmuControl.SystemMessageSpan.InnerHtml = e.Message
    End Sub

    Public Sub DanmuParser_VipEntered(sender As Object, e As VipEnteredEventArgs)
        DanmuControl.TrySendDanmuByEventTask(e.Danmu, DanmuType.VipEnter)
    End Sub

    Public Sub DanmuParser_GuardEntered(sender As Object, e As GuardEnteredEventArgs)
        DanmuControl.TrySendDanmuByEventTask(e.Danmu, DanmuType.GuardEnter)
    End Sub

    Private Sub DanmuParser_DanmuSendEnabled(sender As Object, e As DanmuSendEnabledEventArgs) Handles DanmuParser.DanmuSendEnabledChanged
        DanmuControl.TrySendDanmuByEventTask(e.Danmu, DanmuType.DanmuSendEnabled)
    End Sub

    Private Sub DanmuParser_DanmuSendEnabled(sender As Object, e As RoomViewerBlockedEventArgs) Handles DanmuParser.RoomViewerBlocked
        DanmuControl.TrySendDanmuByEventTask(e.Danmu, DanmuType.RoomViewerBlocked)
    End Sub

    Private Sub DmTcpClient_Received(sender As Object, e As DanmuReceivedEventArgs) Handles DmTcpClient.Received
        DanmuParser.ParseOpCodeTask(e.Packet)
    End Sub

    Private Sub DanmuParser_RoomChanged(sender As Object, e As RoomChangedEventArgs) Handles DanmuParser.RoomChanged
        m_StartLiveSettingForm?.OnRoomChanged(e)
    End Sub

#End Region
#End Region

    Private Sub FrmMain_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.Resize
        If DanmuEntry.Configment.MainForm IsNot Nothing Then
            DanmuEntry.Configment.MainForm.Size = Me.Size
        End If
        If DanmuControl IsNot Nothing AndAlso Me.WindowState = FormWindowState.Normal Then
            ' 解决窗体最大化，然后再恢复正常之后，滚动条不在最底下的问题
            DanmuControl.ScrollToBottom()
        End If
    End Sub

    Private Sub FrmMain_LocationChanged(sender As Object, e As EventArgs) Handles MyBase.LocationChanged
        If DanmuEntry.Configment.MainForm IsNot Nothing Then
            DanmuEntry.Configment.MainForm.Location = Me.Location
        End If
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedTab.Name
            Case tpStartLiveSetting?.Name
                If Not DanmuEntry.User.IsLogined Then
                    Windows2.DrawTipsTask(Me, "你是耍流氓嘛???还没有登录呢~" & RandomEmoji.Angry, 2000, False, False)
                    Return
                End If

                If DanmuEntry.User.Id <> DanmuEntry.User.ViewRoom.UserId Then
                    Windows2.DrawTipsTask(Me, "这不是你的直播间，不能更改直播信息哟" & RandomEmoji.Helpless, 2000， False)
                    Return
                End If

                If m_StartLiveSettingForm Is Nothing Then
                    m_StartLiveSettingForm = New FrmLiveSetting(DanmuEntry.User.ViewRoom, DanmuEntry.User.Token)
                End If
                m_StartLiveSettingForm.OnLiveStatusChanged()

                If Me.tpStartLiveSetting.Controls.Contains(m_StartLiveSettingForm.ControlPanel) Then
                    Return
                Else
                    Me.tpStartLiveSetting.Controls.Add(m_StartLiveSettingForm.ControlPanel)
                End If
        End Select
    End Sub
End Class
