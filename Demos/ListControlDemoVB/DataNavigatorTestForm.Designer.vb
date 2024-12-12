<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataNavigatorTestForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                dc?.Dispose()
                components?.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New Container()
        btnLoad = New Button()
        dnNav = New DataNavigator()
        btnSave = New Button()
        pgProps = New PropertyGrid()
        btnAddDSRow = New Button()
        btnDelDSRow = New Button()
        btnModRow = New Button()
        udcRowNumber = New NumericUpDown()
        label6 = New Label()
        toolTip1 = New ToolTip(components)
        btnGetValue = New Button()
        cboColumns = New ComboBox()
        txtValue = New TextBox()
        txtRowNumber = New NumericUpDown()
        label7 = New Label()
        label8 = New Label()
        pnlData = New Panel()
        txtZip = New TextBox()
        cboState = New MultiColumnComboBox()
        txtCity = New TextBox()
        txtAddress = New TextBox()
        txtLName = New TextBox()
        txtFName = New TextBox()
        label1 = New Label()
        lblKey = New Label()
        label5 = New Label()
        label4 = New Label()
        label3 = New Label()
        label2 = New Label()
        lblAddRow = New Label()
        epErrors = New ErrorProvider(components)
        txtFindName = New TextBox()
        clickableLabel1 = New ClickableLabel()
        CType(udcRowNumber, ISupportInitialize).BeginInit()
        CType(txtRowNumber, ISupportInitialize).BeginInit()
        pnlData.SuspendLayout()
        CType(cboState, ISupportInitialize).BeginInit()
        CType(epErrors, ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' btnLoad
        ' 
        btnLoad.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnLoad.Location = New Point(12, 681)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(104, 32)
        btnLoad.TabIndex = 8
        btnLoad.Text = "L&oad Data"
        toolTip1.SetToolTip(btnLoad, "Load data into the control")
        ' 
        ' dnNav
        ' 
        dnNav.Location = New Point(12, 195)
        dnNav.Name = "dnNav"
        dnNav.Size = New Size(547, 22)
        dnNav.TabIndex = 1
        ' 
        ' btnSave
        ' 
        btnSave.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnSave.Location = New Point(122, 681)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(104, 32)
        btnSave.TabIndex = 9
        btnSave.Text = "&Save Data"
        toolTip1.SetToolTip(btnSave, "Save all changes")
        ' 
        ' pgProps
        ' 
        pgProps.LineColor = SystemColors.ScrollBar
        pgProps.Location = New Point(565, 12)
        pgProps.Name = "pgProps"
        pgProps.Size = New Size(428, 628)
        pgProps.TabIndex = 15
        ' 
        ' btnAddDSRow
        ' 
        btnAddDSRow.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnAddDSRow.Location = New Point(284, 681)
        btnAddDSRow.Name = "btnAddDSRow"
        btnAddDSRow.Size = New Size(104, 32)
        btnAddDSRow.TabIndex = 10
        btnAddDSRow.Text = "Add Row"
        toolTip1.SetToolTip(btnAddDSRow, "Add row directly to the data source")
        ' 
        ' btnDelDSRow
        ' 
        btnDelDSRow.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnDelDSRow.Location = New Point(595, 681)
        btnDelDSRow.Name = "btnDelDSRow"
        btnDelDSRow.Size = New Size(104, 32)
        btnDelDSRow.TabIndex = 13
        btnDelDSRow.Text = "Del DS Row"
        toolTip1.SetToolTip(btnDelDSRow, "Delete row directly from data source")
        ' 
        ' btnModRow
        ' 
        btnModRow.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnModRow.Location = New Point(705, 681)
        btnModRow.Name = "btnModRow"
        btnModRow.Size = New Size(104, 32)
        btnModRow.TabIndex = 14
        btnModRow.Text = "Modify Row"
        toolTip1.SetToolTip(btnModRow, "Modify row directly in data source")
        ' 
        ' udcRowNumber
        ' 
        udcRowNumber.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        udcRowNumber.Location = New Point(525, 683)
        udcRowNumber.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        udcRowNumber.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        udcRowNumber.Name = "udcRowNumber"
        udcRowNumber.Size = New Size(64, 27)
        udcRowNumber.TabIndex = 12
        udcRowNumber.TextAlign = HorizontalAlignment.Right
        udcRowNumber.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' label6
        ' 
        label6.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        label6.Location = New Point(394, 686)
        label6.Name = "label6"
        label6.Size = New Size(125, 23)
        label6.TabIndex = 11
        label6.Text = "Del/Mod Row"
        label6.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' btnGetValue
        ' 
        btnGetValue.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnGetValue.Location = New Point(377, 643)
        btnGetValue.Name = "btnGetValue"
        btnGetValue.Size = New Size(75, 32)
        btnGetValue.TabIndex = 6
        btnGetValue.Text = "&Get"
        toolTip1.SetToolTip(btnGetValue, "Get the specified column from the specified row")
        ' 
        ' cboColumns
        ' 
        cboColumns.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        cboColumns.DropDownStyle = ComboBoxStyle.DropDownList
        cboColumns.Items.AddRange(New Object() {"ID", "FirstName", "LastName", "StreetAddress", "City", "State", "Zip", "SumValue"})
        cboColumns.Location = New Point(122, 647)
        cboColumns.Name = "cboColumns"
        cboColumns.Size = New Size(132, 28)
        cboColumns.TabIndex = 3
        ' 
        ' txtValue
        ' 
        txtValue.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtValue.Location = New Point(458, 646)
        txtValue.Name = "txtValue"
        txtValue.ReadOnly = True
        txtValue.Size = New Size(536, 27)
        txtValue.TabIndex = 7
        txtValue.TabStop = False
        ' 
        ' txtRowNumber
        ' 
        txtRowNumber.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txtRowNumber.Location = New Point(315, 647)
        txtRowNumber.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        txtRowNumber.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        txtRowNumber.Name = "txtRowNumber"
        txtRowNumber.Size = New Size(56, 27)
        txtRowNumber.TabIndex = 5
        txtRowNumber.TextAlign = HorizontalAlignment.Right
        txtRowNumber.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' label7
        ' 
        label7.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        label7.Location = New Point(261, 649)
        label7.Name = "label7"
        label7.Size = New Size(48, 23)
        label7.TabIndex = 4
        label7.Text = "at row"
        label7.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' label8
        ' 
        label8.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        label8.Location = New Point(18, 649)
        label8.Name = "label8"
        label8.Size = New Size(98, 23)
        label8.TabIndex = 2
        label8.Text = "G&et column"
        label8.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' pnlData
        ' 
        pnlData.Controls.Add(txtZip)
        pnlData.Controls.Add(cboState)
        pnlData.Controls.Add(txtCity)
        pnlData.Controls.Add(txtAddress)
        pnlData.Controls.Add(txtLName)
        pnlData.Controls.Add(txtFName)
        pnlData.Controls.Add(label1)
        pnlData.Controls.Add(lblKey)
        pnlData.Controls.Add(label5)
        pnlData.Controls.Add(label4)
        pnlData.Controls.Add(label3)
        pnlData.Controls.Add(label2)
        pnlData.Location = New Point(12, 45)
        pnlData.Name = "pnlData"
        pnlData.Size = New Size(547, 144)
        pnlData.TabIndex = 0
        ' 
        ' txtZip
        ' 
        txtZip.Location = New Point(347, 110)
        txtZip.MaxLength = 10
        txtZip.Name = "txtZip"
        txtZip.Size = New Size(77, 27)
        txtZip.TabIndex = 11
        ' 
        ' cboState
        ' 
        cboState.DropDownStyle = ComboBoxStyle.DropDownList
        cboState.Location = New Point(275, 110)
        cboState.MaxDropDownItems = 16
        cboState.Name = "cboState"
        cboState.Size = New Size(66, 29)
        cboState.TabIndex = 10
        ' 
        ' txtCity
        ' 
        txtCity.Location = New Point(109, 110)
        txtCity.MaxLength = 20
        txtCity.Name = "txtCity"
        txtCity.Size = New Size(160, 27)
        txtCity.TabIndex = 9
        ' 
        ' txtAddress
        ' 
        txtAddress.Location = New Point(109, 77)
        txtAddress.MaxLength = 50
        txtAddress.Name = "txtAddress"
        txtAddress.Size = New Size(407, 27)
        txtAddress.TabIndex = 7
        ' 
        ' txtLName
        ' 
        txtLName.Location = New Point(109, 44)
        txtLName.MaxLength = 30
        txtLName.Name = "txtLName"
        txtLName.Size = New Size(160, 27)
        txtLName.TabIndex = 3
        ' 
        ' txtFName
        ' 
        txtFName.Location = New Point(379, 44)
        txtFName.MaxLength = 20
        txtFName.Name = "txtFName"
        txtFName.Size = New Size(137, 27)
        txtFName.TabIndex = 5
        ' 
        ' label1
        ' 
        label1.Location = New Point(71, 12)
        label1.Name = "label1"
        label1.Size = New Size(32, 23)
        label1.TabIndex = 0
        label1.Text = "ID"
        label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblKey
        ' 
        lblKey.BorderStyle = BorderStyle.Fixed3D
        lblKey.Location = New Point(109, 12)
        lblKey.Name = "lblKey"
        lblKey.Size = New Size(64, 23)
        lblKey.TabIndex = 1
        lblKey.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' label5
        ' 
        label5.BackColor = Color.Transparent
        label5.Location = New Point(6, 112)
        label5.Name = "label5"
        label5.Size = New Size(97, 22)
        label5.TabIndex = 8
        label5.Text = "&City/St/Zip"
        label5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' label4
        ' 
        label4.BackColor = Color.Transparent
        label4.Location = New Point(27, 79)
        label4.Name = "label4"
        label4.Size = New Size(76, 22)
        label4.TabIndex = 6
        label4.Text = "&Address"
        label4.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' label3
        ' 
        label3.BackColor = Color.Transparent
        label3.Location = New Point(8, 46)
        label3.Name = "label3"
        label3.Size = New Size(95, 22)
        label3.TabIndex = 2
        label3.Text = "&Last Name"
        label3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' label2
        ' 
        label2.BackColor = Color.Transparent
        label2.Location = New Point(280, 46)
        label2.Name = "label2"
        label2.Size = New Size(93, 22)
        label2.TabIndex = 4
        label2.Text = "First Name"
        label2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' lblAddRow
        ' 
        lblAddRow.BackColor = SystemColors.ActiveCaption
        lblAddRow.ForeColor = SystemColors.ActiveCaptionText
        lblAddRow.Location = New Point(12, 220)
        lblAddRow.Name = "lblAddRow"
        lblAddRow.Size = New Size(547, 24)
        lblAddRow.TabIndex = 16
        lblAddRow.Text = "Please click the Add button to add a new row"
        lblAddRow.TextAlign = ContentAlignment.MiddleCenter
        lblAddRow.Visible = False
        ' 
        ' epErrors
        ' 
        epErrors.ContainerControl = Me
        ' 
        ' txtFindName
        ' 
        txtFindName.Location = New Point(154, 12)
        txtFindName.MaxLength = 30
        txtFindName.Name = "txtFindName"
        txtFindName.Size = New Size(160, 27)
        txtFindName.TabIndex = 18
        ' 
        ' clickableLabel1
        ' 
        clickableLabel1.Location = New Point(12, 14)
        clickableLabel1.Name = "clickableLabel1"
        clickableLabel1.Size = New Size(136, 23)
        clickableLabel1.TabIndex = 17
        clickableLabel1.Text = "&Find Last Name"
        clickableLabel1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' DataNavigatorTestForm
        ' 
        Me.ClientSize = New Size(1006, 721)
        Me.Controls.Add(txtFindName)
        Me.Controls.Add(clickableLabel1)
        Me.Controls.Add(lblAddRow)
        Me.Controls.Add(pnlData)
        Me.Controls.Add(cboColumns)
        Me.Controls.Add(btnGetValue)
        Me.Controls.Add(txtValue)
        Me.Controls.Add(txtRowNumber)
        Me.Controls.Add(label7)
        Me.Controls.Add(label8)
        Me.Controls.Add(udcRowNumber)
        Me.Controls.Add(label6)
        Me.Controls.Add(btnModRow)
        Me.Controls.Add(btnDelDSRow)
        Me.Controls.Add(btnAddDSRow)
        Me.Controls.Add(pgProps)
        Me.Controls.Add(btnSave)
        Me.Controls.Add(dnNav)
        Me.Controls.Add(btnLoad)
        Me.KeyPreview = True
        Me.MinimumSize = New Size(850, 375)
        Me.Name = "DataNavigatorTestForm"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = SizeGripStyle.Show
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Test Data Navigator Control"
        CType(udcRowNumber, ISupportInitialize).EndInit()
        CType(txtRowNumber, ISupportInitialize).EndInit()
        pnlData.ResumeLayout(False)
        pnlData.PerformLayout()
        CType(cboState, ISupportInitialize).EndInit()
        CType(epErrors, ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents dnNav As EWSoftware.ListControls.DataNavigator
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents pgProps As System.Windows.Forms.PropertyGrid
    Friend WithEvents btnAddDSRow As System.Windows.Forms.Button
    Friend WithEvents btnDelDSRow As System.Windows.Forms.Button
    Friend WithEvents btnModRow As System.Windows.Forms.Button
    Friend WithEvents udcRowNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cboColumns As System.Windows.Forms.ComboBox
    Friend WithEvents btnGetValue As System.Windows.Forms.Button
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents txtRowNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents pnlData As System.Windows.Forms.Panel
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtLName As System.Windows.Forms.TextBox
    Friend WithEvents txtFName As System.Windows.Forms.TextBox
    Friend WithEvents lblKey As System.Windows.Forms.Label
    Friend WithEvents cboState As EWSoftware.ListControls.MultiColumnComboBox
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents lblAddRow As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents epErrors As System.Windows.Forms.ErrorProvider
    Friend WithEvents txtFindName As System.Windows.Forms.TextBox
    Friend WithEvents clickableLabel1 As EWSoftware.ListControls.ClickableLabel

End Class
