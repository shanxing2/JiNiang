<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSelectArea
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSelectArea))
        Me.lblLiveArea = New System.Windows.Forms.Label()
        Me.flpChooseAreaContainer = New System.Windows.Forms.FlowLayoutPanel()
        Me.flpChildAreaContainer = New System.Windows.Forms.FlowLayoutPanel()
        Me.txtSearchAreaQuickly = New System.Windows.Forms.TextBox()
        Me.tlpParentAreaContainer = New System.Windows.Forms.TableLayoutPanel()
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
        Me.lblLiveArea.TabIndex = 156
        Me.lblLiveArea.Text = "直播分类"
        '
        'flpChooseAreaContainer
        '
        Me.flpChooseAreaContainer.AutoScroll = True
        Me.flpChooseAreaContainer.BackColor = System.Drawing.SystemColors.Control
        Me.flpChooseAreaContainer.Location = New System.Drawing.Point(10, 40)
        Me.flpChooseAreaContainer.Name = "flpChooseAreaContainer"
        Me.flpChooseAreaContainer.Size = New System.Drawing.Size(413, 23)
        Me.flpChooseAreaContainer.TabIndex = 155
        '
        'flpChildAreaContainer
        '
        Me.flpChildAreaContainer.AutoScroll = True
        Me.flpChildAreaContainer.BackColor = System.Drawing.SystemColors.Control
        Me.flpChildAreaContainer.Location = New System.Drawing.Point(10, 137)
        Me.flpChildAreaContainer.Name = "flpChildAreaContainer"
        Me.flpChildAreaContainer.Size = New System.Drawing.Size(413, 234)
        Me.flpChildAreaContainer.TabIndex = 2
        '
        'txtSearchAreaQuickly
        '
        Me.txtSearchAreaQuickly.Location = New System.Drawing.Point(10, 69)
        Me.txtSearchAreaQuickly.Name = "txtSearchAreaQuickly"
        Me.txtSearchAreaQuickly.Size = New System.Drawing.Size(413, 21)
        Me.txtSearchAreaQuickly.TabIndex = 1
        '
        'tlpParentAreaContainer
        '
        Me.tlpParentAreaContainer.ColumnCount = 1
        Me.tlpParentAreaContainer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.53623!))
        Me.tlpParentAreaContainer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.46377!))
        Me.tlpParentAreaContainer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97.0!))
        Me.tlpParentAreaContainer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
        Me.tlpParentAreaContainer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpParentAreaContainer.Location = New System.Drawing.Point(10, 104)
        Me.tlpParentAreaContainer.Name = "tlpParentAreaContainer"
        Me.tlpParentAreaContainer.RowCount = 1
        Me.tlpParentAreaContainer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.tlpParentAreaContainer.Size = New System.Drawing.Size(413, 27)
        Me.tlpParentAreaContainer.TabIndex = 163
        '
        'FrmSelectArea
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(433, 383)
        Me.Controls.Add(Me.tlpParentAreaContainer)
        Me.Controls.Add(Me.txtSearchAreaQuickly)
        Me.Controls.Add(Me.flpChildAreaContainer)
        Me.Controls.Add(Me.lblLiveArea)
        Me.Controls.Add(Me.flpChooseAreaContainer)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSelectArea"
        Me.Text = "分区设置"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblLiveArea As Label
    Friend WithEvents flpChooseAreaContainer As FlowLayoutPanel
    Friend WithEvents flpChildAreaContainer As FlowLayoutPanel
    Friend WithEvents txtSearchAreaQuickly As TextBox
    Friend WithEvents tlpParentAreaContainer As TableLayoutPanel
End Class
