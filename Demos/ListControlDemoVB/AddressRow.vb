'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : AddressRow.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/07/2024
' Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
'
' This is a sample row template control for the DataList demo
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 10/29/2005  EFW  Created the code
'================================================================================================================

Public Partial Class AddressRow
    Inherits EWSoftware.ListControls.TemplateControl

    Public Sub New()
        MyBase.New()

        ' At runtime, actual initialization is deferred until needed.
        If Me.DesignMode Then
            InitializeComponent()
        End If
    End Sub

    ' This is overridden to confirm the deletion.  Returns True to allow the delete, False if not.
    Public Overrides ReadOnly Property CanDelete As Boolean
        Get
            If MessageBox.Show($"Are you sure you want to delete the name '{txtFName.Text} {txtLName.Text}'?",
              "Data List Test", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Return False
            End If

            Return True
        End Get
    End Property

    ' Actual initialization is deferred until needed to save time and resources
    Protected Overrides Sub InitializeTemplate()
        ' Create the contained controls
        Me.InitializeComponent()

        ' Use the shared data source for the combo box
        cboState.DisplayMember = "State"
        cboState.ValueMember = "State"
        cboState.DataSource = CType(Me.TemplateParent.SharedDataSources("States"), List(Of StateCode))

        ' Update control states based on the parent's change policy.  This can be omitted if you do not need it.
        Me.ChangePolicyModified()
    End Sub

    ' Bind the controls to their data source
    Protected Overrides Sub Bind()
        Me.AddBinding(txtFName, nameof(Control.Text), nameof(Address.FirstName))
        Me.AddBinding(txtLName, nameof(Control.Text), nameof(Address.LastName))
        Me.AddBinding(txtAddress, nameof(Control.Text), nameof(Address.StreetAddress))
        Me.AddBinding(txtCity, nameof(Control.Text), nameof(Address.City))
        Me.AddBinding(txtZip, nameof(Control.Text), nameof(Address.Zip))

        ' We must enable formatting or the bound values for these control types won't get updated for
        ' some reason.  The sum value is also nullable so it needs a format event handler.
        Me.AddBinding(cboState, nameof(MultiColumnComboBox.SelectedValue), nameof(Address.State)).FormattingEnabled = True
        Me.AddBinding(udcSumValue, NameOf(NumericUpDown.Value), NameOf(Address.SumValue), True,
            Sub(s, e) e.Value = If(e.Value, 0)).FormattingEnabled = True

        ' They also need to update on the property value changing in case the mouse wheel is used
        udcSumValue.DataBindings(0).DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        cboState.DataBindings(0).DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
    End Sub

    ' Enable or disable the controls based on the edit policy
    Protected Overrides Sub ChangePolicyModified()
        Dim allowEdits As Boolean = Me.TemplateParent.AllowEdits

        If allowEdits <> txtFName.Enabled And Me.IsNewRow = False Then
            txtFName.Enabled = allowEdits
            txtLName.Enabled = allowEdits
            txtAddress.Enabled = allowEdits
            txtCity.Enabled = allowEdits
            cboState.Enabled = allowEdits
            txtZip.Enabled = allowEdits
            udcSumValue.Enabled = allowEdits
        End If
    End Sub

    ' Require a first and last name
    Private Sub AddressRow_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) _
        Handles MyBase.Validating

        epErrors.Clear()

        If txtFName.Text.Trim().Length = 0 Then
            epErrors.SetError(txtFName, "A first name is required")
            e.Cancel = True
        End If

        If txtLName.Text.Trim().Length = 0 Then
            epErrors.SetError(txtLName, "A last name is required")
            e.Cancel = True
        End If
    End Sub

End Class
