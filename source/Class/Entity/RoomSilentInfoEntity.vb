Namespace RoomSilentInfoEntity
	Public Class Data
#Disable Warning IDE1006 ' 命名样式
		Public Property type As String
		Public Property level As Integer
		Public Property second As Integer
	End Class

	Public Class Root
		Inherits APIPostResponseBaseEntity.Root
		Public Property data As Data
	End Class
#Enable Warning IDE1006 ' 命名样式

End Namespace
