Imports System.Globalization
Imports System.IO
Imports System.Security.Principal
Imports System.Text.RegularExpressions
Imports System.Threading

Imports Microsoft.Win32

Imports MySql.Data.MySqlClient

Public Class So_Helper
#Region "Enum"
    Public Enum VarTypeEnu
        Type_Boolean
        Type_String
        Type_Integer
        Type_Long
        Type_Double
        Type_Single
        Type_Date
        Type_DateAsString
        Type_Object
    End Enum
    Public Enum ToDates
        DateOnly
        TimeOnly
        DateTime
    End Enum
    Public Enum Platform
        Afterbuy = 1
        Plenty = 2
        CSV = 3
        eBay = 4
        Amazon = 5
        Manuell = 6
        Shopware = 7
        Otto = 8
    End Enum

#End Region
    Public Structure RegFindResult
        Public Hive As RegistryHive
        Public RegPath As String
        Public Key As String
        Public Name As String
        Public VarType As VarTypeEnu
        Public Value As Object
    End Structure

#Region "Registry"
    Public Shared Function KeyExist(Hive As RegistryHive, RegPathLocal As String) As Boolean
        Dim regKey As RegistryKey = Nothing
        Try
            Select Case Hive
                Case RegistryHive.ClassesRoot
                    regKey = Registry.ClassesRoot.OpenSubKey(RegPathLocal)
                Case RegistryHive.CurrentConfig
                    regKey = Registry.CurrentConfig.OpenSubKey(RegPathLocal)
                Case RegistryHive.CurrentUser
                    regKey = Registry.CurrentUser.OpenSubKey(RegPathLocal)
                Case RegistryHive.LocalMachine
                    regKey = Registry.LocalMachine.OpenSubKey(RegPathLocal)
                Case RegistryHive.PerformanceData
                    regKey = Registry.PerformanceData.OpenSubKey(RegPathLocal)
                Case RegistryHive.Users
                    regKey = Registry.Users.OpenSubKey(RegPathLocal)
            End Select
            If regKey Is Nothing Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
        End Try
        Return False
    End Function
    Public Shared Function CopyRecursive(ByVal HiveOld As RegistryHive,
                                     ByVal RegPathOld As String,
                                     ByVal KeyNameOld As String,
                                     ByVal HiveNew As RegistryHive,
                                     ByVal RegPathNew As String,
                                     ByVal KeyNameNew As String) As Boolean
        Dim parentKeyOld As RegistryKey = Nothing
        Dim parentKeyNew As RegistryKey = Nothing
        Select Case HiveOld
            Case RegistryHive.ClassesRoot
                parentKeyOld = Registry.ClassesRoot.OpenSubKey(RegPathOld, True)
            Case RegistryHive.CurrentConfig
                parentKeyOld = Registry.CurrentConfig.OpenSubKey(RegPathOld, True)
            Case RegistryHive.CurrentUser
                parentKeyOld = Registry.CurrentUser.OpenSubKey(RegPathOld, True)
            Case RegistryHive.LocalMachine
                parentKeyOld = Registry.LocalMachine.OpenSubKey(RegPathOld, True)
            Case RegistryHive.PerformanceData
                parentKeyOld = Registry.PerformanceData.OpenSubKey(RegPathOld, True)
            Case RegistryHive.Users
                parentKeyOld = Registry.Users.OpenSubKey(RegPathOld, True)
        End Select
        Select Case HiveNew
            Case RegistryHive.ClassesRoot
                parentKeyNew = Registry.ClassesRoot.OpenSubKey(RegPathNew, True)
            Case RegistryHive.CurrentConfig
                parentKeyNew = Registry.CurrentConfig.OpenSubKey(RegPathNew, True)
            Case RegistryHive.CurrentUser
                parentKeyNew = Registry.CurrentUser.OpenSubKey(RegPathNew, True)
            Case RegistryHive.LocalMachine
                parentKeyNew = Registry.LocalMachine.OpenSubKey(RegPathNew, True)
            Case RegistryHive.PerformanceData
                parentKeyNew = Registry.PerformanceData.OpenSubKey(RegPathNew, True)
            Case RegistryHive.Users
                parentKeyNew = Registry.Users.OpenSubKey(RegPathNew, True)
        End Select
        Return CopyKey(parentKeyOld, KeyNameOld, parentKeyNew, KeyNameNew)

    End Function
    Public Shared Function CopyKey(ByVal parentKeyOld As RegistryKey, SubKeyNameOld As String, ByVal parentKeyNew As RegistryKey, SubKeyNameNew As String) As Boolean
        Dim destinationKey As RegistryKey = parentKeyNew.CreateSubKey(SubKeyNameNew)
        Dim sourceKey As RegistryKey = parentKeyOld.OpenSubKey(SubKeyNameOld, True)
        RecurseCopyKey(sourceKey, destinationKey)
        Return True
    End Function
    Public Shared Function CopyKey(ByVal parentKey As RegistryKey, SubKeyNameOld As String, SubKeyNameNew As String) As Boolean
        Dim destinationKey As RegistryKey = parentKey.CreateSubKey(SubKeyNameNew)
        Dim sourceKey As RegistryKey = parentKey.OpenSubKey(SubKeyNameOld, True)
        RecurseCopyKey(sourceKey, destinationKey)
        Return True
    End Function
    Public Shared Sub RecurseCopyKey(sourceKey As RegistryKey, destinationKey As RegistryKey)
        For Each valueName As String In sourceKey.GetValueNames()
            Dim objValue As Object = sourceKey.GetValue(valueName)
            Dim valKind As RegistryValueKind = sourceKey.GetValueKind(valueName)
            destinationKey.SetValue(valueName, objValue, valKind)
        Next
        For Each sourceSubKeyName As String In sourceKey.GetSubKeyNames()
            Dim sourceSubKey As RegistryKey = sourceKey.OpenSubKey(sourceSubKeyName)
            Dim destSubKey As RegistryKey = destinationKey.CreateSubKey(sourceSubKeyName)
            RecurseCopyKey(sourceSubKey, destSubKey)
        Next
    End Sub
    Public Shared Function RegistryCreateKey(Hive As RegistryHive, RegPathLocal As String) As RegistryKey
        Dim regKey As RegistryKey = Nothing
        Try
            Select Case Hive
                Case RegistryHive.ClassesRoot
                    If Not KeyExist(Hive, RegPathLocal) Then
                        regKey = Registry.ClassesRoot.CreateSubKey(RegPathLocal)
                    Else
                        regKey = Registry.ClassesRoot.OpenSubKey(RegPathLocal)
                    End If
                Case RegistryHive.CurrentConfig
                    If Not KeyExist(Hive, RegPathLocal) Then
                        regKey = Registry.CurrentConfig.CreateSubKey(RegPathLocal)
                    Else
                        regKey = Registry.CurrentConfig.OpenSubKey(RegPathLocal)
                    End If
                Case RegistryHive.CurrentUser
                    If Not KeyExist(Hive, RegPathLocal) Then
                        regKey = Registry.CurrentUser.CreateSubKey(RegPathLocal)
                    Else
                        regKey = Registry.CurrentUser.OpenSubKey(RegPathLocal)
                    End If
                Case RegistryHive.LocalMachine
                    If Not KeyExist(Hive, RegPathLocal) Then
                        regKey = Registry.LocalMachine.CreateSubKey(RegPathLocal)
                    Else
                        regKey = Registry.LocalMachine.OpenSubKey(RegPathLocal)
                    End If
                Case RegistryHive.PerformanceData
                    If Not KeyExist(Hive, RegPathLocal) Then
                        regKey = Registry.PerformanceData.CreateSubKey(RegPathLocal)
                    Else
                        regKey = Registry.PerformanceData.OpenSubKey(RegPathLocal)
                    End If
                Case RegistryHive.Users
                    If Not KeyExist(Hive, RegPathLocal) Then
                        regKey = Registry.Users.CreateSubKey(RegPathLocal)
                    Else
                        regKey = Registry.Users.OpenSubKey(RegPathLocal)
                    End If
            End Select

        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
        End Try
        Return regKey
    End Function
    Public Shared Function RegistryCreateSubKey(Hive As RegistryHive, RegPathLocal As String) As RegistryKey
        Dim regKey As RegistryKey = Nothing
        Select Case Hive
            Case RegistryHive.ClassesRoot
                If Not KeyExist(Hive, RegPathLocal) Then
                    regKey = Registry.ClassesRoot.CreateSubKey(RegPathLocal)
                End If
            Case RegistryHive.CurrentConfig
                If Not KeyExist(Hive, RegPathLocal) Then
                    regKey = Registry.CurrentConfig.CreateSubKey(RegPathLocal)
                End If
            Case RegistryHive.CurrentUser
                If Not KeyExist(Hive, RegPathLocal) Then
                    regKey = Registry.CurrentUser.CreateSubKey(RegPathLocal)
                End If
            Case RegistryHive.LocalMachine
                If Not KeyExist(Hive, RegPathLocal) Then
                    regKey = Registry.LocalMachine.CreateSubKey(RegPathLocal)
                End If
            Case RegistryHive.PerformanceData
                If Not KeyExist(Hive, RegPathLocal) Then
                    regKey = Registry.PerformanceData.CreateSubKey(RegPathLocal)
                End If
            Case RegistryHive.Users
                If Not KeyExist(Hive, RegPathLocal) Then
                    regKey = Registry.Users.CreateSubKey(RegPathLocal)
                End If
        End Select
        Return regKey
    End Function
    Public Shared Sub RegistryDeleteKey(ByVal Hive As RegistryHive, ByVal RegPathLocal As String, ByVal SubKey2Delete As String)
        RegPathLocal = RegPathLocal.Trim & "\" & SubKey2Delete.Trim
        Select Case Hive
            Case RegistryHive.ClassesRoot
                If KeyExist(Hive, RegPathLocal) Then
                    Registry.ClassesRoot.DeleteSubKey(RegPathLocal, False)
                End If
            Case RegistryHive.CurrentConfig
                If KeyExist(Hive, RegPathLocal) Then
                    Registry.CurrentConfig.DeleteSubKey(RegPathLocal, False)
                End If
            Case RegistryHive.CurrentUser
                If KeyExist(Hive, RegPathLocal) Then
                    Registry.CurrentUser.DeleteSubKey(RegPathLocal, False)
                End If
            Case RegistryHive.LocalMachine
                If KeyExist(Hive, RegPathLocal) Then
                    Registry.LocalMachine.DeleteSubKey(RegPathLocal, False)
                End If
            Case RegistryHive.PerformanceData
                If KeyExist(Hive, RegPathLocal) Then
                    Registry.PerformanceData.DeleteSubKey(RegPathLocal, False)
                End If
            Case RegistryHive.Users
                If KeyExist(Hive, RegPathLocal) Then
                    Registry.Users.DeleteSubKey(RegPathLocal, False)
                End If
        End Select
    End Sub
    Public Shared Function RegistryDeleteValue(Hive As RegistryHive, RegPathLocal As String, Key As String) As Boolean
        Dim regKey As RegistryKey = Nothing
        Select Case Hive
            Case RegistryHive.ClassesRoot
                regKey = Registry.ClassesRoot.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.CurrentConfig
                regKey = Registry.CurrentConfig.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.CurrentUser
                regKey = Registry.CurrentUser.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.LocalMachine
                regKey = Registry.LocalMachine.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.PerformanceData
                regKey = Registry.PerformanceData.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.Users
                regKey = Registry.Users.OpenSubKey(RegPathLocal, True)
        End Select
        If regKey IsNot Nothing Then
            regKey.DeleteValue(Key)
        End If
        Return CType(regKey.GetValue(Key, True), Boolean)
    End Function
    Public Shared Function RegistryReadSubKeys(ByVal Hive As RegistryHive, ByVal RegPathLocal As String, ByVal StartsWithString As String) As List(Of String)
        Dim regKey As RegistryKey = Nothing
        Dim SubKeyList As New List(Of String)
        Select Case Hive
            Case RegistryHive.ClassesRoot
                regKey = Registry.ClassesRoot.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.CurrentConfig
                regKey = Registry.CurrentConfig.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.CurrentUser
                regKey = Registry.CurrentUser.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.LocalMachine
                regKey = Registry.LocalMachine.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.PerformanceData
                regKey = Registry.PerformanceData.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.Users
                regKey = Registry.Users.OpenSubKey(RegPathLocal, True)
        End Select
        Try
            For Each Value As String In regKey.GetSubKeyNames().ToList
                If Value.ToUpper.StartsWith(StartsWithString.ToUpper) Then
                    SubKeyList.Add(Value)
                End If
            Next
        Catch ex As Exception
            SubKeyList = Nothing
        End Try
        Return SubKeyList
    End Function
    Public Shared Function RegistryReadValue(ByVal Hive As RegistryHive,
                                         ByVal RegPathLocal As String,
                                         Key As String,
                                         ByVal Optional VarType As So_Helper.VarTypeEnu = So_Helper.VarTypeEnu.Type_String,
                                         ByVal Optional AllowNothing As Boolean = False,
                                         ByVal Optional DefaultValue As Object = Nothing) As Object
        Dim regKey As RegistryKey = Nothing
        Dim rd As Double = 0
        Dim rs As String = ""
        Dim value As New Object
        Dim RetVal As New Object
        Try
            Select Case Hive
                Case RegistryHive.ClassesRoot
                    regKey = Registry.ClassesRoot.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.CurrentConfig
                    regKey = Registry.CurrentConfig.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.CurrentUser
                    regKey = Registry.CurrentUser.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.LocalMachine
                    regKey = Registry.LocalMachine.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.PerformanceData
                    regKey = Registry.PerformanceData.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.Users
                    regKey = Registry.Users.OpenSubKey(RegPathLocal, True)
            End Select
            If regKey Is Nothing Then
                value = Nothing
            Else
                value = regKey.GetValue(Key)
            End If
            If value Is Nothing AndAlso AllowNothing Then
                RetVal = Nothing
            ElseIf value Is Nothing AndAlso Not AllowNothing Then
                Select Case VarType
                    Case So_Helper.VarTypeEnu.Type_Boolean
                        If DefaultValue Is Nothing Then
                            RetVal = False
                        Else
                            RetVal = CBool(DefaultValue)
                        End If
                    Case So_Helper.VarTypeEnu.Type_Double
                        If DefaultValue Is Nothing Then
                            RetVal = 0
                        Else
                            RetVal = So_Helper_VarConvert.ConvertToDouble(DefaultValue)
                        End If
                    Case So_Helper.VarTypeEnu.Type_Integer
                        If DefaultValue Is Nothing Then
                            RetVal = 0
                        Else
                            RetVal = So_Helper_VarConvert.ConvertToInteger(DefaultValue)
                        End If
                    Case So_Helper.VarTypeEnu.Type_String
                        If DefaultValue Is Nothing Then
                            RetVal = ""
                        Else
                            RetVal = CStr(DefaultValue)
                        End If
                    Case So_Helper.VarTypeEnu.Type_DateAsString
                        If DefaultValue Is Nothing Then
                            RetVal = ""
                        Else
                            RetVal = CStr(DefaultValue)
                        End If
                    Case So_Helper.VarTypeEnu.Type_Object
                        RetVal = Nothing
                    Case So_Helper.VarTypeEnu.Type_Date
                        If DefaultValue Is Nothing Then
                            Dim DOB As Nullable(Of Date) = CDate(Now)
                            Dim datestring As String = DOB.Value.ToString("d")
                            DOB = Nothing
                            RetVal = datestring
                        Else
                            RetVal = CDate(DefaultValue)
                        End If
                    Case Else
                        If DefaultValue Is Nothing Then
                            RetVal = ""
                        Else
                            RetVal = CStr(DefaultValue)
                        End If
                End Select
            Else
                Select Case VarType
                    Case So_Helper.VarTypeEnu.Type_Boolean
                        RetVal = CBool(value)
                    Case So_Helper.VarTypeEnu.Type_Double
                        RetVal = So_Helper_VarConvert.ConvertToDouble(Val(value))
                    Case So_Helper.VarTypeEnu.Type_Integer
                        RetVal = So_Helper_VarConvert.ConvertToInteger(Val(value))
                    Case So_Helper.VarTypeEnu.Type_String
                        RetVal = value.ToString
                    Case So_Helper.VarTypeEnu.Type_DateAsString
                        RetVal = value.ToString
                    Case So_Helper.VarTypeEnu.Type_Date
                        RetVal = CDate(value)
                    Case So_Helper.VarTypeEnu.Type_Object
                        RetVal = Nothing
                    Case Else
                        RetVal = value.ToString
                End Select
            End If

        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
            Return ""
        End Try
        Return RetVal
    End Function
    Public Shared Function RegistryReadValuePattern(Hive As RegistryHive, RegPathLocal As String, KeyPattern As String, Optional VariType As So_Helper.VarTypeEnu = So_Helper.VarTypeEnu.Type_String) As List(Of RegFindResult)
        Dim regKey As RegistryKey = Nothing
        Dim rd As Double = 0
        Dim rs As String = ""
        'Dim value As New Object
        Dim RetVal As New List(Of RegFindResult)
        Try
            Select Case Hive
                Case RegistryHive.ClassesRoot
                    regKey = Registry.ClassesRoot.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.CurrentConfig
                    regKey = Registry.CurrentConfig.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.CurrentUser
                    regKey = Registry.CurrentUser.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.LocalMachine
                    regKey = Registry.LocalMachine.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.PerformanceData
                    regKey = Registry.PerformanceData.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.Users
                    regKey = Registry.Users.OpenSubKey(RegPathLocal, True)
            End Select
            If regKey IsNot Nothing Then
                If regKey.ValueCount > 0 Then
                    For Each key As String In regKey.GetValueNames
                        If key Like KeyPattern Then
                            Dim RetValTmp As New RegFindResult
                            RetValTmp.Hive = Hive
                            RetValTmp.RegPath = RegPathLocal
                            RetValTmp.Key = key
                            RetValTmp.VarType = VariType
                            RetValTmp.Value = RegistryReadValue(Hive, RegPathLocal, key, VariType)
                            RetVal.Add(RetValTmp)
                        End If
                    Next
                End If
            End If
            'regKey.Close()
            'regKey.Dispose()
        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
        End Try
        Return RetVal
    End Function
    Public Shared Function RegistryWriteValue(ByVal Hive As RegistryHive,
                                              ByVal RegPathLocal As String,
                                              ByVal Key As String,
                                              ByVal Value As Object) As Boolean
        Dim regKey As RegistryKey = Nothing
        Try
            '\SOFTWARE\TimeShareIT\ConDrop\TestApplication\OttoDll\Parameter
            '\SOFTWARE\TimeShareIT\ConDrop\TestApplication\OttoDll\Parameter
            If Not IsNothing(Value) Then
                If IsNothing(Hive) OrElse Hive = 0 Then
                    'Stop
                Else
                    regKey = RegistryCreateKey(Hive, RegPathLocal)
                    Select Case Hive
                        Case RegistryHive.ClassesRoot
                            regKey = Registry.ClassesRoot.OpenSubKey(RegPathLocal, True)
                        Case RegistryHive.CurrentConfig
                            regKey = Registry.CurrentConfig.OpenSubKey(RegPathLocal, True)
                        Case RegistryHive.CurrentUser
                            regKey = Registry.CurrentUser.OpenSubKey(RegPathLocal, True)
                        Case RegistryHive.LocalMachine
                            regKey = Registry.LocalMachine.OpenSubKey(RegPathLocal, True)
                        Case RegistryHive.PerformanceData
                            regKey = Registry.PerformanceData.OpenSubKey(RegPathLocal, True)
                        Case RegistryHive.Users
                            regKey = Registry.Users.OpenSubKey(RegPathLocal, True)
                    End Select
                    Registry.SetValue(regKey.Name, Key, Value)
                    'regKey.Close()
                    'regKey.Dispose()
                End If
            End If

        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
            Return False
        End Try
        Return True
    End Function
    Public Shared Function RenameSubKey(ByVal Hive As RegistryHive, ByVal RegPathLocal As String, ByVal SubKeyNameOld As String, ByVal SubKeyNameNew As String) As Boolean
        Try
            Dim parentKey As RegistryKey = Nothing
            Select Case Hive
                Case RegistryHive.ClassesRoot
                    parentKey = Registry.ClassesRoot.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.CurrentConfig
                    parentKey = Registry.CurrentConfig.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.CurrentUser
                    parentKey = Registry.CurrentUser.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.LocalMachine
                    parentKey = Registry.LocalMachine.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.PerformanceData
                    parentKey = Registry.PerformanceData.OpenSubKey(RegPathLocal, True)
                Case RegistryHive.Users
                    parentKey = Registry.Users.OpenSubKey(RegPathLocal, True)
            End Select
            Dim rb As Boolean = CopyKey(parentKey, SubKeyNameOld, SubKeyNameNew)
            If rb Then
                parentKey.DeleteSubKeyTree(SubKeyNameOld)
            Else
                rb = False
            End If
            Return rb

        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
            Return False
        End Try
    End Function
    Public Shared Function RegistryKeyExist(ByVal Hive As RegistryHive, ByVal RegPathLocal As String, ByVal RegKey As String) As Boolean
        Dim CheckRegKey As RegistryKey = Nothing
        'RegPath = RegPath & RegKey
        Select Case Hive
            Case RegistryHive.ClassesRoot
                CheckRegKey = Registry.ClassesRoot.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.CurrentConfig
                CheckRegKey = Registry.CurrentConfig.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.CurrentUser
                CheckRegKey = Registry.CurrentUser.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.LocalMachine
                CheckRegKey = Registry.LocalMachine.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.PerformanceData
                CheckRegKey = Registry.PerformanceData.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.Users
                CheckRegKey = Registry.Users.OpenSubKey(RegPathLocal, True)
        End Select

        If CheckRegKey Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Shared Function RegistryKeyExist(Hive As RegistryHive, RegPathLocal As String) As Boolean
        Dim regKey As RegistryKey = Nothing
        Select Case Hive
            Case RegistryHive.ClassesRoot
                regKey = Registry.ClassesRoot.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.CurrentConfig
                regKey = Registry.CurrentConfig.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.CurrentUser
                regKey = Registry.CurrentUser.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.LocalMachine
                regKey = Registry.LocalMachine.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.PerformanceData
                regKey = Registry.PerformanceData.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.Users
                regKey = Registry.Users.OpenSubKey(RegPathLocal, True)
        End Select

        If regKey Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Shared Function RegistrySearchSubKeys(ByVal Path As String, ByVal SearchStr As String) As List(Of String)
        Dim Result As New List(Of String)
        Dim ParentKey As RegistryKey = Registry.LocalMachine.OpenSubKey(Path, True)
        For Each valueName As String In ParentKey.GetValueNames()
            'Dim CurStr As String = ParentKey.GetValue(valueName).ToString
            '? umbauen auf Regex:
            Dim CurStr As String = valueName & " = " & ParentKey.GetValue(valueName).ToString

            If valueName Like SearchStr Then
                'MsgBox(CurStr)
                Result.Add(valueName)
                valueName = ""
            End If
        Next
        If ParentKey.SubKeyCount > 0 Then
            For Each subKeyName As String In ParentKey.GetSubKeyNames()
                Dim Thispath As String = Path & subKeyName & "\\"
                RegistrySearchSubKeys(Thispath, SearchStr)
            Next
        End If
        Return Result
    End Function
    Public Shared Function RegistryListValues(ByVal Hive As RegistryHive, ByVal RegPathLocal As String, ByVal Optional Pattern As String = "") As List(Of String)
        Dim regKey As RegistryKey = Nothing
        Dim rd As Double = 0
        Dim rs As String = ""
        Dim value As New List(Of String)
        Dim count As Integer = 0

        Select Case Hive
            Case RegistryHive.ClassesRoot
                regKey = Registry.ClassesRoot.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.CurrentConfig
                regKey = Registry.CurrentConfig.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.CurrentUser
                regKey = Registry.CurrentUser.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.LocalMachine
                regKey = Registry.LocalMachine.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.PerformanceData
                regKey = Registry.PerformanceData.OpenSubKey(RegPathLocal, True)
            Case RegistryHive.Users
                regKey = Registry.Users.OpenSubKey(RegPathLocal, True)
        End Select
        Try
            count = regKey.ValueCount
            For Each valueName As String In regKey.GetValueNames()
                value.Add(valueName)
            Next
        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
        End Try
        Return value
    End Function
    Public Shared Sub CopyRegistryFrom32BitTo64Bit(Hive As RegistryHive, RegPathLocal As String)
        Try
            ''Call'Helper_Logger.writelog(Level.Info, "Die 32-Bit Registry-Einträge werden nach 64-Bit kopiert.")
            Dim rb As Boolean = So_Helper.RegistryWriteValue(Hive, RegPathLocal, "Test", "Test")
            Dim SourceView32 As RegistryKey = RegistryKey.OpenBaseKey(Hive, RegistryView.Registry32)
            Dim SourceStartKey As RegistryKey = SourceView32.OpenSubKey(RegPathLocal, False)

            Dim DestinationView64 As RegistryKey = RegistryKey.OpenBaseKey(Hive, RegistryView.Registry64)
            Dim DestinationStartKey As RegistryKey = DestinationView64.OpenSubKey(RegPathLocal, True)

            rb = So_Helper.RegistryWriteValue(Hive, RegPathLocal, "Test", "Test")
            Call RecurseCopyKey(SourceStartKey, DestinationStartKey)

            ''Call'Helper_Logger.writelog(Level.Info, "Die 32-Bit Registry-Einträge wurden erfolgreich nach 64-Bit kopiert.")
        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
        End Try
    End Sub
    Public Shared Sub CheckRegistryKey(ByVal RegHive As RegistryHive,
                                   ByVal RegPathLocal As String,
                                   Key As String,
                                   ByVal VarType As VarTypeEnu,
                                   ByVal DefaultValue As Object)

        Select Case VarType
            Case So_Helper.VarTypeEnu.Type_Boolean
                If So_Helper.RegistryReadValue(RegHive, RegPathLocal, Key, VarType, True) Is Nothing Then
                    Call So_Helper.RegistryWriteValue(RegHive, RegPathLocal, Key, DefaultValue)
                End If
            Case So_Helper.VarTypeEnu.Type_Double
                If So_Helper.RegistryReadValue(RegHive, RegPathLocal, Key, VarType, True) Is Nothing Then
                    Call So_Helper.RegistryWriteValue(RegHive, RegPathLocal, Key, DefaultValue)
                End If
            Case So_Helper.VarTypeEnu.Type_Integer
                If So_Helper.RegistryReadValue(RegHive, RegPathLocal, Key, VarType, True) Is Nothing Then
                    Call So_Helper.RegistryWriteValue(RegHive, RegPathLocal, Key, DefaultValue)
                End If
            Case So_Helper.VarTypeEnu.Type_String
                If So_Helper.RegistryReadValue(RegHive, RegPathLocal, Key, VarType, True) Is Nothing Then
                    Call So_Helper.RegistryWriteValue(RegHive, RegPathLocal, Key, DefaultValue)
                End If
            Case So_Helper.VarTypeEnu.Type_DateAsString
                If So_Helper.RegistryReadValue(RegHive, RegPathLocal, Key, VarType, True) Is Nothing Then
                    Call So_Helper.RegistryWriteValue(RegHive, RegPathLocal, Key, DefaultValue)
                End If
            Case So_Helper.VarTypeEnu.Type_Date
                Dim RetVal As Date = So_Helper_VarConvert.ConvertToDate(So_Helper.RegistryReadValue(RegHive, RegPathLocal, Key, VarType, True))
                'If RetVal Is Nothing Then
                '    Call So_Helper.RegistryWriteValue(RegHive, RegPathLocal, Key, DefaultValue)
                'End If
            Case Else
                Dim RetVal As String = So_Helper_VarConvert.ConvertToString(So_Helper.RegistryReadValue(RegHive, RegPathLocal, Key, VarType, True), False, "")
                If String.IsNullOrWhiteSpace(RetVal) Then
                    Call So_Helper.RegistryWriteValue(RegHive, RegPathLocal, Key, DefaultValue)
                End If
        End Select



    End Sub

#End Region
#Region "Variablen"
    Public Shared Function Checkvar(ByVal var As Object,
                                ByVal VariType As VarTypeEnu,
                                Optional ByVal trimed As Boolean = False,
                                Optional ByVal DefaultValue As Object = Nothing) As Object
        Dim Var_Type_String As String = ""
        Dim Var_Type_Date As New Date
        'Dim Var_Type_Boolean As Boolean = False
        'Dim Var_Type_Integer As Integer = 0
        'Dim Var_Type_Double As Double = 0
        'Dim Var_Type_Single As Single = 0
        'Dim Var_Type_Long As Single = 0
        'Dim var_bool As Boolean = False
        'Dim var_dbl As Double = 0
        'Dim var_int As Integer = 0
        'Dim var_sng As Single = 0

        Try
            If VariType = So_Helper.VarTypeEnu.Type_Boolean Then
                Return So_Helper_VarConvert.ConvertToBoolean(var)
            ElseIf VariType = So_Helper.VarTypeEnu.Type_Double Then

                Return So_Helper_VarConvert.ConvertToDouble(var)
            ElseIf VariType = So_Helper.VarTypeEnu.Type_Integer Then
                Return So_Helper_VarConvert.ConvertToInteger(var)
            ElseIf VariType = So_Helper.VarTypeEnu.Type_Long Then
                Return So_Helper_VarConvert.ConvertToLong(var)
            ElseIf VariType = So_Helper.VarTypeEnu.Type_Single Then
                Return So_Helper_VarConvert.ConvertToSingle(var)
            ElseIf VariType = So_Helper.VarTypeEnu.Type_String Then
                If var Is Nothing OrElse System.Convert.IsDBNull(var) OrElse String.IsNullOrWhiteSpace(var.ToString) Then
                    var = ""
                End If
                If trimed Then
                    Var_Type_String = var.ToString.Trim
                Else
                    Var_Type_String = CStr(var)
                End If
                Return Var_Type_String
            ElseIf VariType = So_Helper.VarTypeEnu.Type_Date Then
                If var Is Nothing OrElse System.Convert.IsDBNull(var) OrElse String.IsNullOrWhiteSpace(var.ToString) OrElse Not IsDate(var) Then
                    'Var_Type_Date = Nothing
                    Return Nothing
                ElseIf CStr(var) = "1700-01-01 00:00:00" Then
                    'Var_Type_Date = Nothing
                    Return Nothing
                Else
                    Var_Type_Date = CDate(var)
                    Return CDate(var)
                End If
                'Return Var_Type_Date
            ElseIf VariType = So_Helper.VarTypeEnu.Type_DateAsString Then
                If var Is Nothing OrElse System.Convert.IsDBNull(var) OrElse String.IsNullOrWhiteSpace(var.ToString) Then
                    Var_Type_String = "1700-01-01 00:00:00"
                ElseIf CStr(var) = "1700-01-01 00:00:00" Then
                    Var_Type_String = "1700-01-01 00:00:00"
                Else
                    Var_Type_String = Format(CDate(var), "yyyy-MM-dd HH:mm:ss")
                End If
                Return Var_Type_String
            Else
                If var Is Nothing OrElse System.Convert.IsDBNull(var) OrElse String.IsNullOrWhiteSpace(var.ToString) Then
                    var = ""
                End If
                If trimed Then
                    Var_Type_String = var.ToString.Trim
                Else
                    Var_Type_String = var.ToString
                End If
                Return Var_Type_String
            End If
        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False)
            If So_Helper.IsIDE() Then Stop
            Return ""
        End Try
    End Function
    Public Shared Function Convert2Double(wert As String) As Double
        wert = wert.Trim
        wert = wert.Replace(",", ".")
        If IsNumeric(Val(wert)) Then
            Return So_Helper_VarConvert.ConvertToDouble(Val(wert))
        Else
            Return 0
        End If
    End Function
    Public Shared Function ConvertHtmlToText(source As String) As String
        Dim result As String
        result = source.Replace(vbCr, " ")
        result = result.Replace(vbCrLf, " ")
        result = result.Replace(vbTab, String.Empty)
        result = Regex.Replace(result, "( )+", " ")
        result = Regex.Replace(result, "<( )*head([^>])*>", "<head>", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "(<( )*(/)( )*head( )*>)", "</head>", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "(<head>).*(</head>)", String.Empty, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "<( )*script([^>])*>", "<script>", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "(<( )*(/)( )*script( )*>)", "</script>", RegexOptions.IgnoreCase)
        'result = System.Text.RegularExpressions.Regex.Replace(result, 
        '         @"(<script>)([^(<script>\.</script>)])*(</script>)",
        '         string.Empty, 
        '         System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        result = Regex.Replace(result, "(<script>).*(</script>)", String.Empty, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "<( )*style([^>])*>", "<style>", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "(<( )*(/)( )*style( )*>)", "</style>", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "(<style>).*(</style>)", String.Empty, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "<( )*td([^>])*>", vbTab, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "<( )*br( )*>", vbCr, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "<( )*li( )*>", vbCr, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "<( )*div([^>])*>", vbCr & vbCr, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "<( )*tr([^>])*>", vbCr & vbCr, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "<( )*p([^>])*>", vbCr & vbCr, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "<[^>]*>", String.Empty, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "&nbsp;", " ", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "&bull;", " * ", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "&lsaquo;", "<", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "&rsaquo;", ">", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "&trade;", "(tm)", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "&frasl;", "/", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "<", "<", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, ">", ">", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "&copy;", "(c)", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "&reg;", "(r)", RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "&(.{2,6});", String.Empty, RegexOptions.IgnoreCase)
        result = result.Replace(vbCrLf, vbCr)
        result = Regex.Replace(result, "(" & vbCr & ")( )+(" & vbCr & ")", vbCr & vbCr, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "(" & vbTab & ")( )+(" & vbTab & ")", vbTab & vbTab, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "(" & vbTab & ")( )+(" & vbCr & ")", vbTab & vbCr, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "(" & vbCr & ")( )+(" & vbTab & ")", vbCr & vbTab, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "(" & vbCr & ")(" & vbTab & ")+(" & vbCr & ")", vbCr & vbCr, RegexOptions.IgnoreCase)
        result = Regex.Replace(result, "(" & vbCr & ")(" & vbTab & ")+", vbCr & vbTab, RegexOptions.IgnoreCase)
        Dim breaks As String = vbCr & vbCr & vbCr
        Dim tabs As String = vbTab & vbTab & vbTab & vbTab & vbTab

        For index As Integer = 0 To result.Length - 1
            result = result.Replace(breaks, vbCr & vbCr)
            result = result.Replace(tabs, vbTab & vbTab & vbTab & vbTab)
            breaks = breaks & System.Convert.ToString(vbCr)
            tabs = tabs & System.Convert.ToString(TAB)
        Next
        Return result
    End Function
    Public Shared Function ExtractNummericFromString(ByVal Text As String) As String
        For i As Integer = 1 To Text.Length
            If Asc(Mid(Text, i, 1)) < 48 OrElse Asc(Mid(Text, i, 1)) > 57 Then
                Mid(Text, i, 1) = " "
            End If
        Next
        Text = Text.Trim.Replace(" ", "")
        Return Text
    End Function
    Public Shared Function IsDateWithinDays(ByVal CheckDate As String, Optional ByVal MaxDays As Integer = 30) As Boolean
        'Umbenennung von IsDateEx ==> IsDateWithinDays
        Dim ResultDate As Date
        Dim ddiff As Integer = So_Helper_VarConvert.ConvertToInteger(Math.Abs(DateDiff("d", CheckDate, Now)))
        If DateTime.TryParse(CheckDate, ResultDate) AndAlso ddiff <= MaxDays Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function IsDateWithinDays(ByVal CheckDate As Date, Optional ByVal MaxDays As Integer = 30) As Boolean
        'Umbenennung von IsDateEx ==> IsDateWithinDays
        Dim ResultDate As Date
        Dim ddiff As Integer = So_Helper_VarConvert.ConvertToInteger(Math.Abs(DateDiff("d", CheckDate, Now)))
        If DateTime.TryParse(CheckDate.ToString, ResultDate) AndAlso ddiff <= MaxDays Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function isEven(numToCheck As Integer) As Boolean
        Return (numToCheck And 1) = 0
    End Function
    Public Shared Function IncrementString(ByVal strString As String) As String
        Dim lngLenString As Long
        Dim strChar As String
        Dim lngI As Long

        lngLenString = strString.Length
        ' Start at far right
        For lngI = lngLenString To 0 Step -1
            ' If we reach the far left then add an A and exit
            If lngI = 0 Then
                strString = "a" & strString
                Exit For
            End If
            ' Consider next character
            strChar = Mid(strString, So_Helper_VarConvert.ConvertToInteger(lngI), 1)
            If strChar = "z" Then
                ' If we find Z then increment this to A
                ' and increment the character after this (in next loop iteration)
                strString = Left$(strString, So_Helper_VarConvert.ConvertToInteger(lngI - 1)) & "a" & Mid(strString, So_Helper_VarConvert.ConvertToInteger(lngI + 1), So_Helper_VarConvert.ConvertToInteger(lngLenString))
            Else
                ' Increment this non-Z and exit
                strString = Left$(strString, So_Helper_VarConvert.ConvertToInteger(lngI - 1)) & Chr(Asc(strChar) + 1) & Mid(strString, So_Helper_VarConvert.ConvertToInteger(lngI + 1), So_Helper_VarConvert.ConvertToInteger(lngLenString))
                Exit For
            End If
        Next lngI

        Return strString

    End Function
    Public Shared Function stripString(ByVal StringValue As String, ByVal Delimiter As String) As String
        Dim retval As String = StringValue
        Try
            If Not StringValue IsNot Nothing AndAlso Delimiter = Nothing AndAlso Not String.IsNullOrWhiteSpace(StringValue) AndAlso Not String.IsNullOrWhiteSpace(Delimiter) Then
                Dim strparts() As String = StringValue.Split(CType(Delimiter, Char))
                retval = strparts(0)
                'Dim po As Integer = StringValue.contains( Delimiter)
                'If po > 0 Then
                '    retval = Mid(StringValue, 1, po - 1)
                'End If
            End If

        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
        End Try

        Return retval
    End Function
    Public Shared Function EncryptString(ByVal Decrypted As String, Optional ByVal KeyLength As Integer = 256) As String
        'Dim cCrypt As cCrypt = New cCrypt
        If Not String.IsNullOrWhiteSpace(Decrypted) Then
            So_Helper_cCrypt.Encrypt(KeyLength, Decrypted)
            Return So_Helper_cCrypt.EncryptedString
        Else
            Return ""
        End If
    End Function
    Public Shared Function DecryptString(ByVal Encrypted As String, Optional ByVal KeyLength As Integer = 256) As String
        'Dim cCrypt As cCrypt = New cCrypt
        If Not String.IsNullOrWhiteSpace(Encrypted) Then
            So_Helper_cCrypt.Decrypt(KeyLength, So_Helper_VarConvert.ConvertToString(Encrypted, False, ""))
            Return So_Helper_cCrypt.DecryptedString
        Else
            Return ""
        End If
    End Function
    Public Shared Function Inch2Millimeter(ByVal Inch As Double) As Double
        Dim mm As Double = Inch * 25.4 * 10 / 10
        Return mm
    End Function
    Public Shared Function StringList_SplitIntoChunks(keys As List(Of String), chunkSize As Integer) As List(Of List(Of String))
        Return keys _
.Select(Function(x, i) New With {Key .Index = i, Key .Value = x}) _
.GroupBy(Function(x) (x.Index \ chunkSize)) _
.Select(Function(x) x.Select(Function(v) v.Value).ToList()) _
.ToList()
    End Function
    Public Shared Function splitList(ByVal OriginalList As List(Of String), ByVal chunkSize As Integer) As List(Of List(Of String))
        Dim TargetList As New List(Of List(Of String))
        Dim i As Integer = 0
        While i < OriginalList.Count
            TargetList.Add(OriginalList.GetRange(i, Math.Min(chunkSize, OriginalList.Count - i)))
            i += chunkSize
        End While
        Return TargetList
    End Function
    Public Shared Function GetRandomByString(ByVal ResultLength As Integer,
                                         Optional SelectFrom As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz12345678901234567890") As String
        Dim ran As New Random
        Dim strb As New System.Text.StringBuilder
        Dim chararray() As Char = SelectFrom.ToCharArray
        For i As Integer = 1 To ResultLength
            Dim ti As Integer = ran.Next(0, chararray.Length)
            strb.Append(chararray(ti))
        Next
        Return strb.ToString
    End Function
    Private Shared Function ReplaceFirst(ByVal text As String, ByVal search As String, ByVal replace As String) As String
        Dim pos As Integer = text.IndexOf(search)
        If pos < 0 Then
            Return text
        End If
        Return text.Substring(0, pos) & replace & text.Substring(pos + search.Length)
    End Function

#End Region

    Public Shared Function GetGroups(ByVal userName As String) As List(Of String)
        Dim result As List(Of String) = New List(Of String)()
        Dim wi As WindowsIdentity = New WindowsIdentity(userName)
        For Each group As IdentityReference In wi.Groups
            Try
                result.Add(group.Translate(GetType(NTAccount)).ToString())
            Catch ex As Exception
                So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
                If So_Helper.IsIDE() Then Stop
            End Try
        Next
        result.Sort()
        Return result
    End Function
    Public Shared Function GetProcess(name As String) As Process
        Dim ReturnValue As Process = Nothing
        For Each clsProcess As Process In Process.GetProcesses()
            If clsProcess.ProcessName.StartsWith(name, True, New CultureInfo("en-US")) Then
                ReturnValue = clsProcess
                Exit For
            End If
        Next
        Return ReturnValue
    End Function
    Public Shared Function IsGuiThread() As Boolean
        If Thread.CurrentThread.Name = "GuiThread" Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function IsProcessRunning(name As String) As Boolean
        Dim ReturnValue As Boolean = False
        For Each clsProcess As Process In Process.GetProcesses()
            'Debug.Print(clsProcess.ProcessName)
            If clsProcess.ProcessName.StartsWith(name, True, New CultureInfo("en-US")) Then
                ReturnValue = True
                Exit For
            End If
        Next
        Return ReturnValue
    End Function
    Public Shared Function GetArchitecture() As String
        Dim architecture As String = ""
        If So_Helper_VarConvert.ConvertToInteger(IntPtr.Size) > 4 Then
            architecture = "64 Bit"
        Else
            architecture = "32 Bit"
        End If
        Return architecture
    End Function
    Public Shared Function GetCallingProc() As String
        Dim Trace As New StackTrace()
        Try
            Return Trace.GetFrame(2).GetMethod().Name
        Catch ex As Exception
            Return ""
        End Try
    End Function
    Public Shared Function IsApplicationRunning(ByVal Dateiname As String) As Boolean
        Try
            If Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Dateiname)).Length > 1 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
            Return False
        End Try

    End Function
    Public Shared Function IsNewVersion(ByVal sCurrent As String, ByVal sNew As String) As Boolean
        Dim saCurrrent As String()
        Dim saNew As String()
        Dim nLength As Integer = 0
        Dim IsNewVersionVal As Boolean = False
        If String.IsNullOrWhiteSpace(sCurrent) OrElse String.IsNullOrWhiteSpace(sNew) Then
            Return IsNewVersionVal
        End If
        saCurrrent = Split(sCurrent, ".")
        saNew = Split(sNew, ".")
        nLength = saCurrrent.Length
        If saNew.Length <> nLength Then
            Return IsNewVersionVal
        End If
        For i As Integer = 0 To nLength
            Select Case Val(saNew(i)) - Val(saCurrrent(i))
                Case Is < 0
                    ' Teilnummer der "neuen" Version ist kleiner
                    Exit For
                Case 0
                    ' do nothing - nächste Stelle prüfen
                Case Is > 0
                    ' Teilnummer der "neuen" Version ist größer
                    IsNewVersionVal = True
                    Exit For
            End Select
        Next i
        Return IsNewVersionVal
    End Function
    Public Shared Function ParameterQuery(ByVal TempDBConnection As clsDBconnect) As String
        If IsIDE() Then
            Dim CommandText As String = ""
            Try
                If Not IsNothing(TempDBConnection.cmd) AndAlso TempDBConnection.DBIsOpen Then
                    CommandText = TempDBConnection.cmd.CommandText
                    For Each p As MySqlParameter In TempDBConnection.cmd.Parameters
                        Dim value As String = ""
                        If IsNothing(p.Value) OrElse IsDBNull(p.Value) Then
                            value = "null"
                        Else
                            value = p.Value.ToString
                        End If
                        If p.MySqlDbType.ToString.Contains("VarChar") Then
                            CommandText = CommandText.Replace(p.ParameterName, "'" & CStr(value) & "'")
                        ElseIf p.MySqlDbType.ToString.Contains("Blob") Then
                            CommandText = CommandText.Replace(p.ParameterName, "'BLOB'")
                        Else
                            CommandText = CommandText.Replace(CStr(p.ParameterName), CStr(value))
                        End If
                    Next
                End If
                Return CommandText
            Catch ex As Exception
                If IsIDE() Then Stop
                Return ex.Message
            End Try
        Else
            Return ""
        End If

    End Function


    Public Shared Function IsIDE(ByVal Optional AlwaysIsIde As Boolean = False) As Boolean
        If Debugger.IsAttached AndAlso Not AlwaysIsIde Then
            Return True
        ElseIf Not Debugger.IsAttached AndAlso Not AlwaysIsIde Then
            Return False
        ElseIf Debugger.IsAttached AndAlso AlwaysIsIde Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Sub wait(ByVal milliseconds As Integer)
        For x As Integer = 1 To milliseconds
            Thread.Sleep(1)
            Application.DoEvents()
        Next
    End Sub

End Class
