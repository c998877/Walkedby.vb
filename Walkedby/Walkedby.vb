﻿Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Math
Imports System.TimeZoneInfo
Imports Microsoft
Imports System.Text

Module Walkedby '走過去的常用函数合集
    '修改：2018年1月1日
    '統一：简体字，数字一律整数 Integer

    Public Const 引号 As String = """"

    '左右，相当于 left right
    Public Function 左(str As String, i As Integer) As String
        If i > 0 Then 左 = Left(str, i) Else 左 = ""
    End Function
    Public Function 右(str As String, i As Integer) As String
        If i > 0 Then 右 = Right(str, i) Else 右 = ""
    End Function

    '去掉左边或右边
    Public Function 去左(str As String, i As Integer) As String
        去左 = ""
        If str.Length - i > 0 Then 去左 = Right(str, str.Length - i)
    End Function
    Public Function 去右(str As String, i As Integer) As String
        去右 = ""
        If str.Length - i > 0 Then 去右 = Left(str, str.Length - i)
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
        If str.Length = 0 Or find.Length = 0 Or str.Length < find.Length Then Exit Function
        If forceCase = False Then
            str = LCase(str)
            find = LCase(find)
        End If
        If InStr(str, find) > 0 Then 包含 = True
    End Function

    '取得范围内的随机整数
    Public Function 随机(Optional a As Integer = 1000, Optional b As Integer = 0) As Integer
        If a < b Then
            Dim t As Integer = b
            b = a
            a = t
        End If
        Randomize()
        随机 = Rnd() * (a - b） + b
    End Function

    '随机一个布林值
    Public Function 随机B() As Boolean
        Randomize()
        随机B = (Rnd() >= 0.5)
    End Function

    '随机一个颜色，但是可以固定RGBA
    Public Function 随机颜色(Optional R As Integer = 0, Optional G As Integer = 0, Optional B As Integer = 0, Optional A As Integer = 255) As Color
        If A < 1 Or A > 255 Then A = 随机(1, 255)
        If R < 1 Or R > 255 Then R = 随机(1, 255)
        If G < 1 Or G > 255 Then G = 随机(1, 255)
        If B < 1 Or B > 255 Then B = 随机(1, 255)
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
        If a.Length > 0 And 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, a, "")
        If b.Length > 0 And 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, b, "")
        If c.Length > 0 And 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, c, "")
        If d.Length > 0 And 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, d, "")
        If e.Length > 0 And 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, e, "")
        If e.Length > 0 And 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, e, "")
        If f.Length > 0 And 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, f, "")
        If g.Length > 0 And 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, g, "")
        If h.Length > 0 And 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, h, "")
        If i.Length > 0 And 正则去除.length > 0 Then 正则去除 = Regex.Replace(正则去除, i, "")
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
        回车规范 = 正则去除(str, "\r")
        回车规范 = Regex.Replace(回车规范, "\n", vbCrLf)
    End Function

    '获得小写文件后缀名，不包含第一个点
    Public Function 文件后缀(path As String) As String
        文件后缀 = ""
        If path.Length < 4 Or Not 包含(path, ".") Then Exit Function
        文件后缀 = LCase(去左(Regex.Match(文件名(path), "\..*?$").ToString, 1))
    End Function

    '获得路径或者倒数第二层文件夹
    Public Function 路径(path As String) As String
        路径 = Regex.Match(path, ".*\\").ToString
    End Function

    '去掉不应该有的符号
    Public Function 文件名规范(path As String, Optional 去斜杠 As Boolean = False) As String
        path = Replace(path, "/", "\")
        文件名规范 = 去除(path, "*", "?", "<", ">", "|", 引号)
        文件名规范 = Regex.Replace(文件名规范, "\\{2,}", "\")
        If 去斜杠 Then 文件名规范 = 去除(文件名规范, "\")
    End Function

    '从 path 中获得文件或者最后一层文件夹的名字
    Public Function 文件名(path As String) As String
        path = Replace(path, "/", "\")
        If 右(path, 1).Equals("\") Then path = 去右(path, 1)
        文件名 = 文件名规范(去除(StrReverse(Regex.Match(StrReverse(path), ".*?\\").ToString), "\"))
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
        If path.Length < 3 Or Len(a) < 1 Then Exit Sub
        Dim s As String = "cmd.exe /c " + 左(path, 2)
        If path.Length > 3 Then s = s + " & cd " + 引号 + path + 引号
        If Len(a) < 1 Then Exit Sub Else s = s + " & " + a
        If Len(b) > 0 Then s = s + " & " + b
        If Len(c) > 0 Then s = s + " & " + c
        If Len(d) > 0 Then s = s + " & " + d
        If Len(e) > 0 Then s = s + " & " + e
        Console.WriteLine(s)
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
        If 文件后缀(path).Equals("lnk") And 文件存在(path) Then
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
        程序目录 = Application.StartupPath
        If Not 右(程序目录, 1).Equals("\") Then 程序目录 = 程序目录 + "\"
    End Function

    '取得本程序的文件名
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
        If path.Length < 3 Then Exit Function
        path = 文件名规范(path)
        If 是文件夹 Then 文件存在 = Directory.Exists(path) Else 文件存在 = File.Exists(path)
    End Function

    '检测文件是否处于可写入状态
    Public Function 文件可写(path As String) As Boolean
        文件可写 = False
        If path.Length < 4 Then Exit Function
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
        If Not 文件可写(path) Then Exit Sub
        File.WriteAllText(path, str, Encoding.UTF8)
    End Sub

    '写入 str 到 path 文件的末尾
    Public Sub 附写(path As String, str As String, Optional addenter As Boolean = True)
        If Not 文件可写(path) Then Exit Sub
        If addenter Then str = vbCrLf + str
        File.AppendAllText(path, str, Encoding.UTF8)
    End Sub

    '读入 path 文件为字符串
    Public Function 读(path As String, Optional 默认 As String = "") As String
        读 = 默认
        If Not 文件存在(path) Then Exit Function
        读 = File.ReadAllText(path, Encoding.UTF8)
    End Function

    '获得 str 的行数，最少为1行
    Public Function 行数(str As String) As Integer
        str = 回车规范(str)
        行数 = 1
        If str.Length < 2 Then Exit Function
        行数 = Regex.Matches(str, ".*?(\r|$)").Count - 1
    End Function

    '读取 str 的第 i 行
    Public Function 行(str As String, i As Integer) As String
        行 = ""
        str = 回车规范(str)
        Dim mc As MatchCollection = Regex.Matches(str, ".*?(\r|$)")
        If mc.Count < 1 Then Exit Function
        Dim t As Integer = mc.Count - 1
        If i > t Then Exit Function
        行 = 去除(mc.Item(i - 1).ToString, vbCr, vbLf, vbCrLf)
    End Function

    '改变 str 的第 i 行为 change
    Public Function 改行(str As String, i As String, change As String) As String
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

    '检测 str 是否在列表型控件 l 里
    Public Function 在列表(l As Object, str As String) As Boolean
        在列表 = False
        For Each i As String In l
            If str = i Then
                在列表 = True
                Exit Function
            End If
        Next
    End Function

    '为列表型控件 l 去重
    Public Sub 列表去重(l As Object)
        If l.Items.Count < 1 Then Exit Sub
        Dim go As New ArrayList
        Dim i As Integer
        Dim i2 As Integer
        Dim i3 As Integer
        For i = 0 To l.Items.Count - 1
            For i2 = 0 To l.Items.Count - 1
                If i <> i2 And l.Items.Item(i).ToString = l.Items.Item(i2).ToString Then
                    Dim s As Boolean = False
                    For Each i3 In go
                        If i3 = i2 Then s = True
                    Next
                    If s = False Then go.Add(i2)
                End If
            Next
        Next
        If go.Count < 1 Then Exit Sub
        go.Sort()
        Dim na As String
        For i = go.Count - 1 To 0 Step -1
            na = l.Items.Item(Val(go.Item(i).ToString)).ToString
            l.Items.RemoveAt(Val(go.Item(i).ToString))
            If 在列表(l.items, na) = False Then l.Items.Add(na)
        Next
    End Sub

    '把每行文字加入到列表型控件
    Public Sub 文字转列表(str As String, l As Object)
        If str.Length < 1 Then Exit Sub
        Dim i As Integer
        For i = 1 To 行数(str)
            Dim s As String = 行(str, i).ToString
            If s.Length > 0 Then l.Items.Add(s)
        Next
    End Sub

    '把列表型控件的每一项变成一行行的文字
    Public Function 列表转文字(l As Object) As String
        Dim s As String = "", m As String = ""
        For Each s In l.Items
            m = m + s + vbCrLf
        Next
        列表转文字 = 去右(m, 2)
    End Function

    '获得UNIX时间
    Public Function UNIX时间(time1 As Date) As Long
        UNIX时间 = DateDiff("s", "1970-1-1 0:0:0", ConvertTimeToUtc(time1))
    End Function

    'UNIX时间变成普通的时间
    Public Function UNIX时间恢复(u As Long) As Date
        Dim T1970 As Date = "1970-1-1 0:0:0"
        UNIX时间恢复 = ConvertTime(T1970.Add(New TimeSpan(0, 0, 0, u)), Utc, Local)
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
        If Not (头(str, "https://") Or 头(str, "http://") Or 头(str, "file://")) Then
            str = "http://" + str
        End If
        Process.Start(str)
    End Sub

    '便捷的控制台输出，多个选项自动分开
    Public Sub 控制台(a As Object, Optional b As Object = Nothing, Optional c As Object = Nothing, Optional d As Object = Nothing, Optional e As Object = Nothing)
        Dim s As String = ""
        If Not IsNothing(a) Then s = s + a.ToString + vbTab
        If Not IsNothing(b) Then s = s + b.ToString + vbTab
        If Not IsNothing(c) Then s = s + c.ToString + vbTab
        If Not IsNothing(d) Then s = s + d.ToString + vbTab
        If Not IsNothing(e) Then s = s + e.ToString
        Console.WriteLine(s)
    End Sub

    '验证一个 WebBrowser 是否加载完成
    Public Function 完成Web(wb As WebBrowser, Optional bodylen As Integer = 2000) As Boolean
        完成Web = False
        Try
            If wb.Document = Nothing Then Exit Function
            If wb.ReadyState < 3 Then Exit Function
            If wb.Document.Body.InnerHtml.Length < bodylen Then Exit Function
        Catch
        End Try
        完成Web = True
    End Function

    '下面是走過去的存介系统，需要放置在程序开启和关闭处
    Public 存介 As String

    '抹去指定的组
    Public Sub 删组(GroupName As String)
        存介 = 正则去除(存介, "《" + GroupName + "》([\s\S]*?)《/" + GroupName + "》")
    End Sub

    '抹去指定的项
    Public Sub 删项(项名 As String, Optional GroupName As String = "Main")
        Dim G As String = Regex.Match(存介, "《" + GroupName + "》([\s\S]*?)《/" + GroupName + "》").ToString
        If G.Length < 1 Then Exit Sub
        G = 正则去除(G, "《" + 项名 + "/([\s\S]*?)/" + 项名 + "》")
        存介 = Regex.Replace(存介, "《" + GroupName + "》([\s\S]*?)《/" + GroupName + "》", G)
    End Sub

    '给指定的组，指定的项新增一个值，默认值名为DF，默认组名为 Main，规范：值的长度不能大于2000，组名必须是英文，项名必须是中文，自动生成不存在的组和项
    Public Sub 存值(项名 As String, 值 As String, Optional 值名 As String = "DF", Optional GroupName As String = "Main")
        Dim G As String = Regex.Match(存介, "《" + GroupName + "》([\s\S]*?)《/" + GroupName + "》").ToString
        If G.Length < 1 Then
            G = "《" + GroupName + "》" + vbCrLf + "《/" + GroupName + "》"
            存介 = 存介 + G + vbCrLf
        End If
        Dim s1 As String, s2 As String
        Dim S As String = Regex.Match(G, "《" + 项名 + "/([\s\S]*?)/" + 项名 + "》").ToString
        If S.Length < 1 Then
            s1 = 去右(Regex.Match(G, "《" + GroupName + "》([\s\S]*?)《/").ToString, 2)
            s2 = 去左(G, s1.Length)
            S = "《" + 项名 + "/《》/" + 项名 + "》"
            G = s1 + S + vbCrLf + s2
            存介 = Regex.Replace(存介, "《" + GroupName + "》([\s\S]*?)《/" + GroupName + "》", G)
        End If
        值 = 左(值, 2000)
        值 = Replace(值, "/", "&倾ft;")
        值 = Replace(值, "《", "&左lt;")
        值 = Replace(值, "》", "&右gt;")
        Dim V As String = ""
        S = 正则去除(S, " " + 值名 + "=《([\s\S]*?)》 ")
        If IsNothing(值) Then 值 = ""
        If 值.Length > 0 Then V = " " + 值名 + "=《" + 值 + "》 "
        s1 = Regex.Match(S, "《" + 项名 + "/《").ToString
        s2 = 去左(S, s1.Length)
        S = s1 + V + s2
        G = Regex.Replace(G, "《" + 项名 + "/([\s\S]*?)/" + 项名 + "》", S)
        存介 = Regex.Replace(存介, "《" + GroupName + "》([\s\S]*?)《/" + GroupName + "》", G)
    End Sub

    '读取指定的组的指定的项的指定的值名的值
    Public Function 读值(项名 As String, Optional 默认 As String = "", Optional 值名 As String = "DF", Optional GroupName As String = "Main") As String
        读值 = 默认
        Dim g As String = Regex.Match(存介, "《" + GroupName + "》([\s\S]*?)《/" + GroupName + "》").ToString
        If g.Length < 1 Then Exit Function
        Dim s As String = Regex.Match(g, "《" + 项名 + "/([\s\S]*?)/" + 项名 + "》").ToString
        If s.Length < 1 Then Exit Function
        Dim v As String = Regex.Match(s, " " + 值名 + "=《([\s\S]*?)》 ").ToString
        v = 去除(v, " " + 值名 + "=《" + "》 ")
        读值 = Replace(Replace(Replace(v, "&左lt;", "《"), "&右gt;括", "》"), "&倾ft;", "/")
    End Function

    '读值并转换成布林值
    Public Function 读值B(项名 As String, Optional 默认 As String = "", Optional 值名 As String = "DF", Optional GroupName As String = "Main") As Boolean
        读值B = 读值(项名, 默认, 值名, GroupName).Equals("True")
    End Function

    '读值并转换成数字
    Public Function 读值N(项名 As String, Optional 默认 As String = "", Optional 值名 As String = "DF", Optional GroupName As String = "Main") As Single
        读值N = Val(读值(项名, 默认, 值名, GroupName))
    End Function

    '开启软件时的读取
    Public Sub 存介开始(MyName As String)
        If MyName.Equals(程序名) = False Then
            MsgBox("警告：不要修改本软件的文件名！
Error: Don't change the name of program!
The original name:" + MyName, MsgBoxStyle.OkOnly + MsgBoxStyle.Exclamation)
            End
        End If
        存介 = "《走過去存介系统》请不要尝试乱改！ Please don't change any value!" + vbCrLf
        Dim x As String = 读(程序目录() + 去除(MyName, ".exe") + ".wbs")
        If Regex.IsMatch(x, "^《走過去存介系统》请不要尝试乱改！ Please don't change any value!([\s\S]*?)《Main》([\s\S]*?)《/Main》") Then
            存介 = x
        End If
        存值("最后打开时间", Now)
    End Sub

    '关闭软件时的写入
    Public Sub 存介结束()
        存值("最后保存时间", Now)
        写(程序目录() + 去除(程序名, ".exe") + ".wbs", 存介)
    End Sub

End Module

