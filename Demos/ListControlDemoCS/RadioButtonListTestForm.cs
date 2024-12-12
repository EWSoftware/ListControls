//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : RadioButtonListTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/02/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This is used to demonstrate the RadioButtonList control
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
    /// This is used to demonstrate the RadioButtonList control
    /// </summary>
    internal sealed partial class RadioButtonListTestForm : Form
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
        public RadioButtonListTestForm()
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

            pgProps.SelectedObject = rblDemo;
            pgProps.Refresh();
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Refresh the display and the radio button list settings after they have changed
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void pgProps_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            rblDemo.Invalidate();
            rblDemo.Update();
        }

        /// <summary>
        /// Change the data source for the radio button list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void cboDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Suspend updates to the radio button list to speed it up and prevent flickering
            rblDemo.BeginInit();

            // Clear out the prior definitions
            cboColumns.Items.Clear();
            cboColumns.SelectedIndex = -1;

            rblDemo.DataSource = null;
            rblDemo.DisplayMember = rblDemo.ValueMember = String.Empty;
            rblDemo.Items.Clear();

            // We must have the data
            if(demoData.Count == 0 || productInfo.Count == 0)
            {
                MessageBox.Show("Database not found.  It must be located in the demo project's folder.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                rblDemo.EndInit();
                return;
            }

            // Data tables, views, and sets are also supported but we won't demonstrate that here
            switch(cboDataSource.SelectedIndex)
            {
                case 0:     // Demo data (List<DemoData>)
                    rblDemo.DisplayMember = nameof(DemoTable.Label);
                    rblDemo.ValueMember = nameof(DemoTable.ListKey);
                    rblDemo.DataSource = demoData;
                    break;

                case 1:     // Product info (List<ProductInfo>)
                    rblDemo.DisplayMember = nameof(ProductInfo.ProductName);
                    rblDemo.ValueMember = nameof(ProductInfo.ProductID);
                    rblDemo.DataSource = productInfo;
                    break;

                case 2:     // Array List
                    ArrayList al = new(100);

                    foreach(var p in productInfo)
                        al.Add(new ListItem(p.ProductID, p.ProductName));

                    rblDemo.DisplayMember = nameof(ListItem.Display);
                    rblDemo.ValueMember = nameof(ListItem.Value);
                    rblDemo.DataSource = al;
                    break;

                case 3:     // Item collection strings
                    // Like the above but we add the strings directly to the radio button list's Items collection
                    foreach(var p in productInfo)
                        rblDemo.Items.Add(p.ProductName);

                    // The item collection is the data source for this one.  It's a simple string list so there
                    // are no display or value members.
                    break;

                default:
                    break;
            }

            // Resume updates to the radio button list and display the new set of radio buttons
            rblDemo.EndInit();

            // Load the column names
            if(rblDemo.DataSource != null)
            {
                if(rblDemo.DataSource is ArrayList)
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
        /// Get a value from the radio button list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnGetValue_Click(object sender, EventArgs e)
        {
            // This can be any column from the data source regardless of whether or not it is displayed.  Note
            // that you can also use rblDemo["ColName"] to get a column value from the item indicated by the
            // SelectedIndex property.
            txtValue.Text = $"{cboColumns.Text} = {rblDemo[(int)udcRowNumber.Value, cboColumns.Text]}";
        }

        /// <summary>
        /// Show the current item info when the selected index changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void rblDemo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Note that SelectedValue is only valid if there is a data source
            txtValue.Text = $"Index = {rblDemo.SelectedIndex}, Value = {rblDemo.SelectedValue}, Text = {rblDemo.Text}";
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
                // The image list is not usable with themed radio buttons under .NET 1.1
                if(rblDemo.FlatStyle == FlatStyle.System)
                    rblDemo.FlatStyle = FlatStyle.Standard;

                rblDemo.ImageList = ilImages;
            }
            else
                rblDemo.ImageList = null;
        }
        #endregion
    }
}
