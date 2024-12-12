//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : DataListTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/07/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
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


using System.Text;

namespace ListControlDemoCS
{
    /// <summary>
    /// This is used to demonstrate the DataList and TemplateControl controls.
    /// </summary>
    public partial class DataListTestForm : Form
    {
        #region Private data members
        //=====================================================================

        private DemoDataContext dc = null!;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public DataListTestForm()
        {
            InitializeComponent();

            // Set the data list as the object for the property grid
            pgProps.SelectedObject = dlList;
            pgProps.Refresh();

            // Load data by default
            btnLoad_Click(this, EventArgs.Empty);
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Change the color of the row based on the zip code when a row is data bound
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
		private void dlList_ItemDataBound(object sender, DataListEventArgs e)
        {
            if(dlList[e.Index, nameof(Address.Zip)]?.ToString() == "98122")
                e.Item!.BackColor = Color.LightSteelBlue;
            else
                e.Item!.BackColor = SystemColors.Control;
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

            if(dc.ChangeTracker.HasChanges())
            {
                dr = MessageBox.Show("Do you want to save your changes?  Click YES to save your changes, NO " +
                    "to discard them, or CANCEL to stay here and make further changes.", "DataList Test",
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
        /// Load the data into the data list control
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
		private void btnLoad_Click(object sender, EventArgs e)
        {
            DialogResult dr;

            try
            {
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

                if(!dlList.SharedDataSources.ContainsKey("States"))
                {
                    var states = dc.StateCodes.ToList();

                    states.Insert(0, new StateCode { State = String.Empty, StateDesc = String.Empty });

                    dlList.SharedDataSources.Add("States", states);
                }

                // For entity framework we need to load the entities
                dc.Addresses.Load();

                // Apply a sort by last name
                var pdc = TypeDescriptor.GetProperties(typeof(Address));
                var pd = pdc[nameof(Address.LastName)];
                var dataSource = dc.Addresses.Local.ToObservableCollection().ToBindingList();

                ((IBindingList)dataSource).ApplySort(pd!, ListSortDirection.Ascending);

                // We could set each binding property individually, but this is more efficient
                dlList.SetDataBinding(dataSource, null, typeof(AddressRow), typeof(AddressHeader),
                    typeof(AddressFooter));
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
            {
                if(dlList.IsValid)
                {
                    // We must commit any pending changes
                    dlList.CommitChanges();

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

                var bl = (BindingList<Address>)dlList.DataSource;

                bl.Add(new Address
                {
                    FirstName = "External",
                    LastName = "Row",
                    LastModified = BitConverter.GetBytes(DateTime.Now.Ticks)
                });

                dlList.MoveTo(RowPosition.LastRow);
                dlList.Focus();
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
                CurrencyManager cm = dlList.ListManager!;

                int row = (int)udcRowNumber.Value;

                if(row > 0 && row <= cm.Count)
                {
                    dlList.CancelChanges();

                    var bl = (BindingList<Address>)dlList.DataSource;

                    bl.RemoveAt(row - 1);
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
                CurrencyManager cm = dlList.ListManager!;

                int row = (int)udcRowNumber.Value;

                if(row > 0 && row <= cm.Count)
                {
                    var address = (Address)cm.List[row - 1]!;
                    address.StreetAddress = "Modified externally";

                    dlList.MoveTo(row - 1);
                    dlList.Focus();
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
            if(e.Data?.GetDataPresent(typeof(DataListBeginDragEventArgs)) ?? false)
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
            if(!(e.Data?.GetDataPresent(typeof(DataListBeginDragEventArgs)) ?? false))
                return;

            DataListBeginDragEventArgs dragArgs = (DataListBeginDragEventArgs)e.Data!.GetData(
                typeof(DataListBeginDragEventArgs))!;

            var sb = new StringBuilder("Dropped IDs: ");

            // Use the data list's currency manager
            CurrencyManager cm = dlList.ListManager!;

            // Just list the IDs of the selection
            for(int idx = dragArgs.SelectionStart; idx <= dragArgs.SelectionEnd; idx++)
            {
                if(idx != dragArgs.SelectionStart)
                    sb.Append(',');

                var address = (Address)cm.List[idx]!;

                sb.Append(address.ID);
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
            if(e.Data?.GetDataPresent(typeof(DataListBeginDragEventArgs)) ?? false)
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// Only allow drop if over a row or a row header but not within the current selection
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void dlList_DragOver(object sender, DragEventArgs e)
        {
            DataListBeginDragEventArgs dragArgs = (DataListBeginDragEventArgs)e.Data!.GetData(
                typeof(DataListBeginDragEventArgs))!;

            DataListHitTestInfo hti = dragArgs.Source.HitTest(dlList.PointToClient(Cursor.Position));

            if((hti.Type & DataListHitType.RowOrHeader) != 0 && (hti.Row < dragArgs.SelectionStart ||
              hti.Row > dragArgs.SelectionEnd))
            {
                e.Effect = DragDropEffects.Move;
            }
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
            if(!(e.Data?.GetDataPresent(typeof(DataListBeginDragEventArgs)) ?? false))
                return;

            DataListBeginDragEventArgs dragArgs = (DataListBeginDragEventArgs)e.Data!.GetData(
                typeof(DataListBeginDragEventArgs))!;

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
