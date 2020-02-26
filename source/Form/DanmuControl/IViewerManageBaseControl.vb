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
	''' 生成操作列<see cref="OperateColumn"/>
	''' </summary>
	''' <param name="cellValue"></param>
	''' <param name="displayIndex"></param>
	Sub MakeOperateColumn(ByVal cellValue As Object, ByVal displayIndex As Integer)
End Interface
