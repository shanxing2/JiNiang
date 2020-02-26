Partial Class TcpListener
    ''' <summary>
    ''' 发送心跳包完成事件
    ''' </summary>
    Public Class HeartBeatSendCompletedEventArgs
        Inherits EventArgs

        Public Sub New(connectMode As ConnectMode)
            Me.ConnectMode = connectMode
        End Sub

        Public ReadOnly Property ConnectMode As ConnectMode
    End Class

    ''' <summary>
    ''' 接收到TCP包数据事件
    ''' </summary>
    Public Class ReceiveChangedEventArgs
        Inherits EventArgs

        Public Sub New(data() As Byte)
			Me.Data = data
		End Sub

		''' <summary>
		''' 接收到的数据
		''' </summary>
		''' <returns></returns>
		Public ReadOnly Property Data As Byte()
    End Class
End Class
