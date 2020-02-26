#Disable Warning IDE1006 ' 命名样式  
Imports 姬娘插件.Events

Namespace RoomEntity
    Public Class New_pendants
        ''' <summary>
        ''' Frame
        ''' </summary>
        Public Property frame() As Frame
        ''' <summary>
        ''' Badge
        ''' </summary>
        Public Property badge() As Badge
        ''' <summary>
        ''' Mobile_frame
        ''' </summary>
        Public Property mobile_frame() As Mobile_frame
        ''' <summary>
        ''' Mobile_badge
        ''' </summary>
        Public Property mobile_badge() As String
    End Class

    Public Class Frame
        ''' <summary>
        ''' frame
        ''' </summary>
        Public Property type() As String
        ''' <summary>
        ''' Expire_time
        ''' </summary>
        Public Property expire_time() As Integer
        ''' <summary>
        ''' bls_summer_2018_surfing
        ''' </summary>
        Public Property name() As String
        ''' <summary>
        ''' Area
        ''' </summary>
        Public Property area() As Integer
        ''' <summary>
        ''' Area_old
        ''' </summary>
        Public Property area_old() As Integer
        ''' <summary>
        ''' Use_old_area
        ''' </summary>
        Public Property use_old_area() As Boolean
        ''' <summary>
        ''' Position
        ''' </summary>
        Public Property position() As Integer
        ''' <summary>
        ''' Create_time
        ''' </summary>
        Public Property create_time() As Integer
        ''' <summary>
        ''' Priority
        ''' </summary>
        Public Property priority() As Integer
        ''' <summary>
        ''' https://i0.hdslb.com/bfs/album/99919410eec8d947ee73b99f84670e12a7389cba.png
        ''' </summary>
        Public Property value() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property bg_color() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property bg_pic() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property desc() As String
    End Class

    Public Class Badge
        ''' <summary>
        ''' v_person
        ''' </summary>
        Public Property name() As String
        ''' <summary>
        ''' Position
        ''' </summary>
        Public Property position() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property value() As String
        ''' <summary>
        ''' bilibili 直播签约主播
        ''' </summary>
        Public Property desc() As String
    End Class

    Public Class Mobile_frame
        ''' <summary>
        ''' mobile_frame
        ''' </summary>
        Public Property type() As String
        ''' <summary>
        ''' Expire_time
        ''' </summary>
        Public Property expire_time() As Integer
        ''' <summary>
        ''' bls_summer_2018_surfing
        ''' </summary>
        Public Property name() As String
        ''' <summary>
        ''' Area
        ''' </summary>
        Public Property area() As Integer
        ''' <summary>
        ''' Area_old
        ''' </summary>
        Public Property area_old() As Integer
        ''' <summary>
        ''' Use_old_area
        ''' </summary>
        Public Property use_old_area() As Boolean
        ''' <summary>
        ''' Position
        ''' </summary>
        Public Property position() As Integer
        ''' <summary>
        ''' Create_time
        ''' </summary>
        Public Property create_time() As Integer
        ''' <summary>
        ''' Priority
        ''' </summary>
        Public Property priority() As Integer
        ''' <summary>
        ''' https://i0.hdslb.com/bfs/album/99919410eec8d947ee73b99f84670e12a7389cba.png
        ''' </summary>
        Public Property value() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property bg_color() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property bg_pic() As String
    End Class

    Public Class Data
        ''' <summary>
        ''' Uid
        ''' </summary>
        Public Property uid() As Integer
        ''' <summary>
        ''' Room_id
        ''' </summary>
        Public Property room_id() As Integer
        ''' <summary>
        ''' Short_id
        ''' </summary>
        Public Property short_id() As Integer
        ''' <summary>
        ''' Attention
        ''' </summary>
        Public Property attention() As Integer
        ''' <summary>
        ''' Online
        ''' </summary>
        Public Property online() As Integer
        ''' <summary>
        ''' Is_portrait
        ''' </summary>
        Public Property is_portrait() As Boolean
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property description() As String
        ''' <summary>
        ''' Live_status
        ''' </summary>
        Public Property live_status() As LiveStatus
        ''' <summary>
        ''' Area_id
        ''' </summary>
        Public Property area_id() As Integer
        ''' <summary>
        ''' Parent_area_id
        ''' </summary>
        Public Property parent_area_id() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property parent_area_name() As String
        ''' <summary>
        ''' Old_area_id
        ''' </summary>
        Public Property old_area_id() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property background() As String
        ''' <summary>
        ''' 用户108421387的直播间
        ''' </summary>
        Public Property title() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property user_cover() As String
        ''' <summary>
        ''' Keyframe
        ''' </summary>
        Public Property keyframe() As String
        ''' <summary>
        ''' Is_strict_room
        ''' </summary>
        Public Property is_strict_room() As Boolean
        ''' <summary>
        ''' 0000-00-00 00:00:00
        ''' </summary>
        Public Property live_time() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property tags() As String
        ''' <summary>
        ''' Is_anchor
        ''' </summary>
        Public Property is_anchor() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property room_silent_type() As String
        ''' <summary>
        ''' Room_silent_level
        ''' </summary>
        Public Property room_silent_level() As Integer
        ''' <summary>
        ''' Room_silent_second
        ''' </summary>
        Public Property room_silent_second() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property area_name() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property pendants() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property area_pendants() As String
        ''' <summary>
        ''' Hot_words
        ''' </summary>
        Public Property hot_words() As List(Of String)
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property verify() As String
        ''' <summary>
        ''' New_pendants
        ''' </summary>
        Public Property new_pendants() As New_pendants
        ''' <summary>
        ''' l:one:live:record:4236324
        ''' </summary>
        Public Property up_session() As String
        ''' <summary>
        ''' Pk_status
        ''' </summary>
        Public Property pk_status() As Integer
        ''' <summary>
        ''' Pk_id
        ''' </summary>
        Public Property pk_id() As Integer
        ''' <summary>
        ''' Allow_change_area_time
        ''' </summary>
        Public Property allow_change_area_time() As Integer
        ''' <summary>
        ''' Allow_upload_cover_time
        ''' </summary>
        Public Property allow_upload_cover_time() As Integer
    End Class

    Public Class Root
        ''' <summary>
        ''' Code
        ''' </summary>
        Public Property code() As Integer
        ''' <summary>
        ''' ok
        ''' </summary>
        Public Property msg() As String
        ''' <summary>
        ''' ok
        ''' </summary>
        Public Property message() As String
        ''' <summary>
        ''' Data
        ''' </summary>
        Public Property data() As Data
    End Class
End Namespace
#Enable Warning IDE1006 ' 命名样式  

