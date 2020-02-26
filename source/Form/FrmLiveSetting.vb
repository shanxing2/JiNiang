Imports System.Text.RegularExpressions
Imports ShanXingTech
Imports 姬娘插件.Events

Public Class FrmLiveSetting
    Private WithEvents lblContcatBilibili As Label
    Private m_Liver As LightLiver
	Private m_FrmSelectArea As FrmSelectArea
	Private ReadOnly m_ViewRoom As LiveRoom

	Public Sub New()

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。

	End Sub

	Public Sub New(ByRef room As LiveRoom, ByVal token As String)

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。

		m_ViewRoom = room
		' 必须先实例化之后 再获取使用过的标题
		m_Liver = New LightLiver(room, token)

#Disable Warning BC42358
		GetUsedTitleAsync()
		GetLiveStreamInfoAsync()
#Enable Warning

		Me.SuspendLayout()

		cmbRoomTitle.MaxLength = m_Liver.Room.TitleMaxLength
		cmbRoomTitle.Text = m_Liver.Room.Title
		lblCurrentArea.Text = m_Liver.GetAreaFullName(m_Liver.Room.Area)
		btnChangeArea.Left = lblCurrentArea.Left + lblCurrentArea.Width + lblCurrentArea.Margin.Right + btnChangeArea.Margin.Left
		btnChangeLiveStatus.Text = If(Me.m_Liver.Room.Status = LiveStatus.Live, "关闭直播", "开始直播")

		' 掩码显示，防止推流码无意泄露
		txtRtmpCode.UseSystemPasswordChar = True

		If DanmuEntry.Configment.DisplayUesdTitleCount = 0 Then
			rdbtnDisplayAllUesdTitle.Checked = True
			nudDisplayUesdTitleCount.Value = nudDisplayUesdTitleCount.Minimum
			nudDisplayUesdTitleCount.Enabled = False
		Else
			rdbtnDislayPartOfUsedTitle.Checked = True
			nudDisplayUesdTitleCount.Value = DanmuEntry.Configment.DisplayUesdTitleCount
			nudDisplayUesdTitleCount.Enabled = True
		End If

		Me.lblContcatBilibili = New Label With {
			.Anchor = AnchorStyles.Right Or AnchorStyles.Bottom,
			.Margin = New Padding(3, 3, 3, 3),
			.Name = "lnklblContcatBilibili",
			.Size = New Size(109, 12),
			.Text = "哔哩哔哩800189233",
			.ForeColor = SystemColors.Highlight
		}
		' 编辑框右下角显示
		Dim x = Me.RichTextBox1.Size.Width - lblContcatBilibili.Size.Width
		Dim y = Me.RichTextBox1.Size.Height - lblContcatBilibili.Size.Height
		Me.lblContcatBilibili.Location = New System.Drawing.Point(x, y)
		Me.RichTextBox1.Controls.Add(Me.lblContcatBilibili)

		AddHandler lblContcatBilibili.Click,
			Sub(sender2, e2)
				Process.Start("tencent://message/?Menu=yes&Site=qq&Menu=yes&uin=800189233")
			End Sub

		AddHandler lblContcatBilibili.MouseMove,
			Sub(sender2, e2)
				lblContcatBilibili.Cursor = Cursors.Hand
			End Sub

		AddHandler lblContcatBilibili.MouseLeave,
			Sub(sender2, e2)
				lblContcatBilibili.Cursor = Cursors.Default
			End Sub

		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub

	Private Sub FrmStartLiveSetting_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
		Dim action = Sub(sender2 As Object, e2 As EventArgs)
						 '
					 End Sub
		RemoveHandler lblContcatBilibili.Click, action
		RemoveHandler lblContcatBilibili.MouseMove, action
		RemoveHandler lblContcatBilibili.MouseLeave, action
	End Sub

	Private Async Function GetLiveStreamInfoAsync() As Task
		txtRtmpAddress.Text = "获取中..."
		Dim getStreamRst = Await m_Liver.GetLiveStreamAsync()
		If Not getStreamRst.Success Then Return
		m_ViewRoom.Stream = getStreamRst.LiveStream

		txtLiveRoomAddress.Text = "http://live.bilibili.com/" & m_ViewRoom.RealId
		txtRtmpAddress.Text = m_ViewRoom.Stream.RtmpAddress
		txtRtmpCode.Text = m_ViewRoom.Stream.RtmpCode
	End Function

	Private Async Function GetUsedTitleAsync() As Task
		Try
			If cmbRoomTitle.Items.Count > 0 Then cmbRoomTitle.Items.Clear()
			cmbRoomTitle.Enabled = False
			cmbRoomTitle.Items.AddRange(Await m_Liver.GetUsedTitleAsync)
		Catch ex As Exception
			Logger.WriteLine(ex)
		Finally
			cmbRoomTitle.Enabled = True
		End Try
	End Function

	Private Sub Label_SizeChanged(sender As Object, e As EventArgs) Handles lblCurrentArea.SizeChanged, lblPushStreamText.SizeChanged
		Dim lbl = DirectCast(sender, Label)
		If lbl IsNot Nothing Then
			If Not lbl.IsHandleCreated Then Return

			Select Case lbl.Name
				Case lblCurrentArea.Name
					btnChangeArea.Left = lbl.Left + lbl.Width + lbl.Margin.Right + btnChangeArea.Margin.Left
				Case lblPushStreamText.Name
					lblPushStreamResult.Left = lbl.Left + lbl.Width + lbl.Margin.Right + lblPushStreamResult.Margin.Left
			End Select
		End If
	End Sub

	Private Async Sub Button_ClickAsync(sender As Object, e As EventArgs) Handles btnChangeArea.Click, btnChangeLiveStatus.Click, btnCopyRtmpCode.Click, btnCopyRtmpAddres.Click, btnChangeTitle.Click, btnCopyLiveRoomAddres.Click
		Dim btn = DirectCast(sender, Button)
		If btn IsNot Nothing Then
			btn.Enabled = False

			Select Case btn.Name
				Case btnChangeArea.Name
#Region "更改分区"
					'frmSelectArea = New FrmSelectArea(m_Liver)
					m_FrmSelectArea = If(m_FrmSelectArea, New FrmSelectArea(m_Liver))
					m_FrmSelectArea.ShowFollowMousePosition(MouseLeaveAction.Hide, btn.Height, True)
					Dim selectArea = m_FrmSelectArea.SelectArea
					If selectArea Is Nothing OrElse selectArea.Id Is Nothing Then
						Exit Select
					End If
					If m_Liver.Room.Area.Id = selectArea.Id Then
						Windows2.DrawTipsTask(btn.Parent, "一样的分类，无需更新 (╬ﾟдﾟ)▄︻┻┳═一", 1000, False)
						Exit Select
					End If
					btnChangeArea.Text = "修改中..."
					Dim changeRst = Await m_Liver.UpdateAreaAsync(selectArea.Id)
					Windows2.DrawTipsTask(btn.Parent, If(changeRst.Success, "成功", "失败," & changeRst.Message), 1000, changeRst.Success)
					If changeRst.Success Then
						m_Liver.Room.Area = selectArea
						lblCurrentArea.Text = m_Liver.GetAreaFullName(selectArea)
						DanmuEntry.OnLiveRoomInfoChanged()
					End If
					btnChangeArea.Text = "修改分区"
#End Region
				Case btnChangeLiveStatus.Name
#Region "开关播"
					btnChangeLiveStatus.Text = If(Me.m_Liver.Room.Status = LiveStatus.Live, "关播中...", "直播中...")
					If Me.m_Liver.Room.Status = LiveStatus.Live Then
						Dim changeRst = Await m_Liver.StopLiveAsync()
						m_Liver.Room.Status = changeRst.NewestStatus
						Windows2.DrawTipsTask(btn.Parent, If(changeRst.Success, "成功", "失败," & changeRst.Message), 1000, changeRst.Success)
					Else
						Dim title = cmbRoomTitle.Text.Trim

						Dim changeRst = Await m_Liver.StartLiveAsync(m_Liver.Room.Area.Id, m_Liver.Room.Title)
						m_Liver.Room.Status = changeRst.NewestStatus
						m_Liver.Room.Title = title
						Windows2.DrawTipsTask(btn.Parent, If(changeRst.Success, "成功", "失败," & changeRst.Message), 1000, changeRst.Success)
					End If

					OnLiveStatusChanged()
#End Region
				Case btnChangeTitle.Name
#Region "更改标题"
					Dim title = cmbRoomTitle.Text.Trim
					If title = m_Liver.Room.Title Then
						Windows2.DrawTipsTask(btn.Parent, "一样的标题，无需更新 (╬ﾟдﾟ)▄︻┻┳═一", 1000, False)
						Exit Select
					End If
					Dim changeRst = Await m_Liver.UpdateTitleAsync(title)
					m_Liver.Room.Title = title
					Windows2.DrawTipsTask(btn.Parent, If(changeRst.Success, "成功", "失败," & changeRst.Message), 1000, changeRst.Success)

					If changeRst.Success Then
						Await GetUsedTitleAsync()
					End If
#End Region
				Case btnCopyLiveRoomAddres.Name
#Region "复制 直播间链接"
					Common.CopyToClipboard(txtLiveRoomAddress.Text, btn.Parent)
#End Region
				Case btnCopyRtmpAddres.Name
#Region "复制 RtmpAddress"
					Common.CopyToClipboard(txtRtmpAddress.Text, btn.Parent)
#End Region
				Case btnCopyRtmpCode.Name
#Region "复制 RtmpCode"
					Common.CopyToClipboard(txtRtmpCode.Text, btn.Parent)
#End Region
			End Select

			btn.Enabled = True
		End If
	End Sub

	Private Sub Label_Click(sender As Object, e As EventArgs) Handles lblMyRoomCenterPage.Click
		Dim lbl = DirectCast(sender, Label)
		If lbl IsNot Nothing Then
			Select Case lbl.Name
				Case lblMyRoomCenterPage.Name
					Process.Start(lblMyRoomCenterPage.Text)
			End Select
		End If
	End Sub

	Private Sub Label_MouseMove(sender As Object, e As MouseEventArgs) Handles lblMyRoomCenterPage.MouseMove
		lblMyRoomCenterPage.Cursor = Cursors.Hand
	End Sub

	Private Sub Label_MouseLeave(sender As Object, e As EventArgs) Handles lblMyRoomCenterPage.MouseLeave
		lblMyRoomCenterPage.Cursor = Cursors.Default
	End Sub


	Private Sub cmbRoomTitle_TextChanged(sender As Object, e As EventArgs) Handles cmbRoomTitle.TextChanged
		Dim titleLength = cmbRoomTitle.Text.Length
		lblTitleLength.Text = titleLength.ToStringOfCulture & "/" & m_Liver.Room.TitleMaxLength

		' 符合长度 绿色，超长 红色
		lblTitleLength.ForeColor = If(titleLength > m_Liver.Room.TitleMaxLength, Color.Red, Color.Green)
	End Sub

	Private Sub RadioButton_Click(sender As Object, e As EventArgs) Handles rdbtnDisplayAllUesdTitle.Click, rdbtnDislayPartOfUsedTitle.Click
		Dim rdbtn = DirectCast(sender, RadioButton)
		If rdbtn IsNot Nothing Then
			If Not rdbtn.IsHandleCreated Then Return

			Select Case rdbtn.Name
				Case rdbtnDisplayAllUesdTitle.Name
					If rdbtn.Checked Then
						DanmuEntry.Configment.DisplayUesdTitleCount = 0
						nudDisplayUesdTitleCount.Enabled = False
					End If
				Case rdbtnDislayPartOfUsedTitle.Name
					If rdbtn.Checked Then
						DanmuEntry.Configment.DisplayUesdTitleCount = CInt(nudDisplayUesdTitleCount.Value)
						nudDisplayUesdTitleCount.Enabled = True
					End If
			End Select
		End If
	End Sub

	Private Sub nudDisplayUesdTitleCount_ValueChanged(sender As Object, e As EventArgs) Handles nudDisplayUesdTitleCount.ValueChanged
		If Not nudDisplayUesdTitleCount.IsHandleCreated Then Return
		DanmuEntry.Configment.DisplayUesdTitleCount = CInt(nudDisplayUesdTitleCount.Value)
	End Sub

	Private Sub TextBox_MouseHover(sender As Object, e As EventArgs) Handles txtRtmpCode.MouseHover, txtRtmpAddress.MouseHover
		Dim txt = DirectCast(sender, TextBox)
		If txt IsNot Nothing Then
			Dim tip = txt.Text
			ToolTip1.Show(tip, txt)
		End If
	End Sub

	Public Sub OnLiveStatusChanged()
		Try
			btnChangeLiveStatus.Text = If(
				Me.m_Liver.Room.Status = LiveStatus.Live,
				"关闭直播", If(
				Me.m_Liver.Room.Status = LiveStatus.Break,
				"正在重连...",
				"开始直播"))
		Catch ex As Exception
			Logger.WriteLine(ex)
		End Try
	End Sub

	''' <summary>
	''' 直播间标题/分区改变
	''' </summary>
	''' <param name="e"></param>
	Public Sub OnRoomChanged(ByVal e As RoomChangedEventArgs)
		Dim area = If(m_FrmSelectArea Is Nothing,
			New AreaInfo With {
				.Id = e.AreaId.ToStringOfCulture,
				.Name = e.AreaName,
				.ParentId = e.ParentAreaId.ToStringOfCulture,
				.ParentName = e.ParentAreaName
			},
			m_FrmSelectArea.GetAreaInfo(e.AreaId.ToStringOfCulture))

		lblCurrentArea.Parent.BeginInvoke(Sub()
											  lblCurrentArea.Text = m_Liver.GetAreaFullName(area)
											  cmbRoomTitle.Text = e.Title
										  End Sub)
		m_Liver.Room.Area = area
	End Sub

	''' <summary>
	''' 停止直播
	''' </summary>
	''' <returns></returns>
	Public Async Function OnStopLiveAsync() As Task
        Try
            If Me.m_Liver.Room.Status = LiveStatus.Live Then
                Await m_Liver.StopLiveAsync
            End If
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try
    End Function
End Class