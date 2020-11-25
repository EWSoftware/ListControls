'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : CheckBoxListTestForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 10/02/2014
' Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
' Compiler: Microsoft Visual Basic .NET
'
' This is used to demonstrate the CheckBoxList control
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 10/27/2005  EFW  Created the code
'================================================================================================================

Imports System
Imports System.Collections
Imports System.Data
Imports System.Data.OleDb
Imports System.Windows.Forms

Imports EWSoftware.ListControls

Public Partial Class CheckBoxListTestForm
    Inherits System.Windows.Forms.Form

    Private demoData, productData As DataSet

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        demoData = New DataSet()
        productData = New DataSet()

        Try
            Using dbConn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\TestData.mdb")
                ' Load some data for the demo
                Dim cmd As New OleDbCommand("Select * From DemoTable Order By Label", dbConn)
                cmd.CommandType = CommandType.Text
                Dim adapter As New OleDbDataAdapter(cmd)

                adapter.Fill(demoData)

                ' Use a named table for this one
                adapter.TableMappings.Add("Table", "ProductInfo")
                cmd.CommandText = "Select * From ProductInfo Order By ProductName"
                adapter.Fill(productData)
            End Using

        Catch ex As OleDbException
            MessageBox.Show(ex.Message)

        End Try

        cboDataSource.SelectedIndex = 0

        pgProps.SelectedObject = cblDemo
        pgProps.Refresh()

    End Sub

    ' Refresh the display and the checkbox list settings after they have changed
    Private Sub pgProps_PropertyValueChanged(s As Object, e As System.Windows.Forms.PropertyValueChangedEventArgs) _
      Handles pgProps.PropertyValueChanged
        cblDemo.Invalidate()
        cblDemo.Update()
    End Sub

    ' Change the data source for the checkbox list
    Private Sub cboDataSource_SelectedIndexChanged(sender As Object, e As System.EventArgs) _
        Handles cboDataSource.SelectedIndexChanged

        Dim dr As DataRow, c As DataColumn

        ' Suspend updates to the checkbox list to speed it up and prevent flickering
        cblDemo.BeginInit()

        ' Clear out the prior definitions
        cboColumns.Items.Clear()
        cboColumns.SelectedIndex = -1

        cblDemo.DataSource = Nothing
        cblDemo.DisplayMember = String.Empty
        cblDemo.ValueMember = String.Empty
        cblDemo.Items.Clear()

        ' We must have the data
        If demoData.Tables.Count = 0 Or productData.Tables.Count = 0 Then
            MessageBox.Show("Database not found.  It must be located in the demo project's folder.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Stop)

            cblDemo.EndInit()
            Return
        End If

        Select Case cboDataSource.SelectedIndex
            Case 0      ' Data Table
                cblDemo.DisplayMember = "Label"
                cblDemo.ValueMember = "ListKey"
                cblDemo.DataSource = demoData.Tables(0)

            Case 1      ' Data View
                cblDemo.DisplayMember = "Label"
                cblDemo.ValueMember = "ListKey"
                cblDemo.DataSource = demoData.Tables(0).DefaultView

            Case 2      ' Data Set
                ' Use a named table for this one
                cblDemo.DisplayMember = "ProductInfo.ProductName"
                cblDemo.ValueMember = "ProductInfo.ProductID"
                cblDemo.DataSource = productData

            Case 3      ' Array List
                Dim al As New ArrayList(100)

                For Each dr In productData.Tables(0).Rows
                    al.Add(New ListItem(dr("ProductID"), CType(dr("ProductName"), String)))
                Next

                cblDemo.DisplayMember = "Display"   ' ListItem description
                cblDemo.ValueMember = "Value"       ' ListItem value
                cblDemo.DataSource = al

            Case 4      ' Item collection strings
                ' Like the above but we add the strings directly to the checkbox list's Items collection
                For Each dr In productData.Tables(0).Rows
                    cblDemo.Items.Add(dr("ProductName"))
                Next

                ' The item collection is the data source for this one.  It's a simple string list so there are
                ' no display or value members.

        End Select

        ' Resume updates to the checkbox list and display the new set of checkboxes
        cblDemo.EndInit()

        ' Load the column names
        If Not (cblDemo.DataSource Is Nothing) Then
            If TypeOf(cblDemo.DataSource) Is ArrayList Then
                cboColumns.Items.Add("Display")
                cboColumns.Items.Add("Value")
            Else
                Dim tbl As DataTable

                If TypeOf(cblDemo.DataSource) Is DataSet Then
                    tbl = productData.Tables(0)
                Else
                    tbl = demoData.Tables(0)
                End If

                For Each c In tbl.Columns
                    cboColumns.Items.Add(c.ColumnName)
                Next
            End If
        End If

        If cboColumns.Items.Count = 0 Then
            cboColumns.Enabled = False
            txtRowNumber.Enabled = False
            btnGetValue.Enabled = False
        Else
            cboColumns.Enabled = True
            txtRowNumber.Enabled = True
            btnGetValue.Enabled = True
        End If
    End Sub

    ' Get a value from the checkbox list
    Private Sub btnGetValue_Click(sender As Object, e As System.EventArgs) _
      Handles btnGetValue.Click
        ' This can be any column from the data source regardless of whether or not it is displayed.  Note that
        ' you can also use cblDemo("ColName") to get a column value from the item indicated by the SelectedIndex
        ' property.
        txtValue.Text = String.Format("{0} = {1}", cboColumns.Text, cblDemo(CType(txtRowNumber.Value, Integer),
            cboColumns.Text))
    End Sub

    ' Show the current item info when the selected index changes.  For the checkbox list, this happens whenever a
    ' new checkbox item gains the focus.
    Private Sub cblDemo_SelectedIndexChanged(sender As Object, e As System.EventArgs) _
      Handles cblDemo.SelectedIndexChanged
        ' Note that SelectedValue is only valid if there is a data source
        txtValue.Text = String.Format("Index = {0}, Value = {1}, Text = {2}", cblDemo.SelectedIndex,
            cblDemo.SelectedValue, cblDemo.Text)
    End Sub

    ' When the check state of a checkbox in the list changes, this event is raised
    Private Sub cblDemo_ItemCheckStateChanged(sender As Object, e As EWSoftware.ListControls.ItemCheckStateEventArgs) _
      Handles cblDemo.ItemCheckStateChanged
        txtValue.Text = String.Format("Index = {0}, Current State = {1}", e.Index, e.CheckState)
    End Sub

    ' Use or clear the image list
    Private Sub chkShowImages_CheckedChanged(sender As Object, e As System.EventArgs) _
      Handles chkShowImages.CheckedChanged
        If chkShowImages.Checked = True Then
            ' The image list is not usable with themed checkboxes under .NET 1.1
            If cblDemo.FlatStyle = FlatStyle.System Then
                cblDemo.FlatStyle = FlatStyle.Standard
            End If

            cblDemo.ImageList = ilImages
        Else
            cblDemo.ImageList = Nothing
        End If
    End Sub

    ' Get the currently checked item values
    Private Sub btnCheckedItems_Click(sender As Object, e As System.EventArgs) _
      Handles btnCheckedItems.Click
        Dim items As CheckedItemsCollection = cblDemo.CheckedItems

        If items.Count = 0 Then
            MessageBox.Show("No items are currently checked")
        Else
            ' The values of individual items can be retrieved using the ValueOf() method on the collection.  The
            ' check state of the items can be retrieved using the CheckStateOf() method.  The ToString() method
            ' returns the values as a comma-separated list.
            MessageBox.Show("The values of the currently checked items are:" & Environment.NewLine & items.ToString())
        End If
    End Sub

    ' Get the currently checked item display text values
    Private Sub btnCheckedItemsText_Click( ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles btnCheckedItemsText.Click

        Dim items As CheckedItemsCollection = cblDemo.CheckedItems

        If items.Count =0 Then
            MessageBox.Show("No items are currently checked")
        Else
            ' The display text of individual items can be retrieved using the DisplayTextOf() method on the
            ' collection.  The check state of the items can be retrieved using the CheckStateOf() method.  The
            ' ToDisplayTextString() method returns the display text of the checked items as a comma-separated
            ' list.
            MessageBox.Show("The display text of the currently checked items are:" & Environment.NewLine &
                items.ToDisplayTextString())
        End If
    End Sub

    ' Get the indices of the currently checked items
    Private Sub btnCheckedIndices_Click(sender As Object, e As System.EventArgs) _
      Handles btnCheckedIndices.Click
        Dim indices As CheckedIndicesCollection = cblDemo.CheckedIndices

        If indices.Count = 0 Then
            MessageBox.Show("No items are currently checked")
        Else
            MessageBox.Show("The indices of the currently checked items are:" & Environment.NewLine & indices.ToString())
        End If
    End Sub

End Class
