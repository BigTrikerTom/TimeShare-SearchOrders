Option Strict On

Imports System.ComponentModel
Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Threading
'Imports log4net.Core
Imports Microsoft.Win32
Imports MySql.Data.MySqlClient
Imports srm = System.Reflection.MethodBase

Public Class clsDBconnect
    Implements IDisposable
    Private ReadOnly managedResource As Component
    Private unmanagedResource As IntPtr
    Protected disposed As Boolean = False
    Friend Shared CmdTimeout As Integer = 0

    Friend Shared Sub GetDBValues()
        'Dim cCrypt As cCrypt = New cCrypt
        Try
            If So_Helper.KeyExist(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database") Then
                clsDBconnectLocal.db_table_prost_settings = CStr(So_Helper.Checkvar(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database", "prost_settings"), So_Helper.VarTypeEnu.Type_String))
                If clsDBconnectLocal.db_table_prost_settings = "" Then
                    clsDBconnectLocal.db_table_prost_settings = "prost_settings"
                    rb = So_Helper.RegistryWriteValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database", "prost_settings", clsDBconnectLocal.db_table_prost_settings)
                End If
                db_table_condrop_Settings = clsDBconnectLocal.db_table_prost_settings
                clsDBconnectLocal.DBRegFolder_ProStruktura = CStr(So_Helper.Checkvar(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database", "clsDBconnectLocal.DBRegFolder_ProStruktura"), So_Helper.VarTypeEnu.Type_String))

                If String.IsNullOrEmpty(clsDBconnectLocal.DBRegFolder_ProStruktura) OrElse Not So_Helper.KeyExist(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database\" & clsDBconnectLocal.DBRegFolder_ProStruktura) Then
                    rb = So_Helper.RenameSubKey(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database", "prost_default_sicher", "prost_default")
                    clsDBconnectLocal.DBRegFolder_ProStruktura = "prost_default"
                    rb = So_Helper.RegistryWriteValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database", "clsDBconnectLocal.DBRegFolder_ProStruktura", clsDBconnectLocal.DBRegFolder_ProStruktura)
                End If

                'frmSplash.LblI_InfoText.Text = "DB-Einstellung: User"
                So_Helper_cCrypt.Decrypt(256, CStr(So_Helper.Checkvar(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database\" & clsDBconnectLocal.DBRegFolder_ProStruktura, "db_User"), So_Helper.VarTypeEnu.Type_String)))
                user_prost = So_Helper_cCrypt.DecryptedString
                'frmSplash.LblI_InfoText.Text = "DB-Einstellung: Password"
                So_Helper_cCrypt.Decrypt(256, CStr(So_Helper.Checkvar(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database\" & clsDBconnectLocal.DBRegFolder_ProStruktura, "db_Password"), So_Helper.VarTypeEnu.Type_String)))
                pass_prost = So_Helper_cCrypt.DecryptedString
                'frmSplash.LblI_InfoText.Text = "DB-Einstellung: Host"
                So_Helper_cCrypt.Decrypt(256, CStr(So_Helper.Checkvar(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database\" & clsDBconnectLocal.DBRegFolder_ProStruktura, "db_Host"), So_Helper.VarTypeEnu.Type_String)))
                host_prost = So_Helper_cCrypt.DecryptedString
                'frmSplash.LblI_InfoText.Text = "DB-Einstellung: DB-Name"
                So_Helper_cCrypt.Decrypt(256, CStr(So_Helper.Checkvar(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database\" & clsDBconnectLocal.DBRegFolder_ProStruktura, "db_Databasename"), So_Helper.VarTypeEnu.Type_String)))
                db_prost = So_Helper_cCrypt.DecryptedString
                clsDBconnectLocal.port_prost = CInt(So_Helper.Checkvar(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database\" & clsDBconnectLocal.DBRegFolder_ProStruktura, "db_Databaseport"), So_Helper.VarTypeEnu.Type_Integer))
                If clsDBconnectLocal.port_prost = 0 Then
                    clsDBconnectLocal.port_prost = 3306
                End If

                'frmSplash.LblI_InfoText.Text = "DB-Einstellung: prefix"
                So_Helper_cCrypt.Decrypt(256, CStr(So_Helper.Checkvar(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database\" & clsDBconnectLocal.DBRegFolder_ProStruktura, "db_prefix"), So_Helper.VarTypeEnu.Type_String)))
                clsDBconnectLocal.db_prefix_prost = So_Helper_cCrypt.DecryptedString
                db_prefix = clsDBconnectLocal.db_prefix_prost

                MySqlCommandText = "set net_write_timeout=99999; set net_read_timeout=99999;MultipleActiveResultSets=True"
                ProStrukturaConnectionString = "Server=" & clsDBconnectLocal.host_prost & "; UID=" & clsDBconnectLocal.user_prost & "; Password=" & clsDBconnectLocal.pass_prost & "; Database=" & clsDBconnectLocal.db_prost & "; Port=" & clsDBconnectLocal.port_prost.ToString & ";Convert Zero Datetime=True"

                db_table_prost_approval_checklist = clsDBconnectLocal.db_prefix_prost & "_approval_checklist"
                db_table_prost_customers = clsDBconnectLocal.db_prefix_prost & "_customers"
                db_table_prost_mail_placeholder = clsDBconnectLocal.db_prefix_prost & "_mail_placeholder"
                db_table_prost_load_classes = clsDBconnectLocal.db_prefix_prost & "_load_classes"
                db_table_prost_logs = clsDBconnectLocal.db_prefix_prost & "_logs"
                db_table_prost_mail_recipients = clsDBconnectLocal.db_prefix_prost & "_mail_recipients"
                db_table_prost_material = clsDBconnectLocal.db_prefix_prost & "_material"
                db_table_prost_material_def = clsDBconnectLocal.db_prefix_prost & "_material_def"
                db_table_prost_material_groups = clsDBconnectLocal.db_prefix_prost & "_material_groups"
                db_table_prost_offices = clsDBconnectLocal.db_prefix_prost & "_offices"
                db_table_prost_order_approval_checklist = clsDBconnectLocal.db_prefix_prost & "_order_approval_checklist"
                db_table_prost_order_load_class = clsDBconnectLocal.db_prefix_prost & "_order_load_class"
                db_table_prost_order_log = clsDBconnectLocal.db_prefix_prost & "_order_log"
                db_table_prost_order_main = clsDBconnectLocal.db_prefix_prost & "_order_main"
                db_table_prost_order_material = clsDBconnectLocal.db_prefix_prost & "_order_material"
                db_table_prost_order_DirectedHours = clsDBconnectLocal.db_prefix_prost & "_order_DirectedHours"
                db_table_prost_order_risk_assessment = clsDBconnectLocal.db_prefix_prost & "_order_risk_assessment"
                db_table_prost_personal = clsDBconnectLocal.db_prefix_prost & "_personal"
                db_table_prost_personal_groups = clsDBconnectLocal.db_prefix_prost & "_personal_groups"
                db_table_prost_user = clsDBconnectLocal.db_prefix_prost & "_user"
                db_table_prost_country = clsDBconnectLocal.db_prefix_prost & "_country"
                db_table_prost_company = clsDBconnectLocal.db_prefix_prost & "_company"
                db_table_prost_employment = clsDBconnectLocal.db_prefix_prost & "_employment"
                db_table_prost_order_WorkDescription = clsDBconnectLocal.db_prefix_prost & "_order_WorkDescription"
                db_table_prost_history = clsDBconnectLocal.db_prefix_prost & "_history"
                db_table_prost_documents = clsDBconnectLocal.db_prefix_prost & "_documents"

                'frmSplash.LblI_InfoText.Text = "Tabellen eingelesen!"
            Else

                'Call frmMain.BtnI_Settings_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
        End Try
    End Sub
    'Private Shared Function TestDbConnection(user As String, pass As String, host As String, db As String) As Boolean
    '    Dim result As Boolean = False
    '    Try
    '        If IsNothing(cmd_test.Connection) = False Then
    '            If cmd_test.Connection.State = ConnectionState.Open Then
    '                con_test.Close()
    '            End If
    '        End If

    '        con_test.ConnectionString = "Server=" & host & "; UID=" & user & "; Password=" & pass & "; Database=" & db & ";Convert Zero Datetime=True"
    '        cmd_test = New MySqlCommand("set net_write_timeout=99999; set net_read_timeout=99999", con_test)
    '        '        cmd_test.Connection = con_test
    '        cmd_test.CommandTimeout = CmdTimeout
    '        con_test.Open()

    '        If cmd_test.Connection.State = ConnectionState.Open Then
    '            clsLogger.writelog(Level.Info, "Datenbankverbindung zu " & db & " wurde erfolgreich aufgebaut.")
    '            result = True
    '            con_test.Close()
    '        Else
    '            clsLogger.writelog(Level.Info, "Datenbankverbindung zu " & db & " konnte nicht aufgebaut werden.")
    '            result = False
    '        End If

    '        Return result
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    Protected Overridable Overloads Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposed Then
            If disposing Then
                'managedResource.Dispose()
                con_.Close()
                cmd_.Dispose()
                con_.Dispose()
                con_ = Nothing
                cmd_ = Nothing
                constr_ = Nothing
                DBIsOpen_ = False
                unmanagedResource = IntPtr.Zero
            End If
        End If
        Me.disposed = True
    End Sub

#Region "Zustandsvariablen"

    Private cmd_ As New MySqlCommand
    Private con_ As New MySqlConnection
    Private constr_ As String = ""
    Private DBIsOpen_ As Boolean = False

    Public Enum SelectDatabase
        ConDrop
        Progenerator
        Prostruktura
        Multiserver
        Updater
        License
    End Enum

#End Region

#Region "Methoden"

    Public Function connect(Optional ByVal Connection As clsDBconnectLocal.SelectDatabase = clsDBconnectLocal.SelectDatabase.Prostruktura) As Boolean
        If Me.disposed Then
            Throw New ObjectDisposedException(Me.GetType().ToString, "This object has been disposed.")
        End If
        Dim TestServer As String = ""
        Try
            Select Case Connection
                Case clsDBconnectLocal.SelectDatabase.Prostruktura
                    constr_ = clsDBconnectLocal.ProStrukturaConnectionString
                    Dim reg1 As Regex = New Regex("(Server=)(.*)(; UID.*)",
                                                  RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                    Dim res1 As Match = reg1.Match(clsDBconnectLocal.ProStrukturaConnectionString)
                    If (res1.Success) Then
                        TestServer = res1.Groups(2).ToString
                    End If
                Case clsDBconnectLocal.SelectDatabase.License
                    constr_ = clsDBconnectLocal.LicenseConnectionString
                    Dim reg3 As Regex = New Regex("(Server=)(.*)(; UID.*)",
                                                  RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                    Dim res3 As Match = reg3.Match(clsDBconnectLocal.LicenseConnectionString)
                    If (res3.Success) Then
                        TestServer = res3.Groups(2).ToString
                    End If
            End Select
            con_.ConnectionString = constr_
            cmd_ = New MySqlCommand(clsDBconnectLocal.MySqlCommandText, con_)
            cmd_.CommandTimeout = CmdTimeout
            For I As Integer = 1 To 10
                Try
                    con_.Open()
                    DBIsOpen_ = True
                    Exit For
                Catch ex As Exception
                    Host4InternetTest = TestServer
                    con_.Dispose()
                    DBIsOpen_ = False
                    If ex.Message <> "Timeout in IO operation" Then
                        Throw
                    End If
                End Try
                So_Helper.wait(500)
            Next
        Catch ex As MySqlException
            Dim info As String = "Connectionstring: " & constr_ & vbCrLf
            info = info & "MySqlCommandText: " & clsDBconnectLocal.MySqlCommandText
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Error_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            DBIsOpen_ = False
        End Try
        'frmMain.IsDBConnected = DBIsOpen_
        Return DBIsOpen_
    End Function

    Public Sub close()
        If Me.disposed Then
            Throw New ObjectDisposedException(Me.GetType().ToString, "This object has been disposed.")
        End If
        cmd_.Dispose()
        con_.Close()
        con_.Dispose()
        Me.Dispose()
        'frmMain.IsDBConnected = DBIsOpen_
        DBIsOpen_ = False
    End Sub

#End Region

#Region "Eigenschaften"

    Public ReadOnly Property cmd() As MySqlCommand
        Get
            Return cmd_
        End Get
    End Property

    Public ReadOnly Property DBIsOpen() As Boolean
        Get
            Return DBIsOpen_
        End Get
    End Property

#End Region

#Region " IDisposable Support "
    ' Do not change or add Overridable to these methods.
    ' Put cleanup code in Dispose(ByVal disposing As Boolean).
    Public Overloads Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub

#End Region
End Class
