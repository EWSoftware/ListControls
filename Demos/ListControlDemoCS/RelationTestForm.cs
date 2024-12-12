//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : RelationTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/08/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
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

namespace ListControlDemoCS
{
    /// <summary>
    /// This is used to demonstrate how the data navigator and data list controls can be used with related data
    /// sources in a data set.
    /// </summary>
    public partial class RelationTestForm : Form
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
		public RelationTestForm()
        {
            InitializeComponent();

            lastSearch = String.Empty;

            dlPhones.RowTemplate = typeof(PhoneRow);

            // Load data by default
            btnLoad_Click(this, EventArgs.Empty);
        }
        #endregion

        #region Event handlers
        //=====================================================================

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
            if(dnNav.AllowEdits)
            {
                lblAddRow.Visible = true;
                pnlData.Enabled = dlPhones.Enabled = false;
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
              "Relationship Test", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
              MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Enable the bound controls when a row exists
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dnNav_AddedRow(object sender, DataNavigatorEventArgs e)
        {
            if(!pnlData.Enabled && dnNav.AllowEdits)
            {
                lblAddRow.Visible = false;
                pnlData.Enabled = dlPhones.Enabled = true;
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

            if(dc.ChangeTracker.HasChanges())
            {
                dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your changes, NO " +
                    "to discard them, or CANCEL to stay here and make further changes.", "Relationship Test",
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
        /// Load the data into the controls
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
                        "Relationship Test", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question,
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
                dc.PhoneNumbers.Load();

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
                rblContactType.DataBindings.Clear();

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

                // When using a related data list, you must clear its data source and reset it afterwards
                // when reloading the data set.  If not, it may generate errors as it tries to rebind on data
                // that no longer exists.  This is a problem with the way data binding works in .NET.
                dlPhones.DataSource = null;

                // The checkboxes in the checkbox list can also be bound to members of a different data source.  To
                // do this, set the BindingMembersDataSource and specify the members to which each checkbox is bound
                // in the BindingMembers collection property.  In this demo, the BindingMembers are specified via the
                // designer.  However, we could do it in code as well as shown here:
                //
                // cblAddressTypes.BindingMembers.AddRange(new[] { "Domestic", "International", "Postal", "Parcel",
                //     "Home", "Business" });
                //
                // Also note that there is a BindingMembersBindingContext property that can be set if the binding
                // members data source is in a binding context different than the checkbox list's data source.
                //
                // Note that we could assign a data source for the checkbox list items as well similar to the radio
                // button list data source below but in this case, the list is simple so it's added to the Items
                // collection via the designer.
                cblAddressTypes.BindingMembersDataSource = dataSource;

                // Create the data source for the radio button list items
                List<ListItem> contactTypeList =
                [
                    new('B', "Business"),
                    new('P', "Personal"),
                    new('O', "Other")
                ];

                rblContactType.DisplayMember = "Display";
                rblContactType.ValueMember = "Value";
                rblContactType.DataSource = contactTypeList;

                // Bind the radio button list to the ContactType field.  Since it can be null, we'll add a Format
                // event to default the value to "Business" if it's null.  This wouldn't be needed for fields that
                // are never null (i.e. those with a default value).
                Binding b = new(nameof(RadioButtonList.SelectedValue), dataSource, nameof(Address.ContactType));
                b.Format += (s, e) => e.Value ??= 'B';
                b.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
                b.FormattingEnabled = true;

                rblContactType.DataBindings.Add(b);

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
                if(dc.ChangeTracker.HasChanges())
                {
                    // We must commit any pending changes
                    dnNav.CommitChanges();
                    dlPhones.CommitChanges();

                    // There may be a row added for the placeholder that needs removing
                    var removeAddresses = dc.ChangeTracker.Entries<Address>().Select(a => a.Entity).Where(
                        a => a.LastName == null).ToList();

                    if(removeAddresses.Count != 0)
                        dc.RemoveRange(removeAddresses);

                    var removePhones = dc.ChangeTracker.Entries<Phone>().Select(a => a.Entity).Where(
                        a => a.PhoneNumber == null).ToList();

                    if(removePhones.Count != 0)
                        dc.RemoveRange(removePhones);

                    dc.SaveChanges();
                }
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

        /// <summary>
        /// To keep the data context aware of additions and deletion automatically would likely require a custom
        /// binding list.  As such, we'll handle adding and removing phone numbers from the data context.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments.</param>
        private void dnNav_Current(object sender, DataNavigatorEventArgs e)
        {
            Address? address = (Address?)dnNav.ListManager?.List[dnNav.CurrentRow];

            if(address != null)
                dlPhones.DataSource = new BindingList<Phone>([.. address.PhoneNumbers]);
            else
                dlPhones.DataSource = null;
        }

        /// <summary>
        /// Add a new phone number to the address
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments.</param>
        private void dlPhones_AddedRow(object sender, DataListEventArgs e)
        {
            Address address = (Address?)dnNav.ListManager!.List[dnNav.CurrentRow]!;
            Phone phone = (Phone)e.Item!.RowSource!;

            address.PhoneNumbers.Add(phone);
        }

        /// <summary>
        /// Remove a deleted phone entry
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments.</param>
        private void dlPhones_DeletingRow(object sender, DataListCancelEventArgs e)
        {
            Address address = (Address?)dnNav.ListManager!.List[dnNav.CurrentRow]!;
            Phone phone = (Phone)e.Item!.RowSource!;

            address.PhoneNumbers.Remove(phone);
        }
        #endregion
    }
}
