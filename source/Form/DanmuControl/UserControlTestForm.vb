Public Class UserControlTestForm
	'Dim ctrl As New RoomSilentControl2(True, True)
	Dim ctrl2 As New BlackViewerControl(True, True)

	Private Sub UserControlTestForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		'Me.Controls.Add(ctrl)
		Me.Controls.Add(ctrl2)

	End Sub
End Class