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

Public Class 获得IP地址  '获得外网 IP 和地理位置

    Private txt As String

    Public Sub New()
        txt = 获得Http("http://ip.chinaz.com/getip.aspx", "{ip:'0.0.0.0',address:'未知'}")
    End Sub

    Public ReadOnly Property 公网IP As String
        Get
            Return 提取(txt, "ip:'", "',")
        End Get
    End Property

    Public ReadOnly Property 地理位置 As String
        Get
            Return 提取(txt, "address:'", "'}")
        End Get
    End Property

End Class

Public Class 网速监测   '后台新建一个线程监测网速

    Private card As NetworkInformation.NetworkInterface
    Private LastDown As Long = 0
    Private LastUP As Long = 0
    Private AllDown As Long = 0
    Private AllUP As Long = 0
    Private Down As Long = 0
    Private UP As Long = 0
    Private FirstTime As Boolean = True
    Private th As Thread

    Public Sub New(Optional CardName As String = "")
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
        Do While True
            AllDown = card.GetIPStatistics.BytesReceived
            If Not FirstTime Then Down = AllDown - LastDown
            LastDown = AllDown
            AllUP = card.GetIPStatistics.BytesSent
            If Not FirstTime Then UP = AllUP - LastUP
            LastUP = AllUP
            FirstTime = False
            Thread.Sleep(1000)
        Loop
    End Sub

    Public ReadOnly Property 下载网速(Optional unit As String = "k") As Single
        Get
            Return 单位转换(Down,, unit)
        End Get
    End Property

    Public ReadOnly Property 上传网速(Optional unit As String = "k") As Single
        Get
            Return 单位转换(UP,, unit)
        End Get
    End Property

    Public Sub 停止监控()
        If th.IsAlive Then th.Abort()
    End Sub

End Class

Public Class 分段POST   '用来 POST 分段数据的 http 请求

    Private h As HttpWebRequest
    Private bd As String
    Private s As Stream
    Private heading As String
    Private 数据 As String

    Public Sub New(url As String, Optional ua As String = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.167 Safari/537.36")
        Try
            h = WebRequest.Create(url)
            数据 = ""
            bd = "---------------" + Now.Ticks.ToString("x")
            heading = "--" + bd + vbCrLf
            h.Method = "POST"
            h.UserAgent = ua
            h.ContentType = "multipart/form-data; boundary=" + bd
            s = h.GetRequestStream
        Catch ex As Exception
            s = New MemoryStream
        End Try
    End Sub

    Private Sub 加头()
        Dim b As Byte() = 文字转字节(heading)
        s.Write(b, 0, b.Length)
        数据 += heading
    End Sub

    Public Sub 新参数(name As String, value As String, Optional type As String = "TEXT/HTML")
        加头()
        Dim i As String = "Content-Disposition: form-data; name=" + 引(name) + vbCrLf + "Content-Type: " + type + vbCrLf + vbCrLf + value + vbCrLf
        Dim b As Byte() = 文字转字节(i)
        数据 += i
        s.Write(b, 0, b.Length)
    End Sub

    Public Sub 新文件(name As String, filename As String, type As String, file As Byte())
        加头()
        Dim i As String = "Content-Disposition: form-data; name=" + 引(name) + "; filename=" + 引(filename) + vbCrLf + "Content-Type: " + type + vbCrLf + vbCrLf
        Dim b As Byte() = 文字转字节(i)
        s.Write(b, 0, b.Length)
        s.Write(file, 0, file.Length)
        数据 += i
    End Sub

    Public Function 传回(Optional def As String = "") As String
        Dim i As String = vbCrLf + "--" + bd + "--"
        Dim b As Byte() = 文字转字节(i)
        s.Write(b, 0, b.Length)
        数据 += i
        Try
            Dim rs As New StreamReader(h.GetResponse.GetResponseStream)
            传回 = rs.ReadToEnd
            rs.Close()
        Catch ex As Exception
            If def.Length < 1 Then
                传回 = ex.Message
            Else
                传回 = def
            End If
        End Try
    End Function

    Public ReadOnly Property 数据预览 As String
        Get
            Return 数据 + vbCrLf + "--" + bd + "--"
        End Get
    End Property

End Class
