Partial Class DmCmdParser

    Public Class DanmuEventTransfer

#Region "字段区"
        ''' <summary>
        ''' 已经引发过的事件词典缓存
        ''' </summary>
        Private Shared m_RaisedEventCache As Dictionary(Of String, EventArgs)
#End Region

#Region "构造函数区"
        Shared Sub New()
            m_RaisedEventCache = New Dictionary(Of String, EventArgs)(StringComparison.OrdinalIgnoreCase)
        End Sub
#End Region

#Region "函数区"
        ''' <summary>
        ''' 引发事件
        ''' </summary>
        ''' <typeparam name="T"></typeparam>
        ''' <param name="eventName"></param>
        ''' <param name="EventArgs"></param>
        Public Shared Sub Raise(Of T)(ByVal eventName As String, ByVal EventArgs As T)
            If eventName Is Nothing Then
                Throw New ArgumentNullException(String.Format(My.Resources.NullReference, NameOf(eventName)))
            End If
            If EventArgs Is Nothing Then
                Throw New ArgumentNullException(String.Format(My.Resources.NullReference, NameOf(EventArgs)))
            End If

            Dim e As EventArgs = Nothing
            If m_RaisedEventCache.TryGetValue(eventName, e) Then

            Else

            End If

            ' 反射是相对比较耗时的操作，所以已经引发过的事件可以缓存起来重用以提高效率
            If Not m_RaisedEventCache.TryGetValue(eventName, e) Then
                m_RaisedEventCache.Add(eventName, e)
            End If
        End Sub



#End Region
    End Class

End Class
