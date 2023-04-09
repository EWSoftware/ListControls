//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : DataListTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/06/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
//
// This is used to demonstrate the DataList and TemplateControl controls.
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
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using EWSoftware.ListControls;

namespace ListControlDemoCS
{
	/// <summary>
	/// This is used to demonstrate the DataList and TemplateControl controls.
	/// </summary>
	public partial class DataListTestForm : System.Windows.Forms.Form
	{
        #region Private data members
        //=====================================================================

        private OleDbConnection dbConn;
        private OleDbDataAdapter daAddresses;
        private DataSet dsAddresses;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public DataListTestForm()
		{
			InitializeComponent();

            // Create the data source for the demo
            CreateDataSource();

            // Set the data list as the object for the property grid
            pgProps.SelectedObject = dlList;
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
            dbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\TestData.mdb");
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
            daAddresses.InsertCommand.Parameters.Add(new OleDbParameter("@paramLastName", OleDbType.VarWChar,30,
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

            // Connect the Row Updated event so that we can retrieve the new primary key values as they are
            // identity values.
            daAddresses.RowUpdated += daAddresses_RowUpdated;

            // Load the state codes for the row template's shared data source
            using(var daStates = new OleDbDataAdapter("Select State, StateDesc From States", dbConn))
            {
                DataTable dtStates = new DataTable();
                daStates.Fill(dtStates);

                // Add a blank row to allow no selection
                dtStates.Rows.InsertAt(dtStates.NewRow(), 0);

                dlList.SharedDataSources.Add("States", dtStates.DefaultView);
            }
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
                using(var cmd = new OleDbCommand("Select @@Identity", dbConn))
                {
                    e.Row["ID"] = cmd.ExecuteScalar();
                    e.Row.AcceptChanges();
                }
            }
        }

        /// <summary>
        /// Change the color of the row based on the zip code when a row is data bound
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
		private void dlList_ItemDataBound(object sender, DataListEventArgs e)
		{
		    if(dlList[e.Index, "Zip"].ToString() == "98122")
                e.Item.BackColor = Color.LightSteelBlue;
            else
                e.Item.BackColor = SystemColors.Control;
		}

        /// <summary>
        /// Refresh the display and the data list settings after they have changed
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void pgProps_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            dlList.Invalidate();
            dlList.Update();
        }

        /// <summary>
        /// Prompt to save changes if necessary
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void DataListTestForm_Closing(object sender, CancelEventArgs e)
        {
            DialogResult dr;

            if(dlList.HasChanges)
            {
                dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your changes, NO " +
                    "to discard them, or CANCEL to stay here and make further changes.", "DataList Test",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

                if(dr == DialogResult.Cancel)
                    e.Cancel = true;
                else
                    if(dr == DialogResult.Yes)
                    {
                        btnSave_Click(sender, e);

                        // If it didn't work, stay here
                        if(dlList.HasChanges)
                            e.Cancel = true;
                    }
            }
        }

        /// <summary>
        /// Load the data into the data list control
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
		private void btnLoad_Click(object sender, EventArgs e)
		{
            DialogResult dr;

            try
            {
                if(dlList.DataSource == null)
                {
                    // Initial load
                    daAddresses.Fill(dsAddresses);

                    // We could set each property individually, but this is more efficient.  Since we are using
                    // the DataSet as the data source, we must specify the data member as well.
                    dlList.SetDataBinding(dsAddresses, "Addresses", typeof(AddressRow), typeof(AddressHeader),
                        typeof(AddressFooter));
                }
                else
                {
                    if(dlList.HasChanges)
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
                            if(dlList.HasChanges)
                                return;
                        }
                    }

                    // Reload it
                    dsAddresses.Clear();
                    daAddresses.Fill(dsAddresses);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message);
            }

            dlList.Focus();
		}

        /// <summary>
        /// Save the changes
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(dlList.DataSource == null)
                btnLoad_Click(sender, e);
            else
                if(dlList.IsValid)
                {
                    // We must commit any pending changes
                    dlList.CommitChanges();

                    daAddresses.Update(dsAddresses);
                }
        }

        /// <summary>
        /// Add a row outside of the data list to test its ability to detect and add a new row added in this
        /// manner.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnAddDSRow_Click(object sender, EventArgs e)
        {
            if(dlList.DataSource == null)
                btnLoad_Click(sender, e);
            else
            {
                // Commit any changes to the current row in case it's the new row.  It's temporary until
                // committed and adding a new row here puts this one ahead of it.
                dlList.CommitChanges();

                DataRow r = dsAddresses.Tables[0].NewRow();

                r["FirstName"] = "External";
                r["LastName"] = "Row";

                dsAddresses.Tables[0].Rows.Add(r);
            }
        }

        /// <summary>
        /// Delete a row outside of the data list to test its ability to detect a row deleted in this manner
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnDelDSRow_Click(object sender, EventArgs e)
        {
            if(dlList.DataSource == null)
                btnLoad_Click(sender, e);
            else
            {
                // Use the currency manager as the data set's row collection may have been changed and the
                // indexes won't match up to the actual rows.
                CurrencyManager cm = dlList.ListManager;

                int row = (int)udcRowNumber.Value;

                if(row > 0 && row <= cm.Count)
                {
                    DataRowView drv = (DataRowView)cm.List[row - 1];

                    // If it's an uncommitted new row, just cancel the changes.  Otherwise, delete the row.
                    if(drv.IsNew)
                        dlList.CancelChanges();
                    else
                        drv.Row.Delete();
                }
                else
                    MessageBox.Show("Not a valid row number");
            }
        }

        /// <summary>
        /// Modify a row outside of the data list to test its ability to detect a row changed in this manner
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnModRow_Click(object sender, EventArgs e)
        {
            if(dlList.DataSource == null)
                btnLoad_Click(sender, e);
            else
            {
                // Commit any other pending changes
                dlList.CommitChanges();

                // Use the currency manager as the data set's row collection may have been changed and the
                // indexes won't match up to the actual rows.
                CurrencyManager cm = dlList.ListManager;

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
        /// Get a value from the data list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnGetValue_Click(object sender, EventArgs e)
        {
            // This can be any column from the data source regardless of whether or not it is displayed.  Note
            // that you can also use dlList["ColName"] to get a column value from the item indicated by the
            // CurrentRow property.
            txtValue.Text = $"{cboColumns.Text} = {dlList[(int)txtRowNumber.Value - 1, cboColumns.Text]}";
        }

        /// <summary>
        /// Show or hide the data list's header
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void chkShowHeader_CheckedChanged(object sender, EventArgs e)
        {
            if(chkShowHeader.Checked)
                dlList.HeaderTemplate = typeof(AddressHeader);
            else
                dlList.HeaderTemplate = null;
        }

        /// <summary>
        /// Show or hide the data list's footer
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void chkShowFooter_CheckedChanged(object sender, EventArgs e)
        {
            if(chkShowFooter.Checked)
                dlList.FooterTemplate = typeof(AddressFooter);
            else
                dlList.FooterTemplate = null;
        }

        /// <summary>
        /// Test drag and drop with the data list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dlList_BeginDrag(object sender, DataListBeginDragEventArgs e)
        {
            // Commit any pending changes before the drag operation begins
            dlList.CommitChanges();

            this.DoDragDrop(e, DragDropEffects.Copy | DragDropEffects.Move);
        }

        /// <summary>
        /// Allow drag and drop from data list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtValue_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(typeof(DataListBeginDragEventArgs)))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// Handle drop from data list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtValue_DragDrop(object sender, DragEventArgs e)
        {
            DataRowView drv;
            StringBuilder sb;

            if(!e.Data.GetDataPresent(typeof(DataListBeginDragEventArgs)))
                return;

            DataListBeginDragEventArgs dragArgs = (DataListBeginDragEventArgs)e.Data.GetData(
                typeof(DataListBeginDragEventArgs));

            sb = new StringBuilder("Dropped IDs: ");

            // Use the data list's currency manager
            CurrencyManager cm = dlList.ListManager;

            // Just list the IDs of the selection
            for(int idx = dragArgs.SelectionStart; idx <= dragArgs.SelectionEnd; idx++)
            {
                if(idx != dragArgs.SelectionStart)
                    sb.Append(',');

                drv = (DataRowView)cm.List[idx];

                sb.Append(drv["ID"]);
            }

            txtValue.Text = sb.ToString();
        }

        /// <summary>
        /// Allow drag and drop from within the data list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dlList_DragEnter(object sender, DragEventArgs e)
        {
            if(!e.Data.GetDataPresent(typeof(DataListBeginDragEventArgs)))
                e.Effect = DragDropEffects.None;
            else
                e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// Only allow drop if over a row or a row header but not within the current selection
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dlList_DragOver(object sender, DragEventArgs e)
        {
            DataListBeginDragEventArgs dragArgs = (DataListBeginDragEventArgs)e.Data.GetData(
                typeof(DataListBeginDragEventArgs));

            DataListHitTestInfo hti = dragArgs.Source.HitTest(dlList.PointToClient(Cursor.Position));

            if((hti.Type & DataListHitType.RowOrHeader) != 0 && (hti.Row < dragArgs.SelectionStart ||
              hti.Row > dragArgs.SelectionEnd))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// Handle the drop operation.  This doesn't do anything interesting yet.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dlList_DragDrop(object sender, DragEventArgs e)
        {
            if(!e.Data.GetDataPresent(typeof(DataListBeginDragEventArgs)))
                return;

            DataListBeginDragEventArgs dragArgs = (DataListBeginDragEventArgs)e.Data.GetData(
                typeof(DataListBeginDragEventArgs));

            DataListHitTestInfo hti = dragArgs.Source.HitTest(dlList.PointToClient(Cursor.Position));

            if((hti.Type & DataListHitType.RowOrHeader) != 0 && (hti.Row < dragArgs.SelectionStart ||
              hti.Row > dragArgs.SelectionEnd))
            {
                MessageBox.Show($"Selection dropped on row {hti.Row + 1}");

                dlList.MoveTo(hti.Row);
                dlList.Select(hti.Row, hti.Row, hti.Row);
            }
        }
        #endregion
    }
}
