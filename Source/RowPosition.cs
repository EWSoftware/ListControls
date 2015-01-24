//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : RowPosition.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/30/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains the enumerated type that defines the fixed row positions that can be used with the
// DataList and DataNavigator MoveTo() methods.
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
    /// This enumerated type defines the fixed row positions that can be used with the
    /// <see cref="DataList.MoveTo(RowPosition)">DataList.MoveTo</see> and
    /// <see cref="DataNavigator.MoveTo(RowPosition)">DataNavigator.MoveTo</see> methods.
    /// </summary>
    [Serializable]
    public enum RowPosition
    {
        /// <summary>
        /// Move to the first row.
        /// </summary>
        FirstRow,
        /// <summary>
        /// Move to the last row.
        /// </summary>
        LastRow,
        /// <summary>
        /// Move to the next row.
        /// </summary>
        NextRow,
        /// <summary>
        /// Move to the previous row.
        /// </summary>
        PreviousRow,
        /// <summary>
        /// Move to the new row.  This is only possible if the data list/data navigator allows additions.</summary>
        NewRow
    }
}
