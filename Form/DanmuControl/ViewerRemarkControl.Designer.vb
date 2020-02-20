<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ViewerRemarkControl
    'Inherits System.Windows.Forms.UserControl

    'UserControl 重写释放以清理组件列表。
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
        Me.txtViewerIdOrViewerName = New System.Windows.Forms.TextBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.txtRemark = New System.Windows.Forms.TextBox()
        Me.dgvUserList = New System.Windows.Forms.DataGridView()
        CType(Me.dgvUserList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtViewerIdOrViewerName
        '
        Me.txtViewerIdOrViewerName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtViewerIdOrViewerName.Location = New System.Drawing.Point(0, 0)
        Me.txtViewerIdOrViewerName.Margin = New System.Windows.Forms.Padding(0, 0, 3, 0)
        Me.txtViewerIdOrViewerName.Name = "txtViewerIdOrViewerName"
        Me.txtViewerIdOrViewerName.Size = New System.Drawing.Size(160, 21)
        Me.txtViewerIdOrViewerName.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(333, 0)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "保存"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'txtRemark
        '
        Me.txtRemark.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRemark.Location = New System.Drawing.Point(166, 0)
        Me.txtRemark.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.Size = New System.Drawing.Size(160, 21)
        Me.txtRemark.TabIndex = 1
        '
        'dgvUserList
        '
        Me.dgvUserList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvUserList.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvUserList.Location = New System.Drawing.Point(0, 27)
        Me.dgvUserList.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.dgvUserList.Name = "dgvUserList"
        Me.dgvUserList.RowTemplate.Height = 23
        Me.dgvUserList.Size = New System.Drawing.Size(407, 184)
        Me.dgvUserList.TabIndex = 3
        '
        'ViewerRemarkControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.dgvUserList)
        Me.Controls.Add(Me.txtRemark)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtViewerIdOrViewerName)
        Me.Name = "ViewerRemarkControl"
        Me.Size = New System.Drawing.Size(407, 211)
        CType(Me.dgvUserList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtViewerIdOrViewerName As TextBox
    Friend WithEvents btnOK As Button
    Friend WithEvents txtRemark As TextBox
    Friend WithEvents dgvUserList As DataGridView
End Class
