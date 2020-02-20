<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FunctionTestControl
    Inherits System.Windows.Forms.UserControl

    'UserControl 重写释放以清理组件列表。
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Me.txtRoomId = New System.Windows.Forms.TextBox()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.btnCloseTcp = New System.Windows.Forms.Button()
		Me.btnParseCmd = New System.Windows.Forms.Button()
		Me.btnTcpStreamTestAsync = New System.Windows.Forms.Button()
		Me.btnTcp = New System.Windows.Forms.Button()
		Me.btnWss = New System.Windows.Forms.Button()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.Button3 = New System.Windows.Forms.Button()
		Me.txtCmdSource = New System.Windows.Forms.TextBox()
		Me.btnPushStream = New System.Windows.Forms.Button()
		Me.txtTcpStreamSource = New System.Windows.Forms.TextBox()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		'
		'txtRoomId
		'
		Me.txtRoomId.Location = New System.Drawing.Point(50, 10)
		Me.txtRoomId.Name = "txtRoomId"
		Me.txtRoomId.Size = New System.Drawing.Size(100, 21)
		Me.txtRoomId.TabIndex = 125
		Me.txtRoomId.Text = "48499"
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(3, 13)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(41, 12)
		Me.Label1.TabIndex = 126
		Me.Label1.Text = "roomId"
		'
		'btnCloseTcp
		'
		Me.btnCloseTcp.Location = New System.Drawing.Point(168, 37)
		Me.btnCloseTcp.Name = "btnCloseTcp"
		Me.btnCloseTcp.Size = New System.Drawing.Size(75, 23)
		Me.btnCloseTcp.TabIndex = 127
		Me.btnCloseTcp.Text = "CloseTcp"
		Me.btnCloseTcp.UseVisualStyleBackColor = True
		'
		'btnParseCmd
		'
		Me.btnParseCmd.Location = New System.Drawing.Point(3, 302)
		Me.btnParseCmd.Name = "btnParseCmd"
		Me.btnParseCmd.Size = New System.Drawing.Size(75, 23)
		Me.btnParseCmd.TabIndex = 124
		Me.btnParseCmd.Text = "ParseCmd"
		Me.btnParseCmd.UseVisualStyleBackColor = True
		'
		'btnTcpStreamTestAsync
		'
		Me.btnTcpStreamTestAsync.Location = New System.Drawing.Point(168, 302)
		Me.btnTcpStreamTestAsync.Name = "btnTcpStreamTestAsync"
		Me.btnTcpStreamTestAsync.Size = New System.Drawing.Size(133, 23)
		Me.btnTcpStreamTestAsync.TabIndex = 123
		Me.btnTcpStreamTestAsync.Text = "TcpStreamTestAsync"
		Me.btnTcpStreamTestAsync.UseVisualStyleBackColor = True
		'
		'btnTcp
		'
		Me.btnTcp.Location = New System.Drawing.Point(86, 37)
		Me.btnTcp.Name = "btnTcp"
		Me.btnTcp.Size = New System.Drawing.Size(75, 23)
		Me.btnTcp.TabIndex = 122
		Me.btnTcp.Text = "tcp"
		Me.btnTcp.UseVisualStyleBackColor = True
		'
		'btnWss
		'
		Me.btnWss.Location = New System.Drawing.Point(5, 37)
		Me.btnWss.Name = "btnWss"
		Me.btnWss.Size = New System.Drawing.Size(75, 23)
		Me.btnWss.TabIndex = 121
		Me.btnWss.Text = "wss"
		Me.btnWss.UseVisualStyleBackColor = True
		'
		'Button2
		'
		Me.Button2.Location = New System.Drawing.Point(75, 96)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(75, 23)
		Me.Button2.TabIndex = 128
		Me.Button2.Text = "Button2"
		Me.Button2.UseVisualStyleBackColor = True
		'
		'Button3
		'
		Me.Button3.Location = New System.Drawing.Point(75, 121)
		Me.Button3.Name = "Button3"
		Me.Button3.Size = New System.Drawing.Size(75, 23)
		Me.Button3.TabIndex = 129
		Me.Button3.Text = "Button3"
		Me.Button3.UseVisualStyleBackColor = True
		'
		'txtCmdSource
		'
		Me.txtCmdSource.Location = New System.Drawing.Point(3, 150)
		Me.txtCmdSource.MaxLength = 0
		Me.txtCmdSource.Multiline = True
		Me.txtCmdSource.Name = "txtCmdSource"
		Me.txtCmdSource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtCmdSource.Size = New System.Drawing.Size(158, 146)
		Me.txtCmdSource.TabIndex = 130
		'
		'btnPushStream
		'
		Me.btnPushStream.Location = New System.Drawing.Point(0, 96)
		Me.btnPushStream.Name = "btnPushStream"
		Me.btnPushStream.Size = New System.Drawing.Size(75, 23)
		Me.btnPushStream.TabIndex = 131
		Me.btnPushStream.Text = "推流句柄"
		Me.btnPushStream.UseVisualStyleBackColor = True
		'
		'txtTcpStreamSource
		'
		Me.txtTcpStreamSource.Location = New System.Drawing.Point(167, 150)
		Me.txtTcpStreamSource.MaxLength = 0
		Me.txtTcpStreamSource.Multiline = True
		Me.txtTcpStreamSource.Name = "txtTcpStreamSource"
		Me.txtTcpStreamSource.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
		Me.txtTcpStreamSource.Size = New System.Drawing.Size(159, 146)
		Me.txtTcpStreamSource.TabIndex = 132
		'
		'Label2
		'
		Me.Label2.Location = New System.Drawing.Point(166, 106)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(160, 43)
		Me.Label2.TabIndex = 133
		Me.Label2.Text = "测试拆包情况可以将一个完整的包，拆成多个包，中间用‘;’分割"
		'
		'Button1
		'
		Me.Button1.Location = New System.Drawing.Point(168, 66)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(82, 23)
		Me.Button1.TabIndex = 134
		Me.Button1.Text = "FlashWindow"
		Me.Button1.UseVisualStyleBackColor = True
		'
		'FunctionTestControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.Button1)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.txtTcpStreamSource)
		Me.Controls.Add(Me.btnPushStream)
		Me.Controls.Add(Me.txtCmdSource)
		Me.Controls.Add(Me.txtRoomId)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.btnCloseTcp)
		Me.Controls.Add(Me.btnParseCmd)
		Me.Controls.Add(Me.btnTcpStreamTestAsync)
		Me.Controls.Add(Me.btnTcp)
		Me.Controls.Add(Me.btnWss)
		Me.Controls.Add(Me.Button2)
		Me.Controls.Add(Me.Button3)
		Me.Name = "FunctionTestControl"
		Me.Size = New System.Drawing.Size(329, 328)
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents txtRoomId As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnCloseTcp As Button
    Friend WithEvents btnParseCmd As Button
    Friend WithEvents btnTcpStreamTestAsync As Button
    Friend WithEvents btnTcp As Button
    Friend WithEvents btnWss As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents txtCmdSource As TextBox
    Friend WithEvents btnPushStream As Button
    Friend WithEvents txtTcpStreamSource As TextBox
    Friend WithEvents Label2 As Label
	Friend WithEvents Button1 As Button
End Class
