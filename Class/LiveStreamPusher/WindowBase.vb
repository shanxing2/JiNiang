Imports ShanXingTech.Win32API

Public MustInherit Class WindowBase
    Public ReadOnly Property Hwnd As IntPtr

    ''' <summary>
    ''' 窗体类名
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ClassName As String
    ''' <summary>
    ''' 窗体标题
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Title As String
    ''' <summary>
    ''' 窗体是否可用
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property Enabled As Boolean
        Get
            Return IsWindowEnabled(Hwnd)
        End Get
    End Property
End Class