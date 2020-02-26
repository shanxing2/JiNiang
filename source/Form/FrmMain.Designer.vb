<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    ''Form 重写 Dispose，以清理组件列表。
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tpLiveRoom = New System.Windows.Forms.TabPage()
        Me.tpSetting = New System.Windows.Forms.TabPage()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.tpLiveRoom)
        Me.TabControl1.Controls.Add(Me.tpSetting)
        Me.TabControl1.Location = New System.Drawing.Point(-2, -1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(372, 498)
        Me.TabControl1.TabIndex = 19
        '
        'tpLiveRoom
        '
        Me.tpLiveRoom.BackColor = System.Drawing.SystemColors.Control
        Me.tpLiveRoom.ForeColor = System.Drawing.SystemColors.ControlText
        Me.tpLiveRoom.Location = New System.Drawing.Point(4, 22)
        Me.tpLiveRoom.Margin = New System.Windows.Forms.Padding(0)
        Me.tpLiveRoom.Name = "tpLiveRoom"
        Me.tpLiveRoom.Padding = New System.Windows.Forms.Padding(3, 2, 3, 3)
        Me.tpLiveRoom.Size = New System.Drawing.Size(364, 472)
        Me.tpLiveRoom.TabIndex = 0
        Me.tpLiveRoom.Text = "直播间"
        '
        'tpSetting
        '
        Me.tpSetting.BackColor = System.Drawing.SystemColors.Control
        Me.tpSetting.Location = New System.Drawing.Point(4, 22)
        Me.tpSetting.Name = "tpSetting"
        Me.tpSetting.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSetting.Size = New System.Drawing.Size(364, 472)
        Me.tpSetting.TabIndex = 3
        Me.tpSetting.Text = "配置"
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(367, 496)
        Me.Controls.Add(Me.TabControl1)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "FrmMain"
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents tpLiveRoom As TabPage
    Friend WithEvents tpSetting As TabPage
End Class
