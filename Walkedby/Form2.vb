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

    Dim th As Thread = New Thread(AddressOf GoFuck)

    Sub GoFuck()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        写设置(随机字母(3), 随机)
        RR.Text = 设置XML()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RR.Text = 设置XML()
    End Sub

    Private Sub Form2_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

End Class