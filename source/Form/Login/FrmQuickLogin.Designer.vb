<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmQuickLogin
    Inherits System.Windows.Forms.Form

    ''Form 重写 Dispose，以清理组件列表。
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmQuickLogin))
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbUsersNick = New System.Windows.Forms.ComboBox()
        Me.cmbViewedRooms = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkStoreCookies = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlLoginOptions = New System.Windows.Forms.Panel()
        Me.rdbtnLoginUseQRCode = New System.Windows.Forms.RadioButton()
        Me.btnTryDeleteCookies = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.rdbtnLoginUseCookies = New System.Windows.Forms.RadioButton()
        Me.rdbtnLoginWithBrowser = New System.Windows.Forms.RadioButton()
        Me.pnlLoginOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(64, 208)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(133, 23)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "居然要登录(ΩДΩ)~"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(203, 208)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(131, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "好哒(๑‾ ꇴ ‾๑)~"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "需要登录的账号"
        '
        'cmbUsersNick
        '
        Me.cmbUsersNick.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUsersNick.FormattingEnabled = True
        Me.cmbUsersNick.Location = New System.Drawing.Point(12, 24)
        Me.cmbUsersNick.Name = "cmbUsersNick"
        Me.cmbUsersNick.Size = New System.Drawing.Size(322, 20)
        Me.cmbUsersNick.TabIndex = 2
        '
        'cmbViewedRooms
        '
        Me.cmbViewedRooms.FormattingEnabled = True
        Me.cmbViewedRooms.Location = New System.Drawing.Point(10, 62)
        Me.cmbViewedRooms.Name = "cmbViewedRooms"
        Me.cmbViewedRooms.Size = New System.Drawing.Size(322, 20)
        Me.cmbViewedRooms.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(113, 12)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "需要进入的直播间Id"
        '
        'chkStoreCookies
        '
        Me.chkStoreCookies.AutoSize = True
        Me.chkStoreCookies.Location = New System.Drawing.Point(0, 37)
        Me.chkStoreCookies.Margin = New System.Windows.Forms.Padding(3, 3, 0, 3)
        Me.chkStoreCookies.Name = "chkStoreCookies"
        Me.chkStoreCookies.Size = New System.Drawing.Size(96, 16)
        Me.chkStoreCookies.TabIndex = 3
        Me.chkStoreCookies.Text = "记住登录状态"
        Me.chkStoreCookies.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Red
        Me.Label5.Location = New System.Drawing.Point(90, 38)
        Me.Label5.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(200, 12)
        Me.Label5.TabIndex = 123
        Me.Label5.Text = "（切勿在网吧等公共场合勾选！）"
        '
        'pnlLoginOptions
        '
        Me.pnlLoginOptions.Controls.Add(Me.rdbtnLoginUseQRCode)
        Me.pnlLoginOptions.Controls.Add(Me.btnTryDeleteCookies)
        Me.pnlLoginOptions.Controls.Add(Me.Label6)
        Me.pnlLoginOptions.Controls.Add(Me.rdbtnLoginUseCookies)
        Me.pnlLoginOptions.Controls.Add(Me.chkStoreCookies)
        Me.pnlLoginOptions.Controls.Add(Me.rdbtnLoginWithBrowser)
        Me.pnlLoginOptions.Controls.Add(Me.Label5)
        Me.pnlLoginOptions.Location = New System.Drawing.Point(10, 88)
        Me.pnlLoginOptions.Name = "pnlLoginOptions"
        Me.pnlLoginOptions.Size = New System.Drawing.Size(322, 89)
        Me.pnlLoginOptions.TabIndex = 124
        '
        'rdbtnLoginUseQRCode
        '
        Me.rdbtnLoginUseQRCode.AutoSize = True
        Me.rdbtnLoginUseQRCode.Location = New System.Drawing.Point(208, 15)
        Me.rdbtnLoginUseQRCode.Name = "rdbtnLoginUseQRCode"
        Me.rdbtnLoginUseQRCode.Size = New System.Drawing.Size(71, 16)
        Me.rdbtnLoginUseQRCode.TabIndex = 2
        Me.rdbtnLoginUseQRCode.Text = "扫码登录"
        Me.rdbtnLoginUseQRCode.UseVisualStyleBackColor = True
        '
        'btnTryDeleteCookies
        '
        Me.btnTryDeleteCookies.Location = New System.Drawing.Point(0, 59)
        Me.btnTryDeleteCookies.Name = "btnTryDeleteCookies"
        Me.btnTryDeleteCookies.Size = New System.Drawing.Size(96, 23)
        Me.btnTryDeleteCookies.TabIndex = 4
        Me.btnTryDeleteCookies.Text = "删除旧Cookies"
        Me.btnTryDeleteCookies.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(0, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 12)
        Me.Label6.TabIndex = 132
        Me.Label6.Text = "选择登录方式"
        '
        'rdbtnLoginUseCookies
        '
        Me.rdbtnLoginUseCookies.AutoSize = True
        Me.rdbtnLoginUseCookies.Location = New System.Drawing.Point(65, 15)
        Me.rdbtnLoginUseCookies.Name = "rdbtnLoginUseCookies"
        Me.rdbtnLoginUseCookies.Size = New System.Drawing.Size(137, 16)
        Me.rdbtnLoginUseCookies.TabIndex = 1
        Me.rdbtnLoginUseCookies.Text = "使用登录后的Cookies"
        Me.rdbtnLoginUseCookies.UseVisualStyleBackColor = True
        '
        'rdbtnLoginWithBrowser
        '
        Me.rdbtnLoginWithBrowser.AutoSize = True
        Me.rdbtnLoginWithBrowser.Location = New System.Drawing.Point(0, 15)
        Me.rdbtnLoginWithBrowser.Name = "rdbtnLoginWithBrowser"
        Me.rdbtnLoginWithBrowser.Size = New System.Drawing.Size(59, 16)
        Me.rdbtnLoginWithBrowser.TabIndex = 0
        Me.rdbtnLoginWithBrowser.Text = "浏览器"
        Me.rdbtnLoginWithBrowser.UseVisualStyleBackColor = True
        '
        'FrmQuickLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(346, 241)
        Me.Controls.Add(Me.pnlLoginOptions)
        Me.Controls.Add(Me.cmbViewedRooms)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbUsersNick)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmQuickLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "登录小纠结"
        Me.TopMost = True
        Me.pnlLoginOptions.ResumeLayout(False)
        Me.pnlLoginOptions.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnCancel As Button
    Friend WithEvents btnOK As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbUsersNick As ComboBox
    Friend WithEvents cmbViewedRooms As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents chkStoreCookies As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents pnlLoginOptions As Panel
    Friend WithEvents Label6 As Label
    Friend WithEvents rdbtnLoginUseCookies As RadioButton
    Friend WithEvents rdbtnLoginWithBrowser As RadioButton
    Friend WithEvents btnTryDeleteCookies As Button
    Friend WithEvents rdbtnLoginUseQRCode As RadioButton
End Class
