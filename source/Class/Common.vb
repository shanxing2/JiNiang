Imports ShanXingTech

Public NotInheritable Class Common
#Region "常量"
    ''' <summary>
    ''' 观众弹幕模板(uid-ts-ct-nickname-timestamp-danmu)
    ''' </summary>
    Public Const ViewerNickDanmuTemplete As String = "<div style=""display:inline""><input type=""button"" class=""btn-like-span pointer open-menu"" data-uid=""{0}"" data-ts=""{1}"" data-ct=""{2}"" value=""{3}"" onmouseover=""ViewerNick_OnmouseHover(this)"" onmouseleave=""ViewerNick_OnmouseLeave(this)""/><span> {4}</span><br/><span>{5}</span></div>"
    ''' <summary>
    ''' 播主、自己（进自己直播间就是自己啦）弹幕模板
    ''' </summary>
    Public Const UpOrOwnDanmuTemplete As String = "{0} <span> {1}</span><br/><span>{2}</span>"
#End Region

#Region "函数区"

    Public Shared Sub CopyToClipboard(ByVal data As String, ByRef tipsDisplayControl As Control)
        Try
            If String.IsNullOrEmpty(data) Then Return

            Clipboard.SetText(data, TextDataFormat.UnicodeText)
            Windows2.DrawTipsTask(tipsDisplayControl, "复制成功" & RandomEmoji.Happy, 1000, True)
        Catch ex As Exception
            Windows2.DrawTipsTask(tipsDisplayControl, "复制失败，请重新复制" & RandomEmoji.Helpless, 1000, False)
        End Try
    End Sub

    ''' <summary>
    ''' 显示操作结果
    ''' </summary>
    ''' <param name="ownerControl">提示在哪里（控件或者窗口）显示</param>
    ''' <param name="opRst"></param>
    Public Shared Sub ShowOperateResultTask(ByVal ownerControl As Control, ByVal opRst As (Success As Boolean, Message As String, Result As String))
        Windows2.DrawTipsTask(ownerControl, If(opRst.Success, "成功", opRst.Message), 1000, opRst.Success, False)
    End Sub

    ''' <summary>
    ''' 显示操作结果
    ''' </summary>
    ''' <param name="ownerControl">提示在哪里（控件或者窗口）显示</param>
    ''' <param name="success"></param>
    Public Shared Sub ShowOperateResultTask(ByVal ownerControl As Control, ByVal success As Boolean)
        Windows2.DrawTipsTask(ownerControl, If(success, "成功", "失败"), 1000, success, False)
    End Sub

    ''' <summary>
    ''' 显示操作结果
    ''' </summary>
    ''' <param name="ownerControl">提示在哪里（控件或者窗口）显示</param>
    ''' <param name="success"></param>
    ''' <param name="tips">提示</param>
    ''' <param name="timeout">倒计时，单位毫秒</param>
    Public Shared Sub ShowOperateResultTask(ByVal ownerControl As Control, ByVal success As Boolean, ByVal tips As String, Optional ByVal timeout As Integer = 1000)
        Windows2.DrawTipsTask(ownerControl, tips, timeout, success, False)
    End Sub

#End Region
End Class
