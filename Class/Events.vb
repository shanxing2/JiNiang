''' <summary>
''' 各种事件，包括弹幕，送礼，人气等
''' </summary>
Public Class Events
    Public Class UserEnsuredEventArgs
        Inherits EventArgs

        Public ReadOnly Property MainForm As MainFormEntity
        Public ReadOnly Property LoginResult As LoginResult

        Public Sub New(ByVal mainForm As MainFormEntity, ByVal loginResult As LoginResult)
            Me.MainForm = mainForm
            Me.LoginResult = loginResult
        End Sub
    End Class
End Class
