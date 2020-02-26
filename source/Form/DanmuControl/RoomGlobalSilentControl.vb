Imports System.Text.RegularExpressions
Imports ShanXingTech

Public Class RoomGlobalSilentControl
	Inherits ViewerManageBaseControl

#Region "字段区"
	Private WithEvents m_CountdownTimer As Timer
	Private m_SilentType As BilibiliApi.SilentType
	Private m_SilentLevel As Integer
	Private m_SilentInterval As BilibiliApi.SilentInterval
	Private m_SilentInfo As RoomSilentInfoEntity.Data
#End Region

#Region "构造函数"
	''' <summary>
	''' 
	''' </summary>
	''' <param name="haveAdminAuthorit">是否有管理权限（uper或者管理员）</param>
	Sub New(ByVal haveAdminAuthorit As Boolean)

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。
		m_CountdownTimer = New Timer With {.Interval = 1000, .Enabled = False}
		m_SilentType = BilibiliApi.SilentType.Level
		m_SilentInterval = BilibiliApi.SilentInterval.Three

		Me.SuspendLayout()
		InitCombobox()
		rdbtnViewerLevel.Checked = True
		trbShieldLevel.Minimum = 1
		trbShieldLevel.Maximum = 60
		nudShieldLevel.Minimum = trbShieldLevel.Minimum
		nudShieldLevel.Maximum = trbShieldLevel.Maximum
		lblUnShieldTime.Visible = False

		chkSilent.Enabled = haveAdminAuthorit
		pnlRoomShield.Enabled = haveAdminAuthorit

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

	Public Overrides Async Function LoadDataAsync() As Task
		Await GetSilentInfoAsync()
	End Function

	''' <summary>
	''' 获取禁言状态以及信息
	''' </summary>
	''' <returns></returns>
	Private Async Function GetSilentInfoAsync() As Task
		Dim getRst = Await BilibiliApi.GetSilentInfoAsync()
		If Not getRst.Success Then
			Common.ShowOperateResultTask(Me, False)
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
			Common.ShowOperateResultTask(Me, False)
			Return
		End If

		Threading.Interlocked.Exchange(m_SilentInfo, jsonRoot.data)
		If m_SilentInfo.second = -1 OrElse m_SilentInfo.second > 0 Then
			AdjustUIWhenSilent(True)
		End If
	End Function

	Private Async Sub chkSilent_Click(sender As Object, e As EventArgs) Handles chkSilent.Click
		Dim opScuccess As Boolean
		Dim rst = If(chkSilent.Checked,
			Await BilibiliApi.RoomSilentAsync(m_SilentType, m_SilentLevel, m_SilentInterval),
			Await BilibiliApi.RoomUnSilentAsync())
		opScuccess = rst.Success
		Common.ShowOperateResultTask(Me, rst)

		' 开启禁言并且操作成功才需要重新获取禁言状态信息
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
		lblShieldStatus.Text = If(silented, "禁言已开启", "禁言未开启")
		If silented Then
			ShowCountdownTime()
			nudShieldLevel.Value = m_SilentInfo.level
			Dim tempType As BilibiliApi.SilentType
			[Enum].TryParse(m_SilentInfo.type, True, tempType)
			Select Case tempType
				Case BilibiliApi.SilentType.Level
					rdbtnViewerLevel.Checked = True
				Case BilibiliApi.SilentType.Medal
					rdbtnFanMadel.Checked = True
				Case BilibiliApi.SilentType.Member
					rdbtnAllViewer.Checked = True
				Case Else
					rdbtnViewerLevel.Checked = True
			End Select

			Dim tempTimeSpan = TimeSpan.FromSeconds(m_SilentInfo.second)
			If tempTimeSpan.Minutes > 30 Then
				cmbShieldInterval.SelectedIndex = 3
			ElseIf tempTimeSpan.Minutes > 10 Then
				cmbShieldInterval.SelectedIndex = 2
			ElseIf tempTimeSpan.Minutes > 3 Then
				cmbShieldInterval.SelectedIndex = 1
			Else
				cmbShieldInterval.SelectedIndex = 0
			End If
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
		m_SilentLevel = nudShieldLevel.Value.ToIntegerOfCulture
	End Sub

	Private Sub RdbtnViewerLevel_Click(sender As Object, e As EventArgs) Handles rdbtnViewerLevel.Click, rdbtnFanMadel.Click, rdbtnAllViewer.Click
		Dim rdbtn = DirectCast(sender, RadioButton)
		If rdbtn Is Nothing Then Return

		Select Case rdbtn.Name
			Case rdbtnViewerLevel.Name
				m_SilentType = BilibiliApi.SilentType.Level
			Case rdbtnFanMadel.Name
				m_SilentType = BilibiliApi.SilentType.Medal
			Case rdbtnAllViewer.Name
				m_SilentType = BilibiliApi.SilentType.Member
		End Select

		' 全员禁言不需要设置等级
		trbShieldLevel.Enabled = Not (m_SilentType = BilibiliApi.SilentType.Member)
		nudShieldLevel.Enabled = trbShieldLevel.Enabled
	End Sub

	Private Sub CmbShieldInterval_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbShieldInterval.SelectedValueChanged
		Dim value = cmbShieldInterval.Text
		Dim match = Regex.Match(value, "(\d+)\w+", RegexOptions.IgnoreCase Or RegexOptions.Compiled)
		[Enum].TryParse(If(match.Success, match.Groups(1).Value, "0")， True, m_SilentInterval)
	End Sub
#End Region
End Class
