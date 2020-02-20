<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RoomShieldControl
    'Inherits System.Windows.Forms.UserControl

    ''UserControl 重写释放以清理组件列表。
    '<System.Diagnostics.DebuggerNonUserCode()>
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
		Me.Label6 = New System.Windows.Forms.Label()
		Me.txtShieldKeyword = New System.Windows.Forms.TextBox()
		Me.btnAddShieldKeyword = New System.Windows.Forms.Button()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.lblShieldKeyword = New System.Windows.Forms.Label()
		Me.lblShieldViewer = New System.Windows.Forms.Label()
		Me.chklstShieldKeyword = New System.Windows.Forms.CheckedListBox()
		Me.chkApplyShieldInfo = New System.Windows.Forms.CheckBox()
		Me.chkCheckAll = New System.Windows.Forms.CheckBox()
		Me.btnDelete = New System.Windows.Forms.Button()
		Me.btnAddShieldViewer = New System.Windows.Forms.Button()
		Me.txtShieldViewer = New System.Windows.Forms.TextBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.tlpShieldModeLabel = New System.Windows.Forms.TableLayoutPanel()
		Me.chklstShieldViewer = New System.Windows.Forms.CheckedListBox()
		Me.btnRegetShieldInfo = New System.Windows.Forms.Button()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.Label9 = New System.Windows.Forms.Label()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.chkLevel = New System.Windows.Forms.CheckBox()
		Me.chkAssociateMember = New System.Windows.Forms.CheckBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.chkNotVerifyMember = New System.Windows.Forms.CheckBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.nudShieldLevel = New System.Windows.Forms.NumericUpDown()
		Me.trbShieldLevel = New System.Windows.Forms.TrackBar()
		Me.chkShieldSwitch = New System.Windows.Forms.CheckBox()
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.tlpShieldModeLabel.SuspendLayout()
		CType(Me.nudShieldLevel, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.trbShieldLevel, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
		Me.Label6.ForeColor = System.Drawing.SystemColors.Highlight
		Me.Label6.Location = New System.Drawing.Point(-3, 102)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(93, 16)
		Me.Label6.TabIndex = 185
		Me.Label6.Text = "关键词屏蔽"
		'
		'txtShieldKeyword
		'
		Me.txtShieldKeyword.Location = New System.Drawing.Point(2, 131)
		Me.txtShieldKeyword.Name = "txtShieldKeyword"
		Me.txtShieldKeyword.Size = New System.Drawing.Size(315, 21)
		Me.txtShieldKeyword.TabIndex = 1
		'
		'btnAddShieldKeyword
		'
		Me.btnAddShieldKeyword.Location = New System.Drawing.Point(323, 129)
		Me.btnAddShieldKeyword.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.btnAddShieldKeyword.Name = "btnAddShieldKeyword"
		Me.btnAddShieldKeyword.Size = New System.Drawing.Size(75, 23)
		Me.btnAddShieldKeyword.TabIndex = 2
		Me.btnAddShieldKeyword.Text = "添加"
		Me.btnAddShieldKeyword.UseVisualStyleBackColor = True
		'
		'Label7
		'
		Me.Label7.AutoSize = True
		Me.Label7.Location = New System.Drawing.Point(0, 224)
		Me.Label7.Name = "Label7"
		Me.Label7.Size = New System.Drawing.Size(53, 12)
		Me.Label7.TabIndex = 188
		Me.Label7.Text = "屏蔽列表"
		'
		'lblShieldKeyword
		'
		Me.lblShieldKeyword.AutoSize = True
		Me.lblShieldKeyword.Location = New System.Drawing.Point(0, 0)
		Me.lblShieldKeyword.Margin = New System.Windows.Forms.Padding(0)
		Me.lblShieldKeyword.Name = "lblShieldKeyword"
		Me.lblShieldKeyword.Size = New System.Drawing.Size(65, 12)
		Me.lblShieldKeyword.TabIndex = 0
		Me.lblShieldKeyword.Text = "屏蔽关键词"
		'
		'lblShieldViewer
		'
		Me.lblShieldViewer.AutoSize = True
		Me.lblShieldViewer.Location = New System.Drawing.Point(200, 0)
		Me.lblShieldViewer.Name = "lblShieldViewer"
		Me.lblShieldViewer.Size = New System.Drawing.Size(53, 12)
		Me.lblShieldViewer.TabIndex = 1
		Me.lblShieldViewer.Text = "屏蔽用户"
		'
		'chklstShieldKeyword
		'
		Me.chklstShieldKeyword.BackColor = System.Drawing.SystemColors.Window
		Me.chklstShieldKeyword.FormattingEnabled = True
		Me.chklstShieldKeyword.Location = New System.Drawing.Point(2, 266)
		Me.chklstShieldKeyword.Name = "chklstShieldKeyword"
		Me.chklstShieldKeyword.Size = New System.Drawing.Size(190, 100)
		Me.chklstShieldKeyword.TabIndex = 191
		'
		'chkApplyShieldInfo
		'
		Me.chkApplyShieldInfo.AutoSize = True
		Me.chkApplyShieldInfo.Location = New System.Drawing.Point(2, 393)
		Me.chkApplyShieldInfo.Name = "chkApplyShieldInfo"
		Me.chkApplyShieldInfo.Size = New System.Drawing.Size(240, 16)
		Me.chkApplyShieldInfo.TabIndex = 6
		Me.chkApplyShieldInfo.Text = "将两列表应用到本房间(所有用户不可见)"
		Me.chkApplyShieldInfo.UseVisualStyleBackColor = True
		'
		'chkCheckAll
		'
		Me.chkCheckAll.AutoSize = True
		Me.chkCheckAll.Location = New System.Drawing.Point(2, 372)
		Me.chkCheckAll.Name = "chkCheckAll"
		Me.chkCheckAll.Size = New System.Drawing.Size(48, 16)
		Me.chkCheckAll.TabIndex = 7
		Me.chkCheckAll.Text = "全选"
		Me.chkCheckAll.UseVisualStyleBackColor = True
		'
		'btnDelete
		'
		Me.btnDelete.Location = New System.Drawing.Point(56, 369)
		Me.btnDelete.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.btnDelete.Name = "btnDelete"
		Me.btnDelete.Size = New System.Drawing.Size(75, 23)
		Me.btnDelete.TabIndex = 8
		Me.btnDelete.Text = "删除"
		Me.btnDelete.UseVisualStyleBackColor = True
		'
		'btnAddShieldViewer
		'
		Me.btnAddShieldViewer.Location = New System.Drawing.Point(323, 191)
		Me.btnAddShieldViewer.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.btnAddShieldViewer.Name = "btnAddShieldViewer"
		Me.btnAddShieldViewer.Size = New System.Drawing.Size(75, 23)
		Me.btnAddShieldViewer.TabIndex = 4
		Me.btnAddShieldViewer.Text = "添加"
		Me.btnAddShieldViewer.UseVisualStyleBackColor = True
		'
		'txtShieldViewer
		'
		Me.txtShieldViewer.Location = New System.Drawing.Point(2, 193)
		Me.txtShieldViewer.Name = "txtShieldViewer"
		Me.txtShieldViewer.Size = New System.Drawing.Size(315, 21)
		Me.txtShieldViewer.TabIndex = 3
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
		Me.Label5.ForeColor = System.Drawing.SystemColors.Highlight
		Me.Label5.Location = New System.Drawing.Point(-3, 164)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(76, 16)
		Me.Label5.TabIndex = 199
		Me.Label5.Text = "用户屏蔽"
		'
		'tlpShieldModeLabel
		'
		Me.tlpShieldModeLabel.BackColor = System.Drawing.SystemColors.Control
		Me.tlpShieldModeLabel.ColumnCount = 2
		Me.tlpShieldModeLabel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tlpShieldModeLabel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tlpShieldModeLabel.Controls.Add(Me.lblShieldKeyword, 0, 0)
		Me.tlpShieldModeLabel.Controls.Add(Me.lblShieldViewer, 1, 0)
		Me.tlpShieldModeLabel.Location = New System.Drawing.Point(2, 242)
		Me.tlpShieldModeLabel.Name = "tlpShieldModeLabel"
		Me.tlpShieldModeLabel.RowCount = 1
		Me.tlpShieldModeLabel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tlpShieldModeLabel.Size = New System.Drawing.Size(395, 21)
		Me.tlpShieldModeLabel.TabIndex = 202
		'
		'chklstShieldViewer
		'
		Me.chklstShieldViewer.FormattingEnabled = True
		Me.chklstShieldViewer.Location = New System.Drawing.Point(209, 266)
		Me.chklstShieldViewer.Name = "chklstShieldViewer"
		Me.chklstShieldViewer.Size = New System.Drawing.Size(190, 100)
		Me.chklstShieldViewer.TabIndex = 203
		'
		'btnRegetShieldInfo
		'
		Me.btnRegetShieldInfo.Location = New System.Drawing.Point(59, 220)
		Me.btnRegetShieldInfo.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.btnRegetShieldInfo.Name = "btnRegetShieldInfo"
		Me.btnRegetShieldInfo.Size = New System.Drawing.Size(56, 20)
		Me.btnRegetShieldInfo.TabIndex = 5
		Me.btnRegetShieldInfo.Text = "重取(&R)"
		Me.btnRegetShieldInfo.UseVisualStyleBackColor = True
		'
		'Label10
		'
		Me.Label10.AutoSize = True
		Me.Label10.Location = New System.Drawing.Point(73, 168)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(113, 12)
		Me.Label10.TabIndex = 206
		Me.Label10.Text = "(作用于所有直播间)"
		'
		'Label9
		'
		Me.Label9.AutoSize = True
		Me.Label9.Location = New System.Drawing.Point(90, 106)
		Me.Label9.Name = "Label9"
		Me.Label9.Size = New System.Drawing.Size(113, 12)
		Me.Label9.TabIndex = 205
		Me.Label9.Text = "(作用于所有直播间)"
		'
		'Label8
		'
		Me.Label8.AutoSize = True
		Me.Label8.Location = New System.Drawing.Point(71, 4)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(113, 12)
		Me.Label8.TabIndex = 192
		Me.Label8.Text = "(作用于所有直播间)"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
		Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
		Me.Label1.Location = New System.Drawing.Point(-3, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(76, 16)
		Me.Label1.TabIndex = 176
		Me.Label1.Text = "全局屏蔽"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(34, 49)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(29, 12)
		Me.Label3.TabIndex = 190
		Me.Label3.Text = "等级"
		'
		'chkLevel
		'
		Me.chkLevel.AutoSize = True
		Me.chkLevel.Location = New System.Drawing.Point(81, 74)
		Me.chkLevel.Name = "chkLevel"
		Me.chkLevel.Size = New System.Drawing.Size(48, 16)
		Me.chkLevel.TabIndex = 189
		Me.chkLevel.Text = "等级"
		Me.chkLevel.UseVisualStyleBackColor = True
		'
		'chkAssociateMember
		'
		Me.chkAssociateMember.AutoSize = True
		Me.chkAssociateMember.Location = New System.Drawing.Point(143, 74)
		Me.chkAssociateMember.Name = "chkAssociateMember"
		Me.chkAssociateMember.Size = New System.Drawing.Size(84, 16)
		Me.chkAssociateMember.TabIndex = 187
		Me.chkAssociateMember.Text = "非正式会员"
		Me.chkAssociateMember.UseVisualStyleBackColor = True
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(246, 49)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(41, 12)
		Me.Label4.TabIndex = 186
		Me.Label4.Text = "级以下"
		'
		'chkNotVerifyMember
		'
		Me.chkNotVerifyMember.AutoSize = True
		Me.chkNotVerifyMember.Location = New System.Drawing.Point(241, 74)
		Me.chkNotVerifyMember.Name = "chkNotVerifyMember"
		Me.chkNotVerifyMember.Size = New System.Drawing.Size(108, 16)
		Me.chkNotVerifyMember.TabIndex = 188
		Me.chkNotVerifyMember.Text = "未绑定手机用户"
		Me.chkNotVerifyMember.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(10, 75)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(53, 12)
		Me.Label2.TabIndex = 185
		Me.Label2.Text = "屏蔽范围"
		'
		'nudShieldLevel
		'
		Me.nudShieldLevel.Location = New System.Drawing.Point(207, 45)
		Me.nudShieldLevel.Name = "nudShieldLevel"
		Me.nudShieldLevel.Size = New System.Drawing.Size(35, 21)
		Me.nudShieldLevel.TabIndex = 184
		Me.ToolTip1.SetToolTip(Me.nudShieldLevel, "按 Enter 键提交修改")
		'
		'trbShieldLevel
		'
		Me.trbShieldLevel.Location = New System.Drawing.Point(73, 45)
		Me.trbShieldLevel.Name = "trbShieldLevel"
		Me.trbShieldLevel.Size = New System.Drawing.Size(128, 45)
		Me.trbShieldLevel.TabIndex = 183
		Me.trbShieldLevel.TickStyle = System.Windows.Forms.TickStyle.None
		'
		'chkShieldSwitch
		'
		Me.chkShieldSwitch.AutoSize = True
		Me.chkShieldSwitch.Location = New System.Drawing.Point(81, 26)
		Me.chkShieldSwitch.Name = "chkShieldSwitch"
		Me.chkShieldSwitch.Size = New System.Drawing.Size(84, 16)
		Me.chkShieldSwitch.TabIndex = 175
		Me.chkShieldSwitch.Text = "屏蔽未开启"
		Me.chkShieldSwitch.UseVisualStyleBackColor = True
		'
		'RoomShieldControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.chkLevel)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.chkAssociateMember)
		Me.Controls.Add(Me.Label10)
		Me.Controls.Add(Me.chkNotVerifyMember)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label8)
		Me.Controls.Add(Me.Label9)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.nudShieldLevel)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.trbShieldLevel)
		Me.Controls.Add(Me.txtShieldKeyword)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.chklstShieldViewer)
		Me.Controls.Add(Me.btnAddShieldKeyword)
		Me.Controls.Add(Me.chkShieldSwitch)
		Me.Controls.Add(Me.btnDelete)
		Me.Controls.Add(Me.btnAddShieldViewer)
		Me.Controls.Add(Me.chkCheckAll)
		Me.Controls.Add(Me.chklstShieldKeyword)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.txtShieldViewer)
		Me.Controls.Add(Me.chkApplyShieldInfo)
		Me.Controls.Add(Me.tlpShieldModeLabel)
		Me.Controls.Add(Me.btnRegetShieldInfo)
		Me.Controls.Add(Me.Label7)
		Me.Margin = New System.Windows.Forms.Padding(0)
		Me.Name = "RoomShieldControl"
		Me.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
		Me.Size = New System.Drawing.Size(398, 409)
		Me.tlpShieldModeLabel.ResumeLayout(False)
		Me.tlpShieldModeLabel.PerformLayout()
		CType(Me.nudShieldLevel, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.trbShieldLevel, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents Label6 As Label
	Friend WithEvents txtShieldKeyword As TextBox
	Friend WithEvents btnAddShieldKeyword As Button
	Friend WithEvents Label7 As Label
	Friend WithEvents lblShieldKeyword As Label
	Friend WithEvents lblShieldViewer As Label
	Friend WithEvents chklstShieldKeyword As CheckedListBox
	Friend WithEvents chkApplyShieldInfo As CheckBox
	Friend WithEvents chkCheckAll As CheckBox
	Friend WithEvents btnDelete As Button
	Friend WithEvents btnAddShieldViewer As Button
	Friend WithEvents txtShieldViewer As TextBox
	Friend WithEvents Label5 As Label
	Friend WithEvents tlpShieldModeLabel As TableLayoutPanel
	Friend WithEvents chklstShieldViewer As CheckedListBox
	Friend WithEvents btnRegetShieldInfo As Button
	Friend WithEvents Label1 As Label
	Friend WithEvents Label3 As Label
	Friend WithEvents chkLevel As CheckBox
	Friend WithEvents chkAssociateMember As CheckBox
	Friend WithEvents Label4 As Label
	Friend WithEvents chkNotVerifyMember As CheckBox
	Friend WithEvents Label2 As Label
	Friend WithEvents nudShieldLevel As NumericUpDown
	Friend WithEvents trbShieldLevel As TrackBar
	Friend WithEvents chkShieldSwitch As CheckBox
	Friend WithEvents Label8 As Label
	Friend WithEvents Label10 As Label
	Friend WithEvents Label9 As Label
	Friend WithEvents ToolTip1 As ToolTip
End Class
