<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataNavigatorTestForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.btnLoad = New System.Windows.Forms.Button
        Me.dnNav = New EWSoftware.ListControls.DataNavigator
        Me.btnSave = New System.Windows.Forms.Button
        Me.pgProps = New System.Windows.Forms.PropertyGrid
        Me.btnAddDSRow = New System.Windows.Forms.Button
        Me.btnDelDSRow = New System.Windows.Forms.Button
        Me.btnModRow = New System.Windows.Forms.Button
        Me.udcRowNumber = New System.Windows.Forms.NumericUpDown
        Me.label6 = New System.Windows.Forms.Label
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnGetValue = New System.Windows.Forms.Button
        Me.cboColumns = New System.Windows.Forms.ComboBox
        Me.txtValue = New System.Windows.Forms.TextBox
        Me.txtRowNumber = New System.Windows.Forms.NumericUpDown
        Me.label7 = New System.Windows.Forms.Label
        Me.label8 = New System.Windows.Forms.Label
        Me.pnlData = New System.Windows.Forms.Panel
        Me.txtZip = New System.Windows.Forms.TextBox
        Me.cboState = New EWSoftware.ListControls.MultiColumnComboBox
        Me.txtCity = New System.Windows.Forms.TextBox
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.txtLName = New System.Windows.Forms.TextBox
        Me.txtFName = New System.Windows.Forms.TextBox
        Me.label1 = New System.Windows.Forms.Label
        Me.lblKey = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.label4 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.lblAddRow = New System.Windows.Forms.Label
        Me.epErrors = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.txtFindName = New System.Windows.Forms.TextBox
        Me.clickableLabel1 = New EWSoftware.ListControls.ClickableLabel
        CType(Me.udcRowNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRowNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlData.SuspendLayout()
        CType(Me.cboState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.epErrors, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnLoad.Location = New System.Drawing.Point(12, 375)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(104, 28)
        Me.btnLoad.TabIndex = 8
        Me.btnLoad.Text = "L&oad Data"
        Me.toolTip1.SetToolTip(Me.btnLoad, "Load data into the control")
        '
        'dnNav
        '
        Me.dnNav.Location = New System.Drawing.Point(12, 184)
        Me.dnNav.Name = "dnNav"
        Me.dnNav.Size = New System.Drawing.Size(282, 22)
        Me.dnNav.TabIndex = 1
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnSave.Location = New System.Drawing.Point(122, 375)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(104, 28)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "&Save Data"
        Me.toolTip1.SetToolTip(Me.btnSave, "Save all changes")
        '
        'pgProps
        '
        Me.pgProps.LineColor = System.Drawing.SystemColors.ScrollBar
        Me.pgProps.Location = New System.Drawing.Point(512, 12)
        Me.pgProps.Name = "pgProps"
        Me.pgProps.Size = New System.Drawing.Size(308, 327)
        Me.pgProps.TabIndex = 15
        '
        'btnAddDSRow
        '
        Me.btnAddDSRow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddDSRow.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnAddDSRow.Location = New System.Drawing.Point(284, 375)
        Me.btnAddDSRow.Name = "btnAddDSRow"
        Me.btnAddDSRow.Size = New System.Drawing.Size(104, 28)
        Me.btnAddDSRow.TabIndex = 10
        Me.btnAddDSRow.Text = "Add DS Row"
        Me.toolTip1.SetToolTip(Me.btnAddDSRow, "Add row directly to the data source")
        '
        'btnDelDSRow
        '
        Me.btnDelDSRow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelDSRow.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnDelDSRow.Location = New System.Drawing.Point(565, 375)
        Me.btnDelDSRow.Name = "btnDelDSRow"
        Me.btnDelDSRow.Size = New System.Drawing.Size(104, 28)
        Me.btnDelDSRow.TabIndex = 13
        Me.btnDelDSRow.Text = "Del DS Row"
        Me.toolTip1.SetToolTip(Me.btnDelDSRow, "Delete row directly from data source")
        '
        'btnModRow
        '
        Me.btnModRow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnModRow.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnModRow.Location = New System.Drawing.Point(675, 375)
        Me.btnModRow.Name = "btnModRow"
        Me.btnModRow.Size = New System.Drawing.Size(104, 28)
        Me.btnModRow.TabIndex = 14
        Me.btnModRow.Text = "Modify Row"
        Me.toolTip1.SetToolTip(Me.btnModRow, "Modify row directly in data source")
        '
        'udcRowNumber
        '
        Me.udcRowNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.udcRowNumber.Location = New System.Drawing.Point(495, 377)
        Me.udcRowNumber.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.udcRowNumber.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.udcRowNumber.Name = "udcRowNumber"
        Me.udcRowNumber.Size = New System.Drawing.Size(64, 22)
        Me.udcRowNumber.TabIndex = 12
        Me.udcRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.udcRowNumber.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'label6
        '
        Me.label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label6.Location = New System.Drawing.Point(390, 377)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(99, 23)
        Me.label6.TabIndex = 11
        Me.label6.Text = "Del/Mod Row"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnGetValue
        '
        Me.btnGetValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGetValue.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnGetValue.Location = New System.Drawing.Point(346, 341)
        Me.btnGetValue.Name = "btnGetValue"
        Me.btnGetValue.Size = New System.Drawing.Size(75, 28)
        Me.btnGetValue.TabIndex = 6
        Me.btnGetValue.Text = "&Get"
        Me.toolTip1.SetToolTip(Me.btnGetValue, "Get the specified column from the specified row")
        '
        'cboColumns
        '
        Me.cboColumns.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColumns.Items.AddRange(New Object() {"ID", "FirstName", "LastName", "Address", "City", "State", "Zip", "SumValue"})
        Me.cboColumns.Location = New System.Drawing.Point(100, 345)
        Me.cboColumns.Name = "cboColumns"
        Me.cboColumns.Size = New System.Drawing.Size(132, 24)
        Me.cboColumns.TabIndex = 3
        '
        'txtValue
        '
        Me.txtValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtValue.Location = New System.Drawing.Point(427, 345)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.ReadOnly = True
        Me.txtValue.Size = New System.Drawing.Size(352, 22)
        Me.txtValue.TabIndex = 7
        Me.txtValue.TabStop = False
        '
        'txtRowNumber
        '
        Me.txtRowNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRowNumber.Location = New System.Drawing.Point(284, 345)
        Me.txtRowNumber.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.txtRowNumber.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtRowNumber.Name = "txtRowNumber"
        Me.txtRowNumber.Size = New System.Drawing.Size(56, 22)
        Me.txtRowNumber.TabIndex = 5
        Me.txtRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRowNumber.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'label7
        '
        Me.label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label7.Location = New System.Drawing.Point(236, 345)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(48, 23)
        Me.label7.TabIndex = 4
        Me.label7.Text = "at row"
        Me.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label8
        '
        Me.label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label8.Location = New System.Drawing.Point(16, 344)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(80, 23)
        Me.label8.TabIndex = 2
        Me.label8.Text = "G&et column"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pnlData
        '
        Me.pnlData.Controls.Add(Me.txtZip)
        Me.pnlData.Controls.Add(Me.cboState)
        Me.pnlData.Controls.Add(Me.txtCity)
        Me.pnlData.Controls.Add(Me.txtAddress)
        Me.pnlData.Controls.Add(Me.txtLName)
        Me.pnlData.Controls.Add(Me.txtFName)
        Me.pnlData.Controls.Add(Me.label1)
        Me.pnlData.Controls.Add(Me.lblKey)
        Me.pnlData.Controls.Add(Me.label5)
        Me.pnlData.Controls.Add(Me.label4)
        Me.pnlData.Controls.Add(Me.label3)
        Me.pnlData.Controls.Add(Me.label2)
        Me.pnlData.Location = New System.Drawing.Point(12, 40)
        Me.pnlData.Name = "pnlData"
        Me.pnlData.Size = New System.Drawing.Size(494, 144)
        Me.pnlData.TabIndex = 0
        '
        'txtZip
        '
        Me.txtZip.Location = New System.Drawing.Point(296, 108)
        Me.txtZip.MaxLength = 10
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(77, 22)
        Me.txtZip.TabIndex = 11
        '
        'cboState
        '
        Me.cboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboState.Location = New System.Drawing.Point(228, 108)
        Me.cboState.MaxDropDownItems = 16
        Me.cboState.Name = "cboState"
        Me.cboState.Size = New System.Drawing.Size(57, 24)
        Me.cboState.TabIndex = 10
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(92, 108)
        Me.txtCity.MaxLength = 20
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(125, 22)
        Me.txtCity.TabIndex = 9
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(92, 76)
        Me.txtAddress.MaxLength = 50
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(389, 22)
        Me.txtAddress.TabIndex = 7
        '
        'txtLName
        '
        Me.txtLName.Location = New System.Drawing.Point(92, 44)
        Me.txtLName.MaxLength = 30
        Me.txtLName.Name = "txtLName"
        Me.txtLName.Size = New System.Drawing.Size(160, 22)
        Me.txtLName.TabIndex = 3
        '
        'txtFName
        '
        Me.txtFName.Location = New System.Drawing.Point(344, 44)
        Me.txtFName.MaxLength = 20
        Me.txtFName.Name = "txtFName"
        Me.txtFName.Size = New System.Drawing.Size(137, 22)
        Me.txtFName.TabIndex = 5
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(62, 12)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(24, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "ID"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblKey
        '
        Me.lblKey.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblKey.Location = New System.Drawing.Point(92, 12)
        Me.lblKey.Name = "lblKey"
        Me.lblKey.Size = New System.Drawing.Size(64, 23)
        Me.lblKey.TabIndex = 1
        Me.lblKey.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label5
        '
        Me.label5.BackColor = System.Drawing.Color.Transparent
        Me.label5.Location = New System.Drawing.Point(3, 108)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(83, 22)
        Me.label5.TabIndex = 8
        Me.label5.Text = "&City/St/Zip"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label4
        '
        Me.label4.BackColor = System.Drawing.Color.Transparent
        Me.label4.Location = New System.Drawing.Point(10, 76)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(76, 22)
        Me.label4.TabIndex = 6
        Me.label4.Text = "&Address"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label3
        '
        Me.label3.BackColor = System.Drawing.Color.Transparent
        Me.label3.Location = New System.Drawing.Point(4, 44)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(82, 22)
        Me.label3.TabIndex = 2
        Me.label3.Text = "&Last Name"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Location = New System.Drawing.Point(262, 44)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(76, 22)
        Me.label2.TabIndex = 4
        Me.label2.Text = "First Name"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblAddRow
        '
        Me.lblAddRow.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblAddRow.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblAddRow.Location = New System.Drawing.Point(12, 216)
        Me.lblAddRow.Name = "lblAddRow"
        Me.lblAddRow.Size = New System.Drawing.Size(494, 24)
        Me.lblAddRow.TabIndex = 16
        Me.lblAddRow.Text = "Please click the Add button to add a new row"
        Me.lblAddRow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblAddRow.Visible = False
        '
        'epErrors
        '
        Me.epErrors.ContainerControl = Me
        '
        'txtFindName
        '
        Me.txtFindName.Location = New System.Drawing.Point(127, 12)
        Me.txtFindName.MaxLength = 30
        Me.txtFindName.Name = "txtFindName"
        Me.txtFindName.Size = New System.Drawing.Size(136, 22)
        Me.txtFindName.TabIndex = 18
        '
        'clickableLabel1
        '
        Me.clickableLabel1.Location = New System.Drawing.Point(8, 12)
        Me.clickableLabel1.Name = "clickableLabel1"
        Me.clickableLabel1.Size = New System.Drawing.Size(113, 23)
        Me.clickableLabel1.TabIndex = 17
        Me.clickableLabel1.Text = "&Find Last Name"
        Me.clickableLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DataNavigatorTestForm
        '
        Me.ClientSize = New System.Drawing.Size(832, 415)
        Me.Controls.Add(Me.txtFindName)
        Me.Controls.Add(Me.clickableLabel1)
        Me.Controls.Add(Me.lblAddRow)
        Me.Controls.Add(Me.pnlData)
        Me.Controls.Add(Me.cboColumns)
        Me.Controls.Add(Me.btnGetValue)
        Me.Controls.Add(Me.txtValue)
        Me.Controls.Add(Me.txtRowNumber)
        Me.Controls.Add(Me.label7)
        Me.Controls.Add(Me.label8)
        Me.Controls.Add(Me.udcRowNumber)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.btnModRow)
        Me.Controls.Add(Me.btnDelDSRow)
        Me.Controls.Add(Me.btnAddDSRow)
        Me.Controls.Add(Me.pgProps)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.dnNav)
        Me.Controls.Add(Me.btnLoad)
        Me.KeyPreview = True
        Me.MinimumSize = New System.Drawing.Size(840, 336)
        Me.Name = "DataNavigatorTestForm"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Test Data Navigator Control"
        CType(Me.udcRowNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRowNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlData.ResumeLayout(False)
        Me.pnlData.PerformLayout()
        CType(Me.cboState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.epErrors, System.ComponentModel.ISupportInitialize).EndInit()
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
