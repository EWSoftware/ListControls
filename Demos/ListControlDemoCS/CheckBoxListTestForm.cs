//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : CheckBoxListTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/02/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This is used to demonstrate the CheckBoxList control
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

using System.Collections;

namespace ListControlDemoCS
{
    /// <summary>
    /// This is used to demonstrate the CheckBoxList control
    /// </summary>
    internal sealed partial class CheckBoxListTestForm : Form
    {
        #region Private data members
        //=====================================================================

        private readonly List<DemoTable> demoData;
        private readonly List<ProductInfo> productInfo;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public CheckBoxListTestForm()
        {
            InitializeComponent();

            try
            {
                using var dc = new DemoDataContext();

                demoData = [.. dc.DemoTable.OrderBy(d => d.Label)];
                productInfo = [.. dc.ProductInfo.OrderBy(p => p.ProductName)];
            }
            catch(SqlException ex)
            {
                demoData = [];
                productInfo = [];

                MessageBox.Show(ex.Message);
            }

            cboDataSource.SelectedIndex = 0;

            pgProps.SelectedObject = cblDemo;
            pgProps.Refresh();
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Refresh the display and the checkbox list settings after they have changed
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void pgProps_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            cblDemo.Invalidate();
            cblDemo.Update();
        }

        /// <summary>
        /// Change the data source for the checkbox list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void cboDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Suspend updates to the checkbox list to speed it up and prevent flickering
            cblDemo.BeginInit();

            // Clear out the prior definitions
            cboColumns.Items.Clear();
            cboColumns.SelectedIndex = -1;

            cblDemo.DataSource = null;
            cblDemo.DisplayMember = cblDemo.ValueMember = String.Empty;
            cblDemo.Items.Clear();

            // We must have the data
            if(demoData.Count == 0 || productInfo.Count == 0)
            {
                MessageBox.Show("Database not found.  It must be located in the demo project's folder.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                cblDemo.EndInit();
                return;
            }

            // Data tables, views, and sets are also supported but we won't demonstrate that here
            switch(cboDataSource.SelectedIndex)
            {
                case 0:     // Demo data (List<DemoData>)
                    cblDemo.DisplayMember = nameof(DemoTable.Label);
                    cblDemo.ValueMember = nameof(DemoTable.ListKey);
                    cblDemo.DataSource = demoData;
                    break;

                case 1:     // Product info (List<ProductInfo>)
                    cblDemo.DisplayMember = nameof(ProductInfo.ProductName);
                    cblDemo.ValueMember = nameof(ProductInfo.ProductID);
                    cblDemo.DataSource = productInfo;
                    break;

                case 2:     // Array List
                    ArrayList al = new(100);

                    foreach(var p in productInfo)
                        al.Add(new ListItem(p.ProductID, p.ProductName));

                    cblDemo.DisplayMember = nameof(ListItem.Display);
                    cblDemo.ValueMember = nameof(ListItem.Value);
                    cblDemo.DataSource = al;
                    break;

                case 3:     // Item collection strings
                    // Like the above but we add the strings directly to the checkbox list's Items collection
                    foreach(var p in productInfo)
                        cblDemo.Items.Add(p.ProductName);

                    // The item collection is the data source for this one.  It's a simple string list so there
                    // are no display or value members.
                    break;

                default:
                    break;
            }

            // Resume updates to the checkbox list and display the new set of checkboxes
            cblDemo.EndInit();

            // Load the column names
            if(cblDemo.DataSource != null)
            {
                if(cblDemo.DataSource is ArrayList)
                {
                    cboColumns.Items.Add(nameof(ListItem.Display));
                    cboColumns.Items.Add(nameof(ListItem.Value));
                }
                else
                {
                    Type dataSourceType;

                    if(cboDataSource.SelectedIndex == 0)
                        dataSourceType = typeof(DemoTable);
                    else
                        dataSourceType = typeof(ProductInfo);

                    foreach(var p in dataSourceType.GetProperties())
                        cboColumns.Items.Add(p.Name);
                }
            }

            if(cboColumns.Items.Count == 0)
                cboColumns.Enabled = udcRowNumber.Enabled = btnGetValue.Enabled = false;
            else
                cboColumns.Enabled = udcRowNumber.Enabled = btnGetValue.Enabled = true;
        }

        /// <summary>
        /// Get a value from the checkbox list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnGetValue_Click(object sender, EventArgs e)
        {
            // This can be any column from the data source regardless of whether or not it is displayed.  Note
            // that you can also use cblDemo["ColName"] to get a column value from the item indicated by the
            // SelectedIndex property.
            txtValue.Text = $"{cboColumns.Text} = {cblDemo[(int)udcRowNumber.Value, cboColumns.Text]}";
        }

        /// <summary>
        /// Show the current item info when the selected index changes.  For the checkbox list, this happens
        /// whenever a new checkbox item gains the focus.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void cblDemo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Note that SelectedValue is only valid if there is a data source
            txtValue.Text = $"Index = {cblDemo.SelectedIndex}, Value = {cblDemo.SelectedValue}, Text = {cblDemo.Text}";
        }

        /// <summary>
        /// When the check state of a checkbox in the list changes, this event is raised
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void cblDemo_ItemCheckStateChanged(object sender, ItemCheckStateEventArgs e)
        {
            txtValue.Text = $"Index = {e.Index}, Current State = {e.CheckState}";
        }

        /// <summary>
        /// Use or clear the image list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void chkShowImages_CheckedChanged(object sender, EventArgs e)
        {
            if(chkShowImages.Checked)
            {
                // The image list is not usable with themed checkboxes under .NET 1.1
                if(cblDemo.FlatStyle == FlatStyle.System)
                    cblDemo.FlatStyle = FlatStyle.Standard;

                cblDemo.ImageList = ilImages;
            }
            else
                cblDemo.ImageList = null;
        }

        /// <summary>
        /// Get the currently checked item values
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnCheckedItems_Click(object sender, System.EventArgs e)
        {
            CheckedItemsCollection items = cblDemo.CheckedItems;

            if(items.Count == 0)
                MessageBox.Show("No items are currently checked");
            else
            {
                // The values of individual items can be retrieved using the ValueOf() method on the collection.
                // The check state of the items can be retrieved using the CheckStateOf() method.  The ToString()
                // method returns the values as a comma-separated list.
                MessageBox.Show("The values of the currently checked items are:\r\n" + items.ToString());
            }
        }

        /// <summary>
        /// Get the currently checked item display text values
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnCheckedItemsText_Click(object sender, EventArgs e)
        {
            CheckedItemsCollection items = cblDemo.CheckedItems;

            if(items.Count == 0)
                MessageBox.Show("No items are currently checked");
            else
            {
                // The display text of individual items can be retrieved using the DisplayTextOf() method on the
                // collection.  The check state of the items can be retrieved using the CheckStateOf() method.
                // The ToDisplayTextString() method returns the display text of the checked items as a
                // comma-separated list.
                MessageBox.Show("The display text of the currently checked items are:\r\n" + items.ToDisplayTextString());
            }
        }

        /// <summary>
        /// Get the indices of the currently checked items.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnCheckedIndices_Click(object sender, EventArgs e)
        {
            CheckedIndicesCollection indices = cblDemo.CheckedIndices;

            if(indices.Count == 0)
                MessageBox.Show("No items are currently checked");
            else
                MessageBox.Show("The indices of the currently checked items are:\r\n" + indices.ToString());
        }
        #endregion
    }
}
