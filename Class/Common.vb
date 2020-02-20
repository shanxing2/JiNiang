Imports ShanXingTech

Public NotInheritable Class Common
#Region "函数区"

    Public Shared Sub CopyToClipboard(ByVal data As String, ByRef tipsDisplayControl As Control)
        Try
            Clipboard.SetDataObject(data, False, 3, 100)
            Windows2.DrawTipsTask(tipsDisplayControl, "复制成功" & RandomEmoji.Happy, 1000, True)
        Catch ex As Exception
            Windows2.DrawTipsTask(tipsDisplayControl, "复制失败，请重新复制" & RandomEmoji.Helpless, 1000, False)
        End Try
    End Sub
#End Region
End Class
