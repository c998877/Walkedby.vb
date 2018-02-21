Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Math
Imports System.TimeZoneInfo
Imports Microsoft
Imports System.Text
Imports System.ComponentModel
Imports System.Web
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Net
Imports System.Net.Mail
Imports System.Security.Permissions
Imports Microsoft.Win32
Imports System.Xml

Public Class 在线IP地址  '一个用来获得 IP 的蜜汁库

    Public 公网IP As String
    Public 地理位置 As String
    Public 原文 As String

    Public Sub New()
        刷新()
    End Sub

    Public Sub 刷新()
        原文 = 获得Http("http://ip.chinaz.com/getip.aspx", "{ip:'0.0.0.0',address:'获取失败'}")
        公网IP = 正则提取(原文, "ip:'", "',")
        地理位置 = 正则提取(原文, "address:'", "'}")
    End Sub

End Class
