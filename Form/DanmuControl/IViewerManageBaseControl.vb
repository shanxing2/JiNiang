Public Interface IViewerManageBase
    ''' <summary>
    ''' 操作列，可根据需要定义，比如 删除/撤销等
    ''' </summary>
    ''' <returns></returns>
    Property OperateColumn As DataGridViewButtonColumn
	''' <summary>
	''' 加载数据
	''' </summary>
	''' <returns></returns>
	Function LoadDataAsync() As Task
	''' <summary>
	''' 重新加载数据
	''' </summary>
	Sub ReLoadDataTask()
	''' <summary>
	''' 显示操作结果
	''' </summary>
	''' <param name="success"></param>
	Sub ShowOperateResultTask(ByVal success As Boolean)
	''' <summary>
	''' 显示操作结果
	''' </summary>
	''' <param name="success"></param>
	''' <param name="tips">提示</param>
	''' <param name="timeout">倒计时，单位毫秒</param>
	Sub ShowOperateResultTask(ByVal success As Boolean, ByVal tips As String, Optional ByVal timeout As Integer = 1000)
	''' <summary>
	''' 生成操作列<see cref="OperateColumn"/>
	''' </summary>
	''' <param name="cellValue"></param>
	''' <param name="displayIndex"></param>
	Sub MakeOperateColumn(ByVal cellValue As Object, ByVal displayIndex As Integer)
End Interface
