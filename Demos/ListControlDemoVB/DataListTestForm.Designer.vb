<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DataListTestForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing Then
                dbConn?.Dispose()
                daAddresses?.Dispose()
                dsAddresses?.Dispose()
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
        Me.dlList = New EWSoftware.ListControls.DataList
        Me.btnSave = New System.Windows.Forms.Button
        Me.pgProps = New System.Windows.Forms.PropertyGrid
        Me.btnAddDSRow = New System.Windows.Forms.Button
        Me.btnDelDSRow = New System.Windows.Forms.Button
        Me.btnModRow = New System.Windows.Forms.Button
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnGetValue = New System.Windows.Forms.Button
        Me.label1 = New System.Windows.Forms.Label
        Me.udcRowNumber = New System.Windows.Forms.NumericUpDown
        Me.cboColumns = New System.Windows.Forms.ComboBox
        Me.txtValue = New System.Windows.Forms.TextBox
        Me.txtRowNumber = New System.Windows.Forms.NumericUpDown
        Me.label6 = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.chkShowHeader = New System.Windows.Forms.CheckBox
        Me.chkShowFooter = New System.Windows.Forms.CheckBox
        CType(Me.udcRowNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtRowNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.Location = New System.Drawing.Point(12, 456)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(104, 28)
        Me.btnLoad.TabIndex = 8
        Me.btnLoad.Text = "L&oad Data"
        Me.toolTip1.SetToolTip(Me.btnLoad, "Load data into the control")
        '
        'dlList
        '
        Me.dlList.AllowDrop = True
        Me.dlList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dlList.CaptionText = "Names and Addresses"
        Me.dlList.CaptionVisible = True
        Me.dlList.Location = New System.Drawing.Point(12, 12)
        Me.dlList.Name = "dlList"
        Me.dlList.Size = New System.Drawing.Size(593, 404)
        Me.dlList.TabIndex = 0
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(122, 456)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(104, 28)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "&Save Data"
        Me.toolTip1.SetToolTip(Me.btnSave, "Save all changes")
        '
        'pgProps
        '
        Me.pgProps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgProps.LineColor = System.Drawing.SystemColors.ScrollBar
        Me.pgProps.Location = New System.Drawing.Point(611, 12)
        Me.pgProps.Name = "pgProps"
        Me.pgProps.Size = New System.Drawing.Size(321, 404)
        Me.pgProps.TabIndex = 1
        '
        'btnAddDSRow
        '
        Me.btnAddDSRow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAddDSRow.Location = New System.Drawing.Point(280, 456)
        Me.btnAddDSRow.Name = "btnAddDSRow"
        Me.btnAddDSRow.Size = New System.Drawing.Size(104, 28)
        Me.btnAddDSRow.TabIndex = 10
        Me.btnAddDSRow.Text = "Add Row"
        Me.toolTip1.SetToolTip(Me.btnAddDSRow, "Add row directly to the data source")
        '
        'btnDelDSRow
        '
        Me.btnDelDSRow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelDSRow.Location = New System.Drawing.Point(572, 456)
        Me.btnDelDSRow.Name = "btnDelDSRow"
        Me.btnDelDSRow.Size = New System.Drawing.Size(104, 28)
        Me.btnDelDSRow.TabIndex = 13
        Me.btnDelDSRow.Text = "Del DS Row"
        Me.toolTip1.SetToolTip(Me.btnDelDSRow, "Delete row directly from data source")
        '
        'btnModRow
        '
        Me.btnModRow.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnModRow.Location = New System.Drawing.Point(682, 456)
        Me.btnModRow.Name = "btnModRow"
        Me.btnModRow.Size = New System.Drawing.Size(104, 28)
        Me.btnModRow.TabIndex = 14
        Me.btnModRow.Text = "Modify Row"
        Me.toolTip1.SetToolTip(Me.btnModRow, "Modify row directly in data source")
        '
        'btnGetValue
        '
        Me.btnGetValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGetValue.Location = New System.Drawing.Point(346, 422)
        Me.btnGetValue.Name = "btnGetValue"
        Me.btnGetValue.Size = New System.Drawing.Size(75, 28)
        Me.btnGetValue.TabIndex = 6
        Me.btnGetValue.Text = "&Get"
        Me.toolTip1.SetToolTip(Me.btnGetValue, "Get the specified column from the specified row")
        '
        'label1
        '
        Me.label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label1.Location = New System.Drawing.Point(392, 460)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(102, 23)
        Me.label1.TabIndex = 11
        Me.label1.Text = "Del/Mod Row"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'udcRowNumber
        '
        Me.udcRowNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.udcRowNumber.Location = New System.Drawing.Point(502, 460)
        Me.udcRowNumber.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.udcRowNumber.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.udcRowNumber.Name = "udcRowNumber"
        Me.udcRowNumber.Size = New System.Drawing.Size(64, 22)
        Me.udcRowNumber.TabIndex = 12
        Me.udcRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.udcRowNumber.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cboColumns
        '
        Me.cboColumns.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColumns.Items.AddRange(New Object() {"ID", "FirstName", "LastName", "Address", "City", "State", "Zip", "SumValue"})
        Me.cboColumns.Location = New System.Drawing.Point(99, 426)
        Me.cboColumns.Name = "cboColumns"
        Me.cboColumns.Size = New System.Drawing.Size(132, 24)
        Me.cboColumns.TabIndex = 3
        '
        'txtValue
        '
        Me.txtValue.AllowDrop = True
        Me.txtValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtValue.Location = New System.Drawing.Point(427, 425)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.ReadOnly = True
        Me.txtValue.Size = New System.Drawing.Size(288, 22)
        Me.txtValue.TabIndex = 7
        Me.txtValue.TabStop = False
        '
        'txtRowNumber
        '
        Me.txtRowNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRowNumber.Location = New System.Drawing.Point(283, 426)
        Me.txtRowNumber.Maximum = New Decimal(New Integer() {99999, 0, 0, 0})
        Me.txtRowNumber.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.txtRowNumber.Name = "txtRowNumber"
        Me.txtRowNumber.Size = New System.Drawing.Size(56, 22)
        Me.txtRowNumber.TabIndex = 5
        Me.txtRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtRowNumber.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'label6
        '
        Me.label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label6.Location = New System.Drawing.Point(235, 426)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(48, 23)
        Me.label6.TabIndex = 4
        Me.label6.Text = "at row"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'label5
        '
        Me.label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label5.Location = New System.Drawing.Point(13, 425)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(80, 23)
        Me.label5.TabIndex = 2
        Me.label5.Text = "G&et column"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkShowHeader
        '
        Me.chkShowHeader.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkShowHeader.Checked = True
        Me.chkShowHeader.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowHeader.Location = New System.Drawing.Point(723, 426)
        Me.chkShowHeader.Name = "chkShowHeader"
        Me.chkShowHeader.Size = New System.Drawing.Size(104, 24)
        Me.chkShowHeader.TabIndex = 15
        Me.chkShowHeader.Text = "Show header"
        '
        'chkShowFooter
        '
        Me.chkShowFooter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkShowFooter.Checked = True
        Me.chkShowFooter.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkShowFooter.Location = New System.Drawing.Point(835, 426)
        Me.chkShowFooter.Name = "chkShowFooter"
        Me.chkShowFooter.Size = New System.Drawing.Size(104, 24)
        Me.chkShowFooter.TabIndex = 16
        Me.chkShowFooter.Text = "Show footer"
        '
        'DataListTestForm
        '
        Me.ClientSize = New System.Drawing.Size(944, 496)
        Me.Controls.Add(Me.chkShowFooter)
        Me.Controls.Add(Me.chkShowHeader)
        Me.Controls.Add(Me.cboColumns)
        Me.Controls.Add(Me.btnGetValue)
        Me.Controls.Add(Me.txtValue)
        Me.Controls.Add(Me.txtRowNumber)
        Me.Controls.Add(Me.label6)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.udcRowNumber)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.btnModRow)
        Me.Controls.Add(Me.btnDelDSRow)
        Me.Controls.Add(Me.btnAddDSRow)
        Me.Controls.Add(Me.pgProps)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.dlList)
        Me.Controls.Add(Me.btnLoad)
        Me.MinimumSize = New System.Drawing.Size(795, 190)
        Me.Name = "DataListTestForm"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Test Data List Control"
        CType(Me.udcRowNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtRowNumber, System.ComponentModel.ISupportInitialize).EndInit()
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
