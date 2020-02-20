Imports System.ComponentModel
Imports ShanXingTech

Public Class FrmRoomViewerManage
#Region "枚举区"
    Enum ManageMode
        ''' <summary>
        ''' 房管设置
        ''' </summary>
        <Description("房管设置")>
        RoomAdminSetting
        ''' <summary>
        ''' 黑名单设置
        ''' </summary>
        <Description("黑名单设置")>
        BlackViewerSetting
        ''' <summary>
        ''' 备注设置
        ''' </summary>
        <Description("备注设置")>
        RemarkSetting
        ''' <summary>
        ''' 屏蔽设置
        ''' </summary>
        <Description("屏蔽设置")>
        ShieldSetting
    End Enum
#End Region

#Region "字段区"

    Private m_RoomAdminControl As RoomAdminControl
    Private m_BlackViewerControl As BlackViewerControl
	Private m_RemarkControl As ViewerRemarkControl
	Private m_RoomShieldControl As RoomShieldControl
	''' <summary>
	''' 当前用户是否为当前直播间的播主
	''' </summary>
	Private ReadOnly m_IsUper As Boolean
	''' <summary>
	''' 当前用户是否有管理员权限
	''' </summary>
	Private ReadOnly m_IsAdmin As Boolean
	''' <summary>
	''' 当前观看的直播间Id
	''' </summary>
	Private ReadOnly m_ViewRoomId As String
#End Region

#Region "常量区"
	Private ReadOnly m_TabPages As String()
#End Region

#Region "属性区"
	''' <summary>
	''' 当前的设置模式
	''' </summary>
	''' <returns></returns>
	Public Property CurrentManageMode As ManageMode
	''' <summary>
	''' 被选中的观众Id
	''' </summary>
	''' <returns></returns>
	Public Property ViewerId As String
	''' <summary>
	''' 被选中的观众Id或者观众昵称
	''' </summary>
	''' <returns></returns>
	Public Property ViewerIdOrViewerName As String
#End Region


#Region "构造函数"
	''' <summary>
	''' 请勿调用无参数构造函数(也不能去掉)
	''' </summary>
	Sub New()

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。
	End Sub

	''' <summary>
	''' 
	''' </summary>
	''' <param name="isUper">当前用户是否为当前直播间的播主</param>
	''' <param name="isAdmin">当前用户是否有管理员权限</param>
	''' <param name="viewRoomId">当前观看的直播间Id</param>
	''' <param name="selectedViewerId">观众ID</param>
	''' <param name="selectedViewerIdOrViewerName">观众Id或者观众昵称</param>
	''' <param name="manageMode"></param>
	Sub New(ByVal isUper As Boolean， ByVal isAdmin As Boolean， ByVal viewRoomId As String， ByVal selectedViewerId As String, ByVal selectedViewerIdOrViewerName As String, ByVal manageMode As ManageMode)

		' 此调用是设计器所必需的。
		InitializeComponent()

		Me.m_IsAdmin = isAdmin
		Me.m_IsUper = isUper
		Me.m_ViewRoomId = viewRoomId
		Me.ViewerId = selectedViewerId
		Me.ViewerIdOrViewerName = selectedViewerIdOrViewerName
		CurrentManageMode = manageMode
		m_TabPages = {
			ManageMode.RoomAdminSetting.GetDescription,
			ManageMode.BlackViewerSetting.GetDescription,
			ManageMode.RemarkSetting.GetDescription,
			ManageMode.ShieldSetting.GetDescription
		}
		' 在 InitializeComponent() 调用之后添加任何初始化。
		Me.SuspendLayout()

		tlpManageTab.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
		tlpControlContainer.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
		ShowManageTab()

		Me.ResumeLayout()
	End Sub
#End Region

#Region "函数区"

	Private Sub FrmRoomViewerManage_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
		' 非软件退出时只隐藏，不关闭窗体以便重用
		If e.CloseReason = CloseReason.UserClosing Then
			Me.Hide()
			e.Cancel = True
		End If
	End Sub

	Private Sub ShowManageTab()
		If tlpManageTab.Controls.Count > 0 Then tlpManageTab.Controls.Clear()
		tlpManageTab.ColumnCount = m_TabPages.Length
		Dim colPercent = CSng(100 / tlpManageTab.ColumnCount)
		If tlpManageTab.ColumnStyles.Count > 0 Then tlpManageTab.ColumnStyles.Clear()
		For i = 0 To tlpManageTab.ColumnCount - 1
			tlpManageTab.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, colPercent))
		Next

		Dim labelWidth = tlpManageTab.Width \ tlpManageTab.ColumnCount
		Dim lblArr As New List(Of Label)
		For Each pnl In m_TabPages
			Dim lbl = New Label With {
					.AutoSize = False,
					.Width = labelWidth,
					.Text = pnl,
					.Dock = DockStyle.Fill,
					.FlatStyle = FlatStyle.Flat,
					.BorderStyle = BorderStyle.FixedSingle,
					.TextAlign = ContentAlignment.MiddleCenter,
					.Margin = New Padding(0, 0, 0, 0),
					.Name = pnl
				}
			lblArr.Add(lbl)
			'tlpManageTab.Controls.Add(lbl)

			AddHandler lbl.MouseMove,
				Sub(sender2, e2)
					lbl.Cursor = Cursors.Hand
				End Sub

			AddHandler lbl.MouseLeave,
				Sub(sender2, e2)
					lbl.Cursor = Cursors.Default
				End Sub

			AddHandler lbl.Click, AddressOf lbl_Click
		Next
		tlpManageTab.Controls.AddRange(lblArr.ToArray)
	End Sub

	Private Sub lbl_Click(sender As Object, e As EventArgs)
		Dim lbl = DirectCast(sender, Label)
		If lbl Is Nothing Then Return

		Try
			lbl.Enabled = False

			Select Case lbl.Text
				Case ManageMode.RoomAdminSetting.GetDescription
					' 房管设置
					CurrentManageMode = ManageMode.RoomAdminSetting
					m_RoomAdminControl = If(m_RoomAdminControl, New RoomAdminControl(m_IsUper, m_ViewRoomId))
					'm_RoomAdminControl = New RoomAdminControl(m_IsUper, m_ViewRoomId)
					m_RoomAdminControl.txtUidOrUname.Text = ViewerIdOrViewerName

					AddControl(lbl, m_RoomAdminControl)
				Case ManageMode.BlackViewerSetting.GetDescription
					If Not m_IsUper AndAlso Not m_IsAdmin Then
						Windows2.DrawTipsTask(Me, "您无此直播间管理权限" & RandomEmoji.Helpless, 1000, False, False)
						Exit Try
					End If
					' 黑名单设置
					CurrentManageMode = ManageMode.BlackViewerSetting
					m_BlackViewerControl = If(m_BlackViewerControl, New BlackViewerControl(m_IsUper, m_IsAdmin))
					'm_BlackViewerControl = New BlackViewerControl

					m_BlackViewerControl.txtUidOrUname.Text = ViewerIdOrViewerName
					m_BlackViewerControl.cmbBlockHourItems.Text = String.Empty

					AddControl(lbl, m_BlackViewerControl)
				Case ManageMode.RemarkSetting.GetDescription
					' 备注设置
					CurrentManageMode = ManageMode.RemarkSetting
					m_RemarkControl = If(m_RemarkControl, New ViewerRemarkControl())
					'm_RemarkControl = New ViewerRemarkControl()
					m_RemarkControl.ViewerId = ViewerId
					m_RemarkControl.ViewerIdOrViewerName = ViewerIdOrViewerName

					AddControl(lbl, m_RemarkControl)
				Case ManageMode.ShieldSetting.GetDescription
					' 屏蔽设置
					CurrentManageMode = ManageMode.ShieldSetting
					m_RoomShieldControl = If(m_RoomShieldControl, New RoomShieldControl(m_IsUper, m_IsAdmin))
					m_RoomShieldControl.txtShieldViewer.Text = ViewerIdOrViewerName

					AddControl(lbl, m_RoomShieldControl)
			End Select
		Finally
            lbl.Enabled = True
        End Try
    End Sub

    Private Sub AddControl(ByVal lbl As Label, ByVal control As UserControl)
        ' 不重复加载相同控件
        If tlpControlContainer.Controls.Count > 0 AndAlso
            tlpControlContainer.Controls(0).Name = control.Name Then
            Return
        Else
            For Each ctl In lbl.Parent.Controls
                ctl.ForeColor = Color.Black
            Next
            lbl.ForeColor = SystemColors.Highlight

            Me.Text = "房间" & lbl.Text
            If tlpControlContainer.Controls.Count > 0 Then
                tlpControlContainer.Controls.RemoveAt(0)
            End If
            tlpControlContainer.Controls.Add(control)
        End If
    End Sub


    Private Sub FrmRoomViewerManage_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Not Me.Visible Then Return
        ShowAndActivate()
    End Sub

	''' <summary>
	''' 显示窗体并激活成当前可见窗体（具有焦点）
	''' </summary>
	Public Sub ShowAndActivate()
		If Me.Visible = True Then
			Dim lbl = TryCast(tlpManageTab.Controls.Item(CurrentManageMode.GetDescription), Label)
			If lbl Is Nothing Then Return

			lbl_Click(lbl, New EventArgs)

			' 当窗口是最小化的时恢复正常显示
			If Me.WindowState = FormWindowState.Minimized Then
				Me.WindowState = FormWindowState.Normal
			End If
		Else
			MyBase.Show()
		End If

		MyBase.Activate()
	End Sub

	''' <summary>
	''' 删除备注
	''' </summary>
	''' <param name="userId"></param>
	Public Sub OnRemarkDeleted(ByVal userId As String)
		m_RemarkControl.OnRemarkDelete(userId)
	End Sub

	''' <summary>
	''' 添加备注后调用
	''' </summary>
	Public Sub OnRemarkAdd()
		m_RemarkControl.OnRemarkAdd()
	End Sub
#End Region


End Class