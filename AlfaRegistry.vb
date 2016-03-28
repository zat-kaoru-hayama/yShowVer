Public Class AlfaRegistry
    Implements IDisposable

    Const MakerFolder As String = "Software\Alfatech"

    Private Folder As String
    Private Reg As Microsoft.Win32.RegistryKey

    Public Sub New()
        Me.Folder = System.IO.Path.Combine(MakerFolder, My.Application.Info.AssemblyName)
    End Sub

    Default Public Property Item(ByVal key As String) As String
        Get
            If IsNothing(Reg) Then
                Me.Reg = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(Me.Folder)
            End If
            Return If(Me.Reg IsNot Nothing, Me.Reg.GetValue(key, Microsoft.Win32.RegistryValueKind.String), Nothing)
        End Get
        Set(ByVal value As String)
            If IsNothing(Reg) Then
                Me.Reg = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(Me.Folder)
            End If
            Me.Reg.SetValue(key, value, Microsoft.Win32.RegistryValueKind.String)
        End Set
    End Property

    Public Sub Close()
        If Not IsNothing(Me.Reg) Then
            Me.Reg.Close()
            Me.Reg = Nothing
        End If
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 重複する呼び出しを検出するには

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: マネージ状態を破棄します (マネージ オブジェクト)。
            End If
            ' TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下の Finalize() をオーバーライドします。
            ' TODO: 大きなフィールドを null に設定します。
            Me.Close()
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: 上の Dispose(ByVal disposing As Boolean) にアンマネージ リソースを解放するコードがある場合にのみ、Finalize() をオーバーライドします。
    Protected Overrides Sub Finalize()
        ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(ByVal disposing As Boolean) に記述します。
        Dispose(False)
        MyBase.Finalize()
    End Sub

    ' このコードは、破棄可能なパターンを正しく実装できるように Visual Basic によって追加されました。
    Public Sub Dispose() Implements IDisposable.Dispose
        ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(ByVal disposing As Boolean) に記述します。
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
