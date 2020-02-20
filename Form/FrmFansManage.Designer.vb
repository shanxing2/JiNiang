<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFansManage
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFansManage))
        Me.lblDgv1TotalRow = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.dgvInfo1 = New System.Windows.Forms.DataGridView()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.gbxEditMoudle = New System.Windows.Forms.GroupBox()
        CType(Me.dgvInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblDgv1TotalRow
        '
        Me.lblDgv1TotalRow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDgv1TotalRow.AutoSize = True
        Me.lblDgv1TotalRow.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblDgv1TotalRow.Location = New System.Drawing.Point(68, 305)
        Me.lblDgv1TotalRow.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblDgv1TotalRow.Name = "lblDgv1TotalRow"
        Me.lblDgv1TotalRow.Size = New System.Drawing.Size(11, 12)
        Me.lblDgv1TotalRow.TabIndex = 33
        Me.lblDgv1TotalRow.Text = "0"
        '
        'lblTotal
        '
        Me.lblTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Location = New System.Drawing.Point(7, 305)
        Me.lblTotal.Margin = New System.Windows.Forms.Padding(3, 0, 0, 0)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(65, 12)
        Me.lblTotal.TabIndex = 32
        Me.lblTotal.Text = "共有数据："
        '
        'dgvInfo1
        '
        Me.dgvInfo1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvInfo1.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvInfo1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvInfo1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInfo1.Location = New System.Drawing.Point(9, 130)
        Me.dgvInfo1.Name = "dgvInfo1"
        Me.dgvInfo1.RowTemplate.Height = 23
        Me.dgvInfo1.Size = New System.Drawing.Size(635, 172)
        Me.dgvInfo1.TabIndex = 31
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(546, 336)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 34
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'gbxEditMoudle
        '
        Me.gbxEditMoudle.BackColor = System.Drawing.SystemColors.Control
        Me.gbxEditMoudle.Location = New System.Drawing.Point(9, 0)
        Me.gbxEditMoudle.Margin = New System.Windows.Forms.Padding(0)
        Me.gbxEditMoudle.Name = "gbxEditMoudle"
        Me.gbxEditMoudle.Size = New System.Drawing.Size(200, 100)
        Me.gbxEditMoudle.TabIndex = 35
        Me.gbxEditMoudle.TabStop = False
        '
        'FrmFansManage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(652, 450)
        Me.Controls.Add(Me.gbxEditMoudle)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lblDgv1TotalRow)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.dgvInfo1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmFansManage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "粉丝备注管理"
        CType(Me.dgvInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblDgv1TotalRow As Label
    Friend WithEvents lblTotal As Label
    Friend WithEvents dgvInfo1 As DataGridView
    Friend WithEvents Button1 As Button
    Friend WithEvents gbxEditMoudle As GroupBox
End Class
