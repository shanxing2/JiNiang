Imports System.ComponentModel
''' <summary>
''' 用户角色
''' </summary>
Public Enum UserRole
    ''' <summary>
    ''' 未知
    ''' </summary>
    <Description("未知")>
    UnKwon
    ''' <summary>
    ''' 观众（登录）
    ''' </summary>
    <Description("观众")>
    Viewer
    ''' <summary>
    ''' 主播
    ''' </summary>
    <Description("主播")>
    Uper
    ''' <summary>
    ''' 游客（未登录）
    ''' </summary>
    <Description("游客")>
    Visitor
End Enum