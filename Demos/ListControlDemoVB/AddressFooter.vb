'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : AddressFooter.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/07/2024
' Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
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

Imports System.Globalization

Public Partial Class AddressFooter
    Inherits TemplateControl

    ' Used to track the current data source for totaling
    Private addresses As BindingList(Of Address)

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
        Dim newSource As BindingList(Of Address) = CType(cm.List, BindingList(Of Address))

        ' Hook up the events on the data source to keep the total current
        If newSource IsNot addresses Then
            ' Disconnect from the old source if necessary
            If addresses IsNot Nothing Then
                RemoveHandler addresses.ListChanged, AddressOf DataSource_ListChanged
            End If

            If newSource IsNot Nothing Then
                ' For the total, we'll sum it whenever a row is added, changed, or deleted
                addresses = newSource

                AddHandler addresses.ListChanged, AddressOf DataSource_ListChanged

                ' Show the initial total
                Me.DataSource_ListChanged(Me, New ListChangedEventArgs(ListChangedType.Reset, 0))
            Else
                lblTotal.Text = Nothing
            End If
        End If
    End Sub

    ' Update the total when a row is added
    Private Sub DataSource_ListChanged(sender As Object, e As ListChangedEventArgs)
        If e.ListChangedType = ListChangedType.ItemAdded Then
            lblTotal.Text = addresses.Sum(Function(a) If(a.SumValue, 0)).ToString(CultureInfo.InvariantCulture)
        End If
    End Sub

End Class
