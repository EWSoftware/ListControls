'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : DataNavigatorTestForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/08/2024
' Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
'
' This is used to demonstrate the DataNavigator control
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

Public Partial Class DataNavigatorTestForm
    Inherits Form

    Private dc As DemoDataContext
    Private lastSearch As String

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        lastSearch = String.Empty

        ' Set the data navigator as the object for the property grid
        pgProps.SelectedObject = dnNav
        pgProps.Refresh()

        ' Load data by default
        btnLoad_Click(Me, EventArgs.Empty)
    End Sub

    ' Disable or enable the controls based on the change policy
    Private Sub dnNav_ChangePolicyModified(sender As Object, e As EWSoftware.ListControls.ChangePolicyEventArgs) _
      Handles dnNav.ChangePolicyModified
        If pnlData.Enabled <> e.AllowEdits Or (e.AllowEdits = False And e.AllowAdditions = True) Then
            pnlData.Enabled = e.AllowEdits

            ' Allow or disallow additions based on the edit policy
            dnNav.AllowAdditions = e.AllowEdits
        End If
    End Sub

    ' Require a first and last name
    Private Sub dnNav_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) _
      Handles dnNav.Validating
        epErrors.Clear()

        ' If there are no rows, don't bother
        If pnlData.Enabled = True Then
            If txtFName.Text.Trim().Length = 0 Then
                epErrors.SetError(txtFName, "A first name is required")
                e.Cancel = True
            End If

            If txtLName.Text.Trim().Length = 0 Then
                epErrors.SetError(txtLName, "A last name is required")
                e.Cancel = True
            End If
        End If
    End Sub

    ' Clear errors if edits are canceled
    Private Sub dnNav_CanceledEdits(ByVal sender As Object, ByVal e As EWSoftware.ListControls.DataNavigatorEventArgs) _
        Handles dnNav.CanceledEdits
        epErrors.Clear()
    End Sub

    ' Disable the bound controls when there are no rows.  The AddedRow event is handled to re-enable the panel
    ' when needed.
    Private Sub dnNav_NoRows(sender As Object, e As System.EventArgs) _
      Handles dnNav.NoRows
        ' Ignore if clearing for reload
        If dnNav.AllowEdits = True Then
            pnlData.Enabled = False
            lblAddRow.Visible = True
        End If
    End Sub

    ' Confirm the deletion of a row from the data source
    Private Sub dnNav_DeletingRow(sender As Object, e As EWSoftware.ListControls.DataNavigatorCancelEventArgs) _
      Handles dnNav.DeletingRow
        epErrors.Clear()

        If MessageBox.Show($"Are you sure you want to delete the name '{txtFName.Text} {txtLName.Text}'?",
            "Data Navigator Test", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    ' Enable the bound controls when a row exists.
    Private Sub dnNav_AddedRow(sender As Object, e As EWSoftware.ListControls.DataNavigatorEventArgs) _
      Handles dnNav.AddedRow
        If pnlData.Enabled = False And dnNav.AllowEdits = True Then
            pnlData.Enabled = True
            lblAddRow.Visible = False
        End If
    End Sub

    ' Refresh the display and the data navigator settings after they have changed
    Private Sub pgProps_PropertyValueChanged(s As object, e As System.Windows.Forms.PropertyValueChangedEventArgs) _
      Handles pgProps.PropertyValueChanged
        dnNav.Invalidate()
        dnNav.Update()
    End Sub

    ' Prompt to save changes if necessary
    Private Sub DataNavigatorTestForm_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) _
        Handles MyBase.Closing

        Dim dr As DialogResult

        If dc.ChangeTracker.HasChanges() Then
            dr = MessageBox.Show("Do you want to save your changes? Click YES to save your changes, NO to " &
                "discard them, or CANCEL to stay here and make further changes.", "DataNavigator Test",
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

    ' Load the data for the data navigator control
	Private Sub btnLoad_Click(sender As Object, e As System.EventArgs) _
	  Handles btnLoad.Click
        Dim dr As DialogResult

        Try
            epErrors.Clear()

            If dc IsNot Nothing AndAlso dc.ChangeTracker.HasChanges() Then
                dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your changes, " &
                    "NO to discard them, or CANCEL to stay here and make further changes.",
                    "Data Navigator Test", MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button3)

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

                pnlData.Enabled = True
                lblAddRow.Visible = False

                If cboState.DataSource Is Nothing Then
                    Dim states As List(Of StateCode) = dc.StateCodes.ToList()

                    states.Insert(0, new StateCode With { .State = String.Empty, .StateDesc = String.Empty })

                    cboState.DisplayMember = NameOf(StateCode.State)
                    cboState.ValueMember = NameOf(StateCode.State)
                    cboState.DataSource = states
                End If

                ' For entity framework we need to load the entities
                dc.Addresses.Load()

                ' Apply a sort by last name
                Dim pdc As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(Address))
                Dim pd As PropertyDescriptor = pdc(NameOf(Address.LastName))
                Dim dataSource As BindingList(Of Address) = dc.Addresses.Local.ToObservableCollection().ToBindingList()

                CType(dataSource, IBindingList).ApplySort(pd, ListSortDirection.Ascending)

                ' We could set each binding property individually, but this is more efficient
                dnNav.SetDataBinding(dataSource, Nothing)

                ' Bind the controls to the data source
                txtFName.DataBindings.Clear()
                txtLName.DataBindings.Clear()
                txtAddress.DataBindings.Clear()
                txtCity.DataBindings.Clear()
                cboState.DataBindings.Clear()
                txtZip.DataBindings.Clear()
                lblKey.DataBindings.Clear()

                txtFName.DataBindings.Add(New Binding(NameOf(Control.Text), dataSource, NameOf(Address.FirstName)))
                txtLName.DataBindings.Add(New Binding(NameOf(Control.Text), dataSource, NameOf(Address.LastName)))
                txtAddress.DataBindings.Add(New Binding(NameOf(Control.Text), dataSource, NameOf(Address.StreetAddress)))
                txtCity.DataBindings.Add(New Binding(NameOf(Control.Text), dataSource, NameOf(Address.City)))
                cboState.DataBindings.Add(New Binding(NameOf(MultiColumnComboBox.SelectedValue), dataSource,
                    NameOf(Address.State)))
                txtZip.DataBindings.Add(New Binding(NameOf(Control.Text), dataSource, NameOf(Address.Zip)))
                lblKey.DataBindings.Add(New Binding(NameOf(Control.Text), dataSource, NameOf(Address.ID)))

                ' We must enable formatting or the bound values for these control types won't get updated for
                ' some reason.  They also need to update on the property value changing in case the mouse wheel
                ' is used.
                cboState.DataBindings(0).FormattingEnabled = True
                cboState.DataBindings(0).DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged

        Catch ex As Exception
            MessageBox.Show(Me, ex.Message)
        End Try

	End Sub

    ' Save the changes
    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) _
      Handles btnSave.Click
        If dnNav.DataSource Is Nothing Then
            btnLoad_Click(sender, e)
        Else
            If dnNav.IsValid Then
                ' We must commit any pending changes
                dnNav.CommitChanges()

                ' There may be a row added for the placeholder that needs removing
                Dim removeRows As List(Of Address) = dc.ChangeTracker.Entries(Of Address)().Select(
                    Function (a) a.Entity).Where(Function (a) a.LastName Is Nothing).ToList()

                If removeRows.Count <> 0 Then
                    dc.RemoveRange(removeRows)
                End If

                dc.SaveChanges()
            End If
        End If
    End Sub

    ' Add a row outside of the data navigator to test its ability to detect and add a new row added in this
    ' manner.
    Private Sub btnAddDSRow_Click(sender As Object, e As System.EventArgs) _
      Handles btnAddDSRow.Click
        If dnNav.DataSource Is Nothing Then
            btnLoad_Click(sender, e)
        Else
            ' Commit any changes to the current row in case it's the new row.  It's temporary until committed and
            ' adding a new row here puts this one ahead of it.
            dnNav.CommitChanges()

            Dim bl As BindingList(Of Address) = CType(dnNav.DataSource, BindingList(Of Address))

            bl.Add(new Address With
            {
                .FirstName = "External",
                .LastName = "Row",
                .LastModified = BitConverter.GetBytes(DateTime.Now.Ticks)
            })

            dnNav.MoveTo(RowPosition.LastRow)
            dnNav.Focus()
        End If
    End Sub

    ' Delete a row outside of the data navigator to test its ability to detect a row deleted in this manner
    Private Sub btnDelDSRow_Click(sender As Object, e As System.EventArgs) _
      Handles btnDelDSRow.Click
        If dnNav.DataSource Is Nothing Then
            btnLoad_Click(sender, e)
        Else
            ' Use the currency manager as the data set's row collection may have been changed and the indexes
            ' won't match up to the actual rows.
            Dim cm As CurrencyManager = dnNav.ListManager

            Dim row As Integer = CType(udcRowNumber.Value, Integer)

            If row > 0 And row <= cm.Count Then
                Dim bl As BindingList(Of Address) = CType(dnNav.DataSource, BindingList(Of Address))

                bl.RemoveAt(row - 1)
            Else
                MessageBox.Show("Not a valid row number")
            End If
        End If
    End Sub

    ' Modify a row outside of the data navigator to test its ability to detect a row changed in this manner
    Private Sub btnModRow_Click(sender As Object, e As System.EventArgs) _
      Handles btnModRow.Click
        If dnNav.DataSource Is Nothing Then
            btnLoad_Click(sender, e)
        Else
            ' Commit any other pending changes
            dnNav.CommitChanges()

            ' Use the currency manager as the data set's row collection may have been changed and the indexes
            ' won't match up to the actual rows.
            Dim cm As CurrencyManager = dnNav.ListManager

            Dim row As Integer = CType(udcRowNumber.Value, Integer)

            If row > 0 And row <= cm.Count Then
                Dim address As Address = CType(cm.List(row - 1), Address)
                address.StreetAddress = "Modified externally"

                dnNav.MoveTo(row - 1)
                dnNav.Focus()
            Else
                MessageBox.Show("Not a valid row number")
            End If
        End If
    End Sub

    ' Get a value from the data navigator
    Private Sub btnGetValue_Click(sender As Object, e As System.EventArgs) _
      Handles btnGetValue.Click
        ' This can be any column from the data source regardless of whether or not it is displayed.  Note that
        ' you can also use dnNav("ColName") to get a column value from the item indicated by the CurrentRow
        ' property.
        txtValue.Text = $"{cboColumns.Text} = {dnNav(CType(txtRowNumber.Value, Integer) - 1, cboColumns.Text)}"
    End Sub

    ' Find an entry by last name (incremental search)
    Private Sub txtFindName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles txtFindName.TextChanged

        Dim startRow, row As Integer

        If txtFindName.Text.Length > 0 Then
            If txtFindName.Text.Length <= lastSearch.Length Then
                startRow = -1
            Else
                startRow = dnNav.CurrentRow - 1
            End If

            lastSearch = txtFindName.Text
            row = dnNav.FindString("LastName", txtFindName.Text, startRow)

            If row <> -1 Then
                dnNav.MoveTo(row)
            End If
        End If
    End Sub
End Class
