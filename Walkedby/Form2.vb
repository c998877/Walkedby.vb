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

    Dim r As Long = 0
    Dim b As Long = 0

    Dim th As Thread = New Thread(AddressOf GoFuck)

    Sub GoFuck()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        th = New Thread(AddressOf GoFuck)
        th.Start()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form2_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        th.Abort()
    End Sub

End Class