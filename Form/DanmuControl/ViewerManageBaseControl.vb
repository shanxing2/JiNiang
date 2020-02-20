Imports ShanXingTech
Imports 姬娘

''' <summary>
''' 观众管理控件基类
''' </summary>
Public Class ViewerManageBaseControl
    Implements IViewerManageBase

    ' Build之后才能正常在UI中使用
#Region "属性区"
    ''' <summary>
    ''' 操作列，可根据需要定义，比如 删除/撤销等
    ''' </summary>
    ''' <returns></returns>
    Public Property OperateColumn As DataGridViewButtonColumn Implements IViewerManageBase.OperateColumn
#End Region

#Region "函数区"
    Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。

    End Sub

    '''' <summary>
    '''' 加载数据，基类会在 Me.Load 事件中自动调用,请在派生类中重写此函数
    '''' </summary>
    '''' <returns></returns>
    Public Overridable Function LoadDataAsync() As Task Implements IViewerManageBase.LoadDataAsync
        Return Nothing
    End Function

    ''' <summary>
    ''' 重新加载数据，内部调用 <see cref="LoadDataAsync()"/>
    ''' </summary>
    Public Sub ReLoadDataTask() Implements IViewerManageBase.ReLoadDataTask
#Disable Warning BC42358 ' 在调用完成之前，会继续执行当前方法，原因是此调用不处于等待状态
        LoadDataAsync()
#Enable Warning BC42358 ' 在调用完成之前，会继续执行当前方法，原因是此调用不处于等待状态
    End Sub

    Private Sub ViewerManageBaseControl_Load(sender As Object, e As EventArgs) Handles Me.Load
#Disable Warning BC42358 ' 在调用完成之前，会继续执行当前方法，原因是此调用不处于等待状态
        LoadDataAsync()
#Enable Warning BC42358 ' 在调用完成之前，会继续执行当前方法，原因是此调用不处于等待状态
    End Sub

    ''' <summary>
    ''' 显示操作结果
    ''' </summary>
    ''' <param name="success"></param>
    Public Sub ShowOperateResultTask(ByVal success As Boolean) Implements IViewerManageBase.ShowOperateResultTask
        Windows2.DrawTipsTask(Me, If(success, "成功", "失败"), 1000, success, False)
    End Sub

	''' <summary>
	''' 显示操作结果
	''' </summary>
	''' <param name="success"></param>
	''' <param name="tips">提示</param>
	''' <param name="timeout">倒计时，单位毫秒</param>
	Public Sub ShowOperateResultTask(ByVal success As Boolean, ByVal tips As String, Optional ByVal timeout As Integer = 1000) Implements IViewerManageBase.ShowOperateResultTask
		Windows2.DrawTipsTask(Me, tips, timeout, success, False)
	End Sub

	''' <summary>
	''' 生成操作列
	''' </summary>
	''' <param name="cellValue">单元格值</param>
	''' <param name="displayIndex">显示位置</param>
	Public Sub MakeOperateColumn(ByVal cellValue As Object, ByVal displayIndex As Integer) Implements IViewerManageBase.MakeOperateColumn
		If OperateColumn Is Nothing Then
			OperateColumn = New DataGridViewButtonColumn()
			Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle() With {
				.Alignment = DataGridViewContentAlignment.MiddleCenter,
				.NullValue = cellValue
			}
			OperateColumn.DefaultCellStyle = DataGridViewCellStyle1
			OperateColumn.HeaderText = "操作"
			OperateColumn.Name = "操作"
		End If

		' 必须每次都设置显示的列索引
		OperateColumn.DisplayIndex = displayIndex
	End Sub
#End Region
End Class
