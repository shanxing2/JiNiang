Namespace RoomShieldInfoEntity
	Public Class Shield_user_list
#Disable Warning IDE1006 ' 命名样式
		Public Property uid As Integer
		Public Property uname As String
	End Class

	Public Class Shield_rules
		Public Property rank As Integer
		Public Property verify As Integer
		Public Property level As Integer
	End Class

	Public Class Data
		Public Property shield_user_list As List(Of Shield_user_list)
		Public Property keyword_list As List(Of String)
		Public Property shield_rules As Shield_rules
	End Class

	Public Class Root
		Inherits APIPostResponseBaseEntity.Root

		Public Property data As Data
	End Class
#Enable Warning IDE1006 ' 命名样式
End Namespace