Imports System.Text.RegularExpressions
Imports System.Threading

Imports MySql.Data.MySqlClient
'Imports log4net.Core
'Imports TimeShare_Helper
'Imports TimeShare_Error

Public MustInherit Class clsDBconnectLocal
    Friend Shared DBRegFolder_LizenzGenerator As String = ""

    Friend Shared CmdTimeout As Integer = 3600
    Friend Shared LizenzGeneratorConnectionString As String
    Friend Shared db_prefix As String = ""
    Friend Shared MySqlCommandText As String = ""
    Friend Shared db_prefix_prost As String = ""

    Friend Shared db_table_licenses As String = ""
    Friend Shared db_table_programs As String = ""
    Friend Shared db_table_customer As String = ""
    Friend Shared db_table_condrop_return_tracking As String = ""
    Friend Shared db_table_condrop_Origin As String = ""


    Friend Shared host_LizenzGenerator As String = ""
    Friend Shared pass_LizenzGenerator As String = ""
    Friend Shared user_LizenzGenerator As String = ""

    Friend Shared cmd_ As New MySqlCommand
    Friend Shared con_ As New MySqlConnection
    Friend Shared constr_ As String = ""
    Friend Shared DBIsOpen_ As Boolean = False


#Region "Database"
    Friend Shared db_table_condrop_changelog As String = ""
    Friend Shared db_table_condrop_Settings As String = ""
    Friend Shared db_table_prost_approval_checklist As String = ""
    Friend Shared db_table_prost_company As String = ""
    Friend Shared db_table_prost_documents As String = ""
    Friend Shared db_table_prost_country As String = ""
    Friend Shared db_table_prost_customers As String = ""
    Friend Shared db_table_prost_employment As String = ""
    Friend Shared db_table_prost_load_classes As String = ""
    Friend Shared db_table_prost_logs As String = ""
    Friend Shared db_table_prost_mail_placeholder As String = ""
    Friend Shared db_table_prost_mail_recipients As String = ""
    Friend Shared db_table_prost_material As String = ""
    Friend Shared db_table_prost_material_def As String = ""
    Friend Shared db_table_prost_material_groups As String = ""
    Friend Shared db_table_prost_offices As String = ""
    Friend Shared db_table_prost_order_approval_checklist As String = ""
    Friend Shared db_table_prost_order_DirectedHours As String = ""
    Friend Shared db_table_prost_order_load_class As String = ""
    Friend Shared db_table_prost_order_log As String = ""
    Friend Shared db_table_prost_order_main As String = ""
    Friend Shared db_table_prost_order_material As String = ""
    Friend Shared db_table_prost_order_risk_assessment As String = ""
    Friend Shared db_table_prost_order_WorkDescription As String = ""
    Friend Shared db_table_prost_personal As String = ""
    Friend Shared db_table_prost_personal_groups As String = ""
    Friend Shared db_table_prost_settings As String = ""
    Friend Shared db_table_prost_user As String = ""
    Friend Shared db_table_prost_history As String = ""

    Friend Shared db_table_condrop_country As String = ""

    Friend Shared host_prost As String = ""
    Friend Shared pass_prost As String = ""
    Friend Shared user_prost As String = ""
    Friend Shared port_prost As Integer = 3306

    '? Tabellen der Updater-DB
    Friend Shared db_table_updates As String = ""
    Friend Shared db_table_updater_logs As String = ""
    Friend Shared db_table_updater_changelog As String = ""

    'Friend Shared db_prefix As String = ""
    Friend Shared DB_Query As String = ""
    Friend Shared DB_CountQuery As String = ""

    'Friend Shared MySqlCommandText As String
    Friend Shared UpdateConnectionString As String
    Friend Shared LicenseConnectionString As String
    Friend Shared ProStrukturaConnectionString As String

    'Friend Shared clsDBconnectLocal. CmdTimeout As Integer = 0
    Friend Shared cmd_test As New MySqlCommand
    Friend Shared con_test As New MySqlConnection

    Friend Shared DBRegFolder_ProStruktura As String = ""

    Friend Shared db_prost As String = ""
    'Friend Shared db_prefix_prost As String

#End Region


    Public Enum SelectDatabase
        ConDrop
        Progenerator
        Prostruktura
        Multiserver
        Updater
        License
    End Enum
    Friend Structure ConnStringDef
        Friend MySqlConString As String
        Friend TestServer As String
        Friend Sub New(ByVal Optional constr As String = "",
                       ByVal Optional TestServer As String = "")
            MySqlConString = constr
            Me.TestServer = TestServer
        End Sub
    End Structure
    Friend Shared Sub GetDBValues()

        Try
            If So_Helper.KeyExist(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database") Then
                'Using FormSplash As New frmSplash
                clsDBconnectLocal.db_table_prost_settings = So_Helper_Convert.ConvertToString(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database", "prost_settings"), False, "")
                If clsDBconnectLocal.db_table_prost_settings = "" Then
                    clsDBconnectLocal.db_table_prost_settings = "prost_settings"
                    So_Helper.RegistryWriteValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database", "prost_settings", clsDBconnectLocal.db_table_prost_settings)
                End If
                db_table_condrop_Settings = clsDBconnectLocal.db_table_prost_settings
                clsDBconnectLocal.DBRegFolder_ProStruktura = So_Helper_Convert.ConvertToString(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database", "DBRegFolder_ProStruktura"), False, "")

                If String.IsNullOrEmpty(DBRegFolder_ProStruktura) OrElse Not So_Helper.KeyExist(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database\" & clsDBconnectLocal.DBRegFolder_ProStruktura) Then
                    So_Helper.RenameSubKey(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database", "prost_default_sicher", "prost_default")
                    clsDBconnectLocal.DBRegFolder_ProStruktura = "prost_default"
                    So_Helper.RegistryWriteValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database", "DBRegFolder_ProStruktura", clsDBconnectLocal.DBRegFolder_ProStruktura)
                End If

                'FormSplash.LblI_InfoText.Text = "DB-Einstellung: User"
                So_Helper_cCrypt.Decrypt(256, So_Helper_Convert.ConvertToString(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database\" & clsDBconnectLocal.DBRegFolder_ProStruktura, "db_User"), False, ""))
                clsDBconnectLocal.user_prost = So_Helper_cCrypt.DecryptedString
                'FormSplash.LblI_InfoText.Text = "DB-Einstellung: Password"
                So_Helper_cCrypt.Decrypt(256, So_Helper_Convert.ConvertToString(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database\" & clsDBconnectLocal.DBRegFolder_ProStruktura, "db_Password"), False, ""))
                pass_prost = So_Helper_cCrypt.DecryptedString
                'FormSplash.LblI_InfoText.Text = "DB-Einstellung: Host"
                So_Helper_cCrypt.Decrypt(256, So_Helper_Convert.ConvertToString(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database\" & clsDBconnectLocal.DBRegFolder_ProStruktura, "db_Host"), False, ""))
                host_prost = So_Helper_cCrypt.DecryptedString
                'FormSplash.LblI_InfoText.Text = "DB-Einstellung: DB-Name"
                So_Helper_cCrypt.Decrypt(256, So_Helper_Convert.ConvertToString(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database\" & clsDBconnectLocal.DBRegFolder_ProStruktura, "db_Databasename"), False, ""))
                db_prost = So_Helper_cCrypt.DecryptedString
                port_prost = So_Helper_Convert.ConvertToInteger(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database\" & clsDBconnectLocal.DBRegFolder_ProStruktura, "db_Databaseport"), 0)
                If port_prost = 0 Then
                    port_prost = 3306
                End If

                'FormSplash.LblI_InfoText.Text = "DB-Einstellung: prefix"
                So_Helper_cCrypt.Decrypt(256, So_Helper_Convert.ConvertToString(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Database\" & clsDBconnectLocal.DBRegFolder_ProStruktura, "db_prefix"), False, ""))
                db_prefix_prost = So_Helper_cCrypt.DecryptedString
                db_prefix = db_prefix_prost

                MySqlCommandText = "set net_write_timeout=99999; set net_read_timeout=99999;MultipleActiveResultSets=True"
                ProStrukturaConnectionString = "Server=" & host_prost & "; UID=" & clsDBconnectLocal.user_prost & "; Password=" & pass_prost & "; Database=" & db_prost & "; Port=" & port_prost.ToString & ";Convert Zero Datetime=True;SslMode=none"

                db_table_prost_approval_checklist = db_prefix_prost & "_approval_checklist"
                db_table_prost_customers = db_prefix_prost & "_customers"
                db_table_prost_mail_placeholder = db_prefix_prost & "_mail_placeholder"
                db_table_prost_load_classes = db_prefix_prost & "_load_classes"
                db_table_prost_logs = db_prefix_prost & "_logs"
                db_table_prost_mail_recipients = db_prefix_prost & "_mail_recipients"
                db_table_prost_material = db_prefix_prost & "_material"
                db_table_prost_material_def = db_prefix_prost & "_material_def"
                db_table_prost_material_groups = db_prefix_prost & "_material_groups"
                db_table_prost_offices = db_prefix_prost & "_offices"
                db_table_prost_order_approval_checklist = db_prefix_prost & "_order_approval_checklist"
                db_table_prost_order_load_class = db_prefix_prost & "_order_load_class"
                db_table_prost_order_log = db_prefix_prost & "_order_log"
                db_table_prost_order_main = db_prefix_prost & "_order_main"
                db_table_prost_order_material = db_prefix_prost & "_order_material"
                db_table_prost_order_DirectedHours = db_prefix_prost & "_order_DirectedHours"
                db_table_prost_order_risk_assessment = db_prefix_prost & "_order_risk_assessment"
                db_table_prost_personal = db_prefix_prost & "_personal"
                db_table_prost_personal_groups = db_prefix_prost & "_personal_groups"
                db_table_prost_user = db_prefix_prost & "_user"
                db_table_prost_country = db_prefix_prost & "_country"
                db_table_prost_company = db_prefix_prost & "_company"
                db_table_prost_employment = db_prefix_prost & "_employment"
                db_table_prost_order_WorkDescription = db_prefix_prost & "_order_WorkDescription"
                db_table_prost_history = db_prefix_prost & "_history"
                db_table_prost_documents = db_prefix_prost & "_documents"

                '    FormSplash.LblI_InfoText.Text = "Tabellen eingelesen!"

                'End Using
            Else

                'Using FormMain As New frmMain
                '    Call FormMain.BtnI_Settings_Click(Nothing, Nothing)
                'End Using


            End If

        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Error_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
        End Try
    End Sub
    Friend Shared Function GetConnectionString(ByVal ConnectionString As String) As ConnStringDef
        Dim RetVal As New ConnStringDef
        RetVal.MySqlConString = ConnectionString
        Dim reg3 As Regex = New Regex("(Server=)(.*)(; UID.*)",
                                      RegexOptions.IgnoreCase Or RegexOptions.Singleline)
        Dim res3 As Match = reg3.Match(ConnectionString)
        If (res3.Success) Then
            RetVal.TestServer = res3.Groups(2).ToString
        End If
        Return RetVal
    End Function
    Private Shared Sub ConnectionCheck(ByVal user As String, ByVal pass As String, ByVal host As String, ByVal db As String, ByVal port As Integer)
        Dim CheckResult As New So_Helper_CheckConnect.CheckResults
        Dim DbCredential As New So_Helper_CheckConnect.DbCredentials

        Try
            So_Helper_CheckConnect.CheckInternet = True
            So_Helper_CheckConnect.CheckRegistry = True
            So_Helper_CheckConnect.CheckDb = True

            DbCredential.user = user
            DbCredential.pass = pass
            DbCredential.host = host
            DbCredential.db = db
            DbCredential.port = port

            Call So_Helper_CheckConnect.DoCheck()
            CheckResult = So_Helper_CheckConnect.CheckResult
            If Not CheckResult.InetResult Then
                MessageBox.Show(CheckResult.InetMessage, CheckResult.InetMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
                'Call'Helper_Logger.writelog(Level.Fatal, "Internetanbindung ist fehlgeschlagen." & CheckResult.InetMessage)
                'Application.Exit()
            End If
            If Not CheckResult.DbResult Then
                MessageBox.Show(CheckResult.DbMessage, CheckResult.DbMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
                'Call'Helper_Logger.writelog(Level.Fatal, "Datenbankanbindung ist fehlgeschlagen." & CheckResult.DbMessage)
                'Application.Exit()
            End If
            If Not CheckResult.RegResult Then
                MessageBox.Show(CheckResult.RegMessage, CheckResult.RegMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
                'Call'Helper_Logger.writelog(Level.Fatal, "Registry ist nicht lesbar." & CheckResult.RegMessage)
                'Application.Exit()
            End If

        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Error_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
        End Try
    End Sub
    Private Shared Function TestDbConnection(user As String, pass As String, host As String, db As String) As Boolean
        Dim result As Boolean = False
        Try
            If IsNothing(cmd_test.Connection) = False Then
                If cmd_test.Connection.State = ConnectionState.Open Then
                    con_test.Close()
                End If
            End If

            con_test.ConnectionString = "Server=" & host & "; UID=" & user & "; Password=" & pass & "; Database=" & db & ";Convert Zero Datetime=True"
            cmd_test = New MySqlCommand("set net_write_timeout=99999; set net_read_timeout=99999", con_test)
            '        cmd_test.Connection = con_test
            cmd_test.CommandTimeout = clsDBconnectLocal.CmdTimeout
            con_test.Open()

            If cmd_test.Connection.State = ConnectionState.Open Then
                'Helper_Logger.writelog(Level.Info, "Datenbankverbindung zu " & db & " wurde erfolgreich aufgebaut.")
                result = True
                con_test.Close()
            Else
                'Helper_Logger.writelog(Level.Info, "Datenbankverbindung zu " & db & " konnte nicht aufgebaut werden.")
                result = False
            End If

            Return result
        Catch ex As Exception
            Return False
        End Try
    End Function

    Friend Shared Function SelectCaseConnection(ByVal Connection As clsDBconnectLocal.SelectDatabase) As clsDBconnectLocal.ConnStringDef
        Dim RetVal As New clsDBconnectLocal.ConnStringDef
        Select Case Connection
            'Case clsDBconnectLocal.SelectDatabase.ConDrop
            '    ResVal = clsDBconnectLocal.GetConnectionString(ConDropConnectionString)
            '    constr_ = ResVal.constr_
            '    TestServer = ResVal.TestServer
            'Case clsDBconnectLocal.SelectDatabase.Progenerator
            '    ResVal = clsDBconnectLocal.GetConnectionString(ProgenConnectionString)
            '    constr_ = ResVal.constr_
            '    TestServer = ResVal.TestServer
            'Case clsDBconnectLocal.SelectDatabase.Multiserver
            '    ResVal = clsDBconnectLocal.GetConnectionString(MultiserverConnectionString)
            '    constr_ = ResVal.constr_
            '    TestServer = ResVal.TestServer
            'Case clsDBconnectLocal.SelectDatabase.Updater
            '    ResVal = clsDBconnectLocal.GetConnectionString(UpdateConnectionString)
            '    constr_ = ResVal.constr_
            '    TestServer = ResVal.TestServer
            'Case clsDBconnectLocal.SelectDatabase.LizenzGenerator
            '    ResVal = clsDBconnectLocal.GetConnectionString(clsDBconnectLocal.LizenzGeneratorConnectionString)
            '    constr_ = ResVal.constr_
            '    TestServer = ResVal.TestServer
            Case clsDBconnectLocal.SelectDatabase.License
                RetVal = clsDBconnectLocal.GetConnectionString(LicenseConnectionString)
            Case clsDBconnectLocal.SelectDatabase.Prostruktura
                RetVal = clsDBconnectLocal.GetConnectionString(ProStrukturaConnectionString)
        End Select
        Return RetVal
    End Function
    Friend Shared Function GetDataBaseName(ByVal Connection As SelectDatabase) As String
        Dim RetVal As String = ""
        Select Case Connection
            'Case clsDBconnectLocal.SelectDatabase.ConDrop
            '    ResVal = clsDBconnectLocal.GetConnectionString(ConDropConnectionString)
            '    constr_ = ResVal.constr_
            '    TestServer = ResVal.TestServer
            'Case clsDBconnectLocal.SelectDatabase.Progenerator
            '    ResVal = clsDBconnectLocal.GetConnectionString(ProgenConnectionString)
            '    constr_ = ResVal.constr_
            '    TestServer = ResVal.TestServer
            'Case clsDBconnectLocal.SelectDatabase.Multiserver
            '    ResVal = clsDBconnectLocal.GetConnectionString(MultiserverConnectionString)
            '    constr_ = ResVal.constr_
            '    TestServer = ResVal.TestServer
            'Case clsDBconnectLocal.SelectDatabase.Updater
            '    ResVal = clsDBconnectLocal.GetConnectionString(UpdateConnectionString)
            '    constr_ = ResVal.constr_
            '    TestServer = ResVal.TestServer
            'Case clsDBconnectLocal.SelectDatabase.LizenzGenerator
            '    ResVal = clsDBconnectLocal.GetConnectionString(clsDBconnectLocal.LizenzGeneratorConnectionString)
            '    constr_ = ResVal.constr_
            '    TestServer = ResVal.TestServer
            Case clsDBconnectLocal.SelectDatabase.License
                RetVal = "licgen_licenses"
            Case clsDBconnectLocal.SelectDatabase.Prostruktura
                RetVal = db_prost
        End Select
        Return RetVal
    End Function


End Class
