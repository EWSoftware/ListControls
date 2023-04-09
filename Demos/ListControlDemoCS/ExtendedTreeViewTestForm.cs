//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : ExtendedTreeViewDemo.cs
// Author  : Eric Woodruff
// Updated : 01/06/2023
// Note    : Copyright 2007-2023, Eric Woodruff, All rights reserved
//
// This form is used to demonstrate the extended tree view control.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/09/2007  EFW  Created the code
//===============================================================================================================

// Ignore Spelling: Fld foreach

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using EWSoftware.ListControls;

namespace ListControlDemoCS
{
    /// <summary>
    /// This form is used to demonstrate the ExtendedTreeView control.
    /// </summary>
    public partial class ExtendedTreeViewTestForm : System.Windows.Forms.Form
    {
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public ExtendedTreeViewTestForm()
        {
            TreeNode tnFolder, tnBook, tnPage;

            InitializeComponent();

            tvExtTree.BeginUpdate();

            // Only even numbered nodes gets an image
            for(int i = 0; i < 3; i++)
            {
                tnFolder = tvExtTree.Nodes.Add($"Fld {i}", $"Folder {i}",
                    (i % 2 != 0) ? -1 : 0, (i % 2 != 0) ? -1 : 1);
                tnFolder.ToolTipText = tnFolder.Name;

                for(int j = 0; j < 3; j++)
                {
                    tnBook = tnFolder.Nodes.Add($"Fld{i}Bk{j}", $"Book {j}",
                        (j % 2 != 0) ? -1 : 2, (j % 2 != 0) ? -1 : 3);
                    tnBook.ToolTipText = tnBook.Name;

                    // These also get a state image
                    for(int k = 0; k < 5; k++)
                    {
                        tnPage = tnBook.Nodes.Add($"Fld{i}Bk{j}Pg{k}", $"Page {k}",
                            (k % 2 != 0) ? -1 : 4, (k % 2 != 0) ? -1 : 4);
                        tnPage.StateImageIndex = k;
                        tnPage.ToolTipText = tnPage.Name;
                    }
                }
            }

            tvExtTree.EndUpdate();
            tvExtTree.Nodes[0].ExpandAll();

            pgProps.SelectedObject = tvExtTree;
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// Refresh the tree view when a property value changes
        /// </summary>
        /// <param name="s">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void pgProps_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            tvExtTree.Invalidate();
            tvExtTree.Update();
        }

        /// <summary>
        /// Switch between small and large images
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void chkLargeImages_CheckedChanged(object sender, EventArgs e)
        {
            int newHeight;

            tvExtTree.BeginUpdate();

            if(chkLargeImages.Checked)
                tvExtTree.ImageList = ilLarge;
            else
                tvExtTree.ImageList = ilSmall;

            // Reset the item height and indent
            newHeight = tvExtTree.ImageList.Images[0].Height + 2;

            if(newHeight < tvExtTree.Font.Height + 3)
                newHeight = tvExtTree.Font.Height + 3;

            tvExtTree.ItemHeight = newHeight;
            tvExtTree.Indent = 0;

            tvExtTree.EndUpdate();
            pgProps.Refresh();
        }

        /// <summary>
        /// Enable or disable state images
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void chkStateImages_CheckedChanged(object sender, EventArgs e)
        {
            tvExtTree.BeginUpdate();

            if(chkStateImages.Checked)
                tvExtTree.StateImageList = ilState;
            else
                tvExtTree.StateImageList = null;

            tvExtTree.EndUpdate();
            pgProps.Refresh();
        }

        /// <summary>
        /// Enable or disable the custom expando images
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void chkExpandoImages_CheckedChanged(object sender, EventArgs e)
        {
            tvExtTree.BeginUpdate();

            if(chkExpandoImages.Checked)
                tvExtTree.ExpandoImageList = ilExpando;
            else
                tvExtTree.ExpandoImageList = null;

            tvExtTree.ShowPlusMinus = true;
            tvExtTree.EndUpdate();
            pgProps.Refresh();
        }

        /// <summary>
        /// Enable or disable the form-level OnDrawNode handler
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void chkFormDrawNode_CheckedChanged(object sender, EventArgs e)
        {
            // When enabled, clicks on the extra text drawn by the event will cause the node to get selected too
            tvExtTree.SelectOnRightOfLabelClick = chkFormDrawNode.Checked;

            tvExtTree.Invalidate();
            tvExtTree.Update();
        }

        /// <summary>
        /// Rotate through the state images when they are clicked or the space bar is hit
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void tvExtTree_ChangeStateImage(object sender, TreeViewEventArgs e)
        {
            int idx = e.Node.StateImageIndex;

            idx++;

            if(idx > 4)
                idx = 0;

            e.Node.StateImageIndex = idx;
        }

        #region TreeNodeDrawing Example
        /// <summary>
        /// Test TreeNodeDrawing event handler.  This occurs before the tree
        /// view draws the node.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void tvExtTree_TreeNodeDrawing(object sender, DrawTreeNodeExtendedEventArgs e)
        {
            if(!chkFormDrawNode.Checked)
                return;

            // Use solid 2px lines
            e.LinePen.DashStyle = DashStyle.Solid;
            e.LinePen.Width = 2;

            // Change the text by adding the node name.  Assigning new text
            // automatically recalculates the text and focus bounds. We could
            // draw the text, but we'll let the base class handle it. Note that
            // since the tree view doesn't know about the extra text, it won't
            // show the horizontal scrollbar if it goes off the right edge.
            e.Text += " (" + e.Node.Name + ")";

            // If the item height is larger than 35, wrap the text too
            if(tvExtTree.ItemHeight > 35)
            {
                e.StringFormat.FormatFlags = StringFormatFlags.NoClip;

                // Limit the text to the right edge of the node bounds.  Note
                // that this won't stop the tree view from showing a horizontal
                // scrollbar.
                e.TextBounds = new Rectangle(e.TextBounds.Left, e.NodeBounds.Top,
                    e.NodeBounds.Width - e.TextBounds.Left, e.NodeBounds.Height);
            }

            // NOTE:
            // If you choose to draw one or more parts of the node, you should
            // draw the background first, then the node parts, and then turn
            // off the NodeParts.Background flag along with the other node part
            // flags in the e.NodeParts property before returning.
        }
        #endregion

        #region TreeNodeDrawn Example
        /// <summary>
        /// Test TreeNodeDrawn event handler.  This occurs after the tree view
        /// has drawn the node.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void tvExtTree_TreeNodeDrawn(object sender, DrawTreeNodeExtendedEventArgs e)
        {
            Pen pen;
            int top;

            if(chkFormDrawNode.Checked && e.Node.Level == 0 && e.ImageIndex != -1)
            {
                // Nothing exciting, we'll just draw a line through
                // top level nodes that have an image index set.
                if((e.State & TreeNodeStates.Focused) != 0 && tvExtTree.FullRowSelect)
                    pen = SystemPens.ControlLightLight;
                else
                    pen = SystemPens.ControlDarkDark;

                top = e.NodeBounds.Top + (e.NodeBounds.Height / 2);

                e.Graphics.DrawLine(pen, e.NodeBounds.Left, top,
                    e.NodeBounds.Left + e.NodeBounds.Width, top);
            }
        }
        #endregion

        /// <summary>
        /// Enumerate the tree nodes using the built-in TreeNodeEnumerator
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnEnumTree_Click(object sender, EventArgs e)
        {
            #region Enumerate ExtendedTreeView control
            txtEnumResults.Text = null;

            // Use foreach() on the ExtendedTreeView control itself to
            // enumerate all of its nodes recursively.
            foreach(TreeNode node in tvExtTree)
                txtEnumResults.AppendText($"{new String(' ', node.Level * 4)}{node.Text}\r\n");
            #endregion
        }

        /// <summary>
        /// Enumerate just the selected node (branch) and, based on the sender, the subsequent siblings
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnEnumNode_Click(object sender, EventArgs e)
        {
            #region TreeNodeEnumerator Example
            bool enumerateSiblings = (sender == btnEnumNodeSibs);
            TreeNode node, startNode = tvExtTree.SelectedNode;

            if(startNode == null)
            {
                txtEnumResults.Text = "Select a starting node first";
                return;
            }

            txtEnumResults.Text = null;

            // For this, we create the enumerator manually and pass it
            // the starting node and a flag indicating whether or not
            // to enumerate the siblings of the starting node as well.
            TreeNodeEnumerator enumerator = new TreeNodeEnumerator(startNode,
                enumerateSiblings);

            // Call the MoveNext() method to move through each node.  Use the
            // Current property to access the current node.
            while(enumerator.MoveNext())
            {
                node = enumerator.Current;

                txtEnumResults.AppendText($"Manual Enum: {new String(' ', node.Level * 4)}{node.Text}\r\n");
            }

            txtEnumResults.AppendText("\r\n\r\n");

            // We can also use the helper method to simplify it
            foreach(TreeNode tn in TreeNodeEnumerator.Enumerate(startNode, enumerateSiblings))
                txtEnumResults.AppendText($"Enum Helper: {new String(' ', tn.Level * 4)}{tn.Text}\r\n");
            #endregion
        }

        /// <summary>
        /// Find a node by its name using the tree view's indexer
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event argument</param>
        private void btnFindNode_Click(object sender, EventArgs e)
        {
            #region Tree Node Indexer Example
            string name = txtFindName.Text.Trim();

            if(name.Length == 0)
            {
                MessageBox.Show("Enter a node name to find", "Tree View Demo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Simply use the indexer with the name of the node.  Note that
            // node names aren't necessarily unique (they are in the demo) so
            // it returns the first node found with the given name.
            TreeNode node = tvExtTree[txtFindName.Text];

            if(node != null)
            {
                tvExtTree.SelectedNode = node;
                node.EnsureVisible();
                tvExtTree.Focus();
            }
            else
                MessageBox.Show("Unable to find node named '" + name + "'.  " +
                    "The name is case-sensitive.  Use the tool tips or enable " +
                    "the form DrawNode events to verify the node names and " +
                    "try again.", "Tree View Demo", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            #endregion
        }

        /// <summary>
        /// Display the names of all currently checked nodes in the tree view
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnCheckedNames_Click(object sender, EventArgs e)
        {
            if(tvExtTree.CheckBoxes == false)
            {
                MessageBox.Show("The Checkboxes property must be set to true for this to work", "Tree View Demo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CheckedNodesCollection checkedNodes = tvExtTree.CheckedNodes;

            // The collection's ToNameString() method will return a comma separated list of the Name property
            // values.
            MessageBox.Show("Currently checked node names:\r\n" + checkedNodes.ToNameString(), "Tree View Demo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Display the names of all currently checked nodes in the tree view
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnCheckedText_Click(object sender, EventArgs e)
        {
            if(tvExtTree.CheckBoxes == false)
            {
                MessageBox.Show("The Checkboxes property must be set to true for this to work", "Tree View Demo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CheckedNodesCollection checkedNodes = tvExtTree.CheckedNodes;

            // The collection's ToTextValueString() method will return a comma separated list of the Text
            // property values.
            MessageBox.Show("Currently checked node text values:\r\n" + checkedNodes.ToTextValueString(),
                "Tree View Demo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}
