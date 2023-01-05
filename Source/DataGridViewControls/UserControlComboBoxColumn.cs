//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : UserControlComboBoxColumn.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2023
// Note    : Copyright 2007-2023, Eric Woodruff, All rights reserved
//
// This file contains a data grid view column object that hosts user control combo box cells
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 06/08/2007  EFW  Created the code
//===============================================================================================================

// Ignore Spelling: typeof

using System;
using System.ComponentModel;
using System.Globalization;

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This data grid view column contains <see cref="UserControlComboBoxCell"/> objects
    /// </summary>
    public class UserControlComboBoxColumn : BaseComboBoxColumn
    {
        #region Private data members
        //=====================================================================

        private Type dropDownControl;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// The user control type to use for the drop-down
        /// </summary>
        /// <remarks>This property must be set in order for the drop-down to be of any use</remarks>
        /// <exception cref="ArgumentException">This is thrown if the specified type is not derived from
        /// <see cref="DropDownControl"/>.</exception>
        /// <example>
        /// <code language="cs">
        /// // TreeViewDropDown is a user control derived from DropDownControl
        /// ucCombo.DropDownControl = typeof(TreeViewDropDown);
        /// </code>
        /// <code language="vbnet">
        /// ' TreeViewDropDown is a user control derived from DropDownControl
        /// ucCombo.DropDownControl = GetType(TreeViewDropDown)
        /// </code>
        /// </example>
        [Browsable(false), Description("The user control type to use for the drop-down"),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Type DropDownControl
        {
            get => dropDownControl;
            set
            {
                if(dropDownControl != value)
                {
                    if(value != null && !value.IsSubclassOf(typeof(DropDownControl)))
                        throw new ArgumentException(LR.GetString("ExInvalidDropDownTemplateType"));

                    dropDownControl = value;
                }
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public UserControlComboBoxColumn() : base(new UserControlComboBoxCell())
        {
            this.ComboBoxCellTemplate.TemplateComboBoxColumn = this;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is used to clone the column
        /// </summary>
        /// <returns>A clone of the column</returns>
        public override object Clone()
        {
            UserControlComboBoxColumn clone = (UserControlComboBoxColumn)base.Clone();
            clone.DropDownControl = dropDownControl;

            return clone;
        }

        /// <summary>
        /// Convert the column to its string description
        /// </summary>
        /// <returns>A string description of the column</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "UserControlComboBoxColumn {{ Name={0}, Index={1} }}",
                this.Name, this.Index);
        }
        #endregion
    }
}
