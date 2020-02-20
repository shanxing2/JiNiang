Imports System.Text.RegularExpressions
Imports ShanXingTech
Imports ShanXingTech.Text2

Public Class RoomSilentControl
	Inherits ViewerManageBaseControl

#Region "字段区"
	Private WithEvents m_CountdownTimer As Timer
	Private m_CountdownTime As Date
	Private m_ShieldMode As ShieldMode
	Private m_ShieldType As BilibiliApi.SilentType
	Private m_ShieldLevel As Integer
	Private m_ShieldInterval As BilibiliApi.SilentInterval
	Private m_ShieldKeywordList As List(Of String)
	Private m_ShieldViewerList As List(Of String)
	Private m_NeedRebind As Boolean
	Private m_ShieldKeywordCheckedListBoxColumnWidth As Integer
	Private m_ShieldViewerCheckedListBoxColumnWidth As Integer
	Private m_SilentInfo As RoomSilentInfoEntity.Data
	Private ReadOnly m_IsAdmin As Boolean
	Private ReadOnly m_IsUper As Boolean
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
		m_CountdownTimer = New Timer With {.Interval = 1000, .Enabled = False}
		m_ShieldType = BilibiliApi.SilentType.Level
		m_ShieldInterval = BilibiliApi.SilentInterval.Three
		m_ShieldMode = ShieldMode.Keyword
		m_ShieldKeywordList = New List(Of String)
		m_ShieldViewerList = New List(Of String)
		m_IsUper = isUper
		m_IsAdmin = isAdmin

		Me.SuspendLayout()
		InitCombobox()
		rdbtnViewerLevel.Checked = True
		trbShieldLevel.Minimum = 1
		trbShieldLevel.Maximum = 60
		nudShieldLevel.Minimum = trbShieldLevel.Minimum
		nudShieldLevel.Maximum = trbShieldLevel.Maximum
		lblUnShieldTime.Visible = False
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

		chkApplyShieldKeyword.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom
		chkCheckAll.Anchor = AnchorStyles.Left Or AnchorStyles.Bottom
		btnDelete.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
		Me.Dock = DockStyle.Fill

		chkSilent.Enabled = isAdmin
		pnlRoomShield.Enabled = isAdmin

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
				If m_CountdownTimer IsNot Nothing Then
					If m_CountdownTimer.Enabled Then m_CountdownTimer.Enabled = False
					m_CountdownTimer.Dispose()
					m_CountdownTimer = Nothing
				End If

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

	Private Sub InitCombobox()
		If cmbShieldInterval.Items.Count = 0 Then
			Dim hotSale = {"3分钟", "10分钟", "30分钟", "全场直播"}
			cmbShieldInterval.Items.AddRange(hotSale)
			cmbShieldInterval.DropDownStyle = ComboBoxStyle.DropDownList
		End If
		cmbShieldInterval.SelectedIndex = 0
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
		ShowOperateResultTask(rst.Success, If(rst.Success, "成功", rst.Message))
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
			ShowOperateResultTask(False)
			Return
		End If

		Dim rst = Await BilibiliApi.RoomShieldViewerAsync(user.Uid)
		ShowOperateResultTask(rst.Success, If(rst.Success, "成功", rst.Message))
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
				ShowOperateResultTask(True)
			Else
				' 有失败,有成功
				If haveSuccess Then
					ShowOperateResultTask(False, $"删除失败项:{Environment.NewLine}{String.Join("、", deleteFailItem)}")
				Else
					' 全失败
					ShowOperateResultTask(False)
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

	Public Overrides Async Function LoadDataAsync() As Task
		Dim getShieldInfoTask = GetShieldInfoAsync()
		Dim getSilentInfoTask = GetSilentInfoAsync()
		Await Task.WhenAll(getShieldInfoTask, getSilentInfoTask)
		BindDataToCheckedListBox()
	End Function

	''' <summary>
	''' 获取禁言状态以及信息
	''' </summary>
	''' <returns></returns>
	Private Async Function GetSilentInfoAsync() As Task
		Dim getRst = Await BilibiliApi.GetSilentInfoAsync()
		If Not getRst.Success Then
			ShowOperateResultTask(False)
			Return
		End If

		Dim json = getRst.Message
		Dim jsonRoot As RoomSilentInfoEntity.Root = Nothing
		Try
			If json.Contains("""data"":[]") Then Return
			jsonRoot = MSJsSerializer.Deserialize(Of RoomSilentInfoEntity.Root)(json)
		Catch ex As Exception
			Logger.WriteLine(ex)
		End Try
		If jsonRoot Is Nothing Then
			ShowOperateResultTask(False)
			Return
		End If

		Threading.Interlocked.Exchange(m_SilentInfo, jsonRoot.data)
		If m_SilentInfo.second = -1 OrElse m_SilentInfo.second > 0 Then
			AdjustUIWhenSilent(True)
		End If
	End Function

	''' <summary>
	''' 获取屏蔽信息
	''' </summary>
	''' <returns></returns>
	Private Async Function GetShieldInfoAsync() As Task
		m_NeedRebind = False
		Dim getRst = Await BilibiliApi.GetShieldInfoAsync()
		If Not getRst.Success Then
			ShowOperateResultTask(False)
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
			ShowOperateResultTask(False)
			Return
		End If

		' 展现数据前清空已有数据
		If m_ShieldKeywordList.Count > 0 Then
			m_ShieldKeywordList.Clear()
		End If
		For Each item In jsonRoot.data.keyword_list
			m_ShieldKeywordList.Add(item)
		Next

		' 展现数据前清空已有数据
		If m_ShieldViewerList.Count > 0 Then
			m_ShieldViewerList.Clear()
		End If
		For Each item In jsonRoot.data.shield_user_list
			m_ShieldViewerList.Add($"{item.uid.ToStringOfCulture}_{item.uname}")
		Next

		m_NeedRebind = True
	End Function

	Private Sub BindDataToCheckedListBox()
		If m_ShieldMode = ShieldMode.Keyword Then
			InternalBindDataToCheckedListBox(chklstShieldKeyword, m_ShieldKeywordList)
		ElseIf m_ShieldMode = ShieldMode.Viewer Then
			InternalBindDataToCheckedListBox(chklstShieldViewer, m_ShieldViewerList)
		End If

		chkCheckAll.Checked = False
	End Sub

	Private Sub InternalBindDataToCheckedListBox(ByRef chklst As CheckedListBox, ByRef data As List(Of String))
		' 展现数据前清空已有数据
		If chklst.Items.Count > 0 AndAlso m_NeedRebind Then
			chklst.Items.Clear()
		End If
		For Each item In data
			chklst.Items.Add(item)
		Next
	End Sub


	Private Async Sub chkSilent_Click(sender As Object, e As EventArgs) Handles chkSilent.Click
		Dim opScuccess As Boolean
		Dim rst = If(chkSilent.Checked,
			Await BilibiliApi.RoomSilentAsync(m_ShieldType, m_ShieldLevel, m_ShieldInterval),
			Await BilibiliApi.RoomUnSilentAsync())
		opScuccess = rst.Success
		ShowOperateResultTask(opScuccess, If(opScuccess, "成功", rst.Message))

		' 开启屏蔽并且操作成功才需要重新获取屏蔽状态信息
		If chkSilent.Checked AndAlso opScuccess Then
			Await GetSilentInfoAsync()
		End If
		If opScuccess Then
			AdjustUIWhenSilent(chkSilent.Checked)
		Else
			chkSilent.Checked = Not chkSilent.Checked
		End If
	End Sub

	Private Sub AdjustUIWhenSilent(ByVal silented As Boolean)
		lblShieldStatus.Text = If(silented, "屏蔽已开启", "屏蔽未开启")
		If silented Then
			ShowCountdownTime()
		Else
			lblUnShieldTime.Text = String.Empty
		End If
		chkSilent.Checked = silented
		pnlRoomShield.Enabled = Not silented
		lblUnShieldTime.Visible = silented
		m_CountdownTimer.Enabled = silented
	End Sub


	Private Sub m_CountdownTimer_Tick(sender As Object, e As EventArgs) Handles m_CountdownTimer.Tick
		ShowCountdownTime()
	End Sub

	Private Sub ShowCountdownTime()
		' 全场禁言
		If m_SilentInfo.second = -1 Then
			lblUnShieldTime.Text = $"解除时间：无"
		Else
			m_SilentInfo.second -= 1
			If m_SilentInfo.second = 0 Then
				' 倒计时完取消禁言
				AdjustUIWhenSilent(False)
				Return
			End If
			lblUnShieldTime.Text = $"解除还需 { TimeSpan.FromSeconds(m_SilentInfo.second).ToString("mm\:ss")}"
		End If
	End Sub

	Private Sub trbShieldLevel_Scroll(sender As Object, e As EventArgs) Handles trbShieldLevel.Scroll
		nudShieldLevel.Value = trbShieldLevel.Value
	End Sub

	Private Sub nudShieldLevel_ValueChanged(sender As Object, e As EventArgs) Handles nudShieldLevel.ValueChanged
		trbShieldLevel.Value = nudShieldLevel.Value.ToIntegerOfCulture
		m_ShieldLevel = nudShieldLevel.Value.ToIntegerOfCulture
	End Sub

	Private Async Sub chkApplyShieldKeyword_ClickAsync(sender As Object, e As EventArgs) Handles chkCheckAll.Click, chkApplyShieldKeyword.Click
		Dim chk = DirectCast(sender, CheckBox)
		If chk Is Nothing Then Return

		Select Case chk.Name
			Case chkApplyShieldKeyword.Name
				Dim rst = Await BilibiliApi.ApplyShieldKeywordAsync(chk.Checked)
				ShowOperateResultTask(rst.Success, If(rst.Success, "成功", rst.Message))
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

	Private Sub RdbtnViewerLevel_Click(sender As Object, e As EventArgs) Handles rdbtnViewerLevel.Click, rdbtnFanMadel.Click, rdbtnAllViewer.Click
		Dim rdbtn = DirectCast(sender, RadioButton)
		If rdbtn Is Nothing Then Return

		Select Case rdbtn.Name
			Case rdbtnViewerLevel.Name
				m_ShieldType = BilibiliApi.SilentType.Level
			Case rdbtnFanMadel.Name
				m_ShieldType = BilibiliApi.SilentType.Medal
			Case rdbtnAllViewer.Name
				m_ShieldType = BilibiliApi.SilentType.Member
		End Select

		' 全员禁言不需要设置等级
		trbShieldLevel.Enabled = Not (m_ShieldType = BilibiliApi.SilentType.Member)
		nudShieldLevel.Enabled = trbShieldLevel.Enabled
	End Sub

	Private Sub CmbShieldInterval_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbShieldInterval.SelectedValueChanged
		Dim value = cmbShieldInterval.Text
		Dim match = Regex.Match(value, "(\d+)\w+", RegexOptions.IgnoreCase Or RegexOptions.Compiled)
		[Enum].TryParse(If(match.Success, match.Groups(1).Value, "0")， True, m_ShieldInterval)
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

		chkApplyShieldKeyword.Visible = (m_ShieldMode = ShieldMode.Keyword)
		BindDataToCheckedListBox()
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
#End Region
End Class
