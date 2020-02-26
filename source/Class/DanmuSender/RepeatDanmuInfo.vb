Imports 姬娘插件.Events

Partial Class DanmuSender
    Private Class RepeatDanmuInfo
        Inherits DanmuBase
        ''' <summary>
        ''' 待发送Ticks
        ''' </summary>
        ''' <returns></returns>
        Public Property SendTicks As Long
        ''' <summary>
        ''' 重复次数
        ''' </summary>
        ''' <returns></returns>
        Public Property Count As Integer
        ''' <summary>
        ''' 发送状态
        ''' </summary>
        ''' <returns></returns>
        Public Property SendStatus As SendStatus
    End Class
End Class
