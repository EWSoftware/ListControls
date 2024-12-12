// ==============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DropDownControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains a drop-down control used as the source for creating the drop-down portion of the
// UserControlComboBox control.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 09/08/2005  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.ListControls
{
	/// <summary>
    /// This is the drop-down control used as the source for creating the drop-down portion of the
    /// <see cref="UserControlComboBox"/> control.
	/// </summary>
    /// <remarks>Derive a <see cref="UserControl"/> from this class and add your controls to it for use as a
    /// template in the drop-down.</remarks>
	[ToolboxItem(false)]
	public class DropDownControl : UserControl
	{
        #region Properties
        //=====================================================================

        /// <summary>
        /// This read-only property can be used to get the drop-down's parent (the <see cref="UserControlComboBox"/>
        /// that owns it).
        /// </summary>
        [Browsable(false), Description("Get a reference to the drop-down's parent UserControlComboBox")]
        public UserControlComboBox ComboBox { get; internal set; } = null!;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public DropDownControl()
        {
            // Each drop-down needs its own binding context so that controls like combo boxes can be set
            // independently of each other on different controls.
            this.BindingContext = new BindingContext();

            this.MinimumSize = new Size(30, 30);
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This method is called just after the drop-down has been constructed.  It can be overridden to perform
        /// any additional initialization tasks in the derived class.
        /// </summary>
        /// <remarks>The base class implementation does nothing</remarks>
        public virtual void InitializeDropDown()
        {
        }

        /// <summary>
        /// This method is called just prior to showing the drop-down.  It can be overridden to perform any
        /// additional tasks that need to occur before showing the drop-down.
        /// </summary>
        /// <remarks>The base class implementation does nothing</remarks>
        public virtual void ShowDropDown()
        {
        }

        /// <summary>
        /// This can be called to select an item by index and hide the drop-down
        /// </summary>
        /// <param name="idx">The index to select</param>
        /// <remarks>This will guarantee that the drop-down is closed prior to setting the new value so that the
        /// drop-down doesn't block or hide dialog boxes that may get displayed in a user's event handler.
        /// </remarks>
        /// <overloads>There are two overloads for this method</overloads>
        public void CommitSelection(int idx)
        {
            this.ComboBox.CommitSelection(idx);
        }

        /// <summary>
        /// This can be called to select an item by value and hide the drop-down
        /// </summary>
        /// <remarks>This will guarantee that the drop-down is closed prior to setting the new value so that the
        /// drop-down doesn't block or hide dialog boxes that may get displayed in a user's event handler.
        /// </remarks>
        /// <param name="item">The item to select</param>
        public void CommitSelection(object item)
        {
            this.ComboBox.CommitSelection(item);
        }
        #endregion
    }
}
