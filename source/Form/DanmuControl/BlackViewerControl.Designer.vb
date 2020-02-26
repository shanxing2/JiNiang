<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class BlackViewerControl
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
		Me.btnOK = New System.Windows.Forms.Button()
		Me.dgvUserList = New System.Windows.Forms.DataGridView()
		Me.cmbBlockHourItems = New System.Windows.Forms.ComboBox()
		Me.tlpControlContainer = New System.Windows.Forms.TableLayoutPanel()
		Me.Panel2 = New System.Windows.Forms.Panel()
		CType(Me.dgvUserList, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.tlpControlContainer.SuspendLayout()
		Me.Panel2.SuspendLayout()
		Me.SuspendLayout()
		'
		'txtUidOrUname
		'
		Me.txtUidOrUname.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.txtUidOrUname.Location = New System.Drawing.Point(2, 1)
		Me.txtUidOrUname.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.txtUidOrUname.Name = "txtUidOrUname"
		Me.txtUidOrUname.Size = New System.Drawing.Size(188, 21)
		Me.txtUidOrUname.TabIndex = 1
		'
		'btnOK
		'
		Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.btnOK.Location = New System.Drawing.Point(336, 0)
		Me.btnOK.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.btnOK.Name = "btnOK"
		Me.btnOK.Size = New System.Drawing.Size(75, 23)
		Me.btnOK.TabIndex = 0
		Me.btnOK.Text = "提交"
		Me.btnOK.UseVisualStyleBackColor = True
		'
		'dgvUserList
		'
		Me.dgvUserList.BackgroundColor = System.Drawing.SystemColors.Window
		Me.dgvUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.dgvUserList.Location = New System.Drawing.Point(0, 27)
		Me.dgvUserList.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
		Me.dgvUserList.Name = "dgvUserList"
		Me.dgvUserList.RowTemplate.Height = 23
		Me.dgvUserList.Size = New System.Drawing.Size(290, 218)
		Me.dgvUserList.TabIndex = 3
		'
		'cmbBlockHourItems
		'
		Me.cmbBlockHourItems.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.cmbBlockHourItems.FormattingEnabled = True
		Me.cmbBlockHourItems.Location = New System.Drawing.Point(196, 1)
		Me.cmbBlockHourItems.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
		Me.cmbBlockHourItems.Name = "cmbBlockHourItems"
		Me.cmbBlockHourItems.Size = New System.Drawing.Size(134, 20)
		Me.cmbBlockHourItems.TabIndex = 170
		'
		'tlpControlContainer
		'
		Me.tlpControlContainer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.tlpControlContainer.BackColor = System.Drawing.SystemColors.Control
		Me.tlpControlContainer.ColumnCount = 1
		Me.tlpControlContainer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
		Me.tlpControlContainer.Controls.Add(Me.Panel2, 0, 0)
		Me.tlpControlContainer.Controls.Add(Me.dgvUserList, 0, 1)
		Me.tlpControlContainer.Location = New System.Drawing.Point(0, 0)
		Me.tlpControlContainer.Margin = New System.Windows.Forms.Padding(0)
		Me.tlpControlContainer.Name = "tlpControlContainer"
		Me.tlpControlContainer.RowCount = 2
		Me.tlpControlContainer.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tlpControlContainer.RowStyles.Add(New System.Windows.Forms.RowStyle())
		Me.tlpControlContainer.Size = New System.Drawing.Size(411, 469)
		Me.tlpControlContainer.TabIndex = 206
		'
		'Panel2
		'
		Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Panel2.BackColor = System.Drawing.SystemColors.Control
		Me.Panel2.Controls.Add(Me.txtUidOrUname)
		Me.Panel2.Controls.Add(Me.cmbBlockHourItems)
		Me.Panel2.Controls.Add(Me.btnOK)
		Me.Panel2.Location = New System.Drawing.Point(0, 0)
		Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
		Me.Panel2.Name = "Panel2"
		Me.Panel2.Size = New System.Drawing.Size(411, 24)
		Me.Panel2.TabIndex = 207
		'
		'BlackViewerControl
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.Controls.Add(Me.tlpControlContainer)
		Me.Name = "BlackViewerControl"
		Me.Size = New System.Drawing.Size(411, 469)
		CType(Me.dgvUserList, System.ComponentModel.ISupportInitialize).EndInit()
		Me.tlpControlContainer.ResumeLayout(False)
		Me.Panel2.ResumeLayout(False)
		Me.Panel2.PerformLayout()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents txtUidOrUname As TextBox
	Friend WithEvents btnOK As Button
	Friend WithEvents dgvUserList As DataGridView
	Friend WithEvents cmbBlockHourItems As ComboBox
	Friend WithEvents tlpControlContainer As TableLayoutPanel
	Friend WithEvents Panel2 As Panel
End Class
