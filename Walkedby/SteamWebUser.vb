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

    Public ID64 As String    '64位ID64
    Private Key As String   'APIKey，可以到 https://steamcommunity.com/dev/apikey 直接领取。
    Private PInfo As String  '个人信息合集
    Public 昵称 As String
    Public 真实姓名 As String
    Public 主页 As String
    Public 国家 As String
    Public 最后登录时间 As Date
    Public 创号时间 As Date
    Public 在线状态 As String
    Public 现在游戏ID As Long

    Public Sub New(ID As String, APIkey As String)
        ID64 = 只要数字(ID)
        If ID64.Length <> 17 Then
            Dim get64 As String = 获得Http("https://api.steampowered.com/ISteamUser/ResolveVanityURL/v1/?key=" + APIkey + "&vanityurl=" + ID)
            get64 = Regex.Match(get64, "[0-9]{17}").ToString
            If get64.Length = 17 Then ID64 = get64 Else ID64 = "ERROR"
        End If
        Key = Trim(APIkey)
        刷新()
    End Sub

    Public Sub 刷新()
        PInfo = 获得Http("https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key=" + Key + "&steamids=" + ID64)
        If PInfo.Length < 300 Then PInfo = NoID
        Dim s As String = "personaname"
        昵称 = 去左右(Regex.Match(PInfo, s + 引号 + ": "".*?" + 引号 + ",").ToString, s.Length + 4, 2)
        真实姓名 = ""
        s = "realname"
        If 包含(PInfo, s) Then 真实姓名 = 去左右(Regex.Match(PInfo, s + 引号 + ": "".*?" + 引号 + ",").ToString, s.Length + 4, 2)
        s = "profileurl"
        主页 = 去左右(Regex.Match(PInfo, s + 引号 + ": "".*?" + 引号 + ",").ToString, s.Length + 4, 2)
        s = "lastlogoff"
        最后登录时间 = UNIX时间恢复(只要数字(Regex.Match(PInfo, s + 引号 + ":.*?,").ToString))
        创号时间 = #2000-01-01#
        s = "timecreated"
        If 包含(PInfo, s) Then 创号时间 = UNIX时间恢复(只要数字(Regex.Match(PInfo, s + 引号 + ":.*?,").ToString))
        s = "loccountrycode"
        国家 = 去左右(Regex.Match(PInfo, s + 引号 + ": "".*?" + 引号 + ",").ToString, s.Length + 4, 2)
        s = "personastate"
        Select Case Val(只要数字(Regex.Match(PInfo, s + 引号 + ":.*?\n").ToString))
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
        现在游戏ID = 0
        s = "gameid"
        If 包含(PInfo, s) Then 现在游戏ID = Val(只要数字(Regex.Match(PInfo, s + 引号 + ": " + 引号 + ".*?" + 引号 + ",").ToString))
    End Sub

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
            h = Regex.Match(h, s + 引号 + ": "".*?" + 引号 + ",").ToString
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
