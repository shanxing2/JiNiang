<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPluginManage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPluginManage))
        Me.dgvInfo1 = New System.Windows.Forms.DataGridView()
        CType(Me.dgvInfo1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvInfo1
        '
        Me.dgvInfo1.BackgroundColor = System.Drawing.SystemColors.Window
        Me.dgvInfo1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvInfo1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInfo1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvInfo1.Location = New System.Drawing.Point(0, 0)
        Me.dgvInfo1.Name = "dgvInfo1"
        Me.dgvInfo1.RowTemplate.Height = 23
        Me.dgvInfo1.Size = New System.Drawing.Size(646, 318)
        Me.dgvInfo1.TabIndex = 32
        '
        'FrmPluginManage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(646, 318)
        Me.Controls.Add(Me.dgvInfo1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FrmPluginManage"
        Me.Text = "插件中心"
        CType(Me.dgvInfo1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvInfo1 As DataGridView
End Class
