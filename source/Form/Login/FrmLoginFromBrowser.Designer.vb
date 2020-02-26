<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmLoginFromBrowser
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmLoginFromBrowser))
		Me.SuspendLayout()
		'
		'FrmLoginBrowser
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(628, 433)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "FrmLoginBrowser"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "登录专用浏览器 V1"
		Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
		Me.ResumeLayout(False)

	End Sub
End Class
