'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : DataListTestForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/07/2024
' Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
'
' This is used to demonstrate the DataList and TemplateControl controls
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

Imports System.Text

Partial Public Class DataListTestForm
    Inherits Form

    Private dc As DemoDataContext

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        dc = new DemoDataContext()

        ' Set the data list as the object for the property grid
        pgProps.SelectedObject = dlList
        pgProps.Refresh()

        ' Load data by default
        btnLoad_Click(Me, EventArgs.Empty)
    End Sub


    ' Change the color of the row based on the zip code when a row is data bound
    Private Sub dlList_ItemDataBound(sender As Object, e As DataListEventArgs) _
      Handles dlList.ItemDataBound
        If dlList(e.Index, NameOf(Address.Zip))?.ToString() = "98122" Then
            e.Item.BackColor = Color.LightSteelBlue
        Else
            e.Item.BackColor = SystemColors.Control
        End If
    End Sub

    ' Refresh the display and the data list settings after they have changed
    Private Sub pgProps_PropertyValueChanged(s As Object, e As PropertyValueChangedEventArgs) _
      Handles pgProps.PropertyValueChanged
        dlList.Invalidate()
        dlList.Update()
    End Sub

    ' Prompt to save changes if necessary
    Private Sub DataListTestForm_Closing(sender As Object, e As CancelEventArgs) _
        Handles MyBase.Closing

        Dim dr As DialogResult

        If dc.ChangeTracker.HasChanges() Then
            dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your changes, NO " &
                "to discard them, or CANCEL to stay here and make further changes.", "DataList Test",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3)

            If dr = System.Windows.Forms.DialogResult.Cancel Then
                e.Cancel = True
            Else
                If dr = System.Windows.Forms.DialogResult.Yes Then
                    btnSave_Click(sender, e)

                    ' If it didn't work, stay here
                    If dc.ChangeTracker.HasChanges() Then
                        e.Cancel = True
                    End If
                End If
            End If
        End If
    End Sub

    ' Load the data into the data list control
    Private Sub btnLoad_Click(sender As Object, e As EventArgs) _
      Handles btnLoad.Click
        Dim dr As DialogResult

        Try
            If dc.ChangeTracker.HasChanges() Then
                dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your changes, " &
                    "NO to discard them, or CANCEL to stay here and make further changes.", "DataList Test",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3)

                If dr = System.Windows.Forms.DialogResult.Cancel Then
                    Return
                End If

                If dr = System.Windows.Forms.DialogResult.Yes Then
                    btnSave_Click(sender, e)

                    ' If it didn't work, don't do anything
                    If dc.ChangeTracker.HasChanges() Then
                        Return
                    End If
                End If
            End If

            ' Reload the data.  Create a new data context since we're reloading the information.  The
            ' old context may have changes we no longer care about.
            dc = new DemoDataContext()

            If Not dlList.SharedDataSources.ContainsKey("States") Then
                Dim states As List(Of StateCode) = dc.StateCodes.ToList()

                states.Insert(0, New StateCode With { .State = String.Empty, .StateDesc = String.Empty })

                dlList.SharedDataSources.Add("States", states)
            End If

            ' For entity framework we need to load the entities
            dc.Addresses.Load()

            ' Apply a sort by last name
            Dim pdc As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(Address))
            Dim pd As PropertyDescriptor = pdc(NameOf(Address.LastName))
            Dim dataSource As BindingList(Of Address) = dc.Addresses.Local.ToObservableCollection().ToBindingList()

            CType(dataSource, IBindingList).ApplySort(pd, ListSortDirection.Ascending)

            ' We could set each binding property individually, but this is more efficient
            dlList.SetDataBinding(dataSource, Nothing, GetType(AddressRow), GetType(AddressHeader),
                GetType(AddressFooter))

        Catch ex As Exception
            MessageBox.Show(Me, ex.Message)
        End Try

        dlList.Focus()
    End Sub

    ' Save the changes
    Private Sub btnSave_Click(sender As Object, e As EventArgs) _
      Handles btnSave.Click
        If dlList.DataSource Is Nothing Then
            btnLoad_Click(sender, e)
        Else
            If dlList.IsValid = True Then
                ' We must commit any pending changes
                dlList.CommitChanges()

                ' There may be a row added for the placeholder that needs removing
                Dim removeRows As List(Of Address) = dc.ChangeTracker.Entries(Of Address)().Select(Function (a) a.Entity).Where(
                    Function (a) a.LastName Is Nothing).ToList()

                If removeRows.Count <> 0 Then
                    dc.RemoveRange(removeRows)
                End If

                dc.SaveChanges()
            End If
        End If
    End Sub

    ' Add a row outside of the data list to test its ability to detect and add a new row added in this manner
    Private Sub btnAddDSRow_Click(sender As Object, e As EventArgs) _
      Handles btnAddDSRow.Click
        If dlList.DataSource Is Nothing Then
            btnLoad_Click(sender, e)
        Else
            ' Commit any changes to the current row in case it's the new row.  It's temporary until committed and
            ' adding a new row here puts this one ahead of it.
            dlList.CommitChanges()

            Dim bl As BindingList(Of Address) = dlList.DataSource

            bl.Add(New Address With
            {
                .FirstName = "External",
                .LastName = "Row",
                .LastModified = BitConverter.GetBytes(DateTime.Now.Ticks)
            })

            dlList.MoveTo(RowPosition.LastRow)
            dlList.Focus()
        End If
    End Sub

    ' Delete a row outside of the data list to test its ability to detect a row deleted in this manner
    Private Sub btnDelDSRow_Click(sender As Object, e As EventArgs) _
      Handles btnDelDSRow.Click
        If dlList.DataSource Is Nothing Then
            btnLoad_Click(sender, e)
        Else
            ' Use the currency manager as the data set's row collection may have been changed and the indexes
            ' won't match up to the actual rows.
            Dim cm As CurrencyManager = dlList.ListManager

            Dim row As Integer = CType(udcRowNumber.Value, Integer)

            If row > 0 And row <= cm.Count Then
                dlList.CancelChanges()

                Dim bl As BindingList(Of Address) = dlList.DataSource

                bl.RemoveAt(row - 1)
            Else
                MessageBox.Show("Not a valid row number")
            End If
        End If
    End Sub

    ' Modify a row outside of the data list to test its ability to detect a row changed in this manner
    Private Sub btnModRow_Click(sender As Object, e As EventArgs) _
      Handles btnModRow.Click
        If dlList.DataSource Is Nothing Then
            btnLoad_Click(sender, e)
        Else
            ' Commit any other pending changes
            dlList.CommitChanges()

            ' Use the currency manager as the data set's row collection may have been changed and the indexes
            ' won't match up to the actual rows.
            Dim cm As CurrencyManager = dlList.ListManager

            Dim row As Integer = CType(udcRowNumber.Value, Integer)

            If row > 0 And row <= cm.Count Then
                Dim address As Address = cm.List(row - 1)
                address.StreetAddress = "Modified externally"

                dlList.MoveTo(row - 1)
                dlList.Focus()
            Else
                MessageBox.Show("Not a valid row number")
            End If
        End If
    End Sub

    ' Get a value from the data list
    Private Sub btnGetValue_Click(sender As Object, e As System.EventArgs) _
      Handles btnGetValue.Click
        ' This can be any column from the data source regardless of whether or not it is displayed.  Note that
        ' you can also use dlList("ColName") to get a column value from the item indicated by the CurrentRow
        ' property.
        txtValue.Text = $"{cboColumns.Text} = {dlList(CType(txtRowNumber.Value - 1, Integer), cboColumns.Text)}"
    End Sub

    ' Show or hide the data list's header
    Private Sub chkShowHeader_CheckedChanged(sender As Object, e As EventArgs) _
        Handles chkShowHeader.CheckedChanged

        If chkShowHeader.Checked = True Then
            dlList.HeaderTemplate = GetType(AddressHeader)
        Else
            dlList.HeaderTemplate = Nothing
        End If
    End Sub

    ' Show or hide the data list's footer
    Private Sub chkShowFooter_CheckedChanged(sender As Object, e As EventArgs) _
        Handles chkShowFooter.CheckedChanged

        If chkShowFooter.Checked = True Then
            dlList.FooterTemplate = GetType(AddressFooter)
        Else
            dlList.FooterTemplate = Nothing
        End If
    End Sub

    ' Test drag and drop with the data list.
    Private Sub dlList_BeginDrag(sender As Object, e As DataListBeginDragEventArgs) _
        Handles dlList.BeginDrag
        ' Commit any pending changes before the drag operation begins
        dlList.CommitChanges()

        Me.DoDragDrop(e, DragDropEffects.Copy Or DragDropEffects.Move)
    End Sub

    ' Allow drag and drop from data list
    Private Sub txtValue_DragEnter(sender As Object, e As DragEventArgs) _
        Handles txtValue.DragEnter

        If e.Data.GetDataPresent(GetType(DataListBeginDragEventArgs)) = True Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    ' Handle drop from data list
    Private Sub txtValue_DragDrop(sender As Object, e As DragEventArgs) _
        Handles txtValue.DragDrop

        Dim sb As StringBuilder
        Dim idx As Integer

        If e.Data.GetDataPresent(GetType(DataListBeginDragEventArgs)) = False Then
            Return
        End If

        Dim dragArgs As DataListBeginDragEventArgs = CType(e.Data.GetData(GetType(DataListBeginDragEventArgs)),
            DataListBeginDragEventArgs)

        sb = New StringBuilder("Dropped IDs: ")

        ' Use the data list's currency manager
        Dim cm As CurrencyManager = dlList.ListManager

        ' Just list the IDs of the selection
        For idx = dragArgs.SelectionStart To dragArgs.SelectionEnd
            If idx <> dragArgs.SelectionStart Then
                sb.Append(","c)
            End If

            Dim address As Address = CType(cm.List(idx), Address)

            sb.Append(address.ID)
        Next idx

        txtValue.Text = sb.ToString()
    End Sub

    ' Allow drag and drop from within the data list
    Private Sub dlList_DragEnter(sender As Object, e As DragEventArgs) _
        Handles dlList.DragEnter

        If e.Data.GetDataPresent(GetType(DataListBeginDragEventArgs)) = False Then
            e.Effect = DragDropEffects.None
        Else
            e.Effect = DragDropEffects.Move
        End If
    End Sub

    ' Only allow drop if over a row or a row header but not within the current selection
    Private Sub dlList_DragOver(sender As Object, e As DragEventArgs) _
        Handles dlList.DragOver

        Dim dragArgs As DataListBeginDragEventArgs = CType(e.Data.GetData(GetType(DataListBeginDragEventArgs)),
            DataListBeginDragEventArgs)

        Dim hti As DataListHitTestInfo = dragArgs.Source.HitTest(dlList.PointToClient(Cursor.Position))

        If (hti.Type And DataListHitType.RowOrHeader) <> 0 And (hti.Row < dragArgs.SelectionStart Or
          hti.Row > dragArgs.SelectionEnd) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    ' Handle the drop operation.  This doesn't do anything interesting yet.
    Private Sub dlList_DragDrop(sender As Object, e As DragEventArgs) _
        Handles dlList.DragDrop

        If e.Data.GetDataPresent(GetType(DataListBeginDragEventArgs)) = False Then
            Return
        End If

        Dim dragArgs As DataListBeginDragEventArgs = CType(e.Data.GetData(GetType(DataListBeginDragEventArgs)),
            DataListBeginDragEventArgs)

        Dim hti As DataListHitTestInfo = dragArgs.Source.HitTest(dlList.PointToClient(Cursor.Position))

        If (hti.Type And DataListHitType.RowOrHeader) <> 0 And (hti.Row < dragArgs.SelectionStart Or
          hti.Row > dragArgs.SelectionEnd) Then
            MessageBox.Show($"Selection dropped on row {hti.Row + 1}")

            dlList.MoveTo(hti.Row)
            dlList.Select(hti.Row, hti.Row, hti.Row)
        End If
    End Sub
End Class
