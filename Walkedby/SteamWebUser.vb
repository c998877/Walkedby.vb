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

Public Class Steam用户    '一个神奇的 Steam Web API
    '参考资料：https://partner.steamgames.com/doc/webapi/ISteamUser  
    '参考资料：https://developer.valvesoftware.com/wiki/Steam_Web_API

    Private ID As String    '64位ID
    Private Key As String   'APIKey，可以到 https://steamcommunity.com/dev/apikey 直接领取。
    Private PInfo As String  '个人信息合集

    Public Sub New(ID64 As String, APIkey As String)
        ID = 只要数字(ID64)
        Key = Trim(APIkey)
        刷新()
    End Sub

    Public Sub 刷新()
        PInfo = 获得Http("http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key=" + Key + "&steamids=" + ID)
        If PInfo.Length < 300 Then PInfo = """players"": [
{
""steamid"": ""000"",
""communityvisibilitystate"": 0,
""profilestate"": 0,
""personaname"": ""ERROR"",
""lastlogoff"": 0,
""commentpermission"": 0,
""profileurl"": ""ERROR"",
""avatar"": ""ERROR"",
""avatarmedium"": ""ERROR"",
""avatarfull"": ""ERROR"",
""personastate"": 0
}"
    End Sub

    Public ReadOnly Property 昵称 As String
        Get
            Dim h As String = PInfo
            Dim s As String = "personaname"
            h = Regex.Match(h, s + 引号 + ": "".*?" + 引号 + ",").ToString
            昵称 = 去左右(h, s.Length + 4, 2)
        End Get
    End Property

    Public ReadOnly Property 真实姓名 As String
        Get
            Dim h As String = PInfo
            Dim s As String = "realname"
            If 包含(h, s) Then
                h = Regex.Match(h, s + 引号 + ": "".*?" + 引号 + ",").ToString
                真实姓名 = 去左右(h, s.Length + 4, 2)
            Else
                真实姓名 = ""
            End If
        End Get
    End Property

    Public ReadOnly Property 主页 As String
        Get
            Dim h As String = PInfo
            Dim s As String = "profileurl"
            h = Regex.Match(h, s + 引号 + ": "".*?" + 引号 + ",").ToString
            主页 = 去左右(h, s.Length + 4, 2)
        End Get
    End Property

    Public ReadOnly Property 最后登录时间 As Date
        Get
            Dim h As String = PInfo
            Dim s As String = "lastlogoff"
            h = 只要数字(Regex.Match(h, s + 引号 + ":.*?,").ToString)
            最后登录时间 = UNIX时间恢复(h)
        End Get
    End Property

    Public ReadOnly Property 创号时间 As Date
        Get
            Dim h As String = PInfo
            Dim s As String = "timecreated"
            If 包含(h, s) Then
                h = 只要数字(Regex.Match(h, s + 引号 + ":.*?,").ToString)
                创号时间 = UNIX时间恢复(h)
            Else
                创号时间 = #2000-01-01#
            End If
        End Get
    End Property

    Public ReadOnly Property 国家 As String
        Get
            Dim h As String = PInfo
            Dim s As String = "loccountrycode"
            If 包含(h, s) Then
                h = Regex.Match(h, s + 引号 + ": "".*?" + 引号 + ",").ToString
                国家 = 去左右(h, s.Length + 4, 2)
            Else
                国家 = ""
            End If
        End Get
    End Property

    Public ReadOnly Property 头像(Optional size As Integer = 1) As String
        Get
            Dim s As String
            Select Case size
                Case 1
                    s = "avatar"
                Case 2
                    s = "avatarmedium"
                Case Else
                    s = "avatarfull"
            End Select
            Dim h As String = PInfo
            h = Regex.Match(h, s + 引号 + ": "".*?" + 引号 + ",").ToString
            头像 = 去左右(h, s.Length + 4, 2)
        End Get
    End Property

    Public ReadOnly Property 在线状态 As String
        Get
            Dim h As String = PInfo
            Dim s As String = "personastate"
            控制台(Regex.Match(h, s + 引号 + ":.*?\r").ToString)
            Select Case Val(只要数字(Regex.Match(h, s + 引号 + ":.*?\n").ToString))
                Case 0
                    在线状态 = "Offline"
                Case 1
                    在线状态 = "Online"
                Case 2
                    在线状态 = "Busy"
                Case 3
                    在线状态 = "Away"
                Case 4
                    在线状态 = "Snooze"
                Case 5
                    在线状态 = "Looking to trade"
                Case 6
                    在线状态 = "Looking to play"
                Case Else
                    在线状态 = "Offline"
            End Select
        End Get
    End Property

    Public ReadOnly Property 现在游戏ID() As String
        Get
            Dim h As String = PInfo
            Dim s As String = "gameid"
            If 包含(h, s) Then
                现在游戏ID = 只要数字(Regex.Match(h, s + 引号 + ": " + 引号 + ".*?" + 引号 + ",").ToString)
            Else
                现在游戏ID = ""
            End If
        End Get
    End Property

End Class
