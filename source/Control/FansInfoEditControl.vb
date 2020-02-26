Imports System.ComponentModel
Imports System.Text
Imports System.Text.RegularExpressions
Imports ShanXingTech
Imports ShanXingTech.IO2.Database
Imports ShanXingTech.Net2

Public Class FansInfoEditControl
#Region "枚举"
    Enum EditMode

        <Description("新增")>
        Add
        <Description("保存")>
        Modify
    End Enum
#End Region

#Region "属性区"
    Private m_Mode As EditMode
    ''' <summary>
    ''' 编辑模式
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Mode As EditMode
        Get
            Return m_Mode
        End Get
    End Property

    Public Property OldRemark As String

#End Region

    Sub New(ByVal editMode As EditMode)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        txtRemark.MaxLength = 40
        dtpAttentionTime.Format = DateTimePickerFormat.Custom
        dtpAttentionTime.CustomFormat = "yyyy-MM-dd HH:mm:ss"

        Me.m_Mode = editMode
        InitControl(m_Mode)
    End Sub


#Region "函数区"

#End Region
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="editMode"></param>
    Public Sub InitControl(ByVal editMode As EditMode)
        Dim enabledEdit = (EditMode.Add = editMode)

        txtId.ReadOnly = Not enabledEdit
        txtId.BackColor = If(enabledEdit, SystemColors.Window, SystemColors.Control)
        ' 昵称无论怎样都不能自己编辑
        txtNick.ReadOnly = True
        txtNick.BackColor = SystemColors.Control
        OldRemark = txtRemark.Text.Trim
        dtpAttentionTime.Enabled = True
        txtRemark.ReadOnly = False
        txtRemark.BackColor = SystemColors.Window

        If m_Mode <> editMode Then
            m_Mode = editMode
        End If
    End Sub

    Private Sub FansInfoModifyControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        InitControl(EditMode.Add)
        txtId.Text = String.Empty
        txtNick.Text = String.Empty
        dtpAttentionTime.Value = Date.Now
        txtRemark.Text = String.Empty

        txtId.Focus()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim checkRst = EnsureCanStroe()
        If Not checkRst.Yes Then Return

        Try
            SQLiteHelper.ExecuteNonQuery(checkRst.StroeSql)
            lblTips.Text = "已保存"
            lblTips.Visible = True
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try
    End Sub

    Private Function EnsureCanStroe() As (Yes As Boolean, StroeSql As String)
        Dim funcRst = (False, String.Empty)


        Dim remarkNick = txtRemark.Text.Trim
        If remarkNick.Length = 0 Then
            Windows2.DrawTipsTask(Me, "你是耍流氓嘛???还没有输入备注呢!~", 1000, False, False)
            Return funcRst
        End If

        If OldRemark.Length > 0 AndAlso OldRemark = remarkNick Then
            Windows2.DrawTipsTask(Me, "新备注与旧备注一致，无需更改!~", 1000, False, False)
            Return funcRst
        End If

        Dim userId = txtId.Text.Trim
        If Not Regex.IsMatch(userId， "\d+", RegexOptions.Compiled) Then
            Windows2.DrawTipsTask(Me, "用户ID格式不对头，应该全为数字" & RandomEmoji.Angry, 1000, False, False)
            Return funcRst
        End If


        Dim sql = $"INSERT
                OR REPLACE INTO Fans (
	                UserId,
                    OriginalNick,
	                RemarkNick,
	                AttentTimestamp
                )
                VALUES
	                (
		            {userId},
		            '{txtNick.Text}',
		            '{remarkNick}',
		            {dtpAttentionTime.Value.ToTimeStampString(TimePrecision.Second)}
                    );"

        Return (True, sql)
    End Function

    Private Async Sub txtId_LeaveAsync(sender As Object, e As EventArgs) Handles txtId.Leave
        Dim userId = txtId.Text.Trim
        If EditMode.Add = m_Mode AndAlso userId.Length > 0 Then
            Dim sql = "select userId from Fans where userId = " & userId
            Dim tempUserId = SQLiteHelper.GetFirst(sql)
            If Not tempUserId Is Nothing Then
                lblTips.Text = "已存在该粉丝信息"
                lblTips.Visible = True
                txtId.Focus()
            Else
                lblTips.Visible = False

                ' 获取粉丝的昵称并显示
                If txtNick.Text.Length = 0 Then txtNick.Text = Await DanmuEntry.GetFansNickAsync(userId)
            End If
            End If
    End Sub


End Class
