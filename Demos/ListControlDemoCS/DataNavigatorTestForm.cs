//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : DataNavigatorTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/07/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
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

namespace ListControlDemoCS
{
    /// <summary>
    /// This is used to demonstrate the DataNavigator control
    /// </summary>
    public partial class DataNavigatorTestForm : Form
    {
        #region Private data members
        //=====================================================================

        private DemoDataContext dc = null!;
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

            // Set the data navigator as the object for the property grid
            pgProps.SelectedObject = dnNav;
            pgProps.Refresh();

            // Load data by default
            btnLoad_Click(this, EventArgs.Empty);
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Disable or enable the controls based on the change policy
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dnNav_ChangePolicyModified(object sender, ChangePolicyEventArgs e)
        {
            if(pnlData.Enabled != e.AllowEdits || (!e.AllowEdits && e.AllowAdditions))
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
            if(dnNav.AllowEdits)
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

            if(MessageBox.Show($"Are you sure you want to delete the name '{txtFName.Text} {txtLName.Text}'?",
              "Data Navigator Test", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Enable the bound controls when a row exists.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dnNav_AddedRow(object sender, DataNavigatorEventArgs e)
        {
            if(!pnlData.Enabled && dnNav.AllowEdits)
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

            if(dc.ChangeTracker.HasChanges())
            {
                dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your changes, NO " +
                    "to discard them, or CANCEL to stay here and make further changes.", "Data Navigator Test",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);

                if(dr == DialogResult.Cancel)
                    e.Cancel = true;
                else
                {
                    if(dr == DialogResult.Yes)
                    {
                        btnSave_Click(sender, e);

                        // If it didn't work, stay here
                        if(dc.ChangeTracker.HasChanges())
                            e.Cancel = true;
                    }
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
                epErrors.Clear();

                if(dc?.ChangeTracker.HasChanges() ?? false)
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
                        if(dc.ChangeTracker.HasChanges())
                            return;
                    }
                }

                // Reload the data.  Create a new data context since we're reloading the information.  The
                // old context may have changes we no longer care about.
                dc = new DemoDataContext();

                pnlData.Enabled = true;
                lblAddRow.Visible = false;

                if(cboState.DataSource == null)
                {
                    var states = dc.StateCodes.ToList();

                    states.Insert(0, new StateCode { State = String.Empty, StateDesc = String.Empty });

                    cboState.DisplayMember = cboState.ValueMember = nameof(StateCode.State);
                    cboState.DataSource = states;
                }

                // For entity framework we need to load the entities
                dc.Addresses.Load();

                // Apply a sort by last name
                var pdc = TypeDescriptor.GetProperties(typeof(Address));
                var pd = pdc[nameof(Address.LastName)];
                var dataSource = dc.Addresses.Local.ToObservableCollection().ToBindingList();

                ((IBindingList)dataSource).ApplySort(pd!, ListSortDirection.Ascending);

                // Bind the controls to the data source
                txtFName.DataBindings.Clear();
                txtLName.DataBindings.Clear();
                txtAddress.DataBindings.Clear();
                txtCity.DataBindings.Clear();
                cboState.DataBindings.Clear();
                txtZip.DataBindings.Clear();
                lblKey.DataBindings.Clear();

                txtFName.DataBindings.Add(new Binding(nameof(Control.Text), dataSource, nameof(Address.FirstName)));
                txtLName.DataBindings.Add(new Binding(nameof(Control.Text), dataSource, nameof(Address.LastName)));
                txtAddress.DataBindings.Add(new Binding(nameof(Control.Text), dataSource, nameof(Address.StreetAddress)));
                txtCity.DataBindings.Add(new Binding(nameof(Control.Text), dataSource, nameof(Address.City)));
                cboState.DataBindings.Add(new Binding(nameof(MultiColumnComboBox.SelectedValue), dataSource,
                    nameof(Address.State)));
                txtZip.DataBindings.Add(new Binding(nameof(Control.Text), dataSource, nameof(Address.Zip)));
                lblKey.DataBindings.Add(new Binding(nameof(Control.Text), dataSource, nameof(Address.ID)));

                // We must enable formatting or the bound values for these control types won't get updated for
                // some reason.  They also need to update on the property value changing in case the mouse wheel
                // is used.
                cboState.DataBindings[0].FormattingEnabled = true;
                cboState.DataBindings[0].DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

                // We could set each binding property individually, but this is more efficient
                dnNav.SetDataBinding(dataSource, null);
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
                if(dnNav.IsValid)
                {
                    // We must commit any pending changes
                    dnNav.CommitChanges();

                    // There may be a row added for the placeholder that needs removing
                    var removeRows = dc.ChangeTracker.Entries<Address>().Select(a => a.Entity).Where(
                        a => a.LastName == null).ToList();

                    if(removeRows.Count != 0)
                        dc.RemoveRange(removeRows);

                    dc.SaveChanges();
                }
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

                var bl = (BindingList<Address>)dnNav.DataSource;

                bl.Add(new Address
                {
                    FirstName = "External",
                    LastName = "Row",
                    LastModified = BitConverter.GetBytes(DateTime.Now.Ticks)
                });

                dnNav.MoveTo(RowPosition.LastRow);
                dnNav.Focus();
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
                CurrencyManager cm = dnNav.ListManager!;

                int row = (int)udcRowNumber.Value;

                if(row > 0 && row <= cm.Count)
                {
                    dnNav.CancelChanges();

                    var bl = (BindingList<Address>)dnNav.DataSource;

                    bl.RemoveAt(row - 1);
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
                CurrencyManager cm = dnNav.ListManager!;

                int row = (int)udcRowNumber.Value;

                if(row > 0 && row <= cm.Count)
                {
                    var address = (Address)cm.List[row - 1]!;
                    address.StreetAddress = "Modified externally";

                    dnNav.MoveTo(row - 1);
                    dnNav.Focus();
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
            txtValue.Text = $"{cboColumns.Text} = {dnNav[(int)txtRowNumber.Value - 1, cboColumns.Text]}";
        }

        /// <summary>
        /// Find an entry by last name (incremental search)
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtFindName_TextChanged(object sender, EventArgs e)
        {
            if(txtFindName.Text.Length > 0)
            {
                int startRow = (txtFindName.Text.Length <= lastSearch.Length) ? -1 : dnNav.CurrentRow - 1;

                lastSearch = txtFindName.Text;
                
                int row = dnNav.FindString(nameof(Address.LastName), txtFindName.Text, startRow);

                if(row != -1)
                    dnNav.MoveTo(row);
            }
        }
        #endregion
    }
}
