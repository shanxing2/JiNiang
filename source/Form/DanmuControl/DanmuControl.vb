Imports System.ComponentModel
Imports System.Runtime.CompilerServices
Imports ShanXingTech
Imports ShanXingTech.Win32API
Imports 姬娘
Imports 姬娘插件.Events

Public Class DanmuControl

#Region "字段区"
    Private WithEvents m_WebDoc As HtmlDocument
    Private m_User As UserInfo
    Private m_ThanksHime As ThanksHimeEntity
    Private m_InputDanmu As String
    Private WithEvents m_DanmuSender As DanmuSender
    Private inputBoxPressEnterKey As Boolean
    Private m_PreWebScrolledTime As Date
    Private m_WebScroll As HtmlElement
    Private m_SelectViewerId As String
    Private m_SelectViewerNick As String
    Private m_SelectDanmu As String
    Private m_SelectViewerTs As String
    Private m_SelectViewerCt As String
    Private ReadOnly m_BlockList As Concurrent.ConcurrentDictionary(Of Integer, Integer)
    Private m_RoomViewerManageForm As FrmRoomViewerManage
    Private m_RegistHotkeySucceed As Boolean
    Private m_HotKeyId As Integer
#End Region

#Region "常量区"
    Private Const HotKeyAtomName = "DanmuInputBox"
    Private Const DanmuClearButtonId = "danmu-msg-clear"
    Private Const DanmuWindowAdjustButtonId = "danmu-window-adjust"
    Private Const SystemMsgSpanId = "sys-msg"
    Private Const DanmuDivId = "danmu-msg-box"
#End Region

#Region "属性区"
    Public Property SystemMessageSpan As HtmlElement
    Public Property DanmuDiv As HtmlElement
    ''' <summary>
    ''' 是否可发送弹幕（已登录）
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CanSendDanmu As Boolean
        Get
            Return m_DanmuSender.CanSend
        End Get
    End Property

#End Region

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
                If m_DanmuSender IsNot Nothing Then
                    m_DanmuSender?.Dispose()
                    m_DanmuSender = Nothing
                End If

                If m_RoomViewerManageForm IsNot Nothing Then
                    m_RoomViewerManageForm.Dispose()
                    m_RoomViewerManageForm = Nothing
                End If

                If m_WebDoc IsNot Nothing Then
                    m_WebDoc = Nothing
                End If

                If ToolTip1 IsNot Nothing Then
                    ToolTip1.Dispose()
                    ToolTip1 = Nothing
                End If

                If m_RegistHotkeySucceed Then UnRegisterHotKey(Me.Handle, m_HotKeyId)
                If m_HotKeyId > 0 Then GlobalDeleteAtom(m_HotKeyId)

                If components IsNot Nothing Then
                    components.Dispose()
                    components = Nothing
                End If
            End If

            ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
            ' TODO: 将大型字段设置为 null。

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

    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        m_BlockList = New Concurrent.ConcurrentDictionary(Of Integer, Integer)

        InitControls()
    End Sub

#Region "设置、发送弹幕相关"
    Protected Overrides Sub WndProc(ByRef m As Message)
        Select Case m.Msg
            Case WM_HOTKEY
                If m.WParam.ToInt32 = m_HotKeyId AndAlso CanDanmuInputFocus() Then
                    TryTopParent(Me)

                    cmbDanmuInput.Focus()
                Else
                    MyBase.WndProc(m)
                End If

            Case Else
                MyBase.WndProc(m)
        End Select
    End Sub

    ''' <summary>
    ''' 置顶最顶层的父窗口
    ''' </summary>
    ''' <param name="ctrl"></param>
    Private Sub TryTopParent(ByRef ctrl As Control)
        Dim parent = ctrl.Parent
        If parent Is Nothing Then Return

        Do
            parent = parent.Parent
            If TypeOf parent Is Form Then Exit Do
        Loop Until parent.Parent Is Nothing

        Dim newP = TryCast(parent, Form)
        If newP Is Nothing Then Return

        If newP.WindowState <> FormWindowState.Normal Then
            newP.WindowState = FormWindowState.Normal
        End If

        newP.TopMost = True
    End Sub

    ''' <summary>
    ''' 弹幕输入框是否可获取焦点
    ''' </summary>
    ''' <returns></returns>
    Private Function CanDanmuInputFocus() As Boolean
        Return Not LoginManager.NotLoginUserId = m_User.Id
    End Function

    Private Sub InitControls()
        Me.SuspendLayout()

        UIControl(WorkStatus.Init)

        Me.Size = New Size(150, 150)
        Me.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom

        Dim marginWidth = 1

        webChatHistory.BringToFront()
        webChatHistory.Location = New Point(0, 0)
        webChatHistory.Size = New Size(Me.Width - marginWidth, Me.Height - cmbDanmuInput.Height - btnSendDanmu.Height - btnSendDanmu.Margin.Top - btnSendDanmu.Margin.Bottom - marginWidth * 2)
        webChatHistory.Anchor = Me.Anchor
        webChatHistory.IsWebBrowserContextMenuEnabled = False

        ' ##################发送组件开始#######################
        pnlSendComponentContainer.Size = New Size(Me.Width, cmbDanmuInput.Height + cmbDanmuInput.Margin.Bottom + btnSendDanmu.Height + btnSendDanmu.Margin.Bottom + marginWidth * 2)
        pnlSendComponentContainer.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        pnlSendComponentContainer.Location = New Point(webChatHistory.Left, webChatHistory.Bottom)

        cmbDanmuInput.Size = New Size(pnlSendComponentContainer.Width - lblDanmuLength.Width, cmbDanmuInput.Height)
        cmbDanmuInput.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        cmbDanmuInput.Location = New Point(0, cmbDanmuInput.Margin.Top)
        cmbDanmuInput.SetCueBanner("Ctrl+Shift+F取焦点，Enter键发弹幕，超长自动分条发")

        lblDanmuLength.LocationCenterAlign(cmbDanmuInput, cmbDanmuInput.Location.X + cmbDanmuInput.Width)
        lblDanmuLength.Anchor = AnchorStyles.Right Or AnchorStyles.Bottom

        lblBorderLeft.Location = New Point(0, 0)
        lblBorderLeft.Size = New Size(1, pnlSendComponentContainer.Top + cmbDanmuInput.Height + cmbDanmuInput.Margin.Bottom)
        lblBorderLeft.BringToFront()
        lblBorderLeft.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom Or AnchorStyles.Top

        lblBorderRight.Location = New Point(pnlSendComponentContainer.Width - marginWidth, 0)
        lblBorderRight.Size = lblBorderLeft.Size
        lblBorderRight.BringToFront()
        lblBorderRight.Anchor = AnchorStyles.Right Or AnchorStyles.Bottom Or AnchorStyles.Top

        lblBorderBottom1.Margin = New Padding(3, 0, 3, 0)
        lblBorderBottom1.Location = New Point(0, marginWidth)
        lblBorderBottom1.Size = New Size(pnlSendComponentContainer.Width, marginWidth)
        lblBorderBottom1.Anchor = cmbDanmuInput.Anchor

        lblBorderBottom2.Margin = lblBorderBottom1.Margin
        lblBorderBottom2.Location = New Point(0, cmbDanmuInput.Bottom)
        lblBorderBottom2.Size = lblBorderBottom1.Size
        lblBorderBottom2.Anchor = cmbDanmuInput.Anchor

        btnSendDanmu.Top = lblBorderBottom2.Bottom + btnSendDanmu.Margin.Top
        btnSendDanmu.Left = lblBorderLeft.Left

        lblSendDanmuStatus.Top = btnSendDanmu.Top
        lblSendDanmuStatus.Left = btnSendDanmu.Left + btnSendDanmu.Width

        picDanmuColor.Top = lblSendDanmuStatus.Top
        picDanmuColor.Left = pnlSendComponentContainer.Width - picDanmuColor.Width - picDanmuColor.Margin.Right
        picDanmuColor.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right

        picYanText.Top = lblSendDanmuStatus.Top
        picYanText.Left = picDanmuColor.Left - picYanText.Width - picYanText.Margin.Right
        picYanText.Anchor = picDanmuColor.Anchor

        btnSendTest.Top = lblSendDanmuStatus.Top
        btnSendTest.Left = picYanText.Left - btnSendTest.Width - btnSendTest.Margin.Right
        btnSendTest.Anchor = picDanmuColor.Anchor

        picHotWord.Top = lblSendDanmuStatus.Top
        picHotWord.Left = btnSendTest.Left - picHotWord.Width - picHotWord.Margin.Right
        picHotWord.Anchor = picDanmuColor.Anchor
        ' ##################发送组件结束#######################

        LoadDanmuDisplayTemplete()

        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Private Sub UIControl(ByVal status As WorkStatus)
        Select Case status
            Case WorkStatus.Init
                Me.Enabled = False
            Case WorkStatus.Done
                Me.Enabled = True
        End Select
    End Sub

    Private Sub LoadDanmuDisplayTemplete()
#Region "多线程加载网页"
        ' app.exe and app.vshost.exe
        Dim appname As String = String.Concat(Process.GetCurrentProcess().ProcessName, ".exe")
        ' 改变程序内部IE浏览器默认的版本号
        webChatHistory.SetVersionEmulation(BrowserEmulationMode.IE11, appname)
        'webChatHistory.Navigate("about:blank")
        Dim startPath = Application.StartupPath
        If "\" <> startPath.Substring(startPath.Length - 1) Then
            startPath += "\"
        End If
        ' 多线程加载网页(WebBrowser只能是单线程单元)
        ' 因为task模型不能直接设置Threading.ApartmentState.STA，所以这里目前只能用Threading.Thread实现 20170924
        ' 此处必须加载先一个页面，这里用about:blank，否则会发生“无法获取“WebBrowser”控件的窗口句柄。不支持无窗口的 ActiveX 控件。”错误
        Dim newThread As New Threading.Thread(
            Sub()
                Try
                    webChatHistory.Navigate("about:blank")
                    webChatHistory.Navigate(startPath & "res\DanmuDisplayTemplete.html")
                Catch ex As Exception
                    MessageBox.Show("加载登录页失败，请关闭网页后重新打开再试" & RandomEmoji.Sad, "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Sub)
        ' 多线程操作webbrowser控件，一定要设置为 STA 模式
        newThread.TrySetApartmentState(Threading.ApartmentState.STA)
        newThread.Start()
#End Region
    End Sub

    ''' <summary>
    ''' 创建实例
    ''' </summary>
    Public Sub Create()
        If m_DanmuSender IsNot Nothing Then Return
        m_DanmuSender = New DanmuSender()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="user"></param>
    ''' <param name="thanksHime"></param>
    ''' <param name="location">控件显示位置</param>
    ''' <param name="containerSize">控件父容器的大小</param>
    Public Sub Init(ByRef user As UserInfo, ByRef thanksHime As ThanksHimeEntity, ByVal location As Point, ByVal containerSize As Size)
        ' 初始化弹幕发送器
        Try
            m_User = user
            m_ThanksHime = thanksHime

            Me.Location = location
            Me.Size = New Size(containerSize.Width, containerSize.Height - location.Y)


            If LoginManager.NotLoginUserId = m_User.Id Then
                pnlSendComponentContainer.Enabled = False
                webChatHistory.Size = Me.Size
                lblBorderLeft.Height = webChatHistory.Height
                lblBorderRight.Height = lblBorderLeft.Height
                Exit Try
            End If

            SetDanmuLengthLabel()

            RegistHotkey()

            If m_DanmuSender Is Nothing Then
                Create()
            Else
                Dim sender = New DanmuSender.DanmuSenderInfo With {
                    .RoomId = m_User.ViewRoom.RealId,
                    .Token = m_User.Token,
                    .JoinedLiveRoomTimestamp = m_User.ViewRoom.JoinedTimestamp.ToStringOfCulture,
                    .DanmuMaxLength = m_User.ViewRoom.Guard.DanmuMaxLength,
                    .DanmuColorDec = m_User.ViewRoom.Guard.DanmuColorDec,
                    .DanmuFontSize = m_User.ViewRoom.Guard.DanmuFontSize,
                    .Level = m_User.ViewRoom.Guard.Level,
                    .CanSendDanmuType = DanmuType.All
                }
                m_DanmuSender.Configure(sender, m_ThanksHime.DanmuRepeatitiveHandle)
            End If
        Catch ex As Exception
            Windows2.DrawTipsTask(If(Me.Parent, Me), "弹幕发送组件初始化失败" & RandomEmoji.Sad, 2000, False, False)
            Logger.WriteLine(ex)
        Finally
            If m_DanmuSender IsNot Nothing Then UIControl(WorkStatus.Done)
        End Try
    End Sub

    Private Sub RegistHotkey()
        ' 注册全局热键，Ctrl+Shift+F ，如果已经打开过一个或者多个姬娘 会导致注册失败
        If m_HotKeyId = 0 Then
            m_HotKeyId = GlobalAddAtom(HotKeyAtomName)
        End If

        m_RegistHotkeySucceed = RegisterHotKey(Me.Handle, m_HotKeyId, FsModifiers.MOD_CONTROL Or FsModifiers.MOD_SHIFT, Keys.F)
        If Not m_RegistHotkeySucceed Then
            Windows2.DrawTipsTask(If(Me.Parent, Me), "弹幕输入框取焦快捷键注册失败" & RandomEmoji.Sad, 2000, False, False)
        End If
    End Sub

    Private Sub btnSendDanmu_Click(sender As Object, e As EventArgs) Handles btnSendDanmu.Click
        TrySendDanmuByUser()
    End Sub

    Private Sub cmbDanmuInput_TextChanged(sender As Object, e As EventArgs) Handles cmbDanmuInput.TextChanged
        SetDanmuLengthLabel()
    End Sub

    Private Sub SetDanmuLengthLabel()
        Dim danmuLength = cmbDanmuInput.Text.Length
        Dim maxLength = If(m_User Is Nothing, 20, m_User.ViewRoom.Guard.DanmuMaxLength)
        lblDanmuLength.Text = danmuLength.ToStringOfCulture & "/" & maxLength.ToStringOfCulture

        ' 符合长度 绿色，超长 红色
        lblDanmuLength.ForeColor = If(danmuLength > maxLength, Color.Red, Color.Green)
    End Sub

    Private Sub cmbDanmuInput_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbDanmuInput.SelectedIndexChanged
        ' ###############未实现用户选择快捷短语之后 光标在 combobox 最后 而且不选中字符串###############
        'FocusOnInputbox()
        'If cmbDanmuInput.SelectionLength > 0 Then
        '    Debug.Print(Logger.MakeDebugString( cmbDanmuInput.SelectedText))
        'End If
    End Sub

    Private Sub cmbDanmuInput_KeyUp(sender As Object, e As KeyEventArgs) Handles cmbDanmuInput.KeyUp
        ' 回车发送弹幕
        ' 不能只用按回车键这个条件判断，因为用输入法打字时按回车，也是会触发 cmbDanmuInput.KeyUp 事件（所有包含textbox控件的控件都会这样）
        ' 20181208
        If e.KeyCode = Keys.Enter AndAlso inputBoxPressEnterKey Then
            TrySendDanmuByUser()
            inputBoxPressEnterKey = False
        End If
    End Sub

    Private Sub cmbDanmuInput_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles cmbDanmuInput.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            inputBoxPressEnterKey = True
        End If
    End Sub

    ''' <summary>
    ''' 程序发送弹幕
    ''' </summary>
    Public Sub TrySendDanmuByEventTask(ByVal danmu As String, ByVal type As DanmuType)
        If Not m_DanmuSender.CanSend Then Return

        m_DanmuSender.Add(
            New DanmuInfo With {
                .Context = danmu,
                .Type = type,
                .Source = DanmuSource.Internal
            })
    End Sub

    ''' <summary>
    ''' 程序发送弹幕
    ''' </summary>
    Public Sub TrySendDanmuByEventTask(ByVal danmu As String, ByVal type As DanmuType, ByVal count As Integer, ByVal unit As GiftUnit)
        If Not m_DanmuSender.CanSend Then Return

        m_DanmuSender.Add(
            New DanmuInfo With {
                .Context = danmu,
                .Type = type,
                .Source = DanmuSource.Internal,
                .Count = count,
                .Unit = unit
            })
    End Sub

    ''' <summary>
    ''' 用户发送弹幕
    ''' </summary>
    Private Sub TrySendDanmuByUser()
        If Not m_DanmuSender.CanSend Then Return

        ' 业务逻辑跟ui一起处理

        m_InputDanmu = cmbDanmuInput.Text
        ' 没有输入弹幕时直接返回
        If m_InputDanmu.Length = 0 Then Return

        If Not m_User.IsLogined Then
            Windows2.DrawTipsTask(If(Me.Parent, Me), "你是耍流氓嘛???还没有登录呢~" & RandomEmoji.Angry, 1000, False, False)
            Return
        End If

        'm_RoomViewerManageForm = New FrmRoomViewerManage(If(m_User.Role = UserRole.Uper, True, False), m_User.ViewRoom.IsAdmin, m_User.ViewRoom.RealId, String.Empty, String.Empty, FrmRoomViewerManage.ManageMode.ShieldSetting)
        'm_RoomViewerManageForm.ShowAndActivate()
        'Return

        ' 使用阻塞的方式，发完一条才能继续发下一条
        SendDanmuBegin(DanmuSource.Input)
        m_DanmuSender.Add(New DanmuInfo With {.Context = m_InputDanmu, .Source = DanmuSource.Input})
    End Sub

    Private Sub SendDanmuBegin(ByVal danmuSource As DanmuSource)
        ' 用户发送弹幕时才需要更改UI
        If danmuSource = DanmuSource.Input Then
            cmbDanmuInput.Text = String.Empty
            ' 发送中 金色 ，发送成功 绿色 ，发送失败，橙红色
            ' 发送中 锁住文本框，发送完毕启用文本框（结果可能是发送一条信息之后，用户不能马上输入，有点用户可能不喜欢这样的体验）；
            ' 发送成功 清空文本框， 发送失败， 保留并提示失败原因
            lblSendDanmuStatus.ForeColor = Color.Gold
        End If
    End Sub

    Private Sub SendDanmuCompleted(ByRef e As DanmuSendCompletedEventArgs)
        ' 1.内容非法会导致发送失败
        ' 2.或者情况不明
        ' 为了更好的用户体验，直接将发送失败的消息当做弹幕显示
        If Not e.IsAllow OrElse e.Danmu Is Nothing Then
            AppendToChatHistory(e.Danmu.Context)
        End If

        ' 用户发送弹幕时才需要更改UI
        If e.Danmu.Source = DanmuSource.Input Then
            If e.Success Then
                lblSendDanmuStatus.ForeColor = If(e.IsRepeat, Color.Gold, Color.LimeGreen)
            Else
                ' 回填发送失败的弹幕（不保证每次都回填成功，因为发送失败的弹幕可能已经被新弹幕覆盖了）
                cmbDanmuInput.Text = m_InputDanmu
                lblSendDanmuStatus.ForeColor = Color.OrangeRed

                Windows2.DrawTipsTask(If(Me.Parent, Me), e.Message, 1000, False, False)
            End If

            cmbDanmuInput.Focus()
        End If
    End Sub

    Private Sub m_DanmuSender_SendCompleted(sender As Object, e As DanmuSendCompletedEventArgs) Handles m_DanmuSender.SendCompleted
        If cmbDanmuInput.InvokeRequired Then
            Me.BeginInvoke(Sub() SendDanmuCompleted(e))
        Else
            SendDanmuCompleted(e)
        End If
    End Sub

    ''' <summary>
    ''' 向弹幕显示框插入一条新弹幕
    ''' </summary>
    ''' <param name="danmu"></param>
    Public Sub AppendToChatHistory(ByVal danmu As String)
        Dim p = m_WebDoc.CreateElement("p")
        p.InnerHtml = danmu
        DanmuDiv.AppendChild(p)

        ' 用户调整滚动条后(滚动条不在底部-100的范围之内，视为调整了滚动条wei)的一分钟之内，不自动滚动到最底
        If Not ((m_WebScroll.OffsetRectangle.Height + m_WebScroll.ScrollTop + 100) >= m_WebScroll.ScrollRectangle.Height) Then
            ' AndAlso (m_WebScroll.OffsetRectangle.Height + m_WebScroll.ScrollTop) = m_WebScroll.ScrollRectangle.Height
            If Date.Now.AddMinutes(-1) < m_PreWebScrolledTime Then
                Return
            End If
        End If
        m_PreWebScrolledTime = Date.Now

        ScrollToBottom()

        If DanmuEntry.Configment.FlashWindowWhileReceiveDanmu Then
            ' 闪烁窗体提醒用户查看新信息
            Me.ParentForm.FlashWindowEx
            ' 异步，3秒后停止闪烁,保持橙色图标
            Task.Delay(3000).ContinueWith(Sub() Me.ParentForm.FlashWindowEx(FlashWindow.FLASHW_STOP))
        End If
    End Sub

    ''' <summary>
    ''' 滚动条划到最下
    ''' </summary>
    Public Sub ScrollToBottom()
        If webChatHistory.InvokeRequired Then
            Me.BeginInvoke(Sub() m_WebDoc.Window.ScrollTo(0, m_WebDoc.Body.ScrollRectangle.Height))
        Else
            m_WebDoc.Window.ScrollTo(0, m_WebDoc.Body.ScrollRectangle.Height)
        End If
    End Sub

    Private Sub picYanText_MouseMove(sender As Object, e As MouseEventArgs) Handles picYanText.MouseMove, picHotWord.MouseMove, picDanmuColor.MouseMove
        Dim pic = DirectCast(sender, PictureBox)
        If pic Is Nothing Then Return

        pic.BackColor = Color.Pink
    End Sub

    Private Sub picYanText_MouseLeave(sender As Object, e As EventArgs) Handles picYanText.MouseLeave, picHotWord.MouseLeave, picDanmuColor.MouseLeave
        Dim pic = DirectCast(sender, PictureBox)
        If pic Is Nothing Then Return

        pic.BackColor = Color.Transparent
    End Sub

    Private Sub picYanText_Click(sender As Object, e As EventArgs) Handles picYanText.Click
        Using frm = New FrmYanText
            frm.ShowFollowMousePosition(MouseLeaveAction.Hide, picYanText.Height, True)

            ' 关闭窗体的时候检查，如果用户已经设置了 颜文字 则获取
            If frm.SelectYanText IsNot Nothing Then
                cmbDanmuInput.Text += frm.SelectYanText
            End If
        End Using

        ' 设置焦点，用户可以马上按Enter键发送
        FocusOnInputbox()
    End Sub


    Private Sub picDanmuColor_Click(sender As Object, e As EventArgs) Handles picDanmuColor.Click
        Using frm As New FrmDanmuColor(m_User.ViewRoom.Guard.DanmuColorDec)
            frm.ShowFollowMousePosition(MouseLeaveAction.Hide, picDanmuColor.Size.Height, True)
        End Using

        ' 设置焦点，用户可以马上按Enter键发送
        FocusOnInputbox()
    End Sub

    Private Sub FocusOnInputbox()
        cmbDanmuInput.Focus()
        cmbDanmuInput.SelectionStart = cmbDanmuInput.Text.Length
        cmbDanmuInput.SelectionLength = 0
    End Sub

    Private Sub webChatHistory_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles webChatHistory.DocumentCompleted
        m_WebDoc = webChatHistory.Document
        If m_WebDoc Is Nothing Then Return

        SystemMessageSpan = m_WebDoc.GetElementById(SystemMsgSpanId)
        If SystemMessageSpan IsNot Nothing Then
            SystemMessageSpan.Style = "display:" & If(DanmuEntry.Configment.EnabledSystemMessageHime, "inline", "None")
        End If
        DanmuDiv = m_WebDoc.GetElementById(DanmuDivId)
        m_WebScroll = m_WebDoc.GetElementsByTagName("html")(0)
    End Sub

    Private Sub webChatHistory_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles webChatHistory.PreviewKeyDown
        If e.Control AndAlso e.KeyCode = Keys.C Then
            CopySelectedContextInBrowser()
        End If
    End Sub

    Private Sub tsmitCopyInBrowser_Click(sender As Object, e As EventArgs) Handles tsmitCopyInBrowser.Click
        CopySelectedContextInBrowser()
    End Sub

    Private Function GetSelectedContextInBrowser() As String
        Dim rst = webChatHistory.Document.RunJavaScript("GetSelectedContext")
        Return rst?.ToString
    End Function

    Private Sub CopySelectedContextInBrowser()
        Dim selecteedContext = GetSelectedContextInBrowser()
        If selecteedContext Is Nothing Then Return

        Common.CopyToClipboard(selecteedContext, If(Me.Parent, Me))
    End Sub

    Private Sub cmsBrowserRightButtonClick_Opening(sender As Object, e As CancelEventArgs) Handles cmsBrowserRightButtonClick.Opening
        ' 未选中不显示复制按钮
        Dim selecteedContext = GetSelectedContextInBrowser()
        e.Cancel = (selecteedContext Is Nothing)
        tsmitCopyInBrowser.Visible = Not e.Cancel
    End Sub

    Private Sub lblSendDanmuStatus_MouseHover(sender As Object, e As EventArgs) Handles lblSendDanmuStatus.MouseHover
        Dim tips As String
        Select Case lblSendDanmuStatus.ForeColor
            Case Color.Gold
                tips = "发送中"
            Case Color.LimeGreen
                tips = "成功"
            Case Color.OrangeRed
                tips = "失败"
            Case Else
                tips = "未知状态"
        End Select

        lblSendDanmuStatus.ShowTips(tips)
    End Sub

    ''' <summary>
    ''' 直播状态发生改变
    ''' </summary>
    ''' <param name="status"></param>
    Public Sub OnLiveStatusChange(ByVal status As LiveStatus)
        ' 连接中断后不能发弹幕，因为发了也接收不到
        cmbDanmuInput.Enabled = (LiveStatus.Break <> status)
    End Sub

    ''' <summary>
    ''' 清空聊天记录
    ''' </summary>
    Private Sub ClearChatHistory()
        DanmuDiv.InnerHtml = String.Empty
    End Sub

    'Private Sub m_WebDoc_MouseDown(sender As Object, e As HtmlElementEventArgs) Handles m_WebDoc.MouseDown

    'End Sub

    'Private Sub m_WebDoc_MouseUp(sender As Object, e As HtmlElementEventArgs) Handles m_WebDoc.MouseUp

    'End Sub

    Private Sub m_WebDoc_Click(sender As Object, e As HtmlElementEventArgs) Handles m_WebDoc.Click
        Select Case m_WebDoc.ActiveElement.Id
            Case DanmuClearButtonId
                ClearChatHistory()
            Case DanmuWindowAdjustButtonId

            Case Else
                ShowUserNickClickMenu()
        End Select
    End Sub

    ''' <summary>
    ''' 点击用户昵称弹出右键菜单
    ''' </summary>
    Private Sub ShowUserNickClickMenu()
        m_SelectViewerId = m_WebDoc.ActiveElement.GetAttribute("data-uid")
        If m_SelectViewerId.IsNullOrEmpty Then Return

        m_SelectViewerNick = m_WebDoc.ActiveElement.GetAttribute("value")
        Dim sql = $"select remark from viewerRemark where userId = '{m_SelectViewerId}'"
        Dim remark = IO2.Database.SQLiteHelper.GetFirst(sql)
        mnitmUseUseNickAsReMark.Enabled = (remark Is Nothing)
        mnitmDeleteReMark.Enabled = Not mnitmUseUseNickAsReMark.Enabled
        If remark Is Nothing Then remark = "未设置备注"
        ' 备注与昵称相同则不显示备注
        mnitmNickAndRemark.Text = $"{m_SelectViewerNick}{If(m_SelectViewerNick.Equals(remark.ToString), String.Empty, $"（{remark}）")}"

        m_SelectDanmu = m_WebDoc.ActiveElement.Parent.Children(3).InnerText
        m_SelectViewerTs = m_WebDoc.ActiveElement.GetAttribute("data-ts")
        m_SelectViewerCt = m_WebDoc.ActiveElement.GetAttribute("data-ct")

        cmsUserNickClick.Show(MousePosition)
    End Sub

    ''' <summary>
    ''' 更新进入直播间的时间戳（第一次或者是每次断线重连之后都需要更改加入直播间的时间戳）
    ''' </summary>
    ''' <param name="newValue"></param>
    Public Sub UpdateJoinedLiveRoomTimestamp(ByVal newValue As Long)
        If m_User.ViewRoom.JoinedTimestamp = 0 OrElse
            m_User.ViewRoom.JoinedTimestamp < newValue Then
            ' 第一次或者是每次断线重连之后都需要更改加入直播间的时间戳
            m_User.ViewRoom.JoinedTimestamp = newValue
            m_DanmuSender.UpdateJoinedLiveRoomTimestamp(newValue)
        End If
    End Sub

    ''' <summary>
    ''' 修改重复弹幕处理方式
    ''' </summary>
    ''' <param name="handle"></param>
    Public Sub ChangeDanmuRepeatitiveHandle(ByVal handle As DanmuRepeatOptions)
        m_ThanksHime.DanmuRepeatitiveHandle = handle
        m_DanmuSender.ChangeDanmuRepeatitiveHandle(handle)
    End Sub
#End Region

    Private Async Sub cmsUserNickClickMenuItem_ClickAsync(sender As Object, e As EventArgs) Handles mnitmReportThisDanmu.Click, mnitmNickAndRemark.Click, mnitmGotoUserZone.Click, mnitmRemoveFromBlackList.Click, mnitmAddToBlackList1Hour.Click, mnitmSettingReMark.Click, mnitmDeleteReMark.Click, mnitmBlackListManage.Click, mnitmAddToBlackList8Hour.Click, mnitmAddToBlackList24Hour.Click, mnitmAddToBlackList12Hour.Click, mnitmAdminUnAppoint.Click, mnitmAdminManage.Click, mnitmAdminAppoint.Click, mnitmCopyViewerNick.Click, mnitmCopyViewerId.Click, mnitmRoomShieldViewerManage.Click, mnitmShieldThisBadGuy.Click, mnitmUnShieldThisBadGuy.Click, mnitmUseUseNickAsReMark.Click
        Dim mnitm = DirectCast(sender, ToolStripMenuItem)
        If mnitm Is Nothing Then Return

        Select Case mnitm.Name
            Case mnitmUseUseNickAsReMark.Name
                AddRemark()
            Case mnitmSettingReMark.Name
                ShowRoomViewerManager(m_SelectViewerId, m_SelectViewerNick, FrmRoomViewerManage.ManageMode.RemarkSetting)
            Case mnitmDeleteReMark.Name
                DeleteRemark()
            Case mnitmCopyViewerId.Name
                CopySelectedViewerId()
            Case mnitmCopyViewerNick.Name
                CopySelectedViewerNick()
            Case mnitmGotoUserZone.Name
                GotoUserZone()
            Case mnitmShieldThisBadGuy.Name
                Await ShieldThisBadGuyAsync(True)
            Case mnitmUnShieldThisBadGuy.Name
                Await ShieldThisBadGuyAsync(False)
            Case mnitmRoomShieldViewerManage.Name
                ShowRoomViewerManager(m_SelectViewerId, m_SelectViewerNick, FrmRoomViewerManage.ManageMode.ShieldSetting)
            Case mnitmReportThisDanmu.Name
                Await ReportDanmu()
            Case mnitmAddToBlackList1Hour.Name, mnitmAddToBlackList8Hour.Name, mnitmAddToBlackList12Hour.Name, mnitmAddToBlackList24Hour.Name
                Dim hour = mnitmAddToBlackList1Hour.Text.Substring(0, mnitmAddToBlackList1Hour.Text.Length - 2).ToIntegerOfCulture
                Await AddToBlackListAsync(hour)
            Case mnitmRemoveFromBlackList.Name
                Await RemoveFromBlackListAsync()
            Case mnitmBlackListManage.Name
                ShowRoomViewerManager(m_SelectViewerId, m_SelectViewerNick, FrmRoomViewerManage.ManageMode.BlackViewerSetting)
            Case mnitmAdminAppoint.Name
                Await AppointAdminAsync()
            Case mnitmAdminUnAppoint.Name
                Await UnAppointAdminAsync()
            Case mnitmAdminManage.Name
                ShowRoomViewerManager(m_SelectViewerId, m_SelectViewerNick, FrmRoomViewerManage.ManageMode.RoomAdminSetting)
        End Select
    End Sub

    Private Sub cmsUserNickClick_Opening(sender As Object, e As CancelEventArgs) Handles cmsUserNickClick.Opening
        mnitmBlackList.Visible = m_User.ViewRoom.IsAdmin
        mnitmBlackList.Enabled = m_User.ViewRoom.IsAdmin
        mnitmAdmin.Visible = m_User.ViewRoom.IsAdmin
        mnitmAdmin.Enabled = m_User.ViewRoom.IsAdmin
    End Sub

    Private Sub AddRemark()
        Dim rst = DanmuEntry.AddRemark(m_SelectViewerId, m_SelectViewerNick, m_SelectViewerNick)
        If rst Then
            m_RoomViewerManageForm?.OnRemarkAdd()
        End If
        Common.ShowOperateResultTask(Me, rst)
    End Sub

    Private Sub DeleteRemark()
        Dim rst = DanmuEntry.DeleteRemark(m_SelectViewerId)
        If rst Then
            m_RoomViewerManageForm?.OnRemarkDeleted(m_SelectViewerId)
        End If
        Common.ShowOperateResultTask(Me, rst)
    End Sub

    Private Sub CopySelectedViewerId()
        Common.CopyToClipboard(m_SelectViewerId, If(Me.Parent, Me))
    End Sub

    Private Sub CopySelectedViewerNick()
        Common.CopyToClipboard(m_SelectViewerNick, If(Me.Parent, Me))
    End Sub

    Private Sub GotoUserZone()
        Dim liveUrl = "https://space.bilibili.com/" & m_SelectViewerId
        Process.Start(liveUrl)
    End Sub

    ''' <summary>
    ''' 举报弹幕
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ReportDanmu() As Task
        Dim reason As BilibiliApi.ReportReason
        Using frm As New FrmReportReason(m_SelectViewerNick, m_SelectDanmu)
            frm.ShowFollowMousePosition(MouseLeaveAction.Close, 0, True)
            reason = frm.ReportReason
        End Using
        If BilibiliApi.ReportReason.无 = reason OrElse
            BilibiliApi.ReportReason.其他 < reason Then
            Return
        End If

        Dim rst = Await BilibiliApi.RoomReportDanmuAsync(m_User.ViewRoom.RealId, m_SelectViewerId, m_SelectViewerTs, m_SelectViewerCt, m_SelectDanmu, reason)
        Common.ShowOperateResultTask(Me, rst)
    End Function

    ''' <summary>
    ''' 屏蔽观众
    ''' </summary>
    ''' <param name="yes">屏蔽为True,解除屏蔽为False</param>
    ''' <returns></returns>
    Private Async Function ShieldThisBadGuyAsync(ByVal yes As Boolean) As Task
        If yes Then
            Dim rst = Await BilibiliApi.RoomShieldViewerAsync(m_SelectViewerId)
            Common.ShowOperateResultTask(Me, rst)
        Else
            Dim rst = Await BilibiliApi.RoomUnShieldViewerAsync(m_SelectViewerId)
            Common.ShowOperateResultTask(Me, rst)
        End If
    End Function

    ''' <summary>
    ''' 禁言
    ''' </summary>
    ''' <param name="hour"></param>
    ''' <returns></returns>
    Private Async Function AddToBlackListAsync(ByVal hour As Integer) As Task
        Dim rst = Await BilibiliApi.RoomBlackViewerAsync(m_SelectViewerNick, hour)
        ' 添加到记录表
        If rst.Success Then
            Dim jobj = MSJsSerializer.DeserializeObject(rst.Result)
            m_BlockList.TryAdd(m_SelectViewerId.ToIntegerOfCulture, CInt(jobj("data")("id")))
        End If
        Common.ShowOperateResultTask(Me, rst)
    End Function

    ''' <summary>
    ''' 撤销禁言
    ''' </summary>
    ''' <returns></returns>
    Private Async Function RemoveFromBlackListAsync() As Task
        Dim blockId As Integer
        If Not m_BlockList.TryGetValue(m_SelectViewerId.ToIntegerOfCulture, blockId) Then
            Dim getRst = Await BilibiliApi.GetRoomBlockListAsync(1)
            If Not getRst.Success Then
                Windows2.DrawTipsTask(Me, "获取禁言信息失败", 1000, False, False)
                Return
            End If

            Dim jobj = MSJsSerializer.DeserializeObject(getRst.Message)
            If jobj Is Nothing Then
                Windows2.DrawTipsTask(Me, "快速撤销禁言失败，请使用管理功能撤销", 1000, False, False)
                Return
            End If

            If TryCast(jobj("data"), Array)?.Length = 0 Then
                Windows2.DrawTipsTask(Me, "快速撤销禁言失败，请使用管理功能撤销", 1000, False, False)
                Return
            End If

            Dim list = MSJsSerializer.ConvertToType(Of RoomBlackViewerEntity.Data())(jobj("data"))
            For Each b In list
                m_BlockList.TryAdd(b.uid, b.id)
                If m_SelectViewerId.ToIntegerOfCulture = b.uid Then
                    blockId = b.id
                    Continue For
                End If
            Next
        End If

        If blockId <= 0 Then
            Windows2.DrawTipsTask(Me, "快速撤销禁言失败，请使用管理功能撤销", 1000, False, False)
            Return
        End If

        Dim rst = Await BilibiliApi.RoomUnBlackViewerAsync(blockId.ToString)
        If rst.Success Then
            m_BlockList.TryRemove(m_SelectViewerId.ToIntegerOfCulture, blockId)
        End If

        Common.ShowOperateResultTask(Me, rst)
    End Function

    ''' <summary>
    ''' 任命管理员
    ''' </summary>
    ''' <returns></returns>
    Private Async Function AppointAdminAsync() As Task
        Dim rst = Await BilibiliApi.RoomAppointAdminAsync(m_SelectViewerNick)
        Common.ShowOperateResultTask(Me, rst)
    End Function

    ''' <summary>
    ''' 撤销任命管理员
    ''' </summary>
    ''' <returns></returns>
    Private Async Function UnAppointAdminAsync() As Task
        Dim rst = Await BilibiliApi.RoomUnAppointAdminAsync(m_SelectViewerId)
        Common.ShowOperateResultTask(Me, rst)
    End Function

    Private Sub ShowRoomViewerManager(ByVal selectedViewerId As String, ByVal selectedViewerIdOrViewerName As String, ByVal manageMode As FrmRoomViewerManage.ManageMode)
        If m_RoomViewerManageForm Is Nothing Then
            m_RoomViewerManageForm = New FrmRoomViewerManage(If(m_User.Role = UserRole.Uper, True, False), m_User.ViewRoom.IsAdmin, m_User.ViewRoom.RealId, selectedViewerId, selectedViewerIdOrViewerName, manageMode)
        Else
            m_RoomViewerManageForm.ViewerId = selectedViewerId
            m_RoomViewerManageForm.ViewerIdOrViewerName = selectedViewerIdOrViewerName
            m_RoomViewerManageForm.CurrentManageMode = manageMode
        End If
        'm_User.ViewRoom.IsAdmin
        m_RoomViewerManageForm.ShowAndActivate()
    End Sub

    Private Sub lblDanmuLength_MouseHover(sender As Object, e As EventArgs) Handles lblDanmuLength.MouseHover
        lblDanmuLength.ShowTips("领取成就奖励之后才可以发20以上长度弹幕")
    End Sub
End Class
