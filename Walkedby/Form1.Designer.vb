<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Wbank = New System.Windows.Forms.WebBrowser()
        Me.Always = New System.Windows.Forms.Timer(Me.components)
        Me.BankRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.LabBank = New System.Windows.Forms.Label()
        Me.AutoR = New System.Windows.Forms.Timer(Me.components)
        Me.Wphone = New System.Windows.Forms.WebBrowser()
        Me.LabPhone = New System.Windows.Forms.Label()
        Me.PhoneRefresh = New System.Windows.Forms.Timer(Me.components)
        Me.ButBack = New System.Windows.Forms.Button()
        Me.LabHoliday = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Wbank
        '
        Me.Wbank.Location = New System.Drawing.Point(12, 84)
        Me.Wbank.MinimumSize = New System.Drawing.Size(20, 20)
        Me.Wbank.Name = "Wbank"
        Me.Wbank.ScriptErrorsSuppressed = True
        Me.Wbank.Size = New System.Drawing.Size(497, 177)
        Me.Wbank.TabIndex = 0
        Me.Wbank.Url = New System.Uri("", System.UriKind.Relative)
        '
        'Always
        '
        Me.Always.Enabled = True
        '
        'BankRefresh
        '
        Me.BankRefresh.Interval = 800
        Me.BankRefresh.Tag = "登录银行"
        '
        'LabBank
        '
        Me.LabBank.AutoSize = True
        Me.LabBank.Location = New System.Drawing.Point(12, 9)
        Me.LabBank.Name = "LabBank"
        Me.LabBank.Size = New System.Drawing.Size(129, 20)
        Me.LabBank.TabIndex = 3
        Me.LabBank.Text = "建设银行卡余额："
        '
        'AutoR
        '
        Me.AutoR.Enabled = True
        Me.AutoR.Interval = 30000
        '
        'Wphone
        '
        Me.Wphone.Location = New System.Drawing.Point(16, 267)
        Me.Wphone.MinimumSize = New System.Drawing.Size(20, 20)
        Me.Wphone.Name = "Wphone"
        Me.Wphone.ScriptErrorsSuppressed = True
        Me.Wphone.Size = New System.Drawing.Size(493, 195)
        Me.Wphone.TabIndex = 4
        Me.Wphone.Url = New System.Uri("", System.UriKind.Relative)
        '
        'LabPhone
        '
        Me.LabPhone.AutoSize = True
        Me.LabPhone.Location = New System.Drawing.Point(12, 29)
        Me.LabPhone.Name = "LabPhone"
        Me.LabPhone.Size = New System.Drawing.Size(84, 20)
        Me.LabPhone.TabIndex = 5
        Me.LabPhone.Text = "话费余额："
        '
        'PhoneRefresh
        '
        Me.PhoneRefresh.Interval = 800
        Me.PhoneRefresh.Tag = "登录"
        '
        'ButBack
        '
        Me.ButBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButBack.Location = New System.Drawing.Point(357, 9)
        Me.ButBack.Name = "ButBack"
        Me.ButBack.Size = New System.Drawing.Size(145, 29)
        Me.ButBack.TabIndex = 6
        Me.ButBack.Text = "显示浏览器本体"
        Me.ButBack.UseVisualStyleBackColor = True
        '
        'LabHoliday
        '
        Me.LabHoliday.AutoSize = True
        Me.LabHoliday.Location = New System.Drawing.Point(12, 49)
        Me.LabHoliday.Name = "LabHoliday"
        Me.LabHoliday.Size = New System.Drawing.Size(114, 20)
        Me.LabHoliday.TabIndex = 7
        Me.LabHoliday.Text = "寒假已经过了："
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(514, 473)
        Me.Controls.Add(Me.LabHoliday)
        Me.Controls.Add(Me.ButBack)
        Me.Controls.Add(Me.LabPhone)
        Me.Controls.Add(Me.Wphone)
        Me.Controls.Add(Me.LabBank)
        Me.Controls.Add(Me.Wbank)
        Me.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "查余额"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Wbank As WebBrowser
    Friend WithEvents Always As Timer
    Friend WithEvents BankRefresh As Timer
    Friend WithEvents LabBank As Label
    Friend WithEvents AutoR As Timer
    Friend WithEvents Wphone As WebBrowser
    Friend WithEvents LabPhone As Label
    Friend WithEvents PhoneRefresh As Timer
    Friend WithEvents ButBack As Button
    Friend WithEvents LabHoliday As Label
End Class
