
''' <summary>
''' 配置
''' </summary>
Public Class ConfigEntity
#Region "属性区"
	'Property UserId As String
	''' <summary>
	''' 弹幕服务器连接方式
	''' </summary>
	''' <returns></returns>
	Property ConnectionMode As ConnectMode
	''' <summary>
	''' 心跳间隔
	''' </summary>
	''' <returns></returns>
	Property HeartbeatInterval As Integer
	''' <summary>
	''' 新消息提示（语音）间隔
	''' </summary>
	''' <returns></returns>
	Property BrokeSilenceInterval As Integer
	''' <summary>
	''' 自动签到
	''' </summary>
	''' <returns></returns>
	Property SignAuto As Boolean
	''' <summary>
	''' 开机感谢姬
	''' </summary>
	''' <returns></returns>
	Property EnabledThanksHime As Boolean
	''' <summary>
	''' 开机关注姬
	''' </summary>
	''' <returns></returns>
	Property EnabledAttentionHime As Boolean
	''' <summary>
	''' 开启欢迎姬
	''' </summary>
	''' <returns></returns>
	Property EnabledWelcomeHime As Boolean
	''' <summary>
	''' 接受系统小喇叭公告
	''' </summary>
	''' <returns></returns>
	Property EnabledSystemMessageHime As Boolean
	''' <summary>
	''' 自动领取双端观看直播奖励
	''' </summary>
	''' <returns></returns>
	Property ReceiveDoubleWatchAwardAuto As Boolean
	''' <summary>
	''' 当收到新消息时闪烁窗口（任务栏图标）
	''' </summary>
	''' <returns></returns>
	Property FlashWindowWhileReceiveDanmu As Boolean
	''' <summary>
	''' 观众勋章升级时提示
	''' </summary>
	''' <returns></returns>
	Property EnabledMedalUpgradeHime As Boolean
	''' <summary>
	''' 显示用过的标题个数，默认值为 0 ，表示显示全部
	''' </summary>
	''' <returns></returns>
	Property DisplayUesdTitleCount As Integer
	''' <summary>
	''' 感谢姬
	''' </summary>
	''' <returns></returns>
	Property ThanksHime As ThanksHimeEntity
	''' <summary>
	''' 主窗体
	''' </summary>
	''' <returns></returns>
	Property MainForm As MainFormEntity
	''' <summary>
	''' 弹幕格式
	''' </summary>
	''' <returns></returns>
	Property DanmuFormat As DanmuFormatEntity

#End Region

#Region "构造函数区"
	Sub New()
		Me.ConnectionMode = ConnectMode.Tcp
		Me.HeartbeatInterval = 60
		MainForm = New MainFormEntity
		ThanksHime = New ThanksHimeEntity
		DanmuFormat = New DanmuFormatEntity
	End Sub
#End Region
End Class

