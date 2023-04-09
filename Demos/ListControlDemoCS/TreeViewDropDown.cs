//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : TreeViewDropDown.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/06/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
//
// This is a sample drop-down control for the UserControlComboBox demo
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
using System.Windows.Forms;

using EWSoftware.ListControls;

namespace ListControlDemoCS
{
	/// <summary>
	/// This is a sample drop-down control for the UserControlComboBox demo
	/// </summary>
	public partial class TreeViewDropDown : EWSoftware.ListControls.DropDownControl
	{
        #region Private data members
        //=====================================================================

        private DataView dvItems;
        private DataTable tblItems;
        private bool excludeVisible;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// Get or set whether or not to show the "Exclude discontinued" checkbox
        /// </summary>
        /// <remarks>We need a variable to track the state as the checkbox always reports false for visibility if
        /// the parent isn't visible (i.e. during initialization).</remarks>
        public bool ShowExcludeDiscontinued
        {
            get => excludeVisible;
            set => excludeVisible = chkExcludeDiscontinued.Visible = value;
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public TreeViewDropDown()
		{
			InitializeComponent();
		}
        #endregion

        #region Helper methods and method overrides
        //=====================================================================

        /// <summary>
        /// This loads items from the row collection into the tree view
        /// </summary>
        /// <param name="row">The starting row</param>
        /// <param name="tnRoot">The root node to which items are added</param>
        /// <returns>The index of the next row that starts a new node</returns>
        private int LoadItems(int row, TreeNode tnRoot)
        {
            TreeNode tnChild;

            while(row < dvItems.Count && (string)dvItems[row]["CategoryName"] == tnRoot.Text)
            {
                tnChild = new TreeNode((string)dvItems[row]["ProductName"])
                {
                    Tag = dvItems[row]["ProductID"]
                };

                if((bool)dvItems[row]["Discontinued"])
                    tnChild.Text += " (Discontinued)";

                tnRoot.Nodes.Add(tnChild);
                row++;
            }

            return row;
        }

        /// <summary>
        /// Clone the combo box's data source and load the tree view
        /// </summary>
        public override void InitializeDropDown()
        {
            TreeNode tn;
            object ds = this.ComboBox.DataSource;

            // Determine the data source type in use by the demo form
            if(ds == null)
            {
                foreach(string s in this.ComboBox.Items)
                {
                    tn = new TreeNode(s) { Tag = s };
                    tvItems.Nodes.Add(tn);
                }
            }
            else if(ds is ArrayList)
            {
                foreach(ListItem li in this.ComboBox.Items)
                {
                    tn = new TreeNode(li.Display) { Tag = li.Value };
                    tvItems.Nodes.Add(tn);
                }
            }
            else
            {
                // Data set, view, or table
                if(ds is DataSet set)
                {
                    tblItems = set.Tables["ProductInfo"];
                }
                else
                {
                    if(ds is DataView view)
                        tblItems = view.Table;
                    else
                        tblItems = (DataTable)ds;
                }

                dvItems = tblItems.DefaultView;

                if(excludeVisible)
                {
                    // Sort and load by category name
                    dvItems.Sort = "CategoryName, ProductName";
                    chkExcludeDiscontinued_CheckedChanged(this, EventArgs.Empty);
                }
                else
                {
                    // This is the one that doesn't contain categories
                    for(int idx = 0; idx < dvItems.Count; idx++)
                    {
                        tn = new TreeNode((string)dvItems[idx]["Label"])
                        {
                            Tag = dvItems[idx]["ListKey"]
                        };

                        tvItems.Nodes.Add(tn);
                    }
                }
            }

            // In simple mode, hide the cancel button
            if(this.ComboBox.DropDownStyle == ComboBoxStyle.Simple)
            {
                btnSelect.Left = btnCancel.Left;
                btnCancel.Visible = false;
            }
        }

        /// <summary>
        /// When shown, select the current product as the default row
        /// </summary>
        public override void ShowDropDown()
        {
            if(this.ComboBox.SelectedIndex != -1)
            {
                // If not categorized, just select by index
                if(!excludeVisible)
                {
                    if(this.ComboBox.SelectedIndex < tvItems.Nodes.Count)
                        tvItems.SelectedNode = tvItems.Nodes[this.ComboBox.SelectedIndex];
                }
                else
                {
                    // There doesn't appear to be an easy way to search for a node so do it the hard way
                    object currentValue = this.ComboBox.SelectedValue;

                    foreach(TreeNode tn in tvItems.Nodes)
                        foreach(TreeNode child in tn.Nodes)
                            if(child.Tag.Equals(currentValue))
                            {
                                tvItems.SelectedNode = child;
                                break;
                            }
                }
            }
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Refresh the list based on the filter
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void chkExcludeDiscontinued_CheckedChanged(object sender, EventArgs e)
        {
            TreeNode tnNode;
            int row = 0;

            if(chkExcludeDiscontinued.Checked)
                dvItems.RowFilter = "Discontinued = false";
            else
                dvItems.RowFilter = null;

            tvItems.Nodes.Clear();

            while(row < dvItems.Count)
            {
                tnNode = new TreeNode((string)dvItems[row]["CategoryName"]);
                tvItems.Nodes.Add(tnNode);
                row = LoadItems(row, tnNode);
            }

            btnSelect.Enabled = false;
        }

        /// <summary>
        /// Select the specified item and hide the drop-down.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnSelect_Click(object sender, System.EventArgs e)
        {
            if(!excludeVisible)
                this.CommitSelection(tvItems.Nodes.IndexOf(tvItems.SelectedNode));
            else
                this.CommitSelection(tvItems.SelectedNode.Tag);
        }

        /// <summary>
        /// Disable selection if it is a parent node
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void tvItems_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            btnSelect.Enabled = (tvItems.SelectedNode.Nodes.Count == 0);

            // If this is the one without the categories, track the selection as each node is selected
            if(!excludeVisible)
                this.ComboBox.SelectedIndex = tvItems.Nodes.IndexOf(tvItems.SelectedNode);
        }

        /// <summary>
        /// Select the current item when double clicked if it has no children
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void tvItems_DoubleClick(object sender, System.EventArgs e)
        {
            if(tvItems.SelectedNode.Nodes.Count == 0)
                btnSelect_Click(sender, e);
        }

        /// <summary>
        /// Close the drop-down without making a selection.  Hitting Escape works too.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.ComboBox.DroppedDown = false;
        }
        #endregion
    }
}
