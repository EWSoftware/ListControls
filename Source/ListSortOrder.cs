//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : ListSortOrder.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/08/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains the enumerated type that defines the sort order option for list controls
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

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This enumerated type defines the sort order option for list controls
    /// </summary>
    [Serializable]
    public enum ListSortOrder
    {
        /// <summary>
        /// None.  This is the default and is the only value allowed if the list control has a data source
        /// assigned to it.
        /// </summary>
        None,
        /// <summary>
        /// Sort in ascending order.
        /// </summary>
        Ascending,
        /// <summary>
        /// Sort in descending order.
        /// </summary>
        Descending
    }
}
