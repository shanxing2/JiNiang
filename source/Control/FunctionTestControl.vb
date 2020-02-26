Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports System.Web.Script.Serialization
Imports ShanXingTech
Imports ShanXingTech.Win32API


Public Class FunctionTestControl
#Region "字段区"
	Private m_MainForm As FrmMain
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

				If m_MainForm IsNot Nothing Then
					m_MainForm.Dispose()
					m_MainForm = Nothing
				End If

				'If webSocket IsNot Nothing Then
				'    webSocket.Close()
				'    webSocket = Nothing
				'End If

				If cts IsNot Nothing Then
					cts.Dispose()
					cts = Nothing
				End If
			End If

			' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
			' TODO: 将大型字段设置为 null。

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
	Public Sub New(ByRef mainForm As FrmMain, ByVal container As TabPage)

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。

		m_MainForm = mainForm

		Me.SuspendLayout()

		Me.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
		Me.Location = New Point(0, 0)
		Me.Size = container.Size

		Me.ResumeLayout(False)
		Me.PerformLayout()
	End Sub
#End Region

#Region "辅助测试"
	'Dim webSocket As WebSocket
	Dim cts As New CancellationTokenSource
	Dim ct As CancellationToken


	' Install-Package WebSocketSharp -Pre
	' https://github.com/sta/websocket-sharp
	'Private Sub btnWss_Click(sender As Object, e As EventArgs) Handles btnWss.Click
	'    ct = cts.Token

	'    webSocket = New WebSocket("wss://broadcastlv.chat.bilibili.com:2245/sub")
	'    'webSocket.SslConfiguration.ServerCertificateValidationCallback = Function(wssSender, certificate, chain, sslPolicyErrors)
	'    '                                                                     'certificate.Import("d:\WebSockekKey\-bilibilicom.crt")
	'    '                                                                     Return True
	'    '                                                                 End Function
	'    webSocket.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
	'    'webSocket.EnableRedirection = True

	'    'webSocket.SslConfiguration.CheckCertificateRevocation = True
	'    'webSocket.SslConfiguration.ClientCertificates.Add(New System.Security.Cryptography.X509Certificates.X509Certificate("d:\WebSockekKey\-bilibilicom.crt"))
	'    AddHandler webSocket.OnOpen, Sub(webSocketSender, WebSocketE)
	'                                     Debug.Print(ShanXingTech.Logger.MakeDebugString("连接成功  " & WebSocketE.ToString))
	'                                     webSocket.Ping("ping")
	'                                 End Sub
	'    AddHandler webSocket.OnMessage, Sub(webSocketSender, WebSocketE)
	'                                        Debug.Print(ShanXingTech.Logger.MakeDebugString( WebSocketE.Data))
	'                                    End Sub
	'    AddHandler webSocket.OnClose, Sub(webSocketSender, WebSocketE)
	'                                      Debug.Print(ShanXingTech.Logger.MakeDebugString("连接关闭  " & WebSocketE.Reason))
	'                                  End Sub


	'    'webSocket.SetCredentials("aa", "bb", True)
	'    webSocket.Connect()
	'    Dim sendDatab = "0000006e0010000100000007000000017b22706c6174666f726d223a22666c617368222c22636c69656e74766572223a22322e322e382d3734643866623364222c22726f6f6d6964223a2239373231303933222c22756964223a223532313535383531222c2270726f746f766572223a327d"

	'    webSocket.SendAsync(sendDatab,
	'                    Sub(sendRst)
	'                        If sendRst Then
	'                            Debug.Print(ShanXingTech.Logger.MakeDebugString("已发送"))
	'                        End If
	'                    End Sub)

	'    Dim header As TcpPacketParser.Header
	'    'Dim a = New With {
	'    '    .uid = "52155851",
	'    '    .roomid = "4236342",
	'    '    .protover = 1,
	'    '    .platform = "web",
	'    '    .clientver = "1.4.0"
	'    '}

	'    Dim a = New With {
	'    .platform = "flash",
	'    .clientver = "2.2.8-74d8fb3d",
	'    .roomid = "9721093",
	'    .uid = "52155851",
	'    .protover = 2
	'}
	'    header = New TcpPacketParser.Header With {
	'    .PacketSize = 110,
	'    .Size = 10,
	'    .Protover = 1,
	'    .OpCode = TcpPacketParser.OpCode.JoinLiveRoom,
	'    .Sequence = 1
	'}

	'    Dim body = JsSerializer.Serialize(a)
	'    Dim sendData = header.ToByteArray(ByteOrder.Big).ToHexString(UpperLowerCase.Lower) & body
	'    Dim bytes = System.Text.Encoding.UTF8.GetBytes(sendData)
	'    Dim sb = StringBuilderCache.Acquire(360)
	'    For Each b In bytes
	'        sb.Append(b.ToString("x2"))
	'    Next

	'    sendData = "0000006e0010000100000007000000017b22706c6174666f726d223a22666c617368222c22636c69656e74766572223a22322e322e382d3734643866623364222c22726f6f6d6964223a2239373231303933222c22756964223a223532313535383531222c2270726f746f766572223a327d"

	'    webSocket.SendAsync(sendData,
	'                    Sub(sendRst)
	'                        If sendRst Then
	'                            Debug.Print(ShanXingTech.Logger.MakeDebugString("已发送"))
	'                        End If
	'                    End Sub)
	'End Sub


	Private Async Sub btnTcp_ClickAsync(sender As Object, e As EventArgs) Handles btnTcp.Click
		If txtRoomId.Text.Length = 0 Then
			Windows2.DrawTipsTask(Me, "请输入直播间Id" & RandomEmoji.Angry, 1000, False, True)
			Return
		End If

		m_MainForm.DmTcpClient = New TcpListener()

		'AddHandler DanmuEntry.OnlineChanged,
		'    Sub(sender1 As Object, e1 As DanmuEvents.OnlineChangedEventArgs)
		'        m_MainForm.DmTcpClient. OnOnlineChanged(e1.Online)
		'    End Sub

		m_MainForm.ConfigureEvents(True)

		'Await m_MainForm.DmTcpClient.ConnectAsync(roomId:=CInt(txtRoomId.Text), userId:="52155851", upId:=DanmuEntry.User.ViewRoom.UserId, serverHost:="broadcastlv.chat.bilibili.com", serverPort:=2243)
		Await m_MainForm.DmTcpClient.ConnectAsync(roomId:=CInt(txtRoomId.Text), userId:="0", upId:="183430", serverHost:="broadcastlv.chat.bilibili.com", serverPort:=2243)
	End Sub

#If DEBUG Then
	Private Sub btnUnPackAsync_Click(sender As Object, e As EventArgs) Handles btnTcpStreamTestAsync.Click
		Dim value = txtTcpStreamSource.Text
		If m_MainForm.DmTcpClient Is Nothing Then
			m_MainForm.DmTcpClient = New TcpListener()
		End If
		m_MainForm.DmTcpClient.ReceiveTest(value)
	End Sub

	Private Sub Button5_Click(sender As Object, e As EventArgs) Handles btnParseCmd.Click
		Dim value = txtCmdSource.Text
		If m_MainForm.DanmuParser Is Nothing Then
			m_MainForm.DanmuParser = New OpCodeParser()
		End If
		m_MainForm.DanmuParser.ParseCmd(value)
		'Dim a = DanmuEntry.ParseDanmu(value)
		'Dim sourceStr = TextBox1.Text
		'Dim bilier As BilibiliHttp = New BilibiliHttp()
		'bilier.ParseDanmu(sourceStr)
	End Sub
#End If

	Private Sub btnCloseTcp_Click(sender As Object, e As EventArgs) Handles btnCloseTcp.Click
		m_MainForm.DmTcpClient?.Close()
	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

		'webChatHistory.Document.Write("X 1")

		'webChatHistory.Document.Window.ScrollTo(0, webChatHistory.Document.Body.ScrollRectangle.Height)
		'webChatHistory.Url = Nothing
		'With m_MainForm.DanmuControl
		'    .btnClearChatHistory.Location = New Point(.webChatHistory.Left + .webChatHistory.Width - .btnClearChatHistory.Width - 16,
		'                                         .webChatHistory.Bottom - .btnClearChatHistory.Height - 1)
		'End With
		'm_MainForm.DanmuControl.webChatHistory.Navigate("about:blank")
		'm_MainForm.DanmuControl.webChatHistory.Document.OpenNew(True)
		'm_MainForm.DanmuControl.webChatHistory.Document.Write(txtTestSource.Text)

		'm_MainForm.LiveRoomControl.Width = m_MainForm.tpLiveRoom.Width
		'm_MainForm.DanmuControl.Width = m_MainForm.tpLiveRoom.Width

		m_MainForm.tpLiveRoom.Controls.Add(m_MainForm.LiveRoomControl)
		m_MainForm.tpLiveRoom.Controls.Add(m_MainForm.DanmuControl)
	End Sub


	Private settingControl As SettingControl
	Private Async Sub Button3_ClickAsync(sender As Object, e As EventArgs) Handles Button3.Click
		'webChatHistory.Url = Nothing
		'webChatHistory.Document.Write(TextBox1.Text)
		'm_MainForm.SettingControl.TextBox1.SetCueBanner("请输入弹幕 DA☆ZE～  按Enter键发送，超长后自动分条发送")

		Dim a = ToKeyValuePairs("isg=dfdf&cookie=fd&fe&name=&size=20")

		Return
		'Dim doc = TryCast(webChatHistory.Document.DomDocument， IHTMLDocument2)
		Dim aaaa = "isg=dfdf&cookie=fdfd&name=&size=20"

		Dim bbbb = aaaa.ToHexString(UpperLowerCase.Upper)
		Dim cccc = bbbb.FromHexString
		Dim ddd = bbbb.HexStringToBytes
		Dim eee = Await ddd.CompressAsync
		Dim fff = Await eee.DeCompressAsync
	End Sub

	Private Sub btnPushStream_Click(sender As Object, e As EventArgs) Handles btnPushStream.Click
		Return
		Dim topWindowHwnd As IntPtr
		Dim sb As New StringBuilder(255)
		Do
			topWindowHwnd = Win32API.FindWindowEx(IntPtr.Zero, topWindowHwnd, Nothing, Nothing)
			GetWindowText(topWindowHwnd, sb, sb.Capacity)
			If sb.Length > 0 Then
				Debug.Print($"句柄： {topWindowHwnd.ToString("X")} 标题： {sb.ToString}")
			End If
		Loop Until IntPtr.Zero = topWindowHwnd
		Debug.Print("")


		Do
			topWindowHwnd = Win32API.FindWindowEx(IntPtr.Zero, topWindowHwnd, "Qt5QWindowIcon", Nothing)
			GetWindowText(topWindowHwnd, sb, sb.Capacity)
			If sb.Length > 0 Then
				Debug.Print($"句柄： {topWindowHwnd.ToString("X")} 标题： {sb.ToString}")
			End If
			sb.Length = 0
		Loop Until IntPtr.Zero = topWindowHwnd
		Debug.Print("")


		Dim title = "OBS 21.1.2 (windows) - 配置文件: 未命名 - 场景: 未命名"
		topWindowHwnd = Win32API.FindWindow("Qt5QWindowIcon", "OBS 21.1.2 (windows) - 配置文件: 未命名 - 场景: 未命名")
		If topWindowHwnd <> IntPtr.Zero Then
			Dim pid = GetWindowThreadProcessId(topWindowHwnd, 0)
			Dim proc = Process.GetProcessById(pid)
			Dim tempFileName = proc.MainModule.FileName
			Debug.Print($"句柄： {topWindowHwnd.ToString("X")} 标题： {title}")
		End If

		For Each proc In Process.GetProcesses
			Try
				Debug.Print($"句柄： {proc.Handle.ToString("X")} 进程名： {proc.ProcessName.ToString}")
			Catch ex As Exception
				Continue For
			End Try
		Next
		Debug.Print("")


		Dim proc2 = Process.GetProcessesByName("obs32")

		Debug.Print("")


		Dim cb As New EnumWindowsProc(
			Function(ByVal hWnd As IntPtr, ByRef lParam As IntPtr)
				GetWindowText(hWnd, sb, sb.Capacity)
				If sb.Length > 0 Then
					Debug.Print($"句柄： {hWnd.ToString("X")} 标题： {sb.ToString}")
				End If
				sb.Length = 0

				Return True
			End Function)
		Dim lParam2 As IntPtr
		EnumWindows(AddressOf CallBack, lParam2)
		Debug.Print("")

	End Sub

	Private Function CallBack(ByVal hWnd As IntPtr, ByRef lParam As IntPtr) As Boolean
		Dim sb As New StringBuilder(255)
		GetWindowText(hWnd, sb, sb.Capacity)
		If sb.Length > 0 Then
			Debug.Print($"句柄： {hWnd.ToString("X")} 标题： {sb.ToString}")
		End If
		Return True
	End Function

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

	End Sub

#End Region
End Class
