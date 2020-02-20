Imports ShanXingTech.IO2.Database
Imports ShanXingTech
Imports ShanXingTech.Text2
Imports System.Text.RegularExpressions

Public Class DatabaseUpgrader
#Region "字段区"
    Private Shared ReadOnly dataStructureVersionTabelName As String = "DataStructureVersion"
    ''' <summary>
    ''' #12/13/2018PM#
    ''' </summary>
    Private Shared ReadOnly currentVersionTimestamp As Long = CDate("#12/13/2018PM#").ToTimeStamp(TimePrecision.Second)
    'Private Shared ReadOnly currentVersionTimestamp As Long = 1544673600

#End Region

#Region "函数区"

    Public Shared Function TryUpgrade() As Boolean
        Try
            ' 如果不存在版本表，则表示数据结构为旧版，直接更新
            If Not ExsitTable(dataStructureVersionTabelName) Then
                Return Upgrade()
            End If

            Dim versionTimestamp = GetCurrentVersion()
            ' 更新旧版数据结构
            If versionTimestamp Is Nothing OrElse CLng(versionTimestamp) < currentVersionTimestamp Then
                Return Upgrade()
            End If
        Catch ex As Exception
            Logger.WriteLine(ex)
        End Try

        Return True
    End Function

    Private Shared Function ExsitTable(ByVal tableName As String) As Boolean
        Dim sql = $"select name from sqlite_master where name = '{tableName}'"
        Dim existTable = SQLiteHelper.GetFirst(sql)
        Return existTable IsNot Nothing
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="tableName"></param>
    ''' <param name="fieldName"></param>
    ''' <returns></returns>
    Private Shared Function ExsitField(ByVal tableName As String, ByVal fieldName As String) As Boolean
        Dim sql = $"select sql from sqlite_master where name = '{tableName}'"
        Dim createTableSql = SQLiteHelper.GetFirst(sql)
        ' 字段两边可能会有双引号，左右两边可能有0个或者多个空格，左边不可能是一个或者多个字母，
        Dim pattern = $"(?!\w)\s*?""?{fieldName}""?\s+\w"
        Dim match = Regex.Match(createTableSql.ToString, pattern, RegexOptions.IgnoreCase Or RegexOptions.Compiled)

        Return match.Success
    End Function

    Private Shared Function GetCurrentVersion() As Object
        Dim sql = $"select version from {dataStructureVersionTabelName}"
        Return SQLiteHelper.GetFirst(sql)
    End Function

    Private Shared Function Upgrade() As Boolean
        Logger.WriteLine( "更新数据库结构开始")

#Region "构造更新sql"

        Dim sb = StringBuilderCache.Acquire(360)

        If Not ExsitTable("temp_ViewedRoomInfo") Then
            sb.AppendLine("alter table ViewedRoomInfo rename to temp_ViewedRoomInfo;")
        End If

        sb.AppendLine("CREATE TABLE
                        IF NOT EXISTS ViewedRoomInfo (
	                        RealId INTEGER NOT NULL,
	                        ShortId INTEGER,
                            UpName TEXT,	  
	                        LastAttentionCount INTEGER,
                            LastViewedTimestamp INTEGER,
	                        DanmuTop100 TEXT,                           
                            PRIMARY KEY (RealId ASC)
                        );")

        ' 更新 ViewedRoomInfo表（旧版数据库存在Id字段）
        If ExsitField("ViewedRoomInfo", "id") Then
            sb.AppendLine("INSERT INTO ViewedRoomInfo (
	                        RealId,
	                        ShortId,
	                        UpName,
	                        LastAttentionCount,
	                        LastViewedTimestamp,
	                        DanmuTop100
                        ) SELECT
	                        id,
	                        '',
	                        UpName,
	                        LastAttentionCount,
	                        LastViewedTimeStamp,
	                        DanmuTop100
                        FROM
	                        temp_ViewedRoomInfo;")
        End If
        sb.AppendLine("drop table IF EXISTS temp_ViewedRoomInfo;")

        If Not ExsitTable("temp_UserInfo") Then
            sb.AppendLine("alter table UserInfo rename to temp_UserInfo;")
        End If
        sb.AppendLine("CREATE TABLE
                        IF NOT EXISTS UserInfo (
	                        Id INTEGER NOT NULL,
	                        Nick TEXT NOT NULL,
	                        Cookies TEXT NOT NULL,
	                        SignDate TEXT,
                            SignRewards TEXT,
                            StoreCookies INTEGER,
                            LastViewedRoomRealId INTEGER,
                            RoomShortId INTEGER,
                            RoomRealId INTEGER,
                            PRIMARY KEY (Id ASC)
                        );")
        ' 更新 UserInfo(旧版 数据库不存在RoomShortId字段)
        If Not ExsitField("UserInfo", "RoomShortId") Then
            sb.AppendLine("INSERT INTO UserInfo (
	                        Id,
	                        Nick,
	                        Cookies,
	                        SignDate,
	                        SignRewards,
	                        StoreCookies,
	                        LastViewedRoomRealId,
	                        RoomShortId,
	                        RoomRealId
                        ) SELECT
	                        Id,
	                        Nick,
	                        Cookies,
	                        SignDate,
	                        SignRewards,
	                        StoreCookies,
	                        LastViewedRoomId,
	                        '',
	                        ''
                        FROM
	                        temp_UserInfo;")
        End If
        sb.AppendLine("DROP TABLE IF EXISTS temp_UserInfo;")

        ' 存储版本信息
        sb.AppendLine($"INSERT OR REPLACE INTO DataStructureVersion (Version) VALUES ({currentVersionTimestamp.ToStringOfCulture});")

        Dim sql = StringBuilderCache.GetStringAndReleaseBuilder(sb)
#End Region
        Dim upgradeRst = SQLiteHelper.ExecuteNonQuery(sql)

        Logger.WriteLine( "更新数据库结构完成")

        Return upgradeRst
    End Function
#End Region
End Class
