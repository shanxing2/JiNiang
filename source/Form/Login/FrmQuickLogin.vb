Imports System.Text.RegularExpressions
Imports ShanXingTech
Imports 姬娘.LoginManager

Public Class FrmQuickLogin

#Region "字段区"
	Private ReadOnly m_ShowViewedRoomCount As Integer = 20
	Private ReadOnly m_RoomIdAndUpNameDic As Dictionary(Of String, String）
	Private ReadOnly m_UserDic As Dictionary(Of String, UserInfo）
	Private ReadOnly m_RommIdAndUpNameSeparator As String = "→_→"
	Private ReadOnly m_LoginNewAccount As String = "我要登录新账号"
	Private ReadOnly m_NotLogin As String = "我只看看不说话"
	''' <summary>
	''' 我先瞅瞅
	''' </summary>
	Private ReadOnly m_SeeBefore As String
	Private ReadOnly m_User As UserInfo
#End Region

#Region "属性区"
	Private m_LoginResult As LoginResult
	Public ReadOnly Property LoginResult() As LoginResult
		Get
			Return m_LoginResult
		End Get
	End Property

	Private Const CS_NOCLOSE = &H200

	Protected Overrides ReadOnly Property CreateParams As CreateParams
		Get
			' 关闭按钮不可用
			Dim cr As CreateParams = MyBase.CreateParams
			cr.ClassStyle = cr.ClassStyle Or CS_NOCLOSE
			Return cr
		End Get
	End Property

	Private m_LoginMode As LoginMode
	''' <summary>
	''' 登录方式
	''' </summary>
	''' <returns></returns>
	Public ReadOnly Property LoginMode() As LoginMode
		Get
			Return m_LoginMode
		End Get
	End Property
#End Region

#Region "构造函数区"
	Public Sub New(ByRef user As UserInfo)

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。
		m_User = user
		m_RoomIdAndUpNameDic = New Dictionary(Of String, String）(m_ShowViewedRoomCount)
		m_UserDic = New Dictionary(Of String, UserInfo)

		' 默认扫码登录模式
		rdbtnLoginUseQRCode.Checked = True
		m_SeeBefore = "我先瞅瞅" & RandomEmoji.Happy

		pnlLoginOptions.Enabled = False

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
				If ToolTip1 IsNot Nothing Then
					ToolTip1.Dispose()
					ToolTip1 = Nothing
				End If

				If components IsNot Nothing Then
					components.Dispose()
					components = Nothing
				End If
			End If

			' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
			' TODO: 将大型字段设置为 null

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



	Private Sub FrmLoginPrompt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		cmbViewedRooms.SetCueBanner("选择或者输入直播间Id" & RandomEmoji.Happy)

#Disable Warning BC42358
		GetConfigAsync()
#Enable Warning
	End Sub

	Private Async Function GetConfigAsync() As Task
		Try
			cmbUsersNick.Enabled = False
			cmbViewedRooms.Enabled = False
			Await Task.WhenAll(GetLoginedUserInfoAsync, GetViewedRoomInfoAsync)
		Catch ex As Exception
			Logger.WriteLine(ex)
		Finally
			cmbUsersNick.Enabled = True
			cmbViewedRooms.Enabled = True
		End Try
	End Function

	Private Async Function GetLoginedUserInfoAsync() As Task
		' 获取用户信息
		Using reader = Await IO2.Database.SQLiteHelper.GetDataReaderAsync("SELECT Id, Nick, StoreCookies, LastViewedRoomRealId, RoomShortId, RoomRealId FROM UserInfo WHERE LENGTH(UserInfo.Nick) > 0")
			While Await reader.ReadAsync
				Dim user As New UserInfo With {
					.Id = CStr(reader(0)),
					.Nick = CStr(reader(1)),
					.StoreCookies = CBool(reader(2)),
					.ViewedRoomId = CInt(reader(3)),
					.RoomShortId = CInt(reader(4)),
					.RoomRealId = CInt(reader(5))
				}
				m_UserDic.Add(user.Nick, user)
			End While
		End Using

		cmbUsersNick.Items.Clear()
		cmbUsersNick.Items.Add(m_NotLogin)

		If m_UserDic.Count = 1 Then
			cmbUsersNick.Items.Add(m_UserDic.FirstOrDefault.Value.Nick)
		ElseIf m_UserDic.Count > 1 Then
			For Each user In m_UserDic
				cmbUsersNick.Items.Add(user.Key)
			Next
		End If

		cmbUsersNick.Items.Add(m_LoginNewAccount)

		' 默认选中 m_NotLogin
		cmbUsersNick.SelectedIndex = 0
		m_User.Id = NotLoginUserId
	End Function

	Private Async Function GetViewedRoomInfoAsync() As Task
		' 获取访问过的直播间Id/up昵称
		Using reader = Await IO2.Database.SQLiteHelper.GetDataReaderAsync($"select RealId,UpName from ViewedRoomInfo  order by LastViewedTimestamp desc limit 0, {m_ShowViewedRoomCount.ToStringOfCulture};")
			While Await reader?.ReadAsync
				m_RoomIdAndUpNameDic.Add(reader(0).ToString, $"{reader(0).ToString}{m_RommIdAndUpNameSeparator}{reader(1).ToString}")
			End While
		End Using
		cmbViewedRooms.Items.Clear()
		If m_RoomIdAndUpNameDic.Count > 0 Then
			cmbViewedRooms.Items.AddRange(m_RoomIdAndUpNameDic.Values.ToArray)
		End If
		cmbViewedRooms.Items.Add(m_SeeBefore)
	End Function

	Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
		m_LoginResult = LoginResult.Cancel
		Me.Close()
	End Sub

	Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
		TryLogin()
	End Sub

	Private Sub TryLogin()
		If cmbUsersNick.Text.Length = 0 Then
			Windows2.DrawTipsTask(Me, "你是耍流氓嘛???还没有选择需要登录的账号呢" & RandomEmoji.Shock, 1000, False, False)
			Return
		End If

		If cmbViewedRooms.Text.Length = 0 Then
			Windows2.DrawTipsTask(Me, "你是耍流氓嘛???还没有选择需要进入的直播间呢" & RandomEmoji.Shock, 1000, False, False)
			Return
		End If

		m_User.ViewedRoomId = GetCurrentRoomId()
		If SeeBeforeViewRoomId = m_User.ViewedRoomId AndAlso cmbViewedRooms.Text.Trim <> m_SeeBefore Then
			Windows2.DrawTipsTask(Me, "获取直播间Id失败" & RandomEmoji.Sad, 1000, False, False)
			Return
		End If

		m_LoginResult = If(SeeBeforeViewRoomId = m_User.ViewedRoomId,
			LoginResult.UserOnly,
			If(NotLoginUserId = m_User.Id, LoginResult.NotLogin, LoginResult.Yes))
		m_LoginMode = If(rdbtnLoginWithBrowser.Checked,
			LoginMode.Browser,
			If(rdbtnLoginUseCookies.Checked,
			LoginMode.Cookies,
			LoginMode.QRCode))

		Me.Close()
	End Sub

	Private Sub btnTryDeleteCookies_Click(sender As Object, e As EventArgs) Handles btnTryDeleteCookies.Click
		If m_User?.Id.IsNullOrEmpty Then
			Windows2.DrawTipsTask(Me, "请先选择需要登录的账号~", 1000, False, False)
			Return
		End If

		Dim rst As Boolean
		Try
			Me.Enabled = False
			Dim sql = "update UserInfo set Cookies = '' WHERE Id = " & m_User.Id
			rst = IO2.Database.SQLiteHelper.ExecuteNonQuery(sql)
		Catch ex As Exception
			Logger.WriteLine(ex)
		Finally
			Me.Enabled = True
		End Try
		Windows2.DrawTipsTask(Me, $"删除{If(rst, "成功", "失败")}~", 1000, rst, False)
	End Sub
	Private Sub chkStoreCookies_CheckedChanged(sender As Object, e As EventArgs) Handles chkStoreCookies.CheckedChanged
		m_User.StoreCookies = chkStoreCookies.Checked
	End Sub

	Private Sub GetSelectUserInfo()
		m_User.Nick = cmbUsersNick.Text.Trim

		If cmbUsersNick.SelectedIndex = -1 AndAlso
			m_User.Nick.Length > 0 Then
			m_User.Id = m_User.Nick
			Return
		End If

		Dim selectAccount = CStr(cmbUsersNick.Items(cmbUsersNick.SelectedIndex))
		If m_NotLogin = selectAccount Then
			m_User.Id = NotLoginUserId
		ElseIf m_LoginNewAccount = selectAccount Then
			m_User.Id = String.Empty
		Else
			m_User.Id = m_UserDic(selectAccount).Id
		End If
	End Sub

	Private Function GetCurrentRoomId() As Integer
		Dim roomId As Integer

		If cmbViewedRooms.SelectedIndex = -1 Then
			roomId = GetRoomId()
		ElseIf cmbViewedRooms.SelectedIndex >= m_RoomIdAndUpNameDic.Count Then
			roomId = SeeBeforeViewRoomId
		Else
			roomId = GetRoomId()
		End If

		Return roomId
	End Function

	Private Function GetRoomId() As Integer
		Dim roomId = SeeBeforeViewRoomId

		Dim selectValue = cmbViewedRooms.Text.Trim
		If selectValue.Length = 0 Then
			roomId = SeeBeforeViewRoomId
		Else
			Dim separatorIndex = selectValue.IndexOf(m_RommIdAndUpNameSeparator)
			' 分隔符至少不能出现在开头
			If separatorIndex > 0 Then
				selectValue = selectValue.Substring(0, separatorIndex)
				Integer.TryParse(selectValue, roomId)
				Return roomId
			End If

			' http://live.bilibili.com/(\d+)?visit_id=bap28p17rds0
			Dim pattern = "(?:live\.bilibili\.com/)?(\d+)"
			Try
				Dim match = Regex.Match(selectValue, pattern, RegexOptions.IgnoreCase Or RegexOptions.Compiled, TimeSpan.FromSeconds(3))
				If match.Success Then
					Integer.TryParse(match.Groups(1).Value, roomId)
				End If
			Catch ex As Exception
				'
			End Try
		End If

		Return roomId
	End Function

	Private Sub cmbUsersNick_Leave(sender As Object, e As EventArgs) Handles cmbUsersNick.Leave
		GetSelectUserInfo()
		pnlLoginOptions.Enabled = (m_User.Nick <> m_NotLogin)

		If m_User.Id.IsNullOrEmpty Then Return

		GetUserLastLoginInfo(m_User.Id)

		chkStoreCookies.Checked = m_User.StoreCookies

		' 用户已经手动输入过直播间号就不需要再更改了
		If m_User.ViewedRoomId > 0 AndAlso
			cmbViewedRooms.Text.Trim.Length = 0 Then
			Dim lastViewedRoomIdAndUpName As String = Nothing
			m_RoomIdAndUpNameDic.TryGetValue(m_User.ViewedRoomId.ToStringOfCulture, lastViewedRoomIdAndUpName)
			cmbViewedRooms.Text = If(lastViewedRoomIdAndUpName, m_User.ViewedRoomId.ToStringOfCulture)
		End If
	End Sub

	''' <summary>
	''' 获取用户上次登录信息
	''' </summary>
	''' <param name="userId"></param>
	Private Sub GetUserLastLoginInfo(ByVal userId As String)
		Dim tempKvp = m_UserDic.FirstOrDefault(Function(u) userId = u.Value.Id)
		If tempKvp.Key Is Nothing Then
			m_User.StoreCookies = False
			m_User.ViewedRoomId = 0
			m_User.RoomShortId = 0
			m_User.RoomRealId = 0
			Return
		End If

		m_User.StoreCookies = tempKvp.Value.StoreCookies
		m_User.ViewedRoomId = tempKvp.Value.ViewedRoomId
		m_User.RoomShortId = tempKvp.Value.RoomShortId
		m_User.RoomRealId = tempKvp.Value.RoomRealId
	End Sub

	Private Sub cmbViewedRooms_KeyUp(sender As Object, e As KeyEventArgs) Handles cmbViewedRooms.KeyUp
		If e.KeyCode = Keys.Enter Then
			TryLogin()
		End If
	End Sub

#Region "内部类"

#End Region
End Class