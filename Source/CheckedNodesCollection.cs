//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : CheckedNodesCollection.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/08/2024
// Note    : Copyright 2024, Eric Woodruff, All rights reserved
//
// This file contains a collection class used by the ExtendedTreeView control to return a list of checked nodes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/17/2007  EFW  Created the code
//===============================================================================================================

using System.Collections.ObjectModel;
using System.Text;

namespace EWSoftware.ListControls
{
	/// <summary>
	/// This is a derived <see cref="ReadOnlyCollection{T}"/> class that contains a set of nodes from a
    /// <see cref="ExtendedTreeView"/> control.  Each entry represents a node that has a check state of
    /// <c>Checked</c> or <c>Mixed</c>.
	/// </summary>
    /// <remarks>The collection itself cannot be modified, but the items in it can</remarks>
    public class CheckedNodesCollection : ReadOnlyCollection<TreeNode>
    {
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="list">The list to wrap</param>
        internal CheckedNodesCollection(IList<TreeNode> list) : base(list)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Get the check state of the specified item from the tree view
        /// </summary>
        /// <param name="index">The index of the item in this collection</param>
        /// <returns>The check state of the checked item from the checkbox list</returns>
        public NodeCheckState CheckStateOf(int index)
        {
            TreeNode node = this[index];
            ExtendedTreeView tree = (ExtendedTreeView)node.TreeView;

            return tree.GetNodeCheckState(node.Name);
        }

        /// <summary>
        /// This can be used to determine whether or not the collection contains a node with the specified name
        /// </summary>
        /// <param name="name">The node name to find</param>
        /// <returns>True if the collection contains a node with the given name or false if it does not</returns>
        public bool ContainsName(string name)
        {
            foreach(TreeNode n in this)
                if(n.Name == name)
                    return true;

            return false;
        }

        /// <summary>
        /// Convert the checked nodes' names to a comma-separated list
        /// </summary>
        /// <returns>Returns a string containing the node names separated by commas</returns>
        public virtual string ToNameString()
        {
            StringBuilder sb = new(1024);

            for(int idx = 0; idx < this.Count; idx++)
            {
                if(sb.Length > 0)
                    sb.Append(", ");

                sb.Append(this[idx].Name);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Convert the checked nodes' text values to a comma-separated list
        /// </summary>
        /// <returns>Returns a string containing the node text values separated by commas</returns>
        public virtual string ToTextValueString()
        {
            StringBuilder sb = new(1024);

            for(int idx = 0; idx < this.Count; idx++)
            {
                if(sb.Length > 0)
                    sb.Append(", ");

                sb.Append(this[idx].Text);
            }

            return sb.ToString();
        }
        #endregion
    }
}
