'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : PhoneRow.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 10/02/2014
' Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
' Compiler: Microsoft Visual C#
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

Imports System
Imports System.Data
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Imports EWSoftware.ListControls

Public Partial Class PhoneRow
    Inherits EWSoftware.ListControls.TemplateControl

    ' A simple edit for the phone number format
    Private Shared rePhone As New Regex("^\(\d{3}\) \d{3}-\d{4}$")

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
            If MessageBox.Show(String.Format("Are you sure you want to delete the phone number '{0}'?",
              txtPhoneNumber.Text), "Relationship Test", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
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
        Me.AddBinding(txtPhoneNumber, "Text", "PhoneNumber")
    End Sub

    ' Require a phone number in a specific format
    Private Sub PhoneRow_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) _
        Handles MyBase.Validating

        epErrors.Clear()

        If txtPhoneNumber.Text.Trim().Length = 0 Then
            epErrors.SetError(txtPhoneNumber, "A phone number is required")
            e.Cancel = True
        Else
            If rePhone.IsMatch(txtPhoneNumber.Text) = False Then
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
