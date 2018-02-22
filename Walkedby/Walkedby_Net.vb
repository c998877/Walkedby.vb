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

Public Class IP地址  '获得外网 IP 和地理位置

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

Public Class 网速监测   '一个监测网速的库

    Private card As NetworkInformation.NetworkInterface
    Private LastDown As Long = 0
    Private LastUP As Long = 0
    Public 下载网速 As Long
    Public 上传网速 As Long
    Private th As Thread

    Public Sub New(Optional CardName As String = "")
        下载网速 = 0
        上传网速 = 0
        For Each i As NetworkInformation.NetworkInterface In NetworkInformation.NetworkInterface.GetAllNetworkInterfaces
            If CardName.Length < 1 OrElse 包含(i.Description, CardName) Then
                card = i
                Exit For
            End If
            card = i
        Next
        If Not IsNothing(card) Then
            th = New Thread(AddressOf Watching)
            th.Start()
        End If
    End Sub

    Private Sub Watching()
        Dim FirstTime As Boolean = True
        Do While True
            Dim h As Long = card.GetIPStatistics.BytesReceived
            If Not FirstTime Then 下载网速 = (h - LastDown) / 1024
            LastDown = h
            h = card.GetIPStatistics.BytesSent
            If Not FirstTime Then 上传网速 = (h - LastUP) / 1024
            LastUP = h
            FirstTime = False
            Thread.Sleep(1000)
        Loop
    End Sub

    Public Sub 停止监控()
        If th.IsAlive Then th.Abort()
    End Sub

End Class
