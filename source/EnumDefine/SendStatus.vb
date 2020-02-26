Partial Class DanmuSender
    Enum SendStatus
        UnKnown
        ''' <summary>
        ''' 处理重复弹幕中
        ''' </summary>
        HandlingRepeat
        ''' <summary>
        ''' 排队中
        ''' </summary>
        Queuing
        '''' <summary>
        '''' 已出队
        '''' </summary>
        'DeQueued
        ''' <summary>
        ''' 发送中
        ''' </summary>
        Sending
        ''' <summary>
        ''' 已发送
        ''' </summary>
        Sent
    End Enum

End Class
