'Imports System.Text
'Imports System.Threading
'Imports ShanXingTech
'Imports ShanXingTech.Text2
'Imports ShanXingTech.Win32API

'Public Class LiveStreamPusher
'#Region "事件区"
'    Public Class ClickedEventArgs
'        Inherits EventArgs

'        ''' <summary>
'        ''' 推流结果
'        ''' </summary>
'        ''' <returns></returns>
'        Public Property Sucess As Boolean
'        ''' <summary>
'        ''' 推流结果描述
'        ''' </summary>
'        ''' <returns></returns>
'        Public Property Message As String

'        Public Sub New()
'        End Sub

'        Public Sub New(status As Boolean, message As String)
'            Me.Sucess = status
'            Me.Message = message
'        End Sub
'    End Class

'    Public Event Clicked As EventHandler(Of ClickedEventArgs)
'#End Region

'#Region "字段区"
'    Private m_LButtonDown As Integer
'    Private m_LButtonUp As Integer


'#End Region

'#Region "构造函数区"
'    Sub New()
'        m_LButtonDown = -1
'        m_LButtonUp = -1
'    End Sub
'#End Region

'#Region "函数区"
'    Public Sub Push()
'        Dim lsp As LiveStreamPusherInfo


'        Dim pushButtonHwnd = FindPushButton()
'        If IntPtr.Zero = pushButtonHwnd Then
'            Return
'        End If

'        If Win32API.IsWindowEnabled(pushButtonHwnd) Then
'            ' 有些推流按钮是在控件组中，具体句柄很难查找，按照位置来定位比较容易些
'            ' SendMessage(hwndOfBrowser, WM_MOUSEMOVE, 0, x + (y << 16))
'            m_LButtonDown = SendMessage(pushButtonHwnd, WM_LBUTTONDOWN, 0, lsp.OffsetLocation.X + (lsp.OffsetLocation.Y << 16))
'            If -1 = m_LButtonDown Then
'                Return
'            End If
'            m_LButtonUp = SendMessage(pushButtonHwnd, WM_LBUTTONUP, 0, lsp.OffsetLocation.X + (lsp.OffsetLocation.Y << 16))
'            If -1 = m_LButtonUp Then
'                Return
'            End If

'            GetPushStatus(lsp, pushButtonHwnd)
'        End If
'    End Sub

'    Public Function Find(ByVal lsp As LiveStreamPusherInfo) As IntPtr
'        Dim liveStreamPusherHwnd = Win32API.FindWindow(lsp.ClassName, lsp.Title)
'        Dim isEqual = EnsureEqual(liveStreamPusherHwnd, lsp.StartPath)
'        If isEqual Then
'            Return liveStreamPusherHwnd
'        End If

'        Do
'            liveStreamPusherHwnd = Win32API.FindWindowEx(IntPtr.Zero, liveStreamPusherHwnd, Nothing, Nothing)
'            isEqual = EnsureEqual(liveStreamPusherHwnd, lsp.StartPath)
'            If isEqual Then
'                Exit Do
'            End If
'        Loop Until IntPtr.Zero = liveStreamPusherHwnd

'        Return If(isEqual, liveStreamPusherHwnd, IntPtr.Zero)
'    End Function

'    Private Function EnsureEqual(ByVal hwnd As IntPtr, ByVal fileName As String) As Boolean
'        If hwnd <> IntPtr.Zero Then
'            Dim pid = GetWindowThreadProcessId(hwnd, 0)
'            If pid < 0 Then
'                Return False
'            End If

'            Dim proc = Process.GetProcessById(pid)
'            If proc.MainModule.FileName.IsNullOrEmpty Then
'                Return False
'            End If

'            If proc.MainModule.FileName <> fileName Then
'                Return False
'            End If
'        End If

'        Return True
'    End Function

'    Public Function FindPushButton(ByVal liveStreamPusherHwnd As IntPtr) As IntPtr
'        Dim pushButtonHwnd As IntPtr
'        pushButtonHwnd = Win32API.FindWindowEx(liveStreamPusherHwnd, pushButtonHwnd, Nothing, Nothing)
'    End Function

'    Private Sub GetPushStatus(ByVal lsp As LiveStreamPusherInfo, ByVal hWnd As IntPtr)
'        Dim message As String = "成功"
'        Dim pushSucess As Boolean

'        Dim sb = StringBuilderCache.Acquire(255)
'        GetWindowText(hWnd, sb, sb.Capacity * 2)
'        If sb.Length > 0 Then
'            If lsp.Title = StringBuilderCache.GetStringAndReleaseBuilder(sb) Then
'                message = "推流成功"
'                pushSucess = True
'            Else
'                message = "获取推流状态失败"
'            End If
'        Else
'            message = "获取推流状态失败"
'        End If

'        RaiseEvent Clicked(Nothing, New ClickedEventArgs With {
'            .Sucess = pushSucess,
'            .Message = message
'        })
'    End Sub

'    Public Sub StopPush()

'    End Sub
'#End Region
'End Class
