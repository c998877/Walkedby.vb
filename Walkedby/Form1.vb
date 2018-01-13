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
Imports System.Threading.Tasks
Imports System.Media
Imports System.Media.SystemSounds
Imports System.Device.Location
Imports System.Object

Public Class Form1
    '这个窗体是用来批量改workshop的，自己写的小功能，不发布

    Dim lik As String = "https://steamcommunity.com/sharedfiles/itemedittext/?id="
    Dim nowID As String
    Dim nowLangu As String

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        PB.Value = PB.Maximum - WorkList.Items.Count
        If LanguageList.Items.Count > 0 Then nowLangu = 只要数字(LanguageList.Items.Item(0).ToString)
        If WorkList.Items.Count > 0 Then nowID = WorkList.Items.Item(0)
        Text = Timer1.Tag + "，当前素材ID：" + nowID + "，还剩：" + WorkList.Items.Count.ToString
        If WorkList.Items.Count < 1 Then
            Timer1.Tag = "已经完成"
            Exit Sub
        End If
        If 完成Web(WB, 3000) = False Then Exit Sub
        Select Case LCase(Timer1.Tag)
            Case "寻找语言"
                For Each h As HtmlElement In WB.Document.GetElementsByTagName("option")
                    Dim s As String = h.OuterHtml
                    If 包含(s, ")") Then
                        s = 正则提取(s, "value=", ">", False)
                        LanguageList.Items.Add(只要数字(s))
                    End If
                Next
                列表去重(LanguageList.Items)
                nowLangu = 只要数字(LanguageList.Items.Item(0).ToString)
                Timer1.Tag = "修改"
            Case "修改"
                Dim h As HtmlElement = WB.Document.GetElementById("description")
                Dim s As String = h.InnerText
                s = 正则去除(s, "\[url.*?(099466387|edy)/myworkshopfiles([\s\S]*?)url]")
                s = 正则去除(s, "\[img.*?top/workshop.jpg.*?\]")
                s = 正则去除(s, "\[img.*?ooo.0o0.ooo.*?\]")
                s = 去多余回车(s)
                s = s + vbCrLf + "[url=https://steamcommunity.com/profiles/76561198099466387/myworkshopfiles/][img]https://gordonw.top/workshop.jpg[/img][/url]"
                h.InnerText = s
                RR.Text = s
                RR.SelectionStart = RR.TextLength
                RR.ScrollToCaret()
                WB.Document.InvokeScript("ValidateForm")
                Timer1.Tag = "等待修改完成"
            Case "等待修改完成"
                If 包含(WB.Document.Body.InnerHtml, "workshop_paymentinfo_validation_error") = False Then Exit Sub
                LanguageList.Items.RemoveAt(0)
                If LanguageList.Items.Count > 0 Then
                    Timer1.Tag = "下个语言"
                Else
                    WorkList.Items.RemoveAt(0)
                    Timer1.Tag = "下个addon"
                End If
            Case "下个语言"
                nowLangu = 只要数字(LanguageList.Items.Item(0).ToString)
                WB.Navigate(lik + nowID + "&language=" + nowLangu)
                Timer1.Tag = "等待加载完成再修改"
            Case "等待加载完成再修改"
                Timer1.Tag = "修改"
            Case "下个addon"
                WB.Navigate(lik + nowID + "&language=0")
                Timer1.Tag = "等待加载完成再选择语言"
                控制台("Done:", PB.Value)
            Case "等待加载完成再选择语言"
                If 包含(WB.Url.ToString, "itemedittext") = False Or 包含(WB.DocumentText, "returnLink") Then
                    WorkList.Items.RemoveAt(0)
                    Timer1.Tag = "下个addon"
                    Exit Sub
                End If
                Timer1.Tag = "寻找语言"
        End Select
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        列表去重(WorkList.Items)
        控制台(列表转文字(WorkList.Items))
        PB.Maximum = WorkList.Items.Count
    End Sub

    Private Sub ButX_Click(sender As Object, e As EventArgs) Handles ButX.Click
        Timer1.Enabled = Not Timer1.Enabled
        If Timer1.Enabled Then
            ButX.Text = "暂停"
        Else
            ButX.Text = "继续"
        End If
    End Sub

End Class
