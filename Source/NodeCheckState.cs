//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : NodeCheckState.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/30/2014
// Note    : Copyright 2006-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated type that defines the node checked states for the ExtendedTreeView control
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/12/2006  EFW  Created the code
//===============================================================================================================

using System;
using System.Windows.Forms;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This public enumerated type defines the node checked states for the <see cref="ExtendedTreeView"/>
    /// control.
    /// </summary>
    /// <remarks>These values can be used for comparison against a tree node's <see cref="TreeNode.StateImageIndex"/>
    /// to determine its checked state in addition to the <see cref="TreeNode.Checked"/> property.  This is
    /// useful when parent/child checked states are synchronized and you need to determine if the parent node is
    /// in a mixed state.</remarks>
    [Serializable]
    public enum NodeCheckState
    {
        /// <summary>
        /// The node has no state image associated with it.
        /// </summary>
        None = -1,
        /// <summary>
        /// The node is in an unchecked state.  When the parent/child checked states are synchronized, it also
        /// indicates that all child nodes are unchecked as well.
        /// </summary>
        Unchecked,
        /// <summary>
        /// The node is in a checked state.  When parent/child checked states are synchronized, it also indicates
        /// that all child nodes are checked as well.
        /// </summary>
        Checked,
        /// <summary>
        /// This state is only available when parent/child checked states are synchronized.  This indicates that
        /// the node is in a checked state but the child nodes contain a mix of checked and unchecked states.
        /// </summary>
        Mixed
    }
}
