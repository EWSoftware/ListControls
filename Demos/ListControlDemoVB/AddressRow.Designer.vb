<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddressRow
    Inherits EWSoftware.ListControls.TemplateControl

    'UserControl overrides dispose to clean up the component list.
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
        components = New Container()
        cboState = New MultiColumnComboBox()
        txtZip = New TextBox()
        label4 = New ClickableLabel()
        txtCity = New TextBox()
        label3 = New ClickableLabel()
        txtAddress = New TextBox()
        label2 = New ClickableLabel()
        txtLName = New TextBox()
        label1 = New ClickableLabel()
        txtFName = New TextBox()
        epErrors = New ErrorProvider(components)
        label5 = New ClickableLabel()
        udcSumValue = New NumericUpDown()
        CType(cboState, ISupportInitialize).BeginInit()
        CType(epErrors, ISupportInitialize).BeginInit()
        CType(udcSumValue, ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' cboState
        ' 
        cboState.DropDownStyle = ComboBoxStyle.DropDownList
        cboState.Location = New Point(266, 73)
        cboState.MaxDropDownItems = 16
        cboState.Name = "cboState"
        cboState.Size = New Size(66, 29)
        cboState.TabIndex = 8
        ' 
        ' txtZip
        ' 
        txtZip.Location = New Point(338, 73)
        txtZip.MaxLength = 10
        txtZip.Name = "txtZip"
        txtZip.Size = New Size(92, 27)
        txtZip.TabIndex = 9
        ' 
        ' label4
        ' 
        label4.BackColor = Color.Transparent
        label4.Location = New Point(3, 73)
        label4.Name = "label4"
        label4.Size = New Size(93, 22)
        label4.TabIndex = 6
        label4.Text = "&City/St/Zip"
        label4.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtCity
        ' 
        txtCity.Location = New Point(102, 73)
        txtCity.MaxLength = 20
        txtCity.Name = "txtCity"
        txtCity.Size = New Size(158, 27)
        txtCity.TabIndex = 7
        ' 
        ' label3
        ' 
        label3.BackColor = Color.Transparent
        label3.Location = New Point(20, 42)
        label3.Name = "label3"
        label3.Size = New Size(76, 22)
        label3.TabIndex = 4
        label3.Text = "&Address"
        label3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtAddress
        ' 
        txtAddress.Location = New Point(102, 40)
        txtAddress.MaxLength = 50
        txtAddress.Name = "txtAddress"
        txtAddress.Size = New Size(451, 27)
        txtAddress.TabIndex = 5
        ' 
        ' label2
        ' 
        label2.BackColor = Color.Transparent
        label2.Location = New Point(3, 9)
        label2.Name = "label2"
        label2.Size = New Size(93, 22)
        label2.TabIndex = 0
        label2.Text = "&Last Name"
        label2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtLName
        ' 
        txtLName.Location = New Point(102, 7)
        txtLName.MaxLength = 30
        txtLName.Name = "txtLName"
        txtLName.Size = New Size(177, 27)
        txtLName.TabIndex = 1
        ' 
        ' label1
        ' 
        label1.BackColor = Color.Transparent
        label1.Location = New Point(285, 9)
        label1.Name = "label1"
        label1.Size = New Size(92, 22)
        label1.TabIndex = 2
        label1.Text = "First Name"
        label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtFName
        ' 
        txtFName.Location = New Point(383, 7)
        txtFName.MaxLength = 20
        txtFName.Name = "txtFName"
        txtFName.Size = New Size(170, 27)
        txtFName.TabIndex = 3
        ' 
        ' epErrors
        ' 
        epErrors.ContainerControl = Me
        ' 
        ' label5
        ' 
        label5.BackColor = Color.Transparent
        label5.Location = New Point(438, 74)
        label5.Name = "label5"
        label5.Size = New Size(53, 22)
        label5.TabIndex = 10
        label5.Text = "&Value"
        label5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' udcSumValue
        ' 
        udcSumValue.Location = New Point(497, 72)
        udcSumValue.Name = "udcSumValue"
        udcSumValue.Size = New Size(56, 27)
        udcSumValue.TabIndex = 11
        udcSumValue.TextAlign = HorizontalAlignment.Right
        ' 
        ' AddressRow
        ' 
        Me.Controls.Add(udcSumValue)
        Me.Controls.Add(label5)
        Me.Controls.Add(cboState)
        Me.Controls.Add(txtZip)
        Me.Controls.Add(label4)
        Me.Controls.Add(txtCity)
        Me.Controls.Add(label3)
        Me.Controls.Add(txtAddress)
        Me.Controls.Add(label2)
        Me.Controls.Add(txtLName)
        Me.Controls.Add(label1)
        Me.Controls.Add(txtFName)
        Me.Name = "AddressRow"
        Me.Size = New Size(580, 108)
        CType(cboState, ISupportInitialize).EndInit()
        CType(epErrors, ISupportInitialize).EndInit()
        CType(udcSumValue, ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cboState As EWSoftware.ListControls.MultiColumnComboBox
    Friend WithEvents txtZip As System.Windows.Forms.TextBox
    Friend WithEvents txtCity As System.Windows.Forms.TextBox
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents txtLName As System.Windows.Forms.TextBox
    Friend WithEvents label1 As EWSoftware.ListControls.ClickableLabel
    Friend WithEvents txtFName As System.Windows.Forms.TextBox
    Friend WithEvents epErrors As System.Windows.Forms.ErrorProvider
    Friend WithEvents label4 As EWSoftware.ListControls.ClickableLabel
    Friend WithEvents label3 As EWSoftware.ListControls.ClickableLabel
    Friend WithEvents label2 As EWSoftware.ListControls.ClickableLabel
    Friend WithEvents label5 As EWSoftware.ListControls.ClickableLabel
    Friend WithEvents udcSumValue As System.Windows.Forms.NumericUpDown

End Class

