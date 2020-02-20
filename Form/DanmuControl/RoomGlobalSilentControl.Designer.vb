<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RoomGlobalSilentControl
	'Inherits System.Windows.Forms.UserControl

	''UserControl 重写释放以清理组件列表。
	'<System.Diagnostics.DebuggerNonUserCode()> _
	'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
	'	Try
	'		If disposing AndAlso components IsNot Nothing Then
	'			components.Dispose()
	'		End If
	'	Finally
	'		MyBase.Dispose(disposing)
	'	End Try
	'End Sub

	'Windows 窗体设计器所必需的
	Private components As System.ComponentModel.IContainer

	'注意: 以下过程是 Windows 窗体设计器所必需的
	'可以使用 Windows 窗体设计器修改它。  
	'不要使用代码编辑器修改它。
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label5 = New System.Windows.Forms.Label()
		Me.lblShieldStatus = New System.Windows.Forms.Label()
		Me.pnlRoomShield = New System.Windows.Forms.Panel()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.rdbtnFanMadel = New System.Windows.Forms.RadioButton()
		Me.rdbtnViewerLevel = New System.Windows.Forms.RadioButton()
		Me.nudShieldLevel = New System.Windows.Forms.NumericUpDown()
		Me.rdbtnAllViewer = New System.Windows.Forms.RadioButton()
		Me.cmbShieldInterval = New System.Windows.Forms.ComboBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.trbShieldLevel = New System.Windows.Forms.TrackBar()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.chkSilent = New System.Windows.Forms.CheckBox()
		Me.lblUnShieldTime = New System.Windows.Forms.Label()
		Me.pnlRoomShield.SuspendLayout()
		CType(Me.nudShieldLevel, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.trbShieldLevel, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
		Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
		Me.Label1.Location = New System.Drawing.Point(-3, 0)
		Me.Label1.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(76, 16)
		Me.Label1.TabIndex = 200
		Me.Label1.Text = "全局禁言"
		'
		'Label5
		'
		Me.Label5.AutoSize = True
		Me.Label5.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
		Me.Label5.ForeColor = System.Drawing.SystemColors.Highlight
		Me.Label5.Location = New System.Drawing.Point(-3, 123)
		Me.Label5.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
		Me.Label5.Name = "Label5"
		Me.Label5.Size = New System.Drawing.Size(76, 16)
		Me.Label5.TabIndex = 204
		Me.Label5.Text = "用户禁言"
		'
		'lblShieldStatus
		'
		Me.lblShieldStatus.AutoSize = True
		Me.lblShieldStatus.Location = New System.Drawing.Point(0, 26)
		Me.lblShieldStatus.Name = "lblShieldStatus"
		Me.lblShieldStatus.Size = New System.Drawing.Size(65, 12)
		Me.lblShieldStatus.TabIndex = 201
		Me.lblShieldStatus.Text = "禁言未开启"
		'
		'pnlRoomShield
		'
		Me.pnlRoomShield.Controls.Add(Me.Label6)
		Me.pnlRoomShield.Controls.Add(Me.rdbtnFanMadel)
		Me.pnlRoomShield.Controls.Add(Me.rdbtnViewerLevel)
		Me.pnlRoomShield.Controls.Add(Me.nudShieldLevel)
		Me.pnlRoomShield.Controls.Add(Me.rdbtnAllViewer)
		Me.pnlRoomShield.Controls.Add(Me.cmbShieldInterval)
		Me.pnlRoomShield.Controls.Add(Me.Label2)
		Me.pnlRoomShield.Controls.Add(Me.Label3)
		Me.pnlRoomShield.Controls.Add(Me.trbShieldLevel)
		Me.pnlRoomShield.Controls.Add(Me.Label4)
		Me.pnlRoomShield.Location = New System.Drawing.Point(0, 46)
		Me.pnlRoomShield.Name = "pnlRoomShield"
		Me.pnlRoomShield.Size = New System.Drawing.Size(398, 74)
		Me.pnlRoomShield.TabIndex = 203
		'
		'Label6
		'
		Me.Label6.AutoSize = True
		Me.Label6.Location = New System.Drawing.Point(11, 2)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(53, 12)
		Me.Label6.TabIndex = 183
		Me.Label6.Text = "禁言类型"
		'
		'rdbtnFanMadel
		'
		Me.rdbtnFanMadel.AutoSize = True
		Me.rdbtnFanMadel.Location = New System.Drawing.Point(166, 0)
		Me.rdbtnFanMadel.Name = "rdbtnFanMadel"
		Me.rdbtnFanMadel.Size = New System.Drawing.Size(71, 16)
		Me.rdbtnFanMadel.TabIndex = 1
		Me.rdbtnFanMadel.TabStop = True
		Me.rdbtnFanMadel.Text = "粉丝勋章"
		Me.rdbtnFanMadel.UseVisualStyleBackColor = True
		'
		'rdbtnViewerLevel
		'
		Me.rdbtnViewerLevel.AutoSize = True
		Me.rdbtnViewerLevel.Location = New System.Drawing.Point(83, 0)
		Me.rdbtnViewerLevel.Name = "rdbtnViewerLevel"
		Me.rdbtnViewerLevel.Size = New System.Drawing.Size(71, 16)
		Me.rdbtnViewerLevel.TabIndex = 0
		Me.rdbtnViewerLevel.TabStop = True
		Me.rdbtnViewerLevel.Text = "用户等级"
		Me.rdbtnViewerLevel.UseVisualStyleBackColor = True
		'
		'nudShieldLevel
		'
		Me.nudShieldLevel.Location = New System.Drawing.Point(209, 22)
		Me.nudShieldLevel.Name = "nudShieldLevel"
		Me.nudShieldLevel.Size = New System.Drawing.Size(35, 21)
		Me.nudShieldLevel.TabIndex = 4
		Me.nudShieldLevel.Value = New Decimal(New Integer() {1, 0, 0, 0})
		'
		'rdbtnAllViewer
		'
		Me.rdbtnAllViewer.AutoSize = True
		Me.rdbtnAllViewer.Location = New System.Drawing.Point(243, 0)
		Me.rdbtnAllViewer.Name = "rdbtnAllViewer"
		Me.rdbtnAllViewer.Size = New System.Drawing.Size(47, 16)
		Me.rdbtnAllViewer.TabIndex = 2
		Me.rdbtnAllViewer.TabStop = True
		Me.rdbtnAllViewer.Text = "全员"
		Me.rdbtnAllViewer.UseVisualStyleBackColor = True
		'
		'cmbShieldInterval
		'
		Me.cmbShieldInterval.FormattingEnabled = True
		Me.cmbShieldInterval.Location = New System.Drawing.Point(82, 51)
		Me.cmbShieldInterval.Name = "cmbShieldInterval"
		Me.cmbShieldInterval.Size = New System.Drawing.Size(112, 20)
		Me.cmbShieldInterval.TabIndex = 5
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(11, 26)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(53, 12)
		Me.Label2.TabIndex = 178
		Me.Label2.Text = "禁言等级"
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(11, 54)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(53, 12)
		Me.Label3.TabIndex = 179
		Me.Label3.Text = "禁言时间"
		'
		'trbShieldLevel
		'
		Me.trbShieldLevel.Location = New System.Drawing.Point(75, 22)
		Me.trbShieldLevel.Name = "trbShieldLevel"
		Me.trbShieldLevel.Size = New System.Drawing.Size(128, 45)
		Me.trbShieldLevel.TabIndex = 3
		Me.trbShieldLevel.TickStyle = System.Windows.Forms.TickStyle.None
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(248, 26)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(41, 12)
		Me.Label4.TabIndex = 182
		Me.Label4.Text = "级以下"
		'
		'chkSilent
		'
		Me.chkSilent.AutoSize = True
		Me.chkSilent.Location = New System.Drawing.Point(83, 26)
		Me.chkSilent.Name = "chkSilent"
		Me.chkSilent.Size = New System.Drawing.Size(15, 14)
		Me.chkSilent.TabIndex = 199
		Me.chkSilent.UseVisualStyleBackColor = True
		'
		'lblUnShieldTime
		'
		Me.lblUnShieldTime.AutoSize = True
		Me.lblUnShieldTime.Location = New System.Drawing.Point(104, 27)
		Me.lblUnShieldTime.Name = "lblUnShieldTime"
		Me.lblUnShieldTime.Size = New System.Drawing.Size(59, 12)
		Me.lblUnShieldTime.TabIndex = 202
		Me.lblUnShieldTime.Text = "解除还需 "
		'
		'RoomSilentControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Label5)
		Me.Controls.Add(Me.lblShieldStatus)
		Me.Controls.Add(Me.lblUnShieldTime)
		Me.Controls.Add(Me.pnlRoomShield)
		Me.Controls.Add(Me.chkSilent)
		Me.Margin = New System.Windows.Forms.Padding(0)
		Me.Name = "RoomSilentControl"
		Me.Size = New System.Drawing.Size(407, 150)
		Me.pnlRoomShield.ResumeLayout(False)
		Me.pnlRoomShield.PerformLayout()
		CType(Me.nudShieldLevel, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.trbShieldLevel, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents Label1 As Label
	Friend WithEvents Label5 As Label
	Friend WithEvents lblShieldStatus As Label
	Friend WithEvents pnlRoomShield As Panel
	Friend WithEvents Label6 As Label
	Friend WithEvents rdbtnFanMadel As RadioButton
	Friend WithEvents rdbtnViewerLevel As RadioButton
	Friend WithEvents nudShieldLevel As NumericUpDown
	Friend WithEvents rdbtnAllViewer As RadioButton
	Friend WithEvents cmbShieldInterval As ComboBox
	Friend WithEvents Label2 As Label
	Friend WithEvents Label3 As Label
	Friend WithEvents trbShieldLevel As TrackBar
	Friend WithEvents Label4 As Label
	Friend WithEvents chkSilent As CheckBox
	Friend WithEvents lblUnShieldTime As Label
End Class
