//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : UserControlDropDown.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 04/09/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
//
// This file contains a combo box drop-down form that handles the display of the user control for the user
// control combo box.  Because this is a form, it does not support Simple mode.
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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace EWSoftware.ListControls
{
	/// <summary>
	/// This is a drop-down list form that shows when the down arrow is clicked on the user control combo box.
    /// Because it is a form, it does not support Simple mode.
	/// </summary>
	[ToolboxItem(false)]
	internal class UserControlDropDown : Form, IDropDown
	{
        #region Private data members
        //=====================================================================

        private readonly UserControlComboBox owner;
        private DropDownControl ddControl;
        private int startIndex;
        private bool isCreating, hasInitialized;

        private Point dragOffset;
        private Cursor priorCursor;

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
        internal UserControlDropDown(UserControlComboBox cb)
        {
            owner = cb;

            // Set the value of the double-buffering style bits to true
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            this.ClientSize = new Size(142, 122);
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.Name = "UserControlDropDown";
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.Manual;
            this.TopMost = true;
            this.BackColor = owner.DropDownBackColor;
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
                ConstructorInfo ctor = owner.DropDownControl.GetConstructor(Type.EmptyTypes);
                ddControl = (DropDownControl)ctor.Invoke(null);
                ddControl.Location = new Point(0, 0);
                ddControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
                ddControl.AutoScroll = true;
                ddControl.Font = owner.DropDownFont;
                ddControl.ComboBox = owner;

                if(ddControl.BackColor == SystemColors.Control)
                    ddControl.BackColor = owner.DropDownBackColor;

                // Add 16 pixels so that we can draw the sizing grip
                this.Height = ddControl.Height + 16;
                this.Width = ddControl.Width + 2;
                this.MinimumSize = ddControl.MinimumSize;
                this.Controls.Add(ddControl);

                // Tell everyone it has been created
                owner.OnDropDownControlCreated(ddControl);

                // Give the drop-down control a chance to perform any necessary initialization
                ddControl.InitializeDropDown();

                // Tell everyone it has been initialized
                owner.OnDropDownControlInitialized(ddControl);
            }

            // Size the drop-down to the owner's width if it is wider
            if(this.Width < owner.Width)
                this.Width = owner.Width;

            hasInitialized = true;
        }

        /// <summary>
        /// This is used to size and position the drop-down, perform any additional tasks, and show it
        /// </summary>
        public void ShowDropDown()
        {
            int idx;

            if(!hasInitialized)
                this.CreateHandle();

            startIndex = owner.SelectedIndex;
            dragOffset = Point.Empty;

            // Let the drop-down perform any initialization prior to display
            ddControl?.ShowDropDown();

            if(this.Width < owner.Width)
                this.Width = owner.Width;

            // Make sure we are within the screen bounds.  Take into account the working area of all screens.
            // Note that in certain setups, the leftmost/uppermost screen(s) may have negative coordinates.
            Rectangle workingArea, screen = new Rectangle();

            foreach(Screen s in Screen.AllScreens)
            {
                workingArea = s.WorkingArea;

                if(workingArea.X < screen.X)
                    screen.X = workingArea.X;

                if(workingArea.Y < screen.Y)
                    screen.Y = workingArea.Y;

                if(Math.Abs(workingArea.X) + workingArea.Width > screen.Width)
                    screen.Width = Math.Abs(workingArea.X) + workingArea.Width;

                if(Math.Abs(workingArea.Y) + workingArea.Height > screen.Height)
                    screen.Height = Math.Abs(workingArea.Y) + workingArea.Height;
            }

            Point pOwner = owner.Parent.PointToScreen(owner.Location);
            pOwner.Y += owner.Height;

            Point p = pOwner;

            if(this.Width > screen.Width)
                this.Width = screen.Width;

            if(this.Height > screen.Height)
                this.Height = screen.Height;

            if(p.X < screen.X)
                p.X = screen.X;

            if(p.Y < screen.Y)
                p.Y = screen.Y;

            if(p.X + this.Width > screen.X + screen.Width)
                p.X = screen.X + screen.Width - this.Width - 1;

            if(p.Y + this.Height > screen.Y + screen.Height)
                p.Y = screen.Y + screen.Height - this.Height - 1;

            // If we are covering the combo box, move the location above the combo box
            if(p.Y < pOwner.Y)
            {
                idx = pOwner.Y - this.Height - owner.Height;

                // If we would go off the top of the screen, figure out whether we would have more rows visible
                // if we moved the form above the combo box or left it where it is at below the combo box and
                // shrink it to fit.
                if(idx < screen.Y)
                    if(this.Height + idx > this.Height - (pOwner.Y - p.Y))
                    {
                        this.Height += idx;
                        idx = screen.Y;
                    }
                    else
                    {
                        this.Height -= (pOwner.Y - p.Y);
                        idx = pOwner.Y;
                    }

                p.Y = idx;
            }

            // The first time it's shown, the size may change due to auto-scaling
            Size sz = this.Size;

            this.Location = p;
            this.Show();
            this.BringToFront();

            if(this.Size != sz)
                this.Size = sz;
        }

        /// <summary>
        /// Not used in this control
        /// </summary>
        /// <param name="rows">The number of rows to scroll</param>
        public void ScrollDropDown(int rows)
        {
        }

        /// <summary>
        /// This is overridden to hide the drop-down when it loses the focus
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnDeactivate(EventArgs e)
        {
            // Transfer focus back to the combo box's parent
            owner.ParentForm.Focus();

            base.OnDeactivate(e);
            owner.DroppedDown = false;
        }

        /// <summary>
        /// This is overridden to hide the drop-down when Escape is hit
        /// </summary>
        /// <param name="keyData">The key to process</param>
        /// <returns>True if handled, false if not</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if(keyData == Keys.Escape)
            {
                owner.DroppedDown = false;
                return true;
            }

            return base.ProcessDialogKey(keyData);
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

        /// <summary>
        /// This is overridden to draw a sizing grip in the lower right corner of the window
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            ControlPaint.DrawSizeGrip(e.Graphics, Color.White, this.Width - 17, this.Height - 17, 16, 16);
        }

        /// <summary>
        /// This is overridden to get the offset used for resizing if necessary
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Point p = this.PointToClient(Cursor.Position);

            Rectangle r = new Rectangle(this.Size.Width - 16,
                this.Size.Height - 16, 16, 16);

            if(e.Button == MouseButtons.Left && r.Contains(p))
        	    dragOffset = new Point(this.Width - p.X, this.Height - p.Y);
            else
                dragOffset = Point.Empty;

            base.OnMouseDown(e);
        }

        /// <summary>
        /// This is overridden to resize the window when necessary
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int nWidth, nHeight;

            Point p = this.PointToClient(Cursor.Position);

            // Only resize if dragging and enforce a minimum size
    		if(e.Button == MouseButtons.Left && dragOffset != Point.Empty)
            {
                nWidth = Cursor.Position.X - this.Location.X + dragOffset.X;
                nHeight = Cursor.Position.Y - this.Location.Y + dragOffset.Y;

                Size minSize = (ddControl != null) ? ddControl.MinimumSize :
                    new Size(30, 30);

                if(nWidth < owner.Width)
                    nWidth = owner.Width;
                else
                    if(nWidth < minSize.Width)
                        nWidth = minSize.Width;

                if(nHeight < minSize.Height)
                    nHeight = minSize.Height;

        		this.Size = new Size(nWidth, nHeight);
            }

            Rectangle r = new Rectangle(this.Size.Width - 16, this.Size.Height - 16, 16, 16);

            if(r.Contains(p))
            {
                if(this.Cursor != Cursors.SizeNWSE)
                {
                    priorCursor = this.Cursor;
                    this.Cursor = Cursors.SizeNWSE;
                }
            }
            else
                if(this.Cursor == Cursors.SizeNWSE)
                    this.Cursor = priorCursor;

            base.OnMouseMove(e);
        }

        /// <summary>
        /// This is overridden to reset the mouse cursor if the mouse leaves the drop-down
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <remarks>This also sets the capture again if necessary as the mouse is not inside the window to begin
        /// with and it loses the capture shortly after it is set in <see cref="ShowDropDown"/>.
        /// </remarks>
        protected override void OnMouseLeave(EventArgs e)
        {
            if(this.Cursor == Cursors.SizeNWSE)
                this.Cursor = priorCursor;

            base.OnMouseLeave(e);
        }
        #endregion
    }
}
