<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmYanText
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmYanText))
        Me.flpYanTextContainer = New System.Windows.Forms.FlowLayoutPanel()
        Me.SuspendLayout()
        '
        'flpYanTextContainer
        '
        Me.flpYanTextContainer.BackColor = System.Drawing.SystemColors.Control
        Me.flpYanTextContainer.Location = New System.Drawing.Point(12, 12)
        Me.flpYanTextContainer.Name = "flpYanTextContainer"
        Me.flpYanTextContainer.Size = New System.Drawing.Size(280, 281)
        Me.flpYanTextContainer.TabIndex = 1
        '
        'FrmYanText
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(304, 306)
        Me.Controls.Add(Me.flpYanTextContainer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmYanText"
        Me.Text = "颜文字面板"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents flpYanTextContainer As FlowLayoutPanel
End Class
