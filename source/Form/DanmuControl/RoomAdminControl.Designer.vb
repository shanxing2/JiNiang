<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RoomAdminControl
    'Inherits System.Windows.Forms.UserControl

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
        Me.txtUidOrUname = New System.Windows.Forms.TextBox()
        Me.btnAppoint = New System.Windows.Forms.Button()
        Me.dgvUserList = New System.Windows.Forms.DataGridView()
        Me.lblAdminCount = New System.Windows.Forms.Label()
        CType(Me.dgvUserList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtUidOrUname
        '
        Me.txtUidOrUname.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtUidOrUname.Location = New System.Drawing.Point(0, 0)
        Me.txtUidOrUname.Margin = New System.Windows.Forms.Padding(0, 0, 3, 3)
        Me.txtUidOrUname.Name = "txtUidOrUname"
        Me.txtUidOrUname.Size = New System.Drawing.Size(326, 21)
        Me.txtUidOrUname.TabIndex = 1
        '
        'btnAppoint
        '
        Me.btnAppoint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAppoint.Location = New System.Drawing.Point(333, 0)
        Me.btnAppoint.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.btnAppoint.Name = "btnAppoint"
        Me.btnAppoint.Size = New System.Drawing.Size(75, 23)
        Me.btnAppoint.TabIndex = 0
        Me.btnAppoint.Text = "任命"
        Me.btnAppoint.UseVisualStyleBackColor = True
        '
        'dgvUserList
        '
        Me.dgvUserList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvUserList.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUserList.Location = New System.Drawing.Point(0, 39)
        Me.dgvUserList.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.dgvUserList.Name = "dgvUserList"
        Me.dgvUserList.RowTemplate.Height = 23
        Me.dgvUserList.Size = New System.Drawing.Size(407, 181)
        Me.dgvUserList.TabIndex = 3
        '
        'lblAdminCount
        '
        Me.lblAdminCount.AutoSize = True
        Me.lblAdminCount.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblAdminCount.Location = New System.Drawing.Point(-2, 24)
        Me.lblAdminCount.Name = "lblAdminCount"
        Me.lblAdminCount.Size = New System.Drawing.Size(107, 12)
        Me.lblAdminCount.TabIndex = 6
        Me.lblAdminCount.Text = "当前房管数：0/100"
        '
        'RoomAdminControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblAdminCount)
        Me.Controls.Add(Me.dgvUserList)
        Me.Controls.Add(Me.btnAppoint)
        Me.Controls.Add(Me.txtUidOrUname)
        Me.Name = "RoomAdminControl"
        Me.Size = New System.Drawing.Size(407, 220)
        CType(Me.dgvUserList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtUidOrUname As TextBox
    Friend WithEvents btnAppoint As Button
    Friend WithEvents dgvUserList As DataGridView
    Friend WithEvents lblAdminCount As Label
End Class
