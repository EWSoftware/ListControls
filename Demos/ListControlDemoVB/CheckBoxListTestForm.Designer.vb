<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CheckBoxListTestForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
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
        Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(CheckBoxListTestForm))
        pgProps = New PropertyGrid()
        grpOptions = New GroupBox()
        btnCheckedItemsText = New Button()
        btnCheckedIndices = New Button()
        btnCheckedItems = New Button()
        cboColumns = New ComboBox()
        btnGetValue = New Button()
        udcRowNumber = New NumericUpDown()
        label6 = New Label()
        label5 = New Label()
        txtValue = New TextBox()
        cboDataSource = New ComboBox()
        label1 = New Label()
        cblDemo = New CheckBoxList()
        chkShowImages = New CheckBox()
        ilImages = New ImageList(components)
        grpOptions.SuspendLayout()
        CType(udcRowNumber, ISupportInitialize).BeginInit()
        CType(cblDemo, ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' pgProps
        ' 
        pgProps.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        pgProps.LineColor = SystemColors.ScrollBar
        pgProps.Location = New Point(6, 0)
        pgProps.Name = "pgProps"
        pgProps.Size = New Size(501, 693)
        pgProps.TabIndex = 3
        ' 
        ' grpOptions
        ' 
        grpOptions.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        grpOptions.Controls.Add(btnCheckedItemsText)
        grpOptions.Controls.Add(btnCheckedIndices)
        grpOptions.Controls.Add(btnCheckedItems)
        grpOptions.Controls.Add(cboColumns)
        grpOptions.Controls.Add(btnGetValue)
        grpOptions.Controls.Add(udcRowNumber)
        grpOptions.Controls.Add(label6)
        grpOptions.Controls.Add(label5)
        grpOptions.Controls.Add(txtValue)
        grpOptions.Controls.Add(cboDataSource)
        grpOptions.Controls.Add(label1)
        grpOptions.Location = New Point(513, 547)
        grpOptions.Name = "grpOptions"
        grpOptions.Size = New Size(485, 158)
        grpOptions.TabIndex = 1
        grpOptions.TabStop = False
        grpOptions.Text = "Options"
        ' 
        ' btnCheckedItemsText
        ' 
        btnCheckedItemsText.Location = New Point(180, 58)
        btnCheckedItemsText.Name = "btnCheckedItemsText"
        btnCheckedItemsText.Size = New Size(70, 28)
        btnCheckedItemsText.TabIndex = 3
        btnCheckedItemsText.Text = "DispT&xt"
        ' 
        ' btnCheckedIndices
        ' 
        btnCheckedIndices.Location = New Point(255, 58)
        btnCheckedIndices.Name = "btnCheckedIndices"
        btnCheckedIndices.Size = New Size(70, 28)
        btnCheckedIndices.TabIndex = 4
        btnCheckedIndices.Text = "I&ndices"
        ' 
        ' btnCheckedItems
        ' 
        btnCheckedItems.Location = New Point(104, 58)
        btnCheckedItems.Name = "btnCheckedItems"
        btnCheckedItems.Size = New Size(70, 28)
        btnCheckedItems.TabIndex = 2
        btnCheckedItems.Text = "I&tems"
        ' 
        ' cboColumns
        ' 
        cboColumns.DropDownStyle = ComboBoxStyle.DropDownList
        cboColumns.Location = New Point(104, 92)
        cboColumns.Name = "cboColumns"
        cboColumns.Size = New Size(132, 28)
        cboColumns.TabIndex = 6
        ' 
        ' btnGetValue
        ' 
        btnGetValue.Location = New Point(362, 89)
        btnGetValue.Name = "btnGetValue"
        btnGetValue.Size = New Size(70, 28)
        btnGetValue.TabIndex = 9
        btnGetValue.Text = "&Get"
        ' 
        ' udcRowNumber
        ' 
        udcRowNumber.Location = New Point(288, 92)
        udcRowNumber.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        udcRowNumber.Name = "udcRowNumber"
        udcRowNumber.Size = New Size(56, 27)
        udcRowNumber.TabIndex = 8
        udcRowNumber.TextAlign = HorizontalAlignment.Right
        ' 
        ' label6
        ' 
        label6.Location = New Point(240, 92)
        label6.Name = "label6"
        label6.Size = New Size(48, 23)
        label6.TabIndex = 7
        label6.Text = "at row"
        label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' label5
        ' 
        label5.Location = New Point(9, 91)
        label5.Name = "label5"
        label5.Size = New Size(89, 23)
        label5.TabIndex = 5
        label5.Text = "G&et column"
        label5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtValue
        ' 
        txtValue.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtValue.Location = New Point(8, 126)
        txtValue.Name = "txtValue"
        txtValue.ReadOnly = True
        txtValue.Size = New Size(469, 27)
        txtValue.TabIndex = 10
        txtValue.TabStop = False
        ' 
        ' cboDataSource
        ' 
        cboDataSource.DropDownStyle = ComboBoxStyle.DropDownList
        cboDataSource.Items.AddRange(New Object() {"Demo Data (List(Of DemoData))", "Product Info (List(Of ProductInfo))", "Array List", "Strings"})
        cboDataSource.Location = New Point(104, 24)
        cboDataSource.Name = "cboDataSource"
        cboDataSource.Size = New Size(328, 28)
        cboDataSource.TabIndex = 1
        ' 
        ' label1
        ' 
        label1.Location = New Point(6, 24)
        label1.Name = "label1"
        label1.Size = New Size(92, 23)
        label1.TabIndex = 0
        label1.Text = "&Data Source"
        label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' cblDemo
        ' 
        cblDemo.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        cblDemo.BindingMembersBindingContext = Nothing
        cblDemo.Location = New Point(513, 8)
        cblDemo.Name = "cblDemo"
        cblDemo.Size = New Size(485, 533)
        cblDemo.TabIndex = 0
        cblDemo.TitleFont = New Font("Microsoft Sans Serif", 8F)
        ' 
        ' chkShowImages
        ' 
        chkShowImages.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        chkShowImages.Location = New Point(8, 693)
        chkShowImages.Name = "chkShowImages"
        chkShowImages.Size = New Size(136, 24)
        chkShowImages.TabIndex = 2
        chkShowImages.Text = "Use &Image List"
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
        ' CheckBoxListTestForm
        ' 
        Me.ClientSize = New Size(1006, 721)
        Me.Controls.Add(chkShowImages)
        Me.Controls.Add(cblDemo)
        Me.Controls.Add(grpOptions)
        Me.Controls.Add(pgProps)
        Me.MaximumSize = New Size(2000, 2000)
        Me.MinimumSize = New Size(840, 360)
        Me.Name = "CheckBoxListTestForm"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = SizeGripStyle.Show
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Checkbox List Test"
        grpOptions.ResumeLayout(False)
        grpOptions.PerformLayout()
        CType(udcRowNumber, ISupportInitialize).EndInit()
        CType(cblDemo, ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pgProps As System.Windows.Forms.PropertyGrid
    Friend WithEvents grpOptions As System.Windows.Forms.GroupBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents cblDemo As EWSoftware.ListControls.CheckBoxList
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents chkShowImages As System.Windows.Forms.CheckBox
    Friend WithEvents ilImages As System.Windows.Forms.ImageList
    Friend WithEvents cboColumns As System.Windows.Forms.ComboBox
    Friend WithEvents btnGetValue As System.Windows.Forms.Button
    Friend WithEvents udcRowNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents btnCheckedItems As System.Windows.Forms.Button
    Friend WithEvents btnCheckedIndices As System.Windows.Forms.Button
    Friend WithEvents btnCheckedItemsText As System.Windows.Forms.Button
    Friend WithEvents cboDataSource As System.Windows.Forms.ComboBox
End Class

