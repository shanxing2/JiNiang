Public Class MainFormEntity
    Private _Location As Point
#Region "属性区"
    Property Opacity As Integer
    Property Location As Point
        Get
			' 处理多个屏幕到一个屏幕转变的情况(横向)
			If _Location.X < 0 OrElse
				_Location.Y < 0 Then
				If Screen.AllScreens.Length = 1 Then
					Dim newX = Math.Abs(_Location.X)
					Dim newY = Math.Abs(_Location.Y)
					EnsureShowInScreen(newX, newY)
					_Location = New Point(newX, newY)
				Else
					EnsureShowInScreen(_Location.X, _Location.Y)
				End If
			Else
				EnsureShowInScreen(_Location.X, _Location.Y)
			End If
			Return _Location
        End Get
        Set
            _Location = Value
        End Set
    End Property

    Property Size As Size
    Property TopMost As Boolean
#End Region

#Region "构造函数区"
    Sub New()
        Opacity = 66
    End Sub
#End Region

#Region "函数区"
	Private Sub EnsureShowInScreen(ByRef x As Integer, ByRef y As Integer)
		' 多屏最左上角
		Dim minPoint As New Point
		' 多屏最右下角
		Dim maxPoint As New Point
		For Each scr In Screen.AllScreens
			If minPoint.X > scr.WorkingArea.X Then minPoint.X = scr.WorkingArea.X
			If minPoint.Y > scr.WorkingArea.Y Then minPoint.Y = scr.WorkingArea.Y

			If maxPoint.X < scr.WorkingArea.Width Then maxPoint.X = scr.WorkingArea.Width
			If maxPoint.Y < scr.WorkingArea.Height Then maxPoint.Y = scr.WorkingArea.Height
		Next
		If x < minPoint.X OrElse x > maxPoint.X Then
			x = 0
		End If
		If y < minPoint.Y OrElse y > maxPoint.Y Then
			y = 0
		End If
	End Sub
#End Region
End Class