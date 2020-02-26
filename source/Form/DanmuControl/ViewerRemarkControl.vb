Imports System.Text.RegularExpressions
Imports ShanXingTech
Imports ShanXingTech.IO2.Database
Imports 姬娘

Public Class ViewerRemarkControl
    Inherits ViewerManageBaseControl

#Region "字段区"
    'Private m_DataTotalCountChanged As Boolean
    Private m_LoadFirst As Boolean
    Private m_DgvDataTabel As DataTable
    Private WithEvents m_Pager As PagerControl
#End Region

#Region "属性区"
    Private m_ViewerId As String
    Public Property ViewerId() As String
        Get
            Return m_ViewerId
        End Get
        Set(ByVal value As String)
            txtRemark.Text = String.Empty
            m_ViewerId = value
        End Set
    End Property

    Private m_ViewerIdOrViewerName As String
    Public Property ViewerIdOrViewerName() As String
        Get
            Return m_ViewerIdOrViewerName
        End Get
        Set(ByVal value As String)
            txtViewerIdOrViewerName.Text = value
            m_ViewerIdOrViewerName = value
        End Set
    End Property
#End Region

#Region "构造函数"
    Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。

        Me.SuspendLayout()

        txtViewerIdOrViewerName.SetCueBanner("输入UID或用户名")
        txtViewerIdOrViewerName.MaxLength = 15
        txtRemark.SetCueBanner("输入备注")
        txtRemark.MaxLength = 15

        'm_DataTotalCountChanged = True
        m_LoadFirst = True
        ' 放置于右下角
        m_Pager = New PagerControl(0, 0, 10, True) With {
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        }
        m_Pager.Location = New Point(Me.Width - m_Pager.Width, Me.Height - m_Pager.Height)
        Me.Controls.Add(m_Pager)

        dgvUserList.Size = New Size(dgvUserList.Width, Me.Height - m_Pager.Height - dgvUserList.Location.Y)
        dgvUserList.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        m_Pager.TabIndex = dgvUserList.TabIndex - 1

        Me.Dock = DockStyle.Fill

        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

#End Region


#Region "函数区"
    ''' <summary>
    ''' 获取数据总数
    ''' </summary>
    ''' <returns></returns>
    Private Function GetDataCount() As Long
        Dim sql = "select count(*) from ViewerRemark"
        Dim funcRst = SQLiteHelper.GetFirst(sql)

        Return If(funcRst Is Nothing, 0, CLng(funcRst))
    End Function

    Public Overrides Async Function LoadDataAsync() As Task
        'If m_DataTotalCountChanged Then
        '    m_DataTotalCountChanged = False
        '    Dim totalCount = GetDataCount()
        '    m_Pager.ReSet(totalCount)
        'End If
        If m_LoadFirst Then
            m_LoadFirst = False
            Dim totalCount = GetDataCount()
            m_Pager.ReSet(totalCount)
        End If

        Dim sql = $"select UserId as ID,UserNick as 昵称,Remark as 备注,CreatedTime as 记录时间 from ViewerRemark order by CreatedTime DESC limit {((m_Pager.CurrentPage - 1) * m_Pager.PageSize).ToStringOfCulture},{m_Pager.PageSize.ToStringOfCulture}"
        dgvUserList.DataSource = Nothing
        dgvUserList.ReadOnly = False
        m_DgvDataTabel = Await SQLiteHelper.GetDataTableAsync(sql, True, "序号")
        dgvUserList.DataSource = m_DgvDataTabel
    End Function

    Private Async Sub btnOK_ClickAsync(sender As Object, e As EventArgs) Handles btnOK.Click
		Dim uidOrUname = txtViewerIdOrViewerName.Text.Trim
		If uidOrUname.Length = 0 Then
			Windows2.DrawTipsTask(Me, "请输入UID或用户名" & RandomEmoji.Helpless, 2000, False, False)
			txtViewerIdOrViewerName.Focus()
			Return
		End If

		' 如果备注不为默认（跟昵称一样）则同时清空传入的观众id
		If uidOrUname <> m_ViewerIdOrViewerName AndAlso
			uidOrUname <> m_ViewerId Then
			m_ViewerId = String.Empty
		End If

		Dim remark = txtRemark.Text.Trim
		Dim isViewerNickAsRemark As Boolean
		If remark.Length = 0 Then
			Dim dlgRst = MessageBox.Show("你还没有输入备注，备注是否跟昵称一致？", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
			If dlgRst = DialogResult.Yes Then
				isViewerNickAsRemark = True
			Else
				Windows2.DrawTipsTask(Me, "请输入备注" & RandomEmoji.Helpless, 2000, False, False)
				txtRemark.Focus()
				Return
			End If
		End If

		Try
			btnOK.Parent.Enabled = False

			Dim user = Await DanmuEntry.GetUserInfoAsync(m_ViewerId, uidOrUname)
			If user.Uid.IsNullOrEmpty Then
                Common.ShowOperateResultTask(Me, False)
                Exit Try
            End If

            If isViewerNickAsRemark Then
                remark = user.Uname
            End If

            Dim rst = DanmuEntry.AddRemark(user.Uid, user.Uname, remark)
            Common.ShowOperateResultTask(Me, rst, If(rst, "成功", "已存在重复数据或者失败"))
            ' 只有在有数据更新时才重新加载数据
            If rst Then
                txtRemark.Text = String.Empty

                OnRemarkAdd()
            End If
        Catch ex As Exception
            Logger.WriteLine(ex)
        Finally
            btnOK.Parent.Enabled = True
        End Try
    End Sub

    Private Sub dgvUserList_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvUserList.DataBindingComplete
        MakeOperateColumn("删除", 5)
        AdjustDgv(dgvUserList, True, DataGridViewAutoSizeColumnsMode.AllCells, OperateColumn)
    End Sub

    ''' <summary>
    ''' 调整DataGridView控件 
    ''' 1.添加列标题 
    ''' 2.标题居中 
    ''' 4.不允许用户手动编辑 及新增删除行 只读
    ''' </summary>
    ''' <param name="dgv"></param>
    ''' <param name="showLastColumn">是否显示最后一列</param>
    ''' <param name="autoSizeColumnsMode">设置列宽自动调整模式</param>
    ''' <returns></returns>
    Private Function AdjustDgv(Of T As DataGridView)(ByRef dgv As T， ByVal showLastColumn As Boolean, ByVal autoSizeColumnsMode As DataGridViewAutoSizeColumnsMode, ByVal appendColumn As DataGridViewColumn) As Boolean
        Try
            ' 如果已经调整过 就不需要再次调整
            If dgv.ReadOnly Then
                Return True
            End If

            With dgv
                .SuspendLayout()

                If Not .Columns.Contains(appendColumn) Then
                    .Columns.Insert(.Columns.Count - 1, appendColumn)
                End If

                ' 是否隐藏最后一列
                .Columns.Item(.Columns.Count - 1).Visible = showLastColumn
                ' ID列不显示
                .Columns.Item("ID").Visible = False
                ' 时间格式跟随系统，弃用自定义格式
                '.Columns.Item("记录时间").DefaultCellStyle = New DataGridViewCellStyle With {.Format = "yyyy/MM/dd hh:mm:ss"}

                ' 标题剧中对齐
                .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                ' 所有内容全部显示粗来
                .AutoSizeColumnsMode = autoSizeColumnsMode
                ' 标题不换行
                .ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False

                .AllowDrop = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .ReadOnly = True

                .ResumeLayout(False)
                .PerformLayout()
            End With

            Return True
        Catch ex As Exception
            Logger.WriteLine(ex)

            Return False
        End Try
    End Function

    Private Sub dgvUserList_CellMouseUp(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgvUserList.CellMouseUp
        If e.ColumnIndex <= -1 OrElse e.RowIndex = -1 Then Return

        txtViewerIdOrViewerName.Text = dgvUserList.GetCurrentRowCellValue("昵称")
        txtRemark.Text = dgvUserList.GetCurrentRowCellValue("备注")
        ' 删除备注
        If dgvUserList.Columns(e.ColumnIndex).Name = OperateColumn.Name Then
            Dim userId = dgvUserList.GetCurrentRowCellValue("ID")
            Dim rst = DanmuEntry.DeleteRemark(userId)
            Common.ShowOperateResultTask(Me, rst)

            ' 删除表格中对应的数据
            If rst Then
                OnRemarkDelete(userId)
            End If
        End If
	End Sub

    ''' <summary>
    ''' 删除备注
    ''' </summary>
    ''' <param name="userId"></param>
    Public Sub OnRemarkDelete(ByVal userId As String)
        m_DgvDataTabel.DeleteRowAfterSortOrNot("ID", userId)
        'm_DataTotalCountChanged = True
        m_Pager.OnDataDelete(1)
    End Sub

    ''' <summary>
    ''' 添加备注后调用
    ''' </summary>
    Public Sub OnRemarkAdd()
        'm_DataTotalCountChanged = True
        m_Pager.OnDataAdd(1)
        ReLoadDataTask()
    End Sub

    Private Sub m_Pager_PageNavigated(sender As Object, e As PagerControl.NavigatePageEventArgs) Handles m_Pager.PageNavigated
        ReLoadDataTask()
    End Sub

    Private Sub m_Pager_PageSizeChanged(sender As Object, e As EventArgs) Handles m_Pager.PageSizeChanged
        'm_DataTotalCountChanged = True
        ReLoadDataTask()
    End Sub

    Private Sub txtViewerIdOrViewerName_KeyUp(sender As Object, e As KeyEventArgs) Handles txtViewerIdOrViewerName.KeyUp
        If e.KeyCode = Keys.Enter Then
            btnOK.PerformClick()
        End If
    End Sub
#End Region
End Class
