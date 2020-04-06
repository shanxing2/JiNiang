Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports ShanXingTech
Imports ShanXingTech.Text2
Imports 姬娘插件.Events

Public Class FrmSelectArea
#Region "字段区"
	Private m_ParentAreaDic As Dictionary(Of Integer, AreaInfo())
	Private m_Liver As LightLiver
	Private m_GotChooseArea As Boolean
	Private m_CurrentParentAreaId As Integer
#End Region

#Region "属性区"
	Private m_SelectArea As AreaInfo
	''' <summary>
	''' 当前选中的颜文字
	''' </summary>
	''' <returns></returns>
	Public ReadOnly Property SelectArea() As AreaInfo
		Get
			Return m_SelectArea
		End Get
	End Property
#End Region

#Region "构造函数区"
	Public Sub New()

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。

	End Sub

	Public Sub New(ByRef liver As LightLiver)

		' 此调用是设计器所必需的。
		InitializeComponent()

		' 在 InitializeComponent() 调用之后添加任何初始化。
		m_Liver = liver

		Me.SuspendLayout()

		Me.AutoSize = True
		Me.DoubleBuffered = True

		flpChildAreaContainer.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
		flpChildAreaContainer.AutoSize = True

		' 添加最近使用过的分区
		m_GotChooseArea = True
		Dim getChooseAreaTask = ShowChooseAreaAsync()
		Dim getParentAreaTask = m_Liver.GetAreaListAsync()

		Dim taskAll = Task.WhenAll(getChooseAreaTask, getParentAreaTask)
		taskAll.ContinueWith(
			Sub(taskCompletion As Task)
				' ###########################################################
				' # 在ContinueWith块中，调试命中断点的情况时，Intellisense无法正常工作
				' # 表现为 鼠标停留到变量上时返回的是变量的默认值而不是实时值
				' # 自动窗口&局部变量窗口&即时窗口工作正常
				' ###########################################################                
				m_ParentAreaDic = getParentAreaTask.Result

				' 添加主分区
				Dim firstParentArea = ShowParentArea(m_ParentAreaDic)

				' 默认选中 第一个主分区
				If tlpParentAreaContainer.InvokeRequired Then
					tlpParentAreaContainer.Parent.Invoke(Sub() ShowChildArea(firstParentArea))
				Else
					ShowChildArea(firstParentArea)
				End If
			End Sub, TaskContinuationOptions.None)

		txtSearchAreaQuickly.SetCueBanner("输入拼音首字母或全称，快速搜索")

		Me.PerformLayout()
	End Sub
#End Region

	''' <summary>
	''' 显示最近选择过的分区
	''' </summary>
	''' <returns></returns>
	Private Async Function ShowChooseAreaAsync() As Task
		'Dim yantextArr As String() = {"娱乐 · 学习", "游戏 · 英雄联盟", "娱乐 · 催眠电台", "娱乐 · 学习2", "游戏 · 英雄联盟2", "娱乐 · 催眠电台2"}
		If flpChooseAreaContainer.InvokeRequired Then
			flpChooseAreaContainer.Parent.BeginInvoke(
				Sub()
					flpChooseAreaContainer.Controls.Clear()
					flpChooseAreaContainer.AutoScroll = True
				End Sub)
		Else
			flpChooseAreaContainer.Controls.Clear()
			flpChooseAreaContainer.AutoScroll = True
		End If

		Dim lbl As New Label With {
				.Text = "最近选择：",
				.AutoSize = True,
				.Margin = New Padding(3, 3, 3, 3)
			}
		flpChooseAreaContainer.Controls.Add(lbl)

		Dim areas = Await m_Liver.GetChoosedAreaAsync()
		Dim list As New List(Of Label)
		For Each area In areas
			Dim label = New Label With {
				.Text = m_Liver.GetAreaFullName(area),
				.AutoSize = True,
				.Margin = New Padding(3, 3, 3, 3),
				.Tag = area.Id
			}

			' 高亮当前分区
			If label.Text = m_Liver.GetAreaFullName(m_Liver.Room.Area) Then
				label.ForeColor = SystemColors.HotTrack
			End If

			AddHandler label.MouseMove,
				Sub(sender2, e2)
					label.ForeColor = SystemColors.Highlight
					label.Cursor = Cursors.Hand
				End Sub
			RemoveHandler label.MouseMove, New MouseEventHandler(Sub()
																 End Sub)

			AddHandler label.MouseLeave,
				Sub(sender2, e2)
					label.ForeColor = If(label.Text = m_Liver.GetAreaFullName(m_Liver.Room.Area), SystemColors.HotTrack, Color.Black)
					label.Cursor = Cursors.Default
				End Sub
			RemoveHandler label.MouseLeave, New EventHandler(Sub()
															 End Sub)

			AddHandler label.Click,
				Sub(sender2, e2)
					Dim childAreaId = CStr(label.Tag)
					m_SelectArea = GetAreaInfo(childAreaId)
					Me.Visible = False
					Me.TopMost = False
				End Sub
			RemoveHandler label.Click, New EventHandler(Sub()
														End Sub)

			list.Add(label)
		Next

		flpChooseAreaContainer.Controls.AddRange(list.ToArray)
	End Function

	''' <summary>
	''' 根据子分区Id获取对应的分区信息
	''' </summary>
	''' <param name="childAreaId"></param>
	''' <returns></returns>
	Public Function GetAreaInfo(ByVal childAreaId As String) As AreaInfo
		Dim area As AreaInfo = Nothing
		'For Each pA In m_ParentAreaDic.AsParallel
		'	area = pA.Value.FirstOrDefault(Function(ar) childAreaId = ar.Id)
		'	If area Is Nothing Then
		'		Continue For
		'	Else
		'		Exit For
		'	End If
		'Next
		For Each prtArea In m_ParentAreaDic.AsParallel
			area = prtArea.Value.FirstOrDefault(Function(ar) childAreaId = ar.Id)
			If area Is Nothing Then
				Continue For
			Else
				' 确保选中的子分区的主分区成为当前分区
				If m_CurrentParentAreaId <> prtArea.Key Then
					Dim parentAreaLable = TryCast(tlpParentAreaContainer.Controls(prtArea.Value(0).ParentName), Label)
					If parentAreaLable IsNot Nothing Then
						ShowChildArea(parentAreaLable)
					End If
				End If

				Exit For
			End If
		Next

		Return area
	End Function

	''' <summary>
	''' 显示主分区
	''' </summary>
	''' <param name="areaDic"></param>
	''' <returns></returns>
	Private Function ShowParentArea(ByVal areaDic As Dictionary(Of Integer, AreaInfo())) As Label
		tlpParentAreaContainer.ColumnCount = areaDic.Count
		AdjustColumnStyle()

		Dim labelWidth = tlpParentAreaContainer.Width \ tlpParentAreaContainer.ColumnCount

		Dim firstLabel As New Label
		For Each parentArea In areaDic
			Dim label = New Label With {
				.Name = parentArea.Value(0).ParentName,
				.AutoSize = False,
				.Width = labelWidth,
				.AutoEllipsis = True,
				.Text = parentArea.Value(0).ParentName,
				.Dock = DockStyle.Fill,
				.FlatStyle = FlatStyle.Flat,
				.BorderStyle = BorderStyle.FixedSingle,
				.TextAlign = ContentAlignment.MiddleCenter,
				.Margin = New Padding(0, 0, 0, 0),
				.Tag = parentArea.Value(0).ParentId
			}

			If tlpParentAreaContainer.InvokeRequired Then
				tlpParentAreaContainer.Parent.Invoke(
					Sub()
						tlpParentAreaContainer.Controls.Add(label)
						If tlpParentAreaContainer.Controls.Count = 1 Then
							firstLabel = label
						End If
					End Sub)
			Else
				tlpParentAreaContainer.Controls.Add(label)
				If tlpParentAreaContainer.Controls.Count = 1 Then
					firstLabel = label
				End If
			End If
			AddHandler label.MouseMove,
				Sub(sender2, e2)
					label.Cursor = Cursors.Hand
				End Sub

			AddHandler label.MouseLeave,
				Sub(sender2, e2)
					label.Cursor = Cursors.Default
				End Sub

			' 点击主分区
			AddHandler label.Click,
				Sub(sender2, e2)
					flpChildAreaContainer.SuspendLayout()
					Me.SuspendLayout()
					ShowChildArea(label)
					flpChildAreaContainer.ResumeLayout(False)
					flpChildAreaContainer.PerformLayout()
					Me.ResumeLayout(False)
					Me.PerformLayout()
				End Sub
		Next

		Return firstLabel
	End Function

	Private Sub AdjustColumnStyle()
		Dim columnStylesCount = tlpParentAreaContainer.ColumnStyles.Count
		For i = 0 To columnStylesCount - 1
			tlpParentAreaContainer.ColumnStyles(i).SizeType = SizeType.AutoSize
		Next
		Dim insertCount = tlpParentAreaContainer.ColumnCount - columnStylesCount
		If insertCount > 0 Then
			If insertCount = 1 Then
				tlpParentAreaContainer.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
			Else
				For i = 0 To insertCount - 1
					tlpParentAreaContainer.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))
				Next
			End If
		End If
	End Sub

	''' <summary>
	''' 显示子分区
	''' </summary>
	''' <param name="parentAreaLabel"></param>
	Private Sub ShowChildArea(ByVal parentAreaLabel As Label)
		' 选中的 主分区 字体颜色为  SystemColors.Highlight，其他为 Color.Black
		CancelHighlightParentAreaLabel()
		parentAreaLabel.ForeColor = SystemColors.Highlight

		' 显示子分区
		m_CurrentParentAreaId = CInt(parentAreaLabel.Tag)
		ShowChildAreaInternal(m_ParentAreaDic(m_CurrentParentAreaId))
	End Sub

	Private Sub ClearChildArea()
		If flpChildAreaContainer.InvokeRequired Then
			flpChildAreaContainer.Parent.Invoke(
				Sub()
					flpChildAreaContainer.Controls.Clear()
					flpChildAreaContainer.AutoScroll = True
				End Sub)
		Else
			flpChildAreaContainer.Controls.Clear()
			flpChildAreaContainer.AutoScroll = True
		End If
	End Sub

	Private Sub ShowChildAreaInternal(ByVal childAreas As AreaInfo())
		'Dim yantextArr As String() = {"娱乐 · 学习", "游戏 · 英雄联盟", " 娱乐 · 催眠电台", "娱乐 · 学习2", "游戏 · 英雄联盟2", " 娱乐 · 催眠电台2"}
		ClearChildArea()

		Dim lblArr(childAreas.Length - 1) As Label
		Dim index = 0
		For Each area In childAreas
			lblArr(index) = CreateChildAreaLabel(area)
			index += 1
		Next

		If flpChildAreaContainer.InvokeRequired Then
			flpChildAreaContainer.Parent.Invoke(Sub() flpChildAreaContainer.Controls.AddRange(lblArr))
		Else
			flpChildAreaContainer.Controls.AddRange(lblArr)
		End If
	End Sub

	Private Sub CancelHighlightParentAreaLabel()
		For Each ctrl As Control In tlpParentAreaContainer.Controls
			Dim lbl = TryCast(ctrl, Label)
			If lbl Is Nothing Then Continue For

			lbl.ForeColor = Color.Black
		Next
	End Sub

	Private Sub FrmYanText_Load(sender As Object, e As EventArgs) Handles MyBase.Load
		If Not m_GotChooseArea Then
#Disable Warning BC42358 ' 在调用完成之前，会继续执行当前方法，原因是此调用不处于等待状态
			'ShowChooseAreaAsync("4236342")
			ShowChooseAreaAsync()
#Enable Warning BC42358 ' 在调用完成之前，会继续执行当前方法，原因是此调用不处于等待状态
		End If

		m_SelectArea = New AreaInfo
	End Sub

	Private Sub FrmSelectArea_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
		m_GotChooseArea = False
	End Sub

	Private Sub txtSearchQuickly_TextChanged(sender As Object, e As EventArgs) Handles txtSearchAreaQuickly.TextChanged
		Dim keyword = txtSearchAreaQuickly.Text.Trim
		If keyword.Length = 1 Then
			ShowChildAreaSearchQuickly(Regex.Escape(keyword))
		ElseIf keyword.Length > 1 Then
			' 可拆分有序模糊匹配
			Dim sb = StringBuilderCache.Acquire(66)
			Dim all = ".*?"
			sb.Append(all)
			For i = 0 To keyword.Length - 1
				sb.Append(Regex.Escape(keyword(i))).Append(all)
			Next
			Dim pattern = StringBuilderCache.GetStringAndReleaseBuilder(sb)
			ShowChildAreaSearchQuickly(pattern)
		Else
			ShowChildAreaInternal(m_ParentAreaDic(m_CurrentParentAreaId))
		End If
	End Sub

	Private Sub ShowChildAreaSearchQuickly(ByVal pattern As String)
		'Dim yantextArr As String() = {"娱乐 · 学习", "游戏 · 英雄联盟", " 娱乐 · 催眠电台", "娱乐 · 学习2", "游戏 · 英雄联盟2", " 娱乐 · 催眠电台2"}
		ClearChildArea()

		Dim lblArr(8) As Label
		Dim index = 0

		For Each parentArea In m_ParentAreaDic
			For Each area In parentArea.Value
				' 没有表达式就是加载全部子分区
				' 有表达式的话，满足 拼音或名称 就加载
				Dim isMatch = pattern.IsNullOrEmpty OrElse
					Regex.IsMatch(area.PinYin & area.Name, pattern, RegexOptions.IgnoreCase Or RegexOptions.Compiled)
				If Not isMatch Then
					Continue For
				End If

				lblArr(index) = CreateChildAreaLabel(area)
				index += 1

				If index >= lblArr.Length Then
					ReDim Preserve lblArr(lblArr.Length * 2)
				End If
			Next
		Next

		If flpChildAreaContainer.InvokeRequired Then
			flpChildAreaContainer.Parent.Invoke(Sub() flpChildAreaContainer.Controls.AddRange(lblArr))
		Else
			flpChildAreaContainer.Controls.AddRange(lblArr)
		End If
	End Sub

	Private Function CreateChildAreaLabel(ByRef area As AreaInfo) As Label
		Dim label = New Label With {
			.Text = area.Name,
			.AutoSize = True,
			.FlatStyle = FlatStyle.Flat,
			.BorderStyle = BorderStyle.FixedSingle,
			.TextAlign = ContentAlignment.MiddleCenter,
			.Margin = New Padding(6, 3, 6, 6),
			.Padding = New Padding(3, 3, 3, 3),
			.Tag = area.Id
		}

		AddHandler label.MouseMove,
			Sub(sender2, e2)
				label.ForeColor = SystemColors.Highlight
				label.Cursor = Cursors.Hand
			End Sub

		AddHandler label.MouseLeave,
			Sub(sender2, e2)
				label.ForeColor = Color.Black
				label.Cursor = Cursors.Default
			End Sub

		AddHandler label.Click,
			Sub(sender2, e2)
				Dim childAreaId = CStr(label.Tag)
				m_SelectArea = GetAreaInfo(childAreaId)

				Me.Visible = False
                Me.TopMost = False
            End Sub

        Return label
    End Function

    Private Sub txtSearchAreaQuickly_MouseHover(sender As Object, e As EventArgs) Handles txtSearchAreaQuickly.MouseHover
        txtSearchAreaQuickly.ShowTips("输入拼音首字母或全称，快速搜索")
    End Sub
End Class