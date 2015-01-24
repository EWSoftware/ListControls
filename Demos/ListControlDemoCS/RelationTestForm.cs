//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : RelationTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/02/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This is used to demonstrate how the data navigator and data list controls can be used with related data
// sources in a data set.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/23/2005  EFW  Created the code
// 05/05/2007  EFW  Added demo of data binding the CheckBoxList and RadioButtonList controls
//===============================================================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

using EWSoftware.ListControls;

namespace ListControlDemoCS
{
	/// <summary>
    /// This is used to demonstrate how the data navigator and data list controls can be used with related data
    /// sources in a data set.
	/// </summary>
	public partial class RelationTestForm : System.Windows.Forms.Form
	{
        #region Private data members
        //=====================================================================

        private OleDbConnection dbConn;
        private OleDbDataAdapter daAddresses, daPhones;
        private DataSet dsAddresses;
        private bool clearingDataSet;
        private string lastSearch;
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public RelationTestForm()
		{
			InitializeComponent();

            lastSearch = String.Empty;

            // Create the data source for the demo
            CreateDataSource();

            // Load data by default
            btnLoad_Click(this, EventArgs.Empty);
		}
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Create the data source for the demo
        /// </summary>
        /// <remarks>You can use the designer to create the data source and use strongly typed data sets.  For
        /// this demo, we'll do it by hand.
        /// </remarks>
        private void CreateDataSource()
        {
            // The test database should be in the project folder
            dbConn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\TestData.mdb");
            daAddresses = new OleDbDataAdapter();
            daPhones = new OleDbDataAdapter();
            dsAddresses = new DataSet();

            // Set the table name
            daAddresses.TableMappings.Add("Table", "Addresses");

            // In a real application we wouldn't use literal SQL but we will for the demo
            daAddresses.SelectCommand = new OleDbCommand("Select * From Addresses Order By LastName", dbConn);

            daAddresses.DeleteCommand = new OleDbCommand("Delete From Addresses Where ID = @paramID", dbConn);
            daAddresses.DeleteCommand.Parameters.Add(new OleDbParameter("@paramID", OleDbType.Integer, 0,
                ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Original, null));

            daAddresses.InsertCommand = new OleDbCommand(
                "INSERT INTO Addresses (FirstName, LastName, Address, City, State, Zip, SumValue, Domestic, " +
                "International, Postal, Parcel, Home, Business, ContactType) " +
                "VALUES (@paramFN, @paramLN, @paramAddress, @paramCity, @paramState, @paramZip, " +
                "@paramSumValue, @paramDomestic, @paramInternational, @paramPostal, @paramParcel, " +
                "@paramHome, @paramBusiness, @paramContactType)", dbConn);
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramFirstName", OleDbType.VarWChar,
                20, "FirstName"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramLastName", OleDbType.VarWChar,
                30, "LastName"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramAddress", OleDbType.VarWChar,
                50, "Address"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramCity", OleDbType.VarWChar, 20,
                "City"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramState", OleDbType.VarWChar, 2,
                "State"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramZip", OleDbType.VarWChar, 10,
                "Zip"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramSumValue", OleDbType.Integer, 0,
                "SumValue"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramDomestic", OleDbType.Boolean, 0,
                "Domestic"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramInternational", OleDbType.Boolean,
                0, "International"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramPostal", OleDbType.Boolean, 0,
                "Postal"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramParcel", OleDbType.Boolean, 0,
                "Parcel"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramHome", OleDbType.Boolean, 0,
                "Home"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramBusiness", OleDbType.Boolean, 0,
                "Business"));
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramContactType", OleDbType.Char, 1,
                "ContactType"));

            daAddresses.UpdateCommand = new OleDbCommand(
                "UPDATE Addresses SET FirstName = @paramFirstName, LastName = @paramLastName, " +
                "Address = @paramAddress, City = @paramCity, State = @paramState, Zip = @paramZip, " +
                "SumValue = @paramSumValue, Domestic = @paramDomestic, International = @paramInternational, " +
                "Postal = @paramPostal, Parcel = @paramParcel, Home = @paramHome, Business = @paramBusiness, " +
                "ContactType = @paramContactType WHERE ID = @paramID", dbConn);
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
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramDomestic", OleDbType.Boolean, 0,
                "Domestic"));
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramInternational", OleDbType.Boolean,
                0, "International"));
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramPostal", OleDbType.Boolean, 0,
                "Postal"));
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramParcel", OleDbType.Boolean, 0,
                "Parcel"));
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramHome", OleDbType.Boolean, 0,
                "Home"));
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramBusiness", OleDbType.Boolean, 0,
                "Business"));
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramContactType", OleDbType.Char, 1,
                "ContactType"));
            daAddresses.UpdateCommand.Parameters.Add(new OleDbParameter("@paramID", OleDbType.Integer, 0,
                ParameterDirection.Input, false, 0, 0, "ID", System.Data.DataRowVersion.Original, null));

            // Fill in the schema for auto-increment etc.
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

            // The checkboxes in the checkbox list can also be bound to members of a different data source.  To
            // do this, set the BindingMembersDataSource and specify the members to which each checkbox is bound
            // in the BindingMembers collection property.  In this demo, the BindingMembers are specified via the
            // designer.  However, we could do it in code as well as shown here:
            //
            // cblAddressTypes.BindingMembers.AddRange(new string[] { "Addresses.Domestic",
            //     "Addresses.International", "Addresses.Postal", "Addresses.Parcel", "Addresses.Home",
            //     "Addresses.Business"});
            //
            // As above, since we are binding to a DataSet, we must specify the fully qualified names.  Also note
            // that there is a BindingMembersBindingContext property that can be set if the binding members data
            // source is in a binding context different than the checkbox list's data source.
            //
            // Note that we could assign a data source for the checkbox list items as well similar to the radio
            // button list data source below but in this case, the list is simple so it's added to the Items
            // collection via the designer.
            cblAddressTypes.BindingMembersDataSource = dsAddresses;

            // Create the data source for the radio button list items
            List<ListItem> contactTypeList = new List<ListItem>();
            contactTypeList.Add(new ListItem("B", "Business"));
            contactTypeList.Add(new ListItem("P", "Personal"));
            contactTypeList.Add(new ListItem("O", "Other"));

            rblContactType.DisplayMember = "Display";
            rblContactType.ValueMember = "Value";
            rblContactType.DataSource = contactTypeList;

            // Bind the radio button list to the ContactType field.  Since it can be null, we'll add a Parse
            // event to default the value to "Business" if it's null.  This wouldn't be needed for fields that
            // are never null (i.e. those with a default value).
            Binding b = new Binding("SelectedValue", dsAddresses, "Addresses.ContactType");
            b.Format += ContactType_Format;
            rblContactType.DataBindings.Add(b);

            // Set up the phone info data adapter
            daPhones.SelectCommand = new OleDbCommand("Select * From Phones Order By ID, PhoneNumber", dbConn);

            daPhones.DeleteCommand = new OleDbCommand("Delete From Phones Where PhoneKey = @paramPhoneKey", dbConn);
            daPhones.DeleteCommand.Parameters.Add(new OleDbParameter("@paramPhoneKey", OleDbType.Integer, 0,
                ParameterDirection.Input, false, 0, 0, "PhoneKey", DataRowVersion.Original, null));

            daPhones.InsertCommand = new OleDbCommand("INSERT INTO Phones (ID, PhoneNumber) VALUES (@paramID, " +
                "@paramPhoneNumber)", dbConn);
            daPhones.InsertCommand.Parameters.Add(new OleDbParameter("@paramID", OleDbType.Integer, 0, "ID"));
            daPhones.InsertCommand.Parameters.Add(new OleDbParameter("@paramPhoneNumber", OleDbType.VarWChar, 20,
                "PhoneNumber"));

            daPhones.UpdateCommand = new OleDbCommand(
                "UPDATE Phones SET PhoneNumber = @paramPhoneNumber WHERE PhoneKey = @paramPhoneKey", dbConn);
            daPhones.UpdateCommand.Parameters.Add(new OleDbParameter("@paramPhoneNumber", OleDbType.VarWChar, 20,
                "PhoneNumber"));
            daPhones.UpdateCommand.Parameters.Add(new OleDbParameter("@paramPhoneKey", OleDbType.Integer, 0,
                ParameterDirection.Input, false, 0, 0, "PhoneKey", DataRowVersion.Original, null));

            // Connect the Row Updated event so that we can retrieve the new primary key values as they are
            // identity values.
            daPhones.RowUpdated += daPhones_RowUpdated;

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
        /// This converts null values to a default type for the bound radio button list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void ContactType_Format(object sender, ConvertEventArgs e)
        {
            if(e.Value == null || e.Value == DBNull.Value)
                e.Value = "B";
        }

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
        /// Get the new primary key on added rows
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void daPhones_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
            if(e.Status == UpdateStatus.Continue && e.StatementType == StatementType.Insert)
            {
                OleDbCommand cmd = new OleDbCommand("Select @@Identity", dbConn);
                e.Row["PhoneKey"] = cmd.ExecuteScalar();
                e.Row.AcceptChanges();
            }

            // Ignore deletions that don't find their row.  The cascade delete took care of them already.
            if(e.StatementType == StatementType.Delete && e.RecordsAffected == 0 && e.Status == UpdateStatus.ErrorsOccurred)
            {
                e.Status = UpdateStatus.Continue;
                e.Row.AcceptChanges();
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
                if(!dlPhones.IsValid)
                    e.Cancel = true;
                else
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

                // If not canceled, commit the changes.  This is needed so that the phone data list will receive
                // the key values when rows are added to it.
                if(!e.Cancel)
                    dnNav.CommitChanges();
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
                dlPhones.Enabled = false;
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
              txtFName.Text, txtLName.Text), "Relationship Test", MessageBoxButtons.YesNo,
              MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                e.Cancel = true;
        }

        /// <summary>
        /// Enable the bound controls when a row exists
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dnNav_AddedRow(object sender, DataNavigatorEventArgs e)
        {
            if(pnlData.Enabled == false && dnNav.AllowEdits == true)
            {
                pnlData.Enabled = true;
                lblAddRow.Visible = false;
                dlPhones.Enabled = true;
            }
        }

        /// <summary>
        /// Prompt to save changes if necessary
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void RelationTestForm_Closing(object sender, CancelEventArgs e)
        {
            DialogResult dr;

            if(dnNav.HasChanges || dlPhones.HasChanges)
            {
                dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your changes, NO " +
                    "to discard them, or CANCEL to stay here and make further changes.", "Relationship Test",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

                if(dr == DialogResult.Cancel)
                    e.Cancel = true;
                else
                    if(dr == DialogResult.Yes)
                    {
                        btnSave_Click(sender, e);

                        // If it didn't work, stay here
                        if(dnNav.HasChanges || dlPhones.HasChanges)
                            e.Cancel = true;
                    }
            }
        }

        /// <summary>
        /// Load the data into the controls
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
                    // Fill the data set.  Both tables are related so they go in the same data set
                    daAddresses.Fill(dsAddresses, "Addresses");
                    daPhones.Fill(dsAddresses, "Phones");

                    // Relate the two tables
                    dsAddresses.Relations.Add("AddrPhone", dsAddresses.Tables["Addresses"].Columns["ID"],
                        dsAddresses.Tables["Phones"].Columns["ID"]);

                    // We could set the DataMember and DataSource properties individually.  This does the same
                    // thing in one step.
                    dnNav.SetDataBinding(dsAddresses, "Addresses");

                    // Use the relationship as the data source for the phone number data list
                    dlPhones.SetDataBinding(dsAddresses, "Addresses.AddrPhone", typeof(PhoneRow));
                }
                else
                {
                    epErrors.Clear();

                    if(dnNav.HasChanges || dlPhones.HasChanges)
                    {
                        dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your " +
                            "changes, NO to discard them, or CANCEL to stay here and make further changes.",
                            "Relationship Test", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button3);

                        if(dr == DialogResult.Cancel)
                            return;

                        if(dr == DialogResult.Yes)
                        {
                            btnSave_Click(sender, e);

                            // If it didn't work, don't do anything
                            if(dnNav.HasChanges || dlPhones.HasChanges)
                                return;
                        }
                    }

                    // Reload it
                    pnlData.Enabled = true;
                    lblAddRow.Visible = false;

                    // When using a related data list, you must clear its data source and reset it afterwards
                    // when reloading the data set.  If not, it may generate errors as it tries to rebind on data
                    // that no longer exists.  This is a problem with the way data binding works in .NET.
                    dlPhones.DataSource = null;

                    clearingDataSet = true;
                    dsAddresses.Clear();
                    clearingDataSet = false;

                    daAddresses.Fill(dsAddresses, "Addresses");
                    daPhones.Fill(dsAddresses, "Phones");

                    // Reset the data source on the related phone list
                    dlPhones.DataMember = "Addresses.AddrPhone";
                    dlPhones.DataSource = dsAddresses;
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
            {
                if(dnNav.IsValid && dlPhones.IsValid)
                {
                    // We must commit any pending changes
                    dnNav.CommitChanges();
                    dlPhones.CommitChanges();

                    daAddresses.Update(dsAddresses.Tables["Addresses"]);
                    daPhones.Update(dsAddresses.Tables["Phones"]);
                }
            }
        }

        /// <summary>
        /// Prevent phone information from being entered if the parent row doesn't yet exist in the related data
        /// source or is not valid.
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dlPhones_AddingRow(object sender, DataListCancelEventArgs e)
        {
            DataRowView drv = (DataRowView)dnNav.ListManager.List[dnNav.CurrentRow];

            if(drv.IsNew)
            {
                // The add must always be canceled
                e.Cancel = true;

                // If not valid, go back to the panel.  If valid, we need to rebind or the related data source
                // doesn't always pick up on the fact that the parent row got added.
                if(!dnNav.IsValid)
                    txtLName.Focus();
                else
                    dlPhones.SetDataBinding(dsAddresses, "Addresses.AddrPhone", typeof(PhoneRow));
            }
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
