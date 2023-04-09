'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : DataListTestForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 04/09/2023
' Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
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

Imports System.Data
Imports System.Data.OleDb
Imports System.Text

Imports EWSoftware.ListControls

Public Partial Class DataListTestForm
    Inherits Form

    Private dbConn As OleDbConnection
    Private WithEvents daAddresses As OleDbDataAdapter
    Private dsAddresses As DataSet

    Public Sub New()
        MyBase.New()

        InitializeComponent()

        ' Create the data source for the demo
        CreateDataSource()

        ' Set the data list as the object for the property grid
        pgProps.SelectedObject = dlList
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

        ' In a real application we wouldn't use literal SQL but we will for the demo.
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
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramCity", OleDbType.VarWChar, 20, "City"))
        daAddresses.InsertCommand.Parameters.Add(New OleDbParameter("@paramState", OleDbType.VarWChar, 2, "State"))
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

            dlList.SharedDataSources.Add("States", dtStates.DefaultView)
        End Using
#Enable Warning CA2000
    End Sub

    ' Get the new primary key on added rows
    Private Sub daAddresses_RowUpdated(sender As Object, e As System.Data.OleDb.OleDbRowUpdatedEventArgs) _
      Handles daAddresses.RowUpdated
        If e.Status = UpdateStatus.[Continue] And e.StatementType = StatementType.Insert Then
            Using cmd As New OleDbCommand("Select @@Identity", dbConn)
                e.Row("ID") = cmd.ExecuteScalar()
                e.Row.AcceptChanges()
            End Using
        End If
    End Sub

    ' Change the color of the row based on the zip code when a row is data bound
	Private Sub dlList_ItemDataBound(sender As Object, e As EWSoftware.ListControls.DataListEventArgs) _
      Handles dlList.ItemDataBound
	    If dlList(e.Index, "Zip").ToString() = "98122" Then
            e.Item.BackColor = Color.LightSteelBlue
        Else
            e.Item.BackColor = SystemColors.Control
        End If
	End Sub

    ' Refresh the display and the data list settings after they have changed
    Private Sub pgProps_PropertyValueChanged(s As object, e As System.Windows.Forms.PropertyValueChangedEventArgs) _
      Handles pgProps.PropertyValueChanged
        dlList.Invalidate()
        dlList.Update()
    End Sub

    ' Prompt to save changes if necessary
    Private Sub DataListTestForm_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) _
        Handles MyBase.Closing

        Dim dr As DialogResult

        If dlList.HasChanges Then
            dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your changes, NO " &
                "to discard them, or CANCEL to stay here and make further changes.", "DataList Test",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3)

            If dr = System.Windows.Forms.DialogResult.Cancel Then
                e.Cancel = True
            Else
                If dr = System.Windows.Forms.DialogResult.Yes Then
                    btnSave_Click(sender, e)

                    ' If it didn't work, stay here
                    If dlList.HasChanges Then
                        e.Cancel = True
                    End If
                End If
            End If
        End If
    End Sub

    ' Load the data into the data list control
	Private Sub btnLoad_Click(sender As Object, e As System.EventArgs) _
	  Handles btnLoad.Click
        Dim dr As DialogResult

        Try
            If dlList.DataSource Is Nothing Then
                ' Initial load
                daAddresses.Fill(dsAddresses)

                ' We could set each property individually, but this is more efficient.  Since we are using the
                ' DataSet as the data source, we must specify the data member as well.
                dlList.SetDataBinding(dsAddresses, "Addresses", GetType(AddressRow), GetType(AddressHeader),
                    GetType(AddressFooter))
            Else
                If dlList.HasChanges Then
                    dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your changes, " &
                        "NO to discard them, or CANCEL to stay here and make further changes.", "DataList Test",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3)

                    If dr = System.Windows.Forms.DialogResult.Cancel Then
                        Return
                    End If

                    If dr = System.Windows.Forms.DialogResult.Yes Then
                        btnSave_Click(sender, e)

                        ' If it didn't work, don't do anything
                        If dlList.HasChanges Then
                            Return
                        End If
                    End If
                End If

                ' Reload it
                dsAddresses.Clear()
                daAddresses.Fill(dsAddresses)
            End If
#Disable Warning CA1031
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message)
        End Try
#Enable Warning CA1031

        dlList.Focus()
	End Sub

    ' Save the changes
    Private Sub btnSave_Click(sender As Object, e As System.EventArgs) _
      Handles btnSave.Click
        If dlList.DataSource Is Nothing Then
            btnLoad_Click(sender, e)
        Else
            If dlList.IsValid = True Then
                ' We must commit any pending changes
                dlList.CommitChanges()

                daAddresses.Update(dsAddresses)
            End If
        End If
    End Sub

    ' Add a row outside of the data list to test its ability to detect and add a new row added in this manner
    Private Sub btnAddDSRow_Click(sender As Object, e As System.EventArgs) _
      Handles btnAddDSRow.Click
        If dlList.DataSource Is Nothing Then
            btnLoad_Click(sender, e)
        Else
            ' Commit any changes to the current row in case it's the new row.  It's temporary until committed and
            ' adding a new row here puts this one ahead of it.
            dlList.CommitChanges()

            Dim r As DataRow = dsAddresses.Tables(0).NewRow()

            r("FirstName") = "External"
            r("LastName") = "Row"

            dsAddresses.Tables(0).Rows.Add(r)
        End If
    End Sub

    ' Delete a row outside of the data list to test its ability to detect a row deleted in this manner
    Private Sub btnDelDSRow_Click(sender As Object, e As System.EventArgs) _
      Handles btnDelDSRow.Click
        If dlList.DataSource Is Nothing Then
            btnLoad_Click(sender, e)
        Else
            ' Use the currency manager as the data set's row collection may have been changed and the indexes
            ' won't match up to the actual rows.
            Dim cm As CurrencyManager = dlList.ListManager

            Dim row As Integer = CType(udcRowNumber.Value, Integer)

            If row > 0 And row <= cm.Count Then
                Dim drv As DataRowView = CType(cm.List(row - 1), DataRowView)

                ' If it's an uncommitted new row, just cancel the changes.  Otherwise, delete the row.
                If drv.IsNew Then
                    dlList.CancelChanges()
                Else
                    drv.Row.Delete()
                End If
            Else
                MessageBox.Show("Not a valid row number")
            End If
        End If
    End Sub

    ' Modify a row outside of the data list to test its ability to detect a row changed in this manner
    Private Sub btnModRow_Click(sender As Object, e As System.EventArgs) _
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
                Dim drv As DataRowView = CType(cm.List(row - 1), DataRowView)
                drv.Row("Address") = "Modified externally"
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
    Private Sub chkShowHeader_CheckedChanged(sender As Object, e As System.EventArgs) _
        Handles chkShowHeader.CheckedChanged

        If chkShowHeader.Checked = True Then
            dlList.HeaderTemplate = GetType(AddressHeader)
        Else
            dlList.HeaderTemplate = Nothing
        End If
    End Sub

    ' Show or hide the data list's footer
    Private Sub chkShowFooter_CheckedChanged(sender As Object, e As System.EventArgs) _
        Handles chkShowFooter.CheckedChanged

        If chkShowFooter.Checked = True Then
            dlList.FooterTemplate = GetType(AddressFooter)
        Else
            dlList.FooterTemplate = Nothing
        End If
    End Sub

    ' Test drag and drop with the data list.
    Private Sub dlList_BeginDrag(sender As Object, e As EWSoftware.ListControls.DataListBeginDragEventArgs) _
        Handles dlList.BeginDrag
        ' Commit any pending changes before the drag operation begins
        dlList.CommitChanges()

        Me.DoDragDrop(e, DragDropEffects.Copy Or DragDropEffects.Move)
    End Sub

    ' Allow drag and drop from data list
    Private Sub txtValue_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) _
        Handles txtValue.DragEnter

        If e.Data.GetDataPresent(GetType(DataListBeginDragEventArgs)) = True Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    ' Handle drop from data list
    Private Sub txtValue_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) _
        Handles txtValue.DragDrop

        Dim drv As DataRowView
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

            drv = CType(cm.List(idx), DataRowView)

            sb.Append(drv("ID"))
        Next idx

        txtValue.Text = sb.ToString()
    End Sub

    ' Allow drag and drop from within the data list
    Private Sub dlList_DragEnter(sender As Object, e As System.Windows.Forms.DragEventArgs) _
        Handles dlList.DragEnter

        If e.Data.GetDataPresent(GetType(DataListBeginDragEventArgs)) = False Then
            e.Effect = DragDropEffects.None
        Else
            e.Effect = DragDropEffects.Move
        End If
    End Sub

    ' Only allow drop if over a row or a row header but not within the current selection
    Private Sub dlList_DragOver(sender As Object, e As System.Windows.Forms.DragEventArgs) _
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
    Private Sub dlList_DragDrop(sender As Object, e As System.Windows.Forms.DragEventArgs) _
        Handles dlList.DragDrop

        If e.Data.GetDataPresent(GetType(DataListBeginDragEventArgs)) = False Then
            Return
        End If

        Dim dragArgs As DataListBeginDragEventArgs = CType(e.Data.GetData(GetType(DataListBeginDragEventArgs)),
            DataListBeginDragEventArgs)

        Dim hti As DataListHitTestInfo = dragArgs.Source.HitTest(dlList.PointToClient(Cursor.Position))

        If (hti.Type And DataListHitType.RowOrHeader) <> 0 And (hti.Row < dragArgs.SelectionStart Or _
          hti.Row > dragArgs.SelectionEnd) Then
            MessageBox.Show($"Selection dropped on row {hti.Row + 1}")

            dlList.MoveTo(hti.Row)
            dlList.Select(hti.Row, hti.Row, hti.Row)
        End If
    End Sub
End Class
