Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Math
Imports System.TimeZoneInfo
Imports Microsoft
Imports System.Text
Imports System.ComponentModel
Imports System.Web
Imports System.Net

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RR.Text = 快速HTTPGET(TT.Text)
    End Sub
End Class
