Imports System.Net
Imports ShanXingTech
Imports ShanXingTech.Net2
Imports 姬娘

''' <summary>
''' 登录器主要用来
'''     1.快速登录以及验证历史Cookies的有效性；
'''     2.登录新账号
''' </summary>
Public Class LoginManager
#Region "事件区"
    Public Event UserEnsured As EventHandler(Of UserEnsuredEventArgs)

    Public Class UserEnsuredEventArgs
        Inherits EventArgs
        Public ReadOnly Property UserId As String
        Public ReadOnly Property ViewRoomId As String
        Public ReadOnly Property LoginResult As LoginResult

        Public Sub New(ByVal userId As String, ByVal viewRoomId As String, ByVal loginResult As LoginResult)
            Me.UserId = userId
            Me.ViewRoomId = viewRoomId
            Me.LoginResult = loginResult
        End Sub
    End Class
#End Region

#Region "枚举区"
    Enum LoginMode
        ''' <summary>
        ''' 使用浏览器登录
        ''' </summary>
        Browser
        ''' <summary>
        ''' 使用cookie登录
        ''' </summary>
        Cookies
        ''' <summary>
        ''' 扫码登录
        ''' </summary>
        QRCode
    End Enum
#End Region

#Region "字段区"
    Private ReadOnly m_User As UserInfo
#End Region
#Region "属性区"
    Private ReadOnly m_HttpHeadersParam As Dictionary(Of String, String)

#End Region

#Region "常量区"
    Private Const NavigateUrl = "http://link.bilibili.com/p/center/index#/my-room/start-live"
    ''' <summary>
    ''' 游客（身份登录）Id
    ''' </summary>
    Public Const NotLoginUserId As String = "0"
    ''' <summary>
    ''' 我先瞅瞅 直播间Id
    ''' </summary>
    Public Const SeeBeforeViewRoomId As Integer = -1
#End Region

#Region "构造函数区"
    Sub New()

        '
    End Sub


    ''' <summary>
    ''' 类构造函数
    ''' 类之内的任意一个静态方法第一次调用时调用此构造函数
    ''' 而且程序生命周期内仅调用一次
    ''' </summary>
    Sub New(ByVal user As UserInfo)
        m_HttpHeadersParam = New Dictionary(Of String, String) From {
            {"User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:55.0) Gecko/20100101 Firefox/55.0"},
            {"Connection", "keep-alive"},
            {"Cache-Control", "no-cache"},
            {"Accept-Encoding", "gzip, deflate"}
        }

        Me.m_User = user
    End Sub
#End Region

#Region "函数区"
    Public Async Function TryLoginAsync() As Task(Of LoginResult)
        ' 此函数一定要确保有一个地方引发 UserEnsured 事件

        Dim loginRst As LoginResult
        Dim loginWay As LoginMode
        Using frm As New FrmQuickLogin(m_User)
            frm.ShowDialog()

            loginRst = frm.LoginResult
            loginWay = frm.LoginMode
        End Using

        ' 如果能确定了用户（Id和直播间号），那就引发用户确定事件以便其他地方做出相应更改
        If LoginResult.Yes = loginRst OrElse
            LoginResult.UserOnly = loginRst OrElse
            LoginResult.NotLogin = loginRst Then
            RaiseEvent UserEnsured(Nothing, New UserEnsuredEventArgs(m_User.Id, m_User.ViewedRoomId.ToStringOfCulture, loginRst))
        End If

        Try
            If LoginResult.Yes = loginRst Then
                If m_User.Id.IsNullOrEmpty Then
                    ' 选择登录新账号直接弹出登录窗口
                    loginRst = Await ShowLoginFormAsync(loginWay)
                Else
                    ' 如果能获取到之前登录后的cookies并且能用cookie获取到用户信息，那就不需要再登录
                    Dim success = GetCookiesFromDB()
                    If Not success Then
                        loginRst = Await ShowLoginFormAsync(loginWay)
                        Exit Try
                    End If
                    Dim cookiesKvp = m_User.Cookies.ToKeyValuePairs
                    Dim isLogined = Await EnsureLoginAsync(cookiesKvp)
                    If isLogined Then
                        HttpAsync.Instance.ReInit(m_User.Cookies)
                    Else
                        loginRst = Await ShowLoginFormAsync(loginWay)
                    End If
                End If
            ElseIf LoginResult.NotLogin = loginRst Then
                HttpAsync.Instance.ReInit(m_User.Cookies)
            End If
        Catch ex As Exception
            loginRst = LoginResult.No
        End Try

        Return loginRst
    End Function

    Private Async Function ShowLoginFormAsync(ByVal loginWay As LoginMode) As Task(Of LoginResult)
        Dim loginResult As LoginResult

        If loginWay = LoginMode.Browser Then
            Using frm As New FrmLoginFromBrowser(NavigateUrl)
                frm.ShowDialog()
                m_User.Cookies = frm.LoginedCookies
            End Using
        ElseIf loginWay = LoginMode.Cookies Then
            Using frm As New FrmLoginFromCookies
                frm.ShowDialog()
                m_User.Cookies = frm.LoginedCookies
            End Using
        Else
            Using frm As New FrmLoginFromQRCode()
                frm.ShowDialog()
                m_User.Cookies = frm.LoginedCookies
            End Using
        End If

        Dim isLogined As Boolean?
        ' 登录失败 或者 未登录，不需要进行bili组件初始化操作
        If m_User.Cookies?.Count > 0 Then
            Dim cookiesKvp = m_User.Cookies.ToKeyValuePairs
            isLogined = Await EnsureLoginAsync(cookiesKvp)
        End If
        loginResult = If(isLogined IsNot Nothing AndAlso isLogined, LoginResult.Yes, LoginResult.No)

        Return loginResult
    End Function

    ''' <summary>
    ''' 获取上一次用IE登录之后保存的cookies
    ''' </summary>
    Private Function GetCookiesFromDB() As Boolean
        Dim sql = "SELECT Cookies FROM UserInfo WHERE Id = " & m_User.Id

        Dim cookiesKvp = IO2.Database.SQLiteHelper.GetFirst(sql)
        If cookiesKvp Is Nothing Then Return False

        Dim tempCookiesKvp = cookiesKvp.ToString.DecryptCookies
        m_User.Cookies.GetFromKeyValuePairs(tempCookiesKvp, NavigateUrl)
        Return True
    End Function

    ''' <summary>
    ''' 确保传入的 cookie 可用于访问登录后的资源（能访问就是已经登录成功啦）
    ''' </summary>
    ''' <param name="cookiesKvp"></param>
    ''' <returns></returns>
    Public Async Function EnsureLoginAsync(ByVal cookiesKvp As String) As Task(Of Boolean)
        Dim funcRst As Boolean
        Dim haveLoginFlag = (cookiesKvp.IndexOf("DedeUserID=") > -1 AndAlso cookiesKvp.IndexOf("bili_jct=") > -1)
        If haveLoginFlag Then
            HttpAsync.Instance.ReInit(m_User.Cookies)

            Try
                Dim getRst = Await BilibiliApi.GetCurrentUserNavAsync()
                funcRst = getRst.Success
            Catch ex As Exception
                Logger.WriteLine(ex)
            End Try
        Else
            Return False
        End If

        Return funcRst
    End Function
#End Region
End Class
