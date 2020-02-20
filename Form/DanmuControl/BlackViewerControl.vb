Imports ShanXingTech

Public Class BlackViewerControl
    Inherits ViewerManageBaseControl

#Region "字段区"
    Private ReadOnly m_DgvDataTabel As DataTable
    Private ReadOnly m_RoomSilentControl As RoomGlobalSilentControl
#End Region

#Region "构造函数"
    Sub New(ByVal isUper As Boolean, ByVal isAdmin As Boolean)

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。
		Me.SuspendLayout()

		txtUidOrUname.SetCueBanner("输入UID或用户名")
		InitBlockHourCombobox()
		m_DgvDataTabel = New DataTable()
		m_DgvDataTabel.Columns.Add(New DataColumn(NameOf(RoomBlackViewerEntity.DisplayOnDgv.黑名单Id), GetType(Integer)))
		m_DgvDataTabel.Columns.Add(New DataColumn(NameOf(RoomBlackViewerEntity.DisplayOnDgv.用户名称), GetType(String)))
		m_DgvDataTabel.Columns.Add(New DataColumn(NameOf(RoomBlackViewerEntity.DisplayOnDgv.解禁时间), GetType(Date)))
		' 非UP不能使用全局禁言功能
		If isUper Then
			If m_RoomSilentControl Is Nothing Then
				m_RoomSilentControl = New RoomGlobalSilentControl(isAdmin OrElse isUper)
			End If
			tlpControlContainer.Controls.Add(m_RoomSilentControl, 0, 0)
			For i = 0 To tlpControlContainer.RowStyles.Count - 1
				tlpControlContainer.RowStyles(i).SizeType = SizeType.AutoSize
			Next
		End If

		Me.Dock = DockStyle.Fill

		dgvUserList.Anchor = AnchorStyles.Left Or AnchorStyles.Top Or AnchorStyles.Right Or AnchorStyles.Bottom


		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub

#End Region


#Region "函数区"
	Private Sub InitBlockHourCombobox()
        If cmbBlockHourItems.Items.Count = 0 Then
            Dim hotSale = {"1", "8", "12", "24"}
            cmbBlockHourItems.Items.AddRange(hotSale)
            Dim blockHours(719) As String
            For i = 1 To 720
                blockHours(i - 1) = i.ToStringOfCulture
            Next
            cmbBlockHourItems.AutoCompleteCustomSource.AddRange(blockHours)
            cmbBlockHourItems.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            cmbBlockHourItems.AutoCompleteSource = AutoCompleteSource.CustomSource

        End If

        cmbBlockHourItems.SetCueBanner("封禁时间1-720小时")
    End Sub

    Private Async Sub btnOK_ClickAsync(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim uidOrUname = txtUidOrUname.Text.Trim
        If uidOrUname.Length = 0 Then
            Windows2.DrawTipsTask(If(Me.Parent, Me), "请输入UID或用户名" & RandomEmoji.Helpless, 2000, False, False)
            txtUidOrUname.Focus()
            Return
        End If

        Dim hour = -1
        If Not Integer.TryParse(cmbBlockHourItems.Text.Trim, hour) Then
            Windows2.DrawTipsTask(If(Me.Parent, Me), "封禁时间不合法" & RandomEmoji.Helpless, 2000, False, False)
            cmbBlockHourItems.Focus()
            Return
        End If

        If hour < 1 OrElse hour > 720 Then
            Windows2.DrawTipsTask(If(Me.Parent, Me), "时间范围‘1-720’小时" & RandomEmoji.Helpless, 2000, False, False)
            cmbBlockHourItems.Focus()
            Return
        End If

        Dim rst = Await BilibiliApi.RoomBlackViewerAsync(uidOrUname, hour)
        ShowOperateResultTask(rst.Success, If(rst.Success, "成功", rst.Message))
    End Sub

    Private Sub dgvUserList_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvUserList.DataBindingComplete
        MakeOperateColumn("撤销", 3)
        dgvUserList.AdjustDgv(True, DataGridViewAutoSizeColumnsMode.AllCells, OperateColumn)
    End Sub

    Private Async Sub dgvUserList_CellMouseUpAsync(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvUserList.CellMouseUp
        If e.ColumnIndex <= -1 OrElse e.RowIndex = -1 Then Return

        txtUidOrUname.Text = dgvUserList.GetCurrentRowCellValue(NameOf(RoomBlackViewerEntity.DisplayOnDgv.用户名称))
        If dgvUserList.Columns(e.ColumnIndex).Name = OperateColumn.Name Then
            Dim blackId = dgvUserList.GetCurrentRowCellValue(NameOf(RoomBlackViewerEntity.DisplayOnDgv.黑名单Id))
            Dim rst = Await BilibiliApi.RoomUnBlackViewerAsync(blackId)
            ShowOperateResultTask(rst.Success, If(rst.Success, "成功", rst.Message))

            If rst.Success Then
                m_DgvDataTabel.DeleteRowAfterSortOrNot(NameOf(RoomBlackViewerEntity.DisplayOnDgv.黑名单Id), blackId)
                Dim danmu = $"用户 {txtUidOrUname.Text} 已被管理员解除禁言"
                Dim EventArgs As New 姬娘插件.Events.RoomViewerBlockedEventArgs(danmu)
                OpCodeParser.TransferDanmuEvent("RoomViewerUnBlocked", EventArgs)
            End If
        End If
    End Sub

    Public Overrides Async Function LoadDataAsync() As Task
		Await GetRoomBlackListAsync(1)
	End Function

    ''' <summary>
    ''' 获取房管
    ''' </summary>
    ''' <param name="page">某页</param>
    ''' <returns></returns>
    Private Async Function GetRoomBlackListAsync(ByVal page As Integer) As Task
        Dim getRst = Await BilibiliApi.GetRoomBlockListAsync(page)
        If Not getRst.Success Then
            ShowOperateResultTask(False)
            Return
        End If

        Dim json = getRst.Message
        Dim jsonRoot As RoomBlackViewerEntity.Root = Nothing
        Try
            jsonRoot = MSJsSerializer.Deserialize(Of RoomBlackViewerEntity.Root)(json)
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try
        If jsonRoot Is Nothing Then
            ShowOperateResultTask(False)
            Return
        End If

        ' 展现数据前清空已有数据
        If m_DgvDataTabel.Rows.Count > 0 Then
            m_DgvDataTabel.Rows.Clear()
        End If
        For Each blackUser In jsonRoot.data
            AddToTable(New RoomBlackViewerEntity.DisplayOnDgv With {
            .黑名单Id = blackUser.id,
            .用户名称 = blackUser.uname,
            .解禁时间 = CDate(blackUser.ctime)
                       })
        Next
        dgvUserList.DataSource = m_DgvDataTabel
    End Function

	Private Sub AddToTable(ByVal item As RoomBlackViewerEntity.DisplayOnDgv)
		If item Is Nothing Then Return

		Dim row = m_DgvDataTabel.NewRow
		row(NameOf(item.黑名单Id)) = item.黑名单Id
		row(NameOf(item.用户名称)) = item.用户名称
		row(NameOf(item.解禁时间)) = item.解禁时间

		m_DgvDataTabel.Rows.Add(row)
	End Sub

	Private Sub BlackViewerControl_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
		If m_RoomSilentControl Is Nothing Then Return
		dgvUserList.Size = New Size(tlpControlContainer.Width, tlpControlContainer.Height - Panel2.Height - Panel2.Margin.Top - Panel2.Margin.Bottom - m_RoomSilentControl.Height - m_RoomSilentControl.Margin.Top - m_RoomSilentControl.Margin.Bottom - dgvUserList.Margin.Top - dgvUserList.Margin.Bottom)
	End Sub
#End Region
End Class
