<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FansInfoEditControl
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblFansId = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.txtNick = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.dtpAttentionTime = New System.Windows.Forms.DateTimePicker()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.lblTips = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblFansId
        '
        Me.lblFansId.AutoSize = True
        Me.lblFansId.Location = New System.Drawing.Point(17, 10)
        Me.lblFansId.Name = "lblFansId"
        Me.lblFansId.Size = New System.Drawing.Size(29, 12)
        Me.lblFansId.TabIndex = 0
        Me.lblFansId.Text = "ID："
        '
        'txtId
        '
        Me.txtId.BackColor = System.Drawing.SystemColors.Control
        Me.txtId.Location = New System.Drawing.Point(37, 6)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(261, 21)
        Me.txtId.TabIndex = 1
        '
        'txtNick
        '
        Me.txtNick.BackColor = System.Drawing.SystemColors.Control
        Me.txtNick.Location = New System.Drawing.Point(37, 32)
        Me.txtNick.Name = "txtNick"
        Me.txtNick.Size = New System.Drawing.Size(261, 21)
        Me.txtNick.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "昵称："
        '
        'txtRemark
        '
        Me.txtRemark.Location = New System.Drawing.Point(363, 32)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(261, 21)
        Me.txtRemark.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(331, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 12)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "备注："
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(307, 10)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 12)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "关注时间："
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(549, 59)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "保存"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'dtpAttentionTime
        '
        Me.dtpAttentionTime.Location = New System.Drawing.Point(363, 4)
        Me.dtpAttentionTime.Name = "dtpAttentionTime"
        Me.dtpAttentionTime.Size = New System.Drawing.Size(260, 21)
        Me.dtpAttentionTime.TabIndex = 10
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(468, 59)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 23)
        Me.btnNew.TabIndex = 11
        Me.btnNew.Text = "新增"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'lblTips
        '
        Me.lblTips.AutoSize = True
        Me.lblTips.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblTips.Location = New System.Drawing.Point(35, 64)
        Me.lblTips.Name = "lblTips"
        Me.lblTips.Size = New System.Drawing.Size(0, 12)
        Me.lblTips.TabIndex = 12
        '
        'FansInfoEditControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.lblTips)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.dtpAttentionTime)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtNick)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.lblFansId)
        Me.Name = "FansInfoEditControl"
        Me.Padding = New System.Windows.Forms.Padding(3)
        Me.Size = New System.Drawing.Size(629, 86)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblFansId As Label
    Friend WithEvents txtId As TextBox
    Friend WithEvents txtNick As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents txtRemark As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnSave As Button
    Friend WithEvents dtpAttentionTime As DateTimePicker
    Friend WithEvents btnNew As Button
    Friend WithEvents lblTips As Label
End Class
