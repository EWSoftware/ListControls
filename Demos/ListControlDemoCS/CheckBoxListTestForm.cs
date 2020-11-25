//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : CheckBoxListTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/01/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
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

using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

using EWSoftware.ListControls;

namespace ListControlDemoCS
{
	/// <summary>
	/// This is used to demonstrate the CheckBoxList control
	/// </summary>
	public partial class CheckBoxListTestForm : System.Windows.Forms.Form
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
		public CheckBoxListTestForm()
        {
			InitializeComponent();

            demoData = new DataSet();
            productData = new DataSet();

            try
            {
                using(var dbConn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\TestData.mdb"))
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
            if(demoData.Tables.Count == 0 || productData.Tables.Count == 0)
            {
                MessageBox.Show("Database not found.  It must be located in the demo project's folder.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                cblDemo.EndInit();
                return;
            }

            switch(cboDataSource.SelectedIndex)
            {
                case 0:     // Data Table
                    cblDemo.DisplayMember = "Label";
                    cblDemo.ValueMember = "ListKey";
                    cblDemo.DataSource = demoData.Tables[0];
                    break;

                case 1:     // Data View
                    cblDemo.DisplayMember = "Label";
                    cblDemo.ValueMember = "ListKey";
                    cblDemo.DataSource = demoData.Tables[0].DefaultView;
                    break;

                case 2:     // Data Set
                    // Use a named table for this one
                    cblDemo.DisplayMember = "ProductInfo.ProductName";
                    cblDemo.ValueMember = "ProductInfo.ProductID";
                    cblDemo.DataSource = productData;
                    break;

                case 3:     // Array List
                    ArrayList al = new ArrayList(100);

                    foreach(DataRow dr in productData.Tables[0].Rows)
                        al.Add(new ListItem(dr["ProductID"], (string)dr["ProductName"]));

                    cblDemo.DisplayMember = "Display";  // ListItem description
                    cblDemo.ValueMember = "Value";      // ListItem value
                    cblDemo.DataSource = al;
                    break;

                case 4:     // Item collection strings
                    // Like the above but we add the strings directly to the checkbox list's Items collection
                    foreach(DataRow dr in productData.Tables[0].Rows)
                        cblDemo.Items.Add(dr["ProductName"]);

                    // The item collection is the data source for this one.  It's a simple string list so there
                    // are no display or value members.
                    break;

                default:
                    // Unknown.  Won't happen but it shuts the compiler up.
                    break;
            }

            // Resume updates to the checkbox list and display the new set of checkboxes
            cblDemo.EndInit();

            // Load the column names
            if(cblDemo.DataSource != null)
                if(cblDemo.DataSource is ArrayList)
                {
                    cboColumns.Items.Add("Display");
                    cboColumns.Items.Add("Value");
                }
                else
                {
                    DataTable tbl;

                    if(cblDemo.DataSource is DataSet)
                        tbl = productData.Tables[0];
                    else
                        tbl = demoData.Tables[0];

                    foreach(DataColumn c in tbl.Columns)
                        cboColumns.Items.Add(c.ColumnName);
                }

            if(cboColumns.Items.Count == 0)
                cboColumns.Enabled = txtRowNumber.Enabled = btnGetValue.Enabled = false;
            else
                cboColumns.Enabled = txtRowNumber.Enabled = btnGetValue.Enabled = true;
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
            txtValue.Text = String.Format("{0} = {1}", cboColumns.Text, cblDemo[(int)txtRowNumber.Value,
                cboColumns.Text]);
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
            txtValue.Text = String.Format("Index = {0}, Value = {1}, Text = {2}", cblDemo.SelectedIndex,
                cblDemo.SelectedValue, cblDemo.Text);
        }

        /// <summary>
        /// When the check state of a checkbox in the list changes, this event is raised
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void cblDemo_ItemCheckStateChanged(object sender, ItemCheckStateEventArgs e)
        {
            txtValue.Text = String.Format("Index = {0}, Current State = {1}", e.Index, e.CheckState);
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
