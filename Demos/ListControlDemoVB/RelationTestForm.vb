'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : RelationTestForm.cs
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/08/2024
' Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
'
' This is used to demonstrate how the data navigator and data list controls can be used with related data
' sources in a data set.
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 12/23/2005  EFW  Created the code
' 05/05/2007  EFW  Added demo of data binding the CheckBoxList and RadioButtonList controls
'================================================================================================================

Public Partial Class RelationTestForm
    Inherits Form

    Private dc As DemoDataContext
    Private lastSearch As String

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        lastSearch = String.Empty

        dlPhones.RowTemplate = GetType(PhoneRow)

        ' Load data by default
        btnLoad_Click(Me, EventArgs.Empty)
    End Sub

    ' Require a first and last name
    Private Sub dnNav_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) _
        Handles dnNav.Validating

        epErrors.Clear()

        ' If there are no rows, don't bother
        If pnlData.Enabled Then
            If dlPhones.IsValid = False Then
                e.Cancel = True
            Else
                If txtFName.Text.Trim().Length = 0 Then
                    epErrors.SetError(txtFName, "A first name is required")
                    e.Cancel = True
                End If

                If txtLName.Text.Trim().Length = 0 Then
                    epErrors.SetError(txtLName, "A last name is required")
                    e.Cancel = True
                End If
            End If

            ' If not canceled, commit the changes.  This is needed so that the phone data list will receive the
            ' key values when rows are added to it.
            If e.Cancel = False Then
                dnNav.CommitChanges()
            End If
        End If
    End Sub

    ' Clear the errors if edits are canceled
    Private Sub dnNav_CanceledEdits(sender As Object, e As EWSoftware.ListControls.DataNavigatorEventArgs) _
      Handles dnNav.CanceledEdits
        epErrors.Clear()
    End Sub

    ' Disable the bound controls when there are no rows.  The AddedRow event is handled to re-enable the panel
    ' when needed.
    Private Sub dnNav_NoRows(sender As Object, e As System.EventArgs) _
      Handles dnNav.NoRows
        If dnNav.AllowEdits = True Then
            pnlData.Enabled = False
            lblAddRow.Visible = True
            dlPhones.Enabled = False
        End If
    End Sub

    ' Confirm the deletion of a row from the data source
    Private Sub dnNav_DeletingRow(sender As Object, e As EWSoftware.ListControls.DataNavigatorCancelEventArgs) _
      Handles dnNav.DeletingRow
        epErrors.Clear()

        If MessageBox.Show($"Are you sure you want to delete the name '{txtFName.Text} {txtLName.Text}'?",
          "Relationship Test", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
          MessageBoxDefaultButton.Button2) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    ' Enable the bound controls when a row exists
    Private Sub dnNav_AddedRow(sender As Object, e As EWSoftware.ListControls.DataNavigatorEventArgs) _
      Handles dnNav.AddedRow
        If pnlData.Enabled = False And dnNav.AllowEdits = True Then
            pnlData.Enabled = True
            lblAddRow.Visible = False
            dlPhones.Enabled = True
        End If
    End SUb

    ' Prompt to save changes if necessary
    Private Sub RelationTestForm_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) _
        Handles MyBase.Closing

        Dim dr As DialogResult

        If dc.ChangeTracker.HasChanges() Then
            dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your changes, NO to " &
                "discard them, or CANCEL to stay here and make further changes.", "Relationship Test",
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

    ' Load the data into the controls
    Private Sub btnLoad_Click(sender As Object, e As System.EventArgs) _
      Handles btnLoad.Click
        Dim dr As DialogResult

        Try
            epErrors.Clear()

            If dc IsNot Nothing AndAlso dc.ChangeTracker.HasChanges() Then
                dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your changes, " &
                    "NO to discard them, or CANCEL to stay here and make further changes.",
                    "Relationship Test", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button3)

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
            dc = New DemoDataContext()

            pnlData.Enabled = true
            lblAddRow.Visible = false

            If cboState.DataSource Is Nothing Then
                Dim states As List(Of StateCode) = dc.StateCodes.ToList()

                states.Insert(0, New StateCode With { .State = String.Empty, .StateDesc = String.Empty })

                cboState.DisplayMember = cboState.ValueMember = NameOf(StateCode.State)
                cboState.DataSource = states
            End If

            ' For entity framework we need to load the entities
            dc.Addresses.Load()
            dc.PhoneNumbers.Load()

            ' Apply a sort by last name
            Dim pdc As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(Address))
            Dim pd As PropertyDescriptor = pdc(NameOf(Address.LastName))
            Dim dataSource As BindingList(Of Address) = dc.Addresses.Local.ToObservableCollection().ToBindingList()

            CType(dataSource, IBindingList).ApplySort(pd, ListSortDirection.Ascending)

            ' Bind the controls to the data source
            txtFName.DataBindings.Clear()
            txtLName.DataBindings.Clear()
            txtAddress.DataBindings.Clear()
            txtCity.DataBindings.Clear()
            cboState.DataBindings.Clear()
            txtZip.DataBindings.Clear()
            lblKey.DataBindings.Clear()
            rblContactType.DataBindings.Clear()

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
            cboState.DataBindings(0).FormattingEnabled = true
            cboState.DataBindings(0).DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged

            ' When using a related data list, you must clear its data source and reset it afterwards
            ' when reloading the data set.  If not, it may generate errors as it tries to rebind on data
            ' that no longer exists.  This is a problem with the way data binding works in .NET.
            dlPhones.DataSource = Nothing

            ' The checkboxes in the checkbox list can also be bound to members of a different data source.  To
            ' do this, set the BindingMembersDataSource and specify the members to which each checkbox is bound
            ' in the BindingMembers collection property.  In this demo, the BindingMembers are specified via the
            ' designer.  However, we could do it in code as well as shown here:
            '
            ' cblAddressTypes.BindingMembers.AddRange(new[] { "Domestic", "International", "Postal", "Parcel",
            '     "Home", "Business" })
            '
            ' Also note that there is a BindingMembersBindingContext property that can be set if the binding
            ' members data source is in a binding context different than the checkbox list's data source.
            '
            ' Note that we could assign a data source for the checkbox list items as well similar to the radio
            ' button list data source below but in this case, the list is simple so it's added to the Items
            ' collection via the designer.
            cblAddressTypes.BindingMembersDataSource = dataSource

            ' Create the data source for the radio button list items
            Dim contactTypeList = New List(Of ListItem) From
            {
                New ListItem("B"C, "Business"),
                New ListItem("P"C, "Personal"),
                New ListItem("O"C, "Other")
            }

            rblContactType.DisplayMember = "Display"
            rblContactType.ValueMember = "Value"
            rblContactType.DataSource = contactTypeList

            ' Bind the radio button list to the ContactType field.  Since it can be null, we'll add a Format
            ' event to default the value to "Business" if it's null.  This wouldn't be needed for fields that
            ' are never null (i.e. those with a default value).
            Dim b As New Binding(NameOf(RadioButtonList.SelectedValue), dataSource, NameOf(Address.ContactType))
            AddHandler b.Format, Sub (s, ev) ev.Value = If(ev.Value, "B"C)
            b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
            b.FormattingEnabled = true

            rblContactType.DataBindings.Add(b)

            ' We could set each binding property individually, but this is more efficient
            dnNav.SetDataBinding(dataSource, Nothing)
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
            If dnNav.IsValid And dlPhones.IsValid Then
                ' We must commit any pending changes
                dnNav.CommitChanges()
                dlPhones.CommitChanges()

                ' There may be a row added for the placeholder that needs removing
                Dim removeAddresses As List(Of Address) = dc.ChangeTracker.Entries(Of Address)().Select(
                    Function (a) a.Entity).Where(Function(a) a.LastName Is Nothing).ToList()

                If removeAddresses.Count <> 0
                    dc.RemoveRange(removeAddresses)
                End If

                Dim removePhones As List(Of Phone) = dc.ChangeTracker.Entries(Of Phone)().Select(
                    Function (a) a.Entity).Where(Function(a) a.PhoneNumber Is Nothing).ToList()

                If removePhones.Count <> 0
                    dc.RemoveRange(removePhones)
                End If

                dc.SaveChanges()
            End If
        End If
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

    ' To keep the data context aware of additions and deletion automatically would likely require a custom
    ' binding list.  As such, we'll handle adding and removing phone numbers from the data context.
    Private Sub dnNav_Current(sender As Object, e As DataNavigatorEventArgs) Handles dnNav.Current
        Dim address As Address = CType(dnNav.ListManager.List(dnNav.CurrentRow), Address)

        If address IsNot Nothing Then
            dlPhones.DataSource = New BindingList(Of Phone)(address.PhoneNumbers.ToList())
        Else
            dlPhones.DataSource = Nothing
        End If
    End Sub

    ' Set the related address key and add a new phone entry
    Private Sub dlPhones_AddedRow(sender As Object, e As DataListEventArgs) Handles dlPhones.AddedRow
        Dim address As Address = CType(dnNav.ListManager.List(dnNav.CurrentRow), Address)
        Dim phone As Phone = CType(e.Item.RowSource, Phone)

        address.PhoneNumbers.Add(phone)
    End Sub

    ' Remove a deleted phone entry
    Private Sub dlPhones_DeletingRow(sender As Object, e As DataListCancelEventArgs) Handles dlPhones.DeletingRow
        Dim address As Address = CType(dnNav.ListManager.List(dnNav.CurrentRow), Address)
        Dim phone As Phone = CType(e.Item.RowSource, Phone)

        address.PhoneNumbers.Remove(phone)
    End Sub
End Class
