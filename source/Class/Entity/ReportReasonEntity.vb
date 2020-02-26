Namespace ReportReasonEntity
    Public Class Data

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property id As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property reason As String

    End Class

    Public Class Root
        Inherits APIPostResponseBaseEntity.Root

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property data As List(Of Data)

    End Class
End Namespace