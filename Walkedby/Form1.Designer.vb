<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 覆寫 Dispose 以清除元件清單。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    '為 Windows Form 設計工具的必要項
    Private components As System.ComponentModel.IContainer

    '注意: 以下為 Windows Form 設計工具所需的程序
    '可以使用 Windows Form 設計工具進行修改。
    '請勿使用程式碼編輯器進行修改。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.WB = New System.Windows.Forms.WebBrowser()
        Me.LanguageList = New System.Windows.Forms.ListBox()
        Me.WorkList = New System.Windows.Forms.ListBox()
        Me.PB = New System.Windows.Forms.ProgressBar()
        Me.ButX = New System.Windows.Forms.Button()
        Me.RR = New System.Windows.Forms.RichTextBox()
        Me.SuspendLayout()
        '
        'Timer1
        '
        Me.Timer1.Interval = 500
        Me.Timer1.Tag = "下个addon"
        '
        'WB
        '
        Me.WB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WB.Location = New System.Drawing.Point(0, 0)
        Me.WB.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WB.Name = "WB"
        Me.WB.ScriptErrorsSuppressed = True
        Me.WB.Size = New System.Drawing.Size(858, 533)
        Me.WB.TabIndex = 0
        Me.WB.Url = New System.Uri("", System.UriKind.Relative)
        '
        'LanguageList
        '
        Me.LanguageList.FormattingEnabled = True
        Me.LanguageList.ItemHeight = 21
        Me.LanguageList.Items.AddRange(New Object() {"0"})
        Me.LanguageList.Location = New System.Drawing.Point(26, 12)
        Me.LanguageList.Name = "LanguageList"
        Me.LanguageList.Size = New System.Drawing.Size(110, 214)
        Me.LanguageList.TabIndex = 1
        '
        'WorkList
        '
        Me.WorkList.FormattingEnabled = True
        Me.WorkList.ItemHeight = 21
        Me.WorkList.Items.AddRange(New Object() {"919345462", "917724774", "912967700", "912365648", "903021980", "898838725", "675575381", "898827150", "675573801", "898810976", "675570485", "893885489", "886651153", "876656690", "872597421", "872441048", "871610058", "868948353", "866172091", "862311862", "861513118", "860656348", "859456633", "858859243", "853373042", "846802547", "839981529", "838575689", "834368988", "825541626", "825481096", "802336368", "798923281", "797377960", "788524872", "785700755", "775263905" & Global.Microsoft.VisualBasic.ChrW(9) & Global.Microsoft.VisualBasic.ChrW(9)})
        Me.WorkList.Location = New System.Drawing.Point(157, 12)
        Me.WorkList.Name = "WorkList"
        Me.WorkList.Size = New System.Drawing.Size(153, 109)
        Me.WorkList.TabIndex = 2
        '
        'PB
        '
        Me.PB.Location = New System.Drawing.Point(320, 11)
        Me.PB.Name = "PB"
        Me.PB.Size = New System.Drawing.Size(508, 23)
        Me.PB.TabIndex = 3
        '
        'ButX
        '
        Me.ButX.Location = New System.Drawing.Point(325, 40)
        Me.ButX.Name = "ButX"
        Me.ButX.Size = New System.Drawing.Size(118, 49)
        Me.ButX.TabIndex = 4
        Me.ButX.Text = "暂停"
        Me.ButX.UseVisualStyleBackColor = True
        '
        'RR
        '
        Me.RR.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RR.Font = New System.Drawing.Font("微软雅黑", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RR.Location = New System.Drawing.Point(12, 153)
        Me.RR.Name = "RR"
        Me.RR.Size = New System.Drawing.Size(480, 368)
        Me.RR.TabIndex = 5
        Me.RR.Text = "dsa"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(858, 533)
        Me.Controls.Add(Me.RR)
        Me.Controls.Add(Me.ButX)
        Me.Controls.Add(Me.PB)
        Me.Controls.Add(Me.WorkList)
        Me.Controls.Add(Me.LanguageList)
        Me.Controls.Add(Me.WB)
        Me.Font = New System.Drawing.Font("微软雅黑", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(5)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Form1"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Timer1 As Timer
    Friend WithEvents WB As WebBrowser
    Friend WithEvents LanguageList As ListBox
    Friend WithEvents WorkList As ListBox
    Friend WithEvents PB As ProgressBar
    Friend WithEvents ButX As Button
    Friend WithEvents RR As RichTextBox
End Class
