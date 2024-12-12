'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : PhoneRow.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/08/2024
' Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
'
' This is a sample row template control for the DataList relationship demo
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/23/2005  EFW  Created the code
'================================================================================================================

Imports System.Text.RegularExpressions

Public Partial Class PhoneRow
    Inherits TemplateControl

    ' Constructor
	Public Sub New()
        MyBase.New()

        ' At runtime, actual initialization is deferred until needed
        If Me.DesignMode Then
	        InitializeComponent()
        End If
	End Sub

    ' This is overridden to confirm the deletion.   Returns True to allow the delete, False if not.
    Public Overrides ReadOnly Property CanDelete As Boolean
        Get
            If MessageBox.Show($"Are you sure you want to delete the phone number '{txtPhoneNumber.Text}'?",
              "Relationship Test", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) = DialogResult.No Then
                Return False
            End If

            Return True
        End Get
    End Property

    ' Actual initialization is deferred until needed to save time and resources
    Protected Overrides Sub InitializeTemplate()
        Me.InitializeComponent()
    End Sub

    ' Bind the controls to their data source
    Protected Overrides Sub Bind()
        Me.AddBinding(txtPhoneNumber, NameOf(Control.Text), NameOf(Phone.PhoneNumber))
    End Sub

    ' Require a phone number in a specific format
    Private Sub PhoneRow_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) _
        Handles MyBase.Validating

        epErrors.Clear()

        If txtPhoneNumber.Text.Trim().Length = 0 Then
            epErrors.SetError(txtPhoneNumber, "A phone number is required")
            e.Cancel = True
        Else
            If Not txtPhoneNumber.MaskCompleted Then
                epErrors.SetError(txtPhoneNumber, "Please enter a phone number in the format (###) ###-####")
                e.Cancel = True
            Else
                Me.CommitChanges()
            End If
        End If
    End Sub

    ' Delete this row
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnDelete.Click

        ' There appears to be a bug with the Button control.  The click event can fire if it's in another
        ' container even if validation prevents it getting the focus.  As such, ignore the click if we aren't
        ' focused.
        If Me.ContainsFocus Then
            Me.DeleteRow()
        End If
    End Sub

End Class
