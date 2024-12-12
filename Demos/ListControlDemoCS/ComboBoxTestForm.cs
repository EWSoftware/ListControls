//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : ComboBoxTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/02/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This is used to demonstrate the AutoCompleteComboBox and MultiColumnComboBox controls
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
    /// This is used to demonstrate the AutoCompleteComboBox and MultiColumnComboBox controls
    /// </summary>
    public partial class ComboBoxTestForm : Form
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
        public ComboBoxTestForm()
        {
            InitializeComponent();

            // This demo uses the same data source for both combo boxes so give each their own binding context
            cboAutoComp.BindingContext = new BindingContext();
            cboMultiCol.BindingContext = new BindingContext();

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

            pgProps.SelectedObject = cboMultiCol;
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
            cboMultiCol.Invalidate();
            cboMultiCol.Update();

            // We'll also force the drop-down to refresh its settings.  Note that this has the side-effect of
            // clearing the column definitions.  See the method documentation for the reason why.  If you want
            // to play with the column definitions, do so after setting all other properties.
            cboMultiCol.RefreshSubControls();

            if(cboMultiCol.DropDownStyle != cboAutoComp.DropDownStyle)
            {
                cboAutoComp.DropDownStyle = cboMultiCol.DropDownStyle;

                if(cboMultiCol.DropDownStyle == ComboBoxStyle.Simple)
                {
                    cboAutoComp.Height = 150;
                    cboMultiCol.Height = grpOptions.Top - cboMultiCol.Top - 5;
                }
            }

            cboAutoComp.Enabled = cboMultiCol.Enabled;
            cboAutoComp.BackColor = cboMultiCol.BackColor;
            cboAutoComp.ForeColor = cboMultiCol.ForeColor;
            cboAutoComp.RightToLeft = cboMultiCol.RightToLeft;
            cboAutoComp.FlatStyle = cboMultiCol.FlatStyle;
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

            cboMultiCol.DataSource = null;
            cboMultiCol.DisplayMember = cboMultiCol.ValueMember = String.Empty;
            cboMultiCol.Items.Clear();

            // Clear the column filter as it may not contain columns in the new data set
            cboMultiCol.ColumnFilter.Clear();

            // This will dispose of the drop-down portion and clear out all existing column definitions ready for
            // the new stuff.
            cboMultiCol.RefreshSubControls();

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
            cboMultiCol.BeginInit();
            LoadData(cboMultiCol.Items, out dataSource, out displayMember, out valueMember);
            cboMultiCol.EndInit();

            if(dataSource != null)
            {
                cboMultiCol.DisplayMember = displayMember;
                cboMultiCol.ValueMember = valueMember;
                cboMultiCol.DataSource = dataSource;

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
            // that you can also use cboMultiCol["ColName"] to get a column value from the item indicated by the
            // SelectedIndex property.
            txtValue.Text = $"{cboColumns.Text} = {cboMultiCol[(int)udcRowNumber.Value, cboColumns.Text]}";
        }

        /// <summary>
        /// Show the current item info when the selected index changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void cboMultiCol_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Note that SelectedValue is only valid if there is a data source
            txtValue.Text = $"Index = {cboMultiCol.SelectedIndex}, Value = {cboMultiCol.SelectedValue}, Text = {cboMultiCol.Text}";
        }

        /// <summary>
        /// Draw an image for the demo.  They aren't representative of the items, they're just something to show.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void cboMultiCol_DrawItemImage(object sender, DrawItemEventArgs e)
        {
            if(e.Index == -1)
                e.DrawBackground();
            else
            {
                if((e.State & DrawItemState.Disabled) != 0)
                {
                    ControlPaint.DrawImageDisabled(e.Graphics, ilImages.Images[e.Index % ilImages.Images.Count],
                        e.Bounds.X, e.Bounds.Y, e.BackColor);
                }
                else
                    e.Graphics.DrawImage(ilImages.Images[e.Index % ilImages.Images.Count], e.Bounds.X, e.Bounds.Y);
            }
        }

        /// <summary>
        /// Apply custom formatting to a dropdown column when it is created
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void cboMultiCol_FormatDropDownColumn(object sender, DataGridViewColumnEventArgs e)
        {
            // Format unit price as currency
            if(e.Column.DataPropertyName == nameof(ProductInfo.UnitPrice))
                e.Column.DefaultCellStyle.Format = "C2";
            else
            {
                // When bound to an array list, the value is seen as an object so manually right-align the
                // numeric value.
                if(cboDataSource.SelectedIndex == 2 && e.Column.DataPropertyName == nameof(ListItem.Value) &&
                  e.Column.ValueType == typeof(object))
                {
                    e.Column.HeaderCell.Style.Alignment = e.Column.DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }
            }
        }
        #endregion
    }
}
