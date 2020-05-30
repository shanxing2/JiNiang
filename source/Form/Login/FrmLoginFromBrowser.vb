Imports System.ComponentModel
Imports System.Net
Imports ShanXingTech
Imports ShanXingTech.Net2

''' <summary>
''' 闪星网络信息科技 Q2287190283
''' 神即道, 道法自然, 如来
''' </summary>
Public Class FrmLoginFromBrowser
#Region "属性区"
    Private m_LoginedUrl As String

    Private m_LoginedCookies As CookieContainer
    Public ReadOnly Property LoginedCookies As CookieContainer
        Get
            Return m_LoginedCookies
        End Get
    End Property
#End Region

#Region "字段区"
    ''' <summary>
    ''' 需要访问的地址
    ''' </summary>
    Private ReadOnly m_NavigateUrl As String
    Private WithEvents Web1 As WebBrowser
#End Region

#Region "IDisposable Support"
    ' 要检测冗余调用
    Dim isDisposed2 As Boolean = False

    ''' <summary>
    ''' 重写Dispose 以清理非托管资源
    ''' </summary>
    ''' <param name="disposing"></param>
    Protected Overrides Sub Dispose(disposing As Boolean)
        ' 窗体内的控件调用Close或者Dispose方法时，isDisposed2的值为True
        If isDisposed2 Then Return

        Try
            ' TODO: 释放托管资源(托管对象)。
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                    components = Nothing
                End If
            End If

            ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
            ' TODO: 将大型字段设置为 null。
            If Web1 IsNot Nothing Then
                Web1.Dispose()
                Web1 = Nothing
            End If

            isDisposed2 = True
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '' NOTE: Leave out the finalizer altogether if this class doesn't   
    '' own unmanaged resources itself, but leave the other methods  
    '' exactly as they are.   
    'Protected Overrides Sub Finalize()
    '    Try
    '        ' Finalizer calls Dispose(false)  
    '        Dispose(False)
    '    Finally
    '        MyBase.Finalize()
    '    End Try
    'End Sub
#End Region

#Region "构造函数"
    Sub New(ByVal loginUrl As String)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。

        Me.m_NavigateUrl = loginUrl
    End Sub

#End Region

    Private Sub FrmWeb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If String.IsNullOrEmpty(m_NavigateUrl) Then
            MessageBox.Show(String.Concat("Url未初始化,请先设置", Me.Name, ".Url属性"), My.Resources.MsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If Web1 Is Nothing Then
            Web1 = New WebBrowser With {
                .Dock = DockStyle.Fill,
                .Location = New Point(0, 0)
            }
            Me.Controls.Add(Web1)
        End If

        ' 不显示脚本错误等信息
        Web1.ScriptErrorsSuppressed = True
        Web1.ChangeUserAgent("Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 SE 2.X MetaSr 1.0")
        ' 消除网页加载嘟嘟声
        Web1.DisableNavigationSounds(True)

        ' 如果已经登录 就不需要再次加载登录页了
        If Not String.IsNullOrEmpty(m_LoginedUrl) Then Return

        ' 改变程序内部IE浏览器默认的版本号
        ' app.exe and app.vshost.exe
        Dim appname As String = String.Concat(Process.GetCurrentProcess().ProcessName, ".exe")
        ' 改变程序内部IE浏览器默认的版本号
        ' 注意：如果是淘宝，想要快捷登录（识别已经登录的旺旺），需要设置 项目——属性——编译——目标CPU——勾选首选32位
        Web1.SetVersionEmulation(BrowserEmulationMode.IE10, appname)

        ' 删除此链接所属域名的cookie
        NetHelper.DeleteCookiesAboutDomain(m_NavigateUrl)

        ' 获取程序内部使用的IE内核浏览器版本
        'url = "http://ie.icoa.cn/"

#Region "多线程加载网页"
        ' 多线程加载网页(WebBrowser只能是单线程单元)
        ' 因为task模型不能直接设置Threading.ApartmentState.STA，所以这里目前只能用Threading.Thread实现 20170924
        ' 此处必须加载先一个页面，这里用about:blank，否则会发生“无法获取“WebBrowser”控件的窗口句柄。不支持无窗口的 ActiveX 控件。”错误
        Web1.Navigate("about:blank")
        Dim newThread As New Threading.Thread(Sub()
                                                  Try
                                                      Web1.Navigate(m_NavigateUrl)
                                                  Catch ex As Exception
                                                      MessageBox.Show("加载登录页失败，请关闭网页后重新打开再试" & RandomEmoji.Sad, "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                                  End Try
                                              End Sub)
        ' 多线程操作webbrowser控件，一定要设置为 STA 模式
        newThread.TrySetApartmentState(Threading.ApartmentState.STA)

        ' 如果已经有登录之后的cookie  可以直接去往目标地址
        newThread.Start()
#End Region
    End Sub

    Private Sub FrmWeb_FormClosing(sender As Object, e As CancelEventArgs) Handles Me.FormClosing
        If Web1 Is Nothing Then Return

        ' 启用消除网页加载嘟嘟声
        Web1.DisableNavigationSounds(False)

        ' 有 DedeUserID= 表示登录成功
        If Web1.Document Is Nothing OrElse Web1.Document.Cookie.IsNullOrEmpty Then Return
        If Web1.Document.Cookie.Contains("DedeUserID=") Then
            m_LoginedUrl = Web1.Url.AbsoluteUri

            ' 如果已经登录 则获取cookie
            If m_LoginedUrl.Length > 0 Then
                ' 获取webbrowser登录成功后的cookie,需要带上登录成功后的URL
                ' 而且 也需要从 Web1.Document.Cookie 处获取cookie，否则会漏掉一些cookie(跟TB.Operate.OperateUrl不属于同一个域的cookie)
                ' 不同页面，Domain不一样，视具体情况而定
                m_LoginedCookies.GetFromKeyValuePairs(Web1.Document.Cookie, m_LoginedUrl)
                m_LoginedCookies.GetFromUrl(m_LoginedUrl)
            End If
        End If
    End Sub

    Private Sub Web1_NewWindow(sender As Object, e As CancelEventArgs)
        e.Cancel = True
    End Sub

    Private Sub Web1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles Web1.DocumentCompleted
        If Web1.Document Is Nothing Then Return
        ' 将所有的链接的目标，指向本窗体
        For Each archor As HtmlElement In Web1.Document.Links
            archor.SetAttribute("target", "_self")
        Next

        ' 将所有的FORM的提交目标，指向本窗体
        For Each form As HtmlElement In Web1.Document.Forms
            form.SetAttribute("target", "_self")
        Next

        LocateLoginFrame()
    End Sub

    Private Sub Web1_Navigated(sender As Object, e As WebBrowserNavigatedEventArgs) Handles Web1.Navigated
        ' 登录成功就可以了，不需要点击 用户中心下面 “我的直播间” 选项
        If m_NavigateUrl = e.Url?.AbsoluteUri Then
            Windows2.DrawTipsTask(Me, "如果一千年以后我消失了，页面还是空空如也，请关闭软件重新打开再试" & RandomEmoji.Helpless, 5000, True, False)
        End If
        If e.Url?.AbsoluteUri?.IndexOf("user-center") > -1 Then
            Windows2.DrawTipsTask(Me, "‘用户中心’——‘个人信息’——‘我的信息’ 加载完毕就阔以关闭登录窗口啦" & RandomEmoji.Happy, 3000, True, False)
        End If
    End Sub

    Private Sub Web1_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        LocateLoginFrame()
    End Sub

    ''' <summary>
    ''' 定位到登录框（在浏览器窗口可见范围内能看到登录框）
    ''' </summary>
    Private Sub LocateLoginFrame()
        If Web1?.Document Is Nothing Then Return
        Dim loginDiv = Web1.Document.GetElementById("geetest-wrap")
        If loginDiv IsNot Nothing Then
            loginDiv.ScrollIntoView(False)
        End If
    End Sub
End Class