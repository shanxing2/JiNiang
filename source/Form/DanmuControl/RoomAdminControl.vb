Imports ShanXingTech

Public Class RoomAdminControl
    Inherits ViewerManageBaseControl

#Region "字段区"
    Private ReadOnly m_DgvDataTabel As DataTable
    Private ReadOnly m_IsUper As Boolean
    Private ReadOnly m_ViewRoomId As String
    Private WithEvents m_Pager As PagerControl
#End Region

#Region "构造函数"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="isUper">是否为主播，只有主播才可以设置直播间管理员，否则只能查看</param>
    ''' <param name="viewRoomId"></param>
    Sub New(ByVal isUper As Boolean, ByVal viewRoomId As String)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        m_IsUper = isUper
        m_ViewRoomId = viewRoomId

        Me.SuspendLayout()

        txtUidOrUname.SetCueBanner("输入UID或用户名")
        txtUidOrUname.Enabled = m_IsUper
        ' 主播不能设置页大小(因为跟主播绑定的管理员或者API不能设置页大小，BilibiliApi.GetRoomAdminByAnchorAsync)
        ' 放置于右下角
        m_Pager = New PagerControl(0, 0, 10, Not m_IsUper) With {
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        }
        m_Pager.Location = New Point(Me.Width - m_Pager.Width, Me.Height - m_Pager.Height)
        Me.Controls.Add(m_Pager)

        dgvUserList.Size = New Size(dgvUserList.Width, Me.Height - m_Pager.Height - dgvUserList.Location.Y)
        dgvUserList.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right

        btnAppoint.Enabled = txtUidOrUname.Enabled

        m_DgvDataTabel = New DataTable()
        m_DgvDataTabel.Columns.Add(New DataColumn(NameOf(RoomAdminEntity.DisplayOnDgv.用户ID), GetType(Integer)))
        m_DgvDataTabel.Columns.Add(New DataColumn(NameOf(RoomAdminEntity.DisplayOnDgv.用户名称), GetType(String)))
        m_DgvDataTabel.Columns.Add(New DataColumn(NameOf(RoomAdminEntity.DisplayOnDgv.任命时间), GetType(Date)))
        Me.Dock = DockStyle.Fill

        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

#End Region


#Region "函数区"
    Private Sub dgvUserList_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvUserList.DataBindingComplete
        If Me.Parent Is Nothing Then Return

        lblAdminCount.Text = $"当前房管数：{m_DgvDataTabel.Rows.Count.ToStringOfCulture}，共有：{m_Pager.TotalCount.ToStringOfCulture}/100"
        If m_IsUper Then MakeOperateColumn("撤销", 3)
        dgvUserList.AdjustDgv(True, DataGridViewAutoSizeColumnsMode.AllCells, OperateColumn)
    End Sub

    Private Async Sub dgvUserList_CellMouseUpAsync(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvUserList.CellMouseUp
        If e.ColumnIndex <= -1 OrElse e.RowIndex = -1 Then Return

        ' 撤销房管
        txtUidOrUname.Text = dgvUserList.GetCurrentRowCellValue(NameOf(RoomAdminEntity.DisplayOnDgv.用户名称))
        If dgvUserList.Columns(e.ColumnIndex).Name = OperateColumn?.Name Then
            Dim viewerId = dgvUserList.GetCurrentRowCellValue(NameOf(RoomAdminEntity.DisplayOnDgv.用户ID))
            Dim rst = Await BilibiliApi.RoomUnAppointAdminAsync(viewerId)
            Common.ShowOperateResultTask(Me, rst)

            ' 撤销成功之后，手动删除Dgv中的数据
            If rst.Success Then
                m_DgvDataTabel.DeleteRowAfterSortOrNot(NameOf(RoomAdminEntity.DisplayOnDgv.用户ID), viewerId)
                dgvUserList_DataBindingComplete(Nothing, New DataGridViewBindingCompleteEventArgs(System.ComponentModel.ListChangedType.ItemDeleted))
                m_Pager.OnDataDelete(1)
            End If
        End If
    End Sub

    Private Async Sub btnAppoint_ClickAsync(sender As Object, e As EventArgs) Handles btnAppoint.Click
        Dim uidOrUname = txtUidOrUname.Text.Trim
        If uidOrUname.Length = 0 Then
            Windows2.DrawTipsTask(If(Me.Parent, Me), "请输入UID或用户名" & RandomEmoji.Helpless, 2000, False, False)
            txtUidOrUname.Focus()
            Return
        End If

        Dim rst = Await BilibiliApi.RoomAppointAdminAsync(uidOrUname)
        Common.ShowOperateResultTask(Me, rst)

        If rst.Success Then
            Await GetRoomAdminsAsync(1)
        End If
    End Sub

    Public Overrides Async Function LoadDataAsync() As Task
        Await GetRoomAdminsAsync(1)
    End Function

    ''' <summary>
    ''' 获取房管
    ''' </summary>
    ''' <param name="page">某页</param>
    ''' <returns></returns>
    Private Async Function GetRoomAdminsAsync(ByVal page As Integer) As Task
        Try
            Me.Enabled = False
            Dim rst = Await InternalGetRoomAdminsAsync(page)
            m_Pager.ReSet(rst.TotalCount)
            dgvUserList.DataSource = Nothing
            dgvUserList.ReadOnly = False
            dgvUserList.DataSource = m_DgvDataTabel
        Catch ex As Exception
            Logger.WriteLine(ex)
        Finally
            Me.Enabled = True
        End Try
    End Function

    ''' <summary>
    ''' 获取房管
    ''' </summary>
    ''' <param name="page">某页</param>
    ''' <returns></returns>
    Private Async Function InternalGetRoomAdminsAsync(ByVal page As Integer) As Task(Of (TotalPage As Integer, TotalCount As Integer))
        Dim getRst = If(m_IsUper,
            Await BilibiliApi.GetRoomAdminByAnchorAsync(page),
            Await BilibiliApi.GetRoomAdminByRoomAsync(m_ViewRoomId, page, m_Pager.PageSize))
        If Not getRst.Success Then
            Common.ShowOperateResultTask(Me, False)
            Return (0, 0)
        End If

        Dim json = getRst.Message
        Dim jsonRoot As RoomAdminEntity.Root = Nothing
        Try
            jsonRoot = MSJsSerializer.Deserialize(Of RoomAdminEntity.Root)(json)
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try
        If jsonRoot Is Nothing Then
            Common.ShowOperateResultTask(Me, False)
            Return (0, 0)
        End If

        ' 展现数据前清空已有数据
        If m_DgvDataTabel.Rows.Count > 0 Then
            m_DgvDataTabel.Rows.Clear()
        End If
        For Each admin In jsonRoot.data.data
            AddToTable(New RoomAdminEntity.DisplayOnDgv With {
            .用户ID = admin.uid,
            .用户名称 = admin.uname,
            .任命时间 = admin.ctime
                       })
        Next

        Return (jsonRoot.data.page.total_page, jsonRoot.data.page.total_count)
    End Function

    Private Sub AddToTable(ByVal item As RoomAdminEntity.DisplayOnDgv)
        If item Is Nothing Then Return

        Dim row = m_DgvDataTabel.NewRow
        row(NameOf(item.用户ID)) = item.用户ID
        row(NameOf(item.用户名称)) = item.用户名称
        row(NameOf(item.任命时间)) = item.任命时间

        m_DgvDataTabel.Rows.Add(row)
    End Sub

    Private Async Sub m_Pager_PageNavigated(sender As Object, e As PagerControl.NavigatePageEventArgs) Handles m_Pager.PageNavigated
        Await GetRoomAdminsAsync(e.NavigatePage)
    End Sub

    Private Async Sub m_Pager_PageSizeChanged(sender As Object, e As EventArgs) Handles m_Pager.PageSizeChanged
        Await GetRoomAdminsAsync(1)
    End Sub

#End Region
End Class
