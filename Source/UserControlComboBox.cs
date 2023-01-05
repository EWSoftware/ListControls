//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : UserControlComboBox.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
//
// This file contains a user control combo box control that supports all features of the standard Windows Forms
// combo box but displays a user control for its drop-down and has some extra features such as auto-completion
// and row/column indexers.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/01/2005  EFW  Created the code
//===============================================================================================================

// Ignore Spelling: typeof

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This is a user control combo box control that supports all features of the standard Windows Forms combo
    /// box but displays a user control for its drop-down and has some extra features such as auto-completion and
    /// row/column indexers.
    /// </summary>
    [Description("A combo box that supports a user-defined control for its drop-down")]
    public class UserControlComboBox : BaseComboBox
    {
        #region Private data members
        //====================================================================

        private Type dropDownControl;

        #endregion

        #region Properties
        //====================================================================

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
                    this.RefreshSubControls();
                }
            }
        }
        #endregion

        #region Other Events
        //=====================================================================

        /// <summary>
        /// This event is raised when the drop-down control has been created but before it has been initialized
        /// </summary>
        /// <remarks>This can be used to make adjustments to the drop-down control after it has been created but
        /// before it is initialized and displayed.</remarks>
		[Category("Behavior"), Description("Occurs when the drop-down control is created but before it is " +
          "initialized")]
        public event EventHandler DropDownControlCreated;

        /// <summary>
        /// This raises the <see cref="DropDownControlCreated"/> event
        /// </summary>
        /// <param name="dropDown">The drop-down control</param>
        protected internal virtual void OnDropDownControlCreated(DropDownControl dropDown)
        {
            DropDownControlCreated?.Invoke(dropDown, EventArgs.Empty);
        }

        /// <summary>
        /// This event is raised after the drop-down control has been initialized
        /// </summary>
        /// <remarks>This can be used to make adjustments to the drop-down control after it has been initialized
        /// but before it is displayed.
        /// </remarks>
		[Category("Behavior"), Description("Occurs after the drop-down control has been initialized")]
        public event EventHandler DropDownControlInitialized;

        /// <summary>
        /// This raises the <see cref="DropDownControlInitialized"/> event
        /// </summary>
        /// <param name="dropDown">The drop-down control</param>
        protected internal virtual void OnDropDownControlInitialized(DropDownControl dropDown)
        {
            DropDownControlInitialized?.Invoke(dropDown, EventArgs.Empty);
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>By default, auto-completion is enabled</remarks>
        public UserControlComboBox()
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is used to create the drop-down control when needed
        /// </summary>
        protected override void CreateDropDown()
        {
            // Simple mode uses a different container for this one
            if(this.DropDownStyle != ComboBoxStyle.Simple)
                this.DropDownInterface = new UserControlDropDown(this);
            else
                this.DropDownInterface = new UserControlSimple(this);

            // Add it to the control list in simple mode
            if(this.DropDownStyle == ComboBoxStyle.Simple)
            {
                this.Controls.Add((Control)this.DropDownInterface);
                this.DropDownInterface.ShowDropDown();
                this.PerformLayout();
            }
        }

        /// <summary>
        /// This can be called to force the drop-down portion of the combo box to refresh its settings
        /// </summary>
        /// <remarks>This method will raise the <see cref="BaseListControl.SubControlsRefreshed"/> event when
        /// called.  You can add an event handler to it to reload your preferred settings whenever the drop-down
        /// is refreshed.</remarks>
        public override void RefreshSubControls()
        {
            if(this.DropDownInterface != null)
            {
                // If it's currently being created, don't dispose of it
                if(this.DropDownInterface.IsCreating)
                    return;

                // We do this so that it doesn't dispose of it a second time in the Collection Changed event
                // handler.
                Control dd = (Control)this.DropDownInterface;
                this.DropDownInterface = null;

                if(this.DropDownStyle == ComboBoxStyle.Simple)
                    this.Controls.Remove(dd);

                dd.Dispose();
            }

            base.RefreshSubControls();

            // Recreate the drop-down if using the simple style
            if(this.DropDownStyle == ComboBoxStyle.Simple)
                this.CreateDropDown();
            else
                this.PerformLayout();
        }

        /// <summary>
        /// This is overridden to scroll the selected item with the mouse wheel
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if(e != null && this.DropDownStyle != ComboBoxStyle.Simple)
            {
                if(e.Delta > 0)
                    this.HandleKeys(Keys.Up);
                else
                    this.HandleKeys(Keys.Down);

                if(e is HandledMouseEventArgs mea)
                    mea.Handled = true;
            }

            base.OnMouseWheel(e);
        }
        #endregion
    }
}
