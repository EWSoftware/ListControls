//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : RadioButtonListTestForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/02/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
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

using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

using EWSoftware.ListControls;

namespace ListControlDemoCS
{
	/// <summary>
	/// This is used to demonstrate the RadioButtonList control
	/// </summary>
	public partial class RadioButtonListTestForm : System.Windows.Forms.Form
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
		public RadioButtonListTestForm()
        {
			InitializeComponent();

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
            if(demoData.Tables.Count == 0 || productData.Tables.Count == 0)
            {
                MessageBox.Show("Database not found.  It must be located in the demo project's folder.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                rblDemo.EndInit();
                return;
            }

            switch(cboDataSource.SelectedIndex)
            {
                case 0:     // Data Table
                    rblDemo.DisplayMember = "Label";
                    rblDemo.ValueMember = "ListKey";
                    rblDemo.DataSource = demoData.Tables[0];
                    break;

                case 1:     // Data View
                    rblDemo.DisplayMember = "Label";
                    rblDemo.ValueMember = "ListKey";
                    rblDemo.DataSource = demoData.Tables[0].DefaultView;
                    break;

                case 2:     // Data Set
                    // Use a named table for this one
                    rblDemo.DisplayMember = "ProductInfo.ProductName";
                    rblDemo.ValueMember = "ProductInfo.ProductID";
                    rblDemo.DataSource = productData;
                    break;

                case 3:     // Array List
                    ArrayList al = new ArrayList(100);
                    foreach(DataRow dr in productData.Tables[0].Rows)
                        al.Add(new ListItem(dr["ProductID"], (string)dr["ProductName"]));

                    rblDemo.DisplayMember = "Display";  // ListItem description
                    rblDemo.ValueMember = "Value";      // ListItem value
                    rblDemo.DataSource = al;
                    break;

                case 4:     // Item collection strings
                    // Like the above but we add the strings directly to the radio button list's Items collection
                    foreach(DataRow dr in productData.Tables[0].Rows)
                        rblDemo.Items.Add(dr["ProductName"]);

                    // The item collection is the data source for this one.  It's a simple string list so there
                    // are no display or value members.
                    break;

                default:
                    // Unknown.  Won't happen but it shuts the compiler up.
                    break;
            }

            // Resume updates to the radio button list and display the new set of radio buttons
            rblDemo.EndInit();

            // Load the column names
            if(rblDemo.DataSource != null)
                if(rblDemo.DataSource is ArrayList)
                {
                    cboColumns.Items.Add("Display");
                    cboColumns.Items.Add("Value");
                }
                else
                {
                    DataTable tbl;

                    if(rblDemo.DataSource is DataSet)
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
        /// Get a value from the radio button list
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnGetValue_Click(object sender, EventArgs e)
        {
            // This can be any column from the data source regardless of whether or not it is displayed.  Note
            // that you can also use rblDemo["ColName"] to get a column value from the item indicated by the
            // SelectedIndex property.
            txtValue.Text = String.Format("{0} = {1}", cboColumns.Text, rblDemo[(int)txtRowNumber.Value,
                cboColumns.Text]);
        }

        /// <summary>
        /// Show the current item info when the selected index changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void rblDemo_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Note that SelectedValue is only valid if there is a data source
            txtValue.Text = String.Format("Index = {0}, Value = {1}, Text = {2}", rblDemo.SelectedIndex,
                rblDemo.SelectedValue, rblDemo.Text);
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
