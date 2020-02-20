Imports ShanXingTech
Imports 姬娘插件.Events

Public Class LiveRoomInfoControl

#Region "字段区"
	Private m_TopParentHwnd As IntPtr
	Private m_User As UserInfo
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

				If IntPtr.Zero <> m_TopParentHwnd Then
					m_TopParentHwnd = IntPtr.Zero
				End If
			End If

			' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
			' TODO: 将大型字段设置为 null。
			If ToolTip1 IsNot Nothing Then
				ToolTip1.Dispose()
				ToolTip1 = Nothing
			End If

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


	Public Sub New()

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。

		Me.SuspendLayout()

		Me.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top

		tlpLiveRoomInfoRow1.Location = New Point(0, 0)
		tlpLiveRoomInfoRow1.Size = New Size(Me.Width, tlpLiveRoomInfoRow1.Height)
		tlpLiveRoomInfoRow2.Location = New Point(tlpLiveRoomInfoRow1.Left, tlpLiveRoomInfoRow1.Bottom - 1)
		tlpLiveRoomInfoRow2.Size = tlpLiveRoomInfoRow1.Size
		tlpLiveRoomInfoRow1.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
		tlpLiveRoomInfoRow2.Anchor = tlpLiveRoomInfoRow1.Anchor

		For i = 0 To tlpLiveRoomInfoRow1.ColumnCount - 2
			tlpLiveRoomInfoRow1.ColumnStyles(i).SizeType = SizeType.AutoSize
		Next
		For i = 0 To tlpLiveRoomInfoRow2.ColumnCount - 2
			tlpLiveRoomInfoRow2.ColumnStyles(i).SizeType = SizeType.AutoSize
		Next
		' 调整昵称标签 以使昵称长度大于昵称标签列长度时，显示 '...' 并且鼠标悬停时可以弹出完整昵称
		' 必须在最后一列，这样设置才会有效果
		lblRoomOwnerNick.AutoEllipsis = True
		lblRoomOwnerNick.AutoSize = False
		lblRoomOwnerNick.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
		tlpLiveRoomInfoRow2.ColumnStyles(tlpLiveRoomInfoRow2.ColumnCount - 1).SizeType = SizeType.Absolute
		tlpLiveRoomInfoRow2.ColumnStyles(tlpLiveRoomInfoRow2.ColumnCount - 1).Width = 1.0F

		lblHourRank.AutoEllipsis = True
		lblHourRank.AutoSize = False
		lblHourRank.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
		tlpLiveRoomInfoRow1.ColumnStyles(tlpLiveRoomInfoRow1.ColumnCount - 1).SizeType = SizeType.Absolute
		tlpLiveRoomInfoRow1.ColumnStyles(tlpLiveRoomInfoRow1.ColumnCount - 1).Width = 1.0F

		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub

	Public Sub Init(ByRef user As UserInfo, ByVal location As Point, ByVal containerSize As Size, ByRef topParentHwnd As IntPtr)
		Me.Location = location
		' 2 ——两个 tlp 之间合并了一个边框；跟弹幕控件公用一个边框
		Me.Size = New Size(containerSize.Width, tlpLiveRoomInfoRow1.Height + tlpLiveRoomInfoRow2.Height - 2)

		m_TopParentHwnd = topParentHwnd
		m_User = user
	End Sub

	Private Sub lblRoomOwnerNick_Click(sender As Object, e As EventArgs) Handles lblRoomOwnerNick.Click
#Region "默认浏览器打开直播间"
		If String.IsNullOrEmpty(m_User.ViewRoom?.RealId) Then
			Windows2.DrawTipsTask(Me, "请先登录以获取直播间Id" & RandomEmoji.Helpless, 1000, False, False)
			Return
		End If
		Dim liveUrl = "http://live.bilibili.com/" & m_User.ViewRoom.RealId
		Process.Start(liveUrl)
#End Region
	End Sub

	Private Sub lblHourRank_Click(sender As Object, e As EventArgs) Handles lblHourRank.Click
#Region "默认浏览器打开小时榜"
		If String.IsNullOrEmpty(m_User.Id) Then
			Windows2.DrawTipsTask(Me, "请先登录以获取用户Id" & RandomEmoji.Helpless, 1000, False, False)
			Return
		End If

		Dim liveUrl = "https://live.bilibili.com/blackboard/room-current-rank.html?anchor_uid=" & m_User.ViewRoom.UserId
		Process.Start(liveUrl)
#End Region
	End Sub

	Private Sub lblRoomOwnerNick_MouseHover(sender As Object, e As EventArgs) Handles lblRoomOwnerNick.MouseHover
		If String.IsNullOrEmpty(m_User.ViewRoom?.RealId) Then
			Return
		End If

		Dim liveUrl = $"打开直播间 {m_User.ViewRoom.UserNick} http://live.bilibili.com/{m_User.ViewRoom.RealId}"
		ToolTip1.Show(liveUrl, lblRoomOwnerNick)
	End Sub

	Private Sub lblRoomOwnerNick_MouseMove(sender As Object, e As MouseEventArgs) Handles lblRoomOwnerNick.MouseMove, lblHourRank.MouseMove
		Dim lbl = DirectCast(sender, Label)
		If lbl IsNot Nothing Then
			lbl.Cursor = Cursors.Hand
		End If
	End Sub

	Private Sub lblRoomOwnerNick_MouseLeave(sender As Object, e As EventArgs) Handles lblRoomOwnerNick.MouseLeave, lblHourRank.MouseLeave
		Dim lbl = DirectCast(sender, Label)
		If lbl IsNot Nothing Then
			lbl.Cursor = Cursors.Default
		End If
	End Sub

	Private Sub lblLiveStatus_MouseHover(sender As Object, e As EventArgs) Handles lblLiveStatus.MouseHover
		Dim tip As String
		Select Case m_User.ViewRoom.Status
			Case LiveStatus.Off
				tip = "未开播"
			Case LiveStatus.Round
				tip = "轮播"
			Case LiveStatus.Break
				tip = "直播中断，自动重连中..."
			Case LiveStatus.Live
				tip = "在播"
			Case LiveStatus.CutOff
				tip = "被B站管理员切断"
			Case Else
				tip = "未知"
		End Select
		ToolTip1.Show(tip, lblLiveStatus)
	End Sub

	Private Sub chkTopMost_Click(sender As Object, e As EventArgs) Handles chkTopMost.Click
		' 置顶窗体
		Dim onTop = If(chkTopMost.Checked, Win32API.HWND_TOPMOST, Win32API.HWND_NOTOPMOST)
		Dim isOnTop = Win32API.SetWindowPos(m_TopParentHwnd, onTop, 0, 0, 0, 0, Win32API.WindowPositions.IgnoreMove Or Win32API.WindowPositions.IgnoreResize)
		Debug.Print(Logger.MakeDebugString($"{If(onTop = Win32API.HWND_TOPMOST, "置顶"， "取消置顶")} 成功 = {isOnTop.ToStringOfCulture}"))

		' 保存 置顶 配置
		DanmuEntry.Configment.MainForm.TopMost = chkTopMost.Checked
	End Sub

	Public Sub UpdateLiveRoomInfo(ByVal room As LiveRoom)
		Try
			BeginUpdateLabelsText(lblAttention, "粉丝 " & room.Attention.ToStringOfCulture)
			BeginUpdateLabelsText(lblSan, "SAN " & room.AnchorInRoom.San.ToStringOfCulture)
			BeginUpdateLabelsText(lblAnchorScore, "积分 " & room.AnchorInRoom.AnchorScore.ToStringOfCulture)
			BeginUpdateLabelsText(lblAreaRank, "No. " & room.AreaRank)
			BeginUpdateLabelsText(lblHourRank, room.Area.ParentName & "小时榜 " & room.HourRank)

			' 不要问我为啥是618，你会发现这个项目会出现很多618
			Windows2.Delay(618)

			EndUpdateLabelsText(lblAttention)
			EndUpdateLabelsText(lblSan)
			EndUpdateLabelsText(lblAnchorScore)
			EndUpdateLabelsText(lblAreaRank)
			EndUpdateLabelsText(lblHourRank)
		Catch ex As OperationCanceledException
			Debug.Print(Logger.MakeDebugString("并行更新直播间信息超时"))
		Catch ex As Exception
			Logger.WriteLine(ex)
		End Try
	End Sub

	Private Sub BeginUpdateLabelsText(ByVal label As Label, ByVal newValue As String)
		If newValue <> label.Text Then
			label.ForeColor = Color.OrangeRed
			If label.InvokeRequired Then
				label.Parent.Invoke(Sub() label.Text = newValue)
			Else
				label.Text = newValue
			End If
		End If
	End Sub

	Private Sub EndUpdateLabelsText(ByVal label As Label)
		If label.ForeColor <> SystemColors.Highlight Then
			label.ForeColor = SystemColors.Highlight
		End If
	End Sub

	Public Sub UpdateLabelText(ByVal label As Label, ByVal newValue As String)
		If newValue <> label.Text Then
			label.ForeColor = Color.OrangeRed
			If label.InvokeRequired Then
				label.Parent.Invoke(Sub() label.Text = newValue)
			Else
				label.Text = newValue
			End If
			' 不要问我为啥是618，你会发现这个项目会出现很多618
			Windows2.Delay(618)
			label.ForeColor = SystemColors.Highlight
		End If
	End Sub
End Class
