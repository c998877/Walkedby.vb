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

    Private ID64 As String    '64位ID64
    Private Key As String   'APIKey，可以到 https://steamcommunity.com/dev/apikey 直接领取。
    Private PInfo As String = NoID '个人信息合集
    Private s As String = "personaname"
    Private level As Long

    Public Sub New(六十四位ID As String, APIkey As String)
        ID64 = 左(只要数字(六十四位ID), 17)
        Key = Trim(APIkey)
        刷新()
    End Sub

    Public Sub 刷新()
        PInfo = 获得Http("https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key=" + Key + "&steamids=" + ID64)
        If PInfo.Length < 300 Then PInfo = NoID
        level = Val(只要数字(获得Http("https://api.steampowered.com/IPlayerService/GetSteamLevel/v1/?key=" + Key + "&steamid=" + ID64, "0")))
    End Sub

    Public ReadOnly Property 昵称 As String
        Get
            Return 去左右(Regex.Match(PInfo, s + vbQuote + ": "".*?" + vbQuote + ",").ToString, s.Length + 4, 2)
        End Get
    End Property

    Public ReadOnly Property 真实姓名 As String
        Get
            s = "realname"
            If 包含(PInfo, s) Then
                Return 去左右(Regex.Match(PInfo, s + vbQuote + ": "".*?" + vbQuote + ",").ToString, s.Length + 4, 2)
            Else
                Return ""
            End If
        End Get
    End Property

    Public ReadOnly Property 主页 As String
        Get
            s = "profileurl"
            主页 = 去左右(Regex.Match(PInfo, s + vbQuote + ": "".*?" + vbQuote + ",").ToString, s.Length + 4, 2)
        End Get
    End Property

    Public ReadOnly Property 最后登录时间 As String
        Get
            s = "lastlogoff"
            最后登录时间 = UNIX时间恢复(只要数字(Regex.Match(PInfo, s + vbQuote + ":.*?,").ToString))
        End Get
    End Property

    Public ReadOnly Property 创号时间 As Date
        Get
            创号时间 = #2000-01-01#
            s = "timecreated"
            If 包含(PInfo, s) Then 创号时间 = UNIX时间恢复(只要数字(Regex.Match(PInfo, s + vbQuote + ":.*?,").ToString))
        End Get
    End Property

    Public ReadOnly Property 国家 As String
        Get
            s = "loccountrycode"
            国家 = 去左右(Regex.Match(PInfo, s + vbQuote + ": "".*?" + vbQuote + ",").ToString, s.Length + 4, 2)
        End Get
    End Property

    Public ReadOnly Property 在线状态英文 As String
        Get
            s = "personastate"
            Select Case Val(只要数字(Regex.Match(PInfo, s + vbQuote + ":.*?\n").ToString))
                Case 0
                    Return "Offline"
                Case 1
                    Return "Online"
                Case 2
                    Return "Busy"
                Case 3
                    Return "Away"
                Case 4
                    Return "Snooze"
                Case 5
                    Return "Looking to trade"
                Case 6
                    Return "Looking to play"
                Case Else
                    Return "Offline"
            End Select
        End Get
    End Property

    Public ReadOnly Property 在线状态中文 As String
        Get
            s = "personastate"
            Select Case Val(只要数字(Regex.Match(PInfo, s + vbQuote + ":.*?\n").ToString))
                Case 0
                    Return "离线"
                Case 1
                    Return "在线"
                Case 2
                    Return "忙碌"
                Case 3
                    Return "离开"
                Case 4
                    Return "打盹"
                Case 5
                    Return "想交易"
                Case 6
                    Return "想玩游戏"
                Case Else
                    Return "离线"
            End Select
        End Get
    End Property

    Public ReadOnly Property 现在游戏ID As Long
        Get
            现在游戏ID = 0
            s = "gameid"
            If 包含(PInfo, s) Then 现在游戏ID = Val(只要数字(Regex.Match(PInfo, s + vbQuote + ": " + vbQuote + ".*?" + vbQuote + ",").ToString))
        End Get
    End Property

    Public ReadOnly Property 等级 As Long
        Get
            Return level
        End Get
    End Property

    Public ReadOnly Property 头像链接(Optional size As Integer = 3) As String
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
            h = Regex.Match(h, s + vbQuote + ": "".*?" + vbQuote + ",").ToString
            头像链接 = 去左右(h, s.Length + 4, 2)
        End Get
    End Property

    Public ReadOnly Property 头像图片(Optional size As Integer = 3) As Image
        Get
            头像图片 = 在线图片(头像链接(size))
        End Get
    End Property

    Private Const NoID As String = """players"": [
{
""steamID64"": ""000"",
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

End Class
