Imports ShanXingTech
Imports 姬娘插件.Events

Partial Class DanmuControl
#Region "测试区"
    Private Sub btnSendTest_Click(sender As Object, e As EventArgs) Handles btnSendTest.Click
        'Dim rand As New Random(Date.Now.Millisecond)
        'Dim inputDanmu = cmbDanmuInput.Text
        '' 没有输入弹幕时直接返回
        'If inputDanmu.Length = 0 Then Return
        'SendDanmuBegin(DanmuSource.Input)
        'For i = 1 To 4
        '    DanmuEntry.SendDanmu(New DanmuInfo With {.Context = inputDanmu, .Source = DanmuSource.Internal, .Count = rand.Next(1, 11)})
        '    Windows2.RandDelay(68, 2000, TimePrecision.Millisecond)
        'Next
        'Return
        'Windows2.Delay(2000)
        'For i = 1 To 3
        '    m_DanmuSender.Add(New DanmuInfo With {.Context = inputDanmu, .Source = DanmuSource.Input, .Count = 1})
        'Next

        'MergeTest(cmbDanmuInput.Text)
        'MixMergeTest()
        'Dim isOnBottom = ((m_WebScroll.OffsetRectangle.Height + m_WebScroll.ScrollTop) = m_WebScroll.ScrollRectangle.Height)
        'Debug.Print(Logger.MakeDebugString("在底部 " & isOnBottom.ToStringOfCulture))

        ShowUserNickClickMenu()
    End Sub

    ' 此类仅仅是测试功能的实现性，而不是测试代码覆盖率等
    Public Sub SendDanmuByEventsForTest(ByRef danmu As DanmuInfo)
        If Not m_DanmuSender.CanSend Then Return

        m_DanmuSender.Add(danmu)
    End Sub

    Public Sub MergeTest(ByVal inputDanmu As String)
        Debug.Print(Logger.MakeDebugString("测试开始"))

        Dim rand As New Random(Date.Now.Millisecond)
        ' 没有输入弹幕时直接返回
        If inputDanmu.Length = 0 Then Return
        For i = 1 To 4
            SendDanmuByEventsForTest(New DanmuInfo With {.Context = inputDanmu, .Source = DanmuSource.Internal, .Count = rand.Next(1, 11)})
            Windows2.RandDelay(68, 2000, TimePrecision.Millisecond)
        Next
    End Sub

    Public Sub MixMergeTest()
        Debug.Print(Logger.MakeDebugString("测试开始"))

        Dim rand As New Random(Date.Now.Millisecond)
        Dim fixedDanmu = "test" & Date.Now.Ticks.ToStringOfCulture
        Dim inputDanmu = String.Empty

        Task.Run(
            Sub()
                Dim cts As New Threading.CancellationTokenSource
                Dim ct = cts.Token
                ' 测试持续30秒到60秒
                cts.CancelAfter(rand.Next(CInt(1000 * 60 * 0.5), 1000 * 60 * 1))
                While True
                    If cts.IsCancellationRequested Then Exit While
                    ' 使固定弹幕的出现几率多一点
                    inputDanmu = If(rand.Next(0, 2) = 1, fixedDanmu,
                    If(rand.Next(0, 2) = 1, fixedDanmu, "test" & Date.Now.Ticks.ToStringOfCulture))

                    SendDanmuByEventsForTest(New DanmuInfo With {.Context = inputDanmu, .Source = DanmuSource.Internal, .Count = rand.Next(1, 11)})
                    Windows2.RandDelay(68, 2000, TimePrecision.Millisecond)
                End While
            End Sub)
    End Sub

#End Region
End Class
