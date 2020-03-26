<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PagerControl
    Inherits System.Windows.Forms.UserControl

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
        Me.nudPageSize = New System.Windows.Forms.NumericUpDown()
        Me.lblPageSize = New System.Windows.Forms.Label()
        Me.lblPageInfo = New System.Windows.Forms.Label()
        Me.btnGotoNextPage = New System.Windows.Forms.Button()
        Me.btnGotoPreviousPage = New System.Windows.Forms.Button()
        Me.btnGotoLastPage = New System.Windows.Forms.Button()
        Me.btnGotoFirstPage = New System.Windows.Forms.Button()
        Me.lblTotalCount = New System.Windows.Forms.Label()
        CType(Me.nudPageSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'nudPageSize
        '
        Me.nudPageSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nudPageSize.Location = New System.Drawing.Point(128, 1)
        Me.nudPageSize.Name = "nudPageSize"
        Me.nudPageSize.Size = New System.Drawing.Size(43, 21)
        Me.nudPageSize.TabIndex = 0
        Me.nudPageSize.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'lblPageSize
        '
        Me.lblPageSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPageSize.AutoSize = True
        Me.lblPageSize.Location = New System.Drawing.Point(97, 5)
        Me.lblPageSize.Name = "lblPageSize"
        Me.lblPageSize.Size = New System.Drawing.Size(41, 12)
        Me.lblPageSize.TabIndex = 19
        Me.lblPageSize.Text = "页码："
        '
        'lblPageInfo
        '
        Me.lblPageInfo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPageInfo.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblPageInfo.Location = New System.Drawing.Point(177, 5)
        Me.lblPageInfo.Name = "lblPageInfo"
        Me.lblPageInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblPageInfo.Size = New System.Drawing.Size(39, 12)
        Me.lblPageInfo.TabIndex = 18
        Me.lblPageInfo.Text = "0/0"
        '
        'btnGotoNextPage
        '
        Me.btnGotoNextPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGotoNextPage.Location = New System.Drawing.Point(278, 0)
        Me.btnGotoNextPage.Name = "btnGotoNextPage"
        Me.btnGotoNextPage.Size = New System.Drawing.Size(28, 23)
        Me.btnGotoNextPage.TabIndex = 3
        Me.btnGotoNextPage.Text = ">"
        Me.btnGotoNextPage.UseVisualStyleBackColor = True
        '
        'btnGotoPreviousPage
        '
        Me.btnGotoPreviousPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGotoPreviousPage.Location = New System.Drawing.Point(248, 0)
        Me.btnGotoPreviousPage.Name = "btnGotoPreviousPage"
        Me.btnGotoPreviousPage.Size = New System.Drawing.Size(28, 23)
        Me.btnGotoPreviousPage.TabIndex = 2
        Me.btnGotoPreviousPage.Text = "<"
        Me.btnGotoPreviousPage.UseVisualStyleBackColor = True
        '
        'btnGotoLastPage
        '
        Me.btnGotoLastPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGotoLastPage.Location = New System.Drawing.Point(308, 0)
        Me.btnGotoLastPage.Name = "btnGotoLastPage"
        Me.btnGotoLastPage.Size = New System.Drawing.Size(28, 23)
        Me.btnGotoLastPage.TabIndex = 4
        Me.btnGotoLastPage.Text = ">|"
        Me.btnGotoLastPage.UseVisualStyleBackColor = True
        '
        'btnGotoFirstPage
        '
        Me.btnGotoFirstPage.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGotoFirstPage.Location = New System.Drawing.Point(218, 0)
        Me.btnGotoFirstPage.Name = "btnGotoFirstPage"
        Me.btnGotoFirstPage.Size = New System.Drawing.Size(28, 23)
        Me.btnGotoFirstPage.TabIndex = 1
        Me.btnGotoFirstPage.Text = "|<"
        Me.btnGotoFirstPage.UseVisualStyleBackColor = True
        '
        'lblTotalCount
        '
        Me.lblTotalCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalCount.AutoEllipsis = True
        Me.lblTotalCount.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblTotalCount.Location = New System.Drawing.Point(0, 5)
        Me.lblTotalCount.Name = "lblTotalCount"
        Me.lblTotalCount.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTotalCount.Size = New System.Drawing.Size(98, 12)
        Me.lblTotalCount.TabIndex = 20
        Me.lblTotalCount.Text = "0：总数"
        '
        'PagerControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.lblTotalCount)
        Me.Controls.Add(Me.nudPageSize)
        Me.Controls.Add(Me.lblPageSize)
        Me.Controls.Add(Me.lblPageInfo)
        Me.Controls.Add(Me.btnGotoNextPage)
        Me.Controls.Add(Me.btnGotoPreviousPage)
        Me.Controls.Add(Me.btnGotoLastPage)
        Me.Controls.Add(Me.btnGotoFirstPage)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "PagerControl"
        Me.Size = New System.Drawing.Size(335, 22)
        CType(Me.nudPageSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents nudPageSize As NumericUpDown
    Friend WithEvents lblPageSize As Label
    Friend WithEvents lblPageInfo As Label
    Friend WithEvents btnGotoNextPage As Button
    Friend WithEvents btnGotoPreviousPage As Button
    Friend WithEvents btnGotoLastPage As Button
    Friend WithEvents btnGotoFirstPage As Button
    Friend WithEvents lblTotalCount As Label
End Class
