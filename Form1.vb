Imports System.Security.Cryptography

Public Class Form1
    Const FILE_ADDRESS_OF_NEW_EXE_HEADER As Integer = 60

    Public PathList As New List(Of String)

    Private stamp As New Dictionary(Of String, DateTime)
    Private md5cache As New Dictionary(Of String, String)

    Private Shared Function VersionFilter(ByVal src As String) As String
        Return If(src IsNot Nothing, src.Replace(",", ".").Replace(" ", ""), "(not executable)")
    End Function

    Private Shared Function Bytes2Dword(ByVal Array As Byte()) As Int64
        Return CType(Array(0) + Array(1) * 256 + Array(2) * 256 * 256 + Array(3) * 256 * 256 * 256, Int64)
    End Function

    Private Shared Function Bytes2Word(ByVal array As Byte()) As Int32
        Return CType(array(0) + array(1) * 256, Int32)
    End Function

    Private Shared Function GetBit(r As System.IO.FileStream) As Integer
        Dim array = New Byte(6) {}
        r.Seek(FILE_ADDRESS_OF_NEW_EXE_HEADER, System.IO.SeekOrigin.Begin)
        r.Read(array, 0, 4)
        Dim offset = Bytes2Dword(array)
        r.Seek(offset, IO.SeekOrigin.Begin)
        r.Read(array, 0, 6)
        If array(0) = &H50 AndAlso array(1) = &H45 Then
            If array(4) = &H4C AndAlso array(5) = 1 Then
                Return 32
            ElseIf array(4) = &H64 AndAlso array(5) = &H86 Then
                Return 64
            End If
        End If
        Return 0
    End Function

    Private Sub UpdateTextBox()
        Cursor.Current = Cursors.WaitCursor
        Dim buffer As New System.Text.StringBuilder()
        For Each path1 As String In PathList
            Dim bit As Integer = -1
            If buffer.Length > 0 Then
                buffer.AppendLine()
            End If
            '*** パス ***
            Dim path_ As String
            Select Case Me.ComboBoxPath.SelectedIndex
                Case 2
                    path_ = System.IO.Path.GetFileName(path1)
                Case 1
                    path_ = System.IO.Path.GetFullPath(path1)
                Case Else
                    path_ = path1
            End Select
            If CheckBoxCrLf.Checked Then
                buffer.AppendLine(path_)
                buffer.Append(vbTab)
            Else
                buffer.AppendFormat("{0,-16} ", path_)
            End If

            If System.IO.File.Exists(path1) Then
                Dim vi As System.Diagnostics.FileVersionInfo =
                    System.Diagnostics.FileVersionInfo.GetVersionInfo(path1)

                If vi.FileVersion IsNot Nothing OrElse vi.ProductVersion IsNot Nothing Then
                    buffer.AppendFormat("{0,-16} {1,-16} ",
                    VersionFilter(vi.FileVersion),
                    VersionFilter(vi.ProductVersion))

                    Dim stamp1 As DateTime
                    If Not stamp.TryGetValue(path1, stamp1) Then
                        Using r = System.IO.File.OpenRead(path1)
                            ' Read MZ Header
                            Dim array As Byte() = New Byte(4) {}
                            r.Seek(FILE_ADDRESS_OF_NEW_EXE_HEADER, IO.SeekOrigin.Begin)
                            r.Read(array, 0, 4)
                            Dim dword As Integer = Bytes2Dword(array)
                            ' Move PE Header
                            r.Seek(dword + 8, IO.SeekOrigin.Begin)
                            r.Read(array, 0, 4)
                            stamp1 = New DateTime(1970, 1, 1, 0, 0, 0)
                            stamp1 = stamp1.AddSeconds(Bytes2Dword(array)).ToLocalTime()
                            stamp(path1) = stamp1

                            bit = GetBit(r)
                        End Using
                    End If
                    buffer.AppendFormat("{0:D2}-{1:D02}-{2:D2} {3:D2}:{4:D2}:{5:D2}",
                                        stamp1.Year, stamp1.Month, stamp1.Day,
                                        stamp1.Hour, stamp1.Minute, stamp1.Second)
                End If

                Dim emptyline As Boolean = False
                Dim fi As New System.IO.FileInfo(path1)
                If CheckBoxSize.Checked Then
                    buffer.AppendFormat("{0}{1}{2} bytes", vbCrLf, vbTab, fi.Length)
                    emptyline = True
                End If
                If CheckBoxMD5.Checked Then
                    If Not CheckBoxSize.Checked Then
                        buffer.AppendLine()
                    End If
                    Dim md5sum As String = Nothing
                    If Not md5cache.TryGetValue(path1, md5sum) Then
                        Using md5_ = MD5.Create()
                            Using stream_ = System.IO.File.OpenRead(path1)
                                md5sum = BitConverter.ToString(md5_.ComputeHash(stream_)).Replace("-", "").ToLower()
                                If bit < 0 Then
                                    bit = GetBit(stream_)
                                End If
                            End Using
                        End Using
                        md5cache(path1) = md5sum
                    End If
                    buffer.AppendFormat("{0}md5sum:{1}", vbTab, md5sum)
                    If bit < 0 Then
                        Using reader_ = System.IO.File.OpenRead(path1)
                            bit = GetBit(reader_)
                        End Using
                    End If
                    If bit > 0 AndAlso Me.CheckBoxBit.Checked Then
                        buffer.AppendFormat("{0}({1}bit)", vbTab, bit)
                    End If
                    emptyline = True
                End If
                If emptyline Then
                    buffer.AppendLine()
                End If
            Else
                buffer.Append("(file not found)")
            End If
            Me.Update()
        Next
        TextBox1.Text = buffer.ToString()
    End Sub

    Private Sub CheckBoxFullPath_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
         CheckBoxSize.CheckedChanged,
         CheckBoxMD5.CheckedChanged, _
         ComboBoxPath.SelectedIndexChanged,
         CheckBoxCrLf.CheckedChanged,
         CheckBoxBit.CheckedChanged

        UpdateTextBox()
    End Sub

    Private Sub ToolStripMenuItemAppend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemAppend.Click
        Dim dialog As New OpenFileDialog()
        dialog.Multiselect = True
        If dialog.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
            For Each path1 As String In dialog.FileNames
                PathList.Add(path1)
            Next
            UpdateTextBox()
        End If
    End Sub

    Private Sub ToolStripMenuItemCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItemCopy.Click
        Clipboard.SetDataObject(Me.TextBox1.Text, True)
        System.Media.SystemSounds.Asterisk.Play()
    End Sub

    Private Shared Sub Help()
        MsgBox("yShowVer [/rawpath] [/fullpath] [/nameonly] " & vbCr &
               "[-c] [+c] [-m] [+m] [-s] [+s] {-|FULLPATH}" & vbCr &
               "  +c/-c : Split line On/Off " & vbCr &
               "  +m/-m : Print MD5Sum On/Off" & vbCr &
               "  +s/-s : Print Filesize On/Off" & vbCr &
               "  - : Read Filenames from STDIN")
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UpdateTextBox()
        Dim v = System.Environment.Version
        Using reg As New AlfaRegistry()
            Dim FullPath = reg("FullPath")
            If String.Compare(FullPath, "1") = 0 Then
                Me.ComboBoxPath.SelectedIndex = 1
            ElseIf String.Compare(FullPath, "2") = 0 Then
                Me.ComboBoxPath.SelectedIndex = 2
            Else
                Me.ComboBoxPath.SelectedIndex = 0
            End If
            Me.CheckBoxCrLf.Checked = (String.Compare(reg("CrLf"), "1") = 0)
            Me.CheckBoxMD5.Checked = (String.Compare(reg("MD5"), "1") = 0)
            Me.CheckBoxSize.Checked = (String.Compare(reg("Size"), "1") = 0)
            Me.CheckBoxBit.Checked = (String.Compare(reg("Bit"), "1") = 0)
        End Using
        Dim args = System.Environment.GetCommandLineArgs()
        For i As Integer = args.GetLowerBound(0) + 1 To args.GetUpperBound(0)
            Dim arg1 As String = args(i)
            Select Case arg1
                Case "/rawpath"
                    Me.ComboBoxPath.SelectedIndex = 0
                Case "/fullpath"
                    Me.ComboBoxPath.SelectedIndex = 1
                Case "/nameonly"
                    Me.ComboBoxPath.SelectedIndex = 2
                Case "-c"
                    Me.CheckBoxCrLf.Checked = False
                Case "+c"
                    Me.CheckBoxCrLf.Checked = True
                Case "-m"
                    Me.CheckBoxMD5.Checked = False
                Case "+m"
                    Me.CheckBoxMD5.Checked = True
                Case "-s"
                    Me.CheckBoxSize.Checked = False
                Case "+s"
                    Me.CheckBoxSize.Checked = True
                Case "-b"
                    Me.CheckBoxBit.Checked = False
                Case "+b"
                    Me.CheckBoxBit.Checked = True
                Case "-"
                    Dim files As String = Console.In.ReadToEnd
                    For Each line As String In files.Split(vbLf)
                        If Not String.IsNullOrWhiteSpace(line) Then
                            Me.PathList.Add(line.Trim())
                        End If
                    Next
                Case Else
                    Me.PathList.Add(arg1)
            End Select
        Next
        If Me.PathList.Count <= 0 Then
            Help()
            Me.Close()
        End If
        UpdateTextBox()
        Me.Text = String.Format("{0} - {1}", Me.Text, Application.ProductVersion)
    End Sub

    Private Sub Form1_DragDrop(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragDrop, TextBox1.DragDrop
        Dim filenames As String() = CType(e.Data.GetData(DataFormats.FileDrop, False), String())
        For Each fname As String In filenames
            Me.PathList.Add(fname)
        Next
        UpdateTextBox()
    End Sub

    Private Sub Form1_DragEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles MyBase.DragEnter, TextBox1.DragEnter
        'コントロール内にドラッグされたとき実行される
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            'ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
            e.Effect = DragDropEffects.Copy
        Else
            'ファイル以外は受け付けない
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Using reg As New AlfaRegistry()
            reg("FullPath") = Me.ComboBoxPath.SelectedIndex.ToString()
            reg("CrLf") = If(Me.CheckBoxCrLf.Checked, "1", "0")
            reg("MD5") = If(Me.CheckBoxMD5.Checked, "1", "0")
            reg("Size") = If(Me.CheckBoxSize.Checked, "1", "0")
            reg("Bit") = If(Me.CheckBoxBit.Checked, "1", "0")
        End Using
    End Sub

    Private Sub CheckBoxBit_CheckedChanged(sender As Object, e As EventArgs)

    End Sub
End Class
