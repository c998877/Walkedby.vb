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
Imports System.Net.Mail
Imports System.Security.Permissions
Imports Microsoft.Win32
Imports System.Xml

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Wphone.Focus()
        Wbank.Focus()
        AutoR_Tick(sender, e)
        Height = 118
        Width = 389
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        BankRefresh.Enabled = False
        PhoneRefresh.Enabled = False
        Wphone.Dispose()
        Wbank.Dispose()
    End Sub

    Private Sub AutoR_Tick(sender As Object, e As EventArgs) Handles AutoR.Tick
        BankRefresh.Tag = "登录银行"
        Wphone.Navigate("http://10040.snail.com/pc/login.html")
        PhoneRefresh.Tag = "登录"
        Wbank.Navigate("https://ibsbjstar.ccb.com.cn/CCBIS/V6/STY1/CN/login.jsp")
        BankRefresh.Enabled = True
        PhoneRefresh.Enabled = True
        Wphone.Focus()
        Wbank.Focus()
        Dim 结束时间 As Long = UNIX时间(#2018-3-3 00:00:00#)
        Dim 开始时间 As Long = UNIX时间(#2018-1-26 13:00:00#)
        Dim 现在 As Long = UNIX时间(Now)
        LabHoliday.Text = "寒假已经过了：" + 标准数字((现在 - 开始时间) / (结束时间 - 开始时间) * 100, 2) + "%"
    End Sub

    Private Sub BankRefresh_Tick(sender As Object, e As EventArgs) Handles BankRefresh.Tick
        BankRefresh.Enabled = False
        Dim JSbank As String = "建设银行卡余额："
        Select Case BankRefresh.Tag
            Case "登录银行"
                LabBank.Text = JSbank + "读取中"
                If Not 完成Web(Wbank, 8000, True) Then Exit Select
                If Wbank.Document.Window.Frames.Count < 1 Then Exit Select
                Dim i As HtmlDocument = Wbank.Document.Window.Frames.Item(0).Document
                If IsNothing(i.GetElementById("USERID")) Then Exit Select
                If IsNothing(i.GetElementById("LOGPASS")) Then Exit Select
				Dim sid As string = ""	'身份证号
                i.GetElementById("USERID").InnerText = sid 
                i.GetElementById("LOGPASS").InnerText = ""   '密码
                i.GetElementById("loginButton").InvokeMember("click")
                BankRefresh.Tag = "读取银行key"
            Case "读取银行key"
                LabBank.Text = JSbank + "读取中"
                If Not 完成Web(Wbank, 8000, True) Then Exit Select
                If Wbank.Document.Window.Frames.Count < 1 Then Exit Select
                Dim h As String = Wbank.Document.All.Item(1).InnerHtml
                h = 正则提取(h, """SKEY"": """, """,")
                Wbank.Navigate("https://ibsbjstar.ccb.com.cn/CCBIS/B2CMainPlat_08?SERVLET_NAME=B2CMainPlat_08&CCB_IBSVersion=V6&PT_STYLE=1&isAjaxRequest=true&TXCODE=N31002&SKEY=" + h + "&USERID="  + sid +"&BRANCHID=330000000&ACC_NO=6236681490001430712&SEND_USERID=")
                BankRefresh.Tag = "读取银行余额"
            Case "读取银行余额"
                LabBank.Text = JSbank + "读取中"
                Dim h As String = Wbank.DocumentText
                If h.Length < 100 Then
                    LabBank.Text = JSbank + "读取失败"
                    Exit Select
                End If
                h = Regex.Match(h, "yu_e.*?$").ToString
                h = 去除(h, "yu_e", ":", vbQuote, "}")
                h = 正则去除(h, "<.*?>")
                LabBank.Text = JSbank + h
                Exit Sub
        End Select
        BankRefresh.Enabled = True
    End Sub

    Private Sub PhoneRefresh_Tick(sender As Object, e As EventArgs) Handles PhoneRefresh.Tick
        PhoneRefresh.Enabled = False
        Dim Sn As String = "话费余额："
        Select Case PhoneRefresh.Tag
            Case "登录"
                LabPhone.Text = Sn + "读取中"
                Wphone.Focus()
                If Not 完成Web(Wphone,, True) Then Exit Select
                Dim i As HtmlDocument = Wphone.Document
                If IsNothing(i.GetElementById("pwd")) Then Exit Select
                i.GetElementById("account").Focus()
                i.GetElementById("account").InnerText = ""
                i.GetElementById("pwd").Focus()
                i.GetElementById("pwd").SetAttribute("value", "" & vbCrLf)
                Wphone.Focus()
                i.GetElementById("login").InvokeMember("click")
                Wphone.Focus()
                i.GetElementById("login").InvokeMember("click")
                Wphone.Focus()
                i.GetElementById("login").InvokeMember("click")
                Wphone.Focus()
                i.GetElementById("login").InvokeMember("click")
                PhoneRefresh.Tag = "等待"
            Case "等待"
                LabPhone.Text = Sn + "读取中"
                PhoneRefresh.Tag = "查余额"
            Case "查余额"
                If Not 完成Web(Wphone,, True) Then Exit Select
                Dim s As String = Wphone.Document.Body.InnerText
                If Not 包含(s, "兔兔币") Then Exit Select
                s = 正则提取(s, "兔兔币", "元")
                s = 去除(s, vbCr, vbLf)
                If s.Length > 0 Then
                    LabPhone.Text = Sn + s
                Else
                    LabPhone.Text = Sn + "读取失败"
                End If
                Exit Sub
        End Select
        PhoneRefresh.Enabled = True
    End Sub

    Private Sub ButBack_Click(sender As Object, e As EventArgs) Handles ButBack.Click
        If Height < 200 Then Height = 515 Else Height = 118
        If Width < 400 Then Width = 540 Else Width = 389
    End Sub

    Private Sub Always_Tick(sender As Object, e As EventArgs) Handles Always.Tick
    End Sub

End Class