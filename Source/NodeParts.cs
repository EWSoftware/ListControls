//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : NodeParts.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/30/2014
// Note    : Copyright 2006-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated type that defines the various node parts that are to be drawn for the
// ExtendedTreeView control.
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

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This public enumerated type defines the various node parts that are to be drawn for the
    /// <see cref="ExtendedTreeView"/> control.
    /// </summary>
    [Flags, Serializable]
    public enum NodeParts
    {
        /// <summary>
        /// Nothing is to be drawn
        /// </summary>
        None        = 0x0000,
        /// <summary>
        /// Erase the node's background
        /// </summary>
        Background  = 0x0001,
        /// <summary>
        /// Draw connecting node lines
        /// </summary>
        Lines       = 0x0002,
        /// <summary>
        /// Draw the expando (+/-) image
        /// </summary>
        Expando     = 0x0004,
        /// <summary>
        /// Draw the checkbox/state image
        /// </summary>
        StateImage  = 0x0008,
        /// <summary>
        /// Draw the item image
        /// </summary>
        NodeImage   = 0x0010,
        /// <summary>
        /// Draw the text
        /// </summary>
        Text        = 0x0020
    }
}
