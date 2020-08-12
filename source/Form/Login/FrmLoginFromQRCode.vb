Imports System.Net
Imports QRCoder
Imports ShanXingTech
Imports ShanXingTech.Net2

Public Class FrmLoginFromQRCode
#Region "枚举区"
    Private Enum ScanStatus
        ''' <summary>
        ''' 未扫码
        ''' </summary>
        CantScan
        ''' <summary>
        ''' 已扫码未确认授权
        ''' </summary>
        CantConfirm
        ''' <summary>
        ''' 已确认授权
        ''' </summary>
        Confirmed
        ''' <summary>
        ''' 二维码已失效，需要重新刷新
        ''' </summary>
        NeedRefresh
        ''' <summary>
        ''' 未知情况
        ''' </summary>
        Unknown
    End Enum
#End Region
#Region "字段区"
    Private ReadOnly m_QRCodeGenerator As QRCodeGenerator
    Private WithEvents m_PictureBox As PictureBox
    Private ReadOnly m_Width As Integer
    Private ReadOnly m_Height As Integer
    Private WithEvents m_ScanTimer As Timer
    Private ReadOnly m_ScanStatusDic As Dictionary(Of ScanStatus, String)
    Private m_ScanStatus As ScanStatus
    Private m_PreviousLoadQRCodeTime As Date
    ''' <summary>
    ''' 二维码中间的图标
    ''' </summary>
    Private ReadOnly m_Icon As Bitmap
#End Region

#Region "属性区"
    Private m_OauthUrl As String
    ''' <summary>
    ''' 认证链接
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property OauthUrl As String
        Get
            Return m_OauthUrl
        End Get
    End Property
    Private m_OauthKey As String
    ''' <summary>
    ''' 认证Key
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property OauthKey As String
        Get
            Return m_OauthKey
        End Get
    End Property

    Public ReadOnly Property LoginedCookies As Net.CookieContainer
        Get
            Return HttpAsync.Instance.Cookies
        End Get
    End Property

#End Region

#Region "构造函数"
    Public Sub New()
        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        m_ScanStatusDic = New Dictionary(Of ScanStatus, String) From {
            {ScanStatus.CantScan, "请使用 哔哩哔哩客户端 扫码登录"}，
            {ScanStatus.CantConfirm, "扫描成功，请在手机上确认是否授权"}，
            {ScanStatus.Confirmed, "授权成功,正在跳转..."}，
            {ScanStatus.NeedRefresh, "二维码已失效，点击刷新"},
            {ScanStatus.Unknown, "未知情况，请向开发者反馈！！！"}
        }
        m_Width = 140
        m_Height = m_Width
        m_QRCodeGenerator = New QRCodeGenerator
        m_Icon = New Bitmap("./res/snail_128px.ico")
        m_PictureBox = New PictureBox With {
            .Size = New Size(m_Width, m_Height),
            .SizeMode = PictureBoxSizeMode.AutoSize
        }
        Me.Controls.Add(m_PictureBox)

        m_ScanTimer = New Timer With {
            .Interval = 3000
        }
        m_ScanTimer.Start()

        Me.StartPosition = FormStartPosition.CenterScreen
        Me.FormBorderStyle = FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.TopMost = True

        AutoSizeMe()
    End Sub

#End Region

#Region "IDisposable Support"
    ' 要检测冗余调用
    Dim isDisposed2 As Boolean = False

    ''' <summary>
    ''' 重写Dispose 以清理非托管资源
    ''' </summary>
    ''' <param name="disposing"></param>
    Protected Overrides Sub Dispose(disposing As Boolean)
        ' 窗体内的控件调用Close或者Dispose方法时，isDisposed2的值为True
        If isDisposed2 Then Return

        Try
            ' TODO: 释放托管资源(托管对象)。
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                    components = Nothing
                End If

                m_QRCodeGenerator?.Dispose()
                m_Icon?.Dispose()

                m_ScanTimer?.Stop()
                m_ScanTimer?.Dispose()

                m_PictureBox?.Dispose()
            End If

            ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
            ' TODO: 将大型字段设置为 null。

            isDisposed2 = True
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '' NOTE: Leave out the finalizer altogether if this class doesn't   
    '' own unmanaged resources itself, but leave the other methods  
    '' exactly as they are.   
    'Protected Overrides Sub Finalize()
    '    Try
    '        ' Finalizer calls Dispose(false)  
    '        Dispose(False)
    '    Finally
    '        MyBase.Finalize()
    '    End Try
    'End Sub
#End Region

    Private Function CreateQRCode() As Bitmap
        Dim bmp As Bitmap
        Dim qrData = m_QRCodeGenerator.CreateQrCode(m_OauthUrl, QRCodeGenerator.ECCLevel.H)
        Using qrCode = New QRCode(qrData)
            bmp = qrCode.GetGraphic(5, Color.Black, Color.White, m_Icon)
        End Using

        If bmp Is Nothing Then
            bmp = New Bitmap(m_Width, m_Height)
        End If
        DrawScanTips(DirectCast(bmp, Image))

        Return bmp
    End Function

    Private Sub DrawScanTips(ByRef bmp As Image)
        Dim tipsColor As Color
        Select Case m_ScanStatus
            Case ScanStatus.CantScan
                tipsColor = Color.Red
            Case ScanStatus.CantConfirm
                tipsColor = Color.Gold
            Case ScanStatus.Confirmed
                tipsColor = Color.LimeGreen
            Case ScanStatus.NeedRefresh
                tipsColor = Color.Gray
            Case Else
                tipsColor = Color.Gray
        End Select

        Dim tips = m_ScanStatusDic(m_ScanStatus)
        DrawScanTipsInternal(bmp, tips, tipsColor)
    End Sub

    Private Sub DrawScanTipsInternal(ByRef bmp As Image, ByVal tips As String, ByVal tipsColor As Color)
        If bmp Is Nothing Then
            Return
        End If

        Using g As Graphics = Graphics.FromImage(bmp)
            Dim drawBrush As New SolidBrush(tipsColor)
            Dim drawFont As Font = New Font("宋体", 16, FontStyle.Regular, GraphicsUnit.Pixel)
            Dim tipsSize = g.MeasureString(tips， drawFont)
            ' 居中显示
            Dim tipsAlignX = (bmp.Width - tipsSize.Width) / 2
            Dim drawPoint As New PointF(tipsAlignX, bmp.Height - tipsSize.Height)
            ' 把提示区刷白
            Dim pen As New Pen(Color.White, tipsSize.Height)
            Dim halfOfCharHeight = tipsSize.Height / 2 + (drawFont.Height - drawFont.Size) / 2
            g.DrawLine(pen, New PointF(15, bmp.Height - halfOfCharHeight), New PointF(bmp.Width, bmp.Height - halfOfCharHeight))
            ' 写字
            g.DrawString(tips, drawFont, drawBrush, drawPoint)
        End Using
    End Sub

    Private Async Sub FrmLoginFromQRCode_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Await ShowQRCodeAsync()
    End Sub

    Private Async Function ShowQRCodeAsync() As Task
        Try
            Me.BeginInvoke(Sub() m_PictureBox.Enabled = False)
            If Not m_ScanTimer.Enabled Then m_ScanTimer.Start()

            Await InternalShowQRCodeAsync()
            m_ScanStatus = ScanStatus.CantScan
            m_PreviousLoadQRCodeTime = Now
        Catch ex As Exception
            Logger.WriteLine(ex)
        Finally
            Me.BeginInvoke(Sub() m_PictureBox.Enabled = True)
        End Try
    End Function

    Private Async Function InternalShowQRCodeAsync() As Task
        Dim oauth = Await GetOauthInfoAsync()
        If oauth Is Nothing Then
            ShowFailureTips()
            Return
        End If

        m_OauthUrl = oauth.url
        m_OauthKey = oauth.oauthKey

        Me.BeginInvoke(Sub() m_PictureBox.Image = CreateQRCode())
    End Function

    Private Async Function GetOauthInfoAsync() As Task(Of LoginUrlEntity.Data)
        Dim cts As New Threading.CancellationTokenSource
        cts.CancelAfter(TimeSpan.FromMinutes(1))

GetOauthKeyLoop:
        If cts.IsCancellationRequested Then Return Nothing

        Dim getRst = Await BilibiliApi.GetLoginUrlAsync
        If Not getRst.Success Then
            ShowFailureTips()
            Return Nothing
        End If

        Dim json = getRst.Message
        Dim root = MSJsSerializer.Deserialize(Of LoginUrlEntity.Root)(json)
        If root Is Nothing Then
            ShowFailureTips()
            Return Nothing
        End If

        If root.data?.oauthKey.IsNullOrEmpty Then
            ShowFailureTips("获取OauthKey失败，重试")
            GoTo GetOauthKeyLoop
        End If

        Return root.data
    End Function

    Private Sub ShowFailureTips()
        Windows2.DrawTipsTask(Me, "获取登录信息失败 " & RandomEmoji.Helpless, 3000, False, False)
    End Sub

    Private Sub ShowFailureTips(ByVal value As String)
        Windows2.DrawTipsTask(Me, value & " " & RandomEmoji.Helpless, 3000, False, False)
    End Sub

    Private Sub ShowSuccessTips()
        Windows2.DrawTipsTask(Me, "扫码成功 " & RandomEmoji.Happy, 3000, False, False)
    End Sub

    Private Async Sub m_ScanTimer_Tick(sender As Object, e As EventArgs) Handles m_ScanTimer.Tick
        Dim getRst = Await BilibiliApi.GetLoginInfoAsync(m_OauthKey)

        Dim root = MSJsSerializer.DeserializeObject(getRst.Message)
        If root Is Nothing Then Return

        Dim data As Integer
        If Not Integer.TryParse(root("data").ToString, data) Then Return

        Dim previousStatuss = m_ScanStatus
        Select Case data
            Case -2
                ' Can't Match oauthKey~ 有可能也是已经扫码并且确认了，这个时候需要去获取一下用户信息，如果能获取到说明已经是登录成功了
                m_ScanStatus = ScanStatus.NeedRefresh
            Case -4
                m_ScanStatus = ScanStatus.CantScan
            Case -5
                m_ScanStatus = ScanStatus.CantConfirm
            Case Else
                m_ScanStatus = ScanStatus.Unknown
                m_ScanTimer.Stop()
                Logger.WriteLine($"{NameOf(m_OauthKey)}:{m_OauthKey} {NameOf(getRst.Message)}:{getRst.Message}",,,)
                MessageBox.Show("扫码登录遇到未知情况，请反馈给开发者修复", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Select

        If m_ScanStatus = ScanStatus.NeedRefresh Then
            Dim loginSuccess = Await GetLoginResultAsync()
            If loginSuccess Then
                m_ScanStatus = ScanStatus.Confirmed
                m_ScanTimer.Stop()
                DrawScanTips(m_PictureBox.Image)
                Await Task.Delay(618)
                Me.Close()
                Return
            End If
        End If

        ' 超时刷新
        If m_ScanStatus = ScanStatus.NeedRefresh OrElse
            (Now - m_PreviousLoadQRCodeTime).TotalSeconds > 3 * 60 Then
            m_ScanStatus = ScanStatus.NeedRefresh
            m_PreviousLoadQRCodeTime = Now
            m_ScanTimer.Stop()
        End If

        If previousStatuss <> m_ScanStatus Then
            DrawScanTips(m_PictureBox.Image)
        End If
    End Sub

    Private Async Function GetLoginResultAsync() As Task(Of Boolean)
        Dim getRst = Await BilibiliApi.GetCurrentUserNavAsync
        Return getRst.Success
    End Function

    Private Sub m_PictureBox_MouseMove(sender As Object, e As MouseEventArgs) Handles m_PictureBox.MouseMove
        If m_ScanStatus = ScanStatus.NeedRefresh Then
            Me.Cursor = Cursors.Hand
        End If
    End Sub

    Private Sub m_PictureBox_MouseLeave(sender As Object, e As EventArgs) Handles m_PictureBox.MouseLeave
        Me.Cursor = Cursors.Default
    End Sub

    Private Async Sub m_PictureBox_Click(sender As Object, e As EventArgs) Handles m_PictureBox.Click
        If m_ScanStatus <> ScanStatus.NeedRefresh Then Return
        Try
            m_PictureBox.Enabled = False
            DrawScanTipsInternal(m_PictureBox.Image, "二维码加载中...", Color.Gold)
            Await ShowQRCodeAsync()
        Catch ex As Exception
            Logger.WriteLine(ex)
        Finally
            m_PictureBox.Enabled = True
        End Try
    End Sub

    Private Sub m_PictureBox_Resize(sender As Object, e As EventArgs) Handles m_PictureBox.Resize
        AutoSizeMe()
    End Sub

    Private Sub AutoSizeMe()
#If Not DEBUG Then
        If Not Me.Visible Then Return
        Me.ClientSize = m_PictureBox.Size
#End If
    End Sub

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Await ShowQRCodeAsync()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DrawScanTipsInternal(m_PictureBox.Image, TextBox1.Text.Trim, Color.Gold)
    End Sub

    Private Async Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Await GetLoginResultAsync()
    End Sub
End Class