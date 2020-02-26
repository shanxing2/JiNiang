<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmLoginFromCookies
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLoginFromCookies))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCookies = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.txtDomain = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 45)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(119, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "登录成功后的Cookies"
        '
        'txtCookies
        '
        Me.txtCookies.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCookies.Location = New System.Drawing.Point(12, 60)
        Me.txtCookies.Multiline = True
        Me.txtCookies.Name = "txtCookies"
        Me.txtCookies.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtCookies.Size = New System.Drawing.Size(562, 218)
        Me.txtCookies.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(413, 284)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(161, 23)
        Me.btnOK.TabIndex = 132
        Me.btnOK.Text = "emmmm，就这样吧(๑‾ ꇴ ‾๑)~"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'txtDomain
        '
        Me.txtDomain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDomain.Location = New System.Drawing.Point(12, 21)
        Me.txtDomain.Name = "txtDomain"
        Me.txtDomain.Size = New System.Drawing.Size(562, 21)
        Me.txtDomain.TabIndex = 133
        Me.txtDomain.Text = "https://live.bilibili.com"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 134
        Me.Label2.Text = "domain"
        '
        'FrmLoginCookies
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(586, 319)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtDomain)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtCookies)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "FrmLoginCookies"
        Me.Text = "使用Cookies验证"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtCookies As TextBox
    Friend WithEvents btnOK As Button
    Friend WithEvents txtDomain As TextBox
    Friend WithEvents Label2 As Label
End Class
