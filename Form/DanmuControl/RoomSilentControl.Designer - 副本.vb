<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RoomSilentControl
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
		Me.Label1 = New System.Windows.Forms.Label()
		Me.lblShieldStatus = New System.Windows.Forms.Label()
		Me.chkSilent = New System.Windows.Forms.CheckBox()
		Me.lblUnShieldTime = New System.Windows.Forms.Label()
		Me.rdbtnViewerLevel = New System.Windows.Forms.RadioButton()
		Me.rdbtnFanMadel = New System.Windows.Forms.RadioButton()
		Me.rdbtnAllViewer = New System.Windows.Forms.RadioButton()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.trbShieldLevel = New System.Windows.Forms.TrackBar()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.txtShieldKeyword = New System.Windows.Forms.TextBox()
		Me.btnAddShieldKeyword = New System.Windows.Forms.Button()
		Me.Label7 = New System.Windows.Forms.Label()
		Me.lblShieldKeyword = New System.Windows.Forms.Label()
		Me.lblShieldViewer = New System.Windows.Forms.Label()
		Me.chklstShieldKeyword = New System.Windows.Forms.CheckedListBox()
		Me.chkApplyShieldKeyword = New System.Windows.Forms.CheckBox()
		Me.chkCheckAll = New System.Windows.Forms.CheckBox()
		Me.cmbShieldInterval = New System.Windows.Forms.ComboBox()
		Me.nudShieldLevel = New System.Windows.Forms.NumericUpDown()
		Me.btnDelete = New System.Windows.Forms.Button()
		Me.pnlRoomShield = New System.Windows.Forms.Panel()
		Me.btnAddShieldViewer = New System.Windows.Forms.Button()
		Me.txtShieldViewer = New System.Windows.Forms.TextBox()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.tlpShieldModeLabel = New System.Windows.Forms.TableLayoutPanel()
		Me.chklstShieldViewer = New System.Windows.Forms.CheckedListBox()
		Me.btnRegetShieldInfo = New System.Windows.Forms.Button()
		CType(Me.trbShieldLevel, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.nudShieldLevel, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnlRoomShield.SuspendLayout()
		Me.tlpShieldModeLabel.SuspendLayout()
		Me.SuspendLayout()
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
		Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
		Me.Label1.Location = New System.Drawing.Point(3, 3)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(76, 16)
		Me.Label1.TabIndex = 171
		Me.Label1.Text = "全局禁言"
		'
		'lblShieldStatus
		'
		Me.lblShieldStatus.AutoSize = True
		Me.lblShieldStatus.Location = New System.Drawing.Point(6, 29)
		Me.lblShieldStatus.Name = "lblShieldStatus"
		Me.lblShieldStatus.Size = New System.Drawing.Size(65, 12)
		Me.lblShieldStatus.TabIndex = 172
		Me.lblShieldStatus.Text = "屏蔽未开启"
		'
		'chkSilent
		'
		Me.chkSilent.AutoSize = True
		Me.chkSilent.Location = New System.Drawing.Point(89, 29)
		Me.chkSilent.Name = "chkSilent"
		Me.chkSilent.Size = New System.Drawing.Size(15, 14)
		Me.chkSilent.TabIndex = 0
		Me.chkSilent.UseVisualStyleBackColor = True
		'
		'lblUnShieldTime
		'
		Me.lblUnShieldTime.AutoSize = True
		Me.lblUnShieldTime.Location = New System.Drawing.Point(110, 30)
		Me.lblUnShieldTime.Name = "lblUnShieldTime"
		Me.lblUnShieldTime.Size = New System.Drawing.Size(59, 12)
		Me.lblUnShieldTime.TabIndex = 174
		Me.lblUnShieldTime.Text = "解除还需 "
		'
		'rdbtnViewerLevel
		'
		Me.rdbtnViewerLevel.AutoSize = True
		Me.rdbtnViewerLevel.Location = New System.Drawing.Point(6, 0)
		Me.rdbtnViewerLevel.Name = "rdbtnViewerLevel"
		Me.rdbtnViewerLevel.Size = New System.Drawing.Size(71, 16)
		Me.rdbtnViewerLevel.TabIndex = 0
		Me.rdbtnViewerLevel.TabStop = True
		Me.rdbtnViewerLevel.Text = "用户等级"
		Me.rdbtnViewerLevel.UseVisualStyleBackColor = True
		'
		'rdbtnFanMadel
		'
		Me.rdbtnFanMadel.AutoSize = True
		Me.rdbtnFanMadel.Location = New System.Drawing.Point(89, 0)
		Me.rdbtnFanMadel.Name = "rdbtnFanMadel"
		Me.rdbtnFanMadel.Size = New System.Drawing.Size(71, 16)
		Me.rdbtnFanMadel.TabIndex = 1
		Me.rdbtnFanMadel.TabStop = True
		Me.rdbtnFanMadel.Text = "粉丝勋章"
		Me.rdbtnFanMadel.UseVisualStyleBackColor = True
		'
		'rdbtnAllViewer
		'
		Me.rdbtnAllViewer.AutoSize = True
		Me.rdbtnAllViewer.Location = New System.Drawing.Point(166, 0)
		Me.rdbtnAllViewer.Name = "rdbtnAllViewer"
		Me.rdbtnAllViewer.Size = New System.Drawing.Size(47, 16)
		Me.rdbtnAllViewer.TabIndex = 2
		Me.rdbtnAllViewer.TabStop = True
		Me.rdbtnAllViewer.Text = "全员"
		Me.rdbtnAllViewer.UseVisualStyleBackColor = True
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(17, 26)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(53, 12)
		Me.Label2.TabIndex = 178
		Me.Label2.Text = "屏蔽等级"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(17, 54)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(53, 12)
		Me.Label3.TabIndex = 179
		Me.Label3.Text = "屏蔽时间"
		'
		'trbShieldLevel
		'
		Me.trbShieldLevel.Location = New System.Drawing.Point(81, 22)
		Me.trbShieldLevel.Name = "trbShieldLevel"
		Me.trbShieldLevel.Size = New System.Drawing.Size(128, 45)
		Me.trbShieldLevel.TabIndex = 3
		Me.trbShieldLevel.TickStyle = System.Windows.Forms.TickStyle.None
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(254, 26)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(41, 12)
		Me.Label4.TabIndex = 182
		Me.Label4.Text = "级以下"
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
		Me.Label6.ForeColor = System.Drawing.SystemColors.Highlight
		Me.Label6.Location = New System.Drawing.Point(3, 129)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(93, 16)
		Me.Label6.TabIndex = 185
		Me.Label6.Text = "关键词屏蔽"
		'
		'txtShieldKeyword
		'
		Me.txtShieldKeyword.Location = New System.Drawing.Point(8, 158)
		Me.txtShieldKeyword.Name = "txtShieldKeyword"
		Me.txtShieldKeyword.Size = New System.Drawing.Size(315, 21)
		Me.txtShieldKeyword.TabIndex = 1
		'
		'btnAddShieldKeyword
		'
		Me.btnAddShieldKeyword.Location = New System.Drawing.Point(329, 156)
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
		Me.Label7.Location = New System.Drawing.Point(6, 251)
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
		Me.chklstShieldKeyword.BackColor = System.Drawing.SystemColors.ScrollBar
		Me.chklstShieldKeyword.FormattingEnabled = True
		Me.chklstShieldKeyword.Location = New System.Drawing.Point(8, 293)
		Me.chklstShieldKeyword.Name = "chklstShieldKeyword"
		Me.chklstShieldKeyword.Size = New System.Drawing.Size(190, 100)
		Me.chklstShieldKeyword.TabIndex = 191
		'
		'chkApplyShieldKeyword
		'
		Me.chkApplyShieldKeyword.AutoSize = True
		Me.chkApplyShieldKeyword.Location = New System.Drawing.Point(6, 420)
		Me.chkApplyShieldKeyword.Name = "chkApplyShieldKeyword"
		Me.chkApplyShieldKeyword.Size = New System.Drawing.Size(240, 16)
		Me.chkApplyShieldKeyword.TabIndex = 6
		Me.chkApplyShieldKeyword.Text = "将此列表应用到本房间(所有用户不可见)"
		Me.chkApplyShieldKeyword.UseVisualStyleBackColor = True
		'
		'chkCheckAll
		'
		Me.chkCheckAll.AutoSize = True
		Me.chkCheckAll.Location = New System.Drawing.Point(8, 399)
		Me.chkCheckAll.Name = "chkCheckAll"
		Me.chkCheckAll.Size = New System.Drawing.Size(48, 16)
		Me.chkCheckAll.TabIndex = 7
		Me.chkCheckAll.Text = "全选"
		Me.chkCheckAll.UseVisualStyleBackColor = True
		'
		'cmbShieldInterval
		'
		Me.cmbShieldInterval.FormattingEnabled = True
		Me.cmbShieldInterval.Location = New System.Drawing.Point(88, 51)
		Me.cmbShieldInterval.Name = "cmbShieldInterval"
		Me.cmbShieldInterval.Size = New System.Drawing.Size(112, 20)
		Me.cmbShieldInterval.TabIndex = 5
		'
		'nudShieldLevel
		'
		Me.nudShieldLevel.Location = New System.Drawing.Point(215, 22)
		Me.nudShieldLevel.Name = "nudShieldLevel"
		Me.nudShieldLevel.Size = New System.Drawing.Size(35, 21)
		Me.nudShieldLevel.TabIndex = 4
		Me.nudShieldLevel.Value = New Decimal(New Integer() {1, 0, 0, 0})
		'
		'btnDelete
		'
		Me.btnDelete.Location = New System.Drawing.Point(326, 396)
		Me.btnDelete.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.btnDelete.Name = "btnDelete"
		Me.btnDelete.Size = New System.Drawing.Size(75, 23)
		Me.btnDelete.TabIndex = 8
		Me.btnDelete.Text = "删除"
		Me.btnDelete.UseVisualStyleBackColor = True
		'
		'pnlRoomShield
		'
		Me.pnlRoomShield.Controls.Add(Me.rdbtnFanMadel)
		Me.pnlRoomShield.Controls.Add(Me.rdbtnViewerLevel)
		Me.pnlRoomShield.Controls.Add(Me.nudShieldLevel)
		Me.pnlRoomShield.Controls.Add(Me.rdbtnAllViewer)
		Me.pnlRoomShield.Controls.Add(Me.cmbShieldInterval)
		Me.pnlRoomShield.Controls.Add(Me.Label2)
		Me.pnlRoomShield.Controls.Add(Me.Label3)
		Me.pnlRoomShield.Controls.Add(Me.trbShieldLevel)
		Me.pnlRoomShield.Controls.Add(Me.Label4)
		Me.pnlRoomShield.Location = New System.Drawing.Point(0, 49)
		Me.pnlRoomShield.Name = "pnlRoomShield"
		Me.pnlRoomShield.Size = New System.Drawing.Size(404, 77)
		Me.pnlRoomShield.TabIndex = 198
		'
		'btnAddShieldViewer
		'
		Me.btnAddShieldViewer.Location = New System.Drawing.Point(329, 218)
		Me.btnAddShieldViewer.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.btnAddShieldViewer.Name = "btnAddShieldViewer"
		Me.btnAddShieldViewer.Size = New System.Drawing.Size(75, 23)
		Me.btnAddShieldViewer.TabIndex = 4
		Me.btnAddShieldViewer.Text = "添加"
		Me.btnAddShieldViewer.UseVisualStyleBackColor = True
		'
		'txtShieldViewer
		'
		Me.txtShieldViewer.Location = New System.Drawing.Point(8, 220)
		Me.txtShieldViewer.Name = "txtShieldViewer"
		Me.txtShieldViewer.Size = New System.Drawing.Size(315, 21)
		Me.txtShieldViewer.TabIndex = 3
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
		Me.Label5.ForeColor = System.Drawing.SystemColors.Highlight
		Me.Label5.Location = New System.Drawing.Point(3, 191)
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
		Me.tlpShieldModeLabel.Location = New System.Drawing.Point(8, 269)
		Me.tlpShieldModeLabel.Name = "tlpShieldModeLabel"
		Me.tlpShieldModeLabel.RowCount = 1
		Me.tlpShieldModeLabel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tlpShieldModeLabel.Size = New System.Drawing.Size(395, 21)
		Me.tlpShieldModeLabel.TabIndex = 202
		'
		'chklstShieldViewer
		'
		Me.chklstShieldViewer.FormattingEnabled = True
		Me.chklstShieldViewer.Location = New System.Drawing.Point(211, 293)
		Me.chklstShieldViewer.Name = "chklstShieldViewer"
		Me.chklstShieldViewer.Size = New System.Drawing.Size(190, 100)
		Me.chklstShieldViewer.TabIndex = 203
		'
		'btnRegetShieldInfo
		'
		Me.btnRegetShieldInfo.Location = New System.Drawing.Point(65, 247)
		Me.btnRegetShieldInfo.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
		Me.btnRegetShieldInfo.Name = "btnRegetShieldInfo"
		Me.btnRegetShieldInfo.Size = New System.Drawing.Size(56, 20)
		Me.btnRegetShieldInfo.TabIndex = 5
		Me.btnRegetShieldInfo.Text = "重取(&R)"
		Me.btnRegetShieldInfo.UseVisualStyleBackColor = True
		'
		'RoomShieldControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.btnRegetShieldInfo)
		Me.Controls.Add(Me.chklstShieldViewer)
		Me.Controls.Add(Me.tlpShieldModeLabel)
		Me.Controls.Add(Me.btnAddShieldViewer)
		Me.Controls.Add(Me.txtShieldViewer)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.pnlRoomShield)
		Me.Controls.Add(Me.btnDelete)
		Me.Controls.Add(Me.chkCheckAll)
		Me.Controls.Add(Me.chkApplyShieldKeyword)
		Me.Controls.Add(Me.chklstShieldKeyword)
		Me.Controls.Add(Me.Label7)
		Me.Controls.Add(Me.btnAddShieldKeyword)
		Me.Controls.Add(Me.txtShieldKeyword)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.lblUnShieldTime)
		Me.Controls.Add(Me.chkSilent)
		Me.Controls.Add(Me.lblShieldStatus)
		Me.Controls.Add(Me.Label1)
		Me.Name = "RoomShieldControl"
		Me.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
		Me.Size = New System.Drawing.Size(407, 436)
		CType(Me.trbShieldLevel, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.nudShieldLevel, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnlRoomShield.ResumeLayout(False)
		Me.pnlRoomShield.PerformLayout()
		Me.tlpShieldModeLabel.ResumeLayout(False)
		Me.tlpShieldModeLabel.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents Label1 As Label
    Friend WithEvents lblShieldStatus As Label
    Friend WithEvents chkSilent As CheckBox
    Friend WithEvents lblUnShieldTime As Label
    Friend WithEvents rdbtnViewerLevel As RadioButton
    Friend WithEvents rdbtnFanMadel As RadioButton
    Friend WithEvents rdbtnAllViewer As RadioButton
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents trbShieldLevel As TrackBar
    Friend WithEvents Label4 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtShieldKeyword As TextBox
    Friend WithEvents btnAddShieldKeyword As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents lblShieldKeyword As Label
    Friend WithEvents lblShieldViewer As Label
    Friend WithEvents chklstShieldKeyword As CheckedListBox
    Friend WithEvents chkApplyShieldKeyword As CheckBox
    Friend WithEvents chkCheckAll As CheckBox
    Friend WithEvents cmbShieldInterval As ComboBox
    Friend WithEvents nudShieldLevel As NumericUpDown
    Friend WithEvents btnDelete As Button
    Friend WithEvents pnlRoomShield As Panel
	Friend WithEvents btnAddShieldViewer As Button
	Friend WithEvents txtShieldViewer As TextBox
	Friend WithEvents Label5 As Label
	Friend WithEvents tlpShieldModeLabel As TableLayoutPanel
	Friend WithEvents chklstShieldViewer As CheckedListBox
	Friend WithEvents btnRegetShieldInfo As Button
End Class
