Namespace UserInfoEntity
#Disable Warning IDE1006 ' 命名样式
	Public Class Level_info
		''' <summary>
		''' 
		''' </summary>
		Public Property current_level() As Integer
	End Class

	Public Class Official_verify
		''' <summary>
		''' 
		''' </summary>
		Public Property type() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property desc() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property suffix() As String
	End Class

	Public Class Vip
		''' <summary>
		''' 
		''' </summary>
		Public Property vipType() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property vipStatus() As Integer
	End Class

	Public Class Data
		''' <summary>
		''' 
		''' </summary>
		Public Property mid() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property name() As String
		''' <summary>
		''' 保密
		''' </summary>
		Public Property sex() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property rank() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property face() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property regtime() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property spacesta() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property birthday() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property sign() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property level_info() As Level_info
		''' <summary>
		''' 
		''' </summary>
		Public Property official_verify() As Official_verify
		''' <summary>
		''' 
		''' </summary>
		Public Property vip() As Vip
		''' <summary>
		''' 
		''' </summary>
		Public Property toutu() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property toutuId() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property theme() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property theme_preview() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property coins() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property im9_sign() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property fans_badge() As Boolean
	End Class

	Public Class Root
		''' <summary>
		''' 
		''' </summary>
		Public Property status() As Boolean
		''' <summary>
		''' 
		''' </summary>
		Public Property data() As Data
	End Class
#Enable Warning IDE1006 ' 命名样式

End Namespace

