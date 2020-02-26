Namespace RoomAdminEntity
    Public Class Page
#Disable Warning IDE1006 ' 命名样式
        Public Property page As Integer

        Public Property page_size As Integer
        Public Property total_page As Integer
        Public Property total_count As Integer
    End Class


    Public Class Data
        Public Property page As Page
        Public Property data As List(Of Detail)

        Public Class Detail
            Public Property uid As Integer
            Public Property uname As String
            Public Property face As String
            Public Property ctime As Date
            Public Property medal_name As String
            Public Property level As Integer
        End Class
    End Class


    Public Class Root
        Inherits APIPostResponseBaseEntity.Root
        Public Property data As Data
    End Class

    Public Class DisplayOnDgv
        Public Property 用户ID As Integer
        Public Property 用户名称 As String
        Public Property 任命时间 As Date

    End Class
#Enable Warning IDE1006 ' 命名样式
End Namespace

