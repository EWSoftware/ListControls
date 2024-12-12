//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : TreeNodeEnumerator.cs
// Author  : Eric Woodruff
// Updated : 12/10/2024
// Note    : Copyright 2007-2024, Eric Woodruff, All rights reserved
//
// This file contains a type-safe enumerator used to enumerate the nodes in a tree view or a branch of a tree
// view recursively.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/15/2007  EFW  Created the code
//===============================================================================================================

using System.Collections;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// A type-safe enumerator for tree view controls that can be used to enumerate all of its nodes recursively
    /// or one branch of it.
    /// </summary>
    /// <remarks>In addition to the <see cref="ExtendedTreeView"/>, this can be used manually to enumerate the
    /// nodes in a standard <see cref="TreeView"/> control as well.</remarks>
    /// <example>
    /// <code language="cs" source="..\Demos\ListControlDemoCS\ExtendedTreeViewTestForm.cs"
    ///   region="Enumerate ExtendedTreeView control"
    ///   title="C# - Enumerate the entire tree" />
    /// <code language="vbnet" source="..\Demos\ListControlDemoVB\ExtendedTreeViewTestForm.vb"
    ///   region="Enumerate ExtendedTreeView control"
    ///   title="VB.NET - Enumerate the entire tree" />
    /// <code language="cs" source="..\Demos\ListControlDemoCS\ExtendedTreeViewTestForm.cs"
    ///   region="TreeNodeEnumerator Example"
    ///   title="C# - Enumerate starting at a selected node" />
    /// <code language="vbnet" source="..\Demos\ListControlDemoVB\ExtendedTreeViewTestForm.vb"
    ///   region="TreeNodeEnumerator Example"
    ///   title="VB.NET - Enumerate starting at a selected node" />
    /// </example>
    public class TreeNodeEnumerator : IEnumerator
    {
        #region Private data members
        //=====================================================================

        private readonly TreeNode root;
        private TreeNode? current;
        private readonly bool enumSibs;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="start">The node at which to start enumeration</param>
        /// <param name="enumerateSiblings">True to enumerate the starting node's siblings as well or false to
        /// stop after enumerating the starting node and all of its children.</param>
        /// <example>
        /// <code language="cs" source="..\Demos\ListControlDemoCS\ExtendedTreeViewTestForm.cs"
        ///   region="Enumerate ExtendedTreeView control"
        ///   title="C# - Enumerate the entire tree" />
        /// <code language="vbnet" source="..\Demos\ListControlDemoVB\ExtendedTreeViewTestForm.vb"
        ///   region="Enumerate ExtendedTreeView control"
        ///   title="VB.NET - Enumerate the entire tree" />
        /// <code language="cs" source="..\Demos\ListControlDemoCS\ExtendedTreeViewTestForm.cs"
        ///   region="TreeNodeEnumerator Example"
        ///   title="C# - Enumerate starting at a selected node" />
        /// <code language="vbnet" source="..\Demos\ListControlDemoVB\ExtendedTreeViewTestForm.vb"
        ///   region="TreeNodeEnumerator Example"
        ///   title="VB.NET - Enumerate starting at a selected node" />
        /// </example>
        public TreeNodeEnumerator(TreeNode start, bool enumerateSiblings)
        {
            root = start;
            enumSibs = enumerateSiblings;
        }
        #endregion

        #region IEnumerator Members
        //=====================================================================

        /// <summary>
        /// Type-safe enumerator Current method
        /// </summary>
        /// <returns>The current item or null if there are no items</returns>
        public TreeNode? Current => current;

        /// <summary>
        /// Type-unsafe IEnumerator.Current
        /// </summary>
        object IEnumerator.Current => current!;

        /// <summary>
        /// Move to the next element
        /// </summary>
        /// <returns>Returns true if not at the end, false if it is</returns>
        public bool MoveNext()
        {
            // First time through
            if(current == null)
            {
                current = root;
                return (current != null);
            }

            // Enumerate the node's nodes if any
            if(current.Nodes.Count != 0)
            {
                current = current.Nodes[0];
                return true;
            }

            // Move on to the next sibling if allowed
            try
            {
                if(current.NextNode != null)
                {
                    if(current == root && !enumSibs)
                        return false;

                    current = current.NextNode;
                    return true;
                }
            }
            catch(NullReferenceException)
            {
                // If enumerating node not added to a tree view, this exception will get thrown when it tries
                // to move to look for the next node of the detached node.  We can ignore it and tree it as the
                // end of the list.
                return false;
            }

            do
            {
                // Go back to the parent
                current = current.Parent;

                if(current == null)
                    return false;

                // Not at the starting node?
                if(current != root)
                {
                    // Don't go higher than the starting node's level
                    if(current.Level < root.Level)
                        return false;

                    if(current.NextNode != null)
                    {
                        current = current.NextNode;
                        return true;
                    }
                }
                else
                {
                    // If not enumerating the starting node's siblings, stop now
                    if(!enumSibs)
                        return false;

                    // Enumerate the starting node's next sibling
                    if(current.NextNode != null)
                    {
                        current = current.NextNode;
                        return true;
                    }
                }

            } while(current != null);

            return false;
        }

        /// <summary>
        /// Reset the enumerator to the start
        /// </summary>
        public void Reset()
        {
            current = null;
        }
        #endregion

        #region Static helper method
        //=====================================================================

        /// <summary>
        /// This method can be used to enumerate tree nodes in a more convenient way using a <c>foreach</c>
        /// loop without having to manually construct and manage the enumerator.
        /// </summary>
        /// <param name="start">The node at which to start enumeration.</param>
        /// <param name="enumerateSiblings">True to enumerate the starting node's siblings as well or false to
        /// stop after enumerating the starting node and all of its children.</param>
        /// <returns>An enumerable list of tree nodes</returns>
        public static IEnumerable<TreeNode> Enumerate(TreeNode start, bool enumerateSiblings)
        {
            var tne = new TreeNodeEnumerator(start, enumerateSiblings);

            while(tne.MoveNext())
                yield return tne.Current!;
        }
        #endregion
    }
}
