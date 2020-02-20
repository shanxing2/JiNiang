Imports ShanXingTech


Public Class SettingControl
#Region "事件类"
    Public Class MedalUpgradeHimeEnsuredEventArgs
        Inherits EventArgs

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="enabled">是否开启</param>
        ''' <param name="viewRoomUserId">播主用户Id</param>
        Public Sub New(enabled As Boolean, viewRoomUserId As Integer)
            Me.Enabled = enabled
            Me.UpId = viewRoomUserId
        End Sub

        ''' <summary>
        ''' 是否开启
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Enabled As Boolean
        ''' <summary>
        ''' 播主用户Id
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property UpId As Integer
    End Class
#End Region

#Region "事件"
    ''' <summary>
    ''' 
    ''' </summary>
    Public Event MedalUpgradeHimeChanged As EventHandler(Of MedalUpgradeHimeEnsuredEventArgs)

#End Region

#Region "字段区"
    Private m_MainForm As FrmMain
    Private m_PluginManageForm As FrmPluginManage
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
                If components IsNot Nothing Then
                    components.Dispose()
                    components = Nothing
                End If

                If m_MainForm IsNot Nothing Then
                    m_MainForm.Dispose()
                    m_MainForm = Nothing
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


    '<SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags:=SecurityPermissionFlag.UnmanagedCode)>
    '<SecurityPermissionAttribute(SecurityAction.InheritanceDemand, Flags:=SecurityPermissionFlag.UnmanagedCode)>
    'Protected Overrides Sub WndProc(ByRef m As Message)
    '    Debug.Print(ShanXingTech.Logger.MakeDebugString( m.ToString))

    '    MyBase.WndProc(m)
    'End Sub

    Public Sub New(ByRef mainForm As FrmMain, ByVal container As TabPage)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。

        m_MainForm = mainForm

        'Me.SuspendLayout()

        trbFormOpacity.TickStyle = TickStyle.TopLeft
        trbFormOpacity.Minimum = 1
        trbFormOpacity.Maximum = 100
        trbFormOpacity.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right

        lblMainFormOpacity.AutoSize = False
        lblMainFormOpacity.RightToLeft = RightToLeft.Yes
        lblMainFormOpacity.Text = m_MainForm.Opacity.ToString("P0")
        lblMainFormOpacity.Anchor = AnchorStyles.Top Or AnchorStyles.Right

        If DanmuEntry.User.ViewedRoomId = 0 Then
            btnChangeViewRoom.Enabled = False
        End If

        Me.Location = New Point(0, 0)
        Me.Size = container.Size
        Me.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom

        'Me.ResumeLayout(False)
        'Me.PerformLayout()
    End Sub

    Private Sub RadioButton_Click(sender As Object, e As EventArgs) Handles rdbtnWssMode.Click, rdbtnWsMode.Click, rdbtnTcpMode.Click, rdbtnHttpMode.Click
        Dim rdbtn = DirectCast(sender, RadioButton)
        If rdbtn Is Nothing Then Return

        Select Case rdbtn.Name
            Case rdbtnTcpMode.Name
                DanmuEntry.Configment.ConnectionMode = ConnectMode.Tcp
            Case rdbtnWssMode.Name
                DanmuEntry.Configment.ConnectionMode = ConnectMode.Wss
            Case rdbtnWsMode.Name
                DanmuEntry.Configment.ConnectionMode = ConnectMode.Ws
            Case Else
                DanmuEntry.Configment.ConnectionMode = ConnectMode.Tcp
        End Select

        DanmuEntry.Configment.ConnectionMode = DanmuEntry.Configment.ConnectionMode
        Windows2.DrawTipsTask(Me, rdbtn.Text & " 设置成功" & RandomEmoji.Happy, 500, True, False)
    End Sub

    Private Sub RadioButtonRepeatitive_Click(sender As Object, e As EventArgs) Handles rdbtnMergeRepeatitive.Click, rdbtnBlockRepeatitive.Click
        Dim rdbtn = DirectCast(sender, RadioButton)
        If rdbtn Is Nothing Then Return

        Select Case rdbtn.Name
            Case rdbtnBlockRepeatitive.Name
                m_MainForm.DanmuControl.ChangeDanmuRepeatitiveHandle(DanmuRepeatOptions.Block)
            Case rdbtnMergeRepeatitive.Name
                m_MainForm.DanmuControl.ChangeDanmuRepeatitiveHandle(DanmuRepeatOptions.Merge)
        End Select

        Windows2.DrawTipsTask(Me, rdbtn.Text & " 设置成功" & RandomEmoji.Happy, 500, True, False)
    End Sub

    Private Sub trbFormOpacity_Scroll(sender As Object, e As EventArgs) Handles trbFormOpacity.Scroll
        m_MainForm.Opacity = trbFormOpacity.Value / 100
        lblMainFormOpacity.Text = m_MainForm.Opacity.ToString("P0")
        DanmuEntry.Configment.MainForm.Opacity = trbFormOpacity.Value
    End Sub

    ''' <summary>
    ''' 更改人气刷新间隔
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub nudHeartbeatInterval_Leave(sender As Object, e As EventArgs) Handles nudHeartbeatInterval.Leave, nudBrokeSilenceInterval.Leave
        Dim nud = DirectCast(sender, NumericUpDown)
        If nud IsNot Nothing Then
            Dim newValue = CInt(nud.Value)
            Dim needUpdate As Boolean
            Select Case nud.Name
                Case nudHeartbeatInterval.Name
                    If DanmuEntry.Configment.HeartbeatInterval <> newValue Then
                        DanmuEntry.Configment.HeartbeatInterval = newValue
                        needUpdate = True
                    End If
                Case nudBrokeSilenceInterval.Name
                    If DanmuEntry.Configment.BrokeSilenceInterval <> newValue Then
                        DanmuEntry.Configment.BrokeSilenceInterval = newValue
                        needUpdate = True
                    End If
            End Select

            If needUpdate Then
                Windows2.DrawTipsTask(Me, "更改成功", 500, needUpdate, False)
            End If
        End If
    End Sub

    Private Sub chkSignAuto_MouseHover(sender As Object, e As EventArgs) Handles chkSignAuto.MouseHover
        If String.IsNullOrEmpty(DanmuEntry.User.SignRewards) Then Return
        ToolTip1.Show(DanmuEntry.User.SignRewards, chkSignAuto)
    End Sub

    Private Sub chk_Click(sender As Object, e As EventArgs) Handles chkEnabledThanksHime.Click, chkSignAuto.Click, chkEnabledAttentionHime.Click, chkEnabledWelcomeHime.Click, chkEnabledSystemMessageHime.Click, chkReceiveDoubleWatchAwardAuto.Click, chkFlashWindow.Click, chkEnabledMedalUpgradeHime.Click
        Dim chk = DirectCast(sender, CheckBox)
        If chk Is Nothing Then Return

        Select Case chk.Name
            Case chkFlashWindow.Name
                DanmuEntry.Configment.FlashWindowWhileReceiveDanmu = chk.Checked
            Case chkEnabledThanksHime.Name
                DanmuEntry.Configment.EnabledThanksHime = chk.Checked

                ' 动态配置感谢姬
                If m_MainForm.DanmuParser Is Nothing Then Exit Select
                If chk.Checked Then
                    AddHandler m_MainForm.DanmuParser.SilverFed, AddressOf m_MainForm.DanmuParser_SilverFed
                    AddHandler m_MainForm.DanmuParser.GoldFed, AddressOf m_MainForm.DanmuParser_GoldFed
                Else
                    RemoveHandler m_MainForm.DanmuParser.SilverFed, AddressOf m_MainForm.DanmuParser_SilverFed
                    RemoveHandler m_MainForm.DanmuParser.GoldFed, AddressOf m_MainForm.DanmuParser_GoldFed
                End If
            Case chkEnabledWelcomeHime.Name
                DanmuEntry.Configment.EnabledWelcomeHime = chk.Checked

                ' 动态配置欢迎姬
                If DanmuEntry.Configment.ConnectionMode = ConnectMode.Tcp AndAlso
                    m_MainForm.DanmuParser Is Nothing Then Exit Select

                If chk.Checked Then
                    AddHandler m_MainForm.DanmuParser.VipEntered, AddressOf m_MainForm.DanmuParser_VipEntered
                    AddHandler m_MainForm.DanmuParser.GuardEntered, AddressOf m_MainForm.DanmuParser_GuardEntered
                Else
                    RemoveHandler m_MainForm.DanmuParser.VipEntered, AddressOf m_MainForm.DanmuParser_VipEntered
                    RemoveHandler m_MainForm.DanmuParser.GuardEntered, AddressOf m_MainForm.DanmuParser_GuardEntered
                End If
            Case chkEnabledAttentionHime.Name
                DanmuEntry.Configment.EnabledAttentionHime = chk.Checked

                ' 动态配置粉丝姬
                If DanmuEntry.Configment.ConnectionMode = ConnectMode.Tcp AndAlso
                    m_MainForm.DanmuParser Is Nothing Then Exit Select

                If chk.Checked Then
                    AddHandler m_MainForm.DanmuParser.AttentionIncreased, AddressOf m_MainForm.DanmuParser_AttentionIncreased
                Else
                    RemoveHandler m_MainForm.DanmuParser.AttentionIncreased, AddressOf m_MainForm.DanmuParser_AttentionIncreased
                End If
            Case chkSignAuto.Name
                DanmuEntry.Configment.SignAuto = chk.Checked
            Case chkReceiveDoubleWatchAwardAuto.Name
                DanmuEntry.Configment.ReceiveDoubleWatchAwardAuto = chk.Checked
            Case chkEnabledSystemMessageHime.Name
                DanmuEntry.Configment.EnabledSystemMessageHime = chk.Checked

                If DanmuEntry.Configment.ConnectionMode = ConnectMode.Tcp AndAlso
                    m_MainForm.DmTcpClient IsNot Nothing Then
                    m_MainForm.DanmuControl.SystemMessageSpan.Style = "display:" & If(chk.Checked, "inline", "None")
                    m_MainForm.DanmuControl.DanmuDiv.Style = "padding-top:" & If(chk.Checked, "45px", "6.18px")
                    If chk.Checked Then
                        AddHandler m_MainForm.DanmuParser.SystemMessageChanged, AddressOf m_MainForm.DanmuParser_SystemMessageChanged
                    Else
                        RemoveHandler m_MainForm.DanmuParser.SystemMessageChanged, AddressOf m_MainForm.DanmuParser_SystemMessageChanged
                    End If
                End If
            Case chkEnabledMedalUpgradeHime.Name
                DanmuEntry.Configment.EnabledMedalUpgradeHime = chk.Checked

                If DanmuEntry.Configment.ConnectionMode = ConnectMode.Tcp AndAlso
                    m_MainForm.DanmuParser Is Nothing Then Exit Select

                RaiseEvent MedalUpgradeHimeChanged(Nothing, New MedalUpgradeHimeEnsuredEventArgs(chk.Checked, DanmuEntry.User.ViewRoom.UserId.ToIntegerOfCulture))

                If chk.Checked Then
                    AddHandler m_MainForm.DanmuParser.MedalUpgradeChecking, AddressOf m_MainForm.DanmuParser_MedalUpgradeChecking
                Else
                    RemoveHandler m_MainForm.DanmuParser.MedalUpgradeChecking, AddressOf m_MainForm.DanmuParser_MedalUpgradeChecking
                End If
        End Select
    End Sub

    Private Sub btnChangeViewRoom_Click(sender As Object, e As EventArgs) Handles btnChangeViewRoom.Click

    End Sub

    Private Sub btnCloseExe_Click(sender As Object, e As EventArgs) Handles btnCloseExe.Click
        m_MainForm.Close()
    End Sub

    Private Sub btnPluginManage_Click(sender As Object, e As EventArgs) Handles btnPluginManage.Click
        If m_PluginManageForm Is Nothing Then
            m_PluginManageForm = New FrmPluginManage
        End If

        m_PluginManageForm.Show()
    End Sub
End Class


