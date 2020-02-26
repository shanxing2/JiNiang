<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmRoomViewerManage
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmRoomViewerManage))
		Me.tlpManageTab = New System.Windows.Forms.TableLayoutPanel()
		Me.tlpControlContainer = New System.Windows.Forms.TableLayoutPanel()
		Me.SuspendLayout()
		'
		'tlpManageTab
		'
		Me.tlpManageTab.ColumnCount = 1
		Me.tlpManageTab.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.53623!))
		Me.tlpManageTab.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.46377!))
		Me.tlpManageTab.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97.0!))
		Me.tlpManageTab.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
		Me.tlpManageTab.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tlpManageTab.Location = New System.Drawing.Point(12, 12)
		Me.tlpManageTab.Name = "tlpManageTab"
		Me.tlpManageTab.RowCount = 1
		Me.tlpManageTab.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tlpManageTab.Size = New System.Drawing.Size(524, 25)
		Me.tlpManageTab.TabIndex = 3
		'
		'tlpControlContainer
		'
		Me.tlpControlContainer.BackColor = System.Drawing.SystemColors.Control
		Me.tlpControlContainer.ColumnCount = 1
		Me.tlpControlContainer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.53623!))
		Me.tlpControlContainer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.46377!))
		Me.tlpControlContainer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97.0!))
		Me.tlpControlContainer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 115.0!))
		Me.tlpControlContainer.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
		Me.tlpControlContainer.Location = New System.Drawing.Point(12, 43)
		Me.tlpControlContainer.Name = "tlpControlContainer"
		Me.tlpControlContainer.RowCount = 1
		Me.tlpControlContainer.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
		Me.tlpControlContainer.Size = New System.Drawing.Size(524, 491)
		Me.tlpControlContainer.TabIndex = 4
		'
		'FrmRoomViewerManage
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(548, 546)
		Me.Controls.Add(Me.tlpControlContainer)
		Me.Controls.Add(Me.tlpManageTab)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "FrmRoomViewerManage"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents tlpManageTab As TableLayoutPanel
	Friend WithEvents tlpControlContainer As TableLayoutPanel
End Class
