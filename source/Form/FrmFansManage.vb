
Imports ShanXingTech
Imports ShanXingTech.IO2.Database

Public Class FrmFansManage
#Region "字段区"
    Private m_FansInfoEditControl As FansInfoEditControl
    Private m_Tabel As DataTable

#End Region

    Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。

        If m_FansInfoEditControl Is Nothing Then
            m_FansInfoEditControl = New FansInfoEditControl(FansInfoEditControl.EditMode.Modify)
            gbxEditMoudle.Controls.Add(m_FansInfoEditControl)
        End If

        gbxEditMoudle.Size = New Size(m_FansInfoEditControl.Size.Width + m_FansInfoEditControl.Padding.Left * 2, m_FansInfoEditControl.Size.Height + m_FansInfoEditControl.Padding.Top * 4)
        m_FansInfoEditControl.Location = New Point(m_FansInfoEditControl.Margin.Left, m_FansInfoEditControl.Padding.Top * 3)

        dgvInfo1.Size = New Size(gbxEditMoudle.Width, Me.ClientRectangle.Height - gbxEditMoudle.Bottom - lblTotal.Height * 2)
        dgvInfo1.Location = New Point(gbxEditMoudle.Location.X, gbxEditMoudle.Location.Y + gbxEditMoudle.Height + gbxEditMoudle.Padding.Bottom * 2)

        lblTotal.Location = New Point(gbxEditMoudle.Location.X, dgvInfo1.Location.Y + dgvInfo1.Height + dgvInfo1.Margin.Bottom)
        lblDgv1TotalRow.Location = New Point(lblTotal.Location.X + lblTotal.Size.Width, dgvInfo1.Location.Y + dgvInfo1.Height + dgvInfo1.Margin.Bottom)

        m_Tabel = New DataTable
    End Sub

    Private Async Sub FrmFansManage_LoadAsync(sender As Object, e As EventArgs) Handles MyBase.Load
        SQLiteHelper.Init("C:\ProgramData\ShanXingTech\姬娘\DanmuHime.db")

        Dim sql = "select UserId as ID, OriginalNick as 昵称, RemarkNick as 备注, AttentTimestamp as 关注时间 from Fans order by AttentTimestamp desc limit 0,10"
        m_Tabel = Await SQLiteHelper.GetDataTableAsync(sql, True, "序号")
        dgvInfo1.DataSource = m_Tabel
    End Sub

#Region "dgv操作区"
    Private Sub dgvInfo1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvInfo1.CellClick
        ' 选中一整行
        If e.RowIndex <> -1 Then
            EnsureLock(FansInfoEditControl.EditMode.Modify)
            ShowInfo()
        End If
    End Sub

    Private Sub ShowInfo()
        m_FansInfoEditControl.txtId.Text = dgvInfo1.CurrentRow.Cells("ID").Value.ToString
        m_FansInfoEditControl.txtNick.Text = dgvInfo1.CurrentRow.Cells("昵称").Value.ToString
        Dim attentTimestamp = If(DBNull.Value.Equals(dgvInfo1.CurrentRow.Cells("关注时间").Value),
            0, CInt(dgvInfo1.CurrentRow.Cells("关注时间").Value))
        m_FansInfoEditControl.dtpAttentionTime.Value = attentTimestamp.ToTimeStampTime
        m_FansInfoEditControl.txtRemark.Text = dgvInfo1.CurrentRow.Cells("备注").Value.ToString
    End Sub

    Private Sub EnsureLock(ByVal editMode As FansInfoEditControl.EditMode)
        m_FansInfoEditControl.InitControl(editMode)
    End Sub

    Private Sub dgvInfo1_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvInfo1.DataBindingComplete
        dgvInfo1.AdjustDgv()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New Form With {
            .AutoSize = True,
            .StartPosition = FormStartPosition.CenterScreen,
            .Text = "粉丝备注管理"，
            .FormBorderStyle = FormBorderStyle.FixedSingle,
            .MaximizeBox = False,
            .MinimizeBox = False,
            .Icon = Me.Icon
        }
        Dim a As New FansInfoEditControl(FansInfoEditControl.EditMode.Add)

        frm.Size = New Size(a.Size.Width + a.Padding.Left * 2, a.Size.Height + a.Padding.Top * 4)
        frm.Controls.Add(a)
        frm.ShowDialog()
    End Sub


#End Region

End Class