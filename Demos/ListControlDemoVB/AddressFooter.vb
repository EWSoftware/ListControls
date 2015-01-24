'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : AddressFooter.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 10/02/2014
' Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
' Compiler: Microsoft Visual C#
'
' This is a sample footer template control for the DataList demo.
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
Imports System.ComponentModel
Imports System.Data
Imports System.Windows.Forms

Imports EWSoftware.ListControls

Public Partial Class AddressFooter
    Inherits EWSoftware.ListControls.TemplateControl

    ' Used to track the current data source for totaling
    Private bl As IBindingList
    Private tblItems As DataTable

    Public Sub New()
        MyBase.New()

        ' Since there is only one instance of the header it is created when assigned so we don't need to delay
        ' initialization.
        InitializeComponent()
    End Sub

    ' We still need to override this even if there are no bound controls.  It's also the place to hook up event
    ' handlers if creating such things as footer totals, etc.
    Protected Overrides Sub Bind()
        ' The demo uses a data set so we'll get a reference to the table through the list manager
        Dim cm As CurrencyManager = Me.TemplateParent.ListManager
        Dim newSource As DataTable = CType(cm.List, DataView).Table

        ' Hook up the events on the data source to keep the total current
        If Not newSource.Equals(tblItems) Then
            ' Disconnect from the old source if necessary
            If Not (tblItems Is Nothing) Then
                RemoveHandler bl.ListChanged, AddressOf DataSource_ListChanged
                RemoveHandler tblItems.RowChanged, AddressOf DataSource_RowChgDel
                RemoveHandler tblItems.RowDeleted, AddressOf DataSource_RowChgDel
            End If

            tblItems = newSource

            If Not (tblItems Is Nothing) Then
                ' For the total, we'll sum it whenever a row is added, changed, or deleted
                bl = CType(cm.List, IBindingList)

                AddHandler bl.ListChanged, AddressOf DataSource_ListChanged
                AddHandler tblItems.RowChanged, AddressOf DataSource_RowChgDel
                AddHandler tblItems.RowDeleted, AddressOf DataSource_RowChgDel

                ' Show the initial total
                lblTotal.Text = tblItems.Compute("Sum(SumValue)", Nothing).ToString()
            Else
                lblTotal.Text = Nothing
            End If
        End If
    End Sub

    ' Update the total when a row is added
    Private Sub DataSource_ListChanged(sender As Object, e As ListChangedEventArgs)
        If e.ListChangedType = ListChangedType.ItemAdded Then
            lblTotal.Text = tblItems.Compute("Sum(SumValue)", Nothing).ToString()
        End If
    End Sub

    ' Update the total when a row is changed or deleted
    Private Sub DataSource_RowChgDel(sender As Object, e As DataRowChangeEventArgs)
        lblTotal.Text = tblItems.Compute("Sum(SumValue)", Nothing).ToString()
    End Sub
End Class
