'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : DataNavigatorTestForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 04/09/2023
' Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
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

Imports System.Data
Imports System.Data.OleDb

Imports EWSoftware.ListControls

Public Partial Class DataNavigatorTestForm
    Inherits Form

    Private dbConn As OleDbConnection
    Private WithEvents daAddresses As OleDbDataAdapter
    Private dsAddresses As DataSet
    Private clearingDataSet As Boolean
    Private lastSearch As String

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        lastSearch = String.Empty

        ' Create the data source for the demo
        CreateDataSource()

        ' Set the data navigator as the object for the property grid
        pgProps.SelectedObject = dnNav
        pgProps.Refresh()

        ' Load data by default
        btnLoad_Click(Me, EventArgs.Empty)
    End Sub

    ' Create the data source for the demo.  You can use the designer to create the data source and use strongly
    ' typed data sets.  For this demo, we'll do it by hand.
    Private Sub CreateDataSource()
        ' The test database should be in the project folder
        dbConn = New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\TestData.mdb")
        daAddresses = New OleDbDataAdapter()
        dsAddresses = New DataSet()

        ' Set the table name
        daAddresses.TableMappings.Add("Table", "Addresses")

        ' In a real application we wouldn't use literal SQL but we will for the demo
        daAddresses.SelectCommand = New OleDbCommand("Select * From Addresses Order By LastName", dbConn)

        daAddresses.DeleteCommand = New OleDbCommand("Delete From Addresses Where ID = @paramID", dbConn)
        daAddresses.DeleteCommand.Parameters.Add(New OleDbParameter("@paramID", OleDbType.Integer, 0,
            ParameterDirection.Input, False, 0, 0, "ID", DataRowVersion.Original, Nothing))

        daAddresses.InsertCommand = New OleDbCommand(
            "INSERT INTO Addresses (FirstName, LastName, Address, City, State, Zip, SumValue) " &
            "VALUES (@paramFN, @paramLN, @paramAddress, @paramCity, @paramState, @paramZip, @paramSumValue)",
            dbConn)
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramFirstName", OleDbType.VarWChar, 20,
            "FirstName"))
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramLastName", OleDbType.VarWChar, 30,
            "LastName"))
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramAddress", OleDbType.VarWChar, 50,
            "Address"))
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramCity", OleDbType.VarWChar, 20,
            "City"))
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramState", OleDbType.VarWChar, 2,
            "State"))
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramZip", OleDbType.VarWChar, 10, "Zip"))
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramSumValue", OleDbType.Integer, 0,
            "SumValue"))

        daAddresses.UpdateCommand = New OleDbCommand(
            "UPDATE Addresses SET FirstName = @paramFirstName, LastName = @paramLastName, " &
            "Address = @paramAddress, City = @paramCity, State = @paramState, Zip = @paramZip, " &
            "SumValue = @paramSumValue WHERE ID = @paramID", dbConn)
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
#Disable Warning CA2000
        ' Load the state codes for the row template's shared data source
        Using daStates As New OleDbDataAdapter("Select State, StateDesc From States", dbConn)
            Dim dtStates As New DataTable()
            daStates.Fill(dtStates)

            ' Add a blank row to allow no selection
            dtStates.Rows.InsertAt(dtStates.NewRow(), 0)

            cboState.DisplayMember = "State"
            cboState.ValueMember = "State"
            cboState.DataSource = dtStates.DefaultView
        End Using
#Enable Warning CA2000
    End Sub

    ' Get the new primary key on added rows
    Private Sub daAddresses_RowUpdated(sender As Object, e As System.Data.OleDb.OleDbRowUpdatedEventArgs)
        If e.Status = UpdateStatus.[Continue] And e.StatementType = StatementType.Insert Then
            Using cmd As New OleDbCommand("Select @@Identity", dbConn)
                e.Row("ID") = cmd.ExecuteScalar()
                e.Row.AcceptChanges()
            End Using
        End If
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
        If clearingDataSet = False And dnNav.AllowEdits = True Then
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

        If dnNav.HasChanges Then
            dr = MessageBox.Show("Do you want to save your changes? Click YES to save your changes, NO to " &
                "discard them, or CANCEL to stay here and make further changes.", "DataNavigator Test",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3)

            If dr = System.Windows.Forms.DialogResult.Cancel Then
                e.Cancel = True
            Else
                If dr = System.Windows.Forms.DialogResult.Yes Then
                    btnSave_Click(sender, e)

                    ' If it didn't work, stay here
                    If dnNav.HasChanges Then
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
            If dnNav.DataSource Is Nothing Then
                ' Initial load
                daAddresses.Fill(dsAddresses)

                ' We could set the DataMember and DataSource properties individually.  This does the same thing
                ' in one step.
                dnNav.SetDataBinding(dsAddresses, "Addresses")
            Else
                epErrors.Clear()

                If dnNav.HasChanges Then
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
                        If dnNav.HasChanges Then
                            Return
                        End If
                    End If
                End If

                ' Reload it
                pnlData.Enabled = True
                lblAddRow.Visible = False

                clearingDataSet = True
                dsAddresses.Clear()
                clearingDataSet = False

                daAddresses.Fill(dsAddresses)
            End If
#Disable Warning CA1031
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message)
        End Try
#Enable Warning CA1031
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

                daAddresses.Update(dsAddresses)
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

            Dim r As DataRow = dsAddresses.Tables(0).NewRow()

            r("FirstName") = "External"
            r("LastName") = "Row"

            dsAddresses.Tables(0).Rows.Add(r)
            dnNav.MoveTo(RowPosition.LastRow)
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
                Dim drv As DataRowView = CType(cm.List(row - 1), DataRowView)

                ' If it's an uncommitted new row, just cancel the changes.  Otherwise, delete the row.
                If drv.IsNew Then
                    dnNav.CancelChanges()
                Else
                    drv.Row.Delete()
                End If
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
                Dim drv As DataRowView = CType(cm.List(row - 1), DataRowView)
                drv.Row("Address") = "Modified externally"
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
