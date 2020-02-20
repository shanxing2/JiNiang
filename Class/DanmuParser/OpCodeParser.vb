Imports ShanXingTech
Imports ShanXingTech.ExtensionFunc
Imports 姬娘.TcpPacketParser
Imports 姬娘插件.Events
Imports 姬娘插件.Utils

''' <summary>
''' 解析服务器推送过来的各种操作码 <see cref="OpCode"/>
''' </summary>
Public NotInheritable Class OpCodeParser
    Inherits DmCmdParser
    Implements IDisposable


#Region "属性区"
    ''' <summary>
    ''' 当前登录用户Id
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property UserId() As String
    ''' <summary>
    ''' Up Id
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property UpId() As String
#End Region

#Region "字段区"
    Private m_TcpPacketDataBC As Concurrent.BlockingCollection(Of TcpPacketData)

#End Region
#Region "常量区"
    Private Const m_MaxCount As Integer = 100
#End Region

#Region "IDisposable Support"
    ' 要检测冗余调用
    Private disposedValue As Boolean = False

	' IDisposable
	Protected Overrides Sub Dispose(disposing As Boolean)
		' 窗体内的控件调用Close或者Dispose方法时，isDisposed2的值为True
		If disposedValue Then Return

		' TODO: 释放托管资源(托管对象)。
		If disposing Then
			m_TcpPacketDataBC?.CompleteAdding()
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
	Public Overloads Sub Dispose() Implements IDisposable.Dispose
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(True)
        ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
        'GC.SuppressFinalize(Me)
    End Sub
#End Region

#Region "构造函数区"
    ''' <summary>
    ''' 这个无参数的构造函数只应在测试的时候使用
    ''' </summary>
    Sub New()
        m_TcpPacketDataBC = New Concurrent.BlockingCollection(Of TcpPacketData)(m_MaxCount)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="userId">用户Id。传进来是为了区分观众或者自己</param>
    ''' <param name="upId">Up Id。传进来是为了区分Up或者房管</param>
    Sub New(ByVal userId As String, ByVal upId As String， ByVal danmuFormatDic As Dictionary(Of String, DanmuFormatEntity.FormatInfo))
        MyBase.New(danmuFormatDic)

        Me.UserId = userId
        Me.UpId = upId

        m_TcpPacketDataBC = New Concurrent.BlockingCollection(Of TcpPacketData)(m_MaxCount)

        Task.Run(AddressOf TryParseLoop)
    End Sub
#End Region

#Region "函数区"
    Public Sub ParseOpCodeTask(ByRef tcpPacketData As TcpPacketData)
        m_TcpPacketDataBC.TryAdd(tcpPacketData)
    End Sub

    Private Sub TryParseLoop()
        Try
            Debug.Print(Logger.MakeDebugString("OpCode解析器启动"))
            For Each tcpPacketData In m_TcpPacketDataBC.GetConsumingEnumerable
                ParseInternal(tcpPacketData)
            Next
        Catch ex As OperationCanceledException
            Debug.Print(Logger.MakeDebugString("OpCode解析器取消任务"))
        Catch ex As ObjectDisposedException
            Debug.Print(Logger.MakeDebugString("OpCode解析器取消任务"))
        Catch ex As Exception
            Logger.WriteLine(ex)
        Finally
            If m_TcpPacketDataBC Is Nothing Then
                m_TcpPacketDataBC.Dispose()
                m_TcpPacketDataBC = Nothing
            End If

            Debug.Print(Logger.MakeDebugString("OpCode解析器关闭"))
        End Try
    End Sub

    Private Sub ParseInternal(ByRef tcpPacketData As TcpPacketData)
        Select Case tcpPacketData.Header.OpCode
            Case OpCode.Cmd
                Try
                    ParseCmd(tcpPacketData.Body， UserId, UpId)
                Catch ex As Exception
                    Logger.WriteLine(ex)
                End Try
            Case OpCode.HeartBeatFromServer
                OnJoinLiveRoomSucceeded(New JoinLiveRoomSucceededEventArgs(CLng(Date.Now.ToTimeStampString(TimePrecision.Second))))
            Case OpCode.HeartBeatToServer
                Debug.Print(Logger.MakeDebugString(tcpPacketData.ToString))
            Case OpCode.JoinLiveRoom
                OnChatHistoryChanged(New DanmuChangedEventArgs(tcpPacketData.Body))
            Case OpCode.Popularity
                OnOnlineChanged(New OnlineChangedEventArgs(CInt(tcpPacketData.Body)))
            Case Else
                Logger.WriteLine("无法识别信息：" & tcpPacketData.ToString & tcpPacketData.Body)
        End Select
    End Sub

    ''' <summary>
    ''' 解析包含Cmd的弹幕信息
    ''' </summary>
    ''' <param name="body"></param>
    Protected Friend Overloads Sub ParseCmd(ByRef body As String)
        ParseCmd(body, Me.UserId, Me.UpId)
    End Sub

    ''' <summary>
    ''' 非B站协议产生的弹幕事件中转
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="eventName"></param>
    ''' <param name="EventArgs"></param>
    Public Overloads Shared Sub TransferDanmuEvent(Of T)(ByVal eventName As String, ByVal EventArgs As T)
        DmCmdParser.TransferDanmuEvent(eventName, EventArgs)
    End Sub

#End Region
End Class
