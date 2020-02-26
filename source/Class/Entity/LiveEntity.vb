Namespace LiveEntity
#Disable Warning IDE1006

    Public Class List
        ''' <summary>
        ''' 21
        ''' </summary>
        Public Property id() As String
        ''' <summary>
        ''' 1
        ''' </summary>
        Public Property parent_id() As String
        ''' <summary>
        ''' 10
        ''' </summary>
        Public Property old_area_id() As String
        ''' <summary>
        ''' 视频唱见
        ''' </summary>
        Public Property name() As String
        ''' <summary>
        ''' 0
        ''' </summary>
        Public Property act_id() As String
        ''' <summary>
        ''' 1
        ''' </summary>
        Public Property pk_status() As String
        ''' <summary>
        ''' Hot_status
        ''' </summary>
        Public Property hot_status() As Integer
        ''' <summary>
        ''' 0
        ''' </summary>
        Public Property lock_status() As String
        ''' <summary>
        ''' https://i0.hdslb.com/bfs/vc/72b93ddafdf63c9f0b626ad546847a3c03c92b6f.png
        ''' </summary>
        Public Property pic() As String
        ''' <summary>
        ''' shipinchangjian
        ''' </summary>
        Public Property pinyin() As String
        ''' <summary>
        ''' 12
        ''' </summary>
        Public Property cate_id() As String
        ''' <summary>
        ''' 娱乐
        ''' </summary>
        Public Property parent_name() As String
    End Class

    Public Class Data
        ''' <summary>
        ''' Id
        ''' </summary>
        Public Property id() As Integer
        ''' <summary>
        ''' 娱乐
        ''' </summary>
        Public Property name() As String
        ''' <summary>
        ''' List
        ''' </summary>
        Public Property list() As List(Of List)
    End Class

    Public Class Root
        ''' <summary>
        ''' Code
        ''' </summary>
        Public Property code() As Integer
        ''' <summary>
        ''' success
        ''' </summary>
        Public Property msg() As String
        ''' <summary>
        ''' success
        ''' </summary>
        Public Property message() As String
        ''' <summary>
        ''' Data
        ''' </summary>
        Public Property data() As List(Of Data)
    End Class
#Enable Warning IDE1006
End Namespace


