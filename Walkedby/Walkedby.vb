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

    Public Const vbQuote As String = """"

    '屏幕的宽高
    Public ReadOnly Property 屏幕宽 As Integer
        Get
            Return My.Computer.Screen.Bounds.Width
        End Get
    End Property
    Public ReadOnly Property 屏幕高 As Integer
        Get
            Return My.Computer.Screen.Bounds.Height
        End Get
    End Property
    Public ReadOnly Property 屏幕尺寸 As Size
        Get
            Return New Size With {.Width = 屏幕宽, .Height = 屏幕高}
        End Get
    End Property

    '获得屏幕的截图
    Public Function 截屏() As Bitmap
        Dim b As Bitmap = New Bitmap(屏幕宽, 屏幕高)
        Dim g As Graphics = Graphics.FromImage(b)
        g.CopyFromScreen(0, 0, 0, 0, 屏幕尺寸, CopyPixelOperation.SourceCopy)
        截屏 = b
    End Function

    '压缩保存 JPG 图片
    Public Sub 保存JPG(i As Bitmap, path As String, Optional quality As Integer = 90)
        If IsNothing(i) Then Exit Sub
        If path.Length < 5 Then Exit Sub
        If Not 文件存在(路径(path), True) Then Directory.CreateDirectory(路径(path))
        path = 文件名规范(path)
        Dim h As New Imaging.EncoderParameters
        h.Param(0) = New Imaging.EncoderParameter(Imaging.Encoder.Quality, quality)
        Dim m() As Imaging.ImageCodecInfo = Imaging.ImageCodecInfo.GetImageDecoders
        Dim s As Imaging.ImageCodecInfo = Nothing
        For Each s In m
            If s.MimeType.Equals("image/jpeg") Then Exit For
        Next
        If Not IsNothing(s) Then i.Save(path, s, h)
    End Sub

    '把 jpg 的 bitmap 变成 base64 字符串
    Public Function 图片转base64(i As Bitmap, Optional HeadStr As Boolean = False, Optional quality As Integer = 90) As String
        图片转base64 = ""
        If IsNothing(i) Then Exit Function
        Dim h As New Imaging.EncoderParameters
        h.Param(0) = New Imaging.EncoderParameter(Imaging.Encoder.Quality, quality)
        Dim m() As Imaging.ImageCodecInfo = Imaging.ImageCodecInfo.GetImageDecoders
        Dim s As Imaging.ImageCodecInfo = Nothing
        For Each s In m
            If s.MimeType.Equals("image/jpeg") Then Exit For
        Next
        Dim ms As New MemoryStream
        If Not IsNothing(s) Then i.Save(ms, s, h)
        图片转base64 = Convert.ToBase64String(ms.ToArray)
        If HeadStr Then 图片转base64 = "data:image/jpeg;base64," + 图片转base64
        ms.Close()
    End Function

    '把 nothing 字符串变成空字符串
    Public Sub 空字符(ByRef a As String, Optional ByRef b As String = Nothing, Optional ByRef c As String = Nothing, Optional ByRef d As String = Nothing, Optional ByRef e As String = Nothing)
        Dim h As String = ""
        If IsNothing(a) Then a = h
        If IsNothing(b) Then b = h
        If IsNothing(c) Then c = h
        If IsNothing(d) Then d = h
        If IsNothing(e) Then e = h
    End Sub

    '把 nothing 字符串变成一个空格的字符串
    Public Sub 非空字符(ByRef a As String, Optional ByRef b As String = Nothing, Optional ByRef c As String = Nothing, Optional ByRef d As String = Nothing, Optional ByRef e As String = Nothing)
        Dim h As String = " "
        If IsNothing(a) Then a = h
        If IsNothing(b) Then b = h
        If IsNothing(c) Then c = h
        If IsNothing(d) Then d = h
        If IsNothing(e) Then e = h
    End Sub

    '文字与字节互换
    Public Function 文字转字节(str As String) As Byte()
        文字转字节 = Encoding.UTF8.GetBytes(str)
    End Function
    Public Function 字节转文字(s As Byte()) As String
        字节转文字 = Encoding.UTF8.GetString(s)
    End Function

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

    '把字符串变成 "字符串"
    Public Function 引(str As String) As String
        引 = vbQuote + str + vbQuote
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

    '把数字格式化为指定小数位数的字符串
    Public Function 标准数字(i As Double, Optional 小数 As Integer = 2) As String
        If 小数 < 0 Then 小数 = 0
        If 小数 > 15 Then 小数 = 15
        Dim h As String = Trim(Str(i))
        Dim r As String = 左(只要数字(Regex.Match(h, "\.[0-9].*$").ToString + "000000000000000000000000"), 小数)
        标准数字 = Regex.Match(h, "[0-9].*\.").ToString + r
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
        文件格式 = LCase(去左(Regex.Match(path, "\.+.*").ToString, 1))
    End Function

    '获得路径或者倒数第二层文件夹
    Public Function 路径(path As String) As String
        路径 = Regex.Match(path, ".*\\").ToString
    End Function

    '去掉不应该有的符号
    Public Function 文件名规范(path As String) As String
        文件名规范 = ""
        If path.Length < 3 Then Exit Function
        path = Replace(path, "/", "\")
        Dim h As String = 文件名(path)
        path = 去右(path, h)
        文件名规范 = path + 去除(h, "*", "?", "<", ">", "|", vbQuote, "\", ":")
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
        s = s + " & cd " + vbQuote + LCase(path) + vbQuote
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
        Dim s As String = vbQuote + path + vbQuote
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
        path = 文件名规范(path)
        If 文件存在(路径(path), False) = False Then Directory.CreateDirectory(路径(path))
        If Not 文件可写(path) Then Exit Sub
        File.WriteAllText(path, str, Text.Encoding.UTF8)
    End Sub

    '写入 str 到 path 文件的末尾
    Public Sub 附写(path As String, str As String, Optional AddEnter As Boolean = True)
        path = Replace(path, "/", "\")
        If Not 文件可写(path) Then Exit Sub
        If AddEnter Then str = vbCrLf + str
        File.AppendAllText(path, str, Text.Encoding.UTF8)
    End Sub

    '检查指定名字的程序是否在运行状态，不要后缀名
    Public Function 程序运行中(Pname As String) As Boolean
        程序运行中 = False
        Pname = 去右(Pname, 文件格式(Pname).Length + 1)
        Dim x As Process() = Process.GetProcessesByName(Pname)
        If x.Count > 0 Then 程序运行中 = True
    End Function

    '读入 path 文件为字符串
    Public Function 读(path As String, Optional 默认 As String = "") As String
        path = 文件名规范(path)
        读 = 默认
        If Not 文件存在(path) Then Exit Function
        Try
            读 = File.ReadAllText(path, Text.Encoding.UTF8)
        Catch
            读 = 默认
        End Try
    End Function

    '读入 path 文件为比特
    Public Function 读比特(path As String) As Byte()
        Dim m(1) As Byte
        读比特 = m
        path = 文件名规范(path)
        If Not 文件存在(path) Then Exit Function
        Try
            Dim i As New BinaryReader(File.OpenRead(path))
            读比特 = i.ReadBytes(文件大小("e:\1.jpg", "b"))
            i.Close()
        Catch
        End Try
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
        Process.Start(str)
    End Sub

    '便捷的控制台输出，多个选项自动分开
    Public Sub 控制台(a As Object, Optional b As Object = Nothing, Optional c As Object = Nothing, Optional d As Object = Nothing, Optional e As Object = Nothing)
        Dim s As String = ""
        Dim t As String = "     "
        If Not IsNothing(a) Then
            s = s + a.ToString + t
        End If
        If Not IsNothing(b) Then
            s = s + b.ToString + t
        End If
        If Not IsNothing(c) Then
            s = s + c.ToString + t
        End If
        If Not IsNothing(d) Then
            s = s + d.ToString + t
        End If
        If Not IsNothing(e) Then
            s = s + e.ToString + t
        End If
        Console.WriteLine(s)
    End Sub

    '验证一个 WebBrowser 是否加载完成
    Public Function 完成Web(wb As WebBrowser, Optional bodylen As Integer = 3000, Optional fully As Boolean = False) As Boolean
        完成Web = False
        Try
            If wb.Document = Nothing Then Exit Function
            Dim i As Integer = 3
            If fully Then i = 4
            If wb.ReadyState < i Then Exit Function
            If wb.Document.Body.InnerHtml.Length < bodylen Then Exit Function
        Catch
        End Try
        完成Web = True
    End Function

    '简易的 HTTP GET ，可以很快获得 HTML 内容
    Public Function 获得Http(url As String, Optional df As String = "ERROR") As String
        获得Http = df
        If url.Length < 8 Then Exit Function
        Try
            Dim hq As HttpWebRequest = WebRequest.Create(url)
            hq.Method = "GET"
            hq.UserAgent = "Chrome"
            Dim sr As StreamReader = New StreamReader(hq.GetResponse.GetResponseStream)
            获得Http = sr.ReadToEnd
            sr.Close()
        Catch ex As Exception
            获得Http = df
        End Try
    End Function

    '把 Webbrowser 当成视频播放器
    Public Sub 本地视频Web播放器(wb As WebBrowser, path As String, Optional Controls As Boolean = True, Optional AutoPlay As Boolean = True, Optional AutoSize As Boolean = True)
        If 文件存在(path) Then
            Dim i As String = 文件格式(path)
            If i.Equals("mp4") OrElse i.Equals("webm") OrElse i.Equals("ogg") Then
                'wb.Refresh()
                Dim s As String = "<video src=" + vbQuote + path + vbQuote + " "
                If Controls Then s += "controls=" + vbQuote + "controls" + vbQuote + " "
                If AutoSize Then
                    s += "width=" + vbQuote + wb.Width.ToString + vbQuote + " "
                    s += "height=" + vbQuote + wb.Height.ToString + vbQuote + " "
                End If
                If AutoPlay Then s += "autoplay=" + vbQuote + "autoplay" + vbQuote + " "
                s = s + " />"
                wb.DocumentText = s
            End If
        End If
    End Sub

    '读取一张在线图片为 Image
    Public Function 在线图片(url As String) As Image
        在线图片 = Nothing
        If url.Length < 8 Then Exit Function
        Try
            Dim hq As HttpWebRequest = WebRequest.Create(url)
            hq.Method = "GET"
            在线图片 = Image.FromStream(hq.GetResponse.GetResponseStream)
        Catch
        End Try
    End Function

    '使用 STMP 利用QQ的服务器发送简易的文字信息
    Public Sub 发QQ邮件(你的邮箱 As String, 密码 As String, 收信人 As String, 标题 As String, 正文 As String, Optional 使用HTML As Boolean = False)
        Try
            Dim hm As New SmtpClient
            With hm
                .Host = "smtp.qq.com"
                .EnableSsl = True
                .Port = 587
                .UseDefaultCredentials = False
                .Credentials = New NetworkCredential(你的邮箱, 密码)
            End With
            Dim m As New MailMessage
            With m
                .From = New MailAddress(你的邮箱)
                .To.Add(收信人)
                .Subject = 标题
                .Body = 正文
                .IsBodyHtml = 使用HTML
            End With
            hm.Send(m)
        Catch
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

End Module