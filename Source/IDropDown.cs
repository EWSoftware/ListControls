//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : IDropDown.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/08/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains a simple interface for the drop-down container classes so that the base combo box class
// can access some of their methods.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 09/10/2005  EFW  Created the code
// 05/01/2006  EFW  Added ScrollDropDown method
//===============================================================================================================

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This file contains a simple interface for the drop-down container classes so that the base combo box
    /// class can access some of their methods.
    /// </summary>
    internal interface IDropDown
    {
        /// <summary>
        /// Show or hide the drop-down
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// Get or set the top position
        /// </summary>
        int Top { get; set; }

        /// <summary>
        /// Get or set the left position
        /// </summary>
        int Left { get; set; }

        /// <summary>
        /// Get or set the width
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// Get or set the height
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// This returns the index in effect when the drop-down is shown
        /// </summary>
        /// <value>The combo box uses this to determine whether to fire the selection change committed or
        /// selection change canceled event when the drop-down is closed.</value>
        int StartIndex { get; }

        /// <summary>
        /// This returns a flag indicating whether or not the drop-down is currently being created
        /// </summary>
        /// <value>The combo box uses this to determine whether or not to refresh the sub-controls in certain
        /// situations.</value>
        bool IsCreating { get; }

        /// <summary>
        /// This is used to set the size and position the drop-down, perform any additional tasks, and show it
        /// </summary>
        void ShowDropDown();

        /// <summary>
        /// Scroll the drop-down the specified number of rows
        /// </summary>
        /// <param name="rows">The number of rows to scroll</param>
        void ScrollDropDown(int rows);
    }
}
