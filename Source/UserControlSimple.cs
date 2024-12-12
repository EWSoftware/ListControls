//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : UserControlSimple.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains a combo box user control that handles the display of the user control for the user control
// combo box in simple mode.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 09/08/2005  EFW  Created the code
// 05/01/2006  EFW  Implemented the IDropDown.Scroll method
//===============================================================================================================

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This is a user control that handles the display of the user control
    /// for the user control combo box in simple mode.
    /// </summary>
    [ToolboxItem(false)]
	internal sealed class UserControlSimple : UserControl, IDropDown
	{
        #region Private data members
        //=====================================================================

        private readonly UserControlComboBox owner;
        private DropDownControl? ddControl;
        private int startIndex;
        private bool isCreating, hasInitialized;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This returns the index in effect when the drop-down is shown
        /// </summary>
        /// <value>The combo box uses this to determine whether to fire the selection change committed or
        /// selection change canceled event when the drop-down is closed.</value>
        public int StartIndex => startIndex;

        /// <summary>
        /// This returns a flag indicating whether or not the drop-down is currently being created
        /// </summary>
        /// <value>The combo box uses this to determine whether or not to refresh the sub-controls in certain
        /// situations.</value>
        public bool IsCreating => isCreating;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cb">The owner of the drop-down</param>
        internal UserControlSimple(UserControlComboBox cb)
        {
            owner = cb;

            // Set the value of the double-buffering style bits to true
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            this.Width = owner.Width;
            this.BackColor = owner.DropDownBackColor;
            this.TabStop = false;
		}
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is used to initialize the drop-down styles and data source
        /// </summary>
        private void InitDropDown()
        {
            // Create and initialize the drop-down control
            if(owner.DropDownControl != null)
            {
                ConstructorInfo ctor = owner.DropDownControl.GetConstructor(Type.EmptyTypes)!;
                ddControl = (DropDownControl)ctor.Invoke(null);
                ddControl.Location = new Point(0, 0);
                ddControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
                ddControl.AutoScroll = true;
                ddControl.Font = owner.DropDownFont;
                ddControl.ComboBox = owner;

                if(ddControl.BackColor == SystemColors.Control)
                    ddControl.BackColor = owner.DropDownBackColor;

                ddControl.Height = this.Height;
                ddControl.Width = this.Width;
                this.Controls.Add(ddControl);

                // Tell everyone it has been created
                owner.OnDropDownControlCreated(ddControl);

                // Give the drop-down control a chance to perform any necessary initialization
                ddControl.InitializeDropDown();

                // Tell everyone it has been initialized
                owner.OnDropDownControlInitialized(ddControl);
            }

            hasInitialized = true;
        }

        /// <summary>
        /// This is used to perform any additional tasks when showing the drop-down
        /// </summary>
        public void ShowDropDown()
        {
            if(!hasInitialized)
                this.CreateHandle();

            startIndex = owner.SelectedIndex;

            // Let the drop-down perform any initialization prior to display
            ddControl?.ShowDropDown();
        }

        /// <summary>
        /// Not used in this control
        /// </summary>
        /// <param name="rows">The number of rows to scroll</param>
        public void ScrollDropDown(int rows)
        {
        }

        /// <summary>
        /// This is overridden to initialize the drop-down control when the handle is created
        /// </summary>
        protected override void CreateHandle()
        {
            try
            {
                isCreating = true;
                base.CreateHandle();

                if(!hasInitialized)
                    this.InitDropDown();
            }
            finally
            {
                isCreating = false;
            }
        }

        /// <summary>
        /// This is overridden to ensure it redraws when resized
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Invalidate();
            this.Update();
        }
        #endregion
    }
}
