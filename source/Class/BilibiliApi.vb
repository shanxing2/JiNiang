Imports System.Net
Imports System.Text
Imports ShanXingTech
Imports ShanXingTech.Net2

Public NotInheritable Class BilibiliApi

#Region "字段区"
    Private Shared ReadOnly m_HttpHeadersParam As Dictionary(Of String, String)
    Private Shared m_User As UserInfo
#End Region

#Region "属性区"

    Protected Friend Shared ReadOnly Property Referer() As String
        Get
            'Return "https://live.bilibili.com/" & User?.ViewRoom?.ShortId
            'Dim a = If(, m_User.RoomShortId, m_User.ViewRoom.RealId)
            Return "https://live.bilibili.com/" & If(m_User.ViewRoom Is Nothing,
                m_User.RoomShortId.ToStringOfCulture,
                m_User.ViewRoom.RealId)
        End Get
    End Property
#End Region

#Region "常量区"
    ''' <summary>
    ''' 提交获取弹幕请求的链接
    ''' </summary>
    Private Const GetDanmuUrl As String = "https://api.live.bilibili.com/ajax/msg"

#End Region

#Region "枚举区"
    ''' <summary>
    ''' '禁言时间
    ''' </summary>
    Public Enum SilentInterval
        ''' <summary>
        ''' 全场禁言
        ''' </summary>
        Always = 0
        ''' <summary>
        ''' 禁言3分钟
        ''' </summary>
        Three = 3
        ''' <summary>
        ''' 禁言10分钟
        ''' </summary>
        Ten = 10
        ''' <summary>
        ''' 禁言30分钟
        ''' </summary>
        Thirty = 30
    End Enum

	''' <summary>
	''' 禁言类型
	''' </summary>
	Public Enum SilentType
		''' <summary>
		''' 解除禁言
		''' </summary>
		Off
		''' <summary>
		''' '等级
		''' </summary>
		Level
		''' <summary>
		''' 粉丝勋章
		''' </summary>
		Medal
		''' <summary>
		''' 全员
		''' </summary>
		Member
	End Enum

	''' <summary>
	''' 屏蔽类型
	''' </summary>
	Public Enum ShieldType
		''' <summary>
		''' 无，为启动屏蔽,初始化时用
		''' </summary>
		None
		''' <summary>
		''' '等级
		''' </summary>
		Level
		''' <summary>
		''' 非正式会员
		''' </summary>
		Rank
		''' <summary>
		''' 未绑定手机用户
		''' </summary>
		Verify
	End Enum

	''' <summary>
	''' 屏蔽动作
	''' </summary>
	Public Enum ShieldOperate
		''' <summary>
		''' 关闭
		''' </summary>
		Off = 0
		''' <summary>
		''' 开启
		''' </summary>
		[On] = 1
	End Enum


	''' <summary>
	''' 屏蔽关键词动作
	''' </summary>
	Private Enum ShieldKeywordOperate
		Remove = 0
		Add = 1
	End Enum

	''' <summary>
	''' 举报弹幕理由
	''' </summary>
	Public Enum ReportReason
		''' <summary>
		''' 未选中任何理由
		''' </summary>
		无 = 0
		违法违规 = 1
		低俗色情
		垃圾广告
		辱骂引战
		政治敏感
		青少年不良信息
		其他
	End Enum
#End Region

#Region "构造函数区"
	''' <summary>
	''' 类构造函数
	''' 类之内的任意一个静态方法第一次调用时调用此构造函数
	''' 而且程序生命周期内仅调用一次
	''' </summary>
	Shared Sub New()
		m_HttpHeadersParam = New Dictionary(Of String, String) From {
			{"User-Agent", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:55.0) Gecko/20100101 Firefox/55.0"},
			{"Connection", "keep-alive"},
			{"Cache-Control", "no-cache"},
			{"Accept-Encoding", "gzip, deflate"}
		}
	End Sub

	'Sub New()

	'    '
	'End Sub
#End Region


#Region "函数区"

	Public Shared Sub Init(ByRef user As UserInfo)
		m_User = user
	End Sub

	''' <summary>
	''' 只处理正常情况下返回文本中包含 code 键的 api post请求，其他post请求请调用 <seealso cref="DoPostAsync(String, String, String)"/>
	''' </summary>
	''' <param name="url"></param>
	''' <param name="postData"></param>
	''' <param name="referer"></param>
	''' <returns>Success    POST请求成功与否；Message  请求结果状态码或者请求返回的提示；Result 请求结果</returns>
	Public Shared Async Function DoApiPostAsync(ByVal url As String, ByVal postData As String, ByVal referer As String) As Task(Of (Success As Boolean, Message As String, Result As String))
		m_HttpHeadersParam("Referer") = referer
		Dim postRst = Await HttpAsync.Instance.TryPostAsync(url, m_HttpHeadersParam, postData, Encoding.UTF8, 3)
		If postRst.StatusCode <> HttpStatusCode.OK Then
			Return (False, postRst.StatusCode.ToStringOfCulture, String.Empty)
		End If

		Dim root As APIPostResponseBaseEntity.Root = Nothing
		Dim result = postRst.Message
		Dim sucess As Boolean
		Dim msg As String
		Try
			root = MSJsSerializer.Deserialize(Of APIPostResponseBaseEntity.Root)(result)
		Catch ex As Exception
			Logger.WriteLine(ex)
		Finally
			If root Is Nothing Then
				sucess = postRst.Success
				msg = "解析返回信息失败"
			Else
				msg = If(root.msg, root.message)
				sucess = root.code = 0
			End If
		End Try

		Return (sucess, msg, result)
	End Function

	''' <summary>
	''' 任何post请求都可以调用。正常情况下返回文本中包含 code 键的api post请求，建议调用 <seealso cref="DoApiPostAsync(String, String, String) "/>
	''' </summary>
	''' <param name="url"></param>
	''' <param name="postData"></param>
	''' <param name="referer"></param>
	''' <returns>Success    POST请求成功与否；Message  请求结果状态码或者请求返回的提示；Result 请求结果</returns>
	Public Shared Async Function DoPostAsync(ByVal url As String, ByVal postData As String, ByVal referer As String) As Task(Of HttpResponse)
		m_HttpHeadersParam("Referer") = referer
		Return Await HttpAsync.Instance.TryPostAsync(url, m_HttpHeadersParam, postData, Encoding.UTF8, 3)
	End Function

	''' <summary>
	''' 直播间禁言
	''' </summary>
	''' <param name="viewerIdOrNick"></param>
	''' <param name="hour"></param>
	''' <returns></returns>
	Public Shared Async Function RoomBlackViewerAsync(ByVal viewerIdOrNick As String, ByVal hour As Integer) As Task(Of (Success As Boolean, Message As String, Result As String))
		Dim url = "https://api.live.bilibili.com/liveact/room_block_user"
		Dim postData = $"roomid={m_User.ViewRoom.RealId}&type=1&content={viewerIdOrNick}&hour={hour.ToStringOfCulture}&csrf_token={m_User.Token}&csrf={m_User.Token}&visit_id="
		Return Await DoApiPostAsync(url, postData, Referer)
	End Function

	''' <summary>
	''' 撤销直播间禁言
	''' </summary>
	''' <param name="blackId">选中的黑名单Id</param>
	''' <returns></returns>
	Public Shared Async Function RoomUnBlackViewerAsync(ByVal blackId As String) As Task(Of (Success As Boolean, Message As String, Result As String))
		Dim url = "https://api.live.bilibili.com/banned_service/v1/Silent/del_room_block_user"
		Dim postData = $"id={blackId}&roomid={m_User.ViewRoom.RealId}&csrf_token={m_User.Token}&csrf={m_User.Token}&visit_id="
		Return Await DoApiPostAsync(url, postData, Referer)
	End Function

	''' <summary>
	''' 直播间屏蔽观众
	''' </summary>
	''' <param name="viewerId"></param>
	''' <returns></returns>
	Public Shared Async Function RoomShieldViewerAsync(ByVal viewerId As String) As Task(Of (Success As Boolean, Message As String, Result As String))
		Return Await InternalRoomShieldViewerAsync(viewerId, True)
	End Function

	''' <summary>
	''' 直播间取消屏蔽观众
	''' </summary>
	''' <param name="viewerId"></param>
	''' <returns></returns>
	Public Shared Async Function RoomUnShieldViewerAsync(ByVal viewerId As String) As Task(Of (Success As Boolean, Message As String, Result As String))
		Return Await InternalRoomShieldViewerAsync(viewerId, False)
	End Function

	''' <summary>
	''' 直播间屏蔽观众
	''' </summary>
	''' <param name="viewerId"></param>
	''' <param name="shield">True——屏蔽，False——取消屏蔽</param>
	''' <returns></returns>
	Private Shared Async Function InternalRoomShieldViewerAsync(ByVal viewerId As String, ByVal shield As Boolean) As Task(Of (Success As Boolean, Message As String, Result As String))
		Dim url = "https://api.live.bilibili.com/liveact/shield_user"
		Dim postData = $"roomid={m_User.ViewRoom.RealId}&uid={viewerId}&type={If(shield, "1", "0")}&csrf_token={m_User.Token}&csrf={m_User.Token}&visit_id="

		Return Await DoApiPostAsync(url, postData, Referer)
	End Function

	''' <summary>
	''' 直播间举报弹幕
	''' </summary>
	''' <param name="roomId"></param>
	''' <param name="viewerId"></param>
	''' <param name="ts">弹幕信息中的ts(发弹幕时间)</param>
	''' <param name="sign">弹幕信息中的ct</param>
	''' <param name="danmu"></param>
	''' <param name="reason"></param>
	''' <returns></returns>
	Public Shared Async Function RoomReportDanmuAsync(ByVal roomId As String, ByVal viewerId As String, ByVal ts As String, ByVal sign As String, ByVal danmu As String, ByVal reason As ReportReason) As Task(Of (Success As Boolean, Message As String, Result As String))
		Dim url = "https://api.live.bilibili.com/room_ex/v1/Danmu/danmuReport"
		Dim postData = $"id=0&roomid={roomId}&uid={viewerId}&msg={danmu}&reason={reason}&ts={ts}&sign={sign}&reason_id={reason}&token=&csrf_token={m_User.Token}&csrf={m_User.Token}&visit_id"

		Return Await DoApiPostAsync(url, postData, Referer)
	End Function

	''' <summary>
	''' 获取直播间举报弹幕 举报理由信息
	''' </summary>
	''' <returns></returns>
	Public Shared Async Function GetRoomReportDanmuReasonAsync() As Task(Of HttpResponse)
		Dim url = "https://api.live.bilibili.com/room_ex/v1/Danmu/forDanmuReason"
		m_HttpHeadersParam("Referer") = Referer
		Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
	End Function

	''' <summary>
	''' 直播间添加房管
	''' </summary>
	''' <param name="viewerIdOrNick">观众的Id或者昵称</param>
	''' <returns></returns>
	Public Shared Async Function RoomAppointAdminAsync(ByVal viewerIdOrNick As String) As Task(Of (Success As Boolean, Message As String, Result As String))
		Dim url = "https://api.live.bilibili.com/live_user/v1/RoomAdmin/add"
		Dim postData = $"admin={viewerIdOrNick}&anchor_id={m_User.Id}&csrf_token={m_User.Token}&csrf={m_User.Token}&visit_id="

		Return Await DoApiPostAsync(url, postData, Referer)
	End Function

	''' <summary>
	''' 撤销直播间房管
	''' </summary>
	''' <param name="viewerId">选中的观众Id</param>
	''' <returns></returns>
	Public Shared Async Function RoomUnAppointAdminAsync(ByVal viewerId As String) As Task(Of (Success As Boolean, Message As String, Result As String))
		Dim url = "https://api.live.bilibili.com/xlive/app-ucenter/v1/roomAdmin/dismiss"
		Dim postData = $"uid={viewerId}&csrf_token={m_User.Token}&csrf={m_User.Token}&visit_id="

		Return Await DoApiPostAsync(url, postData, Referer)
	End Function

	''' <summary>
	''' 获取当前用户直播间的房管列表
	''' </summary>
	''' <param name="page"></param>
	''' <returns></returns>
	Public Shared Async Function GetRoomAdminByAnchorAsync(Optional ByVal page As Integer = 1) As Task(Of HttpResponse)
		Dim url = $"https://api.live.bilibili.com/xlive/app-ucenter/v1/roomAdmin/get_by_anchor?page={page.ToStringOfCulture}"
		m_HttpHeadersParam("Referer") = Referer
		Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
	End Function

	''' <summary>
	''' 获取某直播间房管列表
	''' </summary>
	''' <param name="roomId">直播间Id</param>
	''' <param name="page"></param>
	''' <returns></returns>
	Public Shared Async Function GetRoomAdminByRoomAsync(ByVal roomId As String， Optional ByVal page As Integer = 1, Optional ByVal pageSize As Integer = 10) As Task(Of HttpResponse)
		Dim url = $"https://api.live.bilibili.com/xlive/web-room/v1/roomAdmin/get_by_room?roomid={roomId}&page_size={pageSize.ToStringOfCulture}&page={page.ToStringOfCulture}"
		m_HttpHeadersParam("Referer") = Referer
		Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
	End Function

	''' <summary>
	''' 获取黑名单列表
	''' </summary>
	''' <param name="page"></param>
	''' <returns></returns>
	Public Shared Async Function GetRoomBlockListAsync(ByVal page As Integer) As Task(Of HttpResponse)
		Dim url = $"https://api.live.bilibili.com/liveact/ajaxGetBlockList?roomid={m_User.ViewRoom.RealId}&page={page.ToStringOfCulture}"
		m_HttpHeadersParam("Referer") = Referer
		Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
	End Function

	''' <summary>
	''' 获取屏蔽列表
	''' </summary>
	''' <returns></returns>
	Public Shared Async Function GetRoomShieldListAsync() As Task(Of HttpResponse)
		Dim url = "https://api.live.bilibili.com/banned_service/v1/shield/get_shield_info"
		m_HttpHeadersParam("Referer") = Referer
		Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
	End Function

	''' <summary>
	''' 直播间全局禁言
	''' </summary>
	''' <param name="type">类型</param>
	''' <param name="level">等级,[1,60]</param>
	''' <param name="minute">时间</param>
	''' <returns></returns>
	Public Shared Async Function RoomSilentAsync(ByVal type As SilentType, ByVal level As Integer, ByVal minute As SilentInterval) As Task(Of (Success As Boolean, Message As String, Result As String))
		' 不需要判断等级，经测试，api自带判断功能
		Return Await InternalRoomSilentAsync(type, level, minute)
	End Function

	''' <summary>
	''' 解除直播间全局禁言
	''' </summary>
	''' <returns></returns>
	Public Shared Async Function RoomUnSilentAsync() As Task(Of (Success As Boolean, Message As String, Result As String))
		Return Await InternalRoomSilentAsync(SilentType.Off, 0, SilentInterval.Always)
	End Function

	''' <summary>
	''' 直播间全局禁言
	''' </summary>
	''' <param name="type">类型</param>
	''' <param name="level">等级,[1,60]</param>
	''' <param name="minute">时间</param>
	''' <returns></returns>
	Private Shared Async Function InternalRoomSilentAsync(ByVal type As SilentType, ByVal level As Integer, ByVal minute As SilentInterval) As Task(Of (Success As Boolean, Message As String, Result As String))
		' 不需要判断等级，经测试，api自带判断功能
		Dim url = "https://api.live.bilibili.com/liveact/room_silent"
		' type的值必须为小写形式
		Dim postData = $"minute={minute.ToStringOfCulture}&room_id={m_User.ViewRoom.RealId}&type={type.ToString.ToLowerInvariant}&level={If(type = SilentType.Member, "1", level.ToStringOfCulture)}&csrf_token={m_User.Token}&csrf={m_User.Token}"

		Return Await DoApiPostAsync(url, postData, Referer)
	End Function

	''' <summary>
	''' 全局屏蔽(作用于所有直播间)
	''' </summary>
	''' <param name="type">类型</param>
	''' <param name="level">等级，除非 <paramref name="type"/>为 <see cref="ShieldType.Level"/>,否则将忽略用户自定义值，默认为1</param>
	''' <returns></returns>
	Public Shared Async Function UserAnchorShieldAsync(ByVal type As ShieldType, Optional ByVal level As Integer = 1) As Task(Of (Success As Boolean, Message As String, Result As String))
		Return Await InternalUserAnchorShieldAsync(type, ShieldOperate.On， If(type = ShieldType.Level, level, 1))
	End Function

	''' <summary>
	''' 解除全局屏蔽(作用于所有直播间)
	''' </summary>
	''' <returns></returns>
	Public Shared Async Function UserAnchorUnShieldAsync(ByVal type As ShieldType) As Task(Of (Success As Boolean, Message As String, Result As String))
		Return Await InternalUserAnchorShieldAsync(type, ShieldOperate.Off, 0)
	End Function

	''' <summary>
	''' 全局屏蔽(作用于所有直播间)
	''' </summary>
	''' <param name="type">类型</param>
	''' <param name="operate">Off或者On</param>
	''' <param name="level">等级，除非 <paramref name="type"/>为 <see cref="ShieldType.Level"/>,否则将忽略用户自定义值，默认为1</param>
	''' <returns></returns>
	Private Shared Async Function InternalUserAnchorShieldAsync(ByVal type As ShieldType, ByVal operate As ShieldOperate, Optional ByVal level As Integer = 1) As Task(Of (Success As Boolean, Message As String, Result As String))
		Dim url = "https://api.live.bilibili.com/liveact/user_silent"
		' type的值必须为小写形式
		Dim postData = $"type={type.ToString.ToLowerInvariant}&level={If(type = ShieldType.Level, level.ToStringOfCulture, operate.ToStringOfCulture)}&csrf_token={m_User.Token}&csrf={m_User.Token}"

		Return Await DoApiPostAsync(url, postData, Referer)
	End Function

	''' <summary>
	''' 添加屏蔽关键词
	''' </summary>
	''' <param name="keyword"></param>
	''' <returns></returns>
	Public Shared Async Function AddShieldKeywordAsync(ByVal keyword As String) As Task(Of (Success As Boolean, Message As String, Result As String))
		Return Await ManageShieldKeywordAsync(ShieldKeywordOperate.Add, keyword)
	End Function

	''' <summary>
	''' 移除屏蔽关键词
	''' </summary>
	''' <param name="keyword"></param>
	''' <returns></returns>
	Public Shared Async Function RemoveShieldKeywordAsync(ByVal keyword As String) As Task(Of (Success As Boolean, Message As String, Result As String))
		Return Await ManageShieldKeywordAsync(ShieldKeywordOperate.Remove, keyword)
	End Function

	''' <summary>
	''' 管理屏蔽关键词
	''' </summary>
	''' <param name="op">添加或者移除操作</param>
	''' <param name="keyword"></param>
	''' <returns></returns>
	Private Shared Async Function ManageShieldKeywordAsync(ByVal op As ShieldKeywordOperate, ByVal keyword As String) As Task(Of (Success As Boolean, Message As String, Result As String))
		Dim url = "https://api.live.bilibili.com/liveact/shield_keyword"
		Dim postData = $"roomid={m_User.ViewRoom.RealId}&type={op.ToStringOfCulture}&keyword={keyword}&csrf_token={m_User.Token}&csrf={m_User.Token}"

		Return Await DoApiPostAsync(url, postData, Referer)
	End Function

	''' <summary>
	''' 应用屏蔽关键词
	''' </summary>
	''' <param name="enabled">生效true或者失效false</param>
	''' <returns></returns>
	Public Shared Async Function ApplyShieldKeywordAsync(ByVal enabled As Boolean) As Task(Of (Success As Boolean, Message As String, Result As String))
		Dim url = "https://api.live.bilibili.com/liveact/set_room_shield"
		' type的值必须为小写形式
		Dim postData = $"roomid={m_User.ViewRoom.RealId}&type={enabled.ToString.ToLowerInvariant}&csrf_token={m_User.Token}&csrf={m_User.Token}"

		Return Await DoApiPostAsync(url, postData, Referer)
	End Function

	''' <summary>
	''' 获取屏蔽信息（关键词&amp;用户）
	''' </summary>
	''' <returns></returns>
	Public Shared Async Function GetShieldInfoAsync() As Task(Of HttpResponse)
		Dim url = "https://api.live.bilibili.com/banned_service/v1/shield/get_shield_info"
		m_HttpHeadersParam("Referer") = "https://live.bilibili.com/" & m_User.ViewRoom.RealId
		Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
	End Function

	''' <summary>
	''' 获取禁言状态以及信息
	''' </summary>
	''' <returns></returns>
	Public Shared Async Function GetSilentInfoAsync() As Task(Of HttpResponse)
		Dim url = "https://api.live.bilibili.com/banned_service/v1/silent/get_room_silent?room_id=" & m_User.ViewRoom.RealId
		m_HttpHeadersParam("Referer") = "https://live.bilibili.com/" & m_User.ViewRoom.RealId
		Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
	End Function

	''' <summary>
	''' 获取最近选择过的分区
	''' </summary>
	''' <returns></returns>
	Public Shared Async Function GetChoosedAreaAsync() As Task(Of HttpResponse)
        Dim url = "https://api.live.bilibili.com/room/v1/Area/getMyChooseArea?roomid=" & m_User.ViewRoom.RealId
        m_HttpHeadersParam("Referer") = "http://link.bilibili.com/p/center/index"
        Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
    End Function

    ''' <summary>
    ''' 获取分区表
    ''' </summary>
    ''' <returns></returns>
    Public Shared Async Function GetAreaListAsync() As Task(Of HttpResponse)
        Dim url = "https://api.live.bilibili.com/room/v1/Area/getList?show_pinyin=1"
        m_HttpHeadersParam("Referer") = "http://link.bilibili.com/p/center/index"
        Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
    End Function

    ''' <summary>
    ''' 获取Up直播间直播流信息,观众无权限
    ''' </summary>
    ''' <returns></returns>
    Public Shared Async Function GetLiveStreamAsync() As Task(Of HttpResponse)
        Dim url = "https://api.live.bilibili.com/live_stream/v1/StreamList/get_stream_by_roomId?room_id=" & m_User.ViewRoom.RealId
        m_HttpHeadersParam("Referer") = Referer
        Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
    End Function

    ''' <summary>
    ''' 获取弹幕内容
    ''' </summary>
    ''' <returns></returns>
    Public Shared Async Function GetDanmuJsonAsync() As Task(Of String)
        ' 经测试，此方法只能获取当日弹幕内容
        ' 貌似只能获取自家直播间弹幕信息？
        ' &visit_id=9dttywhzf5cs  可以没有
        Dim tcpPacketData = $"roomid={m_User.ViewRoom.RealId}&csrf_token={m_User.Token}"
        Dim postRst = Await DoApiPostAsync(GetDanmuUrl, tcpPacketData, Referer)

        ' 获取成功 {"code":0,"msg":"","data":[]}
        ' 获取失败 {"code":-400,"msg":"\u53c2\u6570\u9519\u8bef","data":[]}    
        ' \u53c2\u6570\u9519\u8bef——参数错误
        Return postRst.Result
    End Function

    ''' <summary>
    ''' 领取双端观看直播奖励
    ''' </summary>
    ''' <returns></returns>
    Public Shared Async Function ReceivedDoubleWatchAsync() As Task(Of Boolean)
        Dim url = "http://api.live.bilibili.com/activity/v1/task/receive_award"
        Dim postData = "task_id=double_watch_task&csrf_token=" & m_User.Token
        Dim referer = "http://link.bilibili.com/p/center/index"
        Dim postRst = Await DoApiPostAsync(url, postData, referer)

        Return postRst.Success
    End Function

    ''' <summary>
    ''' 获取领取双端观看直播奖励状态
    ''' </summary>
    ''' <returns>已领取返回 2，未可领取返回 0，可领取但未领取返回1，未知返回 -1</returns>
    Public Shared Async Function GetDoubleWatchStatusAsync() As Task(Of HttpResponse)
        Dim url = "http://api.live.bilibili.com/i/api/taskInfo"
        m_HttpHeadersParam("Referer") = "http://link.bilibili.com/p/center/index"
        Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
    End Function

    ''' <summary>
    ''' 签到
    ''' </summary>
    ''' <returns></returns>
    Public Shared Async Function SignAsync() As Task(Of HttpResponse)
        Dim url = "https://api.live.bilibili.com/sign/doSign"
        m_HttpHeadersParam("Referer") = "http://link.bilibili.com/p/center/index"
        Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
    End Function

    ''' <summary>
    ''' 获取签到状态
    ''' </summary>
    ''' <returns>已签到返回 1，未签到返回 0，未知返回 -1</returns>
    Public Shared Async Function GetSignInfoAsync() As Task(Of HttpResponse)
        Dim url = "https://api.live.bilibili.com/sign/GetSignInfo"
        m_HttpHeadersParam("Referer") = "http://link.bilibili.com/p/center/index"
        Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
    End Function

	''' <summary>
	''' 获取当前登录用户信息
	''' </summary>
	''' <returns></returns>
	Public Shared Async Function GetCurrentUserInfoAsync() As Task(Of HttpResponse)
		Dim url = "https://api.live.bilibili.com/live_user/v1/UserInfo/live_info"
		m_HttpHeadersParam("Referer") = "http://link.bilibili.com/p/center/index"

		Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
	End Function

	''' <summary>
	''' 获取当前登录用户资产信息
	''' </summary>
	''' <returns></returns>
	Public Shared Async Function GetCurrentUserNavAsync() As Task(Of HttpResponse)
		Dim url = "https://api.bilibili.com/x/web-interface/nav"
		m_HttpHeadersParam("Referer") = "https://www.bilibili.com/"

		Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """isLogin"":true", 3)
	End Function

	''' <summary>
	''' 获取真正的直播间Id
	''' </summary>
	''' <param name="shortRoomId">直播间短Id</param>
	''' <returns></returns>
	Public Shared Async Function GetRoomRealIdFromNetworkAsync(ByVal shortRoomId As String) As Task(Of (Success As Boolean, RoomShortId As Integer, RoomRealId As Integer))
        Dim funcRst As Boolean
        Dim shortId As Integer
        Dim realId As Integer

        Try
            Dim url = "https://api.live.bilibili.com/room/v1/Room/room_init?id=" & shortRoomId
            m_HttpHeadersParam("Referer") = Referer
            Dim getRst = Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)

            Dim json = getRst.Message
            funcRst = getRst.Success

            ' 获取失败，返回空
            If Not funcRst Then
                Return (False, -1, -1)
            Else
                ' 成功 返回具体信息
                Dim jObject = MSJsSerializer.DeserializeObject(json)
                If jObject Is Nothing Then
                    Return (False, -1, -1)
                End If

                If 0 <> CInt(jObject("code")) Then
                    Return (False, -1, -1)
                End If

                ' {"code":0,"msg":"ok","message":"ok","data":{"room_id":1133793,"short_id":443,"uid":16703361,"need_p2p":0,"is_hidden":false,"is_locked":false,"is_portrait":false,"live_status":0,"hidden_till":0,"lock_till":0,"encrypted":false,"pwd_verified":false}}
                shortId = CInt(jObject("data")("short_id"))
                realId = CInt(jObject("data")("room_id"))
            End If
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try

        Return (funcRst, shortId, realId)
    End Function

    ''' <summary>
    ''' 获取当前用户正在观看的直播间相关信息 获取到的结果会有延时
    ''' </summary>
    ''' <returns></returns>
    Public Shared Async Function GetViewRoomInfoAsync(ByVal viewRoomRealId As String) As Task(Of HttpResponse)
        Dim url = "https://api.live.bilibili.com/room/v1/Room/get_info?from=room&room_id=" & viewRoomRealId
        m_HttpHeadersParam("Referer") = "https://live.bilibili.com/" & viewRoomRealId
        Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
    End Function

    ''' <summary>
    ''' 获取当前用户相对于直播间<paramref name="roomRealId"/>的配置信息
    ''' </summary>
    ''' <param name="roomRealId"></param>
    ''' <returns></returns>
    Public Shared Async Function GetServerConfigAsync(ByVal roomRealId As String) As Task(Of String)
        Dim url = $"https://api.live.bilibili.com/room/v1/Danmu/getConf?room_id={roomRealId}&platform=pc&player=web"
        m_HttpHeadersParam("Referer") = "https://live.bilibili.com/" & roomRealId
        Dim getRst = Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)

        Return getRst.Message
    End Function

    ''' <summary>
    ''' 获取当前用户相对于直播间<paramref name="roomRealId"/>的配置信息
    ''' </summary>
    ''' <param name="roomRealId"></param>
    ''' <returns></returns>
    Public Shared Async Function GetInfoByUserAsync(ByVal roomRealId As String) As Task(Of String)
        Dim url = "https://api.live.bilibili.com/xlive/web-room/v1/index/getInfoByUser?room_id=" & roomRealId
        m_HttpHeadersParam("Referer") = "https://live.bilibili.com/" & roomRealId
        Dim getRst = Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)

        Return getRst.Message
    End Function

    ''' <summary>
    ''' 获取直播间积分信息(主播积分、SAN等)
    ''' </summary>
    ''' <returns></returns>
    Public Shared Async Function GetAnchorInRoomAsync(ByVal roomRealId As String) As Task(Of HttpResponse)
        Dim url = "https://api.live.bilibili.com/live_user/v1/UserInfo/get_anchor_in_room?roomid=" & roomRealId
        m_HttpHeadersParam("Referer") = "https://live.bilibili.com/" & roomRealId
        Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
    End Function

    ''' <summary>
    ''' 获取分区排名信息
    ''' </summary>
    ''' <param name="roomRealId"></param>
    ''' <returns></returns>
    Public Shared Async Function GetAreaRankAsync(ByVal roomRealId As String) As Task(Of HttpResponse)
        Dim url = "https://api.live.bilibili.com/rankdb/v1/Common/roomInfo?ruid=" & roomRealId
        m_HttpHeadersParam("Referer") = "https://live.bilibili.com/" & roomRealId
        Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
    End Function

    ''' <summary>
    ''' 获取直播间小时榜排名
    ''' </summary>
    ''' <returns></returns>
    Public Shared Async Function GetHourRankAsync(ByVal roomUserId As String, ByVal roomAreaId As String) As Task(Of HttpResponse)
        ' area_id=0 这个参数不知道怎么来的，貌似没有地区id是0的，地区id从1开始
        Dim url = $"https://api.live.bilibili.com/rankdb/v1/Common/masterInfo?ruid={roomUserId}&rank_name=master_realtime_hour&type=areaid_realtime_hour&best_assist_name=best_assist&type_assist=hour_best_assist&area_id=0"
        m_HttpHeadersParam("Referer") = "https://live.bilibili.com/blackboard/room-current-rank.html?anchor_uid=" & roomAreaId
        Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
    End Function

    ''' <summary>
    ''' 获取粉丝列表
    ''' </summary>
    ''' <param name="userId">用户Id</param>
    ''' <param name="pageNo">页数</param>
    ''' <param name="lastAttentionTime">最新一个粉丝关注的时间</param>
    ''' <param name="pageSize">页码</param>
    ''' <returns></returns>
    Public Shared Async Function GetFansAsync(ByVal userId As String, ByVal pageNo As Integer, ByVal lastAttentionTime As Integer, Optional ByVal pageSize As Integer = 20) As Task(Of HttpResponse)
        Dim url = $"https://api.bilibili.com/x/relation/followers?pn={pageNo.ToStringOfCulture}&ps={pageSize.ToStringOfCulture}&order=desc&vmid={userId}"
        m_HttpHeadersParam("Referer") = "https://space.bilibili.com/" & userId
        Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
    End Function

    ''' <summary>
    ''' 搜索UP主、Lv2及以上绑定手机的大部分用户
    ''' </summary>
    ''' <param name="keyword"></param>
    ''' <param name="page"></param>
    ''' <returns></returns>
    Public Shared Async Function SearchUserAsync(ByVal keyword As String, Optional ByVal page As Integer = 1) As Task(Of HttpResponse)
        Dim url = $"https://api.bilibili.com/x/web-interface/search/type?jsonp=jsonp&search_type=bili_user&highlight=1&keyword={keyword}&page={page.ToStringOfCulture}"
        m_HttpHeadersParam("Referer") = "https://search.bilibili.com/upuser"
        Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
    End Function

	''' <summary>
	''' 获取用户信息
	''' </summary>
	''' <param name="userId"></param>
	''' <returns></returns>
	Public Shared Async Function GetMemberInfoAsync(ByVal userId As String) As Task(Of HttpResponse)
		Dim url = "https://space.bilibili.com/ajax/member/GetInfo"
		Dim postData = $"mid={userId}&csrf={m_User.Token}"
		Dim referer = "https://space.bilibili.com/" & userId
		Return Await DoPostAsync(url, postData, referer)
	End Function

	''' <summary>
	''' 获取观众所在直播间的勋章信息
	''' </summary>
	''' <param name="viewerUid">观众的用户Id</param>
	''' <param name="upUid">播主的用户Id</param>
	''' <returns></returns>
	Public Shared Async Function GetMedalInfoAsync(ByVal viewerUid As String, ByVal upUid As String) As Task(Of HttpResponse)
		Dim url = $"https://api.live.bilibili.com/fans_medal/v1/fans_medal/get_fans_medal_info?source=1&uid={viewerUid}&target_id={upUid}"
		Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
	End Function

	''' <summary>
	''' 获取登录Url（暂时是用于生成二维码）
	''' </summary>
	''' <returns></returns>
	Public Shared Async Function GetLoginUrlAsync() As Task(Of HttpResponse)
		Dim url = "https://passport.bilibili.com/qrcode/getLoginUrl"
		Return Await HttpAsync.Instance.TryGetAsync(url, m_HttpHeadersParam, """code"":0", 3)
	End Function

	''' <summary>
	''' 获取登录（扫码）结果，data值含义:-2 二维码过期或者已确认授权，登录成功、 -4 未扫码、-5 已扫码未确认、。
	''' </summary>
	''' <param name="oauthKey"></param>
	''' <returns></returns>
	Public Shared Async Function GetLoginInfoAsync(ByVal oauthKey As String) As Task(Of HttpResponse)
		Dim url = "https://passport.bilibili.com/qrcode/getLoginInfo"
		Dim postData = $"oauthKey={oauthKey}&gourl=https://www.bilibili.com/"
		Dim referer = "https://passport.bilibili.com/login"
		Return Await DoPostAsync(url, postData, referer)
	End Function
#End Region
End Class
