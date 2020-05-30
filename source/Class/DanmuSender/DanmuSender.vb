Imports System.Collections.Concurrent
Imports System.Net
Imports System.Text
Imports System.Threading
Imports ShanXingTech
Imports ShanXingTech.Net2
Imports ShanXingTech.Text2
Imports 姬娘插件.Events

Public Class DanmuSender
    Implements IDisposable
    ' 弹幕发送器
    ' 发送间隔处理：
    '   实测非重复消息间隔1秒
    ' 重复处理：
    '   间隔大于可发重复消息间隔（实测为5秒）时即发送
    '   详细请看 Res\弹幕发送处理流程.txt
    ' 以上过程设置的发送时间只是理论可发送时间，实际可发送时间还得在发送之前确定
    ' 因为上一条重复弹幕可能没有在理论发送时间发送成功，那么下一条弹幕就会受到影响，
    ' 之前设置的理论发送时间就作废了。

    ' 注：测试重复情况的时候，最好一个用户有且仅有一个工具（客户端）在发送弹幕，
    ' 因为这个弹幕发送器无法精确获取到其他工具发送弹幕的时间（其他工具发送跟此项目另外一个工具DanmuTcp接收到弹幕之前有时间差）
    ' 20180822

#Region "枚举区"
    ''' <summary>
    ''' 已经取消的循环任务标记
    ''' </summary>
    <Flags>
    Private Enum LoopTaskCanceledOptions
        None = 0
        ''' <summary>
        ''' 发送弹幕任务循环
        ''' </summary>
        Send = 1
        ''' <summary>
        ''' 弹幕发送规划任务循环
        ''' </summary>
        Schedule = 2
        ''' <summary>
        ''' 移除五秒前发送的任务循环
        ''' </summary>
        TakeBeforeFiveSecond = 4
        ''' <summary>
        ''' 规划重复弹幕发送任务循环
        ''' </summary>
        HandleRepeat = 8
        All = Send Or Schedule Or TakeBeforeFiveSecond Or HandleRepeat
    End Enum

#End Region

#Region "事件区"
    ''' <summary>
    ''' 弹幕发送完成事件
    ''' </summary>
    Public Shared Event SendCompleted As EventHandler(Of DanmuSendCompletedEventArgs)
#End Region

#Region "字段区"
    Private m_SendSpinner As SpinWait
    Private m_IsWorking As Boolean
	Private Shared s_SenderInfo As DanmuSenderInfo
	Private m_Cts As CancellationTokenSource
    Private m_Ct As CancellationToken
    ''' <summary>
    ''' 待发送的弹幕表
    ''' </summary>
    Private m_ScheduleSendBC As BlockingCollection(Of DanmuInfo)
    ''' <summary>
    ''' 发送中的弹幕表
    ''' </summary>
    Private m_SendingBC As BlockingCollection(Of DanmuInfo)
    ''' <summary>
    ''' 已经发送的弹幕表
    ''' </summary>
    Private m_SentBC As BlockingCollection(Of SentDanmuInfo)
    Private ReadOnly m_SentQueueMaxLength As Integer
    ''' <summary>
    ''' 重复的弹幕表
    ''' </summary>
    Private m_RepeatitiveBC As BlockingCollection(Of RepeatDanmuInfo)
    Private m_TrySendLoopAsyncTask As Task
    Private m_TryScheduleLoopTask As Task
    Private m_TrySendRepeatTask As Task
    Private m_TryTakeBeforFiveSecondTask As Task
    ''' <summary>
    ''' 上一次发送弹幕的Ticks
    ''' </summary>
    Private m_LastSentTicks As Long
    ''' <summary>
    ''' 重复弹幕处理方式
    ''' </summary>
    Private m_RepeatHandle As DanmuRepeatOptions
    'Private ReadOnly m_MessagesLock As Object
    ''' <summary>
    ''' 可发重复弹幕间隔
    ''' </summary>
    Private ReadOnly m_SendRepeatitiveInterval As Long
    ''' <summary>
    ''' 当前正在发送的重复弹幕（已从集合中移除准备发出）
    ''' </summary>
    Private m_DeQueuedRepeatitive As RepeatDanmuInfo
    ''' <summary>
    ''' 当前正在移除的已发送弹幕（已从集合中移除准备去掉）
    ''' </summary>
    Private m_TakingSentDanmu As SentDanmuInfo
    Private m_Referer As String
	'Private m_CanSendDanmuType As DanmuType
	Private Shared m_DanmuSendTemplete As String
	Private m_CancelTasks As LoopTaskCanceledOptions
    Private m_Initialized As Boolean
#End Region

#Region "属性区"
    ''' <summary>
    ''' 是否可发送弹幕（已登录）
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CanSend As Boolean
        Get
            Return Not s_SenderInfo.Token.IsNullOrEmpty
        End Get
    End Property
#End Region

#Region "常量区"
    ''' <summary>
    ''' 提交发送弹幕请求的链接
    ''' </summary>
    Private Const SendDanmuUrl As String = "https://api.live.bilibili.com/msg/send"
	Private Const DanmuIllegal As String = "内容非法"
	Private Const DanmuRepeat As String = "msg repeat"
	Private Const ParseError As String = "解析出错"
	' Ticks 1秒
	Public Const SendInterval As Long = 10000001
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
            If m_Cts IsNot Nothing Then
                If Not m_Cts.IsCancellationRequested Then
                    m_Cts?.Cancel()
                End If
                m_Cts.Dispose()
                m_Cts = Nothing
            End If

            m_ScheduleSendBC.TryRelease(m_TryScheduleLoopTask)
            m_RepeatitiveBC.TryRelease(m_TrySendRepeatTask)
            m_SendingBC.TryRelease(m_TrySendLoopAsyncTask)
            m_SentBC.TryRelease(m_TryTakeBeforFiveSecondTask)
        End If

        ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
        ' TODO: 将大型字段设置为 null。


        disposedValue = True
    End Sub

    '' TODO: 仅当以上 Dispose(disposing As Boolean)拥有用于释放未托管资源的代码时才替代 Finalize()。
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

#Region "构造函数区"
    Sub New()
        m_SendRepeatitiveInterval = 50000001
        m_SentQueueMaxLength = 618

        m_SendSpinner = New SpinWait

        m_Cts = New Threading.CancellationTokenSource
        m_Ct = m_Cts.Token

        m_ScheduleSendBC = New BlockingCollection(Of DanmuInfo)(m_SentQueueMaxLength)
        m_SendingBC = New BlockingCollection(Of DanmuInfo)(m_SentQueueMaxLength)
        m_SentBC = New BlockingCollection(Of SentDanmuInfo)
		m_RepeatitiveBC = New BlockingCollection(Of RepeatDanmuInfo)

		StartLoops()

        m_Initialized = True
    End Sub
#End Region

#Region "函数区"
    ''' <summary>
    ''' 确保提前初始化完成
    ''' </summary>
    Private Sub EnsurePreInit()
        If m_Initialized Then Return

        Dim cts = New CancellationTokenSource
        cts.CancelAfter(6180)
        While Not m_Initialized
            If cts.IsCancellationRequested Then
                m_Initialized = False
                Throw New TaskCanceledException("弹幕发送器提前初始化失败")
            End If
            Windows2.Delay(100)
        End While
    End Sub

    Public Sub Configure(ByRef sender As DanmuSenderInfo， ByVal danmuRepeatitiveHandle As DanmuRepeatOptions)
        EnsurePreInit()

        Debug.Print(Logger.MakeDebugString("弹幕发送器开始配置"))

        CheckSender(sender)

        m_Referer = "https://live.bilibili.com/" & sender.RoomId
        ' 配置同弹幕合并等
        s_SenderInfo = sender
        m_RepeatHandle = danmuRepeatitiveHandle
        MakeDanmuSendTemplete()

        Debug.Print(Logger.MakeDebugString("弹幕发送器配置完成"))
    End Sub

    ''' <summary>
    ''' 启动一篮子工具
    ''' </summary>
    Private Sub StartLoops()
        If Not m_IsWorking Then
            m_IsWorking = True

            m_TrySendLoopAsyncTask = Task.Run(action:=Async Sub() Await TrySendLoopAsync())
            m_TryScheduleLoopTask = Task.Run(AddressOf TryScheduleLoop)
            m_TrySendRepeatTask = Task.Run(AddressOf TrySendRepeat)
            m_TryTakeBeforFiveSecondTask = Task.Run(AddressOf TryTakeBeforFiveSecond)
        End If
    End Sub

    ''' <summary>
    ''' 确保调用者传入的数据可以初始化弹幕发送器
    ''' </summary>
    ''' <param name="sender"></param>
    Private Sub CheckSender(ByVal sender As DanmuSenderInfo)
        If sender.RoomId.IsNullOrEmpty Then
            Throw New ArgumentNullException(String.Format(My.Resources.NullReference, NameOf(sender.RoomId)))
        End If

        If sender.Token.IsNullOrEmpty Then
            Throw New ArgumentNullException(String.Format(My.Resources.NullReference, NameOf(sender.Token)))
        End If

        If sender.JoinedLiveRoomTimestamp.IsNullOrEmpty Then
            Throw New ArgumentNullException(String.Format(My.Resources.NullReference, NameOf(sender.JoinedLiveRoomTimestamp)))
        End If

        If DanmuType.None = sender.CanSendDanmuType Then
            Throw New ArgumentNullException(String.Format(My.Resources.NullReference, NameOf(sender.CanSendDanmuType)))
        End If
    End Sub

	''' <summary>
	''' 把需要发送的弹幕信息添加进弹幕发送器以备发送(不是马上发出，程序内部会选择一个合适的时间，尽快安排发出)，发送结果可在 <see cref="SendCompleted"/> 事件中查询
	''' </summary>
	''' <param name="danmu"></param>
	Public Sub Add(ByRef danmu As DanmuInfo)
        m_ScheduleSendBC.Add(danmu)
    End Sub

    ''' <summary>
    ''' 发送弹幕
    ''' </summary>
    Private Async Function TrySendLoopAsync() As Task
        Try
            Debug.Print(Logger.MakeDebugString("弹幕发送器启动"))

            For Each danmu In m_SendingBC.GetConsumingEnumerable(m_Ct)
                If m_Ct.IsCancellationRequested Then Exit For

                If danmu.Source = DanmuSource.Internal AndAlso
                   Not CheckAllow(danmu.Type) Then
                    Debug.Print(Logger.MakeDebugString("已过滤，无需发出 " & danmu.Context))
                    Continue For
                End If

                Debug.Print(Logger.MakeDebugString($"发送弹幕ing:{danmu.Context}  计数:{danmu.Count.ToStringOfCulture}"))
                Await SendAsync(danmu)
                Debug.Print(Logger.MakeDebugString($"发送弹幕ed:{danmu.Context}  计数:{danmu.Count.ToStringOfCulture}"))

                If m_Ct.IsCancellationRequested Then Exit For
            Next
        Catch ex As OperationCanceledException
            Debug.Print(Logger.MakeDebugString("弹幕发送器取消任务"))
        Catch ex As ObjectDisposedException
            Debug.Print(Logger.MakeDebugString("弹幕发送器取消任务"))
        Catch ex As Exception
            Logger.WriteLine(ex)
        Finally
            Debug.Print(Logger.MakeDebugString("弹幕发送器关闭"))
        End Try
    End Function

    ''' <summary>
    ''' 安排待发送弹幕
    ''' </summary>
    Private Sub TryScheduleLoop()
        Try
            Debug.Print(Logger.MakeDebugString("弹幕规划器启动"))

            For Each danmu In m_ScheduleSendBC.GetConsumingEnumerable(m_Ct)
                If m_Ct.IsCancellationRequested Then Exit For

                If danmu.Source = DanmuSource.Internal AndAlso
                    Not CheckAllow(danmu.Type) Then
                    Debug.Print(Logger.MakeDebugString("已过滤，无需发出 " & danmu.Context))
                    Continue For
                End If

                Debug.Print(Logger.MakeDebugString($"需要发送弹幕:{danmu.Context}  计数:{danmu.Count.ToStringOfCulture}"))

                ' 处理重复
                EnsureHandledRepeat(m_ScheduleSendBC, danmu)

                Debug.Print(Logger.MakeDebugString("处理重复完毕，有重复 = " & CStr（danmu Is Nothing）))

                ' 到此处为非Nothing说明不存在重复，需要安排发出
                If danmu IsNot Nothing AndAlso
                    Not m_SendingBC.IsAddingCompleted Then
                    m_SendingBC.Add(danmu)
                End If

                If m_Ct.IsCancellationRequested Then Exit For
            Next
        Catch ex As OperationCanceledException
            Debug.Print(Logger.MakeDebugString("弹幕规划器取消任务"))
        Catch ex As ObjectDisposedException
            Debug.Print(Logger.MakeDebugString("弹幕规划器取消任务"))
        Catch ex As Exception
            Logger.WriteLine(ex)
        Finally
            Debug.Print(Logger.MakeDebugString("弹幕规划器关闭"))
        End Try
    End Sub

    ''' <summary>
    ''' 尝试从已发弹幕表中移除5秒钟之前发的弹幕
    ''' </summary>
    Private Sub TryTakeBeforFiveSecond()
        Try
            Debug.Print(Logger.MakeDebugString("已发弹幕回收器启动"))
            ' 去除5秒之前发送的弹幕
            For Each m_TakingSentDanmu In m_SentBC.GetConsumingEnumerable(m_Ct)
                If m_Ct.IsCancellationRequested Then Exit For

                While m_TakingSentDanmu IsNot Nothing
                    If m_Ct.IsCancellationRequested Then Return
                    ' 去除5秒之前发送的弹幕
                    If Date.Now.Ticks - m_TakingSentDanmu.SentTicks > m_SendRepeatitiveInterval Then
                        Debug.Print(Logger.MakeDebugString("移除了五秒前发送的弹幕：" & m_TakingSentDanmu.Context))
                        m_TakingSentDanmu = Nothing
                    End If
                    ' 20%cpu占用降低到3%左右
                    Windows2.Delay(618)
                End While

                If m_Ct.IsCancellationRequested Then Exit For
            Next
        Catch ex As OperationCanceledException
            Debug.Print(Logger.MakeDebugString("已发弹幕回收器取消任务"))
        Catch ex As ObjectDisposedException
            Debug.Print(Logger.MakeDebugString("已发弹幕回收器取消任务"))
        Catch ex As Exception
            Logger.WriteLine(ex)
        Finally
            Debug.Print(Logger.MakeDebugString("已发弹幕回收器关闭"))
        End Try
    End Sub

    ''' <summary>
    ''' 尝试从重复弹幕表中发弹幕
    ''' </summary>
    Private Sub TrySendRepeat()
        Try
            Debug.Print(Logger.MakeDebugString("重复弹幕发送器启动"))

            Dim spinner As New SpinWait
            For Each rptDanmu In m_RepeatitiveBC.GetConsumingEnumerable(m_Ct)
                If m_Ct.IsCancellationRequested Then Exit For

                Debug.Print(Logger.MakeDebugString("发送重复弹幕 " & rptDanmu.Context & " 计数： " & rptDanmu.Count.ToStringOfCulture))
                If rptDanmu.Source = DanmuSource.Internal AndAlso
                   Not CheckAllow(rptDanmu.Type) Then
                    Logger.WriteLine("已过滤，无需发出 " & rptDanmu.Context)
                    Continue For
                End If

                m_DeQueuedRepeatitive = rptDanmu

                EnsureSetSendTicks(spinner, rptDanmu)

                ' 等待直到下个可发重复弹幕时间点
                WaitUntilNextSendRepeatTicks(spinner, rptDanmu)

                m_DeQueuedRepeatitive = Nothing

                rptDanmu.SendStatus = SendStatus.Sending
                AddRepeatToSendingBc(rptDanmu)
                rptDanmu.SendStatus = SendStatus.Sent

                If m_Ct.IsCancellationRequested Then Exit For
            Next
        Catch ex As OperationCanceledException
            Debug.Print(Logger.MakeDebugString("重复弹幕发送器取消任务"))
        Catch ex As ObjectDisposedException
            Debug.Print(Logger.MakeDebugString("重复弹幕发送器取消任务"))
        Catch ex As Exception
            Logger.WriteLine(ex)
        Finally
            Debug.Print(Logger.MakeDebugString("重复弹幕发送器关闭"))
        End Try
    End Sub

    ''' <summary>
    ''' 确保已经设置发送时间
    ''' </summary>
    ''' <param name="spinner"></param>
    ''' <param name="rptDanmu"></param>
    Private Sub EnsureSetSendTicks(ByVal spinner As SpinWait, ByRef rptDanmu As RepeatDanmuInfo)
        ' 等待待发送列表中的相同弹幕发出，然后进入到已发送列表，再去获取发送时间
        ' 只有一种情况会需要在这里设置发送时间：发送时间为 -1
        Dim preTicks = Date.Now.Ticks
        spinner.Reset()
        While rptDanmu.SendTicks = -1
            Dim existsSentRptDanmu = ExistsRepeatSent(rptDanmu.Context)
            If existsSentRptDanmu.Yes Then
                rptDanmu.SendTicks = existsSentRptDanmu.SentDanmu.SentTicks + m_SendRepeatitiveInterval
                Exit While
            End If

            ' 如果正在发送的弹幕表中不存在跟已经出队的弹幕相同的，那就可以马上发出
            Dim existsSendingRptDanmu = ExistsRepeatSending(rptDanmu.Context)
            If Not existsSendingRptDanmu Then
                rptDanmu.SendTicks = Date.Now.Ticks
                Exit While
            End If

            ' 或者是等待 5 秒之后也发出
            If Date.Now.Ticks - preTicks >= m_SendRepeatitiveInterval Then
                rptDanmu.SendTicks = Date.Now.Ticks
                Exit While
            End If

            If m_Ct.IsCancellationRequested Then Exit While
            spinner.SpinOnce()
        End While
    End Sub

    ''' <summary>
    ''' 等待直到下个可发重复弹幕时间点
    ''' </summary>
    ''' <param name="spinner"></param>
    ''' <param name="rptDanmu"></param>
    Private Sub WaitUntilNextSendRepeatTicks(ByVal spinner As SpinWait, ByRef rptDanmu As RepeatDanmuInfo)
        spinner.Reset()
        While Date.Now.Ticks - rptDanmu.SendTicks <= 0 OrElse
            rptDanmu.SendStatus = SendStatus.HandlingRepeat
            If m_Ct.IsCancellationRequested Then Return
            spinner.SpinOnce()
        End While
    End Sub

    Private Function CheckAllow(ByVal type As DanmuType) As Boolean
        Return (type And s_SenderInfo.CanSendDanmuType) = type
    End Function

    ''' <summary>
    ''' 添加重复弹幕到待发送列表
    ''' </summary>
    ''' <param name="rptDanmu"></param>
    Private Sub AddRepeatToSendingBc(ByVal rptDanmu As RepeatDanmuInfo)
        If rptDanmu.Count >= 1 Then
            Dim danmuContextBuilder = StringBuilderCache.Acquire(s_SenderInfo.DanmuMaxLength + 20)
            danmuContextBuilder.Append(rptDanmu.Context).Append(" X").Append(rptDanmu.Count)
            Dim danmu = New DanmuInfo With {
                .Context = StringBuilderCache.GetStringAndReleaseBuilder(danmuContextBuilder),
                .Source = rptDanmu.Source
            }
            m_SendingBC.Add(danmu)
        Else
            Dim danmu = New DanmuInfo With {
                .Context = rptDanmu.Context,
                .Source = rptDanmu.Source
            }
            m_SendingBC.Add(danmu)
        End If
    End Sub

    ''' <summary>
    ''' 确保已经处理所有重复弹幕。
    ''' 当处理方式为 Block 时，取得一条重复弹幕就加一条进 m_RepeatitiveDanmuQueues 表；
    ''' 当处理方式为 Merge 时，取得一条重复弹幕就统计重复次数，直到下次取得的不是重复盗墓，然后重新生成合并弹幕并加进 m_RepeatitiveDanmuQueues 表；
    ''' </summary>
    ''' <param name="scheduleSendBC"></param>
    ''' <param name="danmu"></param>
    Private Sub EnsureHandledRepeat(ByRef scheduleSendBC As BlockingCollection(Of DanmuInfo), ByRef danmu As DanmuInfo)
        If m_RepeatHandle = DanmuRepeatOptions.Block Then
            TryHandleRepeatByBlock(scheduleSendBC, danmu)
        ElseIf m_RepeatHandle = DanmuRepeatOptions.Merge Then
            If danmu.Source = DanmuSource.Input Then
                TryHandleRepeatInputByMerge(danmu)
            Else
                TryHandleRepeatInternalByMerge(scheduleSendBC, danmu)
            End If
        End If
    End Sub

    ''' <summary>
    ''' 阻塞重复弹幕(如果有)
    ''' </summary>
    ''' <param name="scheduleSendBC"></param>
    ''' <param name="danmu"></param>
    Private Sub TryHandleRepeatByBlock(ByRef scheduleSendBC As BlockingCollection(Of DanmuInfo), ByRef danmu As DanmuInfo)
        ' 需确保按照顺序检查 待发送列表 已发送列表 已出队重复弹幕 重复列表
        Dim existsRptQueued = Me.ExistsRepeatQueued(danmu.Context)
        Dim existsRptSending = Me.ExistsRepeatSending(danmu.Context)
        Dim existsRptSent = Me.ExistsRepeatSent(danmu.Context)
        Dim existsRptQueuing = Me.ExistsRepeatQueuing(danmu.Context)
        If Not existsRptQueued.Yes AndAlso
            Not existsRptSending AndAlso
            Not existsRptSent.Yes AndAlso
            Not existsRptQueuing.Yes Then
            Return
        End If

        Do
            ' 阻塞模式不需要设置发送时间，让处理发送重复弹幕的过程根据上次重复弹幕来设置
            Dim rptDanmu = New RepeatDanmuInfo With {
                .Context = danmu.Context,
                .Source = danmu.Source,
                .SendTicks = -1,
                .Count = 0,
                .SendStatus = SendStatus.Queuing
            }

            AddToRepeatitiveBC(rptDanmu)

            ' 处理重复弹幕之后继续检查后面的弹幕有没有跟之前的弹幕重复
            If scheduleSendBC.TryTake(danmu, 1000, m_Ct) Then
                Debug.Print(Logger.MakeDebugString($"需要发送弹幕:{danmu.Context}  计数:{danmu.Count.ToStringOfCulture}"))

                ' 重复列表或者是已发送列表中一定能检查出（如果有重复弹幕），所以不再需要检查在发送列表
                existsRptQueued = Me.ExistsRepeatQueued(danmu.Context)
                existsRptSent = Me.ExistsRepeatSent(danmu.Context)
                existsRptQueuing = Me.ExistsRepeatQueuing(danmu.Context)
            Else
                Exit Do
            End If
        Loop While existsRptQueued.Yes OrElse
            existsRptSent.Yes OrElse
            existsRptQueuing.Yes
    End Sub

    ''' <summary>
    ''' 合并软件内部发送的重复弹幕(如果有)
    ''' </summary>
    ''' <param name="scheduleSendBC"></param>
    ''' <param name="danmu"></param>
    Private Sub TryHandleRepeatInternalByMerge(ByRef scheduleSendBC As BlockingCollection(Of DanmuInfo), ByRef danmu As DanmuInfo)
        Dim firstDanmuRepeat = True
        Dim firstDanmuCopy As New DanmuInfo With {
            .Context = danmu.Context,
            .Source = danmu.Source,
            .Type = danmu.Type,
            .Count = danmu.Count,
            .Unit = danmu.Unit
        }

        ' 需确保按照顺序检查 待发送列表 已发送列表 已出队重复弹幕 重复列表
        Dim existsRptQueued = ExistsRepeatQueued(danmu.Context)
        Dim existsRptSending = ExistsRepeatSending(danmu.Context)
        Dim existsRptSent = ExistsRepeatSent(danmu.Context)
        Dim existsRptQueuing = ExistsRepeatQueuing(danmu.Context)
        If Not existsRptQueued.Yes AndAlso
            Not existsRptSending AndAlso
            Not existsRptSent.Yes AndAlso
            Not existsRptQueuing.Yes Then
            firstDanmuRepeat = False
        End If

        ' 如果待发送的重复弹幕列表中有，那就把时间设置为 Date.Now.AddSeconds(1).Ticks(+1秒是为了尽量合并更多重复信息，减少通讯)
        ' 如果已发送的弹幕列表中有，那暂时把时间设置为 + m_SendRepeatitiveInterval，等待检查完之后，再最终确认发送时间（计数大于1可以马上发出）
        If existsRptQueuing.Yes Then
            existsRptQueuing.Danmu.SendStatus = SendStatus.HandlingRepeat
        End If

        Dim tempRptDanmu As New RepeatDanmuInfo With {
            .Context = danmu.Context,
            .Source = danmu.Source,
            .SendTicks = -1,
            .Count = 0,
            .SendStatus = SendStatus.Queuing
        }
        Dim repeatIntervalTicks = Date.Now.Ticks
        Dim existsRptUnScheduled As Boolean
        ' 尽可能处理重复弹幕
        Do
            If existsRptQueuing.Yes Then
                If existsRptQueuing.Danmu.SendStatus <= SendStatus.Queuing Then
                    existsRptQueuing.Danmu.SendStatus = SendStatus.HandlingRepeat
                    Interlocked.Exchange(existsRptQueuing.Danmu.Count, existsRptQueuing.Danmu.Count + danmu.Count)
                ElseIf existsRptQueuing.Danmu.SendStatus > SendStatus.Queuing Then
                    tempRptDanmu.Count += danmu.Count
                End If
                existsRptQueuing = (False, Nothing)
            ElseIf existsRptSending OrElse existsRptQueued.Yes Then
                tempRptDanmu.Count += danmu.Count
                existsRptSending = False
                existsRptQueued = (False, 0)
            ElseIf existsRptSent.Yes Then
                tempRptDanmu.Count += danmu.Count
                tempRptDanmu.SendTicks = existsRptSent.SentDanmu.SentTicks + m_SendRepeatitiveInterval
                existsRptSent = (False, Nothing)
            ElseIf existsRptUnScheduled Then
                tempRptDanmu.Count += danmu.Count
                existsRptUnScheduled = False
            End If

            Try
                ' 尝试处理 m_SendRepeatitiveInterval 之内的重复弹幕
                While Date.Now.Ticks - repeatIntervalTicks <= m_SendRepeatitiveInterval
                    ' 处理重复弹幕之后继续检查后面的弹幕有没有跟之前的弹幕重复
                    If scheduleSendBC.TryTake(danmu, 618, m_Ct) Then
                        Debug.Print(Logger.MakeDebugString($"需要发送弹幕:{danmu.Context}  计数:{danmu.Count.ToStringOfCulture}"))

                        existsRptUnScheduled = (tempRptDanmu.Context = danmu.Context)
                        If existsRptUnScheduled Then
                            repeatIntervalTicks = Date.Now.Ticks
                            Exit Try
                        End If

                        If existsRptQueuing.Yes Then
                            existsRptQueuing.Danmu.SendStatus = SendStatus.Queuing
                        End If

                        existsRptQueued = ExistsRepeatQueued(danmu.Context)
                        existsRptSending = ExistsRepeatSending(danmu.Context)
                        existsRptSent = ExistsRepeatSent(danmu.Context)
                        existsRptQueuing = ExistsRepeatQueuing(danmu.Context)

                        ' 尽快退出 m_SendRepeatitiveInterval 间隔的循环，以保证有不同于上一条弹幕的弹幕时，能及时响应
                        Exit Try
                    End If
                End While

                repeatIntervalTicks = Date.Now.Ticks
            Catch ex As TaskCanceledException
                '
            Catch ex As Exception
                Logger.WriteLine(ex)
            End Try
        Loop While existsRptUnScheduled OrElse
            existsRptQueued.Yes OrElse
            existsRptSending OrElse
            existsRptSent.Yes OrElse
            existsRptQueuing.Yes

        ' 如果传进来的第一条弹幕跟后面的重复，就增加计数
        If Not firstDanmuRepeat AndAlso
            firstDanmuCopy.Context = tempRptDanmu.Context AndAlso
            tempRptDanmu.Count > 0 Then
            tempRptDanmu.Count += Math.Max(firstDanmuCopy.Count, 1)
        ElseIf danmu Is Nothing Then
            ' 如果传进来的第一条弹幕没有重复的话，就传出去准备发送
            danmu = firstDanmuCopy
        End If

        If tempRptDanmu.Count = 0 Then
            If existsRptQueuing.Yes AndAlso
                existsRptQueuing.Danmu.SendStatus <= SendStatus.HandlingRepeat Then
                Debug.Print(Logger.MakeDebugString($"重复列表存在同样信息，增加计数，无需重复加入:{tempRptDanmu.Context}    时间：{tempRptDanmu.SendTicks.ToStringOfCulture}"))
                existsRptQueuing.Danmu.SendTicks = Date.Now.Ticks
                existsRptQueuing.Danmu.SendStatus = SendStatus.Queuing
            End If
        ElseIf tempRptDanmu.Count = 1 Then
            ' 只有一次的话 就当做是普通重复弹幕，下个5秒发送出去
            AddToRepeatitiveBC(tempRptDanmu)
        ElseIf tempRptDanmu.Count > 1 Then
            ' 重复次数大于1， 那么把上次发送时间修改为 Date.Now.Ticks
            ' 因为会在重复消息前面附加重复次数统计信息，所以马上就阔以发送出去
            tempRptDanmu.SendTicks = Date.Now.Ticks
            AddToRepeatitiveBC(tempRptDanmu)
        End If
    End Sub

    ''' <summary>
    ''' 合并用户输入的重复弹幕(如果有)
    ''' </summary>
    ''' <param name="danmu"></param>
    Private Sub TryHandleRepeatInputByMerge(ByRef danmu As DanmuInfo)
        ' 需确保按照顺序检查 待发送列表 已发送列表 已出队重复弹幕 重复列表
        Dim existsRptQueued = ExistsRepeatQueued(danmu.Context)
        Dim existsRptSending = ExistsRepeatSending(danmu.Context)
        Dim existsRptSent = ExistsRepeatSent(danmu.Context)
        Dim existsRptQueuing = ExistsRepeatQueuing(danmu.Context)

        Dim tempRptDanmu = New RepeatDanmuInfo With {
            .Context = danmu.Context,
            .Source = danmu.Source,
            .SendTicks = -1,
            .Count = 0,
            .SendStatus = SendStatus.Queuing
        }

        If existsRptQueuing.Yes Then
            danmu = Nothing

            If existsRptQueuing.Danmu.SendStatus <= SendStatus.Queuing Then
                existsRptQueuing.Danmu.SendStatus = SendStatus.HandlingRepeat
                Interlocked.Exchange(existsRptQueuing.Danmu.Count, existsRptQueuing.Danmu.Count + 1)
            Else
                tempRptDanmu.Count += 1
            End If
        ElseIf existsRptSending OrElse existsRptQueued.Yes Then
            danmu = Nothing
            tempRptDanmu.Count += 1
        ElseIf existsRptSent.Yes Then
            danmu = Nothing
            tempRptDanmu.SendTicks = existsRptSent.SentDanmu.SentTicks + m_SendRepeatitiveInterval
        Else
            ' 没有重复的
            Return
        End If

        If tempRptDanmu.Count = 0 AndAlso tempRptDanmu.SendTicks > -1 Then
            AddToRepeatitiveBC(tempRptDanmu)
        ElseIf tempRptDanmu.Count = 1 Then
            ' 只有一次的话 就当做是普通重复弹幕，下个5秒发送出去
            AddToRepeatitiveBC(tempRptDanmu)
        ElseIf tempRptDanmu.Count > 1 Then
            ' 重复次数大于1， 那么把上次发送时间修改为 Date.Now.Ticks
            ' 因为会在重复消息前面附加重复次数统计信息，所以马上就阔以发送出去
            tempRptDanmu.SendTicks = Date.Now.Ticks
            AddToRepeatitiveBC(tempRptDanmu)
        ElseIf existsRptQueuing.Danmu.Count > 0 Then
            existsRptQueuing.Danmu.SendStatus = SendStatus.Queuing
            Debug.Print(Logger.MakeDebugString("合并到出队中的重复弹幕"))
        Else
            MessageBox.Show("提示内容", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ''' <summary>
    ''' 重复弹幕列表中存在重复弹幕
    ''' </summary>
    ''' <param name="context"></param>
    ''' <returns></returns>
    Private Function ExistsRepeatQueuing(ByVal context As String) As (Yes As Boolean， Danmu As RepeatDanmuInfo)
        If m_RepeatitiveBC.Count = 0 Then
            Return （False， Nothing）
        End If

        Dim rptDanmu = m_RepeatitiveBC.AsParallel.FirstOrDefault(Function(r) r.SendStatus < SendStatus.Sending AndAlso r.Context = context)
        Return (rptDanmu IsNot Nothing, rptDanmu)
    End Function

    ''' <summary>
    ''' 正在处理的弹幕跟已经出队的重复弹幕重复
    ''' </summary>
    ''' <param name="context"></param>
    ''' <returns></returns>
    Private Function ExistsRepeatQueued(ByVal context As String) As (Yes As Boolean， SendTicks As Long)
        Return If(m_DeQueuedRepeatitive?.Context = context,
            （True， m_DeQueuedRepeatitive.SendTicks）,
            （False， 0）)
    End Function

    ''' <summary>
    ''' 发送中列表中存在重复弹幕
    ''' </summary>
    ''' <param name="context"></param>
    ''' <returns></returns>
    Private Function ExistsRepeatSending(ByVal context As String) As Boolean
        If m_SendingBC.Count = 0 Then
            Return False
        End If

        Dim sendingRepeatitiveDanmu = m_SendingBC.AsParallel.FirstOrDefault(Function(r) r.Context = context)
        Return sendingRepeatitiveDanmu IsNot Nothing
    End Function

    ''' <summary>
    ''' 已发送弹幕列表中存在重复弹幕
    ''' </summary>
    ''' <param name="context"></param>
    ''' <returns></returns>
    Private Function ExistsRepeatSent(ByVal context As String) As (Yes As Boolean， SentDanmu As SentDanmuInfo)
        If m_SentBC.Count = 0 AndAlso m_TakingSentDanmu Is Nothing Then
            Return （False， Nothing）
        End If

        If m_TakingSentDanmu?.Context = context Then
            Return (True, m_TakingSentDanmu)
        End If

        Dim sentRepeatitiveDanmu = m_SentBC.AsParallel.FirstOrDefault(Function(r) r.Context = context)
        Return (sentRepeatitiveDanmu IsNot Nothing, sentRepeatitiveDanmu)
    End Function

    ''' <summary>
    ''' 把重复弹幕添加到重复弹幕表中
    ''' </summary>
    ''' <param name="rptDanmu"></param>
    Private Sub AddToRepeatitiveBC(ByRef rptDanmu As RepeatDanmuInfo)
        Debug.Print(Logger.MakeDebugString($"加入重复列表:{rptDanmu.Context}    重复次数:{rptDanmu.Count.ToString} 时间：{rptDanmu.SendTicks.ToStringOfCulture}   {If(rptDanmu.SendTicks <> -1, New Date(rptDanmu.SendTicks).ToLongTimeString, "-1")}"))

        m_RepeatitiveBC.Add(rptDanmu)
        rptDanmu.SendStatus = SendStatus.Queuing

        ' 弹幕反馈给发送者
        ReportSendRepeatitiveAction(rptDanmu)
    End Sub

    ''' <summary>
    ''' 直播间发送弹幕
    ''' </summary>
    ''' <param name="danmu">弹幕内容，无需编码</param>
    ''' <returns></returns>
    Private Async Function SendAsync(ByVal danmu As DanmuInfo) As Task
        Dim danmuContext = BuildDanmuContext(danmu)

        ' 超过长度的话 分多条发出（一个字母或者一个汉字，官方都当做是一个字符）
        If danmuContext.Length > s_SenderInfo.DanmuMaxLength Then
            Await SendOverLengthAsync(danmuContext, danmu.Source)
        Else
            Dim sendRst = Await InternalSendAsync(danmuContext)
            Dim sendDanmu As New SentDanmuInfo With {
                .Context = danmuContext,
                .Source = danmu.Source,
                .SentTicks = m_LastSentTicks
            }
			ReportSendCompletedAction((sendDanmu, sendRst.Success, sendRst.IsAllow, sendRst.Message))

			' 记录log以给程序猿分析
			If Not sendRst.Success Then
                Logger.WriteLine($"{danmuContext}，失败原因 → '{sendRst.Message}'，直播间Id:{s_SenderInfo.RoomId}")
            End If
		End If
	End Function

    ''' <summary>
    ''' 生成要发送的弹幕
    ''' </summary>
    ''' <param name="danmu"></param>
    ''' <returns></returns>
    Private Function BuildDanmuContext(ByVal danmu As DanmuInfo) As String
        Return If(danmu.Count > 0,
            String.Concat(danmu.Context, " X", danmu.Count.ToStringOfCulture),
            danmu.Context)
    End Function

    ''' <summary>
    ''' 发送超长弹幕
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="source"></param>
    ''' <returns></returns>
    Private Async Function SendOverLengthAsync(ByVal context As String, ByVal source As DanmuSource) As Task
		' 已经发送的弹幕长度
		Dim sentLength As Integer
		Dim subLength As Integer

		' 分割超长弹幕，直到所有弹幕发出或者是失败
		Do
			If (s_SenderInfo.DanmuMaxLength + sentLength) > context.Length Then
				' 不足取余下所有
				subLength = (context.Length - sentLength)
			Else
				subLength = s_SenderInfo.DanmuMaxLength
			End If

			Dim subDanmu = context.Substring(sentLength, subLength)
			Dim sendRst = Await InternalSendAsync(subDanmu)
            Dim sentSubDanmu As New SentDanmuInfo With {
                .Context = subDanmu,
                .Source = source,
                .SentTicks = m_LastSentTicks
            }
            ' 暂时不清楚是B站bug，还是本程序有问题，个别同样的内容，20长度，有时候（第一次）发会提示 ’max limit‘,第二次再发就成功了 20200221
            If sendRst.Success Then
				sentLength += s_SenderInfo.DanmuMaxLength
				ReportSendCompletedAction((sentSubDanmu, True， True, String.Empty))
			Else
				' 发送失败的话就获取发送结果，直接返回,不再继续发送剩余弹幕
				' 如有某一部分发送失败，应该报告整条弹幕，呈现完整的失败弹幕给用户看
				sentSubDanmu.Context = context
				ReportSendCompletedAction((sentSubDanmu, sendRst.Success, sendRst.IsAllow, sendRst.Message))

                ' 记录log以给程序猿分析
                Logger.WriteLine($"整串：【{context}】 子串：【{subDanmu}】，失败原因 → '{sendRst.Message}'，直播间Id:{s_SenderInfo.RoomId}")
                Exit Do
            End If
        Loop Until sentLength > context.Length
    End Function


    ''' <summary>
    ''' 发送弹幕到直播间
    ''' </summary>
    ''' <param name="danmu">弹幕内容，无需编码</param>
    ''' <returns></returns>
    Private Async Function InternalSendAsync(ByVal danmu As String) As Task(Of (Success As Boolean, IsAllow As Boolean, Message As String))
        If m_Ct.IsCancellationRequested Then
            Return (False, False, "取消")
        End If

		' 注意事项
		' 1.发送间隔大于1秒
		' 2.相同弹幕内容发送间隔大于5秒

		Dim message As String = String.Empty
		Dim funcRst As Boolean
        Dim isAllow As Boolean
        Dim tempLastSentTicks As Long

        Try
            ' rnd据说这个是进入首次进入直播间的时间
            Dim danmuInput = String.Format(m_DanmuSendTemplete, danmu)

			EnsureArriveNextSendTicks(danmu)

			If m_Ct.IsCancellationRequested Then
				Return (False, False, "取消")
			End If

			Dim postRst = Await BilibiliApi.DoApiPostAsync(SendDanmuUrl, danmuInput, m_Referer)
			tempLastSentTicks = Date.Now.Ticks

			Dim parseRst = ParseSendResponse(postRst.Message, danmu)
			funcRst = parseRst.Success
			isAllow = parseRst.IsAllow
			message = parseRst.Message
		Catch ex As Exception
			message = ex.Message & "；" & danmu
		Finally
			' 在 Finally 块处理 m_LastSentTicks 以确保每次发送弹幕之后都能设置
			If tempLastSentTicks = 0 Then
				Interlocked.Exchange(m_LastSentTicks， Date.Now.Ticks)
			Else
				Interlocked.Exchange(m_LastSentTicks， tempLastSentTicks)
			End If
		End Try

		Return (funcRst, isAllow, message)
	End Function

	''' <summary>
	''' 解析发送弹幕返回结果
	''' </summary>
	''' <param name="json"></param>
	''' <param name="danmu"></param>
	''' <returns></returns>
	Private Function ParseSendResponse(ByVal json As String, ByRef danmu As String) As (Success As Boolean, IsAllow As Boolean, Message As String)
		' 发送成功 {"code":0,"msg":"","data":[]} 新版20181220 {"code":0,"data":[],"message":"","msg":""}
		' 发送成功也有可能返回为空20190816
		' {"code":0,"msg":"\u5185\u5bb9\u975e\u6cd5","data":[]} \u5185\u5bb9\u975e\u6cd5 为 内容非法
		' 比如弹幕内容带有 'http' 时 发送不了
		' {"code":0,"msg":"msg in 1s","data":[]}
		' {"code":0,"msg":"k","data":[]}  弹幕中有直播间设置的屏蔽字，会返回这个信息
		' 新版20181220 {"code":-111,"message":"csrf 校验失败","ttl":1}
		' 20190902貌似B站接口行为改了，发送失败（重复、一秒内等等）会直接返回单个msg，而不是完整的json,发送成功的话返回String.Empty
		If json.IsNullOrEmpty Then Return (True, True, String.Empty)
		' 内容非法、重复、一秒内、k
		If json.Length = 1 OrElse
			(json.Chars(0) <> "{"c AndAlso json.Chars(json.Length - 1) <> "}"c) Then
			Return (False, False, json)
		End If

		Dim isAllow As Boolean
		Dim message = ParseError
		Dim funcRst As Boolean

		Try
			Dim root = MSJsSerializer.Deserialize(Of APIPostResponseBaseEntity.Root)(json)
			If root Is Nothing Then Return (False, True, ParseError)

			message = If(root.message.IsNullOrEmpty, root.msg, root.message)
			funcRst = (0 = root.code AndAlso message.Length = 0)

			' 如果 message 为 内容非法（被河蟹了） 也算发送失败
			If message.Length > 0 AndAlso DanmuIllegal = message Then
				message = message & "：" & danmu
			Else
				isAllow = True
			End If
		Catch ex As Exception
			Logger.WriteLine(ex)
		End Try

		Return (funcRst, isAllow, message)
	End Function

	''' <summary>
	''' 发送之前再次确认这个时间点可以发送（确保重复弹幕在可发送时间间隔之后发送）
	''' </summary>
	''' <param name="danmu"></param>
	Private Sub EnsureArriveNextSendTicks(ByVal danmu As String)
		m_SendSpinner.Reset()
		' 需要保证两次发送的间隔大于1秒，不然会因为被限制而发送失败
		While Date.Now.Ticks - m_LastSentTicks <= SendInterval
			m_SendSpinner.SpinOnce()
		End While

		Dim exists = ExistsRepeatSent(danmu)
		If exists.Yes Then
			WaitUntilNextSendRepeatTicks(exists.SentDanmu.SentTicks)
		End If
	End Sub

	''' <summary>
	''' 等待下个可发送重复弹幕时间点
	''' </summary>
	Private Sub WaitUntilNextSendRepeatTicks(ByVal lastSentTicks As Long)
        Try
            While Date.Now.Ticks - lastSentTicks <= m_SendRepeatitiveInterval
                Windows2.Delay(100, m_Ct)
            End While
        Catch ex As TaskCanceledException
            '
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try
    End Sub

    ''' <summary>
    ''' 添加到已发送弹幕容器
    ''' </summary>
    ''' <param name="danmu"></param>
    Private Sub AddToSentBC(ByVal danmu As SentDanmuInfo)
        Try
            m_SentBC.TryAdd(danmu, 6180, m_Ct)
        Catch ex As OperationCanceledException
            Logger.WriteLine("已发送弹幕添加到已发送表超时：" & danmu.Context)
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try
    End Sub

    ''' <summary>
    ''' 报告发送弹幕结果
    ''' </summary>
    ''' <param name="taskCompletion"></param>
    Private Sub ReportSendCompletedAction(ByVal taskCompletion As (Danmu As SentDanmuInfo, Success As Boolean, IsAllow As Boolean, Message As String))
        Dim isSucceed = taskCompletion.Success
        Dim sentDanmu = taskCompletion.Danmu
        If isSucceed Then
            AddToSentBC(sentDanmu)
        End If

        Dim EventArgs = New DanmuSendCompletedEventArgs With {
            .Danmu = New DanmuInfo With {.Context = sentDanmu.Context, .Source = sentDanmu.Source},
            .IsAllow = taskCompletion.IsAllow,
            .Success = isSucceed,
            .Message = taskCompletion.Message
        }
        RaiseEvent SendCompleted(Nothing, EventArgs)
    End Sub

    ''' <summary>
    ''' 报告发送重复弹幕结果
    ''' </summary>
    Private Sub ReportSendRepeatitiveAction(ByVal rptDanmu As RepeatDanmuInfo)
        Dim EventArgs = New DanmuSendCompletedEventArgs With {
            .Danmu = New DanmuInfo With {.Context = rptDanmu.Context, .Source = rptDanmu.Source},
            .IsRepeat = True,
            .IsAllow = True,
            .Success = True,
            .Message = "重复弹幕"
        }
        RaiseEvent SendCompleted(Nothing, EventArgs)
    End Sub

    ''' <summary>
    ''' 修改重复弹幕处理方式
    ''' </summary>
    ''' <param name="handle"></param>
    Public Sub ChangeDanmuRepeatitiveHandle(ByVal handle As DanmuRepeatOptions)
        Me.m_RepeatHandle = handle
    End Sub

    ''' <summary>
    ''' 更改弹幕颜色
    ''' </summary>
    ''' <returns></returns>
    Public Shared Async Function ChangeDanmuColorAsync(ByVal colorValue As Integer) As Task(Of (Success As Boolean, Message As String, Result As String))
		Dim changeRst = Await InternalChangeDanmuColorAsync(colorValue)
		If changeRst.Success Then
            s_SenderInfo.DanmuColorDec = colorValue
            ReMakeDanmuSendTemplete()
        End If

        Return changeRst
    End Function

    ''' <summary>
    ''' 更改弹幕颜色
    ''' </summary>
    ''' <returns></returns>
    Private Shared Async Function InternalChangeDanmuColorAsync(ByVal colorValue As Integer) As Task(Of (Success As Boolean, Message As String, Result As String))
        Dim url = "https://api.live.bilibili.com/api/ajaxSetConfig"
		Dim postData = $"room_id={s_SenderInfo.RoomId}&color=0x{colorValue.ToString("x")}&csrf_token={s_SenderInfo.Token}&csrf={s_SenderInfo.Token}"
		Dim referer = "https://live.bilibili.com/" & s_SenderInfo.RoomId
		Return Await BilibiliApi.DoApiPostAsync(url, postData, referer)
	End Function

    ''' <summary>
    ''' 更新进入直播间的时间戳（第一次或者是每次断线重连之后都需要更改加入直播间的时间戳）
    ''' </summary>
    ''' <param name="newValue"></param>
    Public Sub UpdateJoinedLiveRoomTimestamp(ByVal newValue As Long)
        If s_SenderInfo Is Nothing Then Return

        Interlocked.Exchange(s_SenderInfo.JoinedLiveRoomTimestamp, newValue.ToStringOfCulture)
        ReMakeDanmuSendTemplete()
    End Sub

    ''' <summary>
    ''' 重新生成发送弹幕模板
    ''' </summary>
    Private Shared Sub ReMakeDanmuSendTemplete()
        ' 改变弹幕颜色，或者重新进入直播间，都需要更新模板
        MakeDanmuSendTemplete()
    End Sub

    ''' <summary>
    ''' 生成发送弹幕模板
    ''' </summary>
    Private Shared Sub MakeDanmuSendTemplete()
        ' ################################ mode 和 bubble参数暂时不清楚有何用途 ################################
        Interlocked.Exchange(m_DanmuSendTemplete, $"color={s_SenderInfo.DanmuColorDec.ToStringOfCulture}&fontsize={s_SenderInfo.DanmuFontSize.ToStringOfCulture}&mode=1&msg={{0}}&rnd={s_SenderInfo.JoinedLiveRoomTimestamp}&roomid={s_SenderInfo.RoomId}&bubble=0&csrf_token={s_SenderInfo.Token}&csrf={s_SenderInfo.Token}")
    End Sub
#End Region
End Class
