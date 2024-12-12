<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserControlComboTestForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing
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
        Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(UserControlComboTestForm))
        cboAutoComp = New AutoCompleteComboBox()
        label2 = New Label()
        label3 = New Label()
        cboUCCombo = New UserControlComboBox()
        pgProps = New PropertyGrid()
        grpOptions = New GroupBox()
        cboColumns = New ComboBox()
        label6 = New Label()
        btnGetValue = New Button()
        txtValue = New TextBox()
        udcRowNumber = New NumericUpDown()
        label5 = New Label()
        cboDataSource = New ComboBox()
        label1 = New Label()
        ilImages = New ImageList(components)
        CType(cboUCCombo, ISupportInitialize).BeginInit()
        grpOptions.SuspendLayout()
        CType(udcRowNumber, ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' cboAutoComp
        ' 
        cboAutoComp.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        cboAutoComp.Location = New Point(519, 12)
        cboAutoComp.Name = "cboAutoComp"
        cboAutoComp.Size = New Size(475, 28)
        cboAutoComp.TabIndex = 1
        ' 
        ' label2
        ' 
        label2.Location = New Point(383, 12)
        label2.Name = "label2"
        label2.Size = New Size(130, 23)
        label2.TabIndex = 0
        label2.Text = "&Auto-Complete"
        label2.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' label3
        ' 
        label3.Location = New Point(383, 160)
        label3.Name = "label3"
        label3.Size = New Size(130, 23)
        label3.TabIndex = 2
        label3.Text = "&User Ctl Combo"
        label3.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' cboUCCombo
        ' 
        cboUCCombo.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        cboUCCombo.Location = New Point(519, 160)
        cboUCCombo.Name = "cboUCCombo"
        cboUCCombo.Size = New Size(475, 29)
        cboUCCombo.TabIndex = 3
        ' 
        ' pgProps
        ' 
        pgProps.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        pgProps.LineColor = SystemColors.ScrollBar
        pgProps.Location = New Point(6, 0)
        pgProps.Name = "pgProps"
        pgProps.Size = New Size(368, 709)
        pgProps.TabIndex = 5
        ' 
        ' grpOptions
        ' 
        grpOptions.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        grpOptions.Controls.Add(cboColumns)
        grpOptions.Controls.Add(label6)
        grpOptions.Controls.Add(btnGetValue)
        grpOptions.Controls.Add(txtValue)
        grpOptions.Controls.Add(udcRowNumber)
        grpOptions.Controls.Add(label5)
        grpOptions.Controls.Add(cboDataSource)
        grpOptions.Controls.Add(label1)
        grpOptions.Location = New Point(380, 582)
        grpOptions.Name = "grpOptions"
        grpOptions.Size = New Size(614, 127)
        grpOptions.TabIndex = 4
        grpOptions.TabStop = False
        grpOptions.Text = "Options"
        ' 
        ' cboColumns
        ' 
        cboColumns.DropDownStyle = ComboBoxStyle.DropDownList
        cboColumns.Location = New Point(135, 60)
        cboColumns.Name = "cboColumns"
        cboColumns.Size = New Size(132, 28)
        cboColumns.TabIndex = 3
        ' 
        ' label6
        ' 
        label6.Location = New Point(270, 60)
        label6.Name = "label6"
        label6.Size = New Size(48, 23)
        label6.TabIndex = 4
        label6.Text = "at row"
        label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' btnGetValue
        ' 
        btnGetValue.Location = New Point(381, 60)
        btnGetValue.Name = "btnGetValue"
        btnGetValue.Size = New Size(75, 28)
        btnGetValue.TabIndex = 6
        btnGetValue.Text = "&Get"
        ' 
        ' txtValue
        ' 
        txtValue.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtValue.Location = New Point(16, 95)
        txtValue.Name = "txtValue"
        txtValue.ReadOnly = True
        txtValue.Size = New Size(590, 27)
        txtValue.TabIndex = 7
        txtValue.TabStop = False
        ' 
        ' udcRowNumber
        ' 
        udcRowNumber.Location = New Point(319, 60)
        udcRowNumber.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        udcRowNumber.Name = "udcRowNumber"
        udcRowNumber.Size = New Size(56, 27)
        udcRowNumber.TabIndex = 5
        udcRowNumber.TextAlign = HorizontalAlignment.Right
        ' 
        ' label5
        ' 
        label5.Location = New Point(37, 60)
        label5.Name = "label5"
        label5.Size = New Size(92, 23)
        label5.TabIndex = 2
        label5.Text = "G&et column"
        label5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' cboDataSource
        ' 
        cboDataSource.DropDownStyle = ComboBoxStyle.DropDownList
        cboDataSource.Items.AddRange(New Object() {"Demo Data (List(Of DemoData))", "Product Info (List(Of ProductInfo))", "Array List", "Strings"})
        cboDataSource.Location = New Point(135, 26)
        cboDataSource.Name = "cboDataSource"
        cboDataSource.Size = New Size(321, 28)
        cboDataSource.TabIndex = 1
        ' 
        ' label1
        ' 
        label1.Location = New Point(27, 26)
        label1.Name = "label1"
        label1.Size = New Size(102, 23)
        label1.TabIndex = 0
        label1.Text = "&Data Source"
        label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' ilImages
        ' 
        ilImages.ColorDepth = ColorDepth.Depth8Bit
        ilImages.ImageStream = CType(resources.GetObject("ilImages.ImageStream"), ImageListStreamer)
        ilImages.TransparentColor = Color.Lime
        ilImages.Images.SetKeyName(0, "")
        ilImages.Images.SetKeyName(1, "")
        ilImages.Images.SetKeyName(2, "")
        ilImages.Images.SetKeyName(3, "")
        ilImages.Images.SetKeyName(4, "")
        ' 
        ' UserControlComboTestForm
        ' 
        Me.ClientSize = New Size(1006, 721)
        Me.Controls.Add(grpOptions)
        Me.Controls.Add(pgProps)
        Me.Controls.Add(label3)
        Me.Controls.Add(cboUCCombo)
        Me.Controls.Add(label2)
        Me.Controls.Add(cboAutoComp)
        Me.MinimumSize = New Size(835, 512)
        Me.Name = "UserControlComboTestForm"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = SizeGripStyle.Show
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "AutoComplete/User Control Combo Test"
        CType(cboUCCombo, ISupportInitialize).EndInit()
        grpOptions.ResumeLayout(False)
        grpOptions.PerformLayout()
        CType(udcRowNumber, ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cboAutoComp As EWSoftware.ListControls.AutoCompleteComboBox
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents pgProps As System.Windows.Forms.PropertyGrid
    Friend WithEvents grpOptions As System.Windows.Forms.GroupBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents cboDataSource As System.Windows.Forms.ComboBox
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents udcRowNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents btnGetValue As System.Windows.Forms.Button
    Friend WithEvents cboColumns As System.Windows.Forms.ComboBox
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents ilImages As System.Windows.Forms.ImageList
    Friend WithEvents cboUCCombo As EWSoftware.ListControls.UserControlComboBox

End Class
