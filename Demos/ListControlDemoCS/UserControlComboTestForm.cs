//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : UserControlComboBoxTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/02/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
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

using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

using EWSoftware.ListControls;

namespace ListControlDemoCS
{
	/// <summary>
	/// This is used to demonstrate the UserControlComboBox control
	/// </summary>
	public partial class UserControlComboTestForm : System.Windows.Forms.Form
    {
        #region Private data members
        //=====================================================================

        private DataSet demoData, productData;

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

            demoData = new DataSet();
            productData = new DataSet();

            try
            {
                using(var dbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\TestData.mdb"))
                {
                    // Load some data for the demo
                    OleDbCommand cmd = new OleDbCommand("Select * From DemoTable Order By Label", dbConn);
                    cmd.CommandType = CommandType.Text;
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                    adapter.Fill(demoData);

                    // Use a named table for this one
                    adapter.TableMappings.Add("Table", "ProductInfo");
                    cmd.CommandText = "Select * From ProductInfo Order By ProductName";
                    adapter.Fill(productData);
                }
            }
            catch(OleDbException ex)
            {
                MessageBox.Show(ex.Message);
            }

            cboUCCombo.DropDownControl = typeof(TreeViewDropDown);

            // Start with the data set as it is grouped by category in the drop-down
            cboDataSource.SelectedIndex = 2;

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
		private void LoadData(IList collection, out object dataSource, out string displayMember,
          out string valueMember)
        {
            // We must have the data
            if(demoData.Tables.Count == 0 || productData.Tables.Count == 0)
            {
                MessageBox.Show("Database not found.  It must be located in the demo project's folder.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                dataSource = null;
                displayMember = valueMember = String.Empty;
                return;
            }

            switch(cboDataSource.SelectedIndex)
            {
                case 0:     // Data Table
                    dataSource = demoData.Tables[0];
                    displayMember = "Label";
                    valueMember = "ListKey";
                    break;

                case 1:     // Data View
                    dataSource = demoData.Tables[0].DefaultView;
                    displayMember = "Label";
                    valueMember = "ListKey";
                    break;

                case 2:     // Data Set
                    dataSource = productData;

                    // Use a named table for this one
                    displayMember = "ProductInfo.ProductName";
                    valueMember = "ProductInfo.ProductID";
                    break;

                case 3:     // Array List
                    ArrayList al = new ArrayList(100);
                    foreach(DataRow dr in productData.Tables[0].Rows)
                        al.Add(new ListItem(dr["ProductID"], (string)dr["ProductName"]));

                    dataSource = al;
                    displayMember = "Display";     // ListItem description
                    valueMember = "Value";         // ListItem value
                    break;

                case 4:     // Combo box strings
                    // Like the above but we add the strings directly to the combo box's Items collection
                    foreach(DataRow dr in productData.Tables[0].Rows)
                        collection.Add(dr["ProductName"]);

                    // The item collection is the data source for this one.  It's a simple string list so there
                    // are no display or value members.
                    dataSource = null;
                    displayMember = valueMember = String.Empty;
                    break;

                default:
                    // Unknown.  Won't happen but it shuts the compiler up.
                    dataSource = null;
                    displayMember = valueMember = String.Empty;
                    break;
            }

            // Load the column names
            if(dataSource != null && cboColumns.Items.Count == 0)
                if(dataSource is ArrayList)
                {
                    cboColumns.Items.Add("Display");
                    cboColumns.Items.Add("Value");
                }
                else
                {
                    DataTable tbl;

                    if(dataSource is DataSet)
                        tbl = productData.Tables[0];
                    else
                        tbl = demoData.Tables[0];

                    foreach(DataColumn c in tbl.Columns)
                        cboColumns.Items.Add(c.ColumnName);
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
            object dataSource;
            string displayMember, valueMember;

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
            LoadData(cboAutoComp.Items, out dataSource, out displayMember, out valueMember);

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
            }

            if(cboColumns.Items.Count == 0)
                cboColumns.Enabled = txtRowNumber.Enabled = btnGetValue.Enabled = false;
            else
                cboColumns.Enabled = txtRowNumber.Enabled = btnGetValue.Enabled = true;
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
            txtValue.Text = String.Format("{0} = {1}", cboColumns.Text, cboUCCombo[(int)txtRowNumber.Value,
                cboColumns.Text]);
        }

        /// <summary>
        /// Show the current item info when the selected index changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void cboUCCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Note that SelectedValue is only valid if there is a data source
            txtValue.Text = String.Format("Index = {0}, Value = {1}, Text = {2}", cboUCCombo.SelectedIndex,
                cboUCCombo.SelectedValue, cboUCCombo.Text);
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
            ddc.ShowExcludeDiscontinued = (cboDataSource.SelectedIndex == 2);
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
