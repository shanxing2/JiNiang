Namespace DanmuServerInfoEntity
#Disable Warning IDE1006 ' 命名样式
    Public Class Host_server_list
        Public Property host As String
        Public Property port As Integer
        Public Property wss_port As Integer
        Public Property ws_port As Integer
    End Class

    Public Class Server_list
        Public Property host As String
        Public Property port As Integer
    End Class

    Public Class Data
        Public Property refresh_row_factor As Double
        Public Property refresh_rate As Integer
        Public Property max_delay As Integer
        Public Property port As Integer
        Public Property host As String
        Public Property host_server_list As List(Of Host_server_list)
        Public Property server_list As List(Of Server_list)
        Public Property token As String
    End Class

    Public Class Root
        Inherits APIPostResponseBaseEntity.Root

        Public Property data As Data
    End Class
#Enable Warning IDE1006 ' 命名样式
End Namespace

