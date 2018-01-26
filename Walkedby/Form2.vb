Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Math
Imports System.TimeZoneInfo
Imports Microsoft
Imports System.Text
Imports System.ComponentModel
Imports System.Web
Imports System.Net
Imports System.Net.Mail
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Xml

Public Class Form2
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim h As Long = 1
        Dim s As Collection = New Collection
        For Each i As String In Directory.EnumerateFiles("D:\Program Files\WPS Office", "*", SearchOption.AllDirectories)
            If 文件格式(i).Equals("exe") Then
                控制台("  {
    ""fn"":""" + i + """,
    ""policies"":[],
    ""pv"":0,
    ""tm"":1
  },")
            End If
        Next

    End Sub

End Class