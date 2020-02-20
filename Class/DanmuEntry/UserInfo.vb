Imports System.Net
Imports ShanXingTech
Imports 姬娘插件.Events

Public Class UserInfo
    Public Property Id As String
    Public Property Nick As String
	Public Property RoomShortId As Integer
	''' <summary>
	''' 直播间真实Id
	''' </summary>
	''' <returns></returns>

	Public Property RoomRealId As Integer

    Public Property Role As UserRole
    Public Property ViewRoom As LiveRoom
    Private m_Token As String
    Public ReadOnly Property Token() As String
        Get
            If Not m_Token.IsNullOrEmpty Then Return m_Token

            ' csrf_token 等于 cookies中的 bili_jct
            ' bili_jct=5211bc5dcd90e98a42c0bd35035f2673;
            Dim pattern = "bili_jct=([0-9a-z]+)"
            m_Token = Cookies?.ToKeyValuePairs.GetFirstMatchValue(pattern)

            Return m_Token
        End Get
    End Property
    Public Property Cookies As CookieContainer
    Public ReadOnly Property IsLogined() As Boolean
        Get
            Return Id IsNot Nothing
        End Get
    End Property
    Public Property SignDate As Date
    Public ReadOnly Property IsSigned As Boolean
        Get
            Return SignDate = Date.Now.Date
        End Get
    End Property

    Public Property SignRewards As String

    Public Property IsReceivedDoubleWatchAward() As Boolean
    ''' <summary>
    ''' 短id或者真实id，由用户输入或者程序自动从数据库中取的真实Id
    ''' </summary>
    ''' <returns></returns>
    Public Property ViewedRoomId As Integer
    Public Property StoreCookies As Boolean

End Class

