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
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PIC = New System.Windows.Forms.PictureBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.LabPIC = New System.Windows.Forms.Label()
        Me.TxtB = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        CType(Me.PIC, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Enabled = False
        Me.Button1.Location = New System.Drawing.Point(12, 180)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(324, 39)
        Me.Button1.TabIndex = 0
        Me.Button1.TabStop = False
        Me.Button1.Text = "上传"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(162, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "JPG PNG 不大于20MB"
        '
        'PIC
        '
        Me.PIC.Location = New System.Drawing.Point(12, 39)
        Me.PIC.Name = "PIC"
        Me.PIC.Size = New System.Drawing.Size(319, 109)
        Me.PIC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PIC.TabIndex = 2
        Me.PIC.TabStop = False
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(200, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(131, 30)
        Me.Button2.TabIndex = 3
        Me.Button2.TabStop = False
        Me.Button2.Text = "剪贴板图片"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'LabPIC
        '
        Me.LabPIC.AutoSize = True
        Me.LabPIC.Font = New System.Drawing.Font("微软雅黑", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabPIC.Location = New System.Drawing.Point(8, 157)
        Me.LabPIC.Name = "LabPIC"
        Me.LabPIC.Size = New System.Drawing.Size(41, 16)
        Me.LabPIC.TabIndex = 4
        Me.LabPIC.Text = "无图片"
        '
        'TxtB
        '
        Me.TxtB.Location = New System.Drawing.Point(12, 225)
        Me.TxtB.Multiline = True
        Me.TxtB.Name = "TxtB"
        Me.TxtB.ReadOnly = True
        Me.TxtB.Size = New System.Drawing.Size(324, 150)
        Me.TxtB.TabIndex = 5
        Me.TxtB.TabStop = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(205, 348)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(131, 27)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "复制 URL"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(348, 387)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.TxtB)
        Me.Controls.Add(Me.LabPIC)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.PIC)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Font = New System.Drawing.Font("微软雅黑", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "B站也能当图床"
        CType(Me.PIC, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents PIC As PictureBox
    Friend WithEvents Button2 As Button
    Friend WithEvents LabPIC As Label
    Friend WithEvents TxtB As TextBox
    Friend WithEvents Button3 As Button
End Class
