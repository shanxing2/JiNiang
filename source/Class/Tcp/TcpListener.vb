Imports System.IO
Imports System.Net.Sockets
Imports System.Threading
Imports ShanXingTech
Imports 姬娘.TcpPacketParser
Imports 姬娘插件
Imports 姬娘插件.Events

''' <summary>
''' 调试中断超过心跳时间会导致连接关闭
''' </summary>
Public Class TcpListener
	'Inherits OpCodeParser
	Implements IDisposable

#Region "事件区"
	''' <summary>
	''' 发送心跳包完成事件
	''' </summary>
	Public Event HeartBeatSendCompleted As EventHandler(Of HeartBeatSendCompletedEventArgs)

	''' <summary>
	''' 接收到服务器推送过来的信息事件（接收到所有信息（包括弹幕、送礼、人气等）都会引发此事件）
	''' </summary>
	Public Event Received As EventHandler(Of DanmuReceivedEventArgs)
	''' <summary>
	''' 直播状态改变事件
	''' </summary>
	Public Event LiveStatusChanged As EventHandler(Of LiveStatusChangedEventArgs)
#End Region

#Region "枚举区"
	<Flags>
	Private Enum LoopFlags
		None = 0
		ParsePacketLoop = 1
		PushReceivedLoop = 2
		All = ParsePacketLoop Or PushReceivedLoop
	End Enum
#End Region

#Region "字段区"
	Private m_TcpClient As TcpClient
	'Private tcpStream As NetworkStream
	Private m_TcpPacketBC As Concurrent.BlockingCollection(Of Byte())
	Private m_TryParsePacketLoopAsyncTask As Task
	Private m_HeartbeatStart As Boolean
	Private m_Reconnect As Boolean
	Private m_TcpPacketParser As TcpPacketParser
	Private m_ReceiveStop As Boolean
	Private m_TcpPacketReceivedBC As Concurrent.BlockingCollection(Of TcpPacketData)
	Private m_TryPushReceivedLoopTask As Task
	Private m_Initialized As Boolean
	Private m_Cts As CancellationTokenSource
	Private m_Ct As CancellationToken
#End Region

#Region "属性区"
	Private _RoomId As Integer
	''' <summary>
	''' 直播间Id
	''' </summary>
	''' <returns></returns>
	Public ReadOnly Property RoomId() As Integer
		Get
			Return _RoomId
		End Get
	End Property

	Private _ServerHost As String
	Public ReadOnly Property ServerHost() As String
		Get
			Return _ServerHost
		End Get
	End Property

	Private _ServerPort As Integer
	Public ReadOnly Property ServerPort() As Integer
		Get
			Return _ServerPort
		End Get
	End Property

	Private _UserId As String

	''' <summary>
	''' 当前登录用户Id
	''' </summary>
	''' <returns></returns>
	Public ReadOnly Property UserId() As String
		Get
			Return _UserId
		End Get
	End Property

	Private _UpId As String
	''' <summary>
	''' Up Id
	''' </summary>
	''' <returns></returns>
	Public ReadOnly Property UpId() As String
		Get
			Return _UpId
		End Get
	End Property

#End Region
#Region "常量"
	Private Const MaxTcpPacketCount As Integer = 618
#End Region

#Region "构造函数"
	''' <summary>
	''' 这个无参数的构造函数只应在测试的时候使用
	''' </summary>
	Sub New()
		'MyBase.New()
		Init()
	End Sub

	'Sub New(ByVal roomId As Integer, ByVal userId As String, ByVal upId As String, Optional ByVal serverHost As String = "broadcastlv.chat.bilibili.com", Optional ByVal serverPort As Integer = 2243)
	'    'MyBase.New(userId, upId)
	'    Me.UserId = userId
	'    Me.UpId = upId
	'    Me.RoomId = roomId
	'    Me.ServerHost = serverHost
	'    Me.ServerPort = serverPort
	'    m_Cts = New CancellationTokenSource
	'    m_Ct = m_Cts.Token

	'    Me.m_TcpPacketBC = New Concurrent.BlockingCollection(Of Byte())(MaxTcpPacketCount)
	'    m_TcpPacketParser = New TcpPacketParser()
	'    AddHandler m_TcpPacketParser.Parsed, AddressOf TcpPacketParser_Parsed
	'    m_TcpParseTask = Task.Run(action:=Async Sub() Await TryParsePacketLoopAsync())

	'    m_TcpPacketReceivedBC = New Concurrent.BlockingCollection(Of TcpPacketData)
	'    Task.Run(AddressOf TryPushReceivedLoop)
	'End Sub

#End Region

#Region "IDisposable Support"
	' 要检测冗余调用
	Private disposedValue As Boolean = False

	' IDisposable
	Protected Overridable Sub Dispose(disposing As Boolean)
		' 窗体内的控件调用Close或者Dispose方法时，isDisposed2的值为True
		If disposedValue Then Return

		' TODO: 释放托管资源(托管对象)。
		If disposing Then
			If m_Cts IsNot Nothing Then
				m_Cts.Dispose()
				m_Cts = Nothing
			End If

			ReleaseTcp()
			m_TcpClient = Nothing

			m_TcpPacketBC.TryRelease(m_TryParsePacketLoopAsyncTask)

			If m_TcpPacketParser IsNot Nothing Then
				RemoveHandler m_TcpPacketParser.Parsed, AddressOf TcpPacketParser_Parsed
			End If

			m_TcpPacketReceivedBC.TryRelease(m_TryPushReceivedLoopTask)
		End If

		' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
		' TODO: 将大型字段设置为 null。

		disposedValue = True
	End Sub

	'' TODO: 仅当以上 Dispose(disposing As Boolean)拥有用于释放未托管资源的代码时才替代 Finalize()。
	'Protected Overrides Sub Finalize()
	'    ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
	'    Dispose(False)
	'    MyBase.Finalize()
	'End Sub

	' Visual Basic 添加此代码以正确实现可释放模式。
	Public Sub Dispose() Implements IDisposable.Dispose
		' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
		Dispose(True)
		' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
		'GC.SuppressFinalize(Me)
	End Sub
#End Region

#Region "函数区"
	Private Sub Init()
		Me.m_TcpPacketBC = New Concurrent.BlockingCollection(Of Byte())(MaxTcpPacketCount)
		m_TcpPacketParser = New TcpPacketParser()
		AddHandler m_TcpPacketParser.Parsed, AddressOf TcpPacketParser_Parsed
		m_TryParsePacketLoopAsyncTask = Task.Run(action:=Async Sub() Await TryParsePacketLoopAsync())

		m_TcpPacketReceivedBC = New Concurrent.BlockingCollection(Of TcpPacketData)
		m_TryPushReceivedLoopTask = Task.Run(AddressOf TryPushReceivedLoop)

		m_Initialized = True
	End Sub

	''' <summary>	
	''' 连接到弹幕服务器
	''' </summary>
	Public Async Function ConnectAsync(ByVal roomId As Integer, ByVal userId As String, ByVal upId As String, Optional ByVal serverHost As String = "broadcastlv.chat.bilibili.com", Optional ByVal serverPort As Integer = 2243) As Task
		If 0 = roomId Then
			Throw New ArgumentException(String.Format(My.Resources.ArgumentOutOfRange & "   这个房间不存在" & RandomEmoji.Helpless, NameOf(roomId), ">0"))
		End If
		If Not Net2.NetHelper.IsConnectedToInternet() Then
			Throw New ArgumentException("网络异常")
		End If

		_UserId = userId
		_UpId = upId
		_RoomId = roomId
		_ServerHost = serverHost
		_ServerPort = serverPort

		Await EnsurePreInitAsync()
		EnsureInitCancellationToken()

		' 确保网络可用再连接
		Await EnsureNetConnectedAsync()
		Await InternalConnectAsync(Me.RoomId, Me.UserId, Me.ServerHost, Me.ServerPort)
	End Function

	''' <summary>
	''' 确保提前初始化完成
	''' </summary>
	''' <returns></returns>
	Private Async Function EnsurePreInitAsync() As Task
		If m_Initialized Then Return

		Using cts = New CancellationTokenSource
			cts.CancelAfter(6180)
			While Not m_Initialized
				If cts.IsCancellationRequested Then
					m_Initialized = False
					Throw New TaskCanceledException("TCP提前初始化失败")
				End If
				Await Task.Delay(100)
			End While
		End Using

		m_Initialized = True
	End Function

	Private Sub EnsureInitCancellationToken()
		m_Cts = New CancellationTokenSource
		m_Ct = m_Cts.Token
	End Sub

	Private Async Function EnsureNetConnectedAsync() As Task
		While Not Net2.NetHelper.IsConnectedToInternet()
			If m_Ct.IsCancellationRequested Then Exit While
			Await Task.Delay(6180)
		End While
	End Function

	''' <summary>
	''' 连接到弹幕服务器
	''' </summary>
	''' <param name="roomId"></param>
	''' <param name="userId"></param>
	''' 
	''' <param name="serverHost"></param>
	''' <param name="serverPort"></param>
	''' <returns></returns>
	Private Async Function InternalConnectAsync(ByVal roomId As Integer, ByVal userId As String, Optional ByVal serverHost As String = "broadcastlv.chat.bilibili.com", Optional ByVal serverPort As Integer = 2243) As Task
		If m_TcpClient IsNot Nothing Then
			Dim rst = Await ClosePreviousConnectAsync()
			If Not rst Then Return
		End If

		m_TcpClient = New TcpClient()
		If m_TcpClient Is Nothing Then Return

		Dim st As New Stopwatch
		st.Start()
		' 连接弹幕服务器
		Await m_TcpClient.ConnectAsync(serverHost, serverPort)
		If Not m_TcpClient.Connected Then
			Throw New SocketException(SocketError.NotConnected)
		End If
		st.Stop()
		Debug.Print(Logger.MakeDebugString("TCP连接耗时 " & st.ElapsedMilliseconds.ToStringOfCulture))

		' 加入直播间
		Dim joinRst = Await JoinLiveRoomAsync(roomId, userId.ToIntegerOfCulture)
		If Not joinRst Then
			Logger.WriteLine($"加入直播间失败,Id:{roomId}")
			Return
		End If

		m_Reconnect = False

		' 多线程有间隔发送心跳包
		' 不能用 Await 否则下面 接收弹幕的代码就运行不了了
		'  防止多次启动发送心跳包进程
		If Not m_HeartbeatStart Then
			m_HeartbeatStart = True
#Disable Warning BC42358 ' 由于此调用不会等待，因此在调用完成前将继续执行当前方法
			SendHeartBeatAsync()
#Enable Warning BC42358 ' 由于此调用不会等待，因此在调用完成前将继续执行当前方法
		End If

		' 接收弹幕
		m_ReceiveStop = False
		Await ReceiveAsync()
		m_ReceiveStop = True
	End Function

	Private Async Function ClosePreviousConnectAsync() As Task(Of Boolean)
		Debug.Print(Logger.MakeDebugString("尝试等待接收任务..."))
		While Not m_ReceiveStop
			If m_Ct.IsCancellationRequested Then Return False
			Await Task.Delay(1000)
		End While
		Debug.Print(Logger.MakeDebugString("接收任务完成"))

		Debug.Print(Logger.MakeDebugString("等待心跳任务完成..."))
		While m_HeartbeatStart AndAlso m_Reconnect
			If m_Ct.IsCancellationRequested Then Return False
			Await Task.Delay(1000)
		End While
		Debug.Print(Logger.MakeDebugString("心跳任务完成"))

		' 释放已经实例化的实例并关闭已有连接
		ReleaseTcp()

		' 等待上一个连接关闭
		Debug.Print(Logger.MakeDebugString("尝试等待已有连接关闭..."))
		While m_TcpClient.Connected
			If m_Ct.IsCancellationRequested Then Return False
			Await Task.Delay(1000)
		End While
		Debug.Print(Logger.MakeDebugString("已有连接已关闭"))

		Return True
	End Function

	''' <summary>
	''' 进入直播间
	''' </summary>
	''' <param name="roomId"></param>
	''' <param name="userId"></param>
	''' <returns></returns>
	Private Async Function JoinLiveRoomAsync(ByVal roomId As Integer, ByVal userId As Integer) As Task(Of Boolean)
		' ######################区分大小写，不可随便更改 key 的大小写，必须与下列key一致######################
		' platform、clientver、roomid、roomid、uid、protover
		' roomId 和 uid 必须是数值形式
#Disable Warning IDE0037 ' 使用推断的成员名称
		Dim joinLiveRoomPakect = New With {
			.platform = "Flash",
			.clientver = "2.2.8-74d8fb3d",
			.roomid = roomId,
			.uid = userId,
			.protover = 2
		}
#Enable Warning IDE0037 ' 使用推断的成员名称

		Dim body = MSJsSerializer.Serialize(joinLiveRoomPakect)
		Dim data = m_TcpPacketParser.Pack(body, OpCode.JoinLiveRoom)
		Dim tcpStream = m_TcpClient.GetStream
		If tcpStream?.CanWrite Then
			Await tcpStream.WriteAsync(data, 0, data.Length)

			Debug.Print(Logger.MakeDebugString("进入直播间：" & data.ToHexString(UpperLowerCase.Lower)))
			Return True
		End If

		Return False
	End Function

	''' <summary>
	''' 发送心跳包 这个进程应只启动一次
	''' </summary>
	Private Async Function SendHeartBeatAsync() As Task
		Debug.Print(Logger.MakeDebugString("发送心跳包进程启动"))

		' 由于用的是 Task，所以此过程退出之后，tcpStream 和 对应的 m_TcpClient 都会被GC
		' 其他地方如果想再次使用，得重新实例化一个 m_TcpClient

		' 另 断点调式的时候 可能会因为超时不发心跳包而导致连接关闭
		Try
			Dim heartBeatBytes = New Byte() {0, 0, 0, 16, 0, 16, 0, 1, 0, 0, 0, 2, 0, 0, 0, 1}
			Dim tcpStream = m_TcpClient.GetStream
			While tcpStream?.CanWrite AndAlso
				Not m_Ct.IsCancellationRequested
				' 连接上服务器之后，开启一个线程，每隔60秒向服务器发送一个心跳包（wiresharp抓包结果是官方70秒发送一次）
				' 1.第一次发送心跳包，服务器会回复一个心跳包+人气值包 0000001000100001000000080000000100000014001000010000000300000001000023da
				' 2.往后服务器都是返回直播间人气值包 0000001400100001000000030000000100002414 
				' 4字节00002414  &H00002414 十进制为 9236；直播页面上显示 '-’,表示 轮播
				Await tcpStream.WriteAsync(heartBeatBytes, 0, heartBeatBytes.Length, m_Ct)
				Debug.Print(Logger.MakeDebugString("发送心跳包：" & heartBeatBytes.ToHexString(UpperLowerCase.Lower)))

				RaiseEvent HeartBeatSendCompleted(Nothing, New HeartBeatSendCompletedEventArgs(ConnectMode.Tcp))

				' 传入 m_Ct 参数，只要取消就马上能终止 Delay  并返
				If m_Ct.IsCancellationRequested Then Exit While
				Await Task.Delay(TimeSpan.FromSeconds(DanmuEntry.Configment.HeartbeatInterval), m_Ct)
			End While
		Catch ex As TaskCanceledException
			' 调用者主动取消，不需要做 任何事
			'Debug.Print(Logger.MakeDebugString("调用者主动取消"))
		Catch ex As IO.IOException
			RaiseLiveStatusChangedEvent(LiveStatus.Break)
			Logger.WriteLine(ex)
		Catch ex As SocketException
			RaiseLiveStatusChangedEvent(LiveStatus.Break)
			Logger.WriteLine(ex)
		Catch ex As Exception
			Logger.WriteLine(ex)
		Finally
			m_HeartbeatStart = False
		End Try

		Debug.Print(Logger.MakeDebugString("NetworkStream不可写或者客户端已关闭，停止发送心跳包"))
	End Function

	''' <summary>
	''' 接收弹幕
	''' </summary>
	''' <returns></returns>
	Private Async Function ReceiveAsync() As Task
		Dim bufferSize As Integer = 2048
		Dim numByteRead As Integer
		Dim totalByteRead As Integer
		Dim tempTotalByteRead As Integer

		While True
			Dim readBuffer = New Byte(bufferSize) {}
			Dim currentPacketBytes = New Byte(bufferSize) {}
			numByteRead = 0
			totalByteRead = 0

#Region "接收tcp包"
			' TcpClient  NetworkStream  是基于流的。DataAvailable在TCP中并不是充当信息边界的，只能标识缓冲是否有数据可读，
			' 一直读取，直到缓冲区没有数据可以读取
			Try
				Dim tcpStream = If(m_TcpClient?.Connected, m_TcpClient.GetStream, Nothing)
				Do
					If m_Ct.IsCancellationRequested Then Exit Do
					If Not tcpStream?.CanRead Then Exit Do

					' NetworkStream 流不兹词取消，此处使用 cancellationToken 参数并不是用来取消流读取操作的
					numByteRead = Await tcpStream?.ReadAsync(readBuffer, 0, readBuffer.Length， m_Ct)
					Debug.Print(Logger.MakeDebugString("读取了字节长度：" & numByteRead.ToStringOfCulture))

					' 如果已经读取到的总字节数大于总字节数组长度，那就把总字节数组的长度重新定义为 totalNumberOfBytesRead 的两倍，并且保留原数 
					tempTotalByteRead = totalByteRead + numByteRead
					If tempTotalByteRead > currentPacketBytes.Length Then
						ReDim Preserve currentPacketBytes(tempTotalByteRead * 2)
					End If
					'Array.Copy(myReadBuffer，0，allBytesOfThisPacket, totalNumberOfBytesRead, numberOfBytesRead)
					Buffer.BlockCopy(readBuffer, 0, currentPacketBytes, totalByteRead, numByteRead)

					totalByteRead = tempTotalByteRead

					If m_Ct.IsCancellationRequested Then Exit While
				Loop While tcpStream?.DataAvailable
			Catch ex As ObjectDisposedException
				' ReadAsync 过程中可能会因为程序关闭而释放了tcpStream
				' do nothing
			Catch ex As TaskCanceledException
				' 调用者主动取消，不需要做 任何事
				Debug.Print(Logger.MakeDebugString("调用者主动取消"))
			Catch ex As Net.Sockets.SocketException
				' do nothing
			Catch ex As IO.IOException
				' do nothing
			Catch ex As Exception
				Logger.WriteLine(ex)
			End Try

			Debug.Print(Logger.MakeDebugString("共读取字节长度：" & totalByteRead.ToStringOfCulture))
			Debug.Print(Logger.MakeDebugString("本次读取完毕"))

			If numByteRead = 0 OrElse totalByteRead = 0 Then
				If m_Ct.IsCancellationRequested Then
					Debug.Print(Logger.MakeDebugString("取消连接"))
				Else
					m_Cts?.Cancel()
					RaiseLiveStatusChangedEvent(LiveStatus.Break)
				End If
				Exit While
			End If

			' 去掉数组尾部多余的空字节
			If totalByteRead <> currentPacketBytes.Length Then
				ReDim Preserve currentPacketBytes(totalByteRead - 1)
			End If

			m_TcpPacketBC.Add(currentPacketBytes)
#End Region
		End While

		Debug.Print(Logger.MakeDebugString("NetworkStream不可读或者客户端已关闭，停止接收弹幕信息"))
	End Function

	''' <summary>
	''' 解包
	''' </summary>
	Private Async Function TryParsePacketLoopAsync() As Task
		Try
			Debug.Print(Logger.MakeDebugString("Tcp解包器启动"))
			Dim unPackFailedBuffer = Array.Empty(Of Byte)()
			Dim preUnPackFailed As Boolean
			For Each tcpPacket In m_TcpPacketBC.GetConsumingEnumerable()
				preUnPackFailed = unPackFailedBuffer.Length > 0
				' 上次未能解析的包拼接到本次接收到的包前面，可能会组成一个完整的包
				unPackFailedBuffer = Await ParsePacketAsync(tcpPacket, unPackFailedBuffer)
				If preUnPackFailed Then
					If unPackFailedBuffer.Length = 0 Then
						Debug.Print(Logger.MakeDebugString("上次解析失败的包已经通过组包解析成功"))
					ElseIf unPackFailedBuffer.Length > 0 AndAlso m_TcpPacketParser.NeedCombinePacket Then
						' 只要检测到需要组包解析则仍然组包而不丢弃，这样能减少数据遗漏的情况
						Debug.Print(Logger.MakeDebugString("需要继续组包解析"))
					Else
						' 丢弃组包之后依然解析失败而且不缺包的包
						Logger.WriteLine($"上次解析失败的包,组包依然失败，解包失败字节长度：{unPackFailedBuffer.Length.ToStringOfCulture}   丢弃：{Environment.NewLine}{unPackFailedBuffer.ToHexString}")
						unPackFailedBuffer = Array.Empty(Of Byte)()
					End If
				End If
			Next
		Catch ex As OperationCanceledException
			Debug.Print(Logger.MakeDebugString("Tcp解包器取消任务"))
		Catch ex As ObjectDisposedException
			Debug.Print(Logger.MakeDebugString("Tcp解包器取消任务"))
		Catch ex As Exception
			Logger.WriteLine(ex)
		Finally
			Debug.Print(Logger.MakeDebugString("Tcp解包器关闭"))
		End Try
	End Function

	Private Async Function ParsePacketAsync(ByVal currentPacketBuffer As Byte(), ByVal unPackFailedBuffer As Byte()) As Task(Of Byte())
#Region "解析tcp包"
		' 确保解包之前非空包
		If currentPacketBuffer.Length = 0 Then
			Return If(unPackFailedBuffer.Length = 0,
				Array.Empty(Of Byte)(),
				unPackFailedBuffer)
		End If

		' 如果上次还有解包失败的数据，那么就合并，再解包
		If unPackFailedBuffer.Length > 0 Then
			' 上次失败的包放到前面，本次接收的包放到后面，可以组合成完整的包
			CombineArray(unPackFailedBuffer, currentPacketBuffer)
			' 组包之后丢弃上次解包失败的包
			unPackFailedBuffer = Array.Empty(Of Byte)()
		End If

		Dim unPacketByteCount As Integer
		Try
			' 解包
			unPacketByteCount = Await m_TcpPacketParser.UnPackAsync(currentPacketBuffer)
		Catch ex As Exception
			Logger.WriteLine(ex, "解析出错" & Environment.NewLine & currentPacketBuffer.ToHexString(UpperLowerCase.Lower),,,)
		Finally
			' 确保解析完成后清空 unPackFailedBuffer ，解析出错的话，返回新的 unPackFailedBuffer
			If unPacketByteCount = currentPacketBuffer.Length Then
				If unPackFailedBuffer.Length > 0 Then
					unPackFailedBuffer = Array.Empty(Of Byte)()
				End If
			Else
				FillUnPackFailedBuffer(currentPacketBuffer, unPacketByteCount, unPackFailedBuffer)
				Debug.Print(Logger.MakeDebugString("解包失败字节长度" & unPackFailedBuffer.Length.ToStringOfCulture))
			End If
		End Try
#End Region

		Return unPackFailedBuffer
	End Function

	''' <summary>
	''' 组合两个数组
	''' </summary>
	''' <param name="unPackFailedBuffer"></param>
	''' <param name="currentPacketBuffer"></param>
	Private Sub CombineArray(ByVal unPackFailedBuffer As Byte(), ByRef currentPacketBuffer As Byte())
		Dim totalSize = currentPacketBuffer.Length + unPackFailedBuffer.Length
		Dim tempBuffer(totalSize - 1) As Byte
		Buffer.BlockCopy(unPackFailedBuffer, 0, tempBuffer, 0, unPackFailedBuffer.Length)
		Buffer.BlockCopy(currentPacketBuffer, 0, tempBuffer, unPackFailedBuffer.Length, currentPacketBuffer.Length)
		currentPacketBuffer = tempBuffer
	End Sub

	''' <summary>
	''' 填充解析失败的数据包到缓存器
	''' </summary>
	''' <param name="currentPacketBuffer">当前需要解析的包</param>
	''' <param name="unPacketByteCount">已经解析完的数据长度</param>
	''' <param name="unPackFailedBuffer"></param>
	Private Sub FillUnPackFailedBuffer(ByRef currentPacketBuffer As Byte(), ByVal unPacketByteCount As Integer, ByRef unPackFailedBuffer As Byte())
		Dim upPacketFailedByteLength = currentPacketBuffer.Length - unPacketByteCount
		If unPackFailedBuffer.Length <> upPacketFailedByteLength Then
			ReDim unPackFailedBuffer(upPacketFailedByteLength - 1)
		End If
		Buffer.BlockCopy(currentPacketBuffer, unPacketByteCount, unPackFailedBuffer, 0, upPacketFailedByteLength)
	End Sub

	''' <summary>
	''' 关闭连接
	''' </summary>
	Public Sub Close()
		' 先关掉tcp包缓存器，再关tcp连接
		m_TcpPacketBC?.CompleteAdding()

		ReleaseTcp()

		m_Cts?.Cancel()
	End Sub

	Private Sub ReleaseTcp()
		If m_TcpClient Is Nothing Then Return

		If m_TcpClient.Connected Then
			m_TcpClient.Client?.Shutdown(SocketShutdown.Both)
		End If
		If m_TcpClient.Connected Then
			m_TcpClient.GetStream?.Close()
		End If
		m_TcpClient.Close()
	End Sub

	''' <summary>
	''' 解析了数据包（单个或者多个）事件
	''' </summary>
	''' <param name="sender"></param>
	''' <param name="e"></param>
	Private Sub TcpPacketParser_Parsed(sender As Object, e As ParsedEventArgs)
		'ParseOpCodeTask(e.TcpPacket)
		m_TcpPacketReceivedBC.TryAdd(e.TcpPacket)
	End Sub

	Private Sub TryPushReceivedLoop()
		' 弹幕推送不能使用同步的方式，不然下游的调用者可能会长时间占用事件
		' 而影响推送速度，所以得另外开一个异步循环
		Try
			Debug.Print(Logger.MakeDebugString("弹幕消息推送器启动"))
			For Each e In m_TcpPacketReceivedBC.GetConsumingEnumerable
				RaiseEvent Received(Nothing, New DanmuReceivedEventArgs(_UserId, _UpId, e))
			Next
		Catch ex As OperationCanceledException
			Debug.Print(Logger.MakeDebugString("弹幕消息推送器取消任务"))
		Catch ex As ObjectDisposedException
			Debug.Print(Logger.MakeDebugString("弹幕消息推送器取消任务"))
		Catch ex As Exception
			Logger.WriteLine(ex)
		Finally
			Debug.Print(Logger.MakeDebugString("弹幕消息推送器关闭"))
		End Try
	End Sub

	Private Sub RaiseLiveStatusChangedEvent(ByVal liveStatus As LiveStatus)
		RaiseEvent LiveStatusChanged(Nothing, New LiveStatusChangedEventArgs(liveStatus))
		'OnLiveStatusChanged(New LiveStatusChangedEventArgs(liveStatus))
	End Sub
#End Region


#Region "测试区"
	''' <summary>
	''' 测试专用
	''' </summary>
	''' <param name="receiveData">数据包16进制形式，多个包用换一个换行符分割</param>
	Public Sub ReceiveTest(ByVal receiveData As String)
		Dim byteArr = receiveData.Split({vbCr, vbCrLf, vbLf, Environment.NewLine, ";"c}, StringSplitOptions.RemoveEmptyEntries)
		For Each rd In byteArr
			Dim compressBytes = rd.HexStringToBytes
			'Await ParsePacketAsync(compressBytes, Array.Empty(Of Byte)())
			If m_TcpPacketBC Is Nothing Then
				Init()
			End If
			m_TcpPacketBC.Add(compressBytes)
		Next
	End Sub
#End Region
End Class
