<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Public Class SearchOrders
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Public components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Public Sub InitializeComponent()
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Test")
        Me.Col_Order_Id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Best_Nr = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Customer = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColLP = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Anlage = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Place = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Col_Order_Date = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel()
        Me.Bar1 = New DevComponents.DotNetBar.Bar()
        Me.Lbli_StatusMessage = New DevComponents.DotNetBar.LabelItem()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.Btn_RefreshList = New DevComponents.DotNetBar.ButtonX()
        Me.Cb_Last30Orders = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.LabelX72 = New DevComponents.DotNetBar.LabelX()
        Me.Cbb_EditOrderOrderId = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.Cb_Select = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.Ii_LimitOrderCount = New DevComponents.Editors.IntegerInput()
        Me.Cb_Last30Days = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.LabelX73 = New DevComponents.DotNetBar.LabelX()
        Me.Cb_Search = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.Lv_ListFoundOrders = New DevComponents.DotNetBar.Controls.ListViewEx()
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader13 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader14 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Cbb_EditOrderOffice = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.LabelX75 = New DevComponents.DotNetBar.LabelX()
        Me.Tb_SearchFor = New DevComponents.DotNetBar.Controls.TextBoxX()
        Me.LabelX76 = New DevComponents.DotNetBar.LabelX()
        Me.Btn_OpenOrder = New DevComponents.DotNetBar.ButtonX()
        Me.Cbb_EditOrderCustomer = New DevComponents.DotNetBar.Controls.ComboBoxEx()
        Me.GroupPanel1.SuspendLayout()
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Ii_LimitOrderCount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Col_Order_Id
        '
        Me.Col_Order_Id.Text = "Auftrags Nr."
        Me.Col_Order_Id.Width = 75
        '
        'Col_Best_Nr
        '
        Me.Col_Best_Nr.Text = "Best. Nr."
        Me.Col_Best_Nr.Width = 73
        '
        'Col_Customer
        '
        Me.Col_Customer.Text = "Kunde"
        Me.Col_Customer.Width = 116
        '
        'ColLP
        '
        Me.ColLP.Text = "LP"
        Me.ColLP.Width = 100
        '
        'Col_Anlage
        '
        Me.Col_Anlage.Text = "Anlage"
        Me.Col_Anlage.Width = 100
        '
        'Col_Place
        '
        Me.Col_Place.Text = "Ort"
        Me.Col_Place.Width = 100
        '
        'Col_Order_Date
        '
        Me.Col_Order_Date.Text = "Best. Datum"
        Me.Col_Order_Date.Width = 160
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Auftrags Nr."
        Me.ColumnHeader1.Width = 75
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Best. Nr."
        Me.ColumnHeader2.Width = 73
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Kunde"
        Me.ColumnHeader3.Width = 116
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "LP"
        Me.ColumnHeader4.Width = 100
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Anlage"
        Me.ColumnHeader5.Width = 100
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Ort"
        Me.ColumnHeader6.Width = 100
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Best. Datum"
        Me.ColumnHeader7.Width = 160
        '
        'GroupPanel1
        '
        Me.GroupPanel1.BackColor = System.Drawing.Color.Transparent
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.GroupPanel1.Controls.Add(Me.Bar1)
        Me.GroupPanel1.Controls.Add(Me.CircularProgress1)
        Me.GroupPanel1.Controls.Add(Me.Btn_RefreshList)
        Me.GroupPanel1.Controls.Add(Me.Cb_Last30Orders)
        Me.GroupPanel1.Controls.Add(Me.LabelX72)
        Me.GroupPanel1.Controls.Add(Me.Cbb_EditOrderOrderId)
        Me.GroupPanel1.Controls.Add(Me.Cb_Select)
        Me.GroupPanel1.Controls.Add(Me.Ii_LimitOrderCount)
        Me.GroupPanel1.Controls.Add(Me.Cb_Last30Days)
        Me.GroupPanel1.Controls.Add(Me.LabelX73)
        Me.GroupPanel1.Controls.Add(Me.Cb_Search)
        Me.GroupPanel1.Controls.Add(Me.Lv_ListFoundOrders)
        Me.GroupPanel1.Controls.Add(Me.Cbb_EditOrderOffice)
        Me.GroupPanel1.Controls.Add(Me.LabelX75)
        Me.GroupPanel1.Controls.Add(Me.Tb_SearchFor)
        Me.GroupPanel1.Controls.Add(Me.LabelX76)
        Me.GroupPanel1.Controls.Add(Me.Btn_OpenOrder)
        Me.GroupPanel1.Controls.Add(Me.Cbb_EditOrderCustomer)
        Me.GroupPanel1.DisabledBackColor = System.Drawing.Color.Empty
        Me.GroupPanel1.Location = New System.Drawing.Point(0, 15)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(374, 264)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderBottomWidth = 1
        Me.GroupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderLeftWidth = 1
        Me.GroupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderRightWidth = 1
        Me.GroupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderTopWidth = 1
        Me.GroupPanel1.Style.CornerDiameter = 4
        Me.GroupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel1.TabIndex = 3
        Me.GroupPanel1.Text = "Auftrag bearbeiten:"
        '
        'Bar1
        '
        Me.Bar1.AntiAlias = True
        Me.Bar1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Bar1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Bar1.IsMaximized = False
        Me.Bar1.Items.AddRange(New DevComponents.DotNetBar.BaseItem() {Me.Lbli_StatusMessage})
        Me.Bar1.Location = New System.Drawing.Point(0, 224)
        Me.Bar1.Name = "Bar1"
        Me.Bar1.Size = New System.Drawing.Size(368, 19)
        Me.Bar1.Stretch = True
        Me.Bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Bar1.TabIndex = 14
        Me.Bar1.TabStop = False
        Me.Bar1.Text = "Bar1"
        '
        'Lbli_StatusMessage
        '
        Me.Lbli_StatusMessage.Name = "Lbli_StatusMessage"
        Me.Lbli_StatusMessage.Stretch = True
        Me.Lbli_StatusMessage.TextAlignment = System.Drawing.StringAlignment.Center
        '
        'CircularProgress1
        '
        Me.CircularProgress1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CircularProgress1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.CircularProgress1.Location = New System.Drawing.Point(3, 99)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Donut
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.Navy
        Me.CircularProgress1.Size = New System.Drawing.Size(110, 22)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 4
        Me.CircularProgress1.Visible = False
        '
        'Btn_RefreshList
        '
        Me.Btn_RefreshList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.Btn_RefreshList.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.Btn_RefreshList.Image = Global.TimeShare_SearchOrders.My.Resources.Resources.refresh_blue_16
        Me.Btn_RefreshList.Location = New System.Drawing.Point(88, 79)
        Me.Btn_RefreshList.Name = "Btn_RefreshList"
        Me.Btn_RefreshList.Size = New System.Drawing.Size(25, 25)
        Me.Btn_RefreshList.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Btn_RefreshList.TabIndex = 13
        Me.Btn_RefreshList.Tooltip = "Liste aktualisieren"
        '
        'Cb_Last30Orders
        '
        Me.Cb_Last30Orders.BackColor = System.Drawing.Color.Gainsboro
        '
        '
        '
        Me.Cb_Last30Orders.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Cb_Last30Orders.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.Cb_Last30Orders.Location = New System.Drawing.Point(3, 53)
        Me.Cb_Last30Orders.Name = "Cb_Last30Orders"
        Me.Cb_Last30Orders.Size = New System.Drawing.Size(110, 20)
        Me.Cb_Last30Orders.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Cb_Last30Orders.TabIndex = 12
        Me.Cb_Last30Orders.Text = "Letzte 30 Aufträge:"
        '
        'LabelX72
        '
        Me.LabelX72.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelX72.BackColor = System.Drawing.Color.Gainsboro
        '
        '
        '
        Me.LabelX72.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX72.Location = New System.Drawing.Point(3, 149)
        Me.LabelX72.Name = "LabelX72"
        Me.LabelX72.Size = New System.Drawing.Size(110, 20)
        Me.LabelX72.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.LabelX72.TabIndex = 4
        Me.LabelX72.Text = "Kunde:"
        Me.LabelX72.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'Cbb_EditOrderOrderId
        '
        Me.Cbb_EditOrderOrderId.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cbb_EditOrderOrderId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Cbb_EditOrderOrderId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Cbb_EditOrderOrderId.DisplayMember = "Text"
        Me.Cbb_EditOrderOrderId.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.Cbb_EditOrderOrderId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cbb_EditOrderOrderId.FormattingEnabled = True
        Me.Cbb_EditOrderOrderId.ItemHeight = 15
        Me.Cbb_EditOrderOrderId.Location = New System.Drawing.Point(115, 172)
        Me.Cbb_EditOrderOrderId.MaxDropDownItems = 12
        Me.Cbb_EditOrderOrderId.Name = "Cbb_EditOrderOrderId"
        Me.Cbb_EditOrderOrderId.Size = New System.Drawing.Size(253, 21)
        Me.Cbb_EditOrderOrderId.Sorted = True
        Me.Cbb_EditOrderOrderId.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Cbb_EditOrderOrderId.TabIndex = 7
        '
        'Cb_Select
        '
        Me.Cb_Select.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Cb_Select.BackColor = System.Drawing.Color.Gainsboro
        '
        '
        '
        Me.Cb_Select.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Cb_Select.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.Cb_Select.Location = New System.Drawing.Point(3, 126)
        Me.Cb_Select.Name = "Cb_Select"
        Me.Cb_Select.Size = New System.Drawing.Size(110, 20)
        Me.Cb_Select.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Cb_Select.TabIndex = 1
        Me.Cb_Select.Text = "Niederlassung:"
        '
        'Ii_LimitOrderCount
        '
        Me.Ii_LimitOrderCount.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        '
        '
        '
        Me.Ii_LimitOrderCount.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.Ii_LimitOrderCount.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Ii_LimitOrderCount.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.Ii_LimitOrderCount.Location = New System.Drawing.Point(152, 198)
        Me.Ii_LimitOrderCount.MaxValue = 120
        Me.Ii_LimitOrderCount.MinValue = 1
        Me.Ii_LimitOrderCount.Name = "Ii_LimitOrderCount"
        Me.Ii_LimitOrderCount.ShowUpDown = True
        Me.Ii_LimitOrderCount.Size = New System.Drawing.Size(39, 20)
        Me.Ii_LimitOrderCount.TabIndex = 9
        Me.Ii_LimitOrderCount.Value = 12
        '
        'Cb_Last30Days
        '
        Me.Cb_Last30Days.BackColor = System.Drawing.Color.Gainsboro
        '
        '
        '
        Me.Cb_Last30Days.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Cb_Last30Days.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.Cb_Last30Days.Location = New System.Drawing.Point(3, 28)
        Me.Cb_Last30Days.Name = "Cb_Last30Days"
        Me.Cb_Last30Days.Size = New System.Drawing.Size(110, 20)
        Me.Cb_Last30Days.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Cb_Last30Days.TabIndex = 0
        Me.Cb_Last30Days.Text = "Letzte 30 Tage:"
        '
        'LabelX73
        '
        Me.LabelX73.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelX73.BackColor = System.Drawing.Color.Gainsboro
        '
        '
        '
        Me.LabelX73.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX73.Location = New System.Drawing.Point(3, 172)
        Me.LabelX73.Name = "LabelX73"
        Me.LabelX73.Size = New System.Drawing.Size(110, 20)
        Me.LabelX73.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.LabelX73.TabIndex = 6
        Me.LabelX73.Text = "Auftragsnummer:"
        Me.LabelX73.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'Cb_Search
        '
        Me.Cb_Search.BackColor = System.Drawing.Color.Gainsboro
        '
        '
        '
        Me.Cb_Search.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Cb_Search.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.Cb_Search.Checked = True
        Me.Cb_Search.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Cb_Search.CheckValue = "Y"
        Me.Cb_Search.Location = New System.Drawing.Point(3, 3)
        Me.Cb_Search.Name = "Cb_Search"
        Me.Cb_Search.Size = New System.Drawing.Size(110, 20)
        Me.Cb_Search.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Cb_Search.TabIndex = 0
        Me.Cb_Search.Text = "Freitext-Suche:"
        '
        'Lv_ListFoundOrders
        '
        Me.Lv_ListFoundOrders.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        '
        '
        '
        Me.Lv_ListFoundOrders.Border.Class = "ListViewBorder"
        Me.Lv_ListFoundOrders.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Lv_ListFoundOrders.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10, Me.ColumnHeader11, Me.ColumnHeader12, Me.ColumnHeader13, Me.ColumnHeader14})
        Me.Lv_ListFoundOrders.DisabledBackColor = System.Drawing.Color.Empty
        Me.Lv_ListFoundOrders.FullRowSelect = True
        Me.Lv_ListFoundOrders.HideSelection = False
        Me.Lv_ListFoundOrders.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1})
        Me.Lv_ListFoundOrders.Location = New System.Drawing.Point(115, 28)
        Me.Lv_ListFoundOrders.Name = "Lv_ListFoundOrders"
        Me.Lv_ListFoundOrders.Size = New System.Drawing.Size(253, 94)
        Me.Lv_ListFoundOrders.TabIndex = 5
        Me.Lv_ListFoundOrders.UseCompatibleStateImageBehavior = False
        Me.Lv_ListFoundOrders.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Auftrags Nr."
        Me.ColumnHeader8.Width = 75
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Best. Nr."
        Me.ColumnHeader9.Width = 73
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Kunde"
        Me.ColumnHeader10.Width = 116
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "LP"
        Me.ColumnHeader11.Width = 100
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "Anlage"
        Me.ColumnHeader12.Width = 100
        '
        'ColumnHeader13
        '
        Me.ColumnHeader13.Text = "Ort"
        Me.ColumnHeader13.Width = 100
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = "Best. Datum"
        Me.ColumnHeader14.Width = 160
        '
        'Cbb_EditOrderOffice
        '
        Me.Cbb_EditOrderOffice.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cbb_EditOrderOffice.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Cbb_EditOrderOffice.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Cbb_EditOrderOffice.DisplayMember = "Text"
        Me.Cbb_EditOrderOffice.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.Cbb_EditOrderOffice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cbb_EditOrderOffice.FormattingEnabled = True
        Me.Cbb_EditOrderOffice.ItemHeight = 15
        Me.Cbb_EditOrderOffice.Location = New System.Drawing.Point(115, 126)
        Me.Cbb_EditOrderOffice.Name = "Cbb_EditOrderOffice"
        Me.Cbb_EditOrderOffice.Size = New System.Drawing.Size(253, 21)
        Me.Cbb_EditOrderOffice.Sorted = True
        Me.Cbb_EditOrderOffice.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Cbb_EditOrderOffice.TabIndex = 3
        '
        'LabelX75
        '
        Me.LabelX75.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelX75.BackColor = System.Drawing.Color.Gainsboro
        '
        '
        '
        Me.LabelX75.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX75.Location = New System.Drawing.Point(3, 198)
        Me.LabelX75.Name = "LabelX75"
        Me.LabelX75.Size = New System.Drawing.Size(148, 20)
        Me.LabelX75.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.LabelX75.TabIndex = 8
        Me.LabelX75.Text = "Zeige nur Aufträge der letzten"
        Me.LabelX75.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'Tb_SearchFor
        '
        Me.Tb_SearchFor.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        '
        '
        '
        Me.Tb_SearchFor.Border.Class = "TextBoxBorder"
        Me.Tb_SearchFor.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.Tb_SearchFor.ButtonCustom.Image = Global.TimeShare_SearchOrders.My.Resources.Resources.cancel_16
        Me.Tb_SearchFor.ButtonCustom.Visible = True
        Me.Tb_SearchFor.ButtonCustom2.Image = Global.TimeShare_SearchOrders.My.Resources.Resources.search2_16
        Me.Tb_SearchFor.ButtonCustom2.Visible = True
        Me.Tb_SearchFor.Location = New System.Drawing.Point(115, 3)
        Me.Tb_SearchFor.Name = "Tb_SearchFor"
        Me.Tb_SearchFor.PreventEnterBeep = True
        Me.Tb_SearchFor.Size = New System.Drawing.Size(253, 22)
        Me.Tb_SearchFor.TabIndex = 2
        '
        'LabelX76
        '
        Me.LabelX76.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.LabelX76.BackColor = System.Drawing.Color.Gainsboro
        '
        '
        '
        Me.LabelX76.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX76.Location = New System.Drawing.Point(193, 198)
        Me.LabelX76.Name = "LabelX76"
        Me.LabelX76.Size = New System.Drawing.Size(41, 20)
        Me.LabelX76.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.LabelX76.TabIndex = 10
        Me.LabelX76.Text = "Monate."
        Me.LabelX76.TextAlignment = System.Drawing.StringAlignment.Far
        '
        'Btn_OpenOrder
        '
        Me.Btn_OpenOrder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.Btn_OpenOrder.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Btn_OpenOrder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground
        Me.Btn_OpenOrder.Image = Global.TimeShare_SearchOrders.My.Resources.Resources.edit_16
        Me.Btn_OpenOrder.Location = New System.Drawing.Point(240, 198)
        Me.Btn_OpenOrder.Name = "Btn_OpenOrder"
        Me.Btn_OpenOrder.Size = New System.Drawing.Size(128, 20)
        Me.Btn_OpenOrder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Btn_OpenOrder.TabIndex = 11
        Me.Btn_OpenOrder.Text = "Öffnen"
        '
        'Cbb_EditOrderCustomer
        '
        Me.Cbb_EditOrderCustomer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Cbb_EditOrderCustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.Cbb_EditOrderCustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.Cbb_EditOrderCustomer.DisplayMember = "Text"
        Me.Cbb_EditOrderCustomer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.Cbb_EditOrderCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Cbb_EditOrderCustomer.FormattingEnabled = True
        Me.Cbb_EditOrderCustomer.ItemHeight = 15
        Me.Cbb_EditOrderCustomer.Location = New System.Drawing.Point(115, 149)
        Me.Cbb_EditOrderCustomer.Name = "Cbb_EditOrderCustomer"
        Me.Cbb_EditOrderCustomer.Size = New System.Drawing.Size(253, 21)
        Me.Cbb_EditOrderCustomer.Sorted = True
        Me.Cbb_EditOrderCustomer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Cbb_EditOrderCustomer.TabIndex = 5
        '
        'SearchOrders
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupPanel1)
        Me.Name = "SearchOrders"
        Me.Size = New System.Drawing.Size(405, 279)
        Me.GroupPanel1.ResumeLayout(False)
        CType(Me.Bar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Ii_LimitOrderCount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents Col_Order_Id As ColumnHeader
    Public WithEvents Col_Best_Nr As ColumnHeader
    Public WithEvents Col_Customer As ColumnHeader
    Public WithEvents ColLP As ColumnHeader
    Public WithEvents Col_Anlage As ColumnHeader
    Public WithEvents Col_Place As ColumnHeader
    Public WithEvents Col_Order_Date As ColumnHeader
    Public WithEvents ColumnHeader1 As ColumnHeader
    Public WithEvents ColumnHeader2 As ColumnHeader
    Public WithEvents ColumnHeader3 As ColumnHeader
    Public WithEvents ColumnHeader4 As ColumnHeader
    Public WithEvents ColumnHeader5 As ColumnHeader
    Public WithEvents ColumnHeader6 As ColumnHeader
    Public WithEvents ColumnHeader7 As ColumnHeader
    Public WithEvents Btn_RefreshList As DevComponents.DotNetBar.ButtonX
    Public WithEvents LabelX72 As DevComponents.DotNetBar.LabelX
    Public WithEvents LabelX73 As DevComponents.DotNetBar.LabelX
    Public WithEvents ColumnHeader8 As ColumnHeader
    Public WithEvents ColumnHeader9 As ColumnHeader
    Public WithEvents ColumnHeader10 As ColumnHeader
    Public WithEvents ColumnHeader11 As ColumnHeader
    Public WithEvents ColumnHeader12 As ColumnHeader
    Public WithEvents ColumnHeader13 As ColumnHeader
    Public WithEvents ColumnHeader14 As ColumnHeader
    Public WithEvents LabelX75 As DevComponents.DotNetBar.LabelX
    Public WithEvents LabelX76 As DevComponents.DotNetBar.LabelX
    Public WithEvents Bar1 As DevComponents.DotNetBar.Bar
    Public WithEvents Lbli_StatusMessage As DevComponents.DotNetBar.LabelItem
    Public WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Public WithEvents Cb_Last30Orders As DevComponents.DotNetBar.Controls.CheckBoxX
    Public WithEvents Cb_Last30Days As DevComponents.DotNetBar.Controls.CheckBoxX
    Public WithEvents Cb_Select As DevComponents.DotNetBar.Controls.CheckBoxX
    Public WithEvents Ii_LimitOrderCount As DevComponents.Editors.IntegerInput
    Public WithEvents Lv_ListFoundOrders As DevComponents.DotNetBar.Controls.ListViewEx
    Public WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Public WithEvents Cb_Search As DevComponents.DotNetBar.Controls.CheckBoxX
    Public WithEvents Tb_SearchFor As DevComponents.DotNetBar.Controls.TextBoxX
    Public WithEvents Btn_OpenOrder As DevComponents.DotNetBar.ButtonX
    Public WithEvents Cbb_EditOrderOrderId As DevComponents.DotNetBar.Controls.ComboBoxEx
    Public WithEvents Cbb_EditOrderOffice As DevComponents.DotNetBar.Controls.ComboBoxEx
    Public WithEvents Cbb_EditOrderCustomer As DevComponents.DotNetBar.Controls.ComboBoxEx
End Class
