<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
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

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtBack = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TxtHistory = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Pic = New System.Windows.Forms.PictureBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.Pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(230, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "拖入不大于 5MB 的图片文件到这"
        '
        'TxtBack
        '
        Me.TxtBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.TxtBack.Font = New System.Drawing.Font("微软雅黑", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBack.Location = New System.Drawing.Point(12, 32)
        Me.TxtBack.Name = "TxtBack"
        Me.TxtBack.ReadOnly = True
        Me.TxtBack.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtBack.Size = New System.Drawing.Size(344, 23)
        Me.TxtBack.TabIndex = 2
        Me.TxtBack.TabStop = False
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(362, 32)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(79, 34)
        Me.Button1.TabIndex = 3
        Me.Button1.TabStop = False
        Me.Button1.Text = "COPY"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.Location = New System.Drawing.Point(175, 61)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(155, 26)
        Me.Button2.TabIndex = 4
        Me.Button2.TabStop = False
        Me.Button2.Text = "删除该图"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TxtHistory
        '
        Me.TxtHistory.Font = New System.Drawing.Font("微软雅黑", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHistory.Location = New System.Drawing.Point(12, 310)
        Me.TxtHistory.Multiline = True
        Me.TxtHistory.Name = "TxtHistory"
        Me.TxtHistory.ReadOnly = True
        Me.TxtHistory.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TxtHistory.Size = New System.Drawing.Size(429, 81)
        Me.TxtHistory.TabIndex = 6
        Me.TxtHistory.TabStop = False
        Me.TxtHistory.Text = "历史记录："
        '
        'Button3
        '
        Me.Button3.Enabled = False
        Me.Button3.Location = New System.Drawing.Point(12, 61)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(155, 26)
        Me.Button3.TabIndex = 7
        Me.Button3.TabStop = False
        Me.Button3.Text = "取消上传"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Pic
        '
        Me.Pic.Image = Global.Walkedby.My.Resources.Resources.smms
        Me.Pic.Location = New System.Drawing.Point(12, 93)
        Me.Pic.Name = "Pic"
        Me.Pic.Size = New System.Drawing.Size(429, 211)
        Me.Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.Pic.TabIndex = 5
        Me.Pic.TabStop = False
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(343, 61)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(97, 25)
        Me.Button4.TabIndex = 8
        Me.Button4.Text = "SM.MS"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(248, 3)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(192, 26)
        Me.Button5.TabIndex = 9
        Me.Button5.TabStop = False
        Me.Button5.Text = "导入剪贴板图片"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        '
        'Form1
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(453, 403)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.TxtHistory)
        Me.Controls.Add(Me.Pic)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TxtBack)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Powered by SM.MS"
        CType(Me.Pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents TxtBack As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Pic As PictureBox
    Friend WithEvents TxtHistory As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Timer1 As Timer
End Class
