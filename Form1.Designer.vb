<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ToolStripMenuItemAppend = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItemCopy = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckBoxSize = New System.Windows.Forms.CheckBox()
        Me.CheckBoxMD5 = New System.Windows.Forms.CheckBox()
        Me.ComboBoxPath = New System.Windows.Forms.ComboBox()
        Me.CheckBoxCrLf = New System.Windows.Forms.CheckBox()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.AllowDrop = True
        Me.TextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox1.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(12, 29)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(619, 194)
        Me.TextBox1.TabIndex = 0
        Me.TextBox1.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItemAppend, Me.ToolStripMenuItemCopy})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(643, 26)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.TabStop = True
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ToolStripMenuItemAppend
        '
        Me.ToolStripMenuItemAppend.Name = "ToolStripMenuItemAppend"
        Me.ToolStripMenuItemAppend.Size = New System.Drawing.Size(109, 22)
        Me.ToolStripMenuItemAppend.Text = "ファイル追加(&F)"
        '
        'ToolStripMenuItemCopy
        '
        Me.ToolStripMenuItemCopy.Name = "ToolStripMenuItemCopy"
        Me.ToolStripMenuItemCopy.Size = New System.Drawing.Size(194, 22)
        Me.ToolStripMenuItemCopy.Text = "クリップボードへ全てコピー(&C)"
        '
        'CheckBoxSize
        '
        Me.CheckBoxSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxSize.AutoSize = True
        Me.CheckBoxSize.Location = New System.Drawing.Point(242, 233)
        Me.CheckBoxSize.Name = "CheckBoxSize"
        Me.CheckBoxSize.Size = New System.Drawing.Size(101, 16)
        Me.CheckBoxSize.TabIndex = 3
        Me.CheckBoxSize.Text = "サイズ要る？(&S)"
        Me.CheckBoxSize.UseVisualStyleBackColor = True
        '
        'CheckBoxMD5
        '
        Me.CheckBoxMD5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxMD5.AutoSize = True
        Me.CheckBoxMD5.Location = New System.Drawing.Point(349, 233)
        Me.CheckBoxMD5.Name = "CheckBoxMD5"
        Me.CheckBoxMD5.Size = New System.Drawing.Size(121, 16)
        Me.CheckBoxMD5.TabIndex = 4
        Me.CheckBoxMD5.Text = "MD5SUM要る？(&M)"
        Me.CheckBoxMD5.UseVisualStyleBackColor = True
        '
        'ComboBoxPath
        '
        Me.ComboBoxPath.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxPath.FormattingEnabled = True
        Me.ComboBoxPath.Items.AddRange(New Object() {"そのまま", "フルパス化", "ファイル名のみ"})
        Me.ComboBoxPath.Location = New System.Drawing.Point(12, 229)
        Me.ComboBoxPath.Name = "ComboBoxPath"
        Me.ComboBoxPath.Size = New System.Drawing.Size(121, 20)
        Me.ComboBoxPath.TabIndex = 6
        '
        'CheckBoxCrLf
        '
        Me.CheckBoxCrLf.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxCrLf.AutoSize = True
        Me.CheckBoxCrLf.Location = New System.Drawing.Point(155, 232)
        Me.CheckBoxCrLf.Name = "CheckBoxCrLf"
        Me.CheckBoxCrLf.Size = New System.Drawing.Size(81, 16)
        Me.CheckBoxCrLf.TabIndex = 7
        Me.CheckBoxCrLf.Text = "改行要る？"
        Me.CheckBoxCrLf.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(643, 263)
        Me.Controls.Add(Me.CheckBoxCrLf)
        Me.Controls.Add(Me.ComboBoxPath)
        Me.Controls.Add(Me.CheckBoxMD5)
        Me.Controls.Add(Me.CheckBoxSize)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Yet Another gui Show Version"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ToolStripMenuItemAppend As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItemCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckBoxSize As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxMD5 As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxPath As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBoxCrLf As System.Windows.Forms.CheckBox

End Class
