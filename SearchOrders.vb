
Imports System.Data.SqlClient
Imports System.Threading

Imports DevComponents.DotNetBar.Controls
Imports DevComponents.Editors

Imports MySql.Data.MySqlClient

Public Class SearchOrders
    Public Shared Event Btn_OpenOrderClick(ByVal id As Integer, ByVal order_id As Integer, ByVal best_nr As String, ByVal best_nr1 As String)

    Public Structure CustomerData
        Public id As Integer
        Public office_id As Integer
        Public customer_id As Integer
        Public vendor_id As String
        Public order_pos_nr As Integer
        Public display_name As String
        Public firmenname As String
        Public strasse As String
        Public plz As String
        Public ort As String
        Public land As String
        Public email As String
        Public telefon As String
        Public anspr_name As String
        Public anspr_telefon As String
        Public anspr_mobil As String
        Public anspr_email As String
        Public mail_orderaddress As String
        Public mail_ordername As String
        Public mail_orderconfirmaddress As String
        Public mail_orderconfirmname As String
        Public mail_dldaddress As String
        Public mail_dldname As String
        Public mail_reportingaddress As String
        Public mail_reportingname As String
        Public mail_freigabe As String
        Public mail_aufmass As String
        Public OpeningHours As String
        Public deputy As String
        Public mail_order_subject As String
        Public mail_order_body_text As String
        Public mail_order_body_html As String
        Public mail_orderconfirm_subject As String
        Public mail_orderconfirm_body_text As String
        Public mail_orderconfirm_body_html As String
        Public mail_dldexport_subject As String
        Public mail_dldexport_body_text As String
        Public mail_dldexport_body_html As String
        Public mail_reporting_subject As String
        Public mail_reporting_body_text As String
        Public mail_reporting_body_html As String
    End Structure
    Public Structure OrderData
        Public id As Integer
        Public office_id As Integer
        Public office As String
        Public customer_id As Integer
        Public order_id As Integer
        Public best_nr As String
        Public best_nr1 As String
        Public order_date As Date
        Public customer_name As String
        Public purchaser As String
        Public gerbestnr As String
        Public site As String
        Public construction As String
        Public kontrakt_nr As String
        Public ger_order_nr As String
        Public subproject As String
        Public cost_unit As String
        Public user As String
        Public cost_centre As String
        Public order_pos_nr As String
        Public project_nr As String
        Public build_height As String
        Public length As Double
        Public width As Double
        Public height As Double
        Public place As String
        Public delivery_date As Date
        Public dismounting_date As Date
        Public ger_approval As String
        Public DirectedHours As String
        Public work_description As String
        Public progress As String
        Public writeable_h_order As Boolean
        Public writeable_b_order As Boolean
        Public writeable_h_aufmass As Boolean
        Public writeable_b_aufmass As Boolean
        Public writeable_h_freigabe As Boolean
        Public writeable_b_freigabe As Boolean
        Public writeable_h_abbau As Boolean
        Public writeable_b_abbau As Boolean
        Public order_created_by As String
        Public order_created_date As Date
        Public order_date_abbau As Date

    End Structure
    Public Structure OfficeData
        Public id As Integer
        Public office_id As Integer
        Public name As String
        Public strasse As String
        Public hsnr As String
        Public plz As String
        Public ort As String
        Public telefon As String
        Public fax As String
        Public email As String
        Public comment As String
    End Structure


#Region "Properties"
    Private Shared _RecentListLengthVal As Integer = 0

    Private Property RecentListLengthVal() As Integer
        Get
            _RecentListLengthVal = CInt(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Defaults", "RecentListLength", So_Helper.VarTypeEnu.Type_Integer))
            If _RecentListLengthVal > 0 Then
                Cb_Last30Days.Text = "Letzte " & _RecentListLengthVal.ToString & " Tage"
                Cb_Last30Orders.Text = "Letzte " & _RecentListLengthVal.ToString & " Aufträge"
            End If
            Return _RecentListLengthVal
        End Get
        Set(ByVal value As Integer)
            _RecentListLengthVal = value
            Dim res As Boolean = So_Helper.RegistryWriteValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Defaults", "RecentListLength", _RecentListLengthVal)

        End Set
    End Property

    Private Shared _RegistryPath As String = "Suchtext"
    Public Property RegistryPath() As String
        Get
            Return _RegistryPath
        End Get
        Set(ByVal value As String)
            _RegistryPath = value
        End Set
    End Property

    Private Shared _tb_SearchFor_WatermarkText As String = "Suchtext"
    Public WriteOnly Property Tb_SearchFor_WatermarkText() As String
        Set(ByVal value As String)
            Tb_SearchFor.WatermarkText = value
        End Set
    End Property
    Private Shared _cbil_Customer_List As New List(Of ComboItem)
    Public Property cbil_Customer_List() As List(Of ComboItem)
        Get
            Return _cbil_Customer_List
        End Get
        Set(ByVal value As List(Of ComboItem))
            _cbil_Customer_List = value
            Cbb_EditOrderCustomer.Items.AddRange(_cbil_Customer_List.ToArray)
        End Set
    End Property
    Private Shared _cbil_Order_List As New List(Of ComboItem)
    Public Property cbil_Order_List() As List(Of ComboItem)
        Get
            Return _cbil_Order_List
        End Get
        Set(ByVal value As List(Of ComboItem))
            _cbil_Order_List = value
            Cbb_EditOrderOrderId.Items.AddRange(_cbil_Order_List.ToArray)
        End Set
    End Property
    Private Shared _cbil_Office_List As New List(Of ComboItem)
    Public Property cbil_Office_List() As List(Of ComboItem)
        Get
            Return _cbil_Office_List
        End Get
        Set(ByVal value As List(Of ComboItem))
            _cbil_Office_List = value
            Cbb_EditOrderOffice.Items.AddRange(_cbil_Office_List.ToArray)
        End Set
    End Property

    Private Shared _officeDataList As List(Of OfficeData)
    Public Shared Property OfficeDataList() As List(Of OfficeData)
        Get
            Return _officeDataList
        End Get
        Set(ByVal value As List(Of OfficeData))
            _officeDataList = value
        End Set
    End Property
    Private Shared _customerDataList As List(Of CustomerData)
    Public Shared Property CustomerDataList() As List(Of CustomerData)
        Get
            Return _customerDataList
        End Get
        Set(ByVal value As List(Of CustomerData))
            _customerDataList = value
        End Set
    End Property
    Private Shared _orderDataList As List(Of OrderData)
    Public Shared Property OrderDataList() As List(Of OrderData)
        Get
            Return _orderDataList
        End Get
        Set(ByVal value As List(Of OrderData))
            _orderDataList = value
        End Set
    End Property

    Private _tb_SearchFor_Text As String = ""
    Public Property Tb_SearchFor_Text() As String
        Get
            _tb_SearchFor_Text = Tb_SearchFor.Text
            Return _tb_SearchFor_Text
        End Get
        Set(ByVal value As String)
            _tb_SearchFor_Text = value
            Tb_SearchFor.Text = value
        End Set
    End Property

    Private Shared _title As String = ""
    Public WriteOnly Property Title() As String
        Set(ByVal value As String)
            _title = value
            GroupPanel1.Text = value
        End Set
    End Property

    Private Shared _search_Customer_Text As String = ""
    Public Shared Property Search_Customer_Text() As String
        Get
            Return _search_Customer_Text
        End Get
        Set(ByVal value As String)
            _search_Customer_Text = value
        End Set
    End Property
    Private Shared _search_Customer_SelectedText As String = ""
    Public Shared Property Search_Customer_SelectedText() As String
        Get
            Return _search_Customer_SelectedText
        End Get
        Set(ByVal value As String)
            _search_Customer_SelectedText = value
        End Set
    End Property


    Private Shared _search_OrderId_Text As String = ""
    Public Shared Property Search_OrderId_Text() As String
        Get
            Return _search_OrderId_Text
        End Get
        Set(ByVal value As String)
            _search_OrderId_Text = value
        End Set
    End Property
    Private Shared _search_OrderId_SelectedText As String = ""
    Public Shared Property Search_OrderId_SelectedText() As String
        Get
            Return _search_OrderId_SelectedText
        End Get
        Set(ByVal value As String)
            _search_OrderId_SelectedText = value
        End Set
    End Property


    Private Shared _search_Office_Text As String = ""
    Public Shared Property Search_Office_Text() As String
        Get
            Return _search_Office_Text
        End Get
        Set(ByVal value As String)
            _search_Office_Text = value
        End Set
    End Property
    Private Shared _search_Office_SelectedText As String = ""
    Public Shared Property Search_Office_SelectedText() As String
        Get
            Return _search_Office_SelectedText
        End Get
        Set(ByVal value As String)
            _search_Office_SelectedText = value
        End Set
    End Property


#End Region

    Private Sub SearchOrders_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim cbil_Office As New List(Of ComboItem)
        'Dim cbi As New ComboItem
        'If OfficeDataList IsNot Nothing Then
        '    For Each Office As SearchOrders.OfficeData In OfficeDataList
        '        cbi = New ComboItem
        '        cbi.Text = Office.name
        '        cbi.Tag = Office
        '        cbil_Office.Add(cbi)
        '    Next
        'End If
        'Dim cbil_Customer As New List(Of ComboItem)
        'If CustomerDataList IsNot Nothing Then
        '    For Each Customer As SearchOrders.CustomerData In CustomerDataList
        '        cbi = New ComboItem
        '        cbi.Text = Customer.firmenname
        '        cbi.Tag = Customer
        '        cbil_Customer.Add(cbi)
        '    Next
        'End If
        'Dim cbil_Order As New List(Of ComboItem)
        'If OrderDataList IsNot Nothing Then
        '    For Each Order As SearchOrders.OrderData In OrderDataList
        '        cbi = New ComboItem
        '        cbi.Text = Order.order_id.ToString
        '        cbi.Tag = Order
        '        cbil_Order.Add(cbi)
        '    Next
        'End If
        'SearchOrders.cbil_Office_List = cbil_Office
        'SearchOrders.cbil_Customer_List = cbil_Customer
        'SearchOrders.cbil_Order_List = cbil_Order

    End Sub


    Public Sub New()
        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        Lv_ListFoundOrders.Items.Clear()
        Tb_SearchFor.Text = ""
        Cbb_EditOrderOffice.Text = ""
        Cbb_EditOrderCustomer.Text = ""
        Cbb_EditOrderOrderId.Text = ""
        Btn_OpenOrder.Enabled = False
        Btn_OpenOrder.Enabled = False

    End Sub
    Private Sub EnableControls(ByVal isenabled As Boolean)
        Tb_SearchFor.Enabled = isenabled
        Tb_SearchFor.ButtonCustom.Enabled = isenabled
        Tb_SearchFor.ButtonCustom2.Enabled = isenabled
        Cb_Search.Enabled = isenabled
        Lv_ListFoundOrders.Enabled = isenabled
        Cb_Select.Enabled = isenabled
        Cbb_EditOrderOffice.Enabled = isenabled
        Cbb_EditOrderCustomer.Enabled = isenabled
        Cbb_EditOrderOrderId.Enabled = isenabled
        Ii_LimitOrderCount.Enabled = isenabled
        'Btn_OpenOrder.Enabled = isenabled

    End Sub
    Private Sub Tb_SearchFor_ButtonCustomClick(sender As Object, e As EventArgs) Handles Tb_SearchFor.ButtonCustomClick
        Tb_SearchFor.Text = ""
    End Sub
    Private Sub Tb_SearchFor_TextChanged(sender As Object, e As EventArgs) Handles Tb_SearchFor.TextChanged
        If String.IsNullOrWhiteSpace(Tb_SearchFor.Text) Then
            Tb_SearchFor.ButtonCustom.Enabled = False
            Tb_SearchFor.ButtonCustom2.Enabled = False
        Else
            Tb_SearchFor.ButtonCustom.Enabled = True
            Tb_SearchFor.ButtonCustom2.Enabled = True
        End If
    End Sub
    Private Sub Cbb_EditOrderOrderId_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cbb_EditOrderOrderId.SelectedIndexChanged
        'Btn_OpenOrder.Text = "Bearbeiten"
        Dim cbi As ComboItem = CType(Cbb_EditOrderOrderId.SelectedItem, ComboItem)
        If Cbb_EditOrderOrderId.Text <> "" Then
            Cb_Select.Checked = True
            Dim TagValue As OrderData = CType(cbi.Tag, OrderData)
            Btn_OpenOrder.Tag = TagValue
            Btn_OpenOrder.Enabled = True
        Else
            Cb_Select.Checked = False
            Btn_OpenOrder.Tag = Nothing
            Btn_OpenOrder.Enabled = False
        End If
    End Sub
    Private Sub Lv_ListFoundOrders_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_ListFoundOrders.SelectedIndexChanged
        If Lv_ListFoundOrders.SelectedItems.Count > 0 Then
            'Dim cbi As ListViewItem = CType(Lv_ListFoundOrders.SelectedItems(0), )
            Dim cbi As New ListViewItem
            If Lv_ListFoundOrders.SelectedItems.Count > 0 Then
                Cb_Search.Checked = True
                Dim TagValue As OrderData = CType(cbi.Tag, OrderData)
                Btn_OpenOrder.Tag = TagValue
                Btn_OpenOrder.Enabled = True
            Else
                Cb_Search.Checked = False
                Btn_OpenOrder.Tag = Nothing
                Btn_OpenOrder.Enabled = False
            End If
        End If
    End Sub
    Private Sub Lv_ListFoundOrders_DoubleClick(sender As Object, e As EventArgs) Handles Lv_ListFoundOrders.DoubleClick
        If Lv_ListFoundOrders.SelectedItems.Count > 0 Then
            Dim cbi As New ListViewItem
            Dim TagValue As OrderData = CType(Lv_ListFoundOrders.SelectedItems(0).Tag, OrderData)
            Btn_OpenOrder.Tag = TagValue
            Call Btn_OpenOrder_Click(Nothing, Nothing)
        End If
    End Sub
    Private Sub Ii_LimitOrderCount_ValueChanged(sender As Object, e As EventArgs) Handles Ii_LimitOrderCount.ValueChanged
        rb = So_Helper.RegistryWriteValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Parameter", "ShowMaxOrders", Ii_LimitOrderCount.Value)
    End Sub
    Private Sub Btn_RefreshList_Click(sender As Object, e As EventArgs) Handles Btn_RefreshList.Click
        If Cb_Search.Checked Then
            Call Tb_SearchFor_ButtonCustom2Click(sender, e)
        ElseIf Cb_Last30Days.Checked Then
            Call Cb_Last30Days_CheckedChanged(sender, e)
        ElseIf Cb_Last30Orders.Checked Then
            Call Cb_Last30Orders_CheckedChanged(sender, e)
        End If
    End Sub






    Private Sub Tb_SearchFor_ButtonCustom2Click(sender As Object, e As EventArgs) Handles Tb_SearchFor.ButtonCustom2Click
        Dim query As String = ""
        Dim query1 As String = ""
        Dim such As String = Tb_SearchFor.Text.Replace("*", "%")

        Try
            Call EnableControls(False)
            Btn_OpenOrder.Enabled = False
            'Btn_OpenOrder.Text = "Bearbeiten"
            CircularProgress1.IsRunning = True
            CircularProgress1.Visible = True
            Using uctlsobso1p As New clsDBconnect
                Using fmtbx1bcc2p As New clsDBconnect
                    If uctlsobso1p.connect(clsDBconnectLocal.SelectDatabase.Prostruktura) AndAlso fmtbx1bcc2p.connect(clsDBconnectLocal.SelectDatabase.Prostruktura) Then
                        query = "SELECT * FROM `" & clsDBconnectLocal.db_table_prost_order_main & "` WHERE "
                        query = query & "`order_id` LIKE ?such? OR "
                        query = query & "`best_nr` LIKE ?such? OR "
                        query = query & "`customer_name` LIKE ?such? OR "
                        query = query & "`place` LIKE ?such? OR "
                        query = query & "`customer_id` LIKE ?such? "
                        query = query & ";"
                        uctlsobso1p.cmd.CommandText = query
                        uctlsobso1p.cmd.Parameters.Clear()
                        uctlsobso1p.cmd.Parameters.AddWithValue("?such?", such.Replace("*", "%"))
                        'Debug.Print(So_HelperDB.ParameterQuery(uctlsobso1p))
                        Lv_ListFoundOrders.BeginUpdate()
                        Lv_ListFoundOrders.Items.Clear()
                        Using reader_uctlsobso1p As MySqlDataReader = uctlsobso1p.cmd.ExecuteReader
                            While reader_uctlsobso1p.Read()
                                Application.DoEvents()
                                Dim OrderData As New OrderData
                                Dim lvi As New ListViewItem
                                Dim lvsi1 As New ListViewItem.ListViewSubItem
                                Dim lvsi2 As New ListViewItem.ListViewSubItem
                                Dim lvsi3 As New ListViewItem.ListViewSubItem
                                Dim lvsi4 As New ListViewItem.ListViewSubItem

                                OrderData.id = So_Helper_Convert.ConvertToInteger(reader_uctlsobso1p("id"), 0)
                                OrderData.office_id = So_Helper_Convert.ConvertToInteger(reader_uctlsobso1p("office_id"), 0)
                                OrderData.office = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("office"), False, "")
                                OrderData.customer_id = So_Helper_Convert.ConvertToInteger(reader_uctlsobso1p("customer_id"), 0)
                                OrderData.order_id = So_Helper_Convert.ConvertToInteger(reader_uctlsobso1p("order_id"), 0)
                                OrderData.best_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("best_nr"), False, "")
                                OrderData.best_nr1 = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("best_nr1"), False, "")
                                OrderData.order_date = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("order_date"), Nothing)
                                OrderData.customer_name = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("customer_name"), False, "")
                                OrderData.purchaser = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("purchaser"), False, "")
                                OrderData.gerbestnr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("gerbestnr"), False, "")
                                OrderData.site = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("site"), False, "")
                                OrderData.construction = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("construction"), False, "")
                                OrderData.kontrakt_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("kontrakt_nr"), False, "")
                                OrderData.ger_order_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("ger_order_nr"), False, "")
                                OrderData.subproject = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("subproject"), False, "")
                                OrderData.cost_unit = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("cost_unit"), False, "")
                                OrderData.user = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("user"), False, "")
                                OrderData.cost_centre = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("cost_centre"), False, "")
                                OrderData.order_pos_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("order_pos_nr"), False, "")
                                OrderData.project_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("project_nr"), False, "")
                                OrderData.build_height = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("build_height"), False, "")
                                OrderData.length = So_Helper_Convert.ConvertToDouble(reader_uctlsobso1p("length"), 0)
                                OrderData.width = So_Helper_Convert.ConvertToDouble(reader_uctlsobso1p("width"), 0)
                                OrderData.height = So_Helper_Convert.ConvertToDouble(reader_uctlsobso1p("height"), 0)
                                OrderData.place = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("place"), False, "")
                                OrderData.delivery_date = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("delivery_date"), Nothing)
                                OrderData.dismounting_date = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("dismounting_date"), Nothing)
                                OrderData.ger_approval = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("ger_approval"), False, "")
                                OrderData.DirectedHours = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("DirectedHours"), False, "")
                                OrderData.work_description = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("work_description"), False, "")
                                OrderData.progress = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("progress"), False, "")
                                OrderData.writeable_h_order = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_h_order"), False)
                                OrderData.writeable_b_order = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_b_order"), False)
                                OrderData.writeable_h_aufmass = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_h_aufmass"), False)
                                OrderData.writeable_b_aufmass = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_b_aufmass"), False)
                                OrderData.writeable_h_freigabe = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_h_freigabe"), False)
                                OrderData.writeable_b_freigabe = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_b_freigabe"), False)
                                OrderData.writeable_h_abbau = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_h_abbau"), False)
                                OrderData.writeable_b_abbau = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_b_abbau"), False)
                                OrderData.order_created_by = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("order_created_by"), False, "")
                                OrderData.order_created_date = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("order_created_date"), Nothing)
                                OrderData.order_date_abbau = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("order_date_abbau"), Nothing)

                                query1 = "SELECT * FROM `" & clsDBconnectLocal.db_table_prost_customers & "` WHERE "
                                query1 = query1 & "`customer_id` LIKE ?customer_id? "
                                query1 = query1 & ";"
                                fmtbx1bcc2p.cmd.CommandText = query1
                                fmtbx1bcc2p.cmd.Parameters.Clear()
                                fmtbx1bcc2p.cmd.Parameters.AddWithValue("?customer_id?", OrderData.customer_id)
                                'Debug.Print(So_HelperDB.ParameterQuery(fmtbx1bcc2p))
                                Dim display_name As String = ""
                                Using reader_fmtbx1bcc2p As MySqlDataReader = fmtbx1bcc2p.cmd.ExecuteReader
                                    While reader_fmtbx1bcc2p.Read()
                                        Application.DoEvents()
                                        display_name = So_Helper_Convert.ConvertToString(reader_fmtbx1bcc2p("display_name"), False, "")
                                    End While
                                End Using

                                lvi.Text = OrderData.order_id.ToString
                                lvi.Tag = OrderData
                                lvsi1.Text = OrderData.best_nr & "." & OrderData.best_nr1
                                lvi.SubItems.Add(lvsi1)
                                lvsi2.Text = display_name
                                lvi.SubItems.Add(lvsi2)
                                lvsi3.Text = OrderData.place
                                lvi.SubItems.Add(lvsi3)
                                lvsi4.Text = Format(OrderData.order_date, "yyyy-MM-dd")
                                lvi.SubItems.Add(lvsi4)
                                Lv_ListFoundOrders.Items.Add(lvi)
                            End While
                        End Using
                        Lv_ListFoundOrders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                        Lv_ListFoundOrders.EndUpdate()
                    End If
                End Using
            End Using
            If Lv_ListFoundOrders.Items.Count > 0 Then
                Cb_Search.Checked = True
            End If
        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
        End Try
        Call EnableControls(True)
        CircularProgress1.IsRunning = False
        CircularProgress1.Visible = False
    End Sub
    Private Sub Cbb_EditOrderOffice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cbb_EditOrderOffice.SelectedIndexChanged
        Dim office_id As Integer = 0
        Dim cbi As New ComboItem
        Dim query As String = ""
        Dim total As Integer = 0

        Try
            Call EnableControls(False)
            Btn_OpenOrder.Enabled = False
            'Btn_OpenOrder.Text = "Bearbeiten"
            CircularProgress1.IsRunning = True
            CircularProgress1.Visible = True
            cbi = CType(Cbb_EditOrderOffice.SelectedItem, ComboItem)
            Dim cbi_tag As OfficeData = CType(cbi.Tag, OfficeData)
            office_id = cbi_tag.office_id
            Dim Result As New List(Of CustomerData)
            Result = CustomerDataList.FindAll(Function(p1)
                                                  Return p1.office_id = office_id
                                              End Function)
            Using uctlsoceoo1p As New clsDBconnect
                If uctlsoceoo1p.connect(clsDBconnectLocal.SelectDatabase.Prostruktura) Then
                    So_Helper_Invoke.Invoke_ComboBoxEx_ItemsClear(Cbb_EditOrderCustomer)
                    So_Helper_Invoke.Invoke_ComboBoxEx_BeginUpdate(Cbb_EditOrderCustomer)
                    For Each Customer As CustomerData In Result
                        query = "SELECT COUNT(*) as `total` FROM `" & clsDBconnectLocal.db_table_prost_order_main & "` WHERE "
                        query = query & "`office_id` = ?office_id? AND "
                        query = query & "`customer_id` = ?customer_id? "
                        query = query & ";"
                        uctlsoceoo1p.cmd.CommandText = query
                        uctlsoceoo1p.cmd.Parameters.Clear()
                        Dim cbi1 As New ComboItem
                        cbi1 = CType(Cbb_EditOrderOffice.SelectedItem, ComboItem)
                        uctlsoceoo1p.cmd.Parameters.AddWithValue("?office_id?", office_id)
                        uctlsoceoo1p.cmd.Parameters.AddWithValue("?customer_id?", Customer.customer_id)
                        'Debug.Print(So_HelperDB.ParameterQuery(uctlsoceoo1p))
                        total = 0
                        Using reader_uctlsoceoo1p As MySqlDataReader = uctlsoceoo1p.cmd.ExecuteReader
                            While reader_uctlsoceoo1p.Read()
                                Application.DoEvents()
                                total = So_Helper_Convert.ConvertToInteger(reader_uctlsoceoo1p("total"), 0)
                            End While
                        End Using

                        Dim new_cbi As New ComboItem
                        If total = 1 Then
                            new_cbi.Text = Customer.display_name & " (" & CStr(total) & " Auftrag)"
                        Else
                            new_cbi.Text = Customer.display_name & " (" & CStr(total) & " Aufträge)"
                        End If
                        new_cbi.Tag = Customer
                        So_Helper_Invoke.Invoke_ComboBoxEx_Items_Add(Cbb_EditOrderCustomer, new_cbi)
                    Next
                    So_Helper_Invoke.Invoke_ComboBoxEx_EndUpdate(Cbb_EditOrderCustomer)
                End If
            End Using

        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
        End Try
        Call EnableControls(True)
        CircularProgress1.IsRunning = False
        CircularProgress1.Visible = False
        Cb_Select.Checked = True
    End Sub
    Private Sub Cbb_EditOrderCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cbb_EditOrderCustomer.SelectedIndexChanged
        Dim customer_id As Integer = 0
        Dim cbi As New ComboItem
        Btn_OpenOrder.Enabled = False
        'Btn_OpenOrder.Text = "Öffnen"
        CircularProgress1.IsRunning = True
        CircularProgress1.Visible = True

        Try
            Call EnableControls(False)
            cbi = CType(Cbb_EditOrderCustomer.SelectedItem, ComboItem)
            Dim cbi_tag As CustomerData = CType(cbi.Tag, CustomerData)
            customer_id = cbi_tag.customer_id
            Dim Result As New List(Of OrderData)

            Result = OrderDataList.FindAll(Function(p1)
                                               Return p1.customer_id = customer_id
                                           End Function)
            So_Helper_Invoke.Invoke_ComboBoxEx_ItemsClear(Cbb_EditOrderOrderId)
            So_Helper_Invoke.Invoke_ComboBoxEx_BeginUpdate(Cbb_EditOrderOrderId)

            Dim cbiList As New List(Of ComboItem)
            For Each Order As OrderData In Result
                Application.DoEvents()
                Dim new_cbi As New ComboItem
                new_cbi.Text = Order.order_id & " (" & Order.best_nr & ")"
                new_cbi.Tag = Order
                cbiList.Add(new_cbi)
            Next
            So_Helper_Invoke.Invoke_ComboBoxEx_Items_AddRange(Cbb_EditOrderOrderId, cbiList)
            So_Helper_Invoke.Invoke_ComboBoxEx_EndUpdate(Cbb_EditOrderOrderId)
            If Cbb_EditOrderOrderId.Items.Count > 0 Then
                So_Helper_Invoke.Invoke_ComboBoxEx_SelectedIndex(Cbb_EditOrderOrderId, 0)
            End If
        Catch ex As Exception
            So_Helper_ErrorHandling.HandleErrorCatch(ex, So_Helper.GetCallingProc(), System.Reflection.MethodBase.GetCurrentMethod().Name, Environment.CurrentManagedThreadId, False, False)
            If So_Helper.IsIDE() Then Stop
        End Try
        Call EnableControls(True)
        CircularProgress1.IsRunning = False
        CircularProgress1.Visible = False
        Cb_Select.Checked = True
    End Sub

    Private Sub Btn_OpenOrder_Click(sender As Object, e As EventArgs) Handles Btn_OpenOrder.Click
        Dim order_id As Integer = 0
        Dim best_nr As String = ""
        Dim best_nr1 As String = ""
        Dim id As Integer = 0
        Dim TagValue As OrderData = CType(Btn_OpenOrder.Tag, OrderData)
        order_id = CInt(TagValue.order_id)
        best_nr = TagValue.best_nr.ToString
        best_nr1 = TagValue.best_nr1.ToString
        id = TagValue.id
        'Btn_OpenOrder.Text = "Aktualisieren"
        RaiseEvent Btn_OpenOrderClick(id, order_id, best_nr, best_nr1)
    End Sub
    Private Sub Cb_Last30Days_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Last30Days.CheckedChanged
        If Not Cb_Last30Days.Checked Then
            Exit Sub
        End If
        Dim query As String = ""
        Dim query1 As String = ""

        Tb_SearchFor.Text = ""
        Call EnableControls(False)
        Btn_OpenOrder.Enabled = False
        'Btn_OpenOrder.Text = "Bearbeiten"
        CircularProgress1.IsRunning = True
        CircularProgress1.Visible = True
        Using uctlsobso1p As New clsDBconnect
            Using fmtbx1bcc2p As New clsDBconnect
                If uctlsobso1p.connect(clsDBconnectLocal.SelectDatabase.Prostruktura) AndAlso fmtbx1bcc2p.connect(clsDBconnectLocal.SelectDatabase.Prostruktura) Then
                    RecentListLengthVal = CInt(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Defaults", "RecentListLength", So_Helper.VarTypeEnu.Type_Integer))
                    If RecentListLengthVal < 5 Then
                        RecentListLengthVal = 30
                    End If
                    query = "SELECT * FROM `" & clsDBconnectLocal.db_table_prost_order_main & "` WHERE "
                    query = query & "`order_date` >= ?such? "
                    query = query & "ORDER BY `order_date` DESC "
                    query = query & ";"
                    uctlsobso1p.cmd.CommandText = query
                    uctlsobso1p.cmd.Parameters.Clear()
                    uctlsobso1p.cmd.Parameters.AddWithValue("?such?", DateAdd(DateInterval.Day, RecentListLengthVal * -1, Now).ToString("yyyy-MM-dd"))
                    Debug.Print(So_Helper.ParameterQuery(uctlsobso1p))
                    Lv_ListFoundOrders.BeginUpdate()
                    Lv_ListFoundOrders.Items.Clear()
                    Using reader_uctlsobso1p As MySqlDataReader = uctlsobso1p.cmd.ExecuteReader
                        While reader_uctlsobso1p.Read()
                            Application.DoEvents()
                            Dim OrderData As New OrderData
                            Dim lvi As New ListViewItem
                            Dim lvsi1 As New ListViewItem.ListViewSubItem
                            Dim lvsi2 As New ListViewItem.ListViewSubItem
                            Dim lvsi3 As New ListViewItem.ListViewSubItem
                            Dim lvsi4 As New ListViewItem.ListViewSubItem
                            Dim lvsi5 As New ListViewItem.ListViewSubItem
                            Dim lvsi6 As New ListViewItem.ListViewSubItem

                            OrderData.id = So_Helper_Convert.ConvertToInteger(reader_uctlsobso1p("id"), 0)
                            OrderData.office_id = So_Helper_Convert.ConvertToInteger(reader_uctlsobso1p("office_id"), 0)
                            OrderData.office = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("office"), False, "")
                            OrderData.customer_id = So_Helper_Convert.ConvertToInteger(reader_uctlsobso1p("customer_id"), 0)
                            OrderData.order_id = So_Helper_Convert.ConvertToInteger(reader_uctlsobso1p("order_id"), 0)
                            OrderData.best_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("best_nr"), False, "")
                            OrderData.best_nr1 = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("best_nr1"), False, "")
                            OrderData.order_date = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("order_date"), Nothing)
                            OrderData.customer_name = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("customer_name"), False, "")
                            OrderData.purchaser = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("purchaser"), False, "")
                            OrderData.gerbestnr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("gerbestnr"), False, "")
                            OrderData.site = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("site"), False, "")
                            OrderData.construction = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("construction"), False, "")
                            OrderData.kontrakt_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("kontrakt_nr"), False, "")
                            OrderData.ger_order_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("ger_order_nr"), False, "")
                            OrderData.subproject = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("subproject"), False, "")
                            OrderData.cost_unit = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("cost_unit"), False, "")
                            OrderData.user = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("user"), False, "")
                            OrderData.cost_centre = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("cost_centre"), False, "")
                            OrderData.order_pos_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("order_pos_nr"), False, "")
                            OrderData.project_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("project_nr"), False, "")
                            OrderData.build_height = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("build_height"), False, "")
                            OrderData.length = So_Helper_Convert.ConvertToDouble(reader_uctlsobso1p("length"), 0)
                            OrderData.width = So_Helper_Convert.ConvertToDouble(reader_uctlsobso1p("width"), 0)
                            OrderData.height = So_Helper_Convert.ConvertToDouble(reader_uctlsobso1p("height"), 0)
                            OrderData.place = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("place"), False, "")
                            OrderData.delivery_date = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("delivery_date"), Nothing)
                            OrderData.dismounting_date = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("dismounting_date"), Nothing)
                            OrderData.ger_approval = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("ger_approval"), False, "")
                            OrderData.DirectedHours = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("DirectedHours"), False, "")
                            OrderData.work_description = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("work_description"), False, "")
                            OrderData.progress = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("progress"), False, "")
                            OrderData.writeable_h_order = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_h_order"), False)
                            OrderData.writeable_b_order = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_b_order"), False)
                            OrderData.writeable_h_aufmass = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_h_aufmass"), False)
                            OrderData.writeable_b_aufmass = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_b_aufmass"), False)
                            OrderData.writeable_h_freigabe = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_h_freigabe"), False)
                            OrderData.writeable_b_freigabe = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_b_freigabe"), False)
                            OrderData.writeable_h_abbau = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_h_abbau"), False)
                            OrderData.writeable_b_abbau = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_b_abbau"), False)
                            OrderData.order_created_by = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("order_created_by"), False, "")
                            OrderData.order_created_date = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("order_created_date"), Nothing)
                            OrderData.order_date_abbau = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("order_date_abbau"), Nothing)

                            query1 = "SELECT * FROM `" & clsDBconnectLocal.db_table_prost_customers & "` WHERE "
                            query1 = query1 & "`customer_id` LIKE ?customer_id? "
                            query1 = query1 & ";"
                            fmtbx1bcc2p.cmd.CommandText = query1
                            fmtbx1bcc2p.cmd.Parameters.Clear()
                            fmtbx1bcc2p.cmd.Parameters.AddWithValue("?customer_id?", OrderData.customer_id)
                            'Debug.Print(So_HelperDB.ParameterQuery(fmtbx1bcc2p))
                            Dim display_name As String = ""
                            Using reader_fmtbx1bcc2p As MySqlDataReader = fmtbx1bcc2p.cmd.ExecuteReader
                                While reader_fmtbx1bcc2p.Read()
                                    Application.DoEvents()
                                    display_name = So_Helper_Convert.ConvertToString(reader_fmtbx1bcc2p("display_name"), False, "")
                                End While
                            End Using

                            lvi.Text = OrderData.order_id.ToString
                            lvi.Tag = OrderData
                            lvsi1.Text = OrderData.best_nr & "." & OrderData.best_nr1
                            lvi.SubItems.Add(lvsi1)
                            lvsi2.Text = display_name
                            lvi.SubItems.Add(lvsi2)
                            lvsi3.Text = OrderData.construction
                            lvi.SubItems.Add(lvsi3)
                            lvsi4.Text = OrderData.site
                            lvi.SubItems.Add(lvsi4)
                            lvsi5.Text = OrderData.place
                            lvi.SubItems.Add(lvsi5)
                            lvsi6.Text = Format(OrderData.order_date, "yyyy-MM-dd")
                            lvi.SubItems.Add(lvsi6)
                            Lv_ListFoundOrders.Items.Add(lvi)
                        End While
                    End Using
                    Lv_ListFoundOrders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                    Lv_ListFoundOrders.EndUpdate()
                End If
            End Using
        End Using
        If Lv_ListFoundOrders.Items.Count > 0 Then
            Cb_Last30Days.Checked = True
        End If
        Call EnableControls(True)
        CircularProgress1.IsRunning = False
        CircularProgress1.Visible = False

    End Sub
    Private Sub Cb_Last30Orders_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Last30Orders.CheckedChanged
        If Not Cb_Last30Orders.Checked Then
            Exit Sub
        End If
        Dim query As String = ""
        Dim query1 As String = ""

        Tb_SearchFor.Text = ""
        Call EnableControls(False)
        Btn_OpenOrder.Enabled = False
        'Btn_OpenOrder.Text = "Bearbeiten"
        CircularProgress1.IsRunning = True
        CircularProgress1.Visible = True
        Using uctlsobso1p As New clsDBconnect
            Using fmtbx1bcc2p As New clsDBconnect
                If uctlsobso1p.connect(clsDBconnectLocal.SelectDatabase.Prostruktura) AndAlso fmtbx1bcc2p.connect(clsDBconnectLocal.SelectDatabase.Prostruktura) Then
                    RecentListLengthVal = CInt(So_Helper.RegistryReadValue(clsMain.RegistryHiveValue, clsMain.RegistryPath & "\Defaults", "RecentListLength", So_Helper.VarTypeEnu.Type_Integer))
                    If RecentListLengthVal < 5 Then
                        RecentListLengthVal = 30
                    End If
                    query = "SELECT * FROM `" & clsDBconnectLocal.db_table_prost_order_main & "` "
                    query = query & "ORDER BY `order_id` DESC "
                    query = query & "LIMIT " & RecentListLengthVal.ToString
                    query = query & ";"
                    uctlsobso1p.cmd.CommandText = query
                    uctlsobso1p.cmd.Parameters.Clear()
                    uctlsobso1p.cmd.Parameters.AddWithValue("?such?", DateAdd(DateInterval.Day, RecentListLengthVal * -1, Now).ToString("yyyy-MM-dd"))
                    Debug.Print(So_Helper.ParameterQuery(uctlsobso1p))
                    'Lv_ListFoundOrders.Sorting = SortOrder.Descending

                    Lv_ListFoundOrders.BeginUpdate()
                    Lv_ListFoundOrders.Items.Clear()
                    Using reader_uctlsobso1p As MySqlDataReader = uctlsobso1p.cmd.ExecuteReader
                        While reader_uctlsobso1p.Read()
                            Application.DoEvents()
                            Dim OrderData As New OrderData
                            Dim lvi As New ListViewItem
                            Dim lvsi1 As New ListViewItem.ListViewSubItem
                            Dim lvsi2 As New ListViewItem.ListViewSubItem
                            Dim lvsi3 As New ListViewItem.ListViewSubItem
                            Dim lvsi4 As New ListViewItem.ListViewSubItem
                            Dim lvsi5 As New ListViewItem.ListViewSubItem
                            Dim lvsi6 As New ListViewItem.ListViewSubItem

                            OrderData.id = So_Helper_Convert.ConvertToInteger(reader_uctlsobso1p("id"), 0)
                            OrderData.office_id = So_Helper_Convert.ConvertToInteger(reader_uctlsobso1p("office_id"), 0)
                            OrderData.office = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("office"), False, "")
                            OrderData.customer_id = So_Helper_Convert.ConvertToInteger(reader_uctlsobso1p("customer_id"), 0)
                            OrderData.order_id = So_Helper_Convert.ConvertToInteger(reader_uctlsobso1p("order_id"), 0)
                            OrderData.best_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("best_nr"), False, "")
                            OrderData.best_nr1 = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("best_nr1"), False, "")
                            OrderData.order_date = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("order_date"), Nothing)
                            OrderData.customer_name = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("customer_name"), False, "")
                            OrderData.purchaser = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("purchaser"), False, "")
                            OrderData.gerbestnr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("gerbestnr"), False, "")
                            OrderData.site = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("site"), False, "")
                            OrderData.construction = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("construction"), False, "")
                            OrderData.kontrakt_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("kontrakt_nr"), False, "")
                            OrderData.ger_order_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("ger_order_nr"), False, "")
                            OrderData.subproject = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("subproject"), False, "")
                            OrderData.cost_unit = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("cost_unit"), False, "")
                            OrderData.user = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("user"), False, "")
                            OrderData.cost_centre = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("cost_centre"), False, "")
                            OrderData.order_pos_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("order_pos_nr"), False, "")
                            OrderData.project_nr = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("project_nr"), False, "")
                            OrderData.build_height = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("build_height"), False, "")
                            OrderData.length = So_Helper_Convert.ConvertToDouble(reader_uctlsobso1p("length"), 0)
                            OrderData.width = So_Helper_Convert.ConvertToDouble(reader_uctlsobso1p("width"), 0)
                            OrderData.height = So_Helper_Convert.ConvertToDouble(reader_uctlsobso1p("height"), 0)
                            OrderData.place = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("place"), False, "")
                            OrderData.delivery_date = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("delivery_date"), Nothing)
                            OrderData.dismounting_date = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("dismounting_date"), Nothing)
                            OrderData.ger_approval = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("ger_approval"), False, "")
                            OrderData.DirectedHours = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("DirectedHours"), False, "")
                            OrderData.work_description = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("work_description"), False, "")
                            OrderData.progress = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("progress"), False, "")
                            OrderData.writeable_h_order = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_h_order"), False)
                            OrderData.writeable_b_order = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_b_order"), False)
                            OrderData.writeable_h_aufmass = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_h_aufmass"), False)
                            OrderData.writeable_b_aufmass = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_b_aufmass"), False)
                            OrderData.writeable_h_freigabe = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_h_freigabe"), False)
                            OrderData.writeable_b_freigabe = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_b_freigabe"), False)
                            OrderData.writeable_h_abbau = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_h_abbau"), False)
                            OrderData.writeable_b_abbau = So_Helper_Convert.ConvertToBoolean(reader_uctlsobso1p("writeable_b_abbau"), False)
                            OrderData.order_created_by = So_Helper_Convert.ConvertToString(reader_uctlsobso1p("order_created_by"), False, "")
                            OrderData.order_created_date = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("order_created_date"), Nothing)
                            OrderData.order_date_abbau = So_Helper_Convert.ConvertToDate(reader_uctlsobso1p("order_date_abbau"), Nothing)

                            query1 = "SELECT * FROM `" & clsDBconnectLocal.db_table_prost_customers & "` WHERE "
                            query1 = query1 & "`customer_id` LIKE ?customer_id? "
                            query1 = query1 & ";"
                            fmtbx1bcc2p.cmd.CommandText = query1
                            fmtbx1bcc2p.cmd.Parameters.Clear()
                            fmtbx1bcc2p.cmd.Parameters.AddWithValue("?customer_id?", OrderData.customer_id)
                            'Debug.Print(So_HelperDB.ParameterQuery(fmtbx1bcc2p))
                            Dim display_name As String = ""
                            Using reader_fmtbx1bcc2p As MySqlDataReader = fmtbx1bcc2p.cmd.ExecuteReader
                                While reader_fmtbx1bcc2p.Read()
                                    Application.DoEvents()
                                    display_name = So_Helper_Convert.ConvertToString(reader_fmtbx1bcc2p("display_name"), False, "")
                                End While
                            End Using

                            lvi.Text = OrderData.order_id.ToString
                            lvi.Tag = OrderData
                            lvsi1.Text = OrderData.best_nr & "." & OrderData.best_nr1
                            lvi.SubItems.Add(lvsi1)
                            lvsi2.Text = display_name
                            lvi.SubItems.Add(lvsi2)
                            lvsi3.Text = OrderData.construction
                            lvi.SubItems.Add(lvsi3)
                            lvsi4.Text = OrderData.site
                            lvi.SubItems.Add(lvsi4)
                            lvsi5.Text = OrderData.place
                            lvi.SubItems.Add(lvsi5)
                            lvsi6.Text = Format(OrderData.order_date, "yyyy-MM-dd")
                            lvi.SubItems.Add(lvsi6)
                            Lv_ListFoundOrders.Items.Add(lvi)
                        End While
                    End Using
                    Lv_ListFoundOrders.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent)
                    Lv_ListFoundOrders.EndUpdate()
                End If
            End Using
        End Using
        If Lv_ListFoundOrders.Items.Count > 0 Then
            Cb_Last30Orders.Checked = True
        End If
        Call EnableControls(True)
        CircularProgress1.IsRunning = False
        CircularProgress1.Visible = False

    End Sub

End Class
