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

Public Class Form2

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim p As String = ("E:\a.gma")
        RR.Text = ""
        Dim re As Stream = File.Open(p, FileMode.Open)
        Dim x As BinaryReader
        x = New BinaryReader(re, Encoding.UTF8)
        For Each i As Byte In x.ReadBytes(re.Length)
            Dim m As String = i.ToString
            Dim h As Integer = i
            If (i >= 65 And i <= 90) Or (i >= 97 And i <= 122) Or (i >= 48 And i <= 57) Or i = 32 Then
                m = ChrW(m)
                If (i >= 48 And i <= 57) Then m = "n" + m
            Else
                m = " " + m + " "
            End If
            RR.Text = RR.Text + m
        Next
        x.Close()
        re.Close()
    End Sub


End Class