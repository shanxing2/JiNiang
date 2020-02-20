Imports ShanXingTech

Public Class FrmYanText
#Region "属性区"
    Private m_SelectYanText As String
    ''' <summary>
    ''' 当前选中的颜文字
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property SelectYanText() As String
        Get
            Return m_SelectYanText
        End Get
    End Property
#End Region

#Region "常量区"
    Private Const YanTextFile = ".\res\YanText.txt"
#End Region


    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        If IO.File.Exists(YanTextFile) Then
            Dim yanText = IO2.Reader.ReadFile(YanTextFile, System.Text.Encoding.UTF8)
            If yanText.IsNullOrEmpty Then Return

            'Me.SuspendLayout()

            flpYanTextContainer.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            flpYanTextContainer.AutoSize = True

            Me.AutoSize = True
            Me.DoubleBuffered = True

            ShowYanText(yanText)

            'Me.ResumeLayout(False)
            'Me.PerformLayout()
        Else
            Dim label = New Label With {
                .Text = "资源文件不存在：" & IO.Path.GetFullPath(YanTextFile),
                .AutoSize = True,
                .Margin = New Padding(3, 3, 3, 8),
                .ForeColor = Color.Red
            }
            flpYanTextContainer.Controls.Add(label)
        End If
    End Sub

    Private Sub ShowYanText(ByVal yanText As String)
        Dim yanTextArr = yanText.Split({Environment.NewLine, vbCr, vbLf, vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
        For Each yt In yanTextArr
            Dim label = New Label With {
                .Text = yt,
                .AutoSize = True,
                .Margin = New Padding(3, 3, 3, 8)
            }

            flpYanTextContainer.Controls.Add(label)
            AddHandler label.MouseMove, Sub(sender2, e2)
                                            label.ForeColor = Color.White
                                            label.BackColor = Color.Pink
                                            label.Cursor = Cursors.Hand
                                        End Sub
            RemoveHandler label.MouseMove, New MouseEventHandler(Sub()
                                                                 End Sub)
            AddHandler label.MouseLeave, Sub(sender2, e2)
                                             label.ForeColor = Color.Black
                                             label.BackColor = Color.Transparent
                                             label.Cursor = Cursors.Default
                                         End Sub
            RemoveHandler label.MouseLeave, New EventHandler(Sub()
                                                             End Sub)

            AddHandler label.Click, Sub(sender2, e2)
                                        m_SelectYanText = label.Text
                                        Me.Visible = False
                                        Me.TopMost = False
                                    End Sub
            RemoveHandler label.Click, New EventHandler(Sub()
                                                        End Sub)
        Next
    End Sub

    Private Sub FrmYanText_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        m_SelectYanText = String.Empty
    End Sub
End Class