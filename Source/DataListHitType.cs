//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DataListHitType.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/08/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains the enumerated type that defines the hit test locations for the data list
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
    /// This enumerated type defines the hit test locations for the data list
    /// </summary>
    [Serializable, Flags]
    public enum DataListHitType
    {
        /// <summary>
        /// The location is undefined.
        /// </summary>
        None        = 0x0000,
        /// <summary>
        /// The location is in the data list's header.
        /// </summary>
        Header      = 0x0001,
        /// <summary>
        /// The location is in the data list's footer.
        /// </summary>
        Footer      = 0x0002,
        /// <summary>
        /// The location is in the data list's navigation control area.
        /// </summary>
        Navigation  = 0x0004,
        /// <summary>
        /// The location is in a row header.
        /// </summary>
        RowHeader   = 0x0008,
        /// <summary>
        /// The location is in a row template.
        /// </summary>
        Row         = 0x0010,
        /// <summary>
        /// The location is in a row template or a row header
        /// </summary>
        RowOrHeader = 0x0018
    }
}
