//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : ChangePolicyEventArgs.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains the event argument class used to provide change policy modifications for the DataList and
// DataNavigator controls.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/20/2005  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.ListControls
{
	/// <summary>
	/// This is used for the <see cref="DataList.ChangePolicyModified"> DataList.ChangePolicyModified</see> and
    /// <see cref="DataNavigator.ChangePolicyModified"> DataNavigator.ChangePolicyModified</see> events.
	/// </summary>
	public class ChangePolicyEventArgs : EventArgs
	{
        /// <summary>
        /// This is used to determine whether or not additions are allowed to be made to the data source
        /// </summary>
        public bool AllowAdditions { get; }

        /// <summary>
        /// This is used to determine whether or not edits are allowed to be made to the data source
        /// </summary>
        public bool AllowEdits { get; }

        /// <summary>
        /// This is used to determine whether or not deletes are allowed to be made to the data source
        /// </summary>
        public bool AllowDeletes { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="canAdd">Whether or not additions are allowed</param>
        /// <param name="canEdit">Whether or not edits are allowed</param>
        /// <param name="canDelete">Whether or not deletions are allowed</param>
        public ChangePolicyEventArgs(bool canAdd, bool canEdit, bool canDelete)
        {
            this.AllowAdditions = canAdd;
            this.AllowEdits = canEdit;
            this.AllowDeletes = canDelete;
        }
	}
}
