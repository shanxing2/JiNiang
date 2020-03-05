Namespace LoginUrlEntity
    Public Class Data

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property url As String

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property oauthKey As String

    End Class



    Public Class Root
        Inherits APIPostResponseBaseEntity.Root
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property status As String

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property ts As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property data As Data
    End Class
End Namespace

