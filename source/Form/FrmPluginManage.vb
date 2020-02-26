Public Class FrmPluginManage

#Region "字段区"
    Private m_Tabel As DataTable
#End Region

#Region "构造函数"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub
#End Region

#Region "函数区"
    Private Sub FrmRoomViewerManage_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ' 非软件退出时只隐藏，不关闭窗体以便重用
        If e.CloseReason = CloseReason.UserClosing Then
            Me.Hide()
            e.Cancel = True
        End If
    End Sub

    Private Sub FrmPluginManage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dgvInfo1.DataSource = m_Tabel
    End Sub
#End Region

End Class