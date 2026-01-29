Imports Microsoft.Win32

Public Class clsMain
    Friend Shared ReadOnly RegistryPath As String = "SOFTWARE\TimeShareIT\ProStruktura"
    Friend Shared ReadOnly RegistryPathUpdater As String = "SOFTWARE\TimeShareIT\Updater"
    Friend Shared RegistryHiveValue As New RegistryHive

    Private Shared _useRegistry As Boolean = False
    Public Shared Property UseRegistry() As Boolean
        Get
            Return _useRegistry
        End Get
        Set(ByVal value As Boolean)
            _useRegistry = value
        End Set
    End Property


End Class
