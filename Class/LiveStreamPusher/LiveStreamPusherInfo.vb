Public Class LiveStreamPusherInfo
    Inherits WindowBase
    ''' <summary>
    ''' 进程名
    ''' </summary>
    ''' <returns></returns>
    Public Property ProcessName As String
    ''' <summary>
    ''' 启动路径(包含文件名及后缀)
    ''' </summary>
    ''' <returns></returns>
    Public Property StartPath As String
    ''' <summary>
    ''' 推流按钮
    ''' </summary>
    ''' <returns></returns>
    Public Property ChildWindow As WindowBase
    ''' <summary>
    ''' 推流按钮的相对位置
    ''' </summary>
    ''' <returns></returns>
    Public Property OffsetLocation As Point
    ' ChildWindowFromPoint  ChildWindowFromPointEx  WindowFromPoint
End Class


