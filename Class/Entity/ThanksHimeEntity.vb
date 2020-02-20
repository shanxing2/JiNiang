Public Class ThanksHimeEntity
#Region "属性区"
    Public Property DanmuRepeatitiveHandle As DanmuRepeatOptions
#End Region


#Region "构造函数区"
    Sub New()
        ' 默认用合并模式
        Me.DanmuRepeatitiveHandle = DanmuRepeatOptions.Merge
    End Sub
#End Region
End Class