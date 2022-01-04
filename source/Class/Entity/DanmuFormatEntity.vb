Imports 姬娘插件.Utils

Public Class DanmuFormatEntity
    Private m_FormatDic As Dictionary(Of String, FormatInfo)
#Region "属性区"
    Public Property FormatDic As Dictionary(Of String, FormatInfo)
        Get
            If m_FormatDic Is Nothing Then
                MakeDefaultFormats()
            End If

            Return m_FormatDic
        End Get
        Set
            m_FormatDic = Value
        End Set
    End Property

#End Region


#Region "构造函数区"

    Private Sub MakeDefaultFormats()
        ' 内置已知类型，还可以动态加载数据库中用户新增的类型
        Dim nowDmCmds = System.Enum.GetNames(GetType(DmCmd))

        m_FormatDic = New Dictionary(Of String, FormatInfo) From {
            {DmCmd.DANMU_MSG.ToString, New FormatInfo With {
            .Id = DmCmd.DANMU_MSG,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.SEND_GIFT.ToString, New FormatInfo With {
            .Id = DmCmd.SEND_GIFT,
            .DefaultStyle = "蟹蟹 {uname} {action}{giftName}",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.WELCOME.ToString, New FormatInfo With {
            .Id = DmCmd.WELCOME,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.WELCOME_GUARD.ToString, New FormatInfo With {
            .Id = DmCmd.WELCOME_GUARD,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.SYS_MSG.ToString, New FormatInfo With {
            .Id = DmCmd.SYS_MSG,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.PREPARING.ToString, New FormatInfo With {
            .Id = DmCmd.PREPARING,
            .DefaultStyle = "直播结束，挥挥~",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.SYS_GIFT.ToString, New FormatInfo With {
            .Id = DmCmd.SYS_GIFT,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.LIVE.ToString, New FormatInfo With {
            .Id = DmCmd.LIVE,
            .DefaultStyle = "开播啦，快搬来小板凳围观吧",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.WISH_BOTTLE.ToString, New FormatInfo With {
            .Id = DmCmd.WISH_BOTTLE,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.ROOM_BLOCK_MSG.ToString, New FormatInfo With {
            .Id = DmCmd.ROOM_BLOCK_MSG,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.ROOM_RANK.ToString, New FormatInfo With {
            .Id = DmCmd.ROOM_RANK,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.COMBO_SEND.ToString, New FormatInfo With {
            .Id = DmCmd.COMBO_SEND,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.COMBO_END.ToString, New FormatInfo With {
            .Id = DmCmd.COMBO_END,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.ROOM_SILENT_OFF.ToString, New FormatInfo With {
            .Id = DmCmd.ROOM_SILENT_OFF,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.ROOM_SILENT_ON.ToString, New FormatInfo With {
            .Id = DmCmd.ROOM_SILENT_ON,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.GUARD_MSG.ToString, New FormatInfo With {
            .Id = DmCmd.GUARD_MSG,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.TV_START.ToString, New FormatInfo With {
            .Id = DmCmd.TV_START,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.TV_END.ToString, New FormatInfo With {
            .Id = DmCmd.TV_END,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.GUARD_BUY.ToString, New FormatInfo With {
            .Id = DmCmd.GUARD_BUY,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.SPECIAL_GIFT.ToString, New FormatInfo With {
            .Id = DmCmd.SPECIAL_GIFT,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.RAFFLE_START.ToString, New FormatInfo With {
            .Id = DmCmd.RAFFLE_START,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.RAFFLE_END.ToString, New FormatInfo With {
            .Id = DmCmd.RAFFLE_END,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.EVENT_CMD.ToString, New FormatInfo With {
            .Id = DmCmd.EVENT_CMD,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.CUT_OFF.ToString, New FormatInfo With {
            .Id = DmCmd.CUT_OFF,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.ROOM_ADMINS.ToString, New FormatInfo With {
            .Id = DmCmd.ROOM_ADMINS,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.PK_INVITE_INIT.ToString, New FormatInfo With {
            .Id = DmCmd.PK_INVITE_INIT,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.PK_INVITE_CANCEL.ToString, New FormatInfo With {
            .Id = DmCmd.PK_INVITE_CANCEL,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.PK_INVITE_SWITCH_OPEN.ToString, New FormatInfo With {
            .Id = DmCmd.PK_INVITE_SWITCH_OPEN,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.PK_INVITE_SWITCH_CLOSE.ToString, New FormatInfo With {
            .Id = DmCmd.PK_INVITE_SWITCH_CLOSE,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.PK_MATCH.ToString, New FormatInfo With {
            .Id = DmCmd.PK_MATCH,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.PK_PRE.ToString, New FormatInfo With {
            .Id = DmCmd.PK_PRE,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.PK_START.ToString, New FormatInfo With {
            .Id = DmCmd.PK_START,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.PK_END.ToString, New FormatInfo With {
            .Id = DmCmd.PK_END,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.PK_PROCESS.ToString, New FormatInfo With {
            .Id = DmCmd.PK_PROCESS,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.PK_SETTLE.ToString, New FormatInfo With {
            .Id = DmCmd.PK_SETTLE,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.PK_MIC_END.ToString, New FormatInfo With {
            .Id = DmCmd.PK_MIC_END,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.ENTRY_EFFECT.ToString, New FormatInfo With {
                .Id = DmCmd.ENTRY_EFFECT,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.PK_CLICK_AGAIN.ToString, New FormatInfo With {
                .Id = DmCmd.PK_CLICK_AGAIN,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.PK_AGAIN.ToString, New FormatInfo With {
                .Id = DmCmd.PK_AGAIN,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.PK_BATTLE_PROCESS.ToString, New FormatInfo With {
                .Id = DmCmd.PK_BATTLE_PROCESS,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.PK_BATTLE_START.ToString, New FormatInfo With {
                .Id = DmCmd.PK_BATTLE_START,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.PK_BATTLE_END.ToString, New FormatInfo With {
                .Id = DmCmd.PK_BATTLE_END,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.PK_BATTLE_SETTLE.ToString, New FormatInfo With {
                .Id = DmCmd.PK_BATTLE_SETTLE,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.PK_BATTLE_SETTLE_USER.ToString, New FormatInfo With {
                .Id = DmCmd.PK_BATTLE_SETTLE_USER,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.PK_BATTLE_ENTRANCE.ToString, New FormatInfo With {
                .Id = DmCmd.PK_BATTLE_ENTRANCE,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.PK_LOTTERY_START.ToString, New FormatInfo With {
                .Id = DmCmd.PK_LOTTERY_START,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.PK_BATTLE_PRE.ToString, New FormatInfo With {
                .Id = DmCmd.PK_BATTLE_PRE,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.PK_BATTLE_CRIT.ToString, New FormatInfo With {
                .Id = DmCmd.PK_BATTLE_CRIT,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.NOTICE_MSG.ToString, New FormatInfo With {
            .Id = DmCmd.NOTICE_MSG,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.NOTICE_MSG_H5.ToString, New FormatInfo With {
            .Id = DmCmd.NOTICE_MSG_H5,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.GUARD_LOTTERY_START.ToString, New FormatInfo With {
            .Id = DmCmd.GUARD_LOTTERY_START,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.WARNING.ToString, New FormatInfo With {
            .Id = DmCmd.WARNING,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.ACTIVITY_EVENT.ToString, New FormatInfo With {
            .Id = DmCmd.ACTIVITY_EVENT,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.Room_Admin_Entrance.ToString, New FormatInfo With {
            .Id = DmCmd.Room_Admin_Entrance,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.USER_TOAST_MSG.ToString, New FormatInfo With {
            .Id = DmCmd.USER_TOAST_MSG,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.CHANGE_ROOM_INFO.ToString, New FormatInfo With {
            .Id = DmCmd.CHANGE_ROOM_INFO,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.ROOM_REAL_TIME_MESSAGE_UPDATE.ToString, New FormatInfo With {
            .Id = DmCmd.ROOM_REAL_TIME_MESSAGE_UPDATE,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.ROOM_CHANGE.ToString, New FormatInfo With {
            .Id = DmCmd.ROOM_CHANGE,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.BOSS_ENERGY.ToString, New FormatInfo With {
            .Id = DmCmd.BOSS_ENERGY,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.ACTIVITY_BANNER_RED_NOTICE_CLOSE.ToString, New FormatInfo With {
            .Id = DmCmd.ACTIVITY_BANNER_RED_NOTICE_CLOSE,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.ACTIVITY_BANNER_UPDATE.ToString, New FormatInfo With {
            .Id = DmCmd.ACTIVITY_BANNER_UPDATE,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.ACTIVITY_BANNER_UPDATE_V2.ToString, New FormatInfo With {
            .Id = DmCmd.ACTIVITY_BANNER_UPDATE_V2,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.ACTIVITY_BANNER_UPDATE_BLS.ToString, New FormatInfo With {
                .Id = DmCmd.ACTIVITY_BANNER_UPDATE_BLS,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}},
            {DmCmd.DAILY_QUEST_NEWDAY.ToString, New FormatInfo With {
                .Id = DmCmd.DAILY_QUEST_NEWDAY,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}},
            {DmCmd.ACTIVITY_MATCH_GIFT.ToString, New FormatInfo With {
            .Id = DmCmd.ACTIVITY_MATCH_GIFT,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.MATCH_TEAM_GIFT_RANK.ToString, New FormatInfo With {
            .Id = DmCmd.MATCH_TEAM_GIFT_RANK,
            .DefaultStyle = "",
            .CustomStyle = Nothing,
            .Memo = Nothing}},
            {DmCmd.LOL_ACTIVITY.ToString, New FormatInfo With {
                .Id = DmCmd.LOL_ACTIVITY,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.WEEK_STAR_CLOCK.ToString, New FormatInfo With {
                .Id = DmCmd.WEEK_STAR_CLOCK,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.ROOM_SHIELD.ToString, New FormatInfo With {
                .Id = DmCmd.ROOM_SHIELD,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.HOUR_RANK_AWARDS.ToString, New FormatInfo With {
                .Id = DmCmd.HOUR_RANK_AWARDS,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.new_anchor_reward.ToString, New FormatInfo With {
                .Id = DmCmd.new_anchor_reward,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.ANCHOR_LOT_START.ToString, New FormatInfo With {
                .Id = DmCmd.ANCHOR_LOT_START,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.ANCHOR_LOT_END.ToString, New FormatInfo With {
                .Id = DmCmd.ANCHOR_LOT_END,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.ANCHOR_LOT_AWARD.ToString, New FormatInfo With {
                .Id = DmCmd.ANCHOR_LOT_AWARD,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.ANCHOR_NORMAL_NOTIFY.ToString, New FormatInfo With {
                .Id = DmCmd.ANCHOR_NORMAL_NOTIFY,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.HOT_ROOM_NOTIFY.ToString, New FormatInfo With {
                .Id = DmCmd.HOT_ROOM_NOTIFY,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.ROOM_SKIN_MSG.ToString, New FormatInfo With {
                .Id = DmCmd.ROOM_SKIN_MSG,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.ROOM_BOX_MASTER.ToString, New FormatInfo With {
                .Id = DmCmd.ROOM_BOX_MASTER,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.SUPER_CHAT_MESSAGE.ToString, New FormatInfo With {
                .Id = DmCmd.SUPER_CHAT_MESSAGE,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.SUPER_CHAT_MESSAGE_JPN.ToString, New FormatInfo With {
                .Id = DmCmd.SUPER_CHAT_MESSAGE_JPN,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.ROOM_BANNER.ToString, New FormatInfo With {
                .Id = DmCmd.ROOM_BANNER,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.PANEL.ToString, New FormatInfo With {
                .Id = DmCmd.PANEL,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.STOP_LIVE_ROOM_LIST.ToString, New FormatInfo With {
                .Id = DmCmd.STOP_LIVE_ROOM_LIST,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.INTERACT_WORD.ToString, New FormatInfo With {
                .Id = DmCmd.INTERACT_WORD,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.ONLINE_RANK_COUNT.ToString, New FormatInfo With {
                .Id = DmCmd.ONLINE_RANK_COUNT,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            },
            {DmCmd.ONLINE_RANK_V2.ToString, New FormatInfo With {
                .Id = DmCmd.ONLINE_RANK_V2,
                .DefaultStyle = "",
                .CustomStyle = Nothing,
                .Memo = Nothing}
            }
        }

        Debug.Assert(nowDmCmds.Length = FormatDic.Count, "DmCmd 格式个数与已定义的不一致，请联系技术员更新")
    End Sub
#End Region

    <DebuggerDisplay("{DebuggerDisplay,nq}")>
    Class FormatInfo
        ''' <summary>
        ''' 标识
        ''' </summary>
        ''' <returns></returns>
        Public Property Id As DmCmd
        ''' <summary>
        ''' 默认格式
        ''' </summary>
        ''' <returns></returns>
        Public Property DefaultStyle As String
        ''' <summary>
        ''' 自定义格式
        ''' </summary>
        ''' <returns></returns>
        Public Property CustomStyle As String
        ''' <summary>
        ''' 备注
        ''' </summary>
        ''' <returns></returns>
        Public Property Memo As String

        ''' <summary>
        ''' IDE调式专用属性，在项目中无实际作用
        ''' </summary>
        ''' <returns></returns>
        Private ReadOnly Property DebuggerDisplay As String
            Get
                Return $"{NameOf(Id)}:{Id.ToString}   {NameOf(DefaultStyle)}:{DefaultStyle} {NameOf(CustomStyle)}:{CustomStyle}"
            End Get
        End Property

    End Class
End Class
