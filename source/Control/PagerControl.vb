Imports ShanXingTech
''' <summary>
''' 分页器
''' </summary>
Public Class PagerControl
#Region "事件类"
    Public Class NavigatePageEventArgs
        Inherits EventArgs

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="navigatePage">需要导航到的页码</param>
        Public Sub New(navigatePage As Integer)
            Me.NavigatePage = navigatePage
        End Sub

        ''' <summary>
        ''' 需要导航到的页码
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property NavigatePage As Integer
    End Class
#End Region

#Region "字段区"


#End Region
#Region "属性区"
    Private m_TotalPage As Integer
    ''' <summary>
    ''' 数据总页数
    ''' </summary>
    Public ReadOnly Property TotalPage As Integer
        Get
            Return m_TotalPage
        End Get
    End Property

    Private m_TotalCount As Long
    ''' <summary>
    ''' 已有数据总数
    ''' </summary>
    Public ReadOnly Property TotalCount As Long
        Get
            Return m_TotalCount
        End Get
    End Property

    ''' <summary>
    ''' 用户设置的旧页码值（翻页之后就是最新的页码值）
    ''' </summary>
    Private m_OldPageSize As Integer
    Public ReadOnly Property PageSize As Integer
        Get
            Return m_OldPageSize
        End Get
    End Property

    ''' <summary>
    ''' 是否可以调整页大小
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CanAdjustPageSize As Boolean

    ''' <summary>
    ''' 当前浏览页数
    ''' </summary>
    Private m_CurrentPage As Integer
    Public ReadOnly Property CurrentPage As Integer
        Get
            Return m_CurrentPage
        End Get
    End Property
#End Region

#Region "事件区"
    ''' <summary>
    ''' 页大小发生变化时引发此事件
    ''' </summary>
    Public Event PageSizeChanged As EventHandler
    ''' <summary>
    ''' 导航到某页时引发此事件
    ''' </summary>
    Public Event PageNavigated As EventHandler(Of NavigatePageEventArgs)

#End Region

#Region "构造函数"
    Sub New(ByVal totalPage As Integer, ByVal totalCount As Integer, ByVal pageSize As Integer, ByVal canAdjustPageSize As Boolean)

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。
        Me.Margin = New Padding(0)

        m_TotalPage = totalPage
        m_TotalCount = totalCount
        Me.CanAdjustPageSize = canAdjustPageSize

        nudPageSize.Enabled = canAdjustPageSize
        nudPageSize.Value = pageSize
        nudPageSize.Maximum = 100
        nudPageSize.Minimum = 1

        m_CurrentPage = 1
        m_OldPageSize = pageSize
    End Sub
#End Region

#Region "函数区"
    Private Sub btnGotoPage_Click(sender As Object, e As EventArgs) Handles btnGotoPreviousPage.Click, btnGotoNextPage.Click, btnGotoLastPage.Click, btnGotoFirstPage.Click
        Dim btn = DirectCast(sender, Button)
        If btn Is Nothing Then Return

        ' 如果页大小改变，则直接去第一页，不需要再执行后续翻页判断及相应操作
        If m_OldPageSize <> nudPageSize.Value.ToIntegerOfCulture Then
            m_CurrentPage = 1
            m_OldPageSize = nudPageSize.Value.ToIntegerOfCulture
            RaiseEvent PageSizeChanged(Me, New EventArgs)
            Return
        End If

        Dim oldCurrentPage = m_CurrentPage
        Select Case btn.Name
            Case btnGotoFirstPage.Name
                If CanGotoPreviousPage() Then
                    m_CurrentPage = 1
                End If
            Case btnGotoPreviousPage.Name
                If CanGotoPreviousPage() Then
                    m_CurrentPage -= 1
                End If
            Case btnGotoNextPage.Name
                If CanGotoNextPage() Then
                    m_CurrentPage += 1
                End If
            Case btnGotoLastPage.Name
                If CanGotoNextPage() Then
                    m_CurrentPage = TotalPage
                End If
        End Select

        If m_CurrentPage = oldCurrentPage Then Return

        RaiseEvent PageNavigated(Me, New NavigatePageEventArgs(m_CurrentPage))
        OnDataShowChange()
    End Sub

    ''' <summary>
    ''' 可以往前翻页
    ''' </summary>
    ''' <returns></returns>
    Private Function CanGotoPreviousPage() As Boolean
        If m_CurrentPage = 1 Then
            Common.ShowOperateResultTask(Me.Parent, False, "已到第一页")
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' 可以往后翻页
    ''' </summary>
    ''' <returns></returns>
    Private Function CanGotoNextPage() As Boolean
        If m_CurrentPage >= TotalPage Then
            Common.ShowOperateResultTask(Me.Parent, False, "已到最后一页")
            Return False
        Else
            m_OldPageSize = nudPageSize.Value.ToIntegerOfCulture
            Return True
        End If
    End Function

    Private Sub NudPageSize_KeyUp(sender As Object, e As KeyEventArgs) Handles nudPageSize.KeyUp
        If e.KeyCode <> Keys.Enter Then Return

        OnPageSizeChange()
    End Sub

    Private Sub nudPageSize_ValueChanged(sender As Object, e As EventArgs) Handles nudPageSize.ValueChanged
        OnPageSizeChange()
    End Sub

    Private Sub OnPageSizeChange()
        ' 更改页大小之后，用新的页大小重新加载数据
        If m_OldPageSize = nudPageSize.Value.ToIntegerOfCulture Then Return

        m_OldPageSize = nudPageSize.Value.ToIntegerOfCulture
        m_CurrentPage = 1
        Dim hasMod = (TotalCount Mod m_OldPageSize > 0)
        m_TotalPage = CInt(TotalCount \ m_OldPageSize + If(hasMod, 1, 0))
        RaiseEvent PageSizeChanged(Me, New EventArgs)
        OnDataShowChange()
    End Sub

    ''' <summary>
    ''' 数据改变后调用
    ''' </summary>
    Private Sub OnDataShowChange()
        lblPageInfo.Text = $"{m_CurrentPage.ToStringOfCulture}/{m_TotalPage.ToStringOfCulture}"
        lblTotalCount.Text = $"{m_TotalCount.ToStringOfCulture}：总数"
    End Sub

    ''' <summary>
    ''' 数据增加后调用
    ''' </summary>
    ''' <param name="addedCount">增加的数据个数</param>
    Public Sub OnDataAdd(ByVal addedCount As Integer)
        ReSet(m_TotalCount + addedCount)

    End Sub

    ''' <summary>
    ''' 数据被删除后调用
    ''' </summary>
    ''' <param name="deletedCount">被删除的数据个数</param>
    Public Sub OnDataDelete(ByVal deletedCount As Integer)
        Dim tempTotalPage = m_TotalPage
        'Dim tempCurrentPage = m_CurrentPage

        ReSet(m_TotalCount - deletedCount)

        If m_TotalCount = 0 Then
            Return
        End If

        If m_OldPageSize = 1 AndAlso m_CurrentPage > 1 Then
            btnGotoPreviousPage.PerformClick()
            Return
        End If

        ' 如果删除了某页所有数据，那么需要往前翻页（如果可以往前翻页）
        If tempTotalPage <> m_TotalPage AndAlso
           tempTotalPage > 1 Then
            RaiseEvent PageNavigated(Me, New NavigatePageEventArgs(m_CurrentPage))
        End If
    End Sub

    ''' <summary>
    ''' 重新设置数据总数
    ''' </summary>
    ''' <param name="totalCount">总数</param>
    Public Sub ReSet(ByVal totalCount As Long)
        If totalCount <= 0 Then
            m_TotalPage = 0
            m_CurrentPage = 0
        Else
            Dim tempTotalPage = m_TotalPage
            Dim hasMod = (totalCount Mod m_OldPageSize > 0)
            m_TotalPage = CInt(totalCount \ m_OldPageSize + If(hasMod, 1, 0))
            If tempTotalPage - m_TotalPage = 1 AndAlso hasMod Then
                ' 少了一页并且有余数（多分配一页）时，需要往前翻页
                m_CurrentPage -= 1
            End If
            If m_CurrentPage = 0 OrElse m_TotalPage = 1 Then m_CurrentPage = 1
        End If
        m_TotalCount = totalCount

        OnDataShowChange()
    End Sub

    Private Sub nudPageSize_KeyPress(sender As Object, e As KeyPressEventArgs) Handles nudPageSize.KeyPress

    End Sub
#End Region
End Class
