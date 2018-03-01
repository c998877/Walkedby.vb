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
Imports System.Net.Http
Imports System.Net.Mail
Imports System.Security.Permissions
Imports Microsoft.Win32
Imports System.Xml
Imports System.Security.Cryptography

Public Class Form1

    Dim th As New Thread(AddressOf 上传)
    Dim PicURL As String = ""

    Sub 上传()
        Dim t As String = LabPIC.Text
        If Not 文件存在(t) Then
            TxtB.Text = "文件已经丢失，无法继续上传。"
            清空()
            Exit Sub
        End If
        Dim i As New 分段POST("https://api.vc.bilibili.com/api/v1/image/upload")
        TxtB.Text = "上传中，请稍等"
        Button1.Enabled = False
        Dim h As Byte() = 读比特(t)
        i.新文件("file_up", 文件名(t), "image/png", h)
        i.新参数("biz", "draw")
        i.新参数("category", "daily")
        t = Replace(i.传回, "\/", "/")
        Dim b As String = ""
        If 包含(t, "success") Then
            PicURL = 正则提取(t, "image_url"":""", """,", False)
            b = "上传成功" + vbCrLf + PicURL + vbCrLf + "图片宽度：" + 只要数字(正则提取(t, "image_width", ",", False)) + vbCrLf + "图片高度：" + 只要数字(正则提取(t, "image_height", "}", False))
        Else
            b = "上传失败，详情：" + vbCrLf + t
            PicURL = ""
        End If
        TxtB.Text = b
    End Sub

    Sub 清空()
        Button1.Enabled = False
        PIC.Image = Nothing
        LabPIC.Text = "无图片"
    End Sub

    Private Sub Form1_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        清空()
        For Each i As String In e.Data.GetData(DataFormats.FileDrop)
            If 文件存在(i) AndAlso 合适文件格式(i, "jpg", "png", "jpeg") AndAlso 文件大小(i) < 20 Then
                PIC.Image = Image.FromFile(i)
                LabPIC.Text = i
                Button1.Enabled = True
                Exit For
            End If
        Next
    End Sub

    Private Sub Form1_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        e.Effect = DragDropEffects.Link
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        清空()
        If IsNothing(Clipboard.GetImage) Then Exit Sub
        Dim i As Image = Clipboard.GetImage
        Dim h As String = 程序目录() + "tmp.jpg"
        i.Save(h)
        If 文件大小(h) < 20 Then
            LabPIC.Text = h
            PIC.Image = i
            Button1.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        th = New Thread(AddressOf 上传)
        th.Start()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        线程越界()
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If th.IsAlive Then th.Abort()
        删除文件(程序目录() + "tmp.jpg")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If PicURL.Length > 5 Then Clipboard.SetText(PicURL)
    End Sub

End Class

