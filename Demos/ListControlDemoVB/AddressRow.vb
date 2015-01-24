'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : AddressRow.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 10/02/2014
' Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
' Compiler: Microsoft Visual C#
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

Imports System
Imports System.Data
Imports System.Windows.Forms

Imports EWSoftware.ListControls

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
            If MessageBox.Show(String.Format("Are you sure you want to delete the name '{0} {1}'?",
              txtFName.Text, txtLName.Text), "Data List Test", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
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
        cboState.DataSource = CType(Me.TemplateParent.SharedDataSources("States"), DataView)

        ' Update control states based on the parent's change policy.  This can be omitted if you do not need it.
        Me.ChangePolicyModified()
    End Sub

    ' Bind the controls to their data source
    Protected Overrides Sub Bind()
        Me.AddBinding(txtFName, "Text", "FirstName")
        Me.AddBinding(txtLName, "Text", "LastName")
        Me.AddBinding(txtAddress, "Text", "Address")
        Me.AddBinding(txtCity, "Text", "City")
        Me.AddBinding(cboState, "SelectedValue", "State")
        Me.AddBinding(txtZip, "Text", "Zip")
        Me.AddBinding(udcSumValue, "Text", "SumValue")
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
