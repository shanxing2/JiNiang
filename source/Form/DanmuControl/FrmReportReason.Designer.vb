<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmReportReason
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
        Me.lblLiveArea = New System.Windows.Forms.Label()
        Me.lblOK = New System.Windows.Forms.Label()
        Me.lblViewerNick = New System.Windows.Forms.Label()
        Me.lblViewerDanmu = New System.Windows.Forms.Label()
        Me.cmbReportReason = New System.Windows.Forms.ComboBox()
        Me.lblCancel = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblLiveArea
        '
        Me.lblLiveArea.AutoSize = True
        Me.lblLiveArea.Font = New System.Drawing.Font("新宋体", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblLiveArea.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblLiveArea.Location = New System.Drawing.Point(12, 15)
        Me.lblLiveArea.Margin = New System.Windows.Forms.Padding(3, 6, 3, 6)
        Me.lblLiveArea.Name = "lblLiveArea"
        Me.lblLiveArea.Size = New System.Drawing.Size(76, 16)
        Me.lblLiveArea.TabIndex = 157
        Me.lblLiveArea.Text = "举报弹幕"
        '
        'lblOK
        '
        Me.lblOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOK.AutoSize = True
        Me.lblOK.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblOK.Font = New System.Drawing.Font("新宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblOK.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOK.Location = New System.Drawing.Point(229, 214)
        Me.lblOK.Margin = New System.Windows.Forms.Padding(3)
        Me.lblOK.Name = "lblOK"
        Me.lblOK.Padding = New System.Windows.Forms.Padding(3)
        Me.lblOK.Size = New System.Drawing.Size(48, 24)
        Me.lblOK.TabIndex = 1
        Me.lblOK.Text = "确认"
        '
        'lblViewerNick
        '
        Me.lblViewerNick.AutoSize = True
        Me.lblViewerNick.Location = New System.Drawing.Point(13, 70)
        Me.lblViewerNick.Name = "lblViewerNick"
        Me.lblViewerNick.Size = New System.Drawing.Size(53, 12)
        Me.lblViewerNick.TabIndex = 159
        Me.lblViewerNick.Text = "观众昵称"
        '
        'lblViewerDanmu
        '
        Me.lblViewerDanmu.AutoSize = True
        Me.lblViewerDanmu.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblViewerDanmu.Location = New System.Drawing.Point(13, 113)
        Me.lblViewerDanmu.Name = "lblViewerDanmu"
        Me.lblViewerDanmu.Size = New System.Drawing.Size(53, 12)
        Me.lblViewerDanmu.TabIndex = 160
        Me.lblViewerDanmu.Text = "观众弹幕"
        '
        'cmbReportReason
        '
        Me.cmbReportReason.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbReportReason.FormattingEnabled = True
        Me.cmbReportReason.Location = New System.Drawing.Point(14, 159)
        Me.cmbReportReason.Name = "cmbReportReason"
        Me.cmbReportReason.Size = New System.Drawing.Size(311, 20)
        Me.cmbReportReason.TabIndex = 3
        '
        'lblCancel
        '
        Me.lblCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCancel.AutoSize = True
        Me.lblCancel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblCancel.Font = New System.Drawing.Font("新宋体", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCancel.Location = New System.Drawing.Point(283, 214)
        Me.lblCancel.Margin = New System.Windows.Forms.Padding(3)
        Me.lblCancel.Name = "lblCancel"
        Me.lblCancel.Padding = New System.Windows.Forms.Padding(3)
        Me.lblCancel.Size = New System.Drawing.Size(48, 24)
        Me.lblCancel.TabIndex = 2
        Me.lblCancel.Text = "取消"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 52)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 161
        Me.Label1.Text = "观众昵称："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 97)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 12)
        Me.Label2.TabIndex = 162
        Me.Label2.Text = "被举报弹幕："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 142)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 163
        Me.Label3.Text = "举报理由："
        '
        'FrmReportReason
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(339, 243)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblCancel)
        Me.Controls.Add(Me.cmbReportReason)
        Me.Controls.Add(Me.lblViewerDanmu)
        Me.Controls.Add(Me.lblViewerNick)
        Me.Controls.Add(Me.lblOK)
        Me.Controls.Add(Me.lblLiveArea)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmReportReason"
        Me.Text = "FrmReportReason"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblLiveArea As Label
    Friend WithEvents lblOK As Label
    Friend WithEvents lblViewerNick As Label
    Friend WithEvents lblViewerDanmu As Label
    Friend WithEvents cmbReportReason As ComboBox
    Friend WithEvents lblCancel As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
