#Disable Warning ide1006

Namespace AnchorInRoomEntity
    Public Class Official_verify
        ''' <summary>
        ''' Type
        ''' </summary>
        Public Property type() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property desc() As String
        ''' <summary>
        ''' Role
        ''' </summary>
        Public Property role() As Integer
    End Class

    Public Class Info
        ''' <summary>
        ''' Uid
        ''' </summary>
        Public Property uid() As Integer
        ''' <summary>
        ''' 小母猪和她胖头鱼蜀黍
        ''' </summary>
        Public Property uname() As String
        ''' <summary>
        ''' https://i2.hdslb.com/bfs/face/074fe049c7d4bc47e3a6ae89906af5d0246a5ea1.jpg
        ''' </summary>
        Public Property face() As String
        ''' <summary>
        ''' 10000
        ''' </summary>
        Public Property rank() As String
        ''' <summary>
        ''' Identification
        ''' </summary>
        Public Property identification() As Integer
        ''' <summary>
        ''' Mobile_verify
        ''' </summary>
        Public Property mobile_verify() As Integer
        ''' <summary>
        ''' Platform_user_level
        ''' </summary>
        Public Property platform_user_level() As Integer
        ''' <summary>
        ''' Vip_type
        ''' </summary>
        Public Property vip_type() As Integer
        ''' <summary>
        ''' Official_verify
        ''' </summary>
        Public Property official_verify() As Official_verify
    End Class

    Public Class Master_level
        ''' <summary>
        ''' Level
        ''' </summary>
        Public Property level() As Integer
        ''' <summary>
        ''' Current
        ''' </summary>
        Public Property current() As List(Of Integer)
        ''' <summary>
        ''' Next
        ''' </summary>
        Public Property [next]() As List(Of Integer)
        ''' <summary>
        ''' Color
        ''' </summary>
        Public Property color() As Integer
        ''' <summary>
        ''' Anchor_score
        ''' </summary>
        Public Property anchor_score() As Integer
        ''' <summary>
        ''' Upgrade_score
        ''' </summary>
        Public Property upgrade_score() As Integer
        ''' <summary>
        ''' Master_level_color
        ''' </summary>
        Public Property master_level_color() As Integer
        ''' <summary>
        ''' >10000
        ''' </summary>
        Public Property sort() As String
    End Class

    Public Class Level
        ''' <summary>
        ''' 52155851
        ''' </summary>
        Public Property uid() As String
        ''' <summary>
        ''' 593600
        ''' </summary>
        Public Property cost() As String
        ''' <summary>
        ''' 1851795
        ''' </summary>
        Public Property rcost() As String
        ''' <summary>
        ''' 1385000
        ''' </summary>
        Public Property user_score() As String
        ''' <summary>
        ''' Vip
        ''' </summary>
        Public Property vip() As Integer
        ''' <summary>
        ''' 2018-07-16 03:11:05
        ''' </summary>
        Public Property vip_time() As String
        ''' <summary>
        ''' Svip
        ''' </summary>
        Public Property svip() As Integer
        ''' <summary>
        ''' 0000-00-00 00:00:00
        ''' </summary>
        Public Property svip_time() As String
        ''' <summary>
        ''' 2018-07-21 01:09:25
        ''' </summary>
        Public Property update_time() As String
        ''' <summary>
        ''' Master_level
        ''' </summary>
        Public Property master_level() As Master_level
        ''' <summary>
        ''' User_level
        ''' </summary>
        Public Property user_level() As Integer
        ''' <summary>
        ''' Color
        ''' </summary>
        Public Property color() As Integer
        ''' <summary>
        ''' Anchor_score
        ''' </summary>
        Public Property anchor_score() As Integer
    End Class

    Public Class Data
        ''' <summary>
        ''' Info
        ''' </summary>
        Public Property info() As Info
        ''' <summary>
        ''' Level
        ''' </summary>
        Public Property level() As Level
        ''' <summary>
        ''' San
        ''' </summary>
        Public Property san() As Integer
    End Class

    Public Class Root
        ''' <summary>
        ''' Code
        ''' </summary>
        Public Property code() As Integer
        ''' <summary>
        ''' success
        ''' </summary>
        Public Property msg() As String
        ''' <summary>
        ''' success
        ''' </summary>
        Public Property message() As String
        ''' <summary>
        ''' Data
        ''' </summary>
        Public Property data() As Data
    End Class
End Namespace
#Enable Warning ide1006