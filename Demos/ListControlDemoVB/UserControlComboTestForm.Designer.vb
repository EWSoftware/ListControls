<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UserControlComboTestForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UserControlComboTestForm))
        Me.cboAutoComp = New EWSoftware.ListControls.AutoCompleteComboBox
        Me.label2 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.cboUCCombo = New EWSoftware.ListControls.UserControlComboBox
        Me.pgProps = New System.Windows.Forms.PropertyGrid
        Me.grpOptions = New System.Windows.Forms.GroupBox
        Me.cboColumns = New System.Windows.Forms.ComboBox
        Me.label6 = New System.Windows.Forms.Label
        Me.btnGetValue = New System.Windows.Forms.Button
        Me.txtValue = New System.Windows.Forms.TextBox
        Me.txtRowNumber = New System.Windows.Forms.NumericUpDown
        Me.label5 = New System.Windows.Forms.Label
        Me.cboDataSource = New System.Windows.Forms.ComboBox
        Me.label1 = New System.Windows.Forms.Label
        Me.ilImages = New System.Windows.Forms.ImageList(Me.components)
        CType(Me.cboUCCombo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpOptions.SuspendLayout()
        CType(Me.txtRowNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cboAutoComp
        '
        Me.cboAutoComp.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboAutoComp.Location = New System.Drawing.Point(496, 16)
        Me.cboAutoComp.Name = "cboAutoComp"
        Me.cboAutoComp.Size = New System.Drawing.Size(323, 24)
        Me.cboAutoComp.TabIndex = 1
        '
        'label2
        '
        Me.label2.Location = New System.Drawing.Point(379, 16)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(111, 23)
        Me.label2.TabIndex = 0
        Me.label2.Text = "&Auto-Complete"
        Me.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'label3
        '
        Me.label3.Location = New System.Drawing.Point(376, 184)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(114, 23)
        Me.label3.TabIndex = 2
        Me.label3.Text = "&User Ctl Combo"
        Me.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboUCCombo
        '
        Me.cboUCCombo.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboUCCombo.Location = New System.Drawing.Point(496, 184)
        Me.cboUCCombo.Name = "cboUCCombo"
        Me.cboUCCombo.Size = New System.Drawing.Size(323, 24)
        Me.cboUCCombo.TabIndex = 3
        '
        'pgProps
        '
        Me.pgProps.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pgProps.LineColor = System.Drawing.SystemColors.ScrollBar
        Me.pgProps.Location = New System.Drawing.Point(6, 0)
        Me.pgProps.Name = "pgProps"
        Me.pgProps.Size = New System.Drawing.Size(368, 467)
        Me.pgProps.TabIndex = 5
        '
        'grpOptions
        '
        Me.grpOptions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpOptions.Controls.Add(Me.cboColumns)
        Me.grpOptions.Controls.Add(Me.label6)
        Me.grpOptions.Controls.Add(Me.btnGetValue)
        Me.grpOptions.Controls.Add(Me.txtValue)
        Me.grpOptions.Controls.Add(Me.txtRowNumber)
        Me.grpOptions.Controls.Add(Me.label5)
        Me.grpOptions.Controls.Add(Me.cboDataSource)
        Me.grpOptions.Controls.Add(Me.label1)
        Me.grpOptions.Location = New System.Drawing.Point(384, 344)
        Me.grpOptions.Name = "grpOptions"
        Me.grpOptions.Size = New System.Drawing.Size(435, 120)
        Me.grpOptions.TabIndex = 4
        Me.grpOptions.TabStop = False
        Me.grpOptions.Text = "Options"
        '
        'cboColumns
        '
        Me.cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColumns.Location = New System.Drawing.Point(104, 59)
        Me.cboColumns.Name = "cboColumns"
        Me.cboColumns.Size = New System.Drawing.Size(132, 24)
        Me.cboColumns.TabIndex = 3
        '
        'label6
        '
        Me.label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label6.Location = New System.Drawing.Point(239, 59)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(48, 23)
        Me.label6.TabIndex = 4
        Me.label6.Text = "at row"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnGetValue
        '
        Me.btnGetValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGetValue.Location = New System.Drawing.Point(350, 55)
        Me.btnGetValue.Name = "btnGetValue"
        Me.btnGetValue.Size = New System.Drawing.Size(75, 28)
        Me.btnGetValue.TabIndex = 6
        Me.btnGetValue.Text = "&Get"
        '
        'txtValue
        '
        Me.txtValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtValue.Location = New System.Drawing.Point(16, 91)
        Me.txtValue.Name = "txtValue"
        Me.txtValue.ReadOnly = True
        Me.txtValue.Size = New System.Drawing.Size(411, 22)
        Me.txtValue.TabIndex = 7
        Me.txtValue.TabStop = False
        '
        'txtRowNumber
        '
        Me.txtRowNumber.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtRowNumber.Location = New System.Drawing.Point(288, 59)
        Me.txtRowNumber.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.txtRowNumber.Name = "txtRowNumber"
        Me.txtRowNumber.Size = New System.Drawing.Size(56, 22)
        Me.txtRowNumber.TabIndex = 5
        Me.txtRowNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'label5
        '
        Me.label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label5.Location = New System.Drawing.Point(6, 58)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(92, 23)
        Me.label5.TabIndex = 2
        Me.label5.Text = "G&et column"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboDataSource
        '
        Me.cboDataSource.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cboDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDataSource.Items.AddRange(New Object() {"Data Table", "Data View", "Data Set", "Array List", "Strings"})
        Me.cboDataSource.Location = New System.Drawing.Point(104, 27)
        Me.cboDataSource.Name = "cboDataSource"
        Me.cboDataSource.Size = New System.Drawing.Size(132, 24)
        Me.cboDataSource.TabIndex = 1
        '
        'label1
        '
        Me.label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.label1.Location = New System.Drawing.Point(10, 27)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(88, 23)
        Me.label1.TabIndex = 0
        Me.label1.Text = "&Data Source"
        Me.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ilImages
        '
        Me.ilImages.ImageStream = CType(resources.GetObject("ilImages.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ilImages.TransparentColor = System.Drawing.Color.Lime
        Me.ilImages.Images.SetKeyName(0, "")
        Me.ilImages.Images.SetKeyName(1, "")
        Me.ilImages.Images.SetKeyName(2, "")
        Me.ilImages.Images.SetKeyName(3, "")
        Me.ilImages.Images.SetKeyName(4, "")
        '
        'UserControlComboTestForm
        '
        Me.ClientSize = New System.Drawing.Size(827, 484)
        Me.Controls.Add(Me.grpOptions)
        Me.Controls.Add(Me.pgProps)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.cboUCCombo)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.cboAutoComp)
        Me.MinimumSize = New System.Drawing.Size(835, 512)
        Me.Name = "UserControlComboTestForm"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AutoComplete/User Control Combo Test"
        CType(Me.cboUCCombo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpOptions.ResumeLayout(False)
        Me.grpOptions.PerformLayout()
        CType(Me.txtRowNumber, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents txtRowNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents txtValue As System.Windows.Forms.TextBox
    Friend WithEvents btnGetValue As System.Windows.Forms.Button
    Friend WithEvents cboColumns As System.Windows.Forms.ComboBox
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents ilImages As System.Windows.Forms.ImageList
    Friend WithEvents cboUCCombo As EWSoftware.ListControls.UserControlComboBox

End Class
