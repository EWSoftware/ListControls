<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataListTestForm
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
        dlList = New DataList()
        btnSave = New Button()
        pgProps = New PropertyGrid()
        btnAddDSRow = New Button()
        btnDelDSRow = New Button()
        btnModRow = New Button()
        toolTip1 = New ToolTip(components)
        btnGetValue = New Button()
        label1 = New Label()
        udcRowNumber = New NumericUpDown()
        cboColumns = New ComboBox()
        txtValue = New TextBox()
        txtRowNumber = New NumericUpDown()
        label6 = New Label()
        label5 = New Label()
        chkShowHeader = New CheckBox()
        chkShowFooter = New CheckBox()
        CType(udcRowNumber, ISupportInitialize).BeginInit()
        CType(txtRowNumber, ISupportInitialize).BeginInit()
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
        ' dlList
        ' 
        dlList.AllowDrop = True
        dlList.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dlList.CaptionText = "Names and Addresses"
        dlList.CaptionVisible = True
        dlList.Location = New Point(12, 12)
        dlList.Name = "dlList"
        dlList.Size = New Size(655, 629)
        dlList.TabIndex = 0
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
        pgProps.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        pgProps.LineColor = SystemColors.ScrollBar
        pgProps.Location = New Point(673, 12)
        pgProps.Name = "pgProps"
        pgProps.Size = New Size(321, 629)
        pgProps.TabIndex = 1
        ' 
        ' btnAddDSRow
        ' 
        btnAddDSRow.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnAddDSRow.Location = New Point(280, 681)
        btnAddDSRow.Name = "btnAddDSRow"
        btnAddDSRow.Size = New Size(104, 32)
        btnAddDSRow.TabIndex = 10
        btnAddDSRow.Text = "Add Row"
        toolTip1.SetToolTip(btnAddDSRow, "Add row directly to the data source")
        ' 
        ' btnDelDSRow
        ' 
        btnDelDSRow.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnDelDSRow.Location = New Point(572, 681)
        btnDelDSRow.Name = "btnDelDSRow"
        btnDelDSRow.Size = New Size(104, 32)
        btnDelDSRow.TabIndex = 13
        btnDelDSRow.Text = "Del DS Row"
        toolTip1.SetToolTip(btnDelDSRow, "Delete row directly from data source")
        ' 
        ' btnModRow
        ' 
        btnModRow.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnModRow.Location = New Point(682, 681)
        btnModRow.Name = "btnModRow"
        btnModRow.Size = New Size(104, 32)
        btnModRow.TabIndex = 14
        btnModRow.Text = "Modify Row"
        toolTip1.SetToolTip(btnModRow, "Modify row directly in data source")
        ' 
        ' btnGetValue
        ' 
        btnGetValue.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnGetValue.Location = New Point(354, 644)
        btnGetValue.Name = "btnGetValue"
        btnGetValue.Size = New Size(75, 32)
        btnGetValue.TabIndex = 6
        btnGetValue.Text = "&Get"
        toolTip1.SetToolTip(btnGetValue, "Get the specified column from the specified row")
        ' 
        ' label1
        ' 
        label1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        label1.Location = New Point(390, 685)
        label1.Name = "label1"
        label1.Size = New Size(102, 23)
        label1.TabIndex = 11
        label1.Text = "Del/Mod Row"
        label1.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' udcRowNumber
        ' 
        udcRowNumber.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        udcRowNumber.Location = New Point(502, 685)
        udcRowNumber.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        udcRowNumber.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        udcRowNumber.Name = "udcRowNumber"
        udcRowNumber.Size = New Size(64, 27)
        udcRowNumber.TabIndex = 12
        udcRowNumber.TextAlign = HorizontalAlignment.Right
        udcRowNumber.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' cboColumns
        ' 
        cboColumns.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        cboColumns.DropDownStyle = ComboBoxStyle.DropDownList
        cboColumns.Items.AddRange(New Object() {"ID", "FirstName", "LastName", "StreetAddress", "City", "State", "Zip", "SumValue"})
        cboColumns.Location = New Point(107, 647)
        cboColumns.Name = "cboColumns"
        cboColumns.Size = New Size(132, 28)
        cboColumns.TabIndex = 3
        ' 
        ' txtValue
        ' 
        txtValue.AllowDrop = True
        txtValue.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        txtValue.Location = New Point(435, 647)
        txtValue.Name = "txtValue"
        txtValue.ReadOnly = True
        txtValue.Size = New Size(350, 27)
        txtValue.TabIndex = 7
        txtValue.TabStop = False
        ' 
        ' txtRowNumber
        ' 
        txtRowNumber.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        txtRowNumber.Location = New Point(291, 647)
        txtRowNumber.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        txtRowNumber.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        txtRowNumber.Name = "txtRowNumber"
        txtRowNumber.Size = New Size(56, 27)
        txtRowNumber.TabIndex = 5
        txtRowNumber.TextAlign = HorizontalAlignment.Right
        txtRowNumber.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' label6
        ' 
        label6.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        label6.Location = New Point(243, 647)
        label6.Name = "label6"
        label6.Size = New Size(48, 23)
        label6.TabIndex = 4
        label6.Text = "at row"
        label6.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' label5
        ' 
        label5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        label5.Location = New Point(11, 649)
        label5.Name = "label5"
        label5.Size = New Size(90, 23)
        label5.TabIndex = 2
        label5.Text = "G&et column"
        label5.TextAlign = ContentAlignment.MiddleRight
        ' 
        ' chkShowHeader
        ' 
        chkShowHeader.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        chkShowHeader.Checked = True
        chkShowHeader.CheckState = CheckState.Checked
        chkShowHeader.Location = New Point(813, 648)
        chkShowHeader.Name = "chkShowHeader"
        chkShowHeader.Size = New Size(142, 24)
        chkShowHeader.TabIndex = 15
        chkShowHeader.Text = "Show header"
        ' 
        ' chkShowFooter
        ' 
        chkShowFooter.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        chkShowFooter.Checked = True
        chkShowFooter.CheckState = CheckState.Checked
        chkShowFooter.Location = New Point(813, 678)
        chkShowFooter.Name = "chkShowFooter"
        chkShowFooter.Size = New Size(142, 24)
        chkShowFooter.TabIndex = 16
        chkShowFooter.Text = "Show footer"
        ' 
        ' DataListTestForm
        ' 
        Me.ClientSize = New Size(1006, 721)
        Me.Controls.Add(chkShowFooter)
        Me.Controls.Add(chkShowHeader)
        Me.Controls.Add(cboColumns)
        Me.Controls.Add(btnGetValue)
        Me.Controls.Add(txtValue)
        Me.Controls.Add(txtRowNumber)
        Me.Controls.Add(label6)
        Me.Controls.Add(label5)
        Me.Controls.Add(udcRowNumber)
        Me.Controls.Add(label1)
        Me.Controls.Add(btnModRow)
        Me.Controls.Add(btnDelDSRow)
        Me.Controls.Add(btnAddDSRow)
        Me.Controls.Add(pgProps)
        Me.Controls.Add(btnSave)
        Me.Controls.Add(dlList)
        Me.Controls.Add(btnLoad)
        Me.MinimumSize = New Size(795, 190)
        Me.Name = "DataListTestForm"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = SizeGripStyle.Show
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Text = "Test Data List Control"
        CType(udcRowNumber, ISupportInitialize).EndInit()
        CType(txtRowNumber, ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents dlList As EWSoftware.ListControls.DataList
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents pgProps As System.Windows.Forms.PropertyGrid
    Friend WithEvents btnAddDSRow As System.Windows.Forms.Button
    Friend WithEvents btnDelDSRow As System.Windows.Forms.Button
    Friend WithEvents btnModRow As System.Windows.Forms.Button
    Friend WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents udcRowNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents cboColumns As System.Windows.Forms.ComboBox
    Friend WithEvents btnGetValue As System.Windows.Forms.Button
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents txtRowNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents chkShowHeader As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowFooter As System.Windows.Forms.CheckBox
End Class
