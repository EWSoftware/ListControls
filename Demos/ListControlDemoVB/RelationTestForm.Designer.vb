<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RelationTestForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                dbConn?.Dispose()
                daAddresses?.Dispose()
                dsAddresses?.Dispose()
                daPhones?.Dispose()
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
        Me.components = New System.ComponentModel.Container
        Me.btnLoad = New System.Windows.Forms.Button
        Me.dnNav = New EWSoftware.ListControls.DataNavigator
        Me.btnSave = New System.Windows.Forms.Button
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
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
        Me.dlPhones = New EWSoftware.ListControls.DataList
        Me.txtFindName = New System.Windows.Forms.TextBox
        Me.clickableLabel1 = New EWSoftware.ListControls.ClickableLabel
        Me.rblContactType = New EWSoftware.ListControls.RadioButtonList
        Me.cblAddressTypes = New EWSoftware.ListControls.CheckBoxList
        Me.pnlData.SuspendLayout()
        CType(Me.cboState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.epErrors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.rblContactType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cblAddressTypes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.Location = New System.Drawing.Point(12, 387)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(104, 28)
        Me.btnLoad.TabIndex = 4
        Me.btnLoad.Text = "L&oad Data"
        Me.toolTip1.SetToolTip(Me.btnLoad, "Load data into the control")
        '
        'dnNav
        '
        Me.dnNav.Location = New System.Drawing.Point(12, 327)
        Me.dnNav.Name = "dnNav"
        Me.dnNav.Size = New System.Drawing.Size(282, 22)
        Me.dnNav.TabIndex = 1
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(122, 387)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(104, 28)
        Me.btnSave.TabIndex = 5
        Me.btnSave.Text = "&Save Data"
        Me.toolTip1.SetToolTip(Me.btnSave, "Save all changes")
        '
        'pnlData
        '
        Me.pnlData.Controls.Add(Me.rblContactType)
        Me.pnlData.Controls.Add(Me.cblAddressTypes)
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
        Me.pnlData.Size = New System.Drawing.Size(494, 281)
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
        Me.txtAddress.Size = New System.Drawing.Size(384, 22)
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
        Me.txtFName.Size = New System.Drawing.Size(132, 22)
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
        Me.label5.Location = New System.Drawing.Point(8, 108)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(78, 22)
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
        Me.label3.Location = New System.Drawing.Point(9, 44)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(77, 22)
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
        Me.lblAddRow.Location = New System.Drawing.Point(12, 352)
        Me.lblAddRow.Name = "lblAddRow"
        Me.lblAddRow.Size = New System.Drawing.Size(492, 24)
        Me.lblAddRow.TabIndex = 3
        Me.lblAddRow.Text = "Please click the Add button to add a new row"
        Me.lblAddRow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblAddRow.Visible = False
        '
        'epErrors
        '
        Me.epErrors.ContainerControl = Me
        '
        'dlPhones
        '
        Me.dlPhones.CaptionText = "Phone Numbers"
        Me.dlPhones.CaptionVisible = True
        Me.dlPhones.Location = New System.Drawing.Point(510, 12)
        Me.dlPhones.Name = "dlPhones"
        Me.dlPhones.Size = New System.Drawing.Size(306, 364)
        Me.dlPhones.TabIndex = 2
        '
        'txtFindName
        '
        Me.txtFindName.Location = New System.Drawing.Point(127, 12)
        Me.txtFindName.MaxLength = 30
        Me.txtFindName.Name = "txtFindName"
        Me.txtFindName.Size = New System.Drawing.Size(136, 22)
        Me.txtFindName.TabIndex = 7
        '
        'clickableLabel1
        '
        Me.clickableLabel1.Location = New System.Drawing.Point(6, 12)
        Me.clickableLabel1.Name = "clickableLabel1"
        Me.clickableLabel1.Size = New System.Drawing.Size(115, 23)
        Me.clickableLabel1.TabIndex = 6
        Me.clickableLabel1.Text = "&Find Last Name"
        Me.clickableLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'rblContactType
        '
        Me.rblContactType.ListPadding = New EWSoftware.ListControls.ListPadding(5, 40, 4, 8, 4, 5)
        Me.rblContactType.Location = New System.Drawing.Point(310, 151)
        Me.rblContactType.Name = "rblContactType"
        Me.rblContactType.Size = New System.Drawing.Size(178, 123)
        Me.rblContactType.TabIndex = 13
        Me.rblContactType.TitleBackColor = System.Drawing.Color.Gainsboro
        Me.rblContactType.TitleText = "Contact Type"
        '
        'cblAddressTypes
        '
        Me.cblAddressTypes.BindingMembers.AddRange(New String() {"Addresses.Domestic", "Addresses.International", "Addresses.Postal", "Addresses.Parcel", "Addresses.Home", "Addresses.Business"})
        Me.cblAddressTypes.BindingMembersBindingContext = Nothing
        Me.cblAddressTypes.Items.AddRange(New Object() {"Domestic", "International", "Postal", "Parcel", "Home", "Business"})
        Me.cblAddressTypes.LayoutMethod = EWSoftware.ListControls.LayoutMethod.DownThenAcross
        Me.cblAddressTypes.ListPadding = New EWSoftware.ListControls.ListPadding(8, 30, 4, 8, 20, 5)
        Me.cblAddressTypes.Location = New System.Drawing.Point(12, 151)
        Me.cblAddressTypes.Name = "cblAddressTypes"
        Me.cblAddressTypes.Size = New System.Drawing.Size(292, 123)
        Me.cblAddressTypes.TabIndex = 12
        Me.cblAddressTypes.TitleBackColor = System.Drawing.Color.Gainsboro
        Me.cblAddressTypes.TitleText = "Address Types"
        '
        'RelationTestForm
        '
        Me.ClientSize = New System.Drawing.Size(828, 427)
        Me.Controls.Add(Me.txtFindName)
        Me.Controls.Add(Me.clickableLabel1)
        Me.Controls.Add(Me.dlPhones)
        Me.Controls.Add(Me.lblAddRow)
        Me.Controls.Add(Me.pnlData)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.dnNav)
        Me.Controls.Add(Me.btnLoad)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RelationTestForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Relationship Test Form"
        Me.pnlData.ResumeLayout(False)
        Me.pnlData.PerformLayout()
        CType(Me.cboState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.epErrors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.rblContactType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cblAddressTypes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Private WithEvents btnLoad As System.Windows.Forms.Button
    Private WithEvents dnNav As EWSoftware.ListControls.DataNavigator
    Private WithEvents btnSave As System.Windows.Forms.Button
    Private WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Private WithEvents pnlData As System.Windows.Forms.Panel
    Private WithEvents txtZip As System.Windows.Forms.TextBox
    Private WithEvents txtCity As System.Windows.Forms.TextBox
    Private WithEvents txtAddress As System.Windows.Forms.TextBox
    Private WithEvents txtLName As System.Windows.Forms.TextBox
    Private WithEvents txtFName As System.Windows.Forms.TextBox
    Private WithEvents lblKey As System.Windows.Forms.Label
    Private WithEvents cboState As EWSoftware.ListControls.MultiColumnComboBox
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents lblAddRow As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents epErrors As System.Windows.Forms.ErrorProvider
    Private WithEvents dlPhones As EWSoftware.ListControls.DataList
    Private WithEvents txtFindName As System.Windows.Forms.TextBox
    Private WithEvents rblContactType As EWSoftware.ListControls.RadioButtonList
    Private WithEvents cblAddressTypes As EWSoftware.ListControls.CheckBoxList
    Private WithEvents clickableLabel1 As EWSoftware.ListControls.ClickableLabel

End Class
