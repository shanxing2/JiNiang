
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DanmuControl
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DanmuControl))
        Me.btnSendDanmu = New System.Windows.Forms.Button()
        Me.lblDanmuLength = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbDanmuInput = New System.Windows.Forms.ComboBox()
        Me.picHotWord = New System.Windows.Forms.PictureBox()
        Me.picYanText = New System.Windows.Forms.PictureBox()
        Me.picDanmuColor = New System.Windows.Forms.PictureBox()
        Me.lblSendDanmuStatus = New System.Windows.Forms.Label()
        Me.lblBorderBottom1 = New System.Windows.Forms.Label()
        Me.webChatHistory = New System.Windows.Forms.WebBrowser()
        Me.cmsBrowserRightButtonClick = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsmitCopyInBrowser = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblBorderLeft = New System.Windows.Forms.Label()
        Me.btnSendTest = New System.Windows.Forms.Button()
        Me.lblBorderBottom2 = New System.Windows.Forms.Label()
        Me.lblBorderRight = New System.Windows.Forms.Label()
        Me.cmsUserNickClick = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnitmNickAndRemark = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmUseUseNickAsReMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmSettingReMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmDeleteReMark = New System.Windows.Forms.ToolStripMenuItem()
        Me.复制ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmCopyViewerId = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmCopyViewerNick = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmGotoUserZone = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmRoomShieldViewer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmShieldThisBadGuy = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmUnShieldThisBadGuy = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmRoomShieldViewerManage = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmReportThisDanmu = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmBlackList = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmAddToBlackList1Hour = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmAddToBlackList8Hour = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmAddToBlackList12Hour = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmAddToBlackList24Hour = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmRemoveFromBlackList = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmBlackListManage = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmAdmin = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmAdminAppoint = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmAdminUnAppoint = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnitmAdminManage = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlSendComponentContainer = New System.Windows.Forms.Panel()
        CType(Me.picHotWord, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picYanText, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picDanmuColor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.cmsBrowserRightButtonClick.SuspendLayout()
        Me.cmsUserNickClick.SuspendLayout()
        Me.pnlSendComponentContainer.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSendDanmu
        '
        Me.btnSendDanmu.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSendDanmu.Location = New System.Drawing.Point(0, 25)
        Me.btnSendDanmu.Name = "btnSendDanmu"
        Me.btnSendDanmu.Size = New System.Drawing.Size(48, 23)
        Me.btnSendDanmu.TabIndex = 1
        Me.btnSendDanmu.Text = "发送"
        Me.btnSendDanmu.UseVisualStyleBackColor = True
        '
        'lblDanmuLength
        '
        Me.lblDanmuLength.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDanmuLength.AutoSize = True
        Me.lblDanmuLength.Font = New System.Drawing.Font("宋体", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblDanmuLength.ForeColor = System.Drawing.Color.LimeGreen
        Me.lblDanmuLength.Location = New System.Drawing.Point(94, 6)
        Me.lblDanmuLength.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.lblDanmuLength.Name = "lblDanmuLength"
        Me.lblDanmuLength.Size = New System.Drawing.Size(35, 12)
        Me.lblDanmuLength.TabIndex = 10
        Me.lblDanmuLength.Text = " 0/20"
        '
        'cmbDanmuInput
        '
        Me.cmbDanmuInput.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.cmbDanmuInput.FormattingEnabled = True
        Me.cmbDanmuInput.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cmbDanmuInput.Location = New System.Drawing.Point(0, 3)
        Me.cmbDanmuInput.Name = "cmbDanmuInput"
        Me.cmbDanmuInput.Size = New System.Drawing.Size(91, 20)
        Me.cmbDanmuInput.TabIndex = 121
        Me.ToolTip1.SetToolTip(Me.cmbDanmuInput, "按Enter键发送，超长后自动分条发送")
        '
        'picHotWord
        '
        Me.picHotWord.BackColor = System.Drawing.SystemColors.Control
        Me.picHotWord.Image = CType(resources.GetObject("picHotWord.Image"), System.Drawing.Image)
        Me.picHotWord.Location = New System.Drawing.Point(141, 25)
        Me.picHotWord.Name = "picHotWord"
        Me.picHotWord.Size = New System.Drawing.Size(21, 21)
        Me.picHotWord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picHotWord.TabIndex = 123
        Me.picHotWord.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picHotWord, "热词面板")
        Me.picHotWord.Visible = False
        '
        'picYanText
        '
        Me.picYanText.BackColor = System.Drawing.Color.Transparent
        Me.picYanText.Image = CType(resources.GetObject("picYanText.Image"), System.Drawing.Image)
        Me.picYanText.Location = New System.Drawing.Point(240, 26)
        Me.picYanText.Name = "picYanText"
        Me.picYanText.Size = New System.Drawing.Size(21, 21)
        Me.picYanText.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picYanText.TabIndex = 122
        Me.picYanText.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picYanText, "颜文字面板")
        '
        'picDanmuColor
        '
        Me.picDanmuColor.BackColor = System.Drawing.SystemColors.Control
        Me.picDanmuColor.Image = CType(resources.GetObject("picDanmuColor.Image"), System.Drawing.Image)
        Me.picDanmuColor.Location = New System.Drawing.Point(267, 26)
        Me.picDanmuColor.Name = "picDanmuColor"
        Me.picDanmuColor.Size = New System.Drawing.Size(21, 21)
        Me.picDanmuColor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picDanmuColor.TabIndex = 124
        Me.picDanmuColor.TabStop = False
        Me.ToolTip1.SetToolTip(Me.picDanmuColor, "弹幕颜色设置")
        '
        'lblSendDanmuStatus
        '
        Me.lblSendDanmuStatus.Font = New System.Drawing.Font("宋体", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        Me.lblSendDanmuStatus.ForeColor = System.Drawing.Color.LimeGreen
        Me.lblSendDanmuStatus.Location = New System.Drawing.Point(81, 26)
        Me.lblSendDanmuStatus.Margin = New System.Windows.Forms.Padding(0)
        Me.lblSendDanmuStatus.Name = "lblSendDanmuStatus"
        Me.lblSendDanmuStatus.Size = New System.Drawing.Size(30, 21)
        Me.lblSendDanmuStatus.TabIndex = 119
        Me.lblSendDanmuStatus.Text = "●"
        '
        'lblBorderBottom1
        '
        Me.lblBorderBottom1.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.lblBorderBottom1.Location = New System.Drawing.Point(132, 5)
        Me.lblBorderBottom1.Name = "lblBorderBottom1"
        Me.lblBorderBottom1.Size = New System.Drawing.Size(30, 1)
        Me.lblBorderBottom1.TabIndex = 136
        '
        'webChatHistory
        '
        Me.webChatHistory.ContextMenuStrip = Me.cmsBrowserRightButtonClick
        Me.webChatHistory.Location = New System.Drawing.Point(27, 8)
        Me.webChatHistory.Margin = New System.Windows.Forms.Padding(0)
        Me.webChatHistory.MinimumSize = New System.Drawing.Size(20, 20)
        Me.webChatHistory.Name = "webChatHistory"
        Me.webChatHistory.Size = New System.Drawing.Size(105, 135)
        Me.webChatHistory.TabIndex = 134
        Me.webChatHistory.WebBrowserShortcutsEnabled = False
        '
        'cmsBrowserRightButtonClick
        '
        Me.cmsBrowserRightButtonClick.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmitCopyInBrowser})
        Me.cmsBrowserRightButtonClick.Name = "cmsBrowserRightButtonClick"
        Me.cmsBrowserRightButtonClick.Size = New System.Drawing.Size(101, 26)
        '
        'tsmitCopyInBrowser
        '
        Me.tsmitCopyInBrowser.Name = "tsmitCopyInBrowser"
        Me.tsmitCopyInBrowser.Size = New System.Drawing.Size(100, 22)
        Me.tsmitCopyInBrowser.Text = "复制"
        '
        'lblBorderLeft
        '
        Me.lblBorderLeft.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.lblBorderLeft.Location = New System.Drawing.Point(12, 91)
        Me.lblBorderLeft.Name = "lblBorderLeft"
        Me.lblBorderLeft.Size = New System.Drawing.Size(1, 78)
        Me.lblBorderLeft.TabIndex = 135
        '
        'btnSendTest
        '
        Me.btnSendTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSendTest.Location = New System.Drawing.Point(168, 24)
        Me.btnSendTest.Name = "btnSendTest"
        Me.btnSendTest.Size = New System.Drawing.Size(66, 23)
        Me.btnSendTest.TabIndex = 137
        Me.btnSendTest.Text = "SendTest"
        Me.btnSendTest.UseVisualStyleBackColor = True
        '
        'lblBorderBottom2
        '
        Me.lblBorderBottom2.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.lblBorderBottom2.Location = New System.Drawing.Point(132, 21)
        Me.lblBorderBottom2.Name = "lblBorderBottom2"
        Me.lblBorderBottom2.Size = New System.Drawing.Size(30, 1)
        Me.lblBorderBottom2.TabIndex = 138
        '
        'lblBorderRight
        '
        Me.lblBorderRight.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.lblBorderRight.Location = New System.Drawing.Point(142, 92)
        Me.lblBorderRight.Name = "lblBorderRight"
        Me.lblBorderRight.Size = New System.Drawing.Size(1, 78)
        Me.lblBorderRight.TabIndex = 139
        '
        'cmsUserNickClick
        '
        Me.cmsUserNickClick.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnitmNickAndRemark, Me.复制ToolStripMenuItem, Me.mnitmGotoUserZone, Me.mnitmRoomShieldViewer, Me.mnitmReportThisDanmu, Me.mnitmBlackList, Me.mnitmAdmin})
        Me.cmsUserNickClick.Name = "ContextMenuStrip1"
        Me.cmsUserNickClick.Size = New System.Drawing.Size(185, 158)
        '
        'mnitmNickAndRemark
        '
        Me.mnitmNickAndRemark.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnitmUseUseNickAsReMark, Me.mnitmSettingReMark, Me.mnitmDeleteReMark})
        Me.mnitmNickAndRemark.Name = "mnitmNickAndRemark"
        Me.mnitmNickAndRemark.Size = New System.Drawing.Size(184, 22)
        Me.mnitmNickAndRemark.Text = "昵称（未设置备注）"
        '
        'mnitmUseUseNickAsReMark
        '
        Me.mnitmUseUseNickAsReMark.Name = "mnitmUseUseNickAsReMark"
        Me.mnitmUseUseNickAsReMark.Size = New System.Drawing.Size(172, 22)
        Me.mnitmUseUseNickAsReMark.Text = "使用昵称作为备注"
        '
        'mnitmSettingReMark
        '
        Me.mnitmSettingReMark.Name = "mnitmSettingReMark"
        Me.mnitmSettingReMark.Size = New System.Drawing.Size(172, 22)
        Me.mnitmSettingReMark.Text = "设置备注"
        '
        'mnitmDeleteReMark
        '
        Me.mnitmDeleteReMark.Name = "mnitmDeleteReMark"
        Me.mnitmDeleteReMark.Size = New System.Drawing.Size(172, 22)
        Me.mnitmDeleteReMark.Text = "删除备注"
        '
        '复制ToolStripMenuItem
        '
        Me.复制ToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnitmCopyViewerId, Me.mnitmCopyViewerNick})
        Me.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem"
        Me.复制ToolStripMenuItem.Size = New System.Drawing.Size(184, 22)
        Me.复制ToolStripMenuItem.Text = "复制"
        '
        'mnitmCopyViewerId
        '
        Me.mnitmCopyViewerId.Name = "mnitmCopyViewerId"
        Me.mnitmCopyViewerId.Size = New System.Drawing.Size(113, 22)
        Me.mnitmCopyViewerId.Text = "用户ID"
        '
        'mnitmCopyViewerNick
        '
        Me.mnitmCopyViewerNick.Name = "mnitmCopyViewerNick"
        Me.mnitmCopyViewerNick.Size = New System.Drawing.Size(113, 22)
        Me.mnitmCopyViewerNick.Text = "昵称"
        '
        'mnitmGotoUserZone
        '
        Me.mnitmGotoUserZone.Name = "mnitmGotoUserZone"
        Me.mnitmGotoUserZone.Size = New System.Drawing.Size(184, 22)
        Me.mnitmGotoUserZone.Text = "去 Ta 的个人空间"
        '
        'mnitmRoomShieldViewer
        '
        Me.mnitmRoomShieldViewer.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnitmShieldThisBadGuy, Me.mnitmUnShieldThisBadGuy, Me.mnitmRoomShieldViewerManage})
        Me.mnitmRoomShieldViewer.Name = "mnitmRoomShieldViewer"
        Me.mnitmRoomShieldViewer.Size = New System.Drawing.Size(184, 22)
        Me.mnitmRoomShieldViewer.Text = "屏蔽"
        '
        'mnitmShieldThisBadGuy
        '
        Me.mnitmShieldThisBadGuy.Name = "mnitmShieldThisBadGuy"
        Me.mnitmShieldThisBadGuy.Size = New System.Drawing.Size(148, 22)
        Me.mnitmShieldThisBadGuy.Text = "屏蔽这个坏银"
        '
        'mnitmUnShieldThisBadGuy
        '
        Me.mnitmUnShieldThisBadGuy.Name = "mnitmUnShieldThisBadGuy"
        Me.mnitmUnShieldThisBadGuy.Size = New System.Drawing.Size(148, 22)
        Me.mnitmUnShieldThisBadGuy.Text = "解除屏蔽"
        '
        'mnitmRoomShieldViewerManage
        '
        Me.mnitmRoomShieldViewerManage.Name = "mnitmRoomShieldViewerManage"
        Me.mnitmRoomShieldViewerManage.Size = New System.Drawing.Size(148, 22)
        Me.mnitmRoomShieldViewerManage.Text = "管理"
        '
        'mnitmReportThisDanmu
        '
        Me.mnitmReportThisDanmu.Name = "mnitmReportThisDanmu"
        Me.mnitmReportThisDanmu.Size = New System.Drawing.Size(184, 22)
        Me.mnitmReportThisDanmu.Text = "举报这条弹幕"
        '
        'mnitmBlackList
        '
        Me.mnitmBlackList.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnitmAddToBlackList1Hour, Me.mnitmAddToBlackList8Hour, Me.mnitmAddToBlackList12Hour, Me.mnitmAddToBlackList24Hour, Me.mnitmRemoveFromBlackList, Me.mnitmBlackListManage})
        Me.mnitmBlackList.Name = "mnitmBlackList"
        Me.mnitmBlackList.Size = New System.Drawing.Size(184, 22)
        Me.mnitmBlackList.Text = "禁言套餐销售部"
        '
        'mnitmAddToBlackList1Hour
        '
        Me.mnitmAddToBlackList1Hour.Name = "mnitmAddToBlackList1Hour"
        Me.mnitmAddToBlackList1Hour.Size = New System.Drawing.Size(182, 22)
        Me.mnitmAddToBlackList1Hour.Text = "1小时"
        '
        'mnitmAddToBlackList8Hour
        '
        Me.mnitmAddToBlackList8Hour.Name = "mnitmAddToBlackList8Hour"
        Me.mnitmAddToBlackList8Hour.Size = New System.Drawing.Size(182, 22)
        Me.mnitmAddToBlackList8Hour.Text = "8小时"
        '
        'mnitmAddToBlackList12Hour
        '
        Me.mnitmAddToBlackList12Hour.Name = "mnitmAddToBlackList12Hour"
        Me.mnitmAddToBlackList12Hour.Size = New System.Drawing.Size(182, 22)
        Me.mnitmAddToBlackList12Hour.Text = "12小时"
        '
        'mnitmAddToBlackList24Hour
        '
        Me.mnitmAddToBlackList24Hour.Name = "mnitmAddToBlackList24Hour"
        Me.mnitmAddToBlackList24Hour.Size = New System.Drawing.Size(182, 22)
        Me.mnitmAddToBlackList24Hour.Text = "24小时"
        '
        'mnitmRemoveFromBlackList
        '
        Me.mnitmRemoveFromBlackList.Name = "mnitmRemoveFromBlackList"
        Me.mnitmRemoveFromBlackList.Size = New System.Drawing.Size(182, 22)
        Me.mnitmRemoveFromBlackList.Text = "放 Ta 出来晒晒太阳"
        '
        'mnitmBlackListManage
        '
        Me.mnitmBlackListManage.Name = "mnitmBlackListManage"
        Me.mnitmBlackListManage.Size = New System.Drawing.Size(182, 22)
        Me.mnitmBlackListManage.Text = "管理"
        '
        'mnitmAdmin
        '
        Me.mnitmAdmin.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnitmAdminAppoint, Me.mnitmAdminUnAppoint, Me.mnitmAdminManage})
        Me.mnitmAdmin.Name = "mnitmAdmin"
        Me.mnitmAdmin.Size = New System.Drawing.Size(184, 22)
        Me.mnitmAdmin.Text = "管理员"
        '
        'mnitmAdminAppoint
        '
        Me.mnitmAdminAppoint.Name = "mnitmAdminAppoint"
        Me.mnitmAdminAppoint.Size = New System.Drawing.Size(100, 22)
        Me.mnitmAdminAppoint.Text = "任命"
        '
        'mnitmAdminUnAppoint
        '
        Me.mnitmAdminUnAppoint.Name = "mnitmAdminUnAppoint"
        Me.mnitmAdminUnAppoint.Size = New System.Drawing.Size(100, 22)
        Me.mnitmAdminUnAppoint.Text = "撤销"
        '
        'mnitmAdminManage
        '
        Me.mnitmAdminManage.Name = "mnitmAdminManage"
        Me.mnitmAdminManage.Size = New System.Drawing.Size(100, 22)
        Me.mnitmAdminManage.Text = "管理"
        '
        'pnlSendComponentContainer
        '
        Me.pnlSendComponentContainer.BackColor = System.Drawing.SystemColors.Control
        Me.pnlSendComponentContainer.Controls.Add(Me.cmbDanmuInput)
        Me.pnlSendComponentContainer.Controls.Add(Me.btnSendDanmu)
        Me.pnlSendComponentContainer.Controls.Add(Me.picDanmuColor)
        Me.pnlSendComponentContainer.Controls.Add(Me.btnSendTest)
        Me.pnlSendComponentContainer.Controls.Add(Me.lblDanmuLength)
        Me.pnlSendComponentContainer.Controls.Add(Me.picHotWord)
        Me.pnlSendComponentContainer.Controls.Add(Me.lblBorderBottom2)
        Me.pnlSendComponentContainer.Controls.Add(Me.lblBorderBottom1)
        Me.pnlSendComponentContainer.Controls.Add(Me.lblSendDanmuStatus)
        Me.pnlSendComponentContainer.Controls.Add(Me.picYanText)
        Me.pnlSendComponentContainer.Location = New System.Drawing.Point(135, 8)
        Me.pnlSendComponentContainer.Name = "pnlSendComponentContainer"
        Me.pnlSendComponentContainer.Size = New System.Drawing.Size(290, 54)
        Me.pnlSendComponentContainer.TabIndex = 140
        '
        'DanmuControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.Controls.Add(Me.webChatHistory)
        Me.Controls.Add(Me.pnlSendComponentContainer)
        Me.Controls.Add(Me.lblBorderLeft)
        Me.Controls.Add(Me.lblBorderRight)
        Me.Name = "DanmuControl"
        Me.Size = New System.Drawing.Size(428, 221)
        CType(Me.picHotWord, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picYanText, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picDanmuColor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.cmsBrowserRightButtonClick.ResumeLayout(False)
        Me.cmsUserNickClick.ResumeLayout(False)
        Me.pnlSendComponentContainer.ResumeLayout(False)
        Me.pnlSendComponentContainer.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSendDanmu As Button
    Friend WithEvents lblDanmuLength As Label
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents lblSendDanmuStatus As Label
    Friend WithEvents cmbDanmuInput As ComboBox
    Friend WithEvents picDanmuColor As PictureBox
    Friend WithEvents picHotWord As PictureBox
    Friend WithEvents picYanText As PictureBox
    Friend WithEvents lblBorderBottom1 As Label
    Friend WithEvents webChatHistory As WebBrowser
    Friend WithEvents lblBorderLeft As Label
    Friend WithEvents btnSendTest As Button
    Friend WithEvents lblBorderBottom2 As Label
    Friend WithEvents lblBorderRight As Label
    Friend WithEvents cmsBrowserRightButtonClick As ContextMenuStrip
    Friend WithEvents tsmitCopyInBrowser As ToolStripMenuItem
    Friend WithEvents cmsUserNickClick As ContextMenuStrip
    Friend WithEvents mnitmNickAndRemark As ToolStripMenuItem
    Friend WithEvents mnitmGotoUserZone As ToolStripMenuItem
    Friend WithEvents mnitmReportThisDanmu As ToolStripMenuItem
    Friend WithEvents mnitmBlackList As ToolStripMenuItem
    Friend WithEvents mnitmAddToBlackList1Hour As ToolStripMenuItem
    Friend WithEvents mnitmRemoveFromBlackList As ToolStripMenuItem
    Friend WithEvents mnitmSettingReMark As ToolStripMenuItem
    Friend WithEvents mnitmBlackListManage As ToolStripMenuItem
    Friend WithEvents mnitmAdmin As ToolStripMenuItem
    Friend WithEvents mnitmAdminAppoint As ToolStripMenuItem
    Friend WithEvents mnitmAdminUnAppoint As ToolStripMenuItem
    Friend WithEvents mnitmAdminManage As ToolStripMenuItem
    Friend WithEvents mnitmDeleteReMark As ToolStripMenuItem
    Friend WithEvents mnitmAddToBlackList8Hour As ToolStripMenuItem
    Friend WithEvents mnitmAddToBlackList12Hour As ToolStripMenuItem
    Friend WithEvents mnitmAddToBlackList24Hour As ToolStripMenuItem
    Friend WithEvents 复制ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnitmCopyViewerId As ToolStripMenuItem
    Friend WithEvents mnitmCopyViewerNick As ToolStripMenuItem
    Friend WithEvents mnitmRoomShieldViewer As ToolStripMenuItem
    Friend WithEvents mnitmUnShieldThisBadGuy As ToolStripMenuItem
    Friend WithEvents mnitmShieldThisBadGuy As ToolStripMenuItem
    Friend WithEvents mnitmRoomShieldViewerManage As ToolStripMenuItem
    Friend WithEvents mnitmUseUseNickAsReMark As ToolStripMenuItem
    Friend WithEvents pnlSendComponentContainer As Panel
End Class


