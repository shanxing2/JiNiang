Namespace RoomViewerInfoEntity
#Disable Warning IDE1006 ' 命名样式
    Public Class User_level
        ''' <summary>
        ''' Level
        ''' </summary>
        Public Property level() As Integer
        ''' <summary>
        ''' Next_level
        ''' </summary>
        Public Property next_level() As Integer
        ''' <summary>
        ''' Color
        ''' </summary>
        Public Property color() As Integer
        ''' <summary>
        ''' >50000
        ''' </summary>
        Public Property level_rank() As String
    End Class

    Public Class Vip
        ''' <summary>
        ''' Vip
        ''' </summary>
        Public Property vip() As Integer
        ''' <summary>
        ''' 2019-11-22 17:31:10
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
    End Class

    Public Class Title
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property title() As String
    End Class

    Public Class Badge
        ''' <summary>
        ''' Is_room_admin
        ''' </summary>
        Public Property is_room_admin() As Boolean
    End Class

    Public Class Privilege
        ''' <summary>
        ''' Target_id
        ''' </summary>
        Public Property target_id() As Integer
        ''' <summary>
        ''' Privilege_type
        ''' </summary>
        Public Property privilege_type() As Integer
        ''' <summary>
        ''' Sub_level
        ''' </summary>
        Public Property sub_level() As Integer
        ''' <summary>
        ''' Notice_status
        ''' </summary>
        Public Property notice_status() As Integer
        ''' <summary>
        ''' Broadcast
        ''' </summary>
        Public Property broadcast() As String
        ''' <summary>
        ''' Buy_guard_notice
        ''' </summary>
        Public Property buy_guard_notice() As String
    End Class

    Public Class Info
        ''' <summary>
        ''' Uid
        ''' </summary>
        Public Property uid() As Integer
        ''' <summary>
        ''' 胖头鱼煲汤好好次
        ''' </summary>
        Public Property uname() As String
        ''' <summary>
        ''' http://i1.hdslb.com/bfs/face/074fe049c7d4bc47e3a6ae89906af5d0246a5ea1.jpg
        ''' </summary>
        Public Property uface() As String
        ''' <summary>
        ''' Main_rank
        ''' </summary>
        Public Property main_rank() As Integer
        ''' <summary>
        ''' Bili_vip
        ''' </summary>
        Public Property bili_vip() As Integer
        ''' <summary>
        ''' Mobile_verify
        ''' </summary>
        Public Property mobile_verify() As Integer
        ''' <summary>
        ''' Identification
        ''' </summary>
        Public Property identification() As Integer
    End Class

    Public Class Danmu
        ''' <summary>
        ''' Mode
        ''' </summary>
        Public Property mode() As Integer
        ''' <summary>
        ''' Color
        ''' </summary>
        Public Property color() As Integer
        ''' <summary>
        ''' Length
        ''' </summary>
        Public Property length() As Integer
        ''' <summary>
        ''' Room_id
        ''' </summary>
        Public Property room_id() As Integer
    End Class

    Public Class [Property]
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property uname_color() As String
        ''' <summary>
        ''' Bubble
        ''' </summary>
        Public Property bubble() As Integer
        ''' <summary>
        ''' Danmu
        ''' </summary>
        Public Property danmu() As Danmu
    End Class

    Public Class Recharge
        ''' <summary>
        ''' Status
        ''' </summary>
        Public Property status() As Integer
        ''' <summary>
        ''' Type
        ''' </summary>
        Public Property type() As Integer
        ''' <summary>
        ''' 首充有礼
        ''' </summary>
        Public Property value() As String
        ''' <summary>
        ''' fb7299
        ''' </summary>
        Public Property color() As String
        ''' <summary>
        ''' Config_id
        ''' </summary>
        Public Property config_id() As Integer
    End Class

    Public Class Relation
        ''' <summary>
        ''' Is_followed
        ''' </summary>
        Public Property is_followed() As Boolean
    End Class

    Public Class Wallet
        ''' <summary>
        ''' Gold
        ''' </summary>
        Public Property gold() As Integer
        ''' <summary>
        ''' Silver
        ''' </summary>
        Public Property silver() As Integer
    End Class

    Public Class Medal
        ''' <summary>
        ''' Is_weared
        ''' </summary>
        Public Property is_weared() As Boolean
        ''' <summary>
        ''' Curr_weared
        ''' </summary>
        Public Property curr_weared() As Curr_weared
    End Class

    Public Class Curr_weared
        ''' <summary>
        ''' Target_id
        ''' </summary>
        Public Property target_id() As Integer
        ''' <summary>
        ''' 一抹桃
        ''' </summary>
        Public Property target_name() As String
        ''' <summary>
        ''' 蜜桃
        ''' </summary>
        Public Property medal_name() As String
        ''' <summary>
        ''' Target_roomid
        ''' </summary>
        Public Property target_roomid() As Integer
        ''' <summary>
        ''' Level
        ''' </summary>
        Public Property level() As Integer
        ''' <summary>
        ''' Intimacy
        ''' </summary>
        Public Property intimacy() As Integer
        ''' <summary>
        ''' Next_intimacy
        ''' </summary>
        Public Property next_intimacy() As Integer
        ''' <summary>
        ''' Day_limit
        ''' </summary>
        Public Property day_limit() As Integer
        ''' <summary>
        ''' Today_feed
        ''' </summary>
        Public Property today_feed() As Integer
        ''' <summary>
        ''' Is_union
        ''' </summary>
        Public Property is_union() As Integer
    End Class

    Public Class Extra_config
        ''' <summary>
        ''' Show_bag
        ''' </summary>
        Public Property show_bag() As Boolean
        ''' <summary>
        ''' Show_vip_broadcast
        ''' </summary>
        Public Property show_vip_broadcast() As Boolean
    End Class

    Public Class Mailbox
        ''' <summary>
        ''' Switch_status
        ''' </summary>
        Public Property switch_status() As Integer
        ''' <summary>
        ''' Red_notice
        ''' </summary>
        Public Property red_notice() As Integer
    End Class

    Public Class Entry_effect
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property id() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property privilege_type() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property priority() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property web_basemap_url() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property web_effective_time() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property web_effect_close() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property web_close_time() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property copy_writing() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property copy_color() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property highlight_color() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property mock_effect() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property business() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property face() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property basemap_url() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property show_avatar() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property effective_time() As Integer
    End Class

    Public Class Welcome
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property allow_mock() As Integer
    End Class

    Public Class User_reward
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property entry_effect() As Entry_effect
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property welcome() As Welcome
    End Class

    Public Class Shield_rules
        ''' <summary>
        ''' Rank
        ''' </summary>
        Public Property rank() As Integer
        ''' <summary>
        ''' Verify
        ''' </summary>
        Public Property verify() As Integer
        ''' <summary>
        ''' Level
        ''' </summary>
        Public Property level() As Integer
    End Class

    Public Class Shield_info
        ''' <summary>
        ''' Shield_user_list
        ''' </summary>
        Public Property shield_user_list() As List(Of String)
        ''' <summary>
        ''' Keyword_list
        ''' </summary>
        Public Property keyword_list() As List(Of String)
        ''' <summary>
        ''' Shield_rules
        ''' </summary>
        Public Property shield_rules() As Shield_rules
    End Class

    Public Class Data
        ' 注释掉的是程序不需要用到的
        '''' <summary>
        '''' User_level
        '''' </summary>
        'Public Property user_level() As User_level
        '''' <summary>
        '''' Vip
        '''' </summary>
        'Public Property vip() As Vip
        '''' <summary>
        '''' Title
        '''' </summary>
        'Public Property title() As Title
        ''' <summary>
        ''' Badge
        ''' </summary>
        Public Property badge() As Badge
        '''' <summary>
        '''' Privilege
        '''' </summary>
        'Public Property privilege() As Privilege
        '''' <summary>
        '''' Info
        '''' </summary>
        'Public Property info() As Info
        ''' <summary>
        ''' Property
        ''' </summary>
        Public Property [property]() As [Property]
        '''' <summary>
        '''' Recharge
        '''' </summary>
        'Public Property recharge() As Recharge
        '''' <summary>
        '''' Relation
        '''' </summary>
        'Public Property relation() As Relation
        '''' <summary>
        '''' Wallet
        '''' </summary>
        'Public Property wallet() As Wallet
        '''' <summary>
        '''' Medal
        '''' </summary>
        'Public Property medal() As Medal
        '''' <summary>
        '''' Extra_config
        '''' </summary>
        'Public Property extra_config() As Extra_config
        '''' <summary>
        '''' Mailbox
        '''' </summary>
        'Public Property mailbox() As Mailbox
        '''' <summary>
        '''' User_reward
        '''' </summary>
        'Public Property user_reward() As User_reward
        '''' <summary>
        '''' Shield_info
        '''' </summary>
        'Public Property shield_info() As Shield_info
    End Class

    Public Class Root
        Inherits APIPostResponseBaseEntity.Root
        ''' <summary>
        ''' Data
        ''' </summary>
        Public Property data() As Data
    End Class
#Enable Warning IDE1006 ' 命名样式
End Namespace
