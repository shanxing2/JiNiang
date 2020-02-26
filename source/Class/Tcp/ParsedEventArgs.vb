Imports 姬娘插件.Events

Partial Public NotInheritable Class TcpPacketParser
	Public Class ParsedEventArgs
		Inherits EventArgs

		Public Sub New(tcpPackets As TcpPacketData)
			Me.TcpPacket = tcpPackets
		End Sub

		''' <summary>
		''' 解析到的tcp数据包
		''' </summary>
		''' <returns></returns>
		Public ReadOnly Property TcpPacket As TcpPacketData
	End Class
End Class
