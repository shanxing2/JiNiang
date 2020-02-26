<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class LiveRoomInfoControl
    Inherits System.Windows.Forms.UserControl

    ''Form 重写 Dispose，以清理组件列表。
    '<System.Diagnostics.DebuggerNonUserCode()> _
    'Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    '    Try
    '        If disposing AndAlso components IsNot Nothing Then
    '            components.Dispose()
    '        End If
    '    Finally
    '        MyBase.Dispose(disposing)
    '    End Try
    'End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.lblOnline = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblLiveStatus = New System.Windows.Forms.Label()
        Me.lblAreaRank = New System.Windows.Forms.Label()
        Me.lblAnchorScore = New System.Windows.Forms.Label()
        Me.lblSan = New System.Windows.Forms.Label()
        Me.lblAttention = New System.Windows.Forms.Label()
        Me.tlpLiveRoomInfoRow1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblHourRank = New System.Windows.Forms.Label()
        Me.chkTopMost = New System.Windows.Forms.CheckBox()
        Me.tlpLiveRoomInfoRow2 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblRoomOwnerNick = New System.Windows.Forms.Label()
        Me.lblJoinRoom = New System.Windows.Forms.Label()
        Me.lblUserNick = New System.Windows.Forms.Label()
        Me.tlpLiveRoomInfoRow1.SuspendLayout()
        Me.tlpLiveRoomInfoRow2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblOnline
        '
        Me.lblOnline.AutoSize = True
        Me.lblOnline.BackColor = System.Drawing.Color.Transparent
        Me.lblOnline.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblOnline.Location = New System.Drawing.Point(1, 4)
        Me.lblOnline.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lblOnline.Name = "lblOnline"
        Me.lblOnline.Size = New System.Drawing.Size(47, 12)
        Me.lblOnline.TabIndex = 13
        Me.lblOnline.Text = "人气 -1"
        '
        'lblLiveStatus
        '
        Me.lblLiveStatus.BackColor = System.Drawing.Color.Transparent
        Me.lblLiveStatus.Font = New System.Drawing.Font("宋体", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblLiveStatus.ForeColor = System.Drawing.Color.Gray
        Me.lblLiveStatus.Location = New System.Drawing.Point(1, 1)
        Me.lblLiveStatus.Margin = New System.Windows.Forms.Padding(0)
        Me.lblLiveStatus.Name = "lblLiveStatus"
        Me.lblLiveStatus.Size = New System.Drawing.Size(30, 21)
        Me.lblLiveStatus.TabIndex = 104
        Me.lblLiveStatus.Text = "●"
        Me.ToolTip1.SetToolTip(Me.lblLiveStatus, "直播状态")
        '
        'lblAreaRank
        '
        Me.lblAreaRank.AutoSize = True
        Me.lblAreaRank.BackColor = System.Drawing.Color.Transparent
        Me.lblAreaRank.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblAreaRank.Location = New System.Drawing.Point(245, 4)
        Me.lblAreaRank.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lblAreaRank.Name = "lblAreaRank"
        Me.lblAreaRank.Size = New System.Drawing.Size(59, 12)
        Me.lblAreaRank.TabIndex = 136
        Me.lblAreaRank.Text = "No. >1000"
        Me.ToolTip1.SetToolTip(Me.lblAreaRank, "分区排名")
        '
        'lblAnchorScore
        '
        Me.lblAnchorScore.AutoSize = True
        Me.lblAnchorScore.BackColor = System.Drawing.Color.Transparent
        Me.lblAnchorScore.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblAnchorScore.Location = New System.Drawing.Point(184, 4)
        Me.lblAnchorScore.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lblAnchorScore.Name = "lblAnchorScore"
        Me.lblAnchorScore.Size = New System.Drawing.Size(47, 12)
        Me.lblAnchorScore.TabIndex = 137
        Me.lblAnchorScore.Text = "积分 -1"
        Me.ToolTip1.SetToolTip(Me.lblAnchorScore, "主播积分：主播每收到 100 瓜子获得 1 点积分")
        '
        'lblSan
        '
        Me.lblSan.AutoSize = True
        Me.lblSan.BackColor = System.Drawing.Color.Transparent
        Me.lblSan.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblSan.Location = New System.Drawing.Point(123, 4)
        Me.lblSan.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lblSan.Name = "lblSan"
        Me.lblSan.Size = New System.Drawing.Size(41, 12)
        Me.lblSan.TabIndex = 138
        Me.lblSan.Text = "SAN -1"
        Me.ToolTip1.SetToolTip(Me.lblSan, "主播 SAN 值")
        '
        'lblAttention
        '
        Me.lblAttention.AutoSize = True
        Me.lblAttention.BackColor = System.Drawing.Color.Transparent
        Me.lblAttention.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblAttention.Location = New System.Drawing.Point(62, 4)
        Me.lblAttention.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lblAttention.Name = "lblAttention"
        Me.lblAttention.Size = New System.Drawing.Size(47, 12)
        Me.lblAttention.TabIndex = 15
        Me.lblAttention.Text = "粉丝 -1"
        '
        'tlpLiveRoomInfoRow1
        '
        Me.tlpLiveRoomInfoRow1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.tlpLiveRoomInfoRow1.ColumnCount = 6
        Me.tlpLiveRoomInfoRow1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tlpLiveRoomInfoRow1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tlpLiveRoomInfoRow1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tlpLiveRoomInfoRow1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tlpLiveRoomInfoRow1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60.0!))
        Me.tlpLiveRoomInfoRow1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 407.0!))
        Me.tlpLiveRoomInfoRow1.Controls.Add(Me.lblHourRank, 5, 0)
        Me.tlpLiveRoomInfoRow1.Controls.Add(Me.lblAnchorScore, 3, 0)
        Me.tlpLiveRoomInfoRow1.Controls.Add(Me.lblAreaRank, 4, 0)
        Me.tlpLiveRoomInfoRow1.Controls.Add(Me.lblSan, 2, 0)
        Me.tlpLiveRoomInfoRow1.Controls.Add(Me.lblAttention, 1, 0)
        Me.tlpLiveRoomInfoRow1.Controls.Add(Me.lblOnline, 0, 0)
        Me.tlpLiveRoomInfoRow1.Location = New System.Drawing.Point(12, 26)
        Me.tlpLiveRoomInfoRow1.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpLiveRoomInfoRow1.Name = "tlpLiveRoomInfoRow1"
        Me.tlpLiveRoomInfoRow1.RowCount = 1
        Me.tlpLiveRoomInfoRow1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58.0!))
        Me.tlpLiveRoomInfoRow1.Size = New System.Drawing.Size(373, 22)
        Me.tlpLiveRoomInfoRow1.TabIndex = 16
        '
        'lblHourRank
        '
        Me.lblHourRank.AutoEllipsis = True
        Me.lblHourRank.BackColor = System.Drawing.Color.Transparent
        Me.lblHourRank.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblHourRank.Location = New System.Drawing.Point(306, 4)
        Me.lblHourRank.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lblHourRank.Name = "lblHourRank"
        Me.lblHourRank.Size = New System.Drawing.Size(65, 12)
        Me.lblHourRank.TabIndex = 134
        Me.lblHourRank.Text = "小时榜"
        '
        'chkTopMost
        '
        Me.chkTopMost.AutoSize = True
        Me.chkTopMost.BackColor = System.Drawing.Color.Transparent
        Me.chkTopMost.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkTopMost.ForeColor = System.Drawing.SystemColors.Highlight
        Me.chkTopMost.Location = New System.Drawing.Point(35, 4)
        Me.chkTopMost.Margin = New System.Windows.Forms.Padding(3, 3, 0, 0)
        Me.chkTopMost.Name = "chkTopMost"
        Me.chkTopMost.Size = New System.Drawing.Size(45, 16)
        Me.chkTopMost.TabIndex = 17
        Me.chkTopMost.Text = "置顶"
        Me.chkTopMost.UseVisualStyleBackColor = False
        '
        'tlpLiveRoomInfoRow2
        '
        Me.tlpLiveRoomInfoRow2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.tlpLiveRoomInfoRow2.ColumnCount = 5
        Me.tlpLiveRoomInfoRow2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.tlpLiveRoomInfoRow2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50.0!))
        Me.tlpLiveRoomInfoRow2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 175.0!))
        Me.tlpLiveRoomInfoRow2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.tlpLiveRoomInfoRow2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 433.0!))
        Me.tlpLiveRoomInfoRow2.Controls.Add(Me.lblRoomOwnerNick, 4, 0)
        Me.tlpLiveRoomInfoRow2.Controls.Add(Me.lblJoinRoom, 3, 0)
        Me.tlpLiveRoomInfoRow2.Controls.Add(Me.lblUserNick, 2, 0)
        Me.tlpLiveRoomInfoRow2.Controls.Add(Me.lblLiveStatus, 0, 0)
        Me.tlpLiveRoomInfoRow2.Controls.Add(Me.chkTopMost, 1, 0)
        Me.tlpLiveRoomInfoRow2.Location = New System.Drawing.Point(12, 57)
        Me.tlpLiveRoomInfoRow2.Margin = New System.Windows.Forms.Padding(0)
        Me.tlpLiveRoomInfoRow2.Name = "tlpLiveRoomInfoRow2"
        Me.tlpLiveRoomInfoRow2.RowCount = 1
        Me.tlpLiveRoomInfoRow2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpLiveRoomInfoRow2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.tlpLiveRoomInfoRow2.Size = New System.Drawing.Size(373, 23)
        Me.tlpLiveRoomInfoRow2.TabIndex = 134
        '
        'lblRoomOwnerNick
        '
        Me.lblRoomOwnerNick.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRoomOwnerNick.AutoEllipsis = True
        Me.lblRoomOwnerNick.BackColor = System.Drawing.Color.Transparent
        Me.lblRoomOwnerNick.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblRoomOwnerNick.Location = New System.Drawing.Point(280, 4)
        Me.lblRoomOwnerNick.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lblRoomOwnerNick.Name = "lblRoomOwnerNick"
        Me.lblRoomOwnerNick.Size = New System.Drawing.Size(433, 15)
        Me.lblRoomOwnerNick.TabIndex = 135
        Me.lblRoomOwnerNick.Text = "另一个很帅的人儿将会出现在这里"
        '
        'lblJoinRoom
        '
        Me.lblJoinRoom.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblJoinRoom.AutoEllipsis = True
        Me.lblJoinRoom.AutoSize = True
        Me.lblJoinRoom.BackColor = System.Drawing.Color.Transparent
        Me.lblJoinRoom.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblJoinRoom.Location = New System.Drawing.Point(259, 4)
        Me.lblJoinRoom.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lblJoinRoom.Name = "lblJoinRoom"
        Me.lblJoinRoom.Size = New System.Drawing.Size(20, 12)
        Me.lblJoinRoom.TabIndex = 136
        Me.lblJoinRoom.Text = "→"
        '
        'lblUserNick
        '
        Me.lblUserNick.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUserNick.AutoEllipsis = True
        Me.lblUserNick.AutoSize = True
        Me.lblUserNick.BackColor = System.Drawing.Color.Transparent
        Me.lblUserNick.ForeColor = System.Drawing.SystemColors.Highlight
        Me.lblUserNick.Location = New System.Drawing.Point(83, 4)
        Me.lblUserNick.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lblUserNick.Name = "lblUserNick"
        Me.lblUserNick.Size = New System.Drawing.Size(175, 12)
        Me.lblUserNick.TabIndex = 0
        Me.lblUserNick.Text = "一个很帅的人儿将会出现在这里"
        '
        'LiveRoomControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.tlpLiveRoomInfoRow2)
        Me.Controls.Add(Me.tlpLiveRoomInfoRow1)
        Me.Name = "LiveRoomControl"
        Me.Size = New System.Drawing.Size(409, 111)
        Me.tlpLiveRoomInfoRow1.ResumeLayout(False)
        Me.tlpLiveRoomInfoRow1.PerformLayout()
        Me.tlpLiveRoomInfoRow2.ResumeLayout(False)
        Me.tlpLiveRoomInfoRow2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblOnline As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents lblAttention As Label
    Friend WithEvents tlpLiveRoomInfoRow1 As TableLayoutPanel
    Friend WithEvents chkTopMost As CheckBox
    Friend WithEvents lblLiveStatus As Label
    Friend WithEvents lblHourRank As Label
    Friend WithEvents tlpLiveRoomInfoRow2 As TableLayoutPanel
    Friend WithEvents lblUserNick As Label
    Friend WithEvents lblAreaRank As Label
    Friend WithEvents lblAnchorScore As Label
    Friend WithEvents lblSan As Label
    Friend WithEvents lblRoomOwnerNick As Label
    Friend WithEvents lblJoinRoom As Label
End Class
