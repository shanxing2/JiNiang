#Disable Warning IDE1006 ' 命名样式  

'Imports System.Runtime.Serialization

Namespace DanmuEntity.HttpDanmu
    Public Class Activity_info
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property uname_color() As String
    End Class

    Public Class Room
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property text() As String
        ''' <summary>
        ''' Uid
        ''' </summary>
        Public Property uid() As Integer
        ''' <summary>
        ''' 是不一啊
        ''' </summary>
        Public Property nickname() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property uname_color() As String
        ''' <summary>
        ''' 2018-06-01 23:44:13
        ''' </summary>
        Public Property timeline() As String
        ''' <summary>
        ''' Isadmin
        ''' </summary>
        Public Property isadmin() As Integer
        ''' <summary>
        ''' Vip
        ''' </summary>
        Public Property vip() As Integer
        ''' <summary>
        ''' Svip
        ''' </summary>
        Public Property svip() As Integer
        ''' <summary>
        ''' Medal
        ''' </summary>
        Public Property medal() As List(Of String)
        ''' <summary>
        ''' Title
        ''' </summary>
        Public Property title() As List(Of String)
        ''' <summary>
        ''' User_level
        ''' </summary>
        Public Property user_level() As List(Of String)
        ''' <summary>
        ''' Rank
        ''' </summary>
        Public Property rank() As Integer
        ''' <summary>
        ''' Teamid
        ''' </summary>
        Public Property teamid() As Integer
        ''' <summary>
        ''' Rnd（据说这个是进入首次进入直播间的时间）
        ''' </summary>
        ''' 有些用户(@spurt寒溪)发弹幕带的 rnd 特殊，超出 integer 的范围，得用long(可能有负数，所以不能是ulong)才行 20180616
        ''' 有些用户的为空，所以得改为字符串类型20200221
        ''' 984132755015867244
        Public Property rnd() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property user_title() As String
        ''' <summary>
        ''' Guard_level
        ''' </summary>
        Public Property guard_level() As Integer
        ''' <summary>
        ''' Activity_info
        ''' </summary>
        Public Property activity_info() As Activity_info

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property bubble As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property check_info As Check_info

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property lpl As Integer
    End Class

    Public Class Data
        ''' <summary>
        ''' Room
        ''' </summary>
        Public Property room() As List(Of Room)
        ''' <summary>
        ''' Admin
        ''' </summary>
        Public Property admin() As List(Of String)
    End Class

    Public Class Check_info

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property ts As Long

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property ct As String

    End Class

    Public Class Root
        ''' <summary>
        ''' Code
        ''' </summary>
        Public Property code() As Integer
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property msg() As String
        ''' <summary>
        ''' Data
        ''' </summary>
        Public Property data() As Data
    End Class
End Namespace


Namespace DanmuEntity.SEND_GIFT
    Public Class Top_list
        ''' <summary>
        ''' Uid
        ''' </summary>
        Public Property uid() As Integer
        ''' <summary>
        ''' 软骨理客中可笑
        ''' </summary>
        Public Property uname() As String
        ''' <summary>
        ''' http://static.hdslb.com/images/member/noface.gif
        ''' </summary>
        Public Property face() As String
        ''' <summary>
        ''' Rank
        ''' </summary>
        Public Property rank() As Integer
        ''' <summary>
        ''' Score
        ''' </summary>
        Public Property score() As Integer
        ''' <summary>
        ''' Guard_level
        ''' </summary>
        Public Property guard_level() As Integer
        ''' <summary>
        ''' IsSelf
        ''' </summary>
        Public Property isSelf() As Integer
    End Class

    Public Class Smalltv_msg
        ''' <summary>
        ''' SYS_MSG
        ''' </summary>
        Public Property cmd() As String
        ''' <summary>
        ''' 哔哩哔哩直播:?送给:?两仪滚:?一个小电视飞船，点击前往TA的房间去抽奖吧
        ''' </summary>
        Public Property msg() As String
        ''' <summary>
        ''' 哔哩哔哩直播:?送给:?两仪滚:?一个小电视飞船，点击前往TA的房间去抽奖吧
        ''' </summary>
        Public Property msg_text() As String
        ''' <summary>
        ''' Rep
        ''' </summary>
        Public Property rep() As Integer
        ''' <summary>
        ''' StyleType
        ''' </summary>
        Public Property styleType() As Integer
        ''' <summary>
        ''' http://live.bilibili.com/388
        ''' </summary>
        Public Property url() As String
        ''' <summary>
        ''' Roomid
        ''' </summary>
        Public Property roomid() As Integer
        ''' <summary>
        ''' Real_roomid
        ''' </summary>
        Public Property real_roomid() As Integer
        ''' <summary>
        ''' Rnd
        ''' </summary>
        Public Property rnd() As Integer
        ''' <summary>
        ''' Tv_id
        ''' </summary>
        Public Property tv_id() As Integer
    End Class

    Public Class Colorful
        ''' <summary>
        ''' Coin
        ''' </summary>
        Public Property coin() As Integer
        ''' <summary>
        ''' Change
        ''' </summary>
        Public Property change() As Integer
        ''' <summary>
        ''' Progress
        ''' </summary>
        Public Property progress() As Progress
    End Class

    Public Class Progress
        ''' <summary>
        ''' Now
        ''' </summary>
        Public Property now() As Integer
        ''' <summary>
        ''' Max
        ''' </summary>
        Public Property max() As Integer
    End Class

    Public Class Normal
        ''' <summary>
        ''' Coin
        ''' </summary>
        Public Property coin() As Integer
        ''' <summary>
        ''' Change
        ''' </summary>
        Public Property change() As Integer
        ''' <summary>
        ''' Progress
        ''' </summary>
        Public Property progress() As Progress
    End Class

    Public Class Capsule
        ''' <summary>
        ''' Colorful
        ''' </summary>
        Public Property colorful() As Colorful
        ''' <summary>
        ''' Normal
        ''' </summary>
        Public Property normal() As Normal
        ''' <summary>
        ''' Move
        ''' </summary>
        Public Property move() As Integer
    End Class

    Public Class Data
        ''' <summary>
        ''' 凉了
        ''' </summary>
        Public Property giftName() As String
        ''' <summary>
        ''' Num
        ''' </summary>
        Public Property num() As Integer
        ''' <summary>
        ''' 清明连雨
        ''' </summary>
        Public Property uname() As String
        ''' <summary>
        ''' http://i1.hdslb.com/bfs/face/36f6c927bd7e128e55d85b0a94732b3dfb3d7b96.jpg
        ''' </summary>
        Public Property face() As String
        ''' <summary>
        ''' Guard_level
        ''' </summary>
        Public Property guard_level() As Integer
        ''' <summary>
        ''' Rcost
        ''' </summary>
        Public Property rcost() As Integer
        ''' <summary>
        ''' Uid
        ''' </summary>
        Public Property uid() As Integer
        ''' <summary>
        ''' Top_list
        ''' </summary>
        Public Property top_list() As List(Of Top_list)
        ''' <summary>
        ''' Timestamp
        ''' </summary>
        Public Property Timestamp() As Integer
        ''' <summary>
        ''' GiftId
        ''' </summary>
        Public Property giftId() As Integer
        ''' <summary>
        ''' GiftType
        ''' </summary>
        Public Property giftType() As Integer
        ''' <summary>
        ''' 赠送
        ''' </summary>
        Public Property action() As String
        ''' <summary>
        ''' Super
        ''' </summary>
        Public Property super() As Integer
        ''' <summary>
        ''' Super_gift_num
        ''' </summary>
        Public Property super_gift_num() As Integer
        ''' <summary>
        ''' Price
        ''' </summary>
        Public Property price() As Integer
        ''' <summary>
        ''' 977840162
        ''' </summary>
        Public Property rnd() As String
        ''' <summary>
        ''' NewMedal
        ''' </summary>
        Public Property newMedal() As Integer
        ''' <summary>
        ''' NewTitle
        ''' </summary>
        Public Property newTitle() As Integer
        ''' <summary>
        ''' Medal
        ''' </summary>
        Public Property medal() As List(Of String)
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property title() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property beatId() As String
        ''' <summary>
        ''' live
        ''' </summary>
        Public Property biz_source() As String
        ''' <summary>
        ''' 
        ''' </summary>
        Public Property metadata() As String
        ''' <summary>
        ''' Remain
        ''' </summary>
        Public Property remain() As Integer
        ''' <summary>
        ''' Gold
        ''' </summary>
        Public Property gold() As Integer
        ''' <summary>
        ''' Silver
        ''' </summary>
        Public Property silver() As Integer
        ''' <summary>
        ''' EventScore
        ''' </summary>
        Public Property eventScore() As Integer
        ''' <summary>
        ''' EventNum
        ''' </summary>
        Public Property eventNum() As Integer
        ''' <summary>
        ''' Smalltv_msg
        ''' </summary>
        Public Property smalltv_msg() As List(Of String)
        ''' <summary>
        ''' SpecialGift
        ''' </summary>
        Public Property specialGift() As String
        ''' <summary>
        ''' Notice_msg
        ''' </summary>
        Public Property notice_msg() As List(Of String)
        ''' <summary>
        ''' Capsule
        ''' </summary>
        Public Property capsule() As Capsule
        ''' <summary>
        ''' AddFollow
        ''' </summary>
        Public Property addFollow() As Integer
        ''' <summary>
        ''' Effect_block
        ''' </summary>
        Public Property effect_block() As Integer
        ''' <summary>
        ''' gold
        ''' </summary>
        Public Property coin_type() As String
        ''' <summary>
        ''' Total_coin
        ''' </summary>
        Public Property total_coin() As Integer
    End Class

    Public Class Root
        ''' <summary>
        ''' SEND_GIFT
        ''' </summary>
        Public Property cmd() As String
        ''' <summary>
        ''' Data
        ''' </summary>
        Public Property data() As Data
    End Class
End Namespace


Namespace DanmuEntity.WELCOME
    Public Class Data
        ''' <summary>
        ''' Uid
        ''' </summary>
        Public Property uid() As Integer
        ''' <summary>
        ''' 昵称
        ''' </summary>
        Public Property uname() As String
        ''' <summary>
        ''' 是否管理员
        ''' </summary>
        Public Property is_admin() As Boolean
        ''' <summary>
        ''' 月费老爷
        ''' </summary>
        Public Property vip() As Integer
        ''' <summary>
        ''' 年费老爷
        ''' </summary>
        Public Property svip() As Integer
    End Class

    Public Class Root
        ''' <summary>
        ''' WELCOME
        ''' </summary>
        Public Property cmd() As String
        ''' <summary>
        ''' Data
        ''' </summary>
        Public Property data() As Data
    End Class
End Namespace

Namespace DanmuEntity.WELCOME_GUARD
    Public Class Data
        ''' <summary>
        ''' Uid
        ''' </summary>
        Public Property uid() As Integer
        ''' <summary>
        ''' 唐人说梦
        ''' </summary>
        Public Property username() As String
        ''' <summary>
        ''' Guard_level
        ''' </summary>
        Public Property guard_level() As Integer
    End Class

    Public Class Root
        ''' <summary>
        ''' WELCOME_GUARD
        ''' </summary>
        Public Property cmd() As String
        ''' <summary>
        ''' Data
        ''' </summary>
        Public Property data() As Data
    End Class
End Namespace

Namespace DanmuEntity.WISH_BOTTLE
    Public Class Wish
        ''' <summary>
        ''' Id
        ''' </summary>
        Public Property id() As Integer
        ''' <summary>
        ''' Uid
        ''' </summary>
        Public Property uid() As Integer
        ''' <summary>
        ''' Type
        ''' </summary>
        Public Property type() As Integer
        ''' <summary>
        ''' Type_id
        ''' </summary>
        Public Property type_id() As Integer
        ''' <summary>
        ''' Wish_limit
        ''' </summary>
        Public Property wish_limit() As Integer
        ''' <summary>
        ''' Wish_progress
        ''' </summary>
        Public Property wish_progress() As Integer
        ''' <summary>
        ''' Status
        ''' </summary>
        Public Property status() As Integer
        ''' <summary>
        ''' 猛男勋章领取处
        ''' </summary>
        Public Property content() As String
        ''' <summary>
        ''' 2018-01-27 18:53:19
        ''' </summary>
        Public Property ctime() As Date
        ''' <summary>
        ''' Count_map
        ''' </summary>
        Public Property count_map() As List(Of Integer)
    End Class

    Public Class Data
        ''' <summary>
        ''' update
        ''' </summary>
        Public Property action() As String
        ''' <summary>
        ''' Id
        ''' </summary>
        Public Property id() As Integer
        ''' <summary>
        ''' Wish
        ''' </summary>
        Public Property wish() As Wish
    End Class

    Public Class Root
        ''' <summary>
        ''' WISH_BOTTLE
        ''' </summary>
        Public Property cmd() As String
        ''' <summary>
        ''' Data
        ''' </summary>
        Public Property data() As Data
    End Class
End Namespace


#Enable Warning IDE1006 ' 命名样式