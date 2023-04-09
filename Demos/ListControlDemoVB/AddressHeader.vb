'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : AddressHeader.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 10/02/2014
' Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
'
' This is a sample header template control for the DataList demo
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
Imports System.Windows.Forms

Imports EWSoftware.ListControls

Public Partial Class AddressHeader
    Inherits EWSoftware.ListControls.TemplateControl

    Private lastSearch As String

    Public Sub New()
        MyBase.New()

        ' Since there is only one instance of the header it is created when assigned so we don't need to delay
        ' initialization.
        InitializeComponent()

        lastSearch = String.Empty
    End Sub


    ' Bind the ID label to the field in the data source
    Protected Overrides Sub Bind()
        ' If the DataList uses a DataSet you must use the fully qualified field name in header and footer
        ' controls as they are bound to the data source as a whole.
        Me.AddBinding(lblKey, "Text", "Addresses.ID")
    End Sub

    ' Find an entry by last name (incremental search)
    Private Sub txtFindName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles txtFindName.TextChanged

        Dim startRow, row As Integer

        If txtFindName.Text.Length > 0 Then
            If txtFindName.Text.Length <= lastSearch.Length Then
                startRow = -1
            Else
                startRow = Me.TemplateParent.CurrentRow - 1
            End If

            lastSearch = txtFindName.Text

            row = Me.TemplateParent.FindString("LastName", txtFindName.Text, startRow)

            If row <> -1 Then
                Me.TemplateParent.MoveTo(row)
                txtFindName.Focus()
            End If
        End If
    End Sub
End Class
