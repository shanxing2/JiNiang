Imports System.Text.RegularExpressions
Imports ShanXingTech
Imports ShanXingTech.Text2

Public Class RoomShieldControl
	Inherits ViewerManageBaseControl

#Region "字段区"
	Private m_ShieldType As BilibiliApi.ShieldType
	Private m_ShieldLevel As Integer
	Private m_ShieldStatus As BilibiliApi.ShieldOperate
	Private m_ShieldInfo As RoomShieldInfoEntity.Data

	Private m_ShieldMode As ShieldMode
	Private m_NeedRebind As Boolean
	Private m_ShieldKeywordCheckedListBoxColumnWidth As Integer
	Private m_ShieldViewerCheckedListBoxColumnWidth As Integer
	Private ReadOnly m_IsAdmin As Boolean
	Private ReadOnly m_IsUper As Boolean
	Private m_RoomGlobalShieldPanel As Panel
	Private m_UserAnchorShieldControlOriginalHeight As Integer
#End Region

#Region "枚举区"
	Private Enum ShieldMode
		Keyword
		Viewer
	End Enum
#End Region

#Region "构造函数"
	Sub New(ByVal isUper As Boolean, ByVal isAdmin As Boolean)

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。

		m_ShieldMode = ShieldMode.Keyword
		m_IsUper = isUper
		m_IsAdmin = isAdmin

		m_ShieldType = BilibiliApi.ShieldType.None
		m_ShieldStatus = BilibiliApi.ShieldOperate.Off

		Me.SuspendLayout()
		trbShieldLevel.Minimum = 0
		trbShieldLevel.Maximum = 60
		nudShieldLevel.Minimum = trbShieldLevel.Minimum
		nudShieldLevel.Maximum = trbShieldLevel.Maximum

		txtShieldKeyword.MaxLength = 30
		txtShieldKeyword.SetCueBanner("请输入您要屏蔽的内容，按 Enter键 快捷添加")
		txtShieldKeyword.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
		txtShieldViewer.MaxLength = 15
		txtShieldViewer.SetCueBanner("请输入您要屏蔽的用户Id或者昵称，按 Enter键 快捷添加")
		txtShieldViewer.Anchor = txtShieldKeyword.Anchor
		' 用户名最大宽度15个字符，关键词30个字符
		' 注：因为是从Single转到Integer, 有可能会丢失小数部分而导致不能完全显示15个字符， 所以需要代入16
		m_ShieldKeywordCheckedListBoxColumnWidth = CInt(Me.CreateGraphics.MeasureString(New String("的"c, txtShieldKeyword.MaxLength + 1), chklstShieldKeyword.Font).Width)
		m_ShieldViewerCheckedListBoxColumnWidth = CInt(Me.CreateGraphics.MeasureString(New String("的"c, txtShieldViewer.MaxLength + 1), chklstShieldViewer.Font).Width)
		btnAddShieldKeyword.Anchor = AnchorStyles.Top Or AnchorStyles.Right
		btnAddShieldViewer.Anchor = btnAddShieldKeyword.Anchor
		tlpShieldModeLabel.Anchor = txtShieldKeyword.Anchor

		lblShieldKeyword.BorderStyle = BorderStyle.FixedSingle
		lblShieldKeyword.AutoSize = False
		lblShieldKeyword.Dock = DockStyle.Fill
		lblShieldKeyword.Margin = New Padding(0)
		lblShieldKeyword.TextAlign = ContentAlignment.MiddleCenter
		lblShieldKeyword.ForeColor = SystemColors.Highlight

		lblShieldViewer.BorderStyle = lblShieldKeyword.BorderStyle
		lblShieldViewer.AutoSize = lblShieldKeyword.AutoSize
		lblShieldViewer.Dock = lblShieldKeyword.Dock
		lblShieldViewer.Margin = lblShieldKeyword.Margin
		lblShieldViewer.TextAlign = lblShieldKeyword.TextAlign

		chklstShieldKeyword.HorizontalScrollbar = False
		chklstShieldKeyword.MultiColumn = True
		chklstShieldKeyword.ColumnWidth = m_ShieldKeywordCheckedListBoxColumnWidth
		chklstShieldKeyword.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
		chklstShieldKeyword.CheckOnClick = True
		chklstShieldKeyword.Width = tlpShieldModeLabel.Width

		chklstShieldViewer.HorizontalScrollbar = chklstShieldKeyword.HorizontalScrollbar
		chklstShieldViewer.MultiColumn = chklstShieldKeyword.MultiColumn
		chklstShieldViewer.ColumnWidth = m_ShieldViewerCheckedListBoxColumnWidth
		chklstShieldViewer.Anchor = chklstShieldKeyword.Anchor
		chklstShieldViewer.CheckOnClick = chklstShieldKeyword.CheckOnClick
		chklstShieldViewer.Width = chklstShieldKeyword.Width
		chklstShieldViewer.Left = chklstShieldKeyword.Left
		' 默认不显示
		chklstShieldViewer.Visible = False

		chkApplyShieldInfo.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom
		chkCheckAll.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom
		btnDelete.Anchor = chkCheckAll.Anchor

		'If Not isUper Then
		'	m_RoomGlobalShieldPanel = New Panel
		'	m_RoomGlobalShieldControl = New UserAnchorGlobalShieldControl() With {
		'		.Dock = DockStyle.Fill
		'	}
		'	m_RoomGlobalShieldPanel.Controls.Add(m_RoomGlobalShieldControl)
		'	m_RoomGlobalShieldPanel.Width = Me.Width
		'	m_RoomGlobalShieldPanel.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right

		'	Me.Controls.Add(m_RoomGlobalShieldPanel)

		'	pnlKeywordAndViewerShield.Location = New Point(m_RoomGlobalShieldPanel.Location.X, m_RoomGlobalShieldPanel.Location.Y + m_RoomGlobalShieldPanel.Height + m_RoomGlobalShieldPanel.Margin.Bottom)
		'End If
		'pnlKeywordAndViewerShield.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right Or AnchorStyles.Bottom
		'pnlKeywordAndViewerShield.Dock = DockStyle.Fill
		'tlpControlContainer.Dock = DockStyle.Fill

		'm_UserAnchorShieldControlOriginalHeight = tlpControlContainer.Controls(0).Height

		Me.Dock = DockStyle.Fill

		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub

#End Region

#Region "IDisposable Support"
	' 要检测冗余调用
	Dim isDisposed2 As Boolean = False

	''' <summary>
	''' 重写Dispose 以清理非托管资源
	''' </summary>
	''' <param name="disposing"></param>
	Protected Overrides Sub Dispose(disposing As Boolean)
		' 窗体内的控件调用Close或者Dispose方法时，isDisposed2的值为True
		If isDisposed2 Then Return

		Try
			' TODO: 释放托管资源(托管对象)。
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
					components = Nothing
				End If
			End If

			' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
			' TODO: 将大型字段设置为 null。

			isDisposed2 = True
		Finally
			MyBase.Dispose(disposing)
		End Try
	End Sub

	'' NOTE: Leave out the finalizer altogether if this class doesn't   
	'' own unmanaged resources itself, but leave the other methods  
	'' exactly as they are.   
	'Protected Overrides Sub Finalize()
	'    Try
	'        ' Finalizer calls Dispose(false)  
	'        Dispose(False)
	'    Finally
	'        MyBase.Finalize()
	'    End Try
	'End Sub
#End Region



#Region "函数区"
	Public Overrides Async Function LoadDataAsync() As Task
		Await GetShieldInfoAsync()
		BindShieldInfo()
	End Function

	''' <summary>
	''' 获取屏蔽信息
	''' </summary>
	''' <returns></returns>
	Private Async Function GetShieldInfoAsync() As Task
		m_NeedRebind = False
		Dim getRst = Await BilibiliApi.GetShieldInfoAsync()
		If Not getRst.Success Then
			Common.ShowOperateResultTask(Me, False)
			Return
		End If

		Dim json = getRst.Message
		Dim jsonRoot As RoomShieldInfoEntity.Root = Nothing
		Try
			jsonRoot = MSJsSerializer.Deserialize(Of RoomShieldInfoEntity.Root)(json)
		Catch ex As Exception
			Logger.WriteLine(ex)
		End Try
		If jsonRoot Is Nothing Then
			Common.ShowOperateResultTask(Me, False)
			Return
		End If

		Threading.Interlocked.Exchange(m_ShieldInfo, jsonRoot.data)

		m_NeedRebind = True
	End Function

	Private Sub BindShieldInfo()
		AdjustUIAfterUserAnchorShield(m_ShieldInfo.shield_rules)

		If m_ShieldMode = ShieldMode.Keyword Then
			BindShieldKeywors(chklstShieldKeyword, m_ShieldInfo.keyword_list)
		ElseIf m_ShieldMode = ShieldMode.Viewer Then
			BindShieldUsers(chklstShieldViewer, m_ShieldInfo.shield_user_list)
		End If

		chkCheckAll.Checked = False
	End Sub

	Private Sub BindShieldKeywors(ByRef chklst As CheckedListBox, ByVal data As List(Of String))
		' 展现数据前清空已有数据
		If chklst.Items.Count > 0 AndAlso m_NeedRebind Then
			chklst.Items.Clear()
		End If

		For Each item In data
			chklst.Items.Add(item)
		Next
	End Sub

	Private Sub BindShieldUsers(ByRef chklst As CheckedListBox, ByVal data As List(Of RoomShieldInfoEntity.Shield_user_list))
		' 展现数据前清空已有数据
		If chklst.Items.Count > 0 AndAlso m_NeedRebind Then
			chklst.Items.Clear()
		End If

		For Each item In data
			chklst.Items.Add($"{item.uid.ToStringOfCulture}_{item.uname}")
		Next
	End Sub

	Private Async Sub chkShieldSwitch_Click(sender As Object, e As EventArgs) Handles chkShieldSwitch.Click
		Try
			chkShieldSwitch.Enabled = Not chkShieldSwitch.Enabled

			' 软件上的屏蔽状态可能跟实时的状态不一致，所以三个屏蔽动作都需要执行一次
			Dim shieldLevelTask = InternalUserAnchorShieldByLevelAsync(chkShieldSwitch.Checked)
			Dim shieldRankTask = If(chkShieldSwitch.Checked,
						 BilibiliApi.UserAnchorShieldAsync(BilibiliApi.ShieldType.Rank),
						 BilibiliApi.UserAnchorUnShieldAsync(BilibiliApi.ShieldType.Rank))
			Dim shieldVerifyTask = If(chkShieldSwitch.Checked,
						 BilibiliApi.UserAnchorShieldAsync(BilibiliApi.ShieldType.Verify),
						 BilibiliApi.UserAnchorUnShieldAsync(BilibiliApi.ShieldType.Verify))
			Await Task.WhenAll(shieldLevelTask, shieldRankTask, shieldVerifyTask)

			Dim shieldLevelRst = shieldLevelTask.Result
			Dim shieldRankRst = shieldRankTask.Result
			Dim shieldVerifyRst = shieldVerifyTask.Result
			Dim haveSuccess = shieldLevelRst.Success OrElse
				shieldRankRst.Success OrElse
				shieldVerifyRst.Success

			' 总结操作结果
			Dim allSuccess = shieldLevelRst.Success AndAlso
				shieldRankRst.Success AndAlso
				shieldVerifyRst.Success
			Dim opScuccess = "成功"
			Dim msg = If(allSuccess,
				opScuccess,
				$"按等级屏蔽：{If(shieldLevelRst.Success, opScuccess, shieldLevelRst.Message)}{Environment.NewLine}按非正式会员屏蔽：{If(shieldRankRst.Success, opScuccess, shieldRankRst.Message)}{Environment.NewLine}按未绑定手机用户屏蔽：{If(shieldVerifyRst.Success, opScuccess, shieldVerifyRst.Message)}{Environment.NewLine}"
				)
			Common.ShowOperateResultTask(Me, allSuccess, msg, 3000)

			If haveSuccess Then
				Await GetShieldInfoAsync()
				AdjustUIAfterUserAnchorShield(m_ShieldInfo.shield_rules)
			Else
				' 操作失败，恢复选择状态
				chkShieldSwitch.Checked = Not chkShieldSwitch.Checked
			End If
		Catch ex As Exception
			Logger.WriteLine(ex)
		Finally
			chkShieldSwitch.Enabled = Not chkShieldSwitch.Enabled
		End Try
	End Sub

	Private Async Sub ChkAssociateMember_Click(sender As Object, e As EventArgs) Handles chkNotVerifyMember.Click, chkAssociateMember.Click, chkShieldSwitch.Click, chkLevel.Click
		Dim chk = DirectCast(sender, CheckBox)
		If chk Is Nothing Then Return

		Dim opScuccess As Boolean
		Dim rst As (Success As Boolean, Message As String, Result As String) = Nothing

		Select Case chk.Name
			Case chkLevel.Name
				rst = Await InternalUserAnchorShieldByLevelAsync(chk.Checked)
			Case chkAssociateMember.Name
				rst = If(chk.Checked,
					Await BilibiliApi.UserAnchorShieldAsync(BilibiliApi.ShieldType.Rank),
					Await BilibiliApi.UserAnchorUnShieldAsync(BilibiliApi.ShieldType.Rank))
			Case chkNotVerifyMember.Name
				rst = If(chk.Checked,
					Await BilibiliApi.UserAnchorShieldAsync(BilibiliApi.ShieldType.Verify),
					Await BilibiliApi.UserAnchorUnShieldAsync(BilibiliApi.ShieldType.Verify))
		End Select

		opScuccess = rst.Success
		Common.ShowOperateResultTask(Me, rst)

		If opScuccess Then
			Await GetShieldInfoAsync()
			AdjustUIAfterUserAnchorShield(m_ShieldInfo.shield_rules)
		Else
			' 操作失败，恢复选择状态
			chk.Checked = Not chk.Checked
		End If
	End Sub

	''' <summary>
	''' 按等级屏蔽
	''' </summary>
	''' <returns></returns>
	Private Async Function UserAnchorShieldByLevelAsync() As Task
		Try
			trbShieldLevel.Enabled = Not trbShieldLevel.Enabled
			nudShieldLevel.Enabled = trbShieldLevel.Enabled

			Dim level = nudShieldLevel.Value.ToIntegerOfCulture
			Dim rst = Await InternalUserAnchorShieldByLevelAsync(level <> 0)

			Dim opScuccess = rst.Success
			Common.ShowOperateResultTask(Me, rst)

			If opScuccess Then
				Await GetShieldInfoAsync()
				AdjustUIAfterUserAnchorShield(m_ShieldInfo.shield_rules)
			End If
		Catch ex As Exception
			Logger.WriteLine(ex)
		Finally
			trbShieldLevel.Enabled = Not trbShieldLevel.Enabled
			nudShieldLevel.Enabled = trbShieldLevel.Enabled
		End Try
	End Function

	''' <summary>
	''' 按等级屏蔽
	''' </summary>
	''' <param name="checkedLevel"></param>
	''' <returns></returns>
	Private Async Function InternalUserAnchorShieldByLevelAsync(ByVal checkedLevel As Boolean) As Task(Of (Success As Boolean, Message As String, Result As String))
		Dim level = nudShieldLevel.Value.ToIntegerOfCulture
		If level = 0 AndAlso checkedLevel Then
			Return (False, "屏蔽等级应设置为大于0", String.Empty)
		End If

		Dim rst = If(checkedLevel,
			Await BilibiliApi.UserAnchorShieldAsync(BilibiliApi.ShieldType.Level, level),
			Await BilibiliApi.UserAnchorUnShieldAsync(BilibiliApi.ShieldType.Level))

		Return rst
	End Function

	Private Sub AdjustUIAfterUserAnchorShield(ByVal shieldRules As RoomShieldInfoEntity.Shield_rules)
		' 有任意一个大于1就表示屏蔽已开启
		Dim shieldOpened = (shieldRules.level + shieldRules.rank + shieldRules.verify) > 0
		chkShieldSwitch.Text = If(shieldOpened, "屏蔽已开启", "屏蔽未开启")
		'If shieldOpened Then
		'End If
		nudShieldLevel.Value = shieldRules.level
		chkLevel.Checked = (shieldRules.level >= 1)
		chkAssociateMember.Checked = (shieldRules.rank = 1)
		chkNotVerifyMember.Checked = (shieldRules.verify = 1)
		chkShieldSwitch.Checked = shieldOpened
	End Sub


	Private Sub trbShieldLevel_Scroll(sender As Object, e As EventArgs) Handles trbShieldLevel.Scroll
		nudShieldLevel.Value = trbShieldLevel.Value
	End Sub

	Private Async Sub TrbShieldLevel_MouseUp(sender As Object, e As MouseEventArgs) Handles trbShieldLevel.MouseUp
		Await UserAnchorShieldByLevelAsync()
	End Sub

	Private Sub nudShieldLevel_ValueChanged(sender As Object, e As EventArgs) Handles nudShieldLevel.ValueChanged
		trbShieldLevel.Value = nudShieldLevel.Value.ToIntegerOfCulture
		m_ShieldLevel = nudShieldLevel.Value.ToIntegerOfCulture
	End Sub

	Private Async Sub NudShieldLevel_KeyUp(sender As Object, e As KeyEventArgs) Handles nudShieldLevel.KeyUp
		If e.KeyCode = Keys.Enter Then
			Await UserAnchorShieldByLevelAsync()
		End If
	End Sub

	Private Async Sub btnAddShieldKeyword_ClickAsync(sender As Object, e As EventArgs) Handles btnAddShieldKeyword.Click
		Try
			btnAddShieldKeyword.Enabled = False
			Await AddShieldKeywordAsync()
		Finally
			btnAddShieldKeyword.Enabled = True
		End Try
	End Sub

	Private Async Function AddShieldKeywordAsync() As Task
		Dim shieldKeyword = txtShieldKeyword.Text.Trim
		If shieldKeyword.Length = 0 Then
			Windows2.DrawTipsTask(If(Me.Parent, Me), "请输入您要屏蔽的内容" & RandomEmoji.Helpless, 2000, False, False)
			txtShieldKeyword.Focus()
			Return
		End If

		Dim rst = Await BilibiliApi.AddShieldKeywordAsync(shieldKeyword)
		Common.ShowOperateResultTask(Me, rst)
		If rst.Success Then ReLoadDataTask()
	End Function

	Private Async Sub BtnAddShieldViewer_Click(sender As Object, e As EventArgs) Handles btnAddShieldViewer.Click
		Try
			btnAddShieldViewer.Enabled = False
			Await AddShieldViewerAsync()
		Finally
			btnAddShieldViewer.Enabled = True
		End Try
	End Sub

	Private Async Function AddShieldViewerAsync() As Task
		Dim shieldViewer = txtShieldViewer.Text.Trim
		If shieldViewer.Length = 0 Then
			Windows2.DrawTipsTask(If(Me.Parent, Me), "请输入您要屏蔽的用户Id或者昵称" & RandomEmoji.Helpless, 2000, False, False)
			txtShieldViewer.Focus()
			Return
		End If

		Dim user = Await DanmuEntry.GetUserInfoAsync(String.Empty, shieldViewer)
		If user.Uid.IsNullOrEmpty Then
			Common.ShowOperateResultTask(Me, False)
			Return
		End If

		Dim rst = Await BilibiliApi.RoomShieldViewerAsync(user.Uid)
		Common.ShowOperateResultTask(Me, rst)
		If rst.Success Then ReLoadDataTask()
	End Function

	Private Sub BtnRegetShieldInfo_Click(sender As Object, e As EventArgs) Handles btnRegetShieldInfo.Click
		Try
			btnRegetShieldInfo.Enabled = False
			ReLoadDataTask()
		Finally
			btnRegetShieldInfo.Enabled = True
		End Try
	End Sub

	Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
		Dim chklst = If(m_ShieldMode = ShieldMode.Keyword, chklstShieldKeyword, chklstShieldViewer)
		If chklst.CheckedItems.Count = 0 Then Return

		Try
			btnDelete.Enabled = Not btnDelete.Enabled

			Dim haveSuccess As Boolean
			Dim deleteFailItem As New Concurrent.ConcurrentBag(Of String)
			Dim success = False
			For Each i As Integer In chklst.CheckedIndices
				Dim item = chklst.Items(i).ToString
				If m_ShieldMode = ShieldMode.Keyword Then
					Dim rst = Await BilibiliApi.RemoveShieldKeywordAsync(item)
					success = rst.Success
				Else
					Dim separatorIndex = item.IndexOf("_")
					Dim viewerId = item.Substring(0, separatorIndex)
					Dim rst = Await BilibiliApi.RoomUnShieldViewerAsync(viewerId)
					success = rst.Success
				End If
				If success Then
					haveSuccess = True
				Else
					deleteFailItem.Add(item)
				End If
			Next

			If haveSuccess AndAlso deleteFailItem.Count = 0 Then
				' 全部删除成功
				Common.ShowOperateResultTask(Me, True)
			Else
				' 有失败,有成功
				If haveSuccess Then
					Common.ShowOperateResultTask(Me, False, $"删除失败项:{Environment.NewLine}{String.Join("、", deleteFailItem)}")
				Else
					' 全失败
					Common.ShowOperateResultTask(Me, False)
				End If
			End If

			' 有删除成功的就需要重新获取加载列表
			If haveSuccess Then
				ReLoadDataTask()
			End If
		Catch ex As Exception
			Logger.WriteLine(ex)
		Finally
			btnDelete.Enabled = Not btnDelete.Enabled
		End Try
	End Sub


	Private Async Sub chkApplyShieldKeyword_ClickAsync(sender As Object, e As EventArgs) Handles chkCheckAll.Click, chkApplyShieldInfo.Click
		Dim chk = DirectCast(sender, CheckBox)
		If chk Is Nothing Then Return

		Select Case chk.Name
			Case chkApplyShieldInfo.Name
				Dim rst = Await BilibiliApi.ApplyShieldKeywordAsync(chk.Checked)
				Common.ShowOperateResultTask(Me, rst)
				If Not rst.Success Then
					chk.Checked = False
				End If
			Case chkCheckAll.Name
				Dim chklst = If(m_ShieldMode = ShieldMode.Keyword,
					chklstShieldKeyword,
					chklstShieldViewer)
				For i = 0 To chklst.Items.Count - 1
					chklst.SetItemChecked(i, chk.Checked)
				Next
		End Select
	End Sub

	Private Sub LblShieldKeyword_MouseMove(sender As Object, e As MouseEventArgs) Handles lblShieldViewer.MouseMove, lblShieldKeyword.MouseMove
		Me.Cursor = Cursors.Hand
	End Sub

	Private Sub LblShieldKeyword_MouseLeave(sender As Object, e As EventArgs) Handles lblShieldViewer.MouseLeave, lblShieldKeyword.MouseLeave
		Me.Cursor = Me.DefaultCursor
	End Sub

	Private Sub InitLabelForeColore()
		For Each lbl As Label In tlpShieldModeLabel.Controls
			lbl.ForeColor = SystemColors.ControlText
		Next
	End Sub

	Private Sub LblShieldKeyword_Click(sender As Object, e As EventArgs) Handles lblShieldViewer.Click, lblShieldKeyword.Click
		Dim lbl = DirectCast(sender, Label)
		If lbl Is Nothing Then Return

		InitLabelForeColore()
		lbl.ForeColor = SystemColors.Highlight

		Select Case lbl.Name
			Case lblShieldKeyword.Name
				chklstShieldKeyword.Visible = True
				chklstShieldViewer.Visible = Not chklstShieldKeyword.Visible
				m_ShieldMode = ShieldMode.Keyword
			Case lblShieldViewer.Name
				chklstShieldKeyword.Visible = False
				chklstShieldViewer.Visible = Not chklstShieldViewer.Visible
				m_ShieldMode = ShieldMode.Viewer
		End Select

		BindShieldInfo()
	End Sub

	Private Async Sub TxtShieldKeyword_KeyUp(sender As Object, e As KeyEventArgs) Handles txtShieldViewer.KeyUp, txtShieldKeyword.KeyUp
		If e.KeyCode <> Keys.Enter Then Return

		Dim txt = DirectCast(sender, TextBox)
		If txt Is Nothing Then Return

		Try
			txt.Enabled = Not txt.Enabled
			Select Case txt.Name
				Case txtShieldKeyword.Name
					Await AddShieldKeywordAsync()
				Case txtShieldViewer.Name
					Await AddShieldViewerAsync()
			End Select
		Finally
			txt.Enabled = Not txt.Enabled
		End Try
	End Sub

	Private Sub RoomShieldControl_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
		'If m_RoomGlobalShieldPanel Is Nothing Then Return
		'pnlKeywordAndViewerShield.Size = New Size(Me.Width, Me.Height - m_RoomGlobalShieldPanel.Height - m_RoomGlobalShieldPanel.Margin.Top - m_RoomGlobalShieldPanel.Margin.Bottom - 3)
	End Sub


#End Region
End Class
