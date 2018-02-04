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

Module Walkedby_ControlMoving '用鼠标移动一个控件或窗体

    Private CmX As Integer, CmY As Integer, CmForm As Form, Cm As Control, CmON As Boolean

    Public Sub 拖动控件(winform As Form, c As Control, e As MouseEventArgs)
        CmForm = winform
        Cm = c
        CmON = True
        CmX = e.X
        CmY = e.Y
        AddHandler Cm.MouseMove, AddressOf 正在拖动控件
        AddHandler Cm.MouseUp, AddressOf 结束拖动控件
    End Sub

    Private Sub 正在拖动控件()
        If Not CmON Then Exit Sub
        Dim i As Integer, hx As Integer = 窗口边框X(CmForm), hy As Integer = 窗口边框Y(CmForm)
        i = Cursor.Position.X - CmForm.Left() - CmX - hx
        If i >= 0 AndAlso i < CmForm.Width - Cm.Width - hx Then Cm.Left = i
        i = Cursor.Position.Y - CmForm.Top - CmY - hy
        If i >= 0 AndAlso i < CmForm.Height - Cm.Height - hy Then Cm.Top = i
    End Sub

    Private Sub 结束拖动控件()
        CmON = False
    End Sub

    Public Sub 拖动窗体(winform As Form, c As Control, e As MouseEventArgs)
        CmForm = winform
        Cm = c
        If CmForm.WindowState = FormWindowState.Maximized Then CmForm.WindowState = FormWindowState.Normal
        CmON = True
        CmX = Cursor.Position.X - 窗口边框X(CmForm) - CmForm.Left
        CmY = Cursor.Position.Y - 窗口边框Y(CmForm) - CmForm.Top
        AddHandler Cm.MouseMove, AddressOf 正在拖动窗体
        AddHandler Cm.MouseUp, AddressOf 结束拖动控件
    End Sub

    Private Sub 正在拖动窗体()
        If Not CmON Then Exit Sub
        Dim i As Integer
        i = Cursor.Position.X - CmX - 窗口边框X(CmForm)
        CmForm.Left = i
        i = Cursor.Position.Y - CmY - 窗口边框Y(CmForm)
        CmForm.Top = i
        Thread.Sleep(1)
    End Sub

    '获得这个窗口的边框的宽度
    Public Function 窗口边框X(winform As Form) As Integer
        窗口边框X = 0
        Select Case CmForm.FormBorderStyle
            Case FormBorderStyle.Fixed3D
                窗口边框X = 10
            Case FormBorderStyle.FixedSingle
                窗口边框X = 10
            Case FormBorderStyle.Sizable
                窗口边框X = 10
            Case FormBorderStyle.FixedDialog
                窗口边框X = 8
            Case FormBorderStyle.FixedToolWindow
                窗口边框X = 8
            Case FormBorderStyle.SizableToolWindow
                窗口边框X = 8
        End Select
    End Function

    '获得这个窗口的边框的高度
    Public Function 窗口边框Y(winform As Form) As Integer
        窗口边框Y = 0
        Select Case CmForm.FormBorderStyle
            Case FormBorderStyle.Fixed3D
                窗口边框Y = 32
            Case FormBorderStyle.FixedSingle
                窗口边框Y = 30
            Case FormBorderStyle.Sizable
                窗口边框Y = 30
            Case FormBorderStyle.FixedDialog
                窗口边框Y = 30
            Case FormBorderStyle.FixedToolWindow
                窗口边框Y = 27
            Case FormBorderStyle.SizableToolWindow
                窗口边框Y = 27
        End Select
    End Function

End Module
