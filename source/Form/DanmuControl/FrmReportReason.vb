Imports ShanXingTech

Public Class FrmReportReason
    Private m_ReportReason As BilibiliApi.ReportReason

    Public ReadOnly Property ReportReason As BilibiliApi.ReportReason
        Get
            Return m_ReportReason
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="viewerNick">观众昵称</param>
    ''' <param name="viewerDanmu">观众弹幕</param>
    Public Sub New(ByVal viewerNick As String, ByVal viewerDanmu As String)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        cmbReportReason.DropDownStyle = ComboBoxStyle.DropDownList
        m_ReportReason = BilibiliApi.ReportReason.无
        lblViewerNick.Text = viewerNick
        lblViewerDanmu.Text = viewerDanmu
    End Sub

    Private Sub lblOK_Click(sender As Object, e As EventArgs) Handles lblOK.Click, lblCancel.Click
        Dim lbl = DirectCast(sender, Label)
        If lbl Is Nothing Then Return

        Select Case lbl.Name
            Case lblOK.Name
                Dim reasonValue = cmbReportReason.Text
                If reasonValue.Length = 0 Then Return

                If [Enum].TryParse(reasonValue, m_ReportReason) Then
                    Me.Close()
                End If
            Case lblCancel.Name
                Me.Close()
        End Select
    End Sub

    Private Sub cmbReportReason_KeyUp(sender As Object, e As KeyEventArgs) Handles cmbReportReason.KeyUp
        If e.KeyCode = Keys.Enter Then
            lblOK_Click(lblOK, New EventArgs)
        End If
    End Sub

    Private Async Sub cmbReportReason_Click(sender As Object, e As EventArgs) Handles cmbReportReason.Click
        If cmbReportReason.Items.Count > 0 Then Return

        Try
            Dim rst = Await BilibiliApi.GetRoomReportDanmuReasonAsync
            If Not rst.Success Then
                Common.ShowOperateResultTask(Me, rst.Success, "获取举报弹幕理由失败" & RandomEmoji.Sad)
                Return
            End If

            Dim root = MSJsSerializer.Deserialize(Of ReportReasonEntity.Root)(rst.Message)
            If root Is Nothing Then
                Common.ShowOperateResultTask(Me, rst.Success, "获取举报弹幕理由失败" & RandomEmoji.Sad)
                Return
            End If

            For Each item In root.data
                cmbReportReason.Items.Add(item.reason)
            Next
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try
    End Sub

    Private Sub lblOK_MouseMove(sender As Object, e As MouseEventArgs) Handles lblOK.MouseMove, lblCancel.MouseMove
        Me.Cursor = Cursors.Hand
        Dim lbl = DirectCast(sender, Label)
        If lbl Is Nothing Then Return

        lbl.ForeColor = SystemColors.Highlight
    End Sub

    Private Sub lblOK_MouseLeave(sender As Object, e As EventArgs) Handles lblOK.MouseLeave, lblCancel.MouseLeave
        Me.Cursor = DefaultCursor

        Dim lbl = DirectCast(sender, Label)
        If lbl Is Nothing Then Return

        lbl.ForeColor = SystemColors.ControlText
    End Sub
End Class