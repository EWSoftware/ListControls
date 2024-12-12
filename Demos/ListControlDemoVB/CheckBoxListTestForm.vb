'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : CheckBoxListTestForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/02/2024
' Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
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

Public Partial Class CheckBoxListTestForm
    Inherits Form

    Private ReadOnly demoData As List(Of DemoTable)
    Private ReadOnly productInfo As List(Of ProductInfo)

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        Try
            Using dc As New DemoDataContext()
                demoData = dc.DemoTable.OrderBy(Function (d) d.Label).ToList()
                productInfo = dc.ProductInfo.OrderBy(Function (p) p.ProductName).ToList()
            End Using

        Catch ex As SqlException
            MessageBox.Show(ex.Message)

        End Try

        cboDataSource.SelectedIndex = 0

        pgProps.SelectedObject = cblDemo
        pgProps.Refresh()

    End Sub

    ' Refresh the display and the checkbox list settings after they have changed
    Private Sub pgProps_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) _
      Handles pgProps.PropertyValueChanged
        cblDemo.Invalidate()
        cblDemo.Update()
    End Sub

    ' Change the data source for the checkbox list
    Private Sub cboDataSource_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles cboDataSource.SelectedIndexChanged

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
        If demoData.Count = 0 Or productInfo.Count = 0 Then
            MessageBox.Show("Database not found.  It must be located in the demo project's folder.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Stop)

            cblDemo.EndInit()
            Return
        End If

        ' Data tables, views, and sets are also supported but we won't demonstrate that here
        Select Case cboDataSource.SelectedIndex
            Case 0      ' Demo data (List(Of DemoData))
                cblDemo.DisplayMember = nameof(DemoTable.Label)
                cblDemo.ValueMember = nameof(DemoTable.ListKey)
                cblDemo.DataSource = demoData

            Case 1      ' Product info (List(Of ProductInfo))
                cblDemo.DisplayMember = nameof(DataBase.ProductInfo.ProductName)
                cblDemo.ValueMember = nameof(Database.ProductInfo.ProductID)
                cblDemo.DataSource = productInfo

            Case 2      ' Array List
                Dim al As New ArrayList(100)

                For Each p In productInfo
                    al.Add(New ListItem(p.ProductID, p.ProductName))
                Next

                cblDemo.DisplayMember = nameof(ListItem.Display)
                cblDemo.ValueMember = nameof(ListItem.Value)
                cblDemo.DataSource = al

            Case 3      ' Item collection strings
                ' Like the above but we add the strings directly to the radio button list's Items collection
                For Each p In productInfo
                    cblDemo.Items.Add(p.ProductName)
                Next

                ' The item collection is the data source for this one.  It's a simple string list so there are no
                ' display or value members.

        End Select

        ' Resume updates to the radio button list and display the new set of radio buttons
        cblDemo.EndInit()

        ' Load the column names
        If Not (cblDemo.DataSource Is Nothing) Then
            If TypeOf(cblDemo.DataSource) Is ArrayList Then
                cboColumns.Items.Add(nameof(ListItem.Display))
                cboColumns.Items.Add(nameof(ListItem.Value))
            Else
                Dim dataSourceType As Type

                If cboDataSource.SelectedIndex = 0
                    dataSourceType = GetType(DemoTable)
                Else
                    dataSourceType = GetType(ProductInfo)
                End If

                For Each p in dataSourceType.GetProperties()
                    cboColumns.Items.Add(p.Name)
                Next
            End If
        End If

        If cboColumns.Items.Count = 0 Then
            cboColumns.Enabled = False
            udcRowNumber.Enabled = False
            btnGetValue.Enabled = False
        Else
            cboColumns.Enabled = True
            udcRowNumber.Enabled = True
            btnGetValue.Enabled = True
        End If
    End Sub

    ' Get a value from the checkbox list
    Private Sub btnGetValue_Click(sender As Object, e As EventArgs) _
      Handles btnGetValue.Click
        ' This can be any column from the data source regardless of whether or not it is displayed.  Note that
        ' you can also use cblDemo("ColName") to get a column value from the item indicated by the SelectedIndex
        ' property.
        txtValue.Text = $"{cboColumns.Text} = {cblDemo(CType(udcRowNumber.Value, Integer), cboColumns.Text)}"
    End Sub

    ' Show the current item info when the selected index changes.  For the checkbox list, this happens whenever a
    ' new checkbox item gains the focus.
    Private Sub cblDemo_SelectedIndexChanged(sender As Object, e As EventArgs) _
      Handles cblDemo.SelectedIndexChanged
        ' Note that SelectedValue is only valid if there is a data source
        txtValue.Text = $"Index = {cblDemo.SelectedIndex}, Value = {cblDemo.SelectedValue}, Text = {cblDemo.Text}"
    End Sub

    ' When the check state of a checkbox in the list changes, this event is raised
    Private Sub cblDemo_ItemCheckStateChanged(sender As Object, e As EWSoftware.ListControls.ItemCheckStateEventArgs) _
      Handles cblDemo.ItemCheckStateChanged
        txtValue.Text = $"Index = {e.Index}, Current State = {e.CheckState}"
    End Sub

    ' Use or clear the image list
    Private Sub chkShowImages_CheckedChanged(sender As Object, e As EventArgs) _
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
    Private Sub btnCheckedItems_Click(sender As Object, e As EventArgs) _
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
    Private Sub btnCheckedItemsText_Click(ByVal sender As System.Object, ByVal e As EventArgs) _
        Handles btnCheckedItemsText.Click

        Dim items As CheckedItemsCollection = cblDemo.CheckedItems

        If items.Count = 0 Then
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
    Private Sub btnCheckedIndices_Click(sender As Object, e As EventArgs) _
      Handles btnCheckedIndices.Click
        Dim indices As CheckedIndicesCollection = cblDemo.CheckedIndices

        If indices.Count = 0 Then
            MessageBox.Show("No items are currently checked")
        Else
            MessageBox.Show("The indices of the currently checked items are:" & Environment.NewLine & indices.ToString())
        End If
    End Sub

End Class
