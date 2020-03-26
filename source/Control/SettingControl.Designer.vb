<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SettingControl
    Inherits System.Windows.Forms.UserControl

    ''UserControl 重写 Dispose，以清理组件列表。
    '<System.Diagnostics.DebuggerNonUserCode()> _
    'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    '    Try
    '        If disposing AndAlso components IsNot Nothing Then
    '            components.Dispose()
    '        End If
    '    Finally
    '        MyBase.Dispose(disposing)
    '    End Try
    'End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.nudBrokeSilenceInterval = New System.Windows.Forms.NumericUpDown()
        Me.lblConfigureThanksHime = New System.Windows.Forms.Label()
        Me.rdbtnBlockRepeatitive = New System.Windows.Forms.RadioButton()
        Me.rdbtnMergeRepeatitive = New System.Windows.Forms.RadioButton()
        Me.lblMainFormOpacity = New System.Windows.Forms.Label()
        Me.trbFormOpacity = New System.Windows.Forms.TrackBar()
        Me.chkEnabledSystemMessageHime = New System.Windows.Forms.CheckBox()
        Me.chkEnabledWelcomeHime = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.rdbtnHttpMode = New System.Windows.Forms.RadioButton()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.rdbtnWsMode = New System.Windows.Forms.RadioButton()
        Me.rdbtnWssMode = New System.Windows.Forms.RadioButton()
        Me.rdbtnTcpMode = New System.Windows.Forms.RadioButton()
        Me.chkOpenSilverBoxAuto = New System.Windows.Forms.CheckBox()
        Me.chkReceiveDoubleWatchAwardAuto = New System.Windows.Forms.CheckBox()
        Me.chkSignAuto = New System.Windows.Forms.CheckBox()
        Me.chkEnabledAttentionHime = New System.Windows.Forms.CheckBox()
        Me.chkEnabledThanksHime = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.nudHeartbeatInterval = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.btnChangeViewRoom = New System.Windows.Forms.Button()
        Me.btnCloseExe = New System.Windows.Forms.Button()
        Me.chkFlashWindow = New System.Windows.Forms.CheckBox()
        Me.btnPluginManage = New System.Windows.Forms.Button()
        Me.pnlHimesEnablerContainer = New System.Windows.Forms.Panel()
        Me.chkEnabledMedalUpgradeHime = New System.Windows.Forms.CheckBox()
        Me.txtViewRoomId = New System.Windows.Forms.TextBox()
        Me.chkDisplayOriginalNick = New System.Windows.Forms.CheckBox()
        CType(Me.nudBrokeSilenceInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.trbFormOpacity, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nudHeartbeatInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlHimesEnablerContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'nudBrokeSilenceInterval
        '
        Me.nudBrokeSilenceInterval.Enabled = False
        Me.nudBrokeSilenceInterval.Location = New System.Drawing.Point(20, 156)
        Me.nudBrokeSilenceInterval.Name = "nudBrokeSilenceInterval"
        Me.nudBrokeSilenceInterval.Size = New System.Drawing.Size(120, 21)
        Me.nudBrokeSilenceInterval.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.nudBrokeSilenceInterval, "设置这个一定程度上能避免漏掉消息")
        '
        'lblConfigureThanksHime
        '
        Me.lblConfigureThanksHime.AutoSize = True
        Me.lblConfigureThanksHime.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblConfigureThanksHime.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblConfigureThanksHime.Location = New System.Drawing.Point(116, 4)
        Me.lblConfigureThanksHime.Name = "lblConfigureThanksHime"
        Me.lblConfigureThanksHime.Size = New System.Drawing.Size(29, 12)
        Me.lblConfigureThanksHime.TabIndex = 123
        Me.lblConfigureThanksHime.Text = "设置"
        Me.ToolTip1.SetToolTip(Me.lblConfigureThanksHime, "点我自定义你的专属感谢姬(*￣︶￣)")
        Me.lblConfigureThanksHime.Visible = False
        '
        'rdbtnBlockRepeatitive
        '
        Me.rdbtnBlockRepeatitive.AutoSize = True
        Me.rdbtnBlockRepeatitive.Location = New System.Drawing.Point(3, 0)
        Me.rdbtnBlockRepeatitive.Name = "rdbtnBlockRepeatitive"
        Me.rdbtnBlockRepeatitive.Size = New System.Drawing.Size(47, 16)
        Me.rdbtnBlockRepeatitive.TabIndex = 2
        Me.rdbtnBlockRepeatitive.Text = "阻塞"
        Me.ToolTip1.SetToolTip(Me.rdbtnBlockRepeatitive, "5秒后发送")
        Me.rdbtnBlockRepeatitive.UseVisualStyleBackColor = True
        '
        'rdbtnMergeRepeatitive
        '
        Me.rdbtnMergeRepeatitive.AutoSize = True
        Me.rdbtnMergeRepeatitive.Checked = True
        Me.rdbtnMergeRepeatitive.Location = New System.Drawing.Point(86, 0)
        Me.rdbtnMergeRepeatitive.Name = "rdbtnMergeRepeatitive"
        Me.rdbtnMergeRepeatitive.Size = New System.Drawing.Size(47, 16)
        Me.rdbtnMergeRepeatitive.TabIndex = 3
        Me.rdbtnMergeRepeatitive.TabStop = True
        Me.rdbtnMergeRepeatitive.Text = "合并"
        Me.ToolTip1.SetToolTip(Me.rdbtnMergeRepeatitive, "五秒内未发出的信息合并成一条")
        Me.rdbtnMergeRepeatitive.UseVisualStyleBackColor = True
        '
        'lblMainFormOpacity
        '
        Me.lblMainFormOpacity.BackColor = System.Drawing.Color.Transparent
        Me.lblMainFormOpacity.ForeColor = System.Drawing.Color.Black
        Me.lblMainFormOpacity.Location = New System.Drawing.Point(345, 116)
        Me.lblMainFormOpacity.Name = "lblMainFormOpacity"
        Me.lblMainFormOpacity.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblMainFormOpacity.Size = New System.Drawing.Size(29, 12)
        Me.lblMainFormOpacity.TabIndex = 138
        Me.lblMainFormOpacity.Text = "100%"
        '
        'trbFormOpacity
        '
        Me.trbFormOpacity.Location = New System.Drawing.Point(21, 105)
        Me.trbFormOpacity.Name = "trbFormOpacity"
        Me.trbFormOpacity.Size = New System.Drawing.Size(330, 45)
        Me.trbFormOpacity.TabIndex = 137
        Me.trbFormOpacity.TickStyle = System.Windows.Forms.TickStyle.TopLeft
        '
        'chkEnabledSystemMessageHime
        '
        Me.chkEnabledSystemMessageHime.AutoSize = True
        Me.chkEnabledSystemMessageHime.Location = New System.Drawing.Point(2, 132)
        Me.chkEnabledSystemMessageHime.Name = "chkEnabledSystemMessageHime"
        Me.chkEnabledSystemMessageHime.Size = New System.Drawing.Size(132, 16)
        Me.chkEnabledSystemMessageHime.TabIndex = 136
        Me.chkEnabledSystemMessageHime.Text = "接收系统小喇叭公告"
        Me.chkEnabledSystemMessageHime.UseVisualStyleBackColor = True
        '
        'chkEnabledWelcomeHime
        '
        Me.chkEnabledWelcomeHime.AutoSize = True
        Me.chkEnabledWelcomeHime.Location = New System.Drawing.Point(3, 22)
        Me.chkEnabledWelcomeHime.Name = "chkEnabledWelcomeHime"
        Me.chkEnabledWelcomeHime.Size = New System.Drawing.Size(84, 16)
        Me.chkEnabledWelcomeHime.TabIndex = 135
        Me.chkEnabledWelcomeHime.Text = "开启欢迎姬"
        Me.chkEnabledWelcomeHime.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 206)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 12)
        Me.Label7.TabIndex = 133
        Me.Label7.Text = "感谢姬"
        '
        'Label8
        '
        Me.Label8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label8.Location = New System.Drawing.Point(50, 212)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(317, 1)
        Me.Label8.TabIndex = 132
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 3)
        Me.Label6.Margin = New System.Windows.Forms.Padding(3, 3, 3, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(113, 12)
        Me.Label6.TabIndex = 131
        Me.Label6.Text = "弹幕服务器连接方式"
        '
        'rdbtnHttpMode
        '
        Me.rdbtnHttpMode.AutoSize = True
        Me.rdbtnHttpMode.Location = New System.Drawing.Point(135, 0)
        Me.rdbtnHttpMode.Name = "rdbtnHttpMode"
        Me.rdbtnHttpMode.Size = New System.Drawing.Size(47, 16)
        Me.rdbtnHttpMode.TabIndex = 3
        Me.rdbtnHttpMode.Text = "HTTP"
        Me.rdbtnHttpMode.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label5.Location = New System.Drawing.Point(104, 9)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(263, 1)
        Me.Label5.TabIndex = 130
        '
        'rdbtnWsMode
        '
        Me.rdbtnWsMode.AutoSize = True
        Me.rdbtnWsMode.Location = New System.Drawing.Point(94, 0)
        Me.rdbtnWsMode.Name = "rdbtnWsMode"
        Me.rdbtnWsMode.Size = New System.Drawing.Size(35, 16)
        Me.rdbtnWsMode.TabIndex = 2
        Me.rdbtnWsMode.Text = "WS"
        Me.rdbtnWsMode.UseVisualStyleBackColor = True
        '
        'rdbtnWssMode
        '
        Me.rdbtnWssMode.AutoSize = True
        Me.rdbtnWssMode.Location = New System.Drawing.Point(47, 0)
        Me.rdbtnWssMode.Name = "rdbtnWssMode"
        Me.rdbtnWssMode.Size = New System.Drawing.Size(41, 16)
        Me.rdbtnWssMode.TabIndex = 1
        Me.rdbtnWssMode.Text = "WSS"
        Me.rdbtnWssMode.UseVisualStyleBackColor = True
        '
        'rdbtnTcpMode
        '
        Me.rdbtnTcpMode.AutoSize = True
        Me.rdbtnTcpMode.Checked = True
        Me.rdbtnTcpMode.Location = New System.Drawing.Point(0, 0)
        Me.rdbtnTcpMode.Name = "rdbtnTcpMode"
        Me.rdbtnTcpMode.Size = New System.Drawing.Size(41, 16)
        Me.rdbtnTcpMode.TabIndex = 0
        Me.rdbtnTcpMode.TabStop = True
        Me.rdbtnTcpMode.Text = "TCP"
        Me.rdbtnTcpMode.UseVisualStyleBackColor = True
        '
        'chkOpenSilverBoxAuto
        '
        Me.chkOpenSilverBoxAuto.AutoSize = True
        Me.chkOpenSilverBoxAuto.Location = New System.Drawing.Point(2, 110)
        Me.chkOpenSilverBoxAuto.Name = "chkOpenSilverBoxAuto"
        Me.chkOpenSilverBoxAuto.Size = New System.Drawing.Size(144, 16)
        Me.chkOpenSilverBoxAuto.TabIndex = 127
        Me.chkOpenSilverBoxAuto.Text = "自动打开直播在线宝箱"
        Me.chkOpenSilverBoxAuto.UseVisualStyleBackColor = True
        '
        'chkReceiveDoubleWatchAwardAuto
        '
        Me.chkReceiveDoubleWatchAwardAuto.AutoSize = True
        Me.chkReceiveDoubleWatchAwardAuto.Location = New System.Drawing.Point(2, 88)
        Me.chkReceiveDoubleWatchAwardAuto.Name = "chkReceiveDoubleWatchAwardAuto"
        Me.chkReceiveDoubleWatchAwardAuto.Size = New System.Drawing.Size(168, 16)
        Me.chkReceiveDoubleWatchAwardAuto.TabIndex = 126
        Me.chkReceiveDoubleWatchAwardAuto.Text = "自动领取双端观看直播奖励"
        Me.chkReceiveDoubleWatchAwardAuto.UseVisualStyleBackColor = True
        '
        'chkSignAuto
        '
        Me.chkSignAuto.AutoSize = True
        Me.chkSignAuto.Location = New System.Drawing.Point(2, 66)
        Me.chkSignAuto.Name = "chkSignAuto"
        Me.chkSignAuto.Size = New System.Drawing.Size(72, 16)
        Me.chkSignAuto.TabIndex = 125
        Me.chkSignAuto.Text = "自动签到"
        Me.chkSignAuto.UseVisualStyleBackColor = True
        '
        'chkEnabledAttentionHime
        '
        Me.chkEnabledAttentionHime.AutoSize = True
        Me.chkEnabledAttentionHime.Location = New System.Drawing.Point(2, 44)
        Me.chkEnabledAttentionHime.Name = "chkEnabledAttentionHime"
        Me.chkEnabledAttentionHime.Size = New System.Drawing.Size(108, 16)
        Me.chkEnabledAttentionHime.TabIndex = 124
        Me.chkEnabledAttentionHime.Text = "新粉丝关注提示"
        Me.chkEnabledAttentionHime.UseVisualStyleBackColor = True
        '
        'chkEnabledThanksHime
        '
        Me.chkEnabledThanksHime.AutoSize = True
        Me.chkEnabledThanksHime.Location = New System.Drawing.Point(3, 0)
        Me.chkEnabledThanksHime.Name = "chkEnabledThanksHime"
        Me.chkEnabledThanksHime.Size = New System.Drawing.Size(84, 16)
        Me.chkEnabledThanksHime.TabIndex = 122
        Me.chkEnabledThanksHime.Text = "开启感谢姬"
        Me.chkEnabledThanksHime.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(141, 187)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 12)
        Me.Label3.TabIndex = 35
        Me.Label3.Text = "数据刷新间隔(秒)"
        '
        'nudHeartbeatInterval
        '
        Me.nudHeartbeatInterval.Location = New System.Drawing.Point(20, 183)
        Me.nudHeartbeatInterval.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.nudHeartbeatInterval.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.nudHeartbeatInterval.Name = "nudHeartbeatInterval"
        Me.nudHeartbeatInterval.Size = New System.Drawing.Size(120, 21)
        Me.nudHeartbeatInterval.TabIndex = 34
        Me.nudHeartbeatInterval.Value = New Decimal(New Integer() {60, 0, 0, 0})
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Enabled = False
        Me.Label1.Location = New System.Drawing.Point(141, 160)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(137, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "分钟后有新弹幕语音提示"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 386)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 12)
        Me.Label4.TabIndex = 140
        Me.Label4.Text = "重复消息处理"
        '
        'Label9
        '
        Me.Label9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label9.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label9.Location = New System.Drawing.Point(71, 392)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(300, 1)
        Me.Label9.TabIndex = 139
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rdbtnBlockRepeatitive)
        Me.Panel1.Controls.Add(Me.rdbtnMergeRepeatitive)
        Me.Panel1.Location = New System.Drawing.Point(20, 407)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(320, 19)
        Me.Panel1.TabIndex = 141
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rdbtnTcpMode)
        Me.Panel2.Controls.Add(Me.rdbtnWsMode)
        Me.Panel2.Controls.Add(Me.rdbtnWssMode)
        Me.Panel2.Controls.Add(Me.rdbtnHttpMode)
        Me.Panel2.Location = New System.Drawing.Point(21, 24)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(240, 16)
        Me.Panel2.TabIndex = 142
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 93)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 144
        Me.Label2.Text = "透明度"
        '
        'Label10
        '
        Me.Label10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label10.Location = New System.Drawing.Point(50, 99)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(317, 1)
        Me.Label10.TabIndex = 143
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 46)
        Me.Label11.Margin = New System.Windows.Forms.Padding(3, 3, 3, 6)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(113, 12)
        Me.Label11.TabIndex = 146
        Me.Label11.Text = "弹幕服务器连接方式"
        '
        'Label12
        '
        Me.Label12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label12.Location = New System.Drawing.Point(104, 52)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(263, 1)
        Me.Label12.TabIndex = 145
        '
        'btnChangeViewRoom
        '
        Me.btnChangeViewRoom.Enabled = False
        Me.btnChangeViewRoom.Location = New System.Drawing.Point(196, 67)
        Me.btnChangeViewRoom.Name = "btnChangeViewRoom"
        Me.btnChangeViewRoom.Size = New System.Drawing.Size(46, 23)
        Me.btnChangeViewRoom.TabIndex = 148
        Me.btnChangeViewRoom.Text = "换房"
        Me.btnChangeViewRoom.UseVisualStyleBackColor = True
        '
        'btnCloseExe
        '
        Me.btnCloseExe.Location = New System.Drawing.Point(268, 67)
        Me.btnCloseExe.Name = "btnCloseExe"
        Me.btnCloseExe.Size = New System.Drawing.Size(46, 23)
        Me.btnCloseExe.TabIndex = 149
        Me.btnCloseExe.Text = "关闭"
        Me.btnCloseExe.UseVisualStyleBackColor = True
        '
        'chkFlashWindow
        '
        Me.chkFlashWindow.AutoSize = True
        Me.chkFlashWindow.Location = New System.Drawing.Point(20, 134)
        Me.chkFlashWindow.Name = "chkFlashWindow"
        Me.chkFlashWindow.Size = New System.Drawing.Size(192, 16)
        Me.chkFlashWindow.TabIndex = 150
        Me.chkFlashWindow.Text = "新信息闪烁提示（弹幕多慎用）"
        Me.chkFlashWindow.UseVisualStyleBackColor = True
        '
        'btnPluginManage
        '
        Me.btnPluginManage.Location = New System.Drawing.Point(292, 180)
        Me.btnPluginManage.Name = "btnPluginManage"
        Me.btnPluginManage.Size = New System.Drawing.Size(75, 23)
        Me.btnPluginManage.TabIndex = 151
        Me.btnPluginManage.Text = "插件管理"
        Me.btnPluginManage.UseVisualStyleBackColor = True
        '
        'pnlHimesEnablerContainer
        '
        Me.pnlHimesEnablerContainer.Controls.Add(Me.chkDisplayOriginalNick)
        Me.pnlHimesEnablerContainer.Controls.Add(Me.chkEnabledMedalUpgradeHime)
        Me.pnlHimesEnablerContainer.Controls.Add(Me.chkEnabledThanksHime)
        Me.pnlHimesEnablerContainer.Controls.Add(Me.chkOpenSilverBoxAuto)
        Me.pnlHimesEnablerContainer.Controls.Add(Me.chkReceiveDoubleWatchAwardAuto)
        Me.pnlHimesEnablerContainer.Controls.Add(Me.chkSignAuto)
        Me.pnlHimesEnablerContainer.Controls.Add(Me.chkEnabledAttentionHime)
        Me.pnlHimesEnablerContainer.Controls.Add(Me.chkEnabledWelcomeHime)
        Me.pnlHimesEnablerContainer.Controls.Add(Me.lblConfigureThanksHime)
        Me.pnlHimesEnablerContainer.Controls.Add(Me.chkEnabledSystemMessageHime)
        Me.pnlHimesEnablerContainer.Location = New System.Drawing.Point(20, 225)
        Me.pnlHimesEnablerContainer.Name = "pnlHimesEnablerContainer"
        Me.pnlHimesEnablerContainer.Size = New System.Drawing.Size(320, 152)
        Me.pnlHimesEnablerContainer.TabIndex = 152
        '
        'chkEnabledMedalUpgradeHime
        '
        Me.chkEnabledMedalUpgradeHime.AutoSize = True
        Me.chkEnabledMedalUpgradeHime.Location = New System.Drawing.Point(185, 4)
        Me.chkEnabledMedalUpgradeHime.Name = "chkEnabledMedalUpgradeHime"
        Me.chkEnabledMedalUpgradeHime.Size = New System.Drawing.Size(96, 16)
        Me.chkEnabledMedalUpgradeHime.TabIndex = 137
        Me.chkEnabledMedalUpgradeHime.Text = "勋章升级提示"
        Me.chkEnabledMedalUpgradeHime.UseVisualStyleBackColor = True
        '
        'txtViewRoomId
        '
        Me.txtViewRoomId.Enabled = False
        Me.txtViewRoomId.Location = New System.Drawing.Point(90, 67)
        Me.txtViewRoomId.Name = "txtViewRoomId"
        Me.txtViewRoomId.Size = New System.Drawing.Size(100, 21)
        Me.txtViewRoomId.TabIndex = 153
        '
        'chkDisplayOriginalNick
        '
        Me.chkDisplayOriginalNick.AutoSize = True
        Me.chkDisplayOriginalNick.Location = New System.Drawing.Point(185, 26)
        Me.chkDisplayOriginalNick.Name = "chkDisplayOriginalNick"
        Me.chkDisplayOriginalNick.Size = New System.Drawing.Size(84, 16)
        Me.chkDisplayOriginalNick.TabIndex = 138
        Me.chkDisplayOriginalNick.Text = "显示原昵称"
        Me.chkDisplayOriginalNick.UseVisualStyleBackColor = True
        '
        'SettingControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.txtViewRoomId)
        Me.Controls.Add(Me.pnlHimesEnablerContainer)
        Me.Controls.Add(Me.btnPluginManage)
        Me.Controls.Add(Me.chkFlashWindow)
        Me.Controls.Add(Me.btnCloseExe)
        Me.Controls.Add(Me.btnChangeViewRoom)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lblMainFormOpacity)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.trbFormOpacity)
        Me.Controls.Add(Me.nudBrokeSilenceInterval)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.nudHeartbeatInterval)
        Me.Controls.Add(Me.Label5)
        Me.Name = "SettingControl"
        Me.Size = New System.Drawing.Size(377, 434)
        CType(Me.nudBrokeSilenceInterval, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.trbFormOpacity, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nudHeartbeatInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlHimesEnablerContainer.ResumeLayout(False)
        Me.pnlHimesEnablerContainer.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Label1 As Label
    Friend WithEvents nudBrokeSilenceInterval As NumericUpDown
    Friend WithEvents rdbtnWsMode As RadioButton
    Friend WithEvents rdbtnWssMode As RadioButton
    Friend WithEvents rdbtnTcpMode As RadioButton
    Friend WithEvents rdbtnHttpMode As RadioButton
    Friend WithEvents Label3 As Label
    Friend WithEvents nudHeartbeatInterval As NumericUpDown
    Friend WithEvents lblConfigureThanksHime As Label
    Friend WithEvents chkEnabledThanksHime As CheckBox
    Friend WithEvents chkEnabledAttentionHime As CheckBox
    Friend WithEvents chkSignAuto As CheckBox
    Friend WithEvents chkOpenSilverBoxAuto As CheckBox
    Friend WithEvents chkReceiveDoubleWatchAwardAuto As CheckBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents chkEnabledWelcomeHime As CheckBox
    Friend WithEvents chkEnabledSystemMessageHime As CheckBox
    Friend WithEvents trbFormOpacity As TrackBar
    Friend WithEvents lblMainFormOpacity As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents rdbtnBlockRepeatitive As RadioButton
    Friend WithEvents rdbtnMergeRepeatitive As RadioButton
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents btnChangeViewRoom As Button
    Friend WithEvents btnCloseExe As Button
	Friend WithEvents chkFlashWindow As CheckBox
	Friend WithEvents btnPluginManage As Button
    Friend WithEvents pnlHimesEnablerContainer As Panel
    Friend WithEvents txtViewRoomId As TextBox
    Friend WithEvents chkEnabledMedalUpgradeHime As CheckBox
    Friend WithEvents chkDisplayOriginalNick As CheckBox
End Class

