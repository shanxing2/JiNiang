#Disable Warning ide1006
Namespace RoomBlackViewerEntity
	Public Class Data
		''' <summary>
		''' Id
		''' </summary>
		Public Property id() As Integer
		''' <summary>
		''' Roomid
		''' </summary>
		Public Property roomid() As Integer
		''' <summary>
		''' Uid
		''' </summary>
		Public Property uid() As Integer
		''' <summary>
		''' Type
		''' </summary>
		Public Property type() As Integer
		''' <summary>
		''' Adminid
		''' </summary>
		Public Property adminid() As Integer
		''' <summary>
		''' 2018-12-21 22:33:22
		''' </summary>
		Public Property block_end_time() As String
		''' <summary>
		''' 2018-12-21 21:33:22
		''' </summary>
		Public Property ctime() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property msg() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property msg_time() As String
		''' <summary>
		''' 胖头鱼和他小母猪妹妹
		''' </summary>
		Public Property uname() As String
		''' <summary>
		''' 小母猪和她胖头鱼哥哥
		''' </summary>
		Public Property admin_uname() As String
	End Class

	Public Class Root
		Inherits APIPostResponseBaseEntity.Root
		''' <summary>
		''' Data
		''' </summary>
		Public Shadows Property data() As List(Of Data)
	End Class

	Public Class DisplayOnDgv
		Public Property 黑名单Id As Integer
		Public Property 用户名称 As String
		Public Property 解禁时间 As Date

	End Class
End Namespace
#Enable Warning ide1006