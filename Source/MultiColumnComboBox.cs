//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : MultiColumnComboBox.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/16/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a multi-column combo box control that supports all features of the standard Windows Forms
// combo box but displays a multi-column drop-down list and has some extra features such as auto-completion and
// row/column indexers.
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

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This is a multi-column combo box control that supports all features of the standard Windows Forms combo
    /// box but displays a multi-column drop-down list and has some extra features such as auto-completion and
    /// row/column indexers.
    /// </summary>
    [Description("A combo box with support for displaying a multi-column drop-down")]
    public class MultiColumnComboBox : BaseComboBox
    {
        #region Private data members
        //====================================================================

        private int dropDownWidth;
        private DropDownTableStyle ddsDropDownFormat;
        private StringCollection scColumnFilter;

        #endregion

        #region Properties
        //====================================================================

        /// <summary>
        /// This is used to define the formatting for the drop down portion of the combo box including the
        /// columns displayed if so desired.
        /// </summary>
        /// <value>Column definitions can be added to the <c>DropDownFormat.GridColumnStyles</c> collection.
        /// Set as many or as few properties as you need.  If the columns are not defined, the drop-down will
        /// show all columns in the data source by default with some basic style settings.</value>
		[Category("DropDown"), Bindable(true), Description("The drop-down's formatting options"),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DropDownTableStyle DropDownFormat
        {
            get { return ddsDropDownFormat; }
        }

        /// <summary>
        /// Gets or sets the width of the of the drop-down portion of the combo box
        /// </summary>
        /// <value>If set to zero, it will default to an appropriate width based on the <see cref="DropDownFormat"/>
        /// options.</value>
        /// <exception cref="ArgumentException">This is thrown if the width is less than zero</exception>
        [Category("DropDown"), Bindable(true), DefaultValue(0), Description("The default width of the drop-down " +
          "portion of the combo box")]
        public int DropDownWidth
        {
            get { return dropDownWidth; }
            set
            {
                if(value < 0)
                    throw new ArgumentException(LR.GetString("ExInvalidDropDownWidth"));

                dropDownWidth = value;
                OnDropDownWidthChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// This gets the <see cref="StringCollection"/> used to filter the columns displayed by the drop-down
        /// portion of the combo box.
        /// </summary>
        /// <value>This is a quick way to filter the drop-down to a specific set of columns without defining
        /// column definitions using the <see cref="DropDownFormat">DropDownFormat.GridColumnStyles</see>
        /// property collection.  If empty, no filtering takes place.</value>
        /// <example>
        /// <code language="cs">
        /// cboVendor.DisplayMember = "VendorName";
        /// cboVendor.ValueMember = "VendorKey";
        /// cboVendor.ColumnFilter.AddRange(new string[] { "VendorName", "Contact" });
        /// cboVendor.DataSource = GetVendors();
        /// </code>
        /// <code language="vbnet">
        /// cboVendor.DisplayMember = "VendorName"
        /// cboVendor.ValueMember = "VendorKey"
        /// cboVendor.ColumnFilter.AddRange(New String() { "VendorName", "Contact" })
        /// cboVendor.DataSource = GetVendors()
        /// </code>
        /// </example>
        [Category("DropDown"), Description("This defines a column name filter for the drop-down"),
          Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection ColumnFilter
        {
            get
            {
                if(scColumnFilter == null)
                    scColumnFilter = new StringCollection();

                return scColumnFilter;
            }
        }

        /// <summary>
        /// Gets or sets whether or not the selection tracks with the mouse as it moves over the items in the
        /// drop-down.
        /// </summary>
        /// <value>True by default.  If false, the selection in the drop-down will not track with the mouse
        /// cursor.  This option is only used in <c>DropDown</c> and <c>DropDownList</c> mode.  In <c>Simple</c>
        /// mode, the selection never tracks the mouse cursor.</value>
        [Category("DropDown"), DefaultValue(true), Description("Determine whether or not the selection tracks " +
          "the mouse cursor in the drop-down")]
        public bool MouseTracking { get; set; }

        #endregion

        #region Other Events
        //=====================================================================

        /// <summary>
        /// This event is raised when the <see cref="DropDownWidth"/> changes
        /// </summary>
		[Category("Property Changed"), Description("Occurs when the drop-down width changes")]
        public event EventHandler DropDownWidthChanged;

        /// <summary>
        /// This raises the <see cref="DropDownWidthChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnDropDownWidthChanged(EventArgs e)
        {
            var handler = DropDownWidthChanged;

            if(handler != null)
                handler(this, e);
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>By default, the combo box will display all columns in its data source without row or column
        /// headers, all columns will be auto-sized to the longest item within each column, and auto-completion
        /// is enabled.</remarks>
        public MultiColumnComboBox()
        {
            this.MouseTracking = true;

            ddsDropDownFormat = new DropDownTableStyle();
            ddsDropDownFormat.GridColumnStyles.CollectionChanged += GridColumnStyles_CollectionChanged;
        }
        #endregion

        #region Methods and event handlers
        //=====================================================================

        /// <summary>
        /// This is handled to reset the drop-down when its column collection changes so that the changes are
        /// displayed.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void GridColumnStyles_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            if(this.IsHandleCreated && this.DropDownInterface != null && this.DropDownInterface.Visible == false)
                this.RefreshSubControls();
        }

        /// <summary>
        /// This is used to create the drop-down control when needed.
        /// </summary>
        protected override void CreateDropDown()
        {
            this.DropDownInterface = new MultiColumnDropDown(this);

            // Add it to the control list in simple mode
            if(this.DropDownStyle == ComboBoxStyle.Simple)
            {
                this.Controls.Add((Control)this.DropDownInterface);
                this.DropDownInterface.ShowDropDown();
                this.PerformLayout();
            }
        }

        /// <summary>
        /// This is overridden to synchronize the drop-down with the selected index when the drop-down is visible
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);

            if(this.DroppedDown)
                ((MultiColumnDropDown)this.DropDownInterface).SelectItem(base.SelectedIndex);
        }

        /// <summary>
        /// This can be called to force the drop-down portion of the combo box to refresh its format settings
        /// </summary>
        /// <remarks><para>Note that this will clear all column definitions from <see cref="DropDownFormat"/> so
        /// if you need to, call this method first and then load the column definitions.  The columns in the
        /// table style do not completely disconnect from the grid when it is disposed.  Unfortunately, there is
        /// no way to do this so the columns must be disposed of as well and recreated.  This is only a problem
        /// if you are modifying the settings after the combo box has been in use after its initial creation
        /// (i.e. the drop-down portion has been made visible at least once).</para>
        /// 
        /// <para>This method will raise the <see cref="BaseListControl.SubControlsRefreshed"/> event when
        /// called.  You can add an event handler to it to reload your preferred column definitions whenever the
        /// drop-down is refreshed and the column collection has been cleared.</para></remarks>
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

            if(this.DropDownFormat.GridColumnStyles.Count == 0)
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
        /// <remarks>If the drop-down is visible, it is scrolled instead</remarks>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if(this.DroppedDown && this.DropDownStyle != ComboBoxStyle.Simple)
                this.DropDownInterface.ScrollDropDown((e.Delta > 0) ? 0 - this.MaxDropDownItems : this.MaxDropDownItems);
            else
                if(e.Delta > 0)
                    base.HandleKeys(Keys.Up);
                else
                    base.HandleKeys(Keys.Down);

            HandledMouseEventArgs mea = e as HandledMouseEventArgs;

            if(mea != null)
                mea.Handled = true;

            base.OnMouseWheel(e);
        }
        #endregion
    }
}
