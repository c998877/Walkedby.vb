Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Math
Imports System.TimeZoneInfo
Imports Microsoft
Imports System.Text
Imports System.ComponentModel
Imports System.Web
Imports System.Net
Imports System.Threading

Public Class Form1

    Dim apikey As String

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim a As Steam用户 = New Steam用户("76561198099466387", apikey)
        RR.Text = RR.Text + a.国家 + vbCrLf
        RR.Text = RR.Text + a.昵称 + vbCrLf
        RR.Text = RR.Text + a.最后登录时间 + vbCrLf
        RR.Text = RR.Text + a.真实姓名 + vbCrLf
        RR.Text = RR.Text + a.主页 + vbCrLf
        RR.Text = RR.Text + a.创号时间 + vbCrLf
        RR.Text = RR.Text + a.在线状态 + vbCrLf
        RR.Text = RR.Text + a.现在游戏ID + vbCrLf
    End Sub
End Class
