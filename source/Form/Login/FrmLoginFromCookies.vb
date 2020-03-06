Imports System.Net
Imports ShanXingTech

Public Class FrmLoginFromCookies

#Region "属性区"
    Private Const CS_NOCLOSE = &H200

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            ' 关闭按钮不可用
            Dim cr As CreateParams = MyBase.CreateParams
            cr.ClassStyle = cr.ClassStyle Or CS_NOCLOSE
            Return cr
        End Get
    End Property

    Private m_LoginedCookies As Net.CookieContainer
    Public ReadOnly Property LoginedCookies As Net.CookieContainer
        Get
            Return m_LoginedCookies
        End Get
    End Property
#End Region

    Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Me.TopMost = True
        Me.StartPosition = FormStartPosition.CenterParent
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Dim domain = txtDomain.Text.Trim
        If domain.IsNullOrEmpty Then
            Windows2.DrawTipsTask(Me, "你是耍流氓嘛???还没有输入Domain呢" & RandomEmoji.Shock, 1000, False, False)
            Return
        End If

        Dim cookeis = txtCookies.Text.Trim
        If cookeis.IsNullOrEmpty Then
            Windows2.DrawTipsTask(Me, "你是耍流氓嘛???还没有输入Cookies呢" & RandomEmoji.Shock, 1000, False, False)
            Return
        End If

        m_LoginedCookies.GetFromKeyValuePairs(cookeis, domain)

        Me.Close()
    End Sub
End Class