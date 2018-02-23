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

Public Class Form1

    Dim DeleteURL As String = ""
    Dim filepath As String = ""
    Dim up As Thread = New Thread(AddressOf 上传)
    Dim redcolor As Color = 随机颜色(255, 128, 128)

    Private Sub Form1_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        e.Effect = DragDropEffects.Link
        TxtBack.BackColor = redcolor
        TxtBack.Text = ""
        Button2.Enabled = False
        Pic.Image = Nothing
        Button1.Enabled = False
        线程越界()
    End Sub

    Private Sub Form1_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        filepath = ""
        up = New Thread(AddressOf 上传)
        Dim i As String = ""
        For Each i In e.Data.GetData(DataFormats.FileDrop)
            Dim h As String = 文件格式(i)
            If h.Equals("jpg") OrElse h.Equals("png") OrElse h.Equals("gif") OrElse h.Equals("bmp") Then
                If 文件大小(i, "m") <= 5 Then Exit For
            End If
            i = ""
        Next
        filepath = i
        If i.Length < 5 Then Exit Sub
        TxtBack.Text = "上传ing"
        up.Start()
        Button3.Enabled = True
    End Sub

    Sub 上传()
        Dim i As String = filepath
        Pic.Image = Image.FromFile(i)
        Dim hq As New 分段POST("https://sm.ms/api/upload")
        hq.新参数("format", "xml")
        hq.新文件("smfile", 文件名(i), "image/" + 文件格式(i), 读比特(i))
        Dim l As String = hq.传回
        If 包含(l, "<code>success</code>") Then
            TxtBack.BackColor = Color.LightGreen
            TxtBack.Text = 正则提取(l, "<url>", "</url>")
            TxtHistory.Text += vbCrLf + TxtBack.Text
            Button2.Enabled = True
            Button1.Enabled = True
            DeleteURL = 正则提取(l, "<delete>", "</delete>")
        Else
            TxtBack.BackColor = redcolor
            TxtBack.Text = l
        End If
        Button3.Enabled = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Clipboard.SetText(TxtBack.Text)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim i As String = 获得Http(DeleteURL)
        If 包含(i, "File delete success") Then
            TxtBack.BackColor = redcolor
            Button2.Enabled = False
            TxtBack.Text = "成功删除刚刚的图片"
            TxtHistory.Text += "(已删除)"
        End If
    End Sub

    Private Sub TxtHistory_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtHistory.KeyDown
        文本框全选(sender, e)
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If up.IsAlive Then up.Abort()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If up.IsAlive Then up.Abort()
        Button3.Enabled = False
        TxtBack.Text = "已经取消"
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        浏览器打开("https://sm.ms/")
    End Sub

End Class