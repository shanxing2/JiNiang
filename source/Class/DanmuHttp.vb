Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.Script.Serialization
Imports ShanXingTech
Imports ShanXingTech.ExtensionFunc
Imports ShanXingTech.Net2
Imports ShanXingTech.Text2
Imports 姬娘


<Obsolete("因为Http方式不能获取粉丝投喂礼物等信息，所以已弃用")>
Public Class DanmuHttp
    Inherits DanmuEvents
    Implements IDisposable



#Region "属性区"


#End Region


#Region "常量区"

#End Region

#Region "字段区"
    'Private JsSerializer As JavaScriptSerializer
    Private m_StopWork As Boolean

#End Region

#Region "事件区"
    ''' <summary>
    ''' 弹幕内容改变事件 有弹幕更新时触发
    ''' </summary>
    Public Event ChatHistoryChanged As EventHandler(Of DanmuChangedEventArgs)
    ''' <summary>
    ''' 在线人数改变事件 
    ''' </summary>
    Public Event OnlineChanged As EventHandler(Of OnlineChangedEventArgs)
    ''' <summary>
    ''' 关注人数改变事件
    ''' </summary>
    Public Event AttentionCountChanged As EventHandler(Of AttentionCountChangedEventArgs)
    ''' <summary>
    ''' 粉丝增加事件
    ''' </summary>
    Public Event AttentionIncreased As EventHandler(Of AttentionIncreasedEventArgs)
    ''' <summary>
    ''' 直播状态改变事件
    ''' </summary>
    Public Event LiveStatusChanged As EventHandler(Of LiveStatusChangedEventArgs)
    ''' <summary>
    '''投喂事件 有投喂时触发
    ''' </summary>
    Public Event Feeded As EventHandler(Of DanmuChangedEventArgs)
    ''' <summary>
    ''' 老爷(年费/月费)进入直播间时触发
    ''' </summary>
    Public Event VipEntered As EventHandler(Of VipEnteredEventArgs)
#End Region

#Region "构造函数区"
    ''' <summary>
    ''' 无参数的构造函数仅用作测试 
    ''' </summary>
    Sub New()
        'JsSerializer = New JavaScriptSerializer
    End Sub

    Sub New(ByRef loginedCookies As CookieContainer)
        aaa(DanmuEntry.User.ViewRoom.Id)
    End Sub
#End Region

#Region "IDisposable Support"
    ' 要检测冗余调用
    Private disposedValue As Boolean = False

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        ' 窗体内的控件调用Close或者Dispose方法时，isDisposed2的值为True
        If disposedValue Then Return

        ' TODO: 释放托管资源(托管对象)。
        If disposing Then
            '
        End If

        ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
        ' TODO: 将大型字段设置为 null。

        disposedValue = True
    End Sub

    ' TODO: 仅当以上 Dispose(disposing As Boolean)拥有用于释放未托管资源的代码时才替代 Finalize()。
    'Protected Overrides Sub Finalize()
    '    ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic 添加此代码以正确实现可释放模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' 请勿更改此代码。将清理代码放入以上 Dispose(disposing As Boolean)中。
        Dispose(True)
        ' TODO: 如果在以上内容中替代了 Finalize()，则取消注释以下行。
        'GC.SuppressFinalize(Me)
    End Sub
#End Region

#Region "函数区"
    ''' <summary>
    ''' 无脑循环，一秒获取一次弹幕信息、人气
    ''' </summary>
    Private Sub aaa(ByVal roomId As String)
        Task.Run(action:=Async Sub()
                             While Not m_StopWork
                                 Await DanmuEntry.UpdateRoomInfoAsync(ConnectMode.Http)

                                 ' 获取弹幕
                                 Dim getRst = Await DanmuEntry.GetDanmuAsync(roomId)
                                 Dim message = getRst.Message
                                 If getRst.NeedChangeChatHistory Then
                                     RaiseEvent ChatHistoryChanged(Nothing, New DanmuChangedEventArgs(message))
                                     Await Task.Delay(1000)
                                 Else
                                     ' 不需要更新弹幕的话 直接等待下一个获取弹幕周期
                                     Await Task.Delay(1000)
                                     Continue While
                                 End If
                             End While
                         End Sub)
    End Sub

    Public Sub Close()
        m_StopWork = True
    End Sub

#End Region
End Class
