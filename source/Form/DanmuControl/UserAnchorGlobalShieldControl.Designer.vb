<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserAnchorGlobalShieldControl
	Inherits System.Windows.Forms.UserControl

	'UserControl 重写释放以清理组件列表。
	<System.Diagnostics.DebuggerNonUserCode()>
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
		Me.chkShieldSwitch = New System.Windows.Forms.CheckBox()
		Me.lblShieldStatus = New System.Windows.Forms.Label()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.nudShieldLevel = New System.Windows.Forms.NumericUpDown()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.trbShieldLevel = New System.Windows.Forms.TrackBar()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.chkAssociateMember = New System.Windows.Forms.CheckBox()
		Me.chkNotVerifyMember = New System.Windows.Forms.CheckBox()
		Me.pnlShield = New System.Windows.Forms.Panel()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.chkLevel = New System.Windows.Forms.CheckBox()
		Me.Panel1 = New System.Windows.Forms.Panel()
		CType(Me.nudShieldLevel, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.trbShieldLevel, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.pnlShield.SuspendLayout()
		Me.Panel1.SuspendLayout()
		Me.SuspendLayout()
		'
		'chkShieldSwitch
		'
		Me.chkShieldSwitch.AutoSize = True
		Me.chkShieldSwitch.Location = New System.Drawing.Point(83, 26)
		Me.chkShieldSwitch.Name = "chkShieldSwitch"
		Me.chkShieldSwitch.Size = New System.Drawing.Size(15, 14)
		Me.chkShieldSwitch.TabIndex = 175
		Me.chkShieldSwitch.UseVisualStyleBackColor = True
		'
		'lblShieldStatus
		'
		Me.lblShieldStatus.AutoSize = True
		Me.lblShieldStatus.Location = New System.Drawing.Point(0, 26)
		Me.lblShieldStatus.Name = "lblShieldStatus"
		Me.lblShieldStatus.Size = New System.Drawing.Size(65, 12)
		Me.lblShieldStatus.TabIndex = 177
		Me.lblShieldStatus.Text = "屏蔽未开启"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Font = New System.Drawing.Font("宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
		Me.Label1.ForeColor = System.Drawing.SystemColors.Highlight
		Me.Label1.Location = New System.Drawing.Point(-1, 0)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(76, 16)
		Me.Label1.TabIndex = 176
		Me.Label1.Text = "全局屏蔽"
		'
		'nudShieldLevel
		'
		Me.nudShieldLevel.Location = New System.Drawing.Point(198, 3)
		Me.nudShieldLevel.Name = "nudShieldLevel"
		Me.nudShieldLevel.Size = New System.Drawing.Size(35, 21)
		Me.nudShieldLevel.TabIndex = 184
		Me.nudShieldLevel.Value = New Decimal(New Integer() {1, 0, 0, 0})
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(1, 33)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(53, 12)
		Me.Label2.TabIndex = 185
		Me.Label2.Text = "屏蔽范围"
		'
		'trbShieldLevel
		'
		Me.trbShieldLevel.Location = New System.Drawing.Point(64, 3)
		Me.trbShieldLevel.Name = "trbShieldLevel"
		Me.trbShieldLevel.Size = New System.Drawing.Size(128, 45)
		Me.trbShieldLevel.TabIndex = 183
		Me.trbShieldLevel.TickStyle = System.Windows.Forms.TickStyle.None
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(237, 7)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(41, 12)
		Me.Label4.TabIndex = 186
		Me.Label4.Text = "级以下"
		'
		'chkAssociateMember
		'
		Me.chkAssociateMember.AutoSize = True
		Me.chkAssociateMember.Location = New System.Drawing.Point(134, 32)
		Me.chkAssociateMember.Name = "chkAssociateMember"
		Me.chkAssociateMember.Size = New System.Drawing.Size(84, 16)
		Me.chkAssociateMember.TabIndex = 187
		Me.chkAssociateMember.Text = "非正式会员"
		Me.chkAssociateMember.UseVisualStyleBackColor = True
		'
		'chkNotVerifyMember
		'
		Me.chkNotVerifyMember.AutoSize = True
		Me.chkNotVerifyMember.Location = New System.Drawing.Point(232, 32)
		Me.chkNotVerifyMember.Name = "chkNotVerifyMember"
		Me.chkNotVerifyMember.Size = New System.Drawing.Size(108, 16)
		Me.chkNotVerifyMember.TabIndex = 188
		Me.chkNotVerifyMember.Text = "未绑定手机用户"
		Me.chkNotVerifyMember.UseVisualStyleBackColor = True
		'
		'pnlShield
		'
		Me.pnlShield.Controls.Add(Me.Label3)
		Me.pnlShield.Controls.Add(Me.chkLevel)
		Me.pnlShield.Controls.Add(Me.chkAssociateMember)
		Me.pnlShield.Controls.Add(Me.Label4)
		Me.pnlShield.Controls.Add(Me.chkNotVerifyMember)
		Me.pnlShield.Controls.Add(Me.Label2)
		Me.pnlShield.Controls.Add(Me.nudShieldLevel)
		Me.pnlShield.Controls.Add(Me.trbShieldLevel)
		Me.pnlShield.Location = New System.Drawing.Point(11, 46)
		Me.pnlShield.Name = "pnlShield"
		Me.pnlShield.Size = New System.Drawing.Size(387, 53)
		Me.pnlShield.TabIndex = 190
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(25, 7)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(29, 12)
		Me.Label3.TabIndex = 190
		Me.Label3.Text = "等级"
		'
		'chkLevel
		'
		Me.chkLevel.AutoSize = True
		Me.chkLevel.Location = New System.Drawing.Point(72, 32)
		Me.chkLevel.Name = "chkLevel"
		Me.chkLevel.Size = New System.Drawing.Size(48, 16)
		Me.chkLevel.TabIndex = 189
		Me.chkLevel.Text = "等级"
		Me.chkLevel.UseVisualStyleBackColor = True
		'
		'Panel1
		'
		Me.Panel1.Controls.Add(Me.Label1)
		Me.Panel1.Controls.Add(Me.pnlShield)
		Me.Panel1.Controls.Add(Me.lblShieldStatus)
		Me.Panel1.Controls.Add(Me.chkShieldSwitch)
		Me.Panel1.Location = New System.Drawing.Point(0, 0)
		Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
		Me.Panel1.Name = "Panel1"
		Me.Panel1.Size = New System.Drawing.Size(407, 101)
		Me.Panel1.TabIndex = 191
		'
		'UserAnchorGlobalShieldControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Panel1)
		Me.Name = "UserAnchorGlobalShieldControl"
		Me.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
		Me.Size = New System.Drawing.Size(407, 102)
		CType(Me.nudShieldLevel, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.trbShieldLevel, System.ComponentModel.ISupportInitialize).EndInit()
		Me.pnlShield.ResumeLayout(False)
		Me.pnlShield.PerformLayout()
		Me.Panel1.ResumeLayout(False)
		Me.Panel1.PerformLayout()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents chkShieldSwitch As CheckBox
	Friend WithEvents lblShieldStatus As Label
	Friend WithEvents Label1 As Label
	Friend WithEvents nudShieldLevel As NumericUpDown
	Friend WithEvents Label2 As Label
	Friend WithEvents trbShieldLevel As TrackBar
	Friend WithEvents Label4 As Label
	Friend WithEvents chkAssociateMember As CheckBox
	Friend WithEvents chkNotVerifyMember As CheckBox
	Friend WithEvents pnlShield As Panel
	Friend WithEvents chkLevel As CheckBox
	Friend WithEvents Label3 As Label
	Friend WithEvents Panel1 As Panel
End Class
