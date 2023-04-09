<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CheckBoxListTestForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                demoData?.Dispose()
                productData?.Dispose()
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CheckBoxListTestForm))
        Me.pgProps = New System.Windows.Forms.PropertyGrid()
        Me.grpOptions = New System.Windows.Forms.GroupBox()
        Me.btnCheckedItemsText = New System.Windows.Forms.Button()
        Me.btnCheckedIndices = New System.Windows.Forms.Button()
        Me.btnCheckedItems = New System.Windows.Forms.Button()
        Me.cboColumns = New System.Windows.Forms.ComboBox()
        Me.btnGetValue = New System.Windows.Forms.Button()
        Me.txtRowNumber = New System.Windows.Forms.NumericUpDown()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.txtValue = New System.Windows.Forms.TextBox()
        Me.cboDataSource = New System.Windows.Forms.ComboBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.cblDemo = New EWSoftware.ListControls.CheckBoxList()
        Me.chkShowImages = New System.Windows.Forms.CheckBox()
        Me.ilImages = New System.Windows.Forms.ImageList(Me.components)
        Me.grpOptions.SuspendLayout
        CType(Me.txtRowNumber,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.cblDemo,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'pgProps
        '
        Me.pgProps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.pgProps.LineColor = System.Drawing.SystemColors.ScrollBar
        Me.pgProps.Location = New System.Drawing.Point(6, 0)
        Me.pgProps.Name = "pgProps"
        Me.pgProps.Size = New System.Drawing.Size(368, 444)
        Me.pgProps.TabIndex = 3
        '
        'grpOptions
        '
        Me.grpOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.grpOptions.Controls.Add(Me.btnCheckedItemsText)
        Me.grpOptions.Controls.Add(Me.btnCheckedIndices)
        Me.grpOptions.Controls.Add(Me.btnCheckedItems)
        Me.grpOptions.Controls.Add(Me.cboColumns)
        Me.grpOptions.Controls.Add(Me.btnGetValue)
        Me.grpOptions.Controls.Add(Me.txtRowNumber)
        Me.grpOptions.Controls.Add(Me.label6)
        Me.grpOptions.Controls.Add(Me.label5)
        Me.grpOptions.Controls.Add(Me.txtValue)
        Me.grpOptions.Controls.Add(Me.cboDataSource)
        Me.grpOptions.Controls.Add(Me.label1)
        Me.grpOptions.Location = New System.Drawing.Point(384, 336)
        Me.grpOptions.Name = "grpOptions"
        Me.grpOptions.Size = New System.Drawing.Size(440, 120)
        Me.grpOptions.TabIndex = 1
        Me.grpOptions.TabStop = false
        Me.grpOptions.Text = "Options"
        '
        'btnCheckedItemsText
        '
        Me.btnCheckedItemsText.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnCheckedItemsText.Location = New System.Drawing.Point(287, 21)
        Me.btnCheckedItemsText.Name = "btnCheckedItemsText"
        Me.btnCheckedItemsText.Size = New System.Drawing.Size(70, 28)
        Me.btnCheckedItemsText.TabIndex = 3
        Me.btnCheckedItemsText.Text = "DispT&xt"
        '
        'btnCheckedIndices
        '
        Me.btnCheckedIndices.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnCheckedIndices.Location = New System.Drawing.Point(362, 21)
        Me.btnCheckedIndices.Name = "btnCheckedIndices"
        Me.btnCheckedIndices.Size = New System.Drawing.Size(70, 28)
        Me.btnCheckedIndices.TabIndex = 4
        Me.btnCheckedIndices.Text = "I&ndices"
        '
        'btnCheckedItems
        '
        Me.btnCheckedItems.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnCheckedItems.Location = New System.Drawing.Point(211, 21)
        Me.btnCheckedItems.Name = "btnCheckedItems"
        Me.btnCheckedItems.Size = New System.Drawing.Size(70, 28)
        Me.btnCheckedItems.TabIndex = 2
        Me.btnCheckedItems.Text = "I&tems"
        '
        'cboColumns
        '
        Me.cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColumns.Location = New System.Drawing.Point(104, 56)
        Me.cboColumns.Name = "cboColumns"
        Me.cboColumns.Size = New System.Drawing.Size(132, 28)
        Me.cboColumns.TabIndex = 6
        '
        'btnGetValue
        '
        Me.btnGetValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.btnGetValue.Location = New System.Drawing.Point(362, 53)
        Me.btnGetValue.Name = "btnGetValue"
        Me.btnGetValue.Size = New System.Drawing.Size(70, 28)
        Me.btnGetValue.TabIndex = 9
        Me.btnGetValue.Text = "&Get"
        '
        'txtRowNumber
        '
        Me.txtRowNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.txtRowNumber.Location = New System.Drawing.Point(288, 56)
        Me.txtRowNumber.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.txtRowNumber.Name = "txtRowNumber"
        Me.txtRowNumber.Size = New System.Drawing.Size(56, 26)
        Me.txtRowNumber.TabIndex = 8
        Me.txtRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'label6
        '
        Me.label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.label6.Location = New System.Drawing.Point(240, 56)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(48, 23)
        Me.label6.TabIndex = 7
        Me.label6.Text = "at row"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label5
        '
        Me.label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.label5.Location = New System.Drawing.Point(9, 55)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(89, 23)
        Me.label5.TabIndex = 5
        Me.label5.Text = "G&et column"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtValue
        '
        Me.txtValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.txtValue.Location = New System.Drawing.Point(8, 88)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.ReadOnly = true
        Me.txtValue.Size = New System.Drawing.Size(424, 26)
        Me.txtValue.TabIndex = 10
        Me.txtValue.TabStop = false
        '
        'cboDataSource
        '
        Me.cboDataSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.cboDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDataSource.Items.AddRange(New Object() {"Data Table", "Data View", "Data Set", "Array List", "Strings"})
        Me.cboDataSource.Location = New System.Drawing.Point(104, 24)
        Me.cboDataSource.Name = "cboDataSource"
        Me.cboDataSource.Size = New System.Drawing.Size(101, 28)
        Me.cboDataSource.TabIndex = 1
        '
        'label1
        '
        Me.label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.label1.Location = New System.Drawing.Point(6, 24)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(92, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "&Data Source"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cblDemo
        '
        Me.cblDemo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cblDemo.BindingMembersBindingContext = Nothing
        Me.cblDemo.Location = New System.Drawing.Point(384, 8)
        Me.cblDemo.Name = "cblDemo"
        Me.cblDemo.Size = New System.Drawing.Size(440, 320)
        Me.cblDemo.TabIndex = 0
        Me.cblDemo.TitleFont = New System.Drawing.Font("Microsoft Sans Serif", 8!)
        '
        'chkShowImages
        '
        Me.chkShowImages.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left),System.Windows.Forms.AnchorStyles)
        Me.chkShowImages.Location = New System.Drawing.Point(8, 444)
        Me.chkShowImages.Name = "chkShowImages"
        Me.chkShowImages.Size = New System.Drawing.Size(136, 24)
        Me.chkShowImages.TabIndex = 2
        Me.chkShowImages.Text = "Use &Image List"
        '
        'ilImages
        '
        Me.ilImages.ImageStream = CType(resources.GetObject("ilImages.ImageStream"),System.Windows.Forms.ImageListStreamer)
        Me.ilImages.TransparentColor = System.Drawing.Color.Lime
        Me.ilImages.Images.SetKeyName(0, "")
        Me.ilImages.Images.SetKeyName(1, "")
        Me.ilImages.Images.SetKeyName(2, "")
        Me.ilImages.Images.SetKeyName(3, "")
        Me.ilImages.Images.SetKeyName(4, "")
        '
        'CheckBoxListTestForm
        '
        Me.ClientSize = New System.Drawing.Size(832, 472)
        Me.Controls.Add(Me.chkShowImages)
        Me.Controls.Add(Me.cblDemo)
        Me.Controls.Add(Me.grpOptions)
        Me.Controls.Add(Me.pgProps)
        Me.MaximumSize = New System.Drawing.Size(2000, 2000)
        Me.MinimumSize = New System.Drawing.Size(840, 360)
        Me.Name = "CheckBoxListTestForm"
        Me.ShowInTaskbar = false
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Checkbox List Test"
        Me.grpOptions.ResumeLayout(false)
        Me.grpOptions.PerformLayout
        CType(Me.txtRowNumber,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.cblDemo,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

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
    Friend WithEvents txtRowNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents btnCheckedItems As System.Windows.Forms.Button
    Friend WithEvents btnCheckedIndices As System.Windows.Forms.Button
    Friend WithEvents btnCheckedItemsText As System.Windows.Forms.Button
    Friend WithEvents cboDataSource As System.Windows.Forms.ComboBox
End Class

