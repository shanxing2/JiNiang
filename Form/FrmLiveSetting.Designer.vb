<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLiveSetting
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLiveSetting))
        Me.ControlPanel = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rdbtnDisplayAllUesdTitle = New System.Windows.Forms.RadioButton()
        Me.nudDisplayUesdTitleCount = New System.Windows.Forms.NumericUpDown()
        Me.rdbtnDislayPartOfUsedTitle = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblMyRoomCenterPage = New System.Windows.Forms.Label()
        Me.lblCurrentArea = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnChangeLiveStatus = New System.Windows.Forms.Button()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnCopyRtmpCode = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnCopyRtmpAddres = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRtmpCode = New System.Windows.Forms.TextBox()
        Me.lblTitleLength = New System.Windows.Forms.Label()
        Me.txtRtmpAddress = New System.Windows.Forms.TextBox()
        Me.cmbRoomTitle = New System.Windows.Forms.ComboBox()
        Me.lblPushStreamResult = New System.Windows.Forms.Label()
        Me.btnChangeTitle = New System.Windows.Forms.Button()
        Me.lblPushStreamText = New System.Windows.Forms.Label()
        Me.btnChangeArea = New System.Windows.Forms.Button()
        Me.picCapturePushButton = New System.Windows.Forms.PictureBox()
        Me.chkAutoPushStream = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label5 = New System.Windows.Forms.Label()
        Me.btnCopyLiveRoomAddres = New System.Windows.Forms.Button()
        Me.txtLiveRoomAddress = New System.Windows.Forms.TextBox()
        Me.ControlPanel.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.nudDisplayUesdTitleCount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCapturePushButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ControlPanel
        '
        Me.ControlPanel.Controls.Add(Me.Label5)
        Me.ControlPanel.Controls.Add(Me.btnCopyLiveRoomAddres)
        Me.ControlPanel.Controls.Add(Me.txtLiveRoomAddress)
        Me.ControlPanel.Controls.Add(Me.Label8)
        Me.ControlPanel.Controls.Add(Me.Panel1)
        Me.ControlPanel.Controls.Add(Me.Label7)
        Me.ControlPanel.Controls.Add(Me.lblMyRoomCenterPage)
        Me.ControlPanel.Controls.Add(Me.lblCurrentArea)
        Me.ControlPanel.Controls.Add(Me.Label1)
        Me.ControlPanel.Controls.Add(Me.btnChangeLiveStatus)
        Me.ControlPanel.Controls.Add(Me.RichTextBox1)
        Me.ControlPanel.Controls.Add(Me.Label2)
        Me.ControlPanel.Controls.Add(Me.btnCopyRtmpCode)
        Me.ControlPanel.Controls.Add(Me.Label3)
        Me.ControlPanel.Controls.Add(Me.btnCopyRtmpAddres)
        Me.ControlPanel.Controls.Add(Me.Label4)
        Me.ControlPanel.Controls.Add(Me.txtRtmpCode)
        Me.ControlPanel.Controls.Add(Me.lblTitleLength)
        Me.ControlPanel.Controls.Add(Me.txtRtmpAddress)
        Me.ControlPanel.Controls.Add(Me.cmbRoomTitle)
        Me.ControlPanel.Controls.Add(Me.lblPushStreamResult)
        Me.ControlPanel.Controls.Add(Me.btnChangeTitle)
        Me.ControlPanel.Controls.Add(Me.lblPushStreamText)
        Me.ControlPanel.Controls.Add(Me.btnChangeArea)
        Me.ControlPanel.Controls.Add(Me.picCapturePushButton)
        Me.ControlPanel.Controls.Add(Me.chkAutoPushStream)
        Me.ControlPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ControlPanel.Location = New System.Drawing.Point(0, 0)
        Me.ControlPanel.Name = "ControlPanel"
        Me.ControlPanel.Size = New System.Drawing.Size(362, 450)
        Me.ControlPanel.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label8.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Label8.Location = New System.Drawing.Point(4, 347)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(356, 1)
        Me.Label8.TabIndex = 161
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rdbtnDisplayAllUesdTitle)
        Me.Panel1.Controls.Add(Me.nudDisplayUesdTitleCount)
        Me.Panel1.Controls.Add(Me.rdbtnDislayPartOfUsedTitle)
        Me.Panel1.Location = New System.Drawing.Point(3, 77)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(321, 27)
        Me.Panel1.TabIndex = 158
        '
        'rdbtnDisplayAllUesdTitle
        '
        Me.rdbtnDisplayAllUesdTitle.AutoSize = True
        Me.rdbtnDisplayAllUesdTitle.Location = New System.Drawing.Point(211, 6)
        Me.rdbtnDisplayAllUesdTitle.Name = "rdbtnDisplayAllUesdTitle"
        Me.rdbtnDisplayAllUesdTitle.Size = New System.Drawing.Size(71, 16)
        Me.rdbtnDisplayAllUesdTitle.TabIndex = 161
        Me.rdbtnDisplayAllUesdTitle.TabStop = True
        Me.rdbtnDisplayAllUesdTitle.Text = "显示全部"
        Me.rdbtnDisplayAllUesdTitle.UseVisualStyleBackColor = True
        '
        'nudDisplayUesdTitleCount
        '
        Me.nudDisplayUesdTitleCount.Location = New System.Drawing.Point(120, 3)
        Me.nudDisplayUesdTitleCount.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nudDisplayUesdTitleCount.Name = "nudDisplayUesdTitleCount"
        Me.nudDisplayUesdTitleCount.Size = New System.Drawing.Size(38, 21)
        Me.nudDisplayUesdTitleCount.TabIndex = 159
        Me.nudDisplayUesdTitleCount.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'rdbtnDislayPartOfUsedTitle
        '
        Me.rdbtnDislayPartOfUsedTitle.AutoSize = True
        Me.rdbtnDislayPartOfUsedTitle.Location = New System.Drawing.Point(2, 6)
        Me.rdbtnDislayPartOfUsedTitle.Name = "rdbtnDislayPartOfUsedTitle"
        Me.rdbtnDislayPartOfUsedTitle.Size = New System.Drawing.Size(203, 16)
        Me.rdbtnDislayPartOfUsedTitle.TabIndex = 160
        Me.rdbtnDislayPartOfUsedTitle.TabStop = True
        Me.rdbtnDislayPartOfUsedTitle.Text = "显示最近使用过的        个标题"
        Me.rdbtnDislayPartOfUsedTitle.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 353)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(137, 12)
        Me.Label7.TabIndex = 155
        Me.Label7.Text = "开播设置——个人中心："
        '
        'lblMyRoomCenterPage
        '
        Me.lblMyRoomCenterPage.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMyRoomCenterPage.AutoEllipsis = True
        Me.lblMyRoomCenterPage.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblMyRoomCenterPage.Location = New System.Drawing.Point(3, 377)
        Me.lblMyRoomCenterPage.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.lblMyRoomCenterPage.Name = "lblMyRoomCenterPage"
        Me.lblMyRoomCenterPage.Size = New System.Drawing.Size(347, 12)
        Me.lblMyRoomCenterPage.TabIndex = 154
        Me.lblMyRoomCenterPage.Text = "https://link.bilibili.com/p/center/index/my-room/start-live#/my-room/start-live"
        '
        'lblCurrentArea
        '
        Me.lblCurrentArea.AutoSize = True
        Me.lblCurrentArea.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblCurrentArea.Location = New System.Drawing.Point(62, 6)
        Me.lblCurrentArea.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.lblCurrentArea.Name = "lblCurrentArea"
        Me.lblCurrentArea.Size = New System.Drawing.Size(53, 12)
        Me.lblCurrentArea.TabIndex = 153
        Me.lblCurrentArea.Text = "当前分区"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 6)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 134
        Me.Label1.Text = "直播分类："
        '
        'btnChangeLiveStatus
        '
        Me.btnChangeLiveStatus.Location = New System.Drawing.Point(3, 420)
        Me.btnChangeLiveStatus.Name = "btnChangeLiveStatus"
        Me.btnChangeLiveStatus.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeLiveStatus.TabIndex = 0
        Me.btnChangeLiveStatus.Text = "开始直播"
        Me.btnChangeLiveStatus.UseVisualStyleBackColor = True
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.RichTextBox1.Location = New System.Drawing.Point(3, 263)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical
        Me.RichTextBox1.Size = New System.Drawing.Size(356, 81)
        Me.RichTextBox1.TabIndex = 152
        Me.RichTextBox1.Text = "每次重新开播，请在这里复制新的rtmp地址和直播码到你的直播软件中。" & Global.Microsoft.VisualBasic.ChrW(10) & "当推流过程中发生卡顿，可以尝试重新复制rtmp地址和直播码到你的直播软件中，如果仍然无法解决" &
    "，请联系客服→_→" & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 30)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 135
        Me.Label2.Text = "房间标题："
        '
        'btnCopyRtmpCode
        '
        Me.btnCopyRtmpCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCopyRtmpCode.Location = New System.Drawing.Point(314, 234)
        Me.btnCopyRtmpCode.Name = "btnCopyRtmpCode"
        Me.btnCopyRtmpCode.Size = New System.Drawing.Size(46, 23)
        Me.btnCopyRtmpCode.TabIndex = 149
        Me.btnCopyRtmpCode.Text = "复制"
        Me.btnCopyRtmpCode.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(3, 164)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 12)
        Me.Label3.TabIndex = 136
        Me.Label3.Text = "rtmp地址/URL："
        '
        'btnCopyRtmpAddress
        '
        Me.btnCopyRtmpAddres.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCopyRtmpAddres.Location = New System.Drawing.Point(314, 183)
        Me.btnCopyRtmpAddres.Name = "btnCopyRtmpAddress"
        Me.btnCopyRtmpAddres.Size = New System.Drawing.Size(46, 23)
        Me.btnCopyRtmpAddres.TabIndex = 148
        Me.btnCopyRtmpAddres.Text = "复制"
        Me.btnCopyRtmpAddres.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 215)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(137, 12)
        Me.Label4.TabIndex = 137
        Me.Label4.Text = "直播码/流名称/推流码："
        '
        'txtRtmpCode
        '
        Me.txtRtmpCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRtmpCode.Location = New System.Drawing.Point(3, 236)
        Me.txtRtmpCode.Name = "txtRtmpCode"
        Me.txtRtmpCode.ReadOnly = True
        Me.txtRtmpCode.Size = New System.Drawing.Size(305, 21)
        Me.txtRtmpCode.TabIndex = 147
        '
        'lblTitleLength
        '
        Me.lblTitleLength.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTitleLength.AutoSize = True
        Me.lblTitleLength.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblTitleLength.ForeColor = System.Drawing.Color.LimeGreen
        Me.lblTitleLength.Location = New System.Drawing.Point(276, 54)
        Me.lblTitleLength.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lblTitleLength.Name = "lblTitleLength"
        Me.lblTitleLength.Size = New System.Drawing.Size(35, 12)
        Me.lblTitleLength.TabIndex = 138
        Me.lblTitleLength.Text = " 0/20"
        '
        'txtRtmpAddress
        '
        Me.txtRtmpAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRtmpAddress.Location = New System.Drawing.Point(3, 185)
        Me.txtRtmpAddress.Name = "txtRtmpAddress"
        Me.txtRtmpAddress.ReadOnly = True
        Me.txtRtmpAddress.Size = New System.Drawing.Size(305, 21)
        Me.txtRtmpAddress.TabIndex = 146
        '
        'cmbRoomTitle
        '
        Me.cmbRoomTitle.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbRoomTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbRoomTitle.FormattingEnabled = True
        Me.cmbRoomTitle.Location = New System.Drawing.Point(3, 51)
        Me.cmbRoomTitle.Name = "cmbRoomTitle"
        Me.cmbRoomTitle.Size = New System.Drawing.Size(270, 20)
        Me.cmbRoomTitle.TabIndex = 139
        '
        'lblPushStreamResult
        '
        Me.lblPushStreamResult.BackColor = System.Drawing.Color.Transparent
        Me.lblPushStreamResult.Font = New System.Drawing.Font("宋体", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblPushStreamResult.ForeColor = System.Drawing.Color.Gray
        Me.lblPushStreamResult.Location = New System.Drawing.Point(270, 395)
        Me.lblPushStreamResult.Margin = New System.Windows.Forms.Padding(0)
        Me.lblPushStreamResult.Name = "lblPushStreamResult"
        Me.lblPushStreamResult.Size = New System.Drawing.Size(30, 21)
        Me.lblPushStreamResult.TabIndex = 145
        Me.lblPushStreamResult.Text = "●"
        Me.ToolTip1.SetToolTip(Me.lblPushStreamResult, "自动推流结果")
        '
        'btnChangeTitle
        '
        Me.btnChangeTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnChangeTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnChangeTitle.Location = New System.Drawing.Point(314, 49)
        Me.btnChangeTitle.Name = "btnChangeTitle"
        Me.btnChangeTitle.Size = New System.Drawing.Size(46, 23)
        Me.btnChangeTitle.TabIndex = 140
        Me.btnChangeTitle.Text = "保存"
        Me.btnChangeTitle.UseVisualStyleBackColor = True
        '
        'lblPushStreamText
        '
        Me.lblPushStreamText.AutoSize = True
        Me.lblPushStreamText.Location = New System.Drawing.Point(136, 400)
        Me.lblPushStreamText.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.lblPushStreamText.Name = "lblPushStreamText"
        Me.lblPushStreamText.Size = New System.Drawing.Size(131, 12)
        Me.lblPushStreamText.TabIndex = 144
        Me.lblPushStreamText.Text = "←_←捕捉开始推流按钮"
        Me.ToolTip1.SetToolTip(Me.lblPushStreamText, "推流按钮文本")
        '
        'btnChangeArea
        '
        Me.btnChangeArea.Location = New System.Drawing.Point(121, 1)
        Me.btnChangeArea.Name = "btnChangeArea"
        Me.btnChangeArea.Size = New System.Drawing.Size(75, 23)
        Me.btnChangeArea.TabIndex = 141
        Me.btnChangeArea.Text = "修改分区"
        Me.btnChangeArea.UseVisualStyleBackColor = True
        '
        'picCapturePushButton
        '
        Me.picCapturePushButton.Image = CType(resources.GetObject("picCapturePushButton.Image"), System.Drawing.Image)
        Me.picCapturePushButton.Location = New System.Drawing.Point(105, 394)
        Me.picCapturePushButton.Name = "picCapturePushButton"
        Me.picCapturePushButton.Size = New System.Drawing.Size(25, 25)
        Me.picCapturePushButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picCapturePushButton.TabIndex = 143
        Me.picCapturePushButton.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picCapturePushButton, "按住此图标到推流按钮处，然后释放")
        '
        'chkAutoPushStream
        '
        Me.chkAutoPushStream.AutoSize = True
        Me.chkAutoPushStream.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkAutoPushStream.Location = New System.Drawing.Point(3, 398)
        Me.chkAutoPushStream.Name = "chkAutoPushStream"
        Me.chkAutoPushStream.Size = New System.Drawing.Size(93, 16)
        Me.chkAutoPushStream.TabIndex = 142
        Me.chkAutoPushStream.Text = "自动开始推流"
        Me.chkAutoPushStream.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(3, 113)
        Me.Label5.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 12)
        Me.Label5.TabIndex = 162
        Me.Label5.Text = "直播间地址/URL："
        '
        'btnCopyLiveRoomAddress
        '
        Me.btnCopyLiveRoomAddres.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCopyLiveRoomAddres.Location = New System.Drawing.Point(314, 132)
        Me.btnCopyLiveRoomAddres.Name = "btnCopyLiveRoomAddress"
        Me.btnCopyLiveRoomAddres.Size = New System.Drawing.Size(46, 23)
        Me.btnCopyLiveRoomAddres.TabIndex = 164
        Me.btnCopyLiveRoomAddres.Text = "复制"
        Me.btnCopyLiveRoomAddres.UseVisualStyleBackColor = True
        '
        'txtLiveRoomAddress
        '
        Me.txtLiveRoomAddress.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLiveRoomAddress.Location = New System.Drawing.Point(3, 134)
        Me.txtLiveRoomAddress.Name = "txtLiveRoomAddress"
        Me.txtLiveRoomAddress.ReadOnly = True
        Me.txtLiveRoomAddress.Size = New System.Drawing.Size(305, 21)
        Me.txtLiveRoomAddress.TabIndex = 163
        '
        'FrmLiveSetting
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(362, 450)
        Me.Controls.Add(Me.ControlPanel)
        Me.DoubleBuffered = True
        Me.Name = "FrmLiveSetting"
        Me.Text = "FrmLiveSetting"
        Me.ControlPanel.ResumeLayout(False)
        Me.ControlPanel.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.nudDisplayUesdTitleCount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCapturePushButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ControlPanel As Panel
    Friend WithEvents lblCurrentArea As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents btnChangeLiveStatus As Button
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnCopyRtmpCode As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents btnCopyRtmpAddres As Button
    Friend WithEvents Label4 As Label
    Friend WithEvents txtRtmpCode As TextBox
    Friend WithEvents lblTitleLength As Label
    Friend WithEvents txtRtmpAddress As TextBox
    Friend WithEvents cmbRoomTitle As ComboBox
    Friend WithEvents lblPushStreamResult As Label
    Friend WithEvents btnChangeTitle As Button
    Friend WithEvents lblPushStreamText As Label
    Friend WithEvents btnChangeArea As Button
    Friend WithEvents picCapturePushButton As PictureBox
    Friend WithEvents chkAutoPushStream As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents Label7 As Label
    Friend WithEvents lblMyRoomCenterPage As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents nudDisplayUesdTitleCount As NumericUpDown
    Friend WithEvents rdbtnDisplayAllUesdTitle As RadioButton
    Friend WithEvents rdbtnDislayPartOfUsedTitle As RadioButton
    Friend WithEvents Label8 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents btnCopyLiveRoomAddres As Button
    Friend WithEvents txtLiveRoomAddress As TextBox
End Class
