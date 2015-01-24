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
        Me.components = New System.ComponentModel.Container
        Me.cboState = New EWSoftware.ListControls.MultiColumnComboBox
        Me.txtZip = New System.Windows.Forms.TextBox
        Me.label4 = New EWSoftware.ListControls.ClickableLabel
        Me.txtCity = New System.Windows.Forms.TextBox
        Me.label3 = New EWSoftware.ListControls.ClickableLabel
        Me.txtAddress = New System.Windows.Forms.TextBox
        Me.label2 = New EWSoftware.ListControls.ClickableLabel
        Me.txtLName = New System.Windows.Forms.TextBox
        Me.label1 = New EWSoftware.ListControls.ClickableLabel
        Me.txtFName = New System.Windows.Forms.TextBox
        Me.epErrors = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.label5 = New EWSoftware.ListControls.ClickableLabel
        Me.udcSumValue = New System.Windows.Forms.NumericUpDown
        CType(Me.cboState, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.epErrors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.udcSumValue, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboState
        '
        Me.cboState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboState.Location = New System.Drawing.Point(223, 72)
        Me.cboState.MaxDropDownItems = 16
        Me.cboState.Name = "cboState"
        Me.cboState.Size = New System.Drawing.Size(57, 24)
        Me.cboState.TabIndex = 8
        '
        'txtZip
        '
        Me.txtZip.Location = New System.Drawing.Point(288, 72)
        Me.txtZip.MaxLength = 10
        Me.txtZip.Name = "txtZip"
        Me.txtZip.Size = New System.Drawing.Size(77, 22)
        Me.txtZip.TabIndex = 9
        '
        'label4
        '
        Me.label4.BackColor = System.Drawing.Color.Transparent
        Me.label4.Location = New System.Drawing.Point(3, 72)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(79, 22)
        Me.label4.TabIndex = 6
        Me.label4.Text = "&City/St/Zip"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(88, 72)
        Me.txtCity.MaxLength = 20
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(120, 22)
        Me.txtCity.TabIndex = 7
        '
        'label3
        '
        Me.label3.BackColor = System.Drawing.Color.Transparent
        Me.label3.Location = New System.Drawing.Point(6, 40)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(76, 22)
        Me.label3.TabIndex = 4
        Me.label3.Text = "&Address"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(88, 40)
        Me.txtAddress.MaxLength = 50
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(408, 22)
        Me.txtAddress.TabIndex = 5
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.Color.Transparent
        Me.label2.Location = New System.Drawing.Point(0, 9)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(82, 22)
        Me.label2.TabIndex = 0
        Me.label2.Text = "&Last Name"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLName
        '
        Me.txtLName.Location = New System.Drawing.Point(88, 9)
        Me.txtLName.MaxLength = 30
        Me.txtLName.Name = "txtLName"
        Me.txtLName.Size = New System.Drawing.Size(160, 22)
        Me.txtLName.TabIndex = 1
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.Color.Transparent
        Me.label1.Location = New System.Drawing.Point(254, 9)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(81, 22)
        Me.label1.TabIndex = 2
        Me.label1.Text = "First Name"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFName
        '
        Me.txtFName.Location = New System.Drawing.Point(341, 9)
        Me.txtFName.MaxLength = 20
        Me.txtFName.Name = "txtFName"
        Me.txtFName.Size = New System.Drawing.Size(155, 22)
        Me.txtFName.TabIndex = 3
        '
        'epErrors
        '
        Me.epErrors.ContainerControl = Me
        '
        'label5
        '
        Me.label5.BackColor = System.Drawing.Color.Transparent
        Me.label5.Location = New System.Drawing.Point(376, 72)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(58, 22)
        Me.label5.TabIndex = 10
        Me.label5.Text = "&Value"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'udcSumValue
        '
        Me.udcSumValue.Location = New System.Drawing.Point(440, 72)
        Me.udcSumValue.Name = "udcSumValue"
        Me.udcSumValue.Size = New System.Drawing.Size(56, 22)
        Me.udcSumValue.TabIndex = 11
        Me.udcSumValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'AddressRow
        '
        Me.Controls.Add(Me.udcSumValue)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.cboState)
        Me.Controls.Add(Me.txtZip)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.txtCity)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.txtAddress)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.txtLName)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.txtFName)
        Me.Name = "AddressRow"
        Me.Size = New System.Drawing.Size(504, 106)
        CType(Me.cboState, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.epErrors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.udcSumValue, System.ComponentModel.ISupportInitialize).EndInit()
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

