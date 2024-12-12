<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RelationTestForm
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
        toolTip1 = New ToolTip(components)
        pnlData = New Panel()
        rblContactType = New RadioButtonList()
        cblAddressTypes = New CheckBoxList()
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
        dlPhones = New DataList()
        txtFindName = New TextBox()
        clickableLabel1 = New ClickableLabel()
        pnlData.SuspendLayout()
        CType(rblContactType, ISupportInitialize).BeginInit()
        CType(cblAddressTypes, ISupportInitialize).BeginInit()
        CType(cboState, ISupportInitialize).BeginInit()
        CType(epErrors, ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' btnLoad
        ' 
        btnLoad.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnLoad.Location = New Point(12, 388)
        btnLoad.Name = "btnLoad"
        btnLoad.Size = New Size(104, 32)
        btnLoad.TabIndex = 4
        btnLoad.Text = "L&oad Data"
        toolTip1.SetToolTip(btnLoad, "Load data into the control")
        ' 
        ' dnNav
        ' 
        dnNav.Location = New Point(12, 336)
        dnNav.Name = "dnNav"
        dnNav.Size = New Size(600, 22)
        dnNav.TabIndex = 1
        ' 
        ' btnSave
        ' 
        btnSave.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnSave.Location = New Point(122, 388)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(104, 32)
        btnSave.TabIndex = 5
        btnSave.Text = "&Save Data"
        toolTip1.SetToolTip(btnSave, "Save all changes")
        ' 
        ' pnlData
        ' 
        pnlData.Controls.Add(rblContactType)
        pnlData.Controls.Add(cblAddressTypes)
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
        pnlData.Size = New Size(600, 285)
        pnlData.TabIndex = 0
        ' 
        ' rblContactType
        ' 
        rblContactType.ListPadding = New ListPadding(5, 40, 4, 8, 4, 5)
        rblContactType.Location = New Point(399, 151)
        rblContactType.Name = "rblContactType"
        rblContactType.Size = New Size(198, 123)
        rblContactType.TabIndex = 13
        rblContactType.TitleBackColor = Color.Gainsboro
        rblContactType.TitleFont = New Font("Segoe UI", 9F)
        rblContactType.TitleText = "Contact Type"
        ' 
        ' cblAddressTypes
        ' 
        cblAddressTypes.BindingMembers.Add("Domestic")
        cblAddressTypes.BindingMembers.Add("International")
        cblAddressTypes.BindingMembers.Add("Postal")
        cblAddressTypes.BindingMembers.Add("Parcel")
        cblAddressTypes.BindingMembers.Add("Home")
        cblAddressTypes.BindingMembers.Add("Business")
        cblAddressTypes.BindingMembersBindingContext = Nothing
        cblAddressTypes.Items.AddRange(New Object() {"Domestic", "International", "Postal", "Parcel", "Home", "Business"})
        cblAddressTypes.LayoutMethod = LayoutMethod.DownThenAcross
        cblAddressTypes.ListPadding = New ListPadding(8, 30, 4, 8, 20, 5)
        cblAddressTypes.Location = New Point(12, 151)
        cblAddressTypes.Name = "cblAddressTypes"
        cblAddressTypes.Size = New Size(381, 123)
        cblAddressTypes.TabIndex = 12
        cblAddressTypes.TitleBackColor = Color.Gainsboro
        cblAddressTypes.TitleFont = New Font("Segoe UI", 9F)
        cblAddressTypes.TitleText = "Address Types"
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
        lblAddRow.Location = New Point(12, 361)
        lblAddRow.Name = "lblAddRow"
        lblAddRow.Size = New Size(600, 24)
        lblAddRow.TabIndex = 3
        lblAddRow.Text = "Please click the Add button to add a new row"
        lblAddRow.TextAlign = ContentAlignment.MiddleCenter
        lblAddRow.Visible = False
        ' 
        ' epErrors
        ' 
        epErrors.ContainerControl = Me
        ' 
        ' dlPhones
        ' 
        dlPhones.CaptionText = "Phone Numbers"
        dlPhones.CaptionVisible = True
        dlPhones.Location = New Point(618, 12)
        dlPhones.Name = "dlPhones"
        dlPhones.Size = New Size(376, 373)
        dlPhones.TabIndex = 2
        ' 
        ' txtFindName
        ' 
        txtFindName.Location = New Point(154, 12)
        txtFindName.MaxLength = 30
        txtFindName.Name = "txtFindName"
        txtFindName.Size = New Size(160, 27)
        txtFindName.TabIndex = 7
        ' 
        ' clickableLabel1
        ' 
        clickableLabel1.Location = New Point(12, 14)
        clickableLabel1.Name = "clickableLabel1"
        clickableLabel1.Size = New Size(136, 23)
        clickableLabel1.TabIndex = 6
        clickableLabel1.Text = "&Find Last Name"
        clickableLabel1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' RelationTestForm
        ' 
        Me.ClientSize = New Size(1006, 432)
        Me.Controls.Add(txtFindName)
        Me.Controls.Add(clickableLabel1)
        Me.Controls.Add(dlPhones)
        Me.Controls.Add(lblAddRow)
        Me.Controls.Add(pnlData)
        Me.Controls.Add(btnSave)
        Me.Controls.Add(dnNav)
        Me.Controls.Add(btnLoad)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "RelationTestForm"
        Me.ShowInTaskbar = False
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Relationship Test Form"
        pnlData.ResumeLayout(False)
        pnlData.PerformLayout()
        CType(rblContactType, ISupportInitialize).EndInit()
        CType(cblAddressTypes, ISupportInitialize).EndInit()
        CType(cboState, ISupportInitialize).EndInit()
        CType(epErrors, ISupportInitialize).EndInit()
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
