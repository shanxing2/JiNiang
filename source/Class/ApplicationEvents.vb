Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.Devices
Imports ShanXingTech

Namespace My
    ' 以下事件可用于 MyApplication: 
    ' Startup:应用程序启动时在创建启动窗体之前引发。
    ' Shutdown:在关闭所有应用程序窗体后引发。如果应用程序非正常终止，则不会引发此事件。
    ' UnhandledException:在应用程序遇到未经处理的异常时引发。
    ' StartupNextInstance:在启动单实例应用程序且应用程序已处于活动状态时引发。 
    ' NetworkAvailabilityChanged:在连接或断开网络连接时引发。
    ' 注：Release版才有效
    Partial Friend Class MyApplication
        Private Sub MyApplication_UnhandledException(sender As Object, e As UnhandledExceptionEventArgs) Handles Me.UnhandledException
            If Not DanmuEntry.IsClosed Then
                Try
                    DanmuEntry.Close(FrmMain.DanmuControl)
                Catch ex As Exception
                    Logger.WriteLine(ex)
                End Try
            End If

            Logger.WriteLine(e.Exception)
            MessageBox.Show("程序遇到未处理异常，请关闭程序后重启程序", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Sub
    End Class
End Namespace
