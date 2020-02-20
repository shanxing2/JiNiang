Imports System.Text.RegularExpressions
Imports ShanXingTech


Public Class UserAnchorGlobalShieldControl

#Region "字段区"
	Private m_ShieldType As BilibiliApi.ShieldType
	Private m_ShieldLevel As Integer
	Private m_ShieldStatus As BilibiliApi.ShieldOperate
	Private m_ShieldInfo As RoomSilentInfoEntity.Data
#End Region

#Region "构造函数"
	Sub New()

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。
		m_ShieldType = BilibiliApi.ShieldType.None
		m_ShieldStatus = BilibiliApi.ShieldOperate.Off

		Me.SuspendLayout()
		trbShieldLevel.Minimum = 1
		trbShieldLevel.Maximum = 60
		nudShieldLevel.Minimum = trbShieldLevel.Minimum
		nudShieldLevel.Maximum = trbShieldLevel.Maximum

		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub

#End Region

#Region "函数区"
	''' <summary>
	''' 获取屏蔽状态以及信息
	''' </summary>
	''' <returns></returns>
	Private Async Function GetSilentInfoAsync() As Task
		Dim getRst = Await BilibiliApi.GetSilentInfoAsync()
		If Not getRst.Success Then
			'ShowOperateResultTask(False)
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
			'ShowOperateResultTask(False)
			Return
		End If

		Threading.Interlocked.Exchange(m_ShieldInfo, jsonRoot.data)
		If m_ShieldInfo.second = -1 OrElse m_ShieldInfo.second > 0 Then
			AdjustUIAfterShield(True)
		End If
	End Function

	Private Async Sub ChkAssociateMember_Click(sender As Object, e As EventArgs) Handles chkNotVerifyMember.Click, chkAssociateMember.Click, chkShieldSwitch.Click, chkLevel.Click
		Dim chk = DirectCast(sender, CheckBox)
		If chk Is Nothing Then Return

		Select Case chk.Name
			Case chkShieldSwitch.Name

			Case chkLevel.Name


			Case chkAssociateMember.Name

			Case chkNotVerifyMember.Name

		End Select


		Dim opScuccess As Boolean
		Dim rst = If(chkShieldSwitch.Checked,
			Await BilibiliApi.UserAnchorShieldAsync(m_ShieldType),
			Await BilibiliApi.UserAnchorUnShieldAsync(BilibiliApi.ShieldType.None))
		opScuccess = rst.Success
		'ShowOperateResultTask(opScuccess, If(opScuccess, "成功", rst.Message))

		' 开启屏蔽并且操作成功才需要重新获取屏蔽状态信息
		If chkShieldSwitch.Checked AndAlso opScuccess Then
			Await GetSilentInfoAsync()
		End If
		If opScuccess Then
			AdjustUIAfterShield(chkShieldSwitch.Checked)
		Else
			chkShieldSwitch.Checked = Not chkShieldSwitch.Checked
		End If
	End Sub

	Private Sub AdjustUIAfterShield(ByVal shielded As Boolean)
		lblShieldStatus.Text = If(shielded, "屏蔽已开启", "屏蔽未开启")
		If shielded Then
			nudShieldLevel.Value = m_ShieldInfo.level
			Dim tempType As BilibiliApi.SilentType
			[Enum].TryParse(m_ShieldInfo.type, True, tempType)
			'Select Case tempType
			'	Case BilibiliApi.SilentType.Level
			'		rdbtnViewerLevel.Checked = True
			'	Case BilibiliApi.SilentType.Medal
			'		rdbtnFanMadel.Checked = True
			'	Case BilibiliApi.SilentType.Member
			'		rdbtnAllViewer.Checked = True
			'	Case Else
			'		rdbtnViewerLevel.Checked = True
			'End Select
		End If
		chkShieldSwitch.Checked = shielded
		pnlShield.Enabled = Not shielded
	End Sub


	Private Sub trbShieldLevel_Scroll(sender As Object, e As EventArgs) Handles trbShieldLevel.Scroll
		nudShieldLevel.Value = trbShieldLevel.Value
	End Sub

	Private Sub nudShieldLevel_ValueChanged(sender As Object, e As EventArgs) Handles nudShieldLevel.ValueChanged
		trbShieldLevel.Value = nudShieldLevel.Value.ToIntegerOfCulture
		m_ShieldLevel = nudShieldLevel.Value.ToIntegerOfCulture
	End Sub
#End Region
End Class
