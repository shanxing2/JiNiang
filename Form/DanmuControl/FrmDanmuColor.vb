Imports ShanXingTech

Public Class FrmDanmuColor
#Region "字段区"
    Private m_LastRadioButton As RadioButton
    Private m_DanmuColorDec As Integer
#End Region

    Sub New(ByVal danmuColorDec As Integer)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        m_DanmuColorDec = danmuColorDec

        Me.SuspendLayout()
        Me.Panel1.SuspendLayout()

        lblCenterLine.Visible = False

		' 可改为从 （旧版）https://api.live.bilibili.com/api/ajaxGetConfig  处自动获取，自动创建rdbtn，自动设置颜色
		' 新版 https://api.live.bilibili.com/userext/v1/DanmuConf/getConfig?roomid=4236342
		rdbtnBlack.ForeColor = Color.FromArgb(0, 0, 0)
        rdbtnRed.ForeColor = Color.FromArgb(255, 104, 104)
        rdbtnBlue.ForeColor = Color.FromArgb(102, 204, 255)
        rdbtnPurple.ForeColor = Color.FromArgb(227, 63, 255)
        rdbtnCyan.ForeColor = Color.FromArgb(0, 255, 252)
        rdbtnGreen.ForeColor = Color.FromArgb(126, 255, 0)
        rdbtnYellow.ForeColor = Color.FromArgb(255, 237, 79)
        rdbtnOrange.ForeColor = Color.FromArgb(255, 152, 0)
        rdbtnPink.ForeColor = Color.FromArgb(255, 115, 154)

		' 初始化弹幕颜色RadioButton样式
		For Each ctrl As Control In Me.Panel1.Controls
            Dim rdbtn = TryCast(ctrl, RadioButton)
            If rdbtn Is Nothing Then Continue For

            rdbtn.AutoSize = True
            rdbtn.FlatStyle = FlatStyle.Flat
            If danmuColorDec = ParseColorRgbValue(rdbtn.ForeColor).Dec Then
                rdbtn.Checked = True
                m_LastRadioButton = rdbtn
            End If
        Next

        Me.Panel1.ResumeLayout()
        Me.ResumeLayout()
    End Sub

    Private Function ParseColorRgbValue(ByVal color As Color) As (Hex As String, Dec As Integer)
        Dim colorRgbHexValue = color.R.ToString("x02") & color.G.ToString("x02") & color.B.ToString("x02")
        ' 因为白色的选中状态下很难看出来是已经选中，所以改成黑色
        ' 因此 当用户选择黑色时，当做是 白色处理
        If "000000" = colorRgbHexValue Then
            colorRgbHexValue = "ffffff"
        End If

        Return (colorRgbHexValue, CInt(colorRgbHexValue.Insert(0, "&h")))
    End Function

    Private Sub lblAchievement_MouseMove(sender As Object, e As MouseEventArgs) Handles lblAchievement.MouseMove
        lblAchievement.Cursor = Cursors.Hand
    End Sub

    Private Sub lblAchievement_MouseLeave(sender As Object, e As EventArgs) Handles lblAchievement.MouseLeave
        lblAchievement.Cursor = Cursors.Default
    End Sub

    Private Sub lblAchievement_Click(sender As Object, e As EventArgs) Handles lblAchievement.Click
        Process.Start("http://link.bilibili.com/p/center/index#/user-center/achievement/achievement-normal")
    End Sub

    Private Sub RadioButton_MouseMove(sender As Object, e As MouseEventArgs) Handles rdbtnCyan.MouseMove, rdbtnPink.MouseMove, rdbtnOrange.MouseMove, rdbtnYellow.MouseMove, rdbtnGreen.MouseMove, rdbtnPurple.MouseMove, rdbtnBlue.MouseMove, rdbtnRed.MouseMove, rdbtnBlack.MouseMove
        Dim rdbtn = DirectCast(sender, RadioButton)
        If rdbtn IsNot Nothing Then
            rdbtn.Font = New Font(rdbtn.Font.Name, 15.0F)
            Dim centerPointY = lblCenterLine.Location.Y - rdbtn.Height \ 2
            rdbtn.Location = New Point(rdbtn.Location.X, centerPointY)
            rdbtn.Cursor = Cursors.Hand

            Dim tips = ToolTip1.GetToolTip(rdbtn)
            If tips.Length > 0 Then Return

            tips = rdbtn.Name.Substring(NameOf(rdbtn).Length)
            Dim displayPoint = New Point(0, rdbtn.Cursor.Size.Height)
            ToolTip1.Show(tips, rdbtn, displayPoint)
        End If
    End Sub

    Private Sub RadioButton_MouseLeave(sender As Object, e As EventArgs) Handles rdbtnCyan.MouseLeave, rdbtnPink.MouseLeave, rdbtnOrange.MouseLeave, rdbtnYellow.MouseLeave, rdbtnGreen.MouseLeave, rdbtnPurple.MouseLeave, rdbtnBlue.MouseLeave, rdbtnRed.MouseLeave, rdbtnBlack.MouseLeave
        Dim rdbtn = DirectCast(sender, RadioButton)
        If rdbtn IsNot Nothing Then
            rdbtn.Font = New Font(rdbtn.Font.OriginalFontName, 12.0F)
            Dim centerPointY = lblCenterLine.Location.Y - rdbtn.Height \ 2
            rdbtn.Location = New Point(rdbtn.Location.X, centerPointY)
            rdbtn.Cursor = Cursors.Default

            ToolTip1.RemoveAll()
        End If
    End Sub

	Private Async Sub RadioButton_ClickAsync(sender As Object, e As EventArgs) Handles rdbtnYellow.Click, rdbtnRed.Click, rdbtnPurple.Click, rdbtnPink.Click, rdbtnOrange.Click, rdbtnGreen.Click, rdbtnCyan.Click, rdbtnBlue.Click, rdbtnBlack.Click
		Dim rdbtn = DirectCast(sender, RadioButton)
		If rdbtn IsNot Nothing Then
			Dim colorRgb = ParseColorRgbValue(rdbtn.ForeColor)

			If m_DanmuColorDec = colorRgb.Dec Then
				' 一样的话不需要再更改
				'Windows2.DrawTipsTask(Me, rdbtn.Text & " 设置成功" & RandomEmoji.Happy, 2000, True, False)
			Else
				Dim changeRst = Await DanmuSender.ChangeDanmuColorAsync(colorRgb.Dec)
				Windows2.DrawTipsTask(Me, If(changeRst.Success, changeRst.Message.Replace("~", RandomEmoji.Happy), changeRst.Message & RandomEmoji.Shock), 2000, changeRst.Success, False)
				If changeRst.Success Then
					m_LastRadioButton = rdbtn
					m_DanmuColorDec = colorRgb.Dec
				Else
					' 设置为上一个选中的
					m_LastRadioButton.Checked = True
				End If
			End If
		End If
	End Sub
End Class