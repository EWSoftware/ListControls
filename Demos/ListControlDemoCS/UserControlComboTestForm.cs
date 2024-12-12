//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : UserControlComboBoxTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/02/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This is used to demonstrate the UserControlComboBox control
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
    /// This is used to demonstrate the UserControlComboBox control
    /// </summary>
    public partial class UserControlComboTestForm : Form
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
        public UserControlComboTestForm()
        {
            InitializeComponent();

            // This demo uses the same data source for both combo boxes so give each their own binding context
            cboAutoComp.BindingContext = new BindingContext();
            cboUCCombo.BindingContext = new BindingContext();

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

            cboUCCombo.DropDownControl = typeof(TreeViewDropDown);

            // Start with the product info as it is grouped by category in the drop-down
            cboDataSource.SelectedIndex = 1;

            pgProps.SelectedObject = cboUCCombo;
            pgProps.Refresh();
        }
        #endregion

        #region Helper methods
        //=====================================================================

        /// <summary>
        /// Load data from the selected source and return the binding info
        /// </summary>
        /// <param name="collection">The combo box item collection used by a couple of the data sources</param>
        /// <param name="dataSource">The variable used to receive the data source</param>
        /// <param name="displayMember">The variable used to receive the display member name</param>
        /// <param name="valueMember">The variable used to receive the value member name</param>
		private void LoadData(IList collection, out object? dataSource, out string displayMember,
          out string valueMember)
        {
            // We must have the data
            if(demoData.Count == 0 || productInfo.Count == 0)
            {
                MessageBox.Show("Database not found.  It must be located in the demo project's folder.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                dataSource = null;
                displayMember = valueMember = String.Empty;
                return;
            }

            // Data tables, views, and sets are also supported but we won't demonstrate that here
            switch(cboDataSource.SelectedIndex)
            {
                case 0:     // Demo data (List<DemoData>)
                    dataSource = demoData;
                    displayMember = nameof(DemoTable.Label);
                    valueMember = nameof(DemoTable.ListKey);
                    break;

                case 1:     // Product info (List<ProductInfo>)
                    dataSource = productInfo;
                    displayMember = nameof(ProductInfo.ProductName);
                    valueMember = nameof(ProductInfo.ProductID);
                    break;

                case 2:     // Array List
                    ArrayList al = new(100);

                    foreach(var p in productInfo)
                        al.Add(new ListItem(p.ProductID, p.ProductName));

                    dataSource = al;
                    displayMember = nameof(ListItem.Display);
                    valueMember = nameof(ListItem.Value);
                    break;

                case 3:     // Combo box strings
                    // Like the above but we add the strings directly to the combo box's Items collection
                    foreach(var p in productInfo)
                        collection.Add(p.ProductName);

                    // The item collection is the data source for this one.  It's a simple string list so there
                    // are no display or value members.
                    dataSource = null;
                    displayMember = valueMember = String.Empty;
                    break;

                default:
                    dataSource = null;
                    displayMember = valueMember = String.Empty;
                    break;
            }
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Refresh the display and the combo box drop-down settings after they have changed
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void pgProps_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            cboUCCombo.Invalidate();
            cboUCCombo.Update();

            // We'll also force the drop-down to refresh its settings
            cboUCCombo.RefreshSubControls();

            if(cboUCCombo.DropDownStyle != cboAutoComp.DropDownStyle)
            {
                cboAutoComp.DropDownStyle = cboUCCombo.DropDownStyle;

                if(cboUCCombo.DropDownStyle == ComboBoxStyle.Simple)
                {
                    cboAutoComp.Height = 150;
                    cboUCCombo.Height = grpOptions.Top - cboUCCombo.Top - 5;
                }
            }

            cboAutoComp.Enabled = cboUCCombo.Enabled;
            cboAutoComp.BackColor = cboUCCombo.BackColor;
            cboAutoComp.ForeColor = cboUCCombo.ForeColor;
            cboAutoComp.RightToLeft = cboUCCombo.RightToLeft;
            cboAutoComp.FlatStyle = cboUCCombo.FlatStyle;
        }

        /// <summary>
        /// Change the data source for the combo boxes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void cboDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear out the prior definitions
            cboColumns.Items.Clear();
            cboColumns.SelectedIndex = -1;

            cboAutoComp.DataSource = null;
            cboAutoComp.DisplayMember = cboAutoComp.ValueMember = String.Empty;
            cboAutoComp.Items.Clear();

            cboUCCombo.DataSource = null;
            cboUCCombo.DisplayMember = cboUCCombo.ValueMember = String.Empty;
            cboUCCombo.Items.Clear();

            // This will dispose of the drop-down portion and clear out all existing definitions ready for the
            // new stuff.
            cboUCCombo.RefreshSubControls();

            // Keep it simple.  We'll bind them to the same data but using different instances so no need for
            // binding contexts.
            LoadData(cboAutoComp.Items, out object? dataSource, out string displayMember, out string valueMember);

            if(dataSource != null)
            {
                cboAutoComp.DisplayMember = displayMember;
                cboAutoComp.ValueMember = valueMember;
                cboAutoComp.DataSource = dataSource;
            }

            // Suspend updates to the combo box to speed it up and prevent flickering
            cboUCCombo.BeginInit();
            LoadData(cboUCCombo.Items, out dataSource, out displayMember, out valueMember);
            cboUCCombo.EndInit();

            if(dataSource != null)
            {
                cboUCCombo.DisplayMember = displayMember;
                cboUCCombo.ValueMember = valueMember;
                cboUCCombo.DataSource = dataSource;

                // Load the column names
                if(dataSource is ArrayList)
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
        /// Get a value from the combo box
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnGetValue_Click(object sender, EventArgs e)
        {
            // This can be any column from the data source regardless of whether or not it is displayed.  Note
            // that you can also use cboUCCombo["ColName"] to get a column value from the item indicated by the
            // SelectedIndex property.
            txtValue.Text = $"{cboColumns.Text} = {cboUCCombo[(int)udcRowNumber.Value, cboColumns.Text]}";
        }

        /// <summary>
        /// Show the current item info when the selected index changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void cboUCCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Note that SelectedValue is only valid if there is a data source
            txtValue.Text = $"Index = {cboUCCombo.SelectedIndex}, Value = {cboUCCombo.SelectedValue}, Text = {cboUCCombo.Text}";
        }

        /// <summary>
        /// For this demo, the drop-down has to determine the data source for itself so it could figure out
        /// whether or not to hide the checkbox.  This just demonstrates one possible use of this event.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void cboUCCombo_DropDownControlCreated(object sender, EventArgs e)
        {
            TreeViewDropDown ddc = (TreeViewDropDown)sender;
            ddc.ShowExcludeDiscontinued = (cboDataSource.SelectedIndex == 1);
        }

        /// <summary>
        /// Draw an image for the demo.  They aren't representative of the items, they're just something to show.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void cboUCCombo_DrawItemImage(object sender, DrawItemEventArgs e)
        {
            if(e.Index == -1)
                e.DrawBackground();
            else
                if((e.State & DrawItemState.Disabled) != 0)
            {
                ControlPaint.DrawImageDisabled(e.Graphics, ilImages.Images[e.Index % ilImages.Images.Count],
                    e.Bounds.X, e.Bounds.Y, e.BackColor);
            }
            else
                e.Graphics.DrawImage(ilImages.Images[e.Index % ilImages.Images.Count], e.Bounds.X, e.Bounds.Y);
        }
        #endregion
    }
}
