<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RadioButtonListTestForm
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
        Dim resources As ComponentResourceManager = New ComponentResourceManager(GetType(RadioButtonListTestForm))
        pgProps = New PropertyGrid()
        grpOptions = New GroupBox()
        cboColumns = New ComboBox()
        btnGetValue = New Button()
        udcRowNumber = New NumericUpDown()
        label6 = New Label()
        label5 = New Label()
        txtValue = New TextBox()
        cboDataSource = New ComboBox()
        label1 = New Label()
        rblDemo = New RadioButtonList()
        chkShowImages = New CheckBox()
        ilImages = New ImageList(components)
        grpOptions.SuspendLayout()
        CType(udcRowNumber, ISupportInitialize).BeginInit()
        CType(rblDemo, ISupportInitialize).BeginInit()
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
        grpOptions.Controls.Add(cboColumns)
        grpOptions.Controls.Add(btnGetValue)
        grpOptions.Controls.Add(udcRowNumber)
        grpOptions.Controls.Add(label6)
        grpOptions.Controls.Add(label5)
        grpOptions.Controls.Add(txtValue)
        grpOptions.Controls.Add(cboDataSource)
        grpOptions.Controls.Add(label1)
        grpOptions.Location = New Point(513, 585)
        grpOptions.Name = "grpOptions"
        grpOptions.Size = New Size(485, 120)
        grpOptions.TabIndex = 1
        grpOptions.TabStop = False
        grpOptions.Text = "Options"
        ' 
        ' cboColumns
        ' 
        cboColumns.DropDownStyle = ComboBoxStyle.DropDownList
        cboColumns.Location = New Point(104, 56)
        cboColumns.Name = "cboColumns"
        cboColumns.Size = New Size(132, 28)
        cboColumns.TabIndex = 3
        ' 
        ' btnGetValue
        ' 
        btnGetValue.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnGetValue.Location = New Point(350, 52)
        btnGetValue.Name = "btnGetValue"
        btnGetValue.Size = New Size(75, 28)
        btnGetValue.TabIndex = 6
        btnGetValue.Text = "&Get"
        ' 
        ' udcRowNumber
        ' 
        udcRowNumber.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        udcRowNumber.Location = New Point(288, 56)
        udcRowNumber.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        udcRowNumber.Name = "udcRowNumber"
        udcRowNumber.Size = New Size(56, 27)
        udcRowNumber.TabIndex = 5
        udcRowNumber.TextAlign = HorizontalAlignment.Right
        ' 
        ' label6
        ' 
        label6.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        label6.Location = New Point(240, 56)
        label6.Name = "label6"
        label6.Size = New Size(48, 23)
        label6.TabIndex = 4
        label6.Text = "at row"
        label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' label5
        ' 
        label5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        label5.Location = New Point(9, 56)
        label5.Name = "label5"
        label5.Size = New Size(89, 23)
        label5.TabIndex = 2
        label5.Text = "G&et column"
        label5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' txtValue
        ' 
        txtValue.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtValue.Location = New Point(8, 88)
        txtValue.Name = "txtValue"
        txtValue.ReadOnly = True
        txtValue.Size = New Size(469, 27)
        txtValue.TabIndex = 7
        txtValue.TabStop = False
        ' 
        ' cboDataSource
        ' 
        cboDataSource.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        cboDataSource.DropDownStyle = ComboBoxStyle.DropDownList
        cboDataSource.Items.AddRange(New Object() {"Demo Data (List(Of DemoData))", "Product Info (List(Of ProductInfo))", "Array List", "Strings"})
        cboDataSource.Location = New Point(104, 24)
        cboDataSource.Name = "cboDataSource"
        cboDataSource.Size = New Size(321, 28)
        cboDataSource.TabIndex = 1
        ' 
        ' label1
        ' 
        label1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        label1.Location = New Point(6, 24)
        label1.Name = "label1"
        label1.Size = New Size(92, 23)
        label1.TabIndex = 0
        label1.Text = "&Data Source"
        label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' rblDemo
        ' 
        rblDemo.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        rblDemo.Location = New Point(513, 8)
        rblDemo.Name = "rblDemo"
        rblDemo.Size = New Size(485, 561)
        rblDemo.TabIndex = 0
        rblDemo.TitleFont = New Font("Segoe UI", 9F)
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
        ' RadioButtonListTestForm
        ' 
        Me.ClientSize = New Size(1006, 721)
        Me.Controls.Add(chkShowImages)
        Me.Controls.Add(rblDemo)
        Me.Controls.Add(grpOptions)
        Me.Controls.Add(pgProps)
        Me.MaximumSize = New Size(2000, 2000)
        Me.MinimumSize = New Size(840, 360)
        Me.Name = "RadioButtonListTestForm"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = SizeGripStyle.Show
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Radio Button List Test"
        grpOptions.ResumeLayout(False)
        grpOptions.PerformLayout()
        CType(udcRowNumber, ISupportInitialize).EndInit()
        CType(rblDemo, ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pgProps As System.Windows.Forms.PropertyGrid
    Friend WithEvents grpOptions As System.Windows.Forms.GroupBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents rblDemo As EWSoftware.ListControls.RadioButtonList
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents chkShowImages As System.Windows.Forms.CheckBox
    Friend WithEvents ilImages As System.Windows.Forms.ImageList
    Friend WithEvents cboColumns As System.Windows.Forms.ComboBox
    Friend WithEvents btnGetValue As System.Windows.Forms.Button
    Friend WithEvents udcRowNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents cboDataSource As System.Windows.Forms.ComboBox

End Class
