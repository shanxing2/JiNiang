Namespace ViewerMedalEntity
    Public Class Data

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property guard_type As Integer

        ''' <summary>
        ''' 当前等级积累亲密度（升级后清0）
        ''' </summary>
        ''' <returns></returns>
        Public Property intimacy As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property is_receive As Integer

        ''' <summary>
        ''' 上次佩戴时间戳（10位）
        ''' </summary>
        ''' <returns></returns>
        Public Property last_wear_time As Long

        ''' <summary>
        ''' 勋章等级
        ''' </summary>
        ''' <returns></returns>
        Public Property level As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property lpl_status As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property master_available As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property master_status As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property medal_id As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property medal_name As String

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property receive_channel As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property receive_time As String

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property source As Integer

        ''' <summary>
        ''' 勋章佩戴状态（0取下，1佩戴）
        ''' </summary>
        ''' <returns></returns>
        Public Property status As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property target_id As Integer

        ''' <summary>
        ''' 当日增长亲密度
        ''' </summary>
        ''' <returns></returns>
        Public Property today_intimacy As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property uid As Integer

        ''' <summary>
        ''' 两仪滚
        ''' </summary>
        ''' <returns></returns>
        Public Property target_name As String

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property target_face As String

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property live_stream_status As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property icon_code As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property icon_text As String

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property rank As String

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property medal_color As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property today_feed As Integer

        ''' <summary>
        ''' 当日限制（最多）增长亲密度
        ''' </summary>
        ''' <returns></returns>
        Public Property day_limit As Integer

        ''' <summary>
        ''' 下一级所需亲密度
        ''' </summary>
        ''' <returns></returns>
        Public Property next_intimacy As Integer

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property score As Integer

    End Class


    Public Class Root
        Inherits APIPostResponseBaseEntity.Root
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Property data As Data
    End Class
End Namespace
