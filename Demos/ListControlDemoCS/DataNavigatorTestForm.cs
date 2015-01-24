//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : DataNavigatorTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/01/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This is used to demonstrate the DataNavigator control
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/17/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

using EWSoftware.ListControls;

namespace ListControlDemoCS
{
	/// <summary>
	/// This is used to demonstrate the DataNavigator control
	/// </summary>
	public partial class DataNavigatorTestForm : System.Windows.Forms.Form
	{
        #region Private data members
        //=====================================================================

        private OleDbConnection dbConn;
        private OleDbDataAdapter daAddresses;
        private DataSet dsAddresses;
        private bool clearingDataSet;
        private string lastSearch;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public DataNavigatorTestForm()
		{
			InitializeComponent();

            lastSearch = String.Empty;

            // Create the data source for the demo
            CreateDataSource();

            // Set the data navigator as the object for the property grid
            pgProps.SelectedObject = dnNav;
            pgProps.Refresh();

            // Load data by default
            btnLoad_Click(this, EventArgs.Empty);
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Create the data source for the demo.
        /// </summary>
        /// <remarks>You can use the designer to create the data source and use strongly typed data sets.  For
        /// this demo, we'll do it by hand.
        /// </remarks>
        private void CreateDataSource()
        {
            // The test database should be in the project folder
            dbConn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\TestData.mdb");
            daAddresses = new OleDbDataAdapter();
            dsAddresses = new DataSet();

            // Set the table name
            daAddresses.TableMappings.Add("Table", "Addresses");

            // In a real application we wouldn't use literal SQL but we will for the demo
            daAddresses.SelectCommand = new OleDbCommand("Select * From Addresses Order By LastName", dbConn);

            daAddresses.DeleteCommand = new OleDbCommand("Delete From Addresses Where ID = @paramID", dbConn);
            daAddresses.DeleteCommand.Parameters.Add(new OleDbParameter("@paramID", OleDbType.Integer, 0,
                ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Original, null));

            daAddresses.InsertCommand = new OleDbCommand(
                "INSERT INTO Addresses (FirstName, LastName, Address, City, State, Zip, SumValue) " +
                "VALUES (@paramFN, @paramLN, @paramAddress, @paramCity, @paramState, @paramZip, @paramSumValue)",
                dbConn);
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramFirstName", OleDbType.VarWChar,
                20, "FirstName"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramLastName", OleDbType.VarWChar, 30,
                "LastName"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramAddress", OleDbType.VarWChar, 50,
                "Address"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramCity", OleDbType.VarWChar, 20,
                "City"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramState", OleDbType.VarWChar, 2,
                "State"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramZip", OleDbType.VarWChar, 10,
                "Zip"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramSumValue", OleDbType.Integer, 0,
                "SumValue"));

            daAddresses.UpdateCommand = new OleDbCommand(
                "UPDATE Addresses SET FirstName = @paramFirstName, LastName = @paramLastName, " +
                "Address = @paramAddress, City = @paramCity, State = @paramState, Zip = @paramZip, " +
                "SumValue = @paramSumValue WHERE ID = @paramID", dbConn);
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramFirstName", OleDbType.VarWChar,
                20, "FirstName"));
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramLastName", OleDbType.VarWChar, 30,
                "LastName"));
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramAddress", OleDbType.VarWChar, 50,
                "Address"));
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramCity", OleDbType.VarWChar, 20,
                "City"));
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramState", OleDbType.VarWChar, 2,
                "State"));
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramZip", OleDbType.VarWChar, 10,
                "Zip"));
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramSumValue", OleDbType.Integer, 0,
                "SumValue"));
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramID", OleDbType.Integer, 0,
                ParameterDirection.Input, false, 0, 0, "ID", System.Data.DataRowVersion.Original, null));

            // Fill in the schema for auto-increment etc
            daAddresses.FillSchema(dsAddresses, SchemaType.Mapped);

            // Bind the controls to the data source.  Since we are using a data set, we need to use fully
            // qualified names.
            txtFName.DataBindings.Add(new Binding("Text", dsAddresses, "Addresses.FirstName"));
            txtLName.DataBindings.Add(new Binding("Text", dsAddresses, "Addresses.LastName"));
            txtAddress.DataBindings.Add(new Binding("Text", dsAddresses, "Addresses.Address"));
            txtCity.DataBindings.Add(new Binding("Text", dsAddresses, "Addresses.City"));
            cboState.DataBindings.Add(new Binding("SelectedValue", dsAddresses, "Addresses.State"));
            txtZip.DataBindings.Add(new Binding("Text", dsAddresses, "Addresses.Zip"));
            lblKey.DataBindings.Add(new Binding("Text", dsAddresses, "Addresses.ID"));

            // Connect the Row Updated event so that we can retrieve the new primary key values as they are
            // identity values.
            daAddresses.RowUpdated += daAddresses_RowUpdated;

            // Load the state codes for the row template's shared data source
            OleDbDataAdapter daStates = new OleDbDataAdapter("Select State, StateDesc From States", dbConn);

            DataTable dtStates = new DataTable();
            daStates.Fill(dtStates);

            // Add a blank row to allow no selection
            dtStates.Rows.InsertAt(dtStates.NewRow(), 0);

            cboState.DisplayMember = cboState.ValueMember = "State";
            cboState.DataSource = dtStates.DefaultView;
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Get the new primary key on added rows
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void daAddresses_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
            if(e.Status == UpdateStatus.Continue && e.StatementType == StatementType.Insert)
            {
                OleDbCommand cmd = new OleDbCommand("Select @@Identity", dbConn);
                e.Row["ID"] = cmd.ExecuteScalar();
                e.Row.AcceptChanges();
            }
        }

        /// <summary>
        /// Disable or enable the controls based on the change policy
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dnNav_ChangePolicyModified(object sender, ChangePolicyEventArgs e)
        {
            if(pnlData.Enabled != e.AllowEdits || (e.AllowEdits == false && e.AllowAdditions == true))
            {
                pnlData.Enabled = e.AllowEdits;

                // Allow or disallow additions based on the edit policy
                dnNav.AllowAdditions = e.AllowEdits;
            }
        }

        /// <summary>
        /// Require a first and last name
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dnNav_Validating(object sender, CancelEventArgs e)
        {
            epErrors.Clear();

            // If there are no rows, don't bother
            if(pnlData.Enabled)
            {
                if(txtFName.Text.Trim().Length == 0)
                {
                    epErrors.SetError(txtFName, "A first name is required");
                    e.Cancel = true;
                }

                if(txtLName.Text.Trim().Length == 0)
                {
                    epErrors.SetError(txtLName, "A last name is required");
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Clear the errors if edits are canceled
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dnNav_CanceledEdits(object sender, DataNavigatorEventArgs e)
        {
            epErrors.Clear();
        }

        /// <summary>
        /// Disable the bound controls when there are no rows.  The AddedRow event is handled to re-enable the
        /// panel when needed.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dnNav_NoRows(object sender, EventArgs e)
        {
            // Ignore if clearing for reload
            if(!clearingDataSet && dnNav.AllowEdits == true)
            {
                pnlData.Enabled = false;
                lblAddRow.Visible = true;
            }
        }

        /// <summary>
        /// Confirm the deletion of a row from the data source
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dnNav_DeletingRow(object sender, DataNavigatorCancelEventArgs e)
        {
            epErrors.Clear();

            if(MessageBox.Show(String.Format("Are you sure you want to delete the name '{0} {1}'?",
              txtFName.Text, txtLName.Text), "Data Navigator Test", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        /// <summary>
        /// Enable the bound controls when a row exists.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dnNav_AddedRow(object sender, DataNavigatorEventArgs e)
        {
            if(pnlData.Enabled == false && dnNav.AllowEdits == true)
            {
                pnlData.Enabled = true;
                lblAddRow.Visible = false;
            }
        }

        /// <summary>
        /// Refresh the display and the data navigator settings after they have changed
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void pgProps_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            dnNav.Invalidate();
            dnNav.Update();
        }

        /// <summary>
        /// Prompt to save changes if necessary
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void DataNavigatorTestForm_Closing(object sender, CancelEventArgs e)
        {
            DialogResult dr;

            if(dnNav.HasChanges)
            {
                dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your changes, NO " +
                    "to discard them, or CANCEL to stay here and make further changes.", "Data Navigator Test",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

                if(dr == DialogResult.Cancel)
                    e.Cancel = true;
                else
                    if(dr == DialogResult.Yes)
                    {
                        btnSave_Click(sender, e);

                        // If it didn't work, stay here
                        if(dnNav.HasChanges)
                            e.Cancel = true;
                    }
            }
        }

        /// <summary>
        /// Load the data for the data navigator control
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
		private void btnLoad_Click(object sender, EventArgs e)
		{
            DialogResult dr;

            try
            {
                if(dnNav.DataSource == null)
                {
                    // Initial load
                    daAddresses.Fill(dsAddresses);

                    // We could set the DataMember and DataSource properties individually.  This does the same
                    // thing in one step.
                    dnNav.SetDataBinding(dsAddresses, "Addresses");
                }
                else
                {
                    epErrors.Clear();

                    if(dnNav.HasChanges)
                    {
                        dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your " +
                            "changes, NO to discard them, or CANCEL to stay here and make further changes.",
                            "DataList Test", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button3);

                        if(dr == DialogResult.Cancel)
                            return;

                        if(dr == DialogResult.Yes)
                        {
                            btnSave_Click(sender, e);

                            // If it didn't work, don't do anything
                            if(dnNav.HasChanges)
                                return;
                        }
                    }

                    // Reload it
                    pnlData.Enabled = true;
                    lblAddRow.Visible = false;

                    clearingDataSet = true;
                    dsAddresses.Clear();
                    clearingDataSet = false;

                    daAddresses.Fill(dsAddresses);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }
		}

        /// <summary>
        /// Save the changes
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(dnNav.DataSource == null)
                btnLoad_Click(sender, e);
            else
                if(dnNav.IsValid)
                {
                    // We must commit any pending changes
                    dnNav.CommitChanges();

                    daAddresses.Update(dsAddresses);
                }
        }

        /// <summary>
        /// Add a row outside of the data navigator to test its ability to detect and add a new row added in this
        /// manner.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnAddDSRow_Click(object sender, EventArgs e)
        {
            if(dnNav.DataSource == null)
                btnLoad_Click(sender, e);
            else
            {
                // Commit any changes to the current row in case it's the new row.  It's temporary until
                // committed and adding a new row here puts this one ahead of it.
                dnNav.CommitChanges();

                DataRow r = dsAddresses.Tables[0].NewRow();

                r["FirstName"] = "External";
                r["LastName"] = "Row";

                dsAddresses.Tables[0].Rows.Add(r);
                dnNav.MoveTo(RowPosition.LastRow);
            }
        }

        /// <summary>
        /// Delete a row outside of the data navigator to test its ability to detect a row deleted in this manner
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnDelDSRow_Click(object sender, EventArgs e)
        {
            if(dnNav.DataSource == null)
                btnLoad_Click(sender, e);
            else
            {
                // Use the currency manager as the data set's row collection may have been changed and the
                // indexes won't match up to the actual rows.
                CurrencyManager cm = dnNav.ListManager;

                int row = (int)udcRowNumber.Value;

                if(row > 0 && row <= cm.Count)
                {
                    DataRowView drv = (DataRowView)cm.List[row - 1];

                    // If it's an uncommitted new row, just cancel the changes.  Otherwise, delete the row.
                    if(drv.IsNew)
                        dnNav.CancelChanges();
                    else
                        drv.Row.Delete();
                }
                else
                    MessageBox.Show("Not a valid row number");
            }
        }

        /// <summary>
        /// Modify a row outside of the data navigator to test its ability to detect a row changed in this manner
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnModRow_Click(object sender, EventArgs e)
        {
            if(dnNav.DataSource == null)
                btnLoad_Click(sender, e);
            else
            {
                // Commit any other pending changes
                dnNav.CommitChanges();

                // Use the currency manager as the data set's row collection may have been changed and the
                // indexes won't match up to the actual rows.
                CurrencyManager cm = dnNav.ListManager;

                int row = (int)udcRowNumber.Value;

                if(row > 0 && row <= cm.Count)
                {
                    DataRowView drv = (DataRowView)cm.List[row - 1];
                    drv.Row["Address"] = "Modified externally";
                }
                else
                    MessageBox.Show("Not a valid row number");
            }
        }

        /// <summary>
        /// Get a value from the data navigator
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnGetValue_Click(object sender, EventArgs e)
        {
            // This can be any column from the data source regardless of whether or not it is displayed.  Note
            // that you can also use dnNav["ColName"] to get a column value from the item indicated by the
            // CurrentRow property.
            txtValue.Text = String.Format("{0} = {1}", cboColumns.Text, dnNav[(int)txtRowNumber.Value - 1,
                cboColumns.Text]);
        }

        /// <summary>
        /// Find an entry by last name (incremental search)
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtFindName_TextChanged(object sender, EventArgs e)
        {
            int startRow, row;

            if(txtFindName.Text.Length > 0)
            {
                startRow = (txtFindName.Text.Length <= lastSearch.Length) ? -1 : dnNav.CurrentRow - 1;

                lastSearch = txtFindName.Text;
                row = dnNav.FindString("LastName", txtFindName.Text, startRow);

                if(row != -1)
                    dnNav.MoveTo(row);
            }
        }
        #endregion
    }
}
