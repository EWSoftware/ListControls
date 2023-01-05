'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : RelationTestForm.cs
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 10/02/2014
' Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
' Compiler: Microsoft Visual C#
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

Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.OleDb
Imports System.Windows.Forms

Imports EWSoftware.ListControls

Public Partial Class RelationTestForm
    Inherits System.Windows.Forms.Form

    Private dbConn As OleDbConnection
    Private daAddresses, daPhones As OleDbDataAdapter
    Private dsAddresses As DataSet
    Private clearingDataSet As Boolean
    Private lastSearch As String

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        lastSearch = String.Empty

        ' Create the data source for the demo
        CreateDataSource()

        ' Load data by default
        btnLoad_Click(Me, EventArgs.Empty)
    End Sub

    ' Create the data source for the demo.  You can use the designer to create the data source and use strongly
    ' typed data sets.  For this demo, we'll do it by hand.
    Private Sub CreateDataSource()
        ' The test database should be in the project folder
        dbConn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\TestData.mdb")
        daAddresses = New OleDbDataAdapter()
        daPhones = New OleDbDataAdapter()
        dsAddresses = New DataSet()

        ' Set the table name
        daAddresses.TableMappings.Add("Table", "Addresses")

        ' In a real application we wouldn't use literal SQL but we will for the demo
        daAddresses.SelectCommand = New OleDbCommand("Select * From Addresses Order By LastName", dbConn)

        daAddresses.DeleteCommand = New OleDbCommand("Delete From Addresses Where ID = @paramID", dbConn)
        daAddresses.DeleteCommand.Parameters.Add(New OleDbParameter("@paramID", OleDbType.Integer, 0,
            ParameterDirection.Input, False, 0, 0, "ID", DataRowVersion.Original, Nothing))

        daAddresses.InsertCommand = New OleDbCommand( _
            "INSERT INTO Addresses (FirstName, LastName, Address, City, State, Zip, SumValue, Domestic, " &
            "International, Postal, Parcel, Home, Business, ContactType) " &
            "VALUES (@paramFN, @paramLN, @paramAddress, @paramCity, @paramState, @paramZip, @paramSumValue, " &
            "@paramDomestic, @paramInternational, @paramPostal, @paramParcel, @paramHome, @paramBusiness, " &
            "@paramContactType)", dbConn)
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramFirstName", OleDbType.VarWChar, 20,
            "FirstName"))
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramLastName", OleDbType.VarWChar, 30,
            "LastName"))
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramAddress", OleDbType.VarWChar, 50,
            "Address"))
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramCity", OleDbType.VarWChar, 20, "City"))
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramState", OleDbType.VarWChar, 2, "State"))
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramZip", OleDbType.VarWChar, 10, "Zip"))
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramSumValue", OleDbType.Integer, 0,
            "SumValue"))
        daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramDomestic", OleDbType.Boolean, 0,
            "Domestic"))
        daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramInternational", OleDbType.Boolean, 0,
            "International"))
        daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramPostal", OleDbType.Boolean, 0, "Postal"))
        daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramParcel", OleDbType.Boolean, 0, "Parcel"))
        daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramHome", OleDbType.Boolean, 0, "Home"))
        daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramBusiness", OleDbType.Boolean, 0,
            "Business"))
        daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramContactType", OleDbType.Char, 1,
            "ContactType"))

        daAddresses.UpdateCommand = New OleDbCommand( _
            "UPDATE Addresses SET FirstName = @paramFirstName, LastName = @paramLastName, " &
            "Address = @paramAddress, City = @paramCity, State = @paramState, Zip = @paramZip, " &
            "SumValue = @paramSumValue, Domestic = @paramDomestic, International = @paramInternational, " &
            "Postal = @paramPostal, Parcel = @paramParcel, Home = @paramHome, Business = @paramBusiness, " &
            "ContactType = @paramContactType WHERE ID = @paramID", dbConn)
        daAddresses.UpdateCommand.Parameters.Add(New OleDbParameter("@paramFirstName", OleDbType.VarWChar, 20,
            "FirstName"))
        daAddresses.UpdateCommand.Parameters.Add(New OleDbParameter("@paramLastName", OleDbType.VarWChar, 30,
            "LastName"))
        daAddresses.UpdateCommand.Parameters.Add(New OleDbParameter("@paramAddress", OleDbType.VarWChar, 50,
            "Address"))
        daAddresses.UpdateCommand.Parameters.Add(New OleDbParameter("@paramCity", OleDbType.VarWChar, 20, "City"))
        daAddresses.UpdateCommand.Parameters.Add(New OleDbParameter("@paramState", OleDbType.VarWChar, 2, "State"))
        daAddresses.UpdateCommand.Parameters.Add(New OleDbParameter("@paramZip", OleDbType.VarWChar, 10, "Zip"))
        daAddresses.UpdateCommand.Parameters.Add(New OleDbParameter("@paramSumValue", OleDbType.Integer, 0,
            "SumValue"))
        daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramDomestic", OleDbType.Boolean, 0,
            "Domestic"))
        daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramInternational", OleDbType.Boolean, 0,
            "International"))
        daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramPostal", OleDbType.Boolean, 0, "Postal"))
        daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramParcel", OleDbType.Boolean, 0, "Parcel"))
        daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramHome", OleDbType.Boolean, 0, "Home"))
        daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramBusiness", OleDbType.Boolean, 0,
            "Business"))
        daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramContactType", OleDbType.Char, 1,
            "ContactType"))
        daAddresses.UpdateCommand.Parameters.Add(New OleDbParameter("@paramID", OleDbType.Integer, 0,
            ParameterDirection.Input, False, 0, 0, "ID", System.Data.DataRowVersion.Original, Nothing))

        ' Fill in the schema for auto-increment etc
        daAddresses.FillSchema(dsAddresses, SchemaType.Mapped)

        ' Bind the controls to the data source.  Since we are using a data set, we need to use fully qualified
        ' names.
        txtFName.DataBindings.Add(New Binding("Text", dsAddresses, "Addresses.FirstName"))
        txtLName.DataBindings.Add(New Binding("Text", dsAddresses, "Addresses.LastName"))
        txtAddress.DataBindings.Add(New Binding("Text", dsAddresses, "Addresses.Address"))
        txtCity.DataBindings.Add(New Binding("Text", dsAddresses, "Addresses.City"))
        cboState.DataBindings.Add(New Binding("SelectedValue", dsAddresses, "Addresses.State"))
        txtZip.DataBindings.Add(New Binding("Text", dsAddresses, "Addresses.Zip"))
        lblKey.DataBindings.Add(New Binding("Text", dsAddresses, "Addresses.ID"))

        ' Connect the Row Updated event so that we can retrieve the new primary key values as they are identity
        ' values.
        AddHandler daAddresses.RowUpdated, AddressOf daAddresses_RowUpdated

        ' The checkboxes in the checkbox list can also be bound to members of a different data source.  To do
        ' this, set the BindingMembersDataSource and specify the members to which each checkbox is bound in the
        ' BindingMembers collection property.  In this demo, the BindingMembers are specified via the designer.
        ' However, we could do it in code as well as shown here:
        '
        ' cblAddressTypes.BindingMembers.AddRange(New String() { "Addresses.Domestic", "Addresses.International",
        '     "Addresses.Postal", "Addresses.Parcel", "Addresses.Home", "Addresses.Business"})
        '
        ' As above, since we are binding to a DataSet, we must specify the fully qualified names.  Also note that
        ' there is a BindingMembersBindingContext property that can be set if the binding members data source is
        ' in a binding context different than the checkbox list's data source.
        '
        ' Note that we could assign a data source for the checkbox list items as well similar to the radio button
        ' list data source below but in this case, the list is simple so it's added to the Items collection via
        ' the designer.
        cblAddressTypes.BindingMembersDataSource = dsAddresses

        ' Create the data source for the radio button list items
        Dim contactTypeList As New List(Of ListItem)()
        contactTypeList.Add(new ListItem("B", "Business"))
        contactTypeList.Add(new ListItem("P", "Personal"))
        contactTypeList.Add(new ListItem("O", "Other"))

        rblContactType.DisplayMember = "Display"
        rblContactType.ValueMember = "Value"
        rblContactType.DataSource = contactTypeList

        ' Bind the radio button list to the ContactType field.  Since it can be null, we'll add a Parse event to
        ' default the value to "Business" if it's null.  This wouldn't be needed for fields that are never null
        ' (i.e. those with a default value).
        Dim b As New Binding("SelectedValue", dsAddresses, "Addresses.ContactType")
        AddHandler b.Format, New ConvertEventHandler(AddressOf ContactType_Format)
        rblContactType.DataBindings.Add(b)

        ' Set up the phone info data adapter
        daPhones.SelectCommand = New OleDbCommand("Select * From Phones Order By ID, PhoneNumber", dbConn)

        daPhones.DeleteCommand = New OleDbCommand("Delete From Phones Where PhoneKey = @paramPhoneKey", dbConn)
        daPhones.DeleteCommand.Parameters.Add(New OleDbParameter("@paramPhoneKey", OleDbType.Integer, 0,
            ParameterDirection.Input, False, 0, 0, "PhoneKey", DataRowVersion.Original, Nothing))

        daPhones.InsertCommand = New OleDbCommand(
            "INSERT INTO Phones (ID, PhoneNumber) VALUES (@paramID, @paramPhoneNumber)", dbConn)
        daPhones.InsertCommand.Parameters.Add(New OleDbParameter("@paramID", OleDbType.Integer, 0, "ID"))
        daPhones.InsertCommand.Parameters.Add(New OleDbParameter("@paramPhoneNumber", OleDbType.VarWChar, 20,
            "PhoneNumber"))

        daPhones.UpdateCommand = New OleDbCommand( _
            "UPDATE Phones SET PhoneNumber = @paramPhoneNumber WHERE PhoneKey = @paramPhoneKey", dbConn)
        daPhones.UpdateCommand.Parameters.Add(New OleDbParameter("@paramPhoneNumber", OleDbType.VarWChar, 20,
            "PhoneNumber"))
        daPhones.UpdateCommand.Parameters.Add(New OleDbParameter("@paramPhoneKey", OleDbType.Integer, 0,
            ParameterDirection.Input, False, 0, 0, "PhoneKey", System.Data.DataRowVersion.Original, Nothing))

        ' Connect the Row Updated event so that we can retrieve the new primary key values as they are identity
        ' values.
        AddHandler daPhones.RowUpdated, AddressOf daPhones_RowUpdated

        ' Load the state codes for the row template's shared data source
        Dim daStates As New OleDbDataAdapter("Select State, StateDesc From States", dbConn)

        Dim dtStates As New DataTable()
        daStates.Fill(dtStates)

        ' Add a blank row to allow no selection
        dtStates.Rows.InsertAt(dtStates.NewRow(), 0)

        cboState.DisplayMember = "State"
        cboState.ValueMember = "State"
        cboState.DataSource = dtStates.DefaultView
    End Sub

    ' This converts null values to a default type for the bound radio button list
    Private Sub ContactType_Format(sender As Object, e As ConvertEventArgs)
        If e.Value Is Nothing OrElse e.Value Is DBNull.Value Then
            e.Value = "B"
        End If
    End Sub

    ' Get the new primary key on added rows
    Private Sub daAddresses_RowUpdated(sender As Object, e As System.Data.OleDb.OleDbRowUpdatedEventArgs)
        If e.Status = UpdateStatus.[Continue] And e.StatementType = StatementType.Insert Then
            Dim cmd As New OleDbCommand("Select @@Identity", dbConn)
            e.Row("ID") = cmd.ExecuteScalar()
            e.Row.AcceptChanges()
        End If
    End Sub

    ' Get the new primary key on added rows
    Private Sub daPhones_RowUpdated(sender As Object, e As System.Data.OleDb.OleDbRowUpdatedEventArgs)
        If e.Status = UpdateStatus.[Continue] And e.StatementType = StatementType.Insert Then
            Dim cmd As New OleDbCommand("Select @@Identity", dbConn)
            e.Row("PhoneKey") = cmd.ExecuteScalar()
            e.Row.AcceptChanges()
        End If

        ' Ignore deletions that don't find their row.  The cascade delete took care of them already.
        If e.StatementType = StatementType.Delete And e.RecordsAffected = 0 And
          e.Status = UpdateStatus.ErrorsOccurred Then
            e.Status = UpdateStatus.[Continue]
            e.Row.AcceptChanges()
        End If
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
        ' Ignore if clearing for reload
        If clearingDataSet = False And dnNav.AllowEdits = True Then
            pnlData.Enabled = False
            lblAddRow.Visible = True
            dlPhones.Enabled = False
        End If
    End Sub

    ' Confirm the deletion of a row from the data source
    Private Sub dnNav_DeletingRow(sender As Object, e As EWSoftware.ListControls.DataNavigatorCancelEventArgs) _
      Handles dnNav.DeletingRow
        epErrors.Clear()

        If MessageBox.Show(String.Format("Are you sure you want to delete the name '{0} {1}'?", txtFName.Text,
          txtLName.Text), "Relationship Test", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
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

        If dnNav.HasChanges Or dlPhones.HasChanges Then
            dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your changes, NO to " &
                "discard them, or CANCEL to stay here and make further changes.", "Relationship Test",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3)

            If dr = System.Windows.Forms.DialogResult.Cancel Then
                e.Cancel = True
            Else
                If dr = System.Windows.Forms.DialogResult.Yes Then
                    btnSave_Click(sender, e)

                    ' If it didn't work, stay here
                    If dnNav.HasChanges Or dlPhones.HasChanges Then
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
            If dnNav.DataSource Is Nothing Then
                ' Fill the data set.  Both tables are related so they go in the same data set
                daAddresses.Fill(dsAddresses, "Addresses")
                daPhones.Fill(dsAddresses, "Phones")

                ' Relate the two tables
                dsAddresses.Relations.Add("AddrPhone", dsAddresses.Tables("Addresses").Columns("ID"),
                    dsAddresses.Tables("Phones").Columns("ID"))

                ' We could set the DataMember and DataSource properties individually.  This does the same thing
                ' in one step.
                dnNav.SetDataBinding(dsAddresses, "Addresses")

                ' Use the relationship as the data source for the phone number data list
                dlPhones.SetDataBinding(dsAddresses, "Addresses.AddrPhone", GetType(PhoneRow))
            Else
                epErrors.Clear()

                If dnNav.HasChanges Or dlPhones.HasChanges Then
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
                        If dnNav.HasChanges Or dlPhones.HasChanges Then
                            Return
                        End If
                    End If
                End If

                ' Reload it
                pnlData.Enabled = True
                lblAddRow.Visible = False

                ' When using a related data list, you must clear its data source and reset it afterwards when
                ' reloading the data set.  If not, it may generate errors as it tries to rebind on data that no
                ' longer exists.  This is a problem with the way data binding works in .NET.
                dlPhones.DataSource = Nothing

                clearingDataSet = True
                dsAddresses.Clear()
                clearingDataSet = False

                daAddresses.Fill(dsAddresses, "Addresses")
                daPhones.Fill(dsAddresses, "Phones")

                ' Reset the data source on the related phone list
                dlPhones.DataMember = "Addresses.AddrPhone"
                dlPhones.DataSource = dsAddresses
            End If

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

                daAddresses.Update(dsAddresses.Tables("Addresses"))
                daPhones.Update(dsAddresses.Tables("Phones"))
            End If
        End If
    End Sub

    ' Prevent phone information from being entered if the parent row doesn't yet exist in the related data source
    ' or is not valid.
    Private Sub dlPhones_AddingRow(sender As Object, e As EWSoftware.ListControls.DataListCancelEventArgs) _
      Handles dlPhones.AddingRow
        Dim drv As DataRowView = CType(dnNav.ListManager.List(dnNav.CurrentRow), DataRowView)

        If drv.IsNew Then
            ' The add must always be canceled
            e.Cancel = True

            ' If not valid, go back to the panel.  If valid, we need to rebind or the related data source doesn't
            ' always pick up on the fact that the parent row got added.
            If dnNav.IsValid = False Then
                txtLName.Focus()
            Else
                dlPhones.SetDataBinding(dsAddresses, "Addresses.AddrPhone", GetType(PhoneRow))
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
End Class
