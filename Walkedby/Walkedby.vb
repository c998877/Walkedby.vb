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

Module Walkedby '走過去的常用函数合集
    '統一：简体字，数字一律整数 Integer

    Public Const 引号 As String = """"

    '左右，相当于 left right
    Public Function 左(str As String, i As Object) As String
        Dim s As Integer
        If i.GetType.Equals(String.Empty.GetType) Then s = i.length Else s = i
        If s > 0 Then 左 = Left(str, s) Else 左 = ""
    End Function
    Public Function 右(str As String, i As Object) As String
        Dim s As Integer
        If i.GetType.Equals(String.Empty.GetType) Then s = i.length Else s = i
        If s > 0 Then 右 = Right(str, s) Else 右 = ""
    End Function
    Public Function 左右(str As String, Optional 左边 As Object = 0, Optional 右边 As Object = 0) As String
        Dim l As Integer, r As Integer
        If 左边.GetType.Equals(String.Empty.GetType) Then l = 左边.length Else l = 左边
        If 右边.GetType.Equals(String.Empty.GetType) Then r = 右边.length Else r = 右边
        If l + r >= str.Length Then 左右 = str Else 左右 = 左(str, l) + 右(str, r)
    End Function

    '去掉左边或右边
    Public Function 去左(str As String, i As Object) As String
        去左 = ""
        Dim s As Integer
        If i.GetType.Equals(String.Empty.GetType) Then s = i.length Else s = i
        If str.Length - s > 0 Then 去左 = Right(str, str.Length - s)
    End Function
    Public Function 去右(str As String, i As Object) As String
        去右 = ""
        Dim s As Integer
        If i.GetType.Equals(String.Empty.GetType) Then s = i.length Else s = i
        If str.Length - s > 0 Then 去右 = Left(str, str.Length - s)
    End Function
    Public Function 去左右(str As String, Optional 左边 As Object = 0, Optional 右边 As Object = 0) As String
        Dim l As Integer, r As Integer
        If 左边.GetType.Equals(String.Empty.GetType) Then l = 左边.length Else l = 左边
        If 右边.GetType.Equals(String.Empty.GetType) Then r = 右边.length Else r = 右边
        If l + r >= str.Length Then 去左右 = "" Else 去左右 = Mid(str, l + 1, str.Length - r - l)
    End Function

    '检查字符串头或者尾是不是对应的
    Public Function 头(str As String, a As String) As Boolean
        If a.Length < 1 Then 头 = False : Exit Function
        头 = (左(str, a.Length).Equals(a))
    End Function
    Public Function 尾(str As String, a As String) As Boolean
        If a.Length < 1 Then 尾 = False : Exit Function
        尾 = (右(str, a.Length).Equals(a))
    End Function

    'find 是否包含在 str 里，默认不检查大小写
    Public Function 包含(str As String, find As String, Optional forceCase As Boolean = False) As Boolean
        包含 = False
        If str.Length < 1 OrElse find.Length < 1 OrElse str.Length < find.Length Then Exit Function
        If forceCase = False Then
            str = LCase(str)
            find = LCase(find)
        End If
        If InStr(str, find) > 0 Then 包含 = True
    End Function
    Public Function 正则包含(str As String, find As String, Optional forceCase As Boolean = False) As Boolean
        正则包含 = False
        If str.Length < 1 OrElse find.Length < 1 OrElse str.Length < find.Length Then Exit Function
        If Not forceCase Then
            str = LCase(str)
            find = LCase(find)
        End If
        If Regex.IsMatch(str, find) Then 正则包含 = True
    End Function

    '正则提取头尾字符中间的字符
    Public Function 正则提取(str As String, head As String, tail As String, Optional multiLine As Boolean = True, Optional forceCase As Boolean = True) As String
        正则提取 = ""
        If str.Length < 1 OrElse head.Length + tail.Length > str.Length OrElse head.Length < 1 OrElse tail.Length < 1 Then Exit Function
        If Not forceCase Then
            str = LCase(str)
            head = LCase(head)
            tail = LCase(tail)
        End If
        Dim x As String = ".*?"
        If multiLine Then x = "([\s\S]*?)"
        Dim s As String = Regex.Match(str, head + x + tail).ToString
        正则提取 = 去左右(s, head, tail)
    End Function

    '去掉多余的回车换行
    Public Function 去多余回车(str As String) As String
        str = 回车规范(str)
        去多余回车 = str
        去多余回车 = Regex.Replace(str, "[\r\n]{5,}", vbCrLf + vbCrLf)
    End Function

    '取得范围内的随机整数
    Public Function 随机(Optional a As Integer = 1000, Optional b As Integer = 0) As Integer
        If a < b Then
            Dim t As Integer = b
            b = a
            a = t
        End If
        Randomize()
        随机 = Int(Rnd() * (a - b + 1） + b)
    End Function

    '随机一个布林值
    Public Function 随机B() As Boolean
        随机B = (随机(1, 2) = 2)
    End Function

    '随机一个颜色，但是可以固定RGBA
    Public Function 随机颜色(Optional R As Integer = 0, Optional G As Integer = 0, Optional B As Integer = 0, Optional A As Integer = 255) As Color
        If A < 1 OrElse A > 255 Then A = 随机(1, 255)
        If R < 1 OrElse R > 255 Then R = 随机(1, 255)
        If G < 1 OrElse G > 255 Then G = 随机(1, 255)
        If B < 1 OrElse B > 255 Then B = 随机(1, 255)
        随机颜色 = Color.FromArgb(A, R, G, B)
    End Function

    '生成指定长度的随机的小写字母
    Public Function 随机字母(Optional l As Integer = 10) As String
        Dim i As Integer, s As String = ""
        For i = 1 To l
            s = s + Chr(随机(97, 122))
        Next
        随机字母 = s
    End Function

    '随机a小段，每小段b长度，用s相连的 KEY 出来
    Public Function 随机KEY(Optional a As Integer = 5, Optional b As Integer = 5, Optional s As String = "-") As String
        随机KEY = s
        Dim i As Integer, j As Integer, x As String = ""
        For i = 1 To a
            For j = 1 To b
                If 随机B() Then x = x & 随机字母(1) Else x = x & 随机(9, 0)
            Next
            x = x & s
        Next
        随机KEY = UCase(去右(x, s.Length))
    End Function

    '从 str 中去除指定的文字
    Public Function 去除(str As String, a As String, Optional b As String = "", Optional c As String = "", Optional d As String = "", Optional e As String = "", Optional f As String = "", Optional g As String = "", Optional h As String = "", Optional i As String = "")
        去除 = str
        If str.Length < 1 Then Exit Function
        If a.Length > 0 Then 去除 = Replace(str, a, "")
        If b.Length > 0 Then 去除 = Replace(去除, b, "")
        If c.Length > 0 Then 去除 = Replace(去除, c, "")
        If d.Length > 0 Then 去除 = Replace(去除, d, "")
        If e.Length > 0 Then 去除 = Replace(去除, e, "")
        If e.Length > 0 Then 去除 = Replace(去除, e, "")
        If f.Length > 0 Then 去除 = Replace(去除, f, "")
        If g.Length > 0 Then 去除 = Replace(去除, g, "")
        If h.Length > 0 Then 去除 = Replace(去除, h, "")
        If i.Length > 0 Then 去除 = Replace(去除, i, "")
        If 去除 = Nothing Then 去除 = ""
    End Function

    '从 str 中去除对应的正则文字
    Public Function 正则去除(str As String, a As String, Optional b As String = "", Optional c As String = "", Optional d As String = "", Optional e As String = "", Optional f As String = "", Optional g As String = "", Optional h As String = "", Optional i As String = "")
        正则去除 = str
        If a.Length > 0 AndAlso 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, a, "")
        If b.Length > 0 AndAlso 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, b, "")
        If c.Length > 0 AndAlso 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, c, "")
        If d.Length > 0 AndAlso 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, d, "")
        If e.Length > 0 AndAlso 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, e, "")
        If e.Length > 0 AndAlso 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, e, "")
        If f.Length > 0 AndAlso 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, f, "")
        If g.Length > 0 AndAlso 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, g, "")
        If h.Length > 0 AndAlso 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, h, "")
        If i.Length > 0 AndAlso 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, i, "")
        If 正则去除 = Nothing Then 正则去除 = ""
    End Function

    '生成i个str组合成的字符串，最少一个
    Public Function 凑(str As String, i As Integer) As String
        Dim s As Integer
        凑 = ""
        For s = 1 To i
            凑 = 凑 + str
        Next
    End Function

    '第一个词
    Public Function 第一个词(str As String) As String
        第一个词 = ""
        str = Trim(str) + " "
        If str.Length < 2 Then Exit Function
        str = Regex.Match(str, "(\S).*? ").ToString
        第一个词 = Trim(str)
    End Function

    '回车规范化，统一到 vbcrlf
    Public Function 回车规范(str As String) As String
        回车规范 = str
        Dim cr As Integer = Regex.Matches(str, "\r").Count
        Dim lf As Integer = Regex.Matches(str, "\n").Count
        If cr > lf Then
            str = 正则去除(str, "\n")
            回车规范 = Regex.Replace(str, "\r", vbCrLf)
        ElseIf cr < lf Then
            str = 正则去除(str, "\r")
            回车规范 = Regex.Replace(str, "\n", vbCrLf)
        ElseIf cr = lf Then
            回车规范 = str
        End If
    End Function

    '获得小写文件格式名，不包含第一个点
    Public Function 文件格式(path As String) As String
        文件格式 = ""
        If path.Length < 4 OrElse Not 包含(path, ".") Then Exit Function
        文件格式 = LCase(去左(Regex.Match(文件名(path), "\..*?$").ToString, 1))
    End Function

    '获得路径或者倒数第二层文件夹
    Public Function 路径(path As String) As String
        路径 = Regex.Match(path, ".*\\").ToString
    End Function

    '去掉不应该有的符号
    Public Function 文件名规范(path As String, Optional 去斜杠 As Boolean = False, Optional 去引号 As Boolean = False) As String
        文件名规范 = ""
        If path.Length < 1 Then Exit Function
        path = Replace(path, "/", "\")
        文件名规范 = 去除(path, "*", "?", "<", ">", "|", 引号)
        文件名规范 = Regex.Replace(文件名规范, "\\{2,}", "\")
        If 去斜杠 Then 文件名规范 = 去除(文件名规范, "\")
        If 去引号 Then 文件名规范 = 去除(文件名规范, ":")
    End Function

    '从 path 中获得文件或者最后一层文件夹的名字
    Public Function 文件名(path As String) As String
        path = Replace(path, "/", "\")
        If 右(path, 1).Equals("\") Then path = 去右(path, 1)
        文件名 = 文件名规范(去除(StrReverse(Regex.Match(StrReverse(path), ".*?\\").ToString), "\"))
    End Function

    '文件大小，单位默认是 MB
    Public Function 文件大小(path As String, Optional unit As String = "M") As Single
        文件大小 = 0
        If 文件存在(path, False) = False Then Exit Function
        Dim h As Long = FileLen(path)
        Select Case 左(UCase(unit), 1)
            Case "B"
                文件大小 = h
            Case "K"
                文件大小 = h / 1024
            Case "G"
                文件大小 = h / 1024 / 1024 / 1024
            Case "T"
                文件大小 = h / 1024 / 1024 / 1024 / 1024
            Case "P"
                文件大小 = h / 1024 / 1024 / 1024 / 1024 / 1024
            Case Else
                文件大小 = h / 1024 / 1024
        End Select
    End Function

    '运行 CMD 命令
    Public Sub 运行CMD(a As String, Optional b As String = "", Optional c As String = "", Optional d As String = "", Optional e As String = "")
        If Len(a) < 1 Then Exit Sub
        Dim s As String = "cmd.exe /c " + a
        If Len(b) > 0 Then s = s + " & " + b
        If Len(c) > 0 Then s = s + " & " + c
        If Len(d) > 0 Then s = s + " & " + d
        If Len(e) > 0 Then s = s + " & " + e
        If 包含(s, "pause") Then
            Shell(s)
        Else
            Shell(s, vbHide)
        End If
    End Sub

    '到指定的路径运行 cmd
    Public Sub 运行路径CMD(path As String, a As String, Optional b As String = "", Optional c As String = "", Optional d As String = "", Optional e As String = "")
        If path.Length < 3 OrElse Len(a) < 1 Then Exit Sub
        Dim s As String = "cmd.exe /c " + LCase(左(path, 2))
        If 尾(path, "\") AndAlso path.Length > 3 Then path = 去右(path, "\")
        s = s + " & cd " + 引号 + LCase(path) + 引号
        If Len(a) < 1 Then Exit Sub Else s = s + " & " + a
        If Len(b) > 0 Then s = s + " & " + b
        If Len(c) > 0 Then s = s + " & " + c
        If Len(d) > 0 Then s = s + " & " + d
        If Len(e) > 0 Then s = s + " & " + e
        If 包含(s, "pause") Then
            Shell(s)
        Else
            Shell(s, vbHide)
        End If
    End Sub

    '删除一个文件，超级强悍的火力
    Public Sub 删除文件(path As String)
        If path.Length < 4 Then Exit Sub
        Dim s As String = 引号 + path + 引号
        运行CMD("del /q /f " + s, " rmdir /s /q " + s)
    End Sub

    '读取快捷方式的来源地
    Public Function 快捷方式目的(path As String) As String
        快捷方式目的 = ""
        If 文件格式(path).Equals("lnk") AndAlso 文件存在(path) Then
            Dim s As String = 路径(path) + 随机字母(10) + ".lnk"
            FileCopy(path, s)
            Try
                Dim wshLink, wshShell
                wshShell = CreateObject("WScript.Shell")
                wshLink = wshShell.CreateShortcut(s)
                快捷方式目的 = wshLink.TargetPath
            Catch
            End Try
            删除文件(s)
        End If
    End Function

    '取得本程序的目录，最后带个"\"
    Public Function 程序目录() As String
        程序目录 = My.Application.Info.DirectoryPath
        If Not 右(程序目录, 1).Equals("\") Then 程序目录 = 程序目录 + "\"
    End Function

    '给线程权限可以直接篡改控件
    Public Sub 线程越界()
        Control.CheckForIllegalCrossThreadCalls = False
    End Sub

    '取得本程序的文件名，包含".exe"
    Public Function 程序名() As String
        程序名 = 文件名(Application.ExecutablePath)
    End Function

    '检测系统是不是64位的
    Public Function 系统64位() As Boolean
        系统64位 = (Runtime.InteropServices.Marshal.SizeOf(IntPtr.Zero) * 8 = 64)
    End Function

    '检测文件或者路径是否存在 
    Public Function 文件存在(path As String, Optional 是文件夹 As Boolean = False) As Boolean
        文件存在 = False
        If path.Length < 2 Then Exit Function
        path = 文件名规范(path)
        If 是文件夹 Then 文件存在 = Directory.Exists(path) Else 文件存在 = File.Exists(path)
    End Function

    '检测文件是否处于可写入状态
    Public Function 文件可写(path As String) As Boolean
        文件可写 = False
        If path.Length < 2 Then Exit Function
        If 文件存在(path) = False Then
            文件可写 = True
        Else
            Try
                File.Open(path, FileMode.Open).Close()
                文件可写 = True
            Catch
                文件可写 = False
            End Try
        End If
    End Function

    '写入 str 到 path 文件里
    Public Sub 写(path As String, str As String)
        If 文件存在(路径(path), False) = False Then Directory.CreateDirectory(路径(path))
        If Not 文件可写(path) Then Exit Sub
        File.WriteAllText(path, str, Text.Encoding.UTF8)
    End Sub

    '写入 str 到 path 文件的末尾
    Public Sub 附写(path As String, str As String, Optional AddEnter As Boolean = True)
        If Not 文件可写(path) Then Exit Sub
        If AddEnter Then str = vbCrLf + str
        File.AppendAllText(path, str, Text.Encoding.UTF8)
    End Sub

    '检查指定名字的程序是否在运行状态，不要后缀名
    Public Function 程序运行中(Pname As String) As Boolean
        程序运行中 = False
        Pname = 去右(Pname, 文件格式(Pname))
        Dim x As Process() = Process.GetProcessesByName(Pname)
        If x.Count > 0 Then 程序运行中 = True
    End Function

    '读入 path 文件为字符串
    Public Function 读(path As String, Optional 默认 As String = "") As String
        读 = 默认
        If Not 文件存在(path) Then Exit Function
        Try
            读 = File.ReadAllText(path, Text.Encoding.UTF8)
        Catch
            读 = 默认
        End Try
    End Function

    '获得 str 的行数，最少为1行
    Public Function 行数(str As String) As Integer
        str = 回车规范(str)
        行数 = 1
        If str.Length < 2 Then Exit Function
        行数 = Regex.Matches(str, ".*?(\r|$)").Count - 1
    End Function

    '读取 str 的第 i 行
    Public Function 行(str As String, i As Integer, Optional def As String = "") As String
        行 = def
        str = 回车规范(str)
        Dim mc As MatchCollection = Regex.Matches(str, ".*?(\r|$)")
        If mc.Count < 1 Then Exit Function
        Dim t As Integer = mc.Count - 1
        If i > t Then Exit Function
        行 = 去除(mc.Item(i - 1).ToString, vbCr, vbLf, vbCrLf)
    End Function

    '改变 str 的第 i 行为 change
    Public Function 改行(str As String, i As Integer, change As String) As String
        If str.Length < 2 Then str = vbCrLf
        str = 回车规范(str)
        改行 = str
        Dim mc As MatchCollection = Regex.Matches(str, ".*?(\r|$)")
        Dim t As Integer = mc.Count - 1
        If t < i Then
            改行 = str + 凑(vbCrLf, i - t) + change
        ElseIf t = i Then
            If i > 1 Then
                改行 = Regex.Match(str, "([\s\S]*)\n").ToString + change
            ElseIf i <= 1 Then
                改行 = change
            End If
        ElseIf t > i Then
            Dim s1 As String = ""
            Dim s2 As String = ""
            Dim m As Integer
            For m = 1 To i - 1
                s1 = s1 + (mc.Item(m - 1).ToString)
            Next
            For m = i + 1 To t
                s2 = s2 + (mc.Item(m - 1).ToString)
            Next
            改行 = s1 + change + vbCrLf + s2
        End If
    End Function

    '检测 str 是否在列表 l 里
    Public Function 在列表(l As Object, str As String) As Boolean
        在列表 = False
        If str.Length = 0 Then Exit Function
        If l.Count < 1 Then Exit Function
        For Each i As Object In l
            If str.Equals(i.ToString) Then
                在列表 = True
                Exit Function
            End If
        Next
    End Function

    '检测 str 是否是列表 l 里的某一项的一部分
    Public Function 包含在列表(l As Object, str As String) As Boolean
        包含在列表 = False
        If str.Length = 0 Then Exit Function
        If l.Count < 1 Then Exit Function
        For Each i As Object In l
            If 包含(i.ToString, str) Then
                包含在列表 = True
                Exit Function
            End If
        Next
    End Function

    '为列表 l 去重
    Public Sub 列表去重(l As Object)
        If l.Count < 1 Then Exit Sub
        Dim x As Collection = New Collection
        For Each i As String In l
            If 在列表(x, i) = False AndAlso Trim(i).Length > 0 Then x.Add(i)
        Next
        l.clear
        For Each i As Object In x
            If Trim(i).ToString.Length > 0 Then l.Add(i.ToString)
        Next
    End Sub

    '把每行文字加入到列表 l
    Public Sub 文字转列表(str As String, l As Object)
        If str.Length < 2 Then Exit Sub
        Dim i As Integer
        For i = 1 To 行数(str)
            Dim s As String = 行(str, i).ToString
            If s.Length > 0 Then l.Add(s)
        Next
    End Sub

    '把列表型控件的每一项变成一行行的文字
    Public Function 列表转文字(l As Object) As String
        Dim s As String = "", m As String = ""
        For Each s In l
            m = m + s + vbCrLf
        Next
        列表转文字 = 去右(m, 2)
    End Function

    '获得UNIX时间
    Public Function UNIX时间(Optional time1 As Date = Nothing) As Long
        If IsNothing(time1) Then time1 = Now
        UNIX时间 = DateDiff("s", "1970-1-1 0:0:0", ConvertTimeToUtc(time1))
    End Function

    'UNIX时间变成普通的时间
    Public Function UNIX时间恢复(u As Long) As Date
        Dim T1970 As Date = "1970-1-1 0:0:0"
        UNIX时间恢复 = ConvertTime(T1970.Add(New TimeSpan(0, 0, 0, u)), Utc, Local)
    End Function

    '把计算机时间变成北京的时间
    Public Function 北京时间(time1 As Date) As Date
        北京时间 = ConvertTimeBySystemTimeZoneId(time1, Local.Id, "China Standard Time")
    End Function

    '去掉字符串里不是数字的字符
    Public Function 只要数字(str As String) As String
        只要数字 = ""
        If str.Length < 1 Then Exit Function
        For Each m As Match In Regex.Matches(str, "[0-9]")
            只要数字 = 只要数字 + m.ToString
        Next
    End Function

    '去掉字符串里不是字母的字符，可以选择保留数字
    Public Function 只要字母(str As String, Optional KeepNum As Boolean = False) As String
        只要字母 = ""
        If str.Length < 1 Then Exit Function
        Dim s As String = "([a-z]|[A-Z])"
        If KeepNum Then s = "([a-z]|[A-Z]|[0-9])"
        For Each m As Match In Regex.Matches(str, s)
            只要字母 = 只要字母 + m.ToString
        Next
    End Function

    '打开网址到浏览器
    Public Sub 浏览器打开(str As String)
        If Not (头(str, "https://") OrElse 头(str, "http://") OrElse 头(str, "file://")) Then
            str = "http://" + str
        End If
        Process.Start(str)
    End Sub

    '便捷的控制台输出，多个选项自动分开
    Public Sub 控制台(a As Object, Optional b As Object = Nothing, Optional c As Object = Nothing, Optional d As Object = Nothing, Optional e As Object = Nothing)
        Dim s As String = ""
        Dim t As String = "     "
        If Not IsNothing(a) Then
            If a.GetType.Equals(String.Empty.GetType) Then s = s + a + vbTab Else s = s + a.ToString + t
        End If
        If Not IsNothing(b) Then
            If b.GetType.Equals(String.Empty.GetType) Then s = s + b + vbTab Else s = s + b.ToString + t
        End If
        If Not IsNothing(c) Then
            If c.GetType.Equals(String.Empty.GetType) Then s = s + c + vbTab Else s = s + c.ToString + t
        End If
        If Not IsNothing(d) Then
            If d.GetType.Equals(String.Empty.GetType) Then s = s + d + vbTab Else s = s + d.ToString + t
        End If
        If Not IsNothing(e) Then
            If e.GetType.Equals(String.Empty.GetType) Then s = s + e + vbTab Else s = s + e.ToString + t
        End If
        Console.WriteLine(s)
    End Sub

    '验证一个 WebBrowser 是否加载完成
    Public Function 完成Web(wb As WebBrowser, Optional bodylen As Integer = 3000) As Boolean
        完成Web = False
        Try
            If wb.Document = Nothing Then Exit Function
            If wb.ReadyState < 3 Then Exit Function
            If wb.Document.Body.InnerHtml.Length < bodylen Then Exit Function
        Catch
        End Try
        完成Web = True
    End Function

    '简易的 HTTP GET ，可以很快获得 HTML 内容
    Public Function 获得Http(url As String, Optional df As String = "ERROR") As String
        If url.Length < 3 Then Exit Function
        If Regex.IsMatch(左(url, 8), "htt(p|ps)://") = False Then url = "https://" + url
        Try
            Dim hq As HttpWebRequest = WebRequest.Create(url)
            hq.Method = "GET"
            Dim sr As StreamReader = New StreamReader(hq.GetResponse.GetResponseStream)
            获得Http = sr.ReadToEnd
            sr.Close()
        Catch
            获得Http = df
        End Try
    End Function

    '读取一张在线图片为 Image
    Public Function 在线图片(url As String) As Image
        在线图片 = Nothing
        Try
            If url.Length < 8 Then Exit Function
            If Regex.IsMatch(左(url, 8), "htt(p|ps)://") = False Then url = "https://" + url
            Dim hq As HttpWebRequest = WebRequest.Create(url)
            hq.Method = "GET"
            在线图片 = Image.FromStream(hq.GetResponse.GetResponseStream)
        Catch
        End Try
    End Function

    '使用 STMP 利用QQ的服务器发送简易的文字信息
    Public Sub 发QQ邮件(你的邮箱 As String, 密码 As String, 收信人 As String, 标题 As String, 正文 As String)
        Try
            Dim hm As SmtpClient = New SmtpClient
            With hm
                .Host = "smtp.qq.com"
                .EnableSsl = True
                .Port = 587
                .UseDefaultCredentials = False
                .Credentials = New NetworkCredential(你的邮箱, 密码)
                .Send(你的邮箱, 收信人, 标题, 正文)
            End With
        Catch ex As Exception
        End Try
    End Sub

    '检查是否已经打了补丁并且 IE 版本为最新版本
    Public Function 最新IE() As Boolean
        最新IE = False
        Dim h As WebBrowser = New WebBrowser
        Dim s As Integer = h.Version.Major
        h.Dispose()
        If s < 11 Then Exit Function
        Dim r As RegistryKey = Registry.CurrentUser.CreateSubKey("Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION")
        If (r.GetValue(程序名)) <> 11001 Then Exit Function
        最新IE = True
    End Function

    '修复多行文本框无法快捷键全选的问题
    Public Sub 文本框全选(sender As Object, e As KeyEventArgs)
        If e.Control AndAlso e.KeyCode = Keys.A Then sender.SelectAll()
    End Sub

    '使用 XML 保存用户信息，不依赖 My.Settings 方便用户卸载
    Private 设置 As XmlDocument
    Public Function 设置XML() As String
        设置XML = 设置.OuterXml
    End Function

    '读入初始信息使用，需要放在程序开启处
    Public Sub 读取设置()
        清空设置()
        Dim h As String = 程序目录() + 去右(程序名, 4) + "_Saves.XML"
        Dim s As String = 读(h)
        If 头(s, "<?xml version=""1.0"" encoding=""UTF-8""?><root>") AndAlso 尾(s, "</root>") Then
            Try
                设置.LoadXml(s)
            Catch ex As Exception
                清空设置()
            End Try
        End If
    End Sub

    '清空所有设置
    Public Sub 清空设置()
        设置 = New XmlDocument
        Dim h As String = "<?xml version=""1.0"" encoding=""UTF-8""?><root></root>"
        设置.LoadXml(h)
    End Sub

    '放在程序结束处，保存设置
    Public Sub 保存设置()
        Dim h As String = 程序目录() + 去右(程序名, 4) + "_Saves.XML"
        写(h, 设置XML)
    End Sub

    Public Sub 写设置(ID As String, Value As Object)
        Dim h As String
        If Value.GetType.Equals(String.Empty.GetType) Then h = Value Else h = Value.ToString
        Dim r As XmlNode = 设置.SelectSingleNode("root"), x As XmlNode
        x = r.SelectSingleNode(ID)
        If IsNothing(x) = False Then r.RemoveChild(x)
        x = 设置.CreateElement(ID)
        x.InnerText = h
        r.AppendChild(x)
    End Sub

    Public Function 读设置(ID As String, Optional def As String = "") As String
        Dim r As XmlNode = 设置.SelectSingleNode("root"), x As XmlNode
        x = r.SelectSingleNode(ID)
        If IsNothing(x) = False Then
            读设置 = x.InnerText
        Else
            读设置 = def
            写设置(ID, def)
        End If
    End Function

End Module
