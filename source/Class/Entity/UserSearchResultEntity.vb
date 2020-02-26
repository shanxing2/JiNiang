Namespace UserSearchResultEntity
#Disable Warning ide1006
	Public Class Cost_time
		''' <summary>
		''' 
		''' </summary>
		Public Property params_check() As String
		'''' <summary>
		'''' 
		'''' </summary>
		''''public string get upuser live status { get; set; }
		'''' <summary>
		'''' 
		'''' </summary>
		Public Property illegal_handler() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property as_response_format() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property as_request() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property deserialize_response() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property as_request_format() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property total() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property main_handler() As String
	End Class

	Public Class Exp_list
		'''' <summary>
		'''' 
		'''' </summary>
		''''public bool 6615 { get; set; }
	End Class

	Public Class Official_verify
		''' <summary>
		''' 
		''' </summary>
		Public Property type() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property desc() As String
	End Class

	Public Class Res
		''' <summary>
		''' 
		''' </summary>
		Public Property play() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property dm() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property pubdate() As Integer
		''' <summary>
		''' Xbox金会员的退订方式
		''' </summary>
		Public Property title() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property pic() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property fav() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property is_union_video() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property is_pay() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property duration() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property aid() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property coin() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property arcurl() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property desc() As String
	End Class

	Public Class Result
		''' <summary>
		''' 
		''' </summary>
		Public Property rank_offset() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property usign() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property videos() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property fans() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property is_upuser() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property upic() As String
		''' <summary>
		''' 哇_JK
		''' </summary>
		Public Property uname() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property official_verify() As Official_verify
		''' <summary>
		''' 
		''' </summary>
		Public Property verify_info() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property rank_score() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property level() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property gender() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property hit_columns() As List(Of String)
		''' <summary>
		''' 
		''' </summary>
		Public Property mid() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property is_live() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property room_id() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property res() As List(Of Res)
		''' <summary>
		''' 
		''' </summary>
		Public Property rank_index() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property type() As String
	End Class

	Public Class Data
		''' <summary>
		''' 
		''' </summary>
		Public Property seid() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property page() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property pagesize() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property numResults() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property numPages() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property suggest_keyword() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property rqt_type() As String
		''' <summary>
		''' 
		''' </summary>
		Public Property cost_time() As Cost_time
		''' <summary>
		''' 
		''' </summary>
		Public Property exp_list() As Exp_list
		''' <summary>
		''' 
		''' </summary>
		Public Property egg_hit() As Integer
		''' <summary>
		''' 
		''' </summary>
		Public Property result() As List(Of Result)
		''' <summary>
		''' 
		''' </summary>
		Public Property show_column() As Integer
	End Class

	Public Class Root
		Inherits APIPostResponseBaseEntity.Root
		''' <summary>
		''' 
		''' </summary>
		Public Property data() As Data
	End Class
#Enable Warning ide1006
End Namespace


