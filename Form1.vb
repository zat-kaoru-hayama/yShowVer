Imports System.Security.Cryptography

Public Class Form1
    Public PathList As New List(Of String)

    Private stamp As New Dictionary(Of String, DateTime)
    Private md5cache As New Dictionary(Of String, String)

    Private Shared Function VersionFilter(ByVal src As String) As String
        Return If(src IsNot Nothing, src.Replace(",", ".").Replace(" ", ""), "(not executable)")
    End Function

    Private Shared Function Bytes2Dword(ByVal Array As Byte()) As Int64
        Return CType(Array(0) + Array(1) * 256 + Array(2) * 256 * 256 + Array(3) * 256 * 256 * 256, Int64)
    End Function

    Private Sub UpdateTextBox()
        Dim buffer As New System.Text.StringBuilder()
        For Each path1 As String In PathList
            If buffer.Length > 0 Then
                buffer.AppendLine()
            End If
            '*** パス ***
            If CheckBoxFullPath.Checked Then
                buffer.AppendFormat("{0}{1}{2}", System.IO.Path.GetFullPath(path1), vbCrLf, vbTab)
            Else
                buffer.AppendFormat("{0,-16}", System.IO.Path.GetFileName(path1))
            End If

            If System.IO.File.Exists(path1) Then
                Dim vi As System.Diagnostics.FileVersionInfo =
                    System.Diagnostics.FileVersionInfo.GetVersionInfo(path1)

                buffer.AppendFormat("{0,-17}{1,-17}",
                                    VersionFilter(vi.FileVersion),
                                    VersionFilter(vi.ProductVersion))

                Dim stamp1 As DateTime
                If Not String.IsNullOrWhiteSpace(vi.FileVersion) Then
                    If Not stamp.TryGetValue(path1, stamp1) Then
                        Using r = System.IO.File.OpenRead(path1)
                            Dim array As Byte() = New Byte(4) {}
                            r.Seek(60, IO.SeekOrigin.Begin)
                            r.Read(array, 0, 4)
                            Dim dword As Integer = Bytes2Dword(array)
                            r.Seek(dword + 8, IO.SeekOrigin.Begin)
                            r.Read(array, 0, 4)
                            stamp1 = New DateTime(1970, 1, 1, 0, 0, 0)
                            stamp1 = stamp1.AddSeconds(Bytes2Dword(array)).ToLocalTime()
                            stamp(path1) = stamp1
                        End Using
                    End If
                    buffer.AppendFormat("{0:D2}-{1:D02}-{2:D2} {3:D2}:{4:D2}:{5:D2}",
                                        stamp1.Year, stamp1.Month, stamp1.Day,
                                        stamp1.Hour, stamp1.Minute, stamp1.Second)
                End If

                Dim emptyline As Boolean = False
                Dim fi As New System.IO.FileInfo(path1)
                If CheckBoxSize.Checked Then
                    buffer.AppendFormat("{0}{1}{2} bytes ", vbCrLf, vbTab, fi.Length)
                    emptyline = True
                End If
                If CheckBoxMD5.Checked Then
                    If Not CheckBoxSize.Checked Then
                        buffer.AppendLine()
                        buffer.Append(vbTab)
                    End If
                    Dim md5sum As String = Nothing
                    If Not md5cache.TryGetValue(path1, md5sum) Then
                        Using md5_ = MD5.Create()
                            Using stream_ = System.IO.File.OpenRead(path1)
                                md5sum = BitConverter.ToString(md5_.ComputeHash(stream_)).Replace("-", "").ToLower()
                            End Using
                        End Using
                        md5cache(path1) = md5sum
                    End If
                    buffer.AppendFormat("md5sum:{1}", vbTab, md5sum)
                    emptyline = True
                End If
                If emptyline Then
                    buffer.AppendLine()
                End If
            Else
                buffer.Append("(file not found)")
            End If
        Next
        TextBox1.Text = buffer.ToString()
    End Sub

    Private Sub CheckBoxFullPath_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        CheckBoxFullPath.CheckedChanged, CheckBoxSize.CheckedChanged, CheckBoxMD5.CheckedChanged
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
End Class
