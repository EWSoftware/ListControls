//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : ChangePolicy.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains the class that keeps track of the change policy for the DataList and DataNavigator
// controls.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/29/2005  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This class is used to contain the change policy for the data source in the <see cref="DataList"/> and
    /// <see cref="DataNavigator"/> controls.
    /// </summary>
    internal sealed class ChangePolicy
    {
        #region Private data members
        //=====================================================================

        private readonly DataList? dataList;
        private readonly DataNavigator? dataNav;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to determine whether or not additions are allowed to be made to the data source
        /// </summary>
        internal bool AllowAdditions { get; private set; }

        /// <summary>
        /// This is used to determine whether or not edits are allowed to be made to the data source
        /// </summary>
        internal bool AllowEdits { get; private set; }

        /// <summary>
        /// This is used to determine whether or not deletes are allowed to be made to the data source
        /// </summary>
        internal bool AllowDeletes { get; private set; }

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="owner">The control that owns this policy object</param>
        /// <remarks>Additions, edits, and deletes are allowed by default</remarks>
        internal ChangePolicy(object owner)
        {
            dataList = owner as DataList;

            if(dataList == null)
                dataNav = (DataNavigator)owner;

            this.AllowAdditions = this.AllowEdits = this.AllowDeletes = true;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is used to update the change policy based on the owning data list or data navigator
        /// </summary>
        /// <param name="canAdd">If true and the data source permits it, allow additions</param>
        /// <param name="canEdit">If true and the data source permits it, allow edits</param>
        /// <param name="canDelete">If true and the data source permits it, allow deletes</param>
        internal void UpdatePolicy(bool canAdd, bool canEdit, bool canDelete)
        {
            bool listCanAdd, listCanEdit, listCanDelete, listChangeNotify, oldAdd = this.AllowAdditions,
                oldEdit = this.AllowEdits, oldDelete = this.AllowDeletes;

            CurrencyManager? listManager = dataList?.ListManager ?? dataNav?.ListManager;

            if(listManager == null)
            {
                this.AllowAdditions = canAdd;
                this.AllowEdits = canEdit;
                this.AllowDeletes = canDelete;
            }
            else
            {
                if(listManager.List is IBindingList bl)
                {
                    listCanAdd = bl.AllowNew;
                    listCanEdit = bl.AllowEdit;
                    listCanDelete = bl.AllowRemove;
                    listChangeNotify = bl.SupportsChangeNotification;
                }
                else
                {
                    listCanAdd = listCanDelete = (!listManager.List.IsReadOnly && !listManager.List.IsFixedSize);
                    listCanEdit = !listManager.List.IsReadOnly;
                    listChangeNotify = false;
                }

                this.AllowAdditions = (canAdd && listCanAdd && listChangeNotify);
                this.AllowEdits = (canEdit && listCanEdit);
                this.AllowDeletes = (canDelete && listCanDelete && listChangeNotify);
            }

            // If the change policy was modified, raise the ChangePolicyModified event on the owner
            if(dataList != null)
            {
                if(this.AllowAdditions != oldAdd || this.AllowEdits != oldEdit || this.AllowDeletes != oldDelete)
                {
                    dataList.OnChangePolicyModified(new ChangePolicyEventArgs(this.AllowAdditions,
                        this.AllowEdits, this.AllowDeletes));
                }
                else
                {
                    // Enable or disable Add and Delete based on the policy
                    dataList.btnAdd.Enabled = (this.AllowAdditions && listManager != null);
                    dataList.btnDelete.Enabled = (this.AllowDeletes && listManager != null &&
                        listManager.Count > 0);
                }
            }
            else
            {
                if(this.AllowAdditions != oldAdd || this.AllowEdits != oldEdit || this.AllowDeletes != oldDelete)
                {
                    dataNav!.OnChangePolicyModified(new ChangePolicyEventArgs(this.AllowAdditions, this.AllowEdits,
                        this.AllowDeletes));
                }
                else
                {
                    // Enable or disable Add and Delete based on the policy
                    dataNav!.btnAdd.Enabled = (this.AllowAdditions && listManager != null);
                    dataNav.btnDelete.Enabled = (this.AllowDeletes && listManager != null && listManager.Count > 0);
                }
            }
        }
        #endregion
    }
}
