//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : LayoutMethod.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/30/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated types for the library.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/16/2005  EFW  Created the code
//===============================================================================================================

using System;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This enumerated type defines the layout method for the <see cref="BaseButtonList"/> control and its
    /// derived classes.
    /// </summary>
    /// <remarks>In all cases, the <c>BaseButtonList</c> derived classes will display scrollbars as needed if all
    /// items will not fit within their bounds.
    /// </remarks>
    [Serializable]
    public enum LayoutMethod
    {
        /// <summary>
        /// A single column of buttons.  This is the default.
        /// </summary>
        SingleColumn,
        /// <summary>
        /// A single row of buttons.
        /// </summary>
        SingleRow,
        /// <summary>
        /// A multi-column list.  There will be as many rows as will fit vertically and extra columns are added
        /// to handle any overflow.
        /// </summary>
        DownThenAcross,
        /// <summary>
        /// A multi-column list.  There will be as many columns as will fit horizontally and extra rows are added
        /// to handle any overflow.
        /// </summary>
        AcrossThenDown
    }
}
