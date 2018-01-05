Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Math
Imports System.TimeZoneInfo
Imports Microsoft
Imports System.Text


Public Class Form1
    Dim t1 As Integer = 0
    Dim t2 As Integer = 0
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If 随机B() Then
            t1 = t1 + 1
        Else
            t2 = t2 + 1
        End If
        控制台(t1, t2)
    End Sub
End Class
