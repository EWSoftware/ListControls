//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : TreeViewDropDown.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
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

using System.Collections;

namespace ListControlDemoCS
{
	/// <summary>
	/// This is a sample drop-down control for the UserControlComboBox demo
	/// </summary>
	public partial class TreeViewDropDown : DropDownControl
	{
        #region Private data members
        //=====================================================================

        private List<DemoTable>? demoData;
        private List<ProductInfo>? productInfo;
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
        /// Clone the combo box's data source and load the tree view
        /// </summary>
        public override void InitializeDropDown()
        {
            TreeNode tn;
            object? ds = this.ComboBox.DataSource;

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
                // List of items
                if(ds is List<ProductInfo> p)
                    productInfo = p;
                else
                    demoData = (List<DemoTable>)ds;

                if(excludeVisible)
                {
                    // Sort and load by category name
                    productInfo = [.. productInfo!.OrderBy(p => p.CategoryName).ThenBy(p => p.ProductName)];

                    chkExcludeDiscontinued_CheckedChanged(this, EventArgs.Empty);
                }
                else
                {
                    // This is the one that doesn't contain categories
                    for(int idx = 0; idx < demoData!.Count; idx++)
                    {
                        tn = new TreeNode(demoData[idx].Label)
                        {
                            Tag = demoData[idx].ListKey
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
                    // Select the node by its key value stored in the tag
                    object? currentValue = this.ComboBox.SelectedValue;

                    var tne = new TreeNodeEnumerator(tvItems.Nodes[0], true);

                    while(tne.MoveNext())
                    {
                        if(tne.Current!.Tag?.Equals(currentValue) ?? false)
                        {
                            tvItems.SelectedNode = tne.Current;
                            break;
                        }
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
            int row = 0;
            var products = productInfo!;

            if(chkExcludeDiscontinued.Checked)
                products = products.Where(p => !p.Discontinued).ToList();

            tvItems.Nodes.Clear();

            while(row < products.Count)
            {
                TreeNode root = new(products[row].CategoryName);
                tvItems.Nodes.Add(root);

                while(row < products.Count && products[row].CategoryName == root.Text)
                {
                    TreeNode child = new(products[row].ProductName)
                    {
                        Tag = products[row].ProductID
                    };

                    if(products[row].Discontinued)
                        child.Text += " (Discontinued)";

                    root.Nodes.Add(child);
                    row++;
                }
            }

            tvItems.ExpandAll();
            tvItems.Nodes[0].EnsureVisible();

            btnSelect.Enabled = false;
        }

        /// <summary>
        /// Select the specified item and hide the drop-down.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnSelect_Click(object sender, EventArgs e)
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
        private void tvItems_AfterSelect(object sender, TreeViewEventArgs e)
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
        private void tvItems_DoubleClick(object sender, EventArgs e)
        {
            if(tvItems.SelectedNode.Nodes.Count == 0)
                btnSelect_Click(sender, e);
        }

        /// <summary>
        /// Close the drop-down without making a selection.  Hitting Escape works too.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ComboBox.DroppedDown = false;
        }
        #endregion
    }
}
