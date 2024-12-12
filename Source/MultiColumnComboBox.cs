//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : MultiColumnComboBox.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
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

using System.Drawing.Design;

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
        private StringCollection scColumnFilter = null!;

        #endregion

        #region Properties
        //====================================================================

        /// <summary>
        /// This is used to modify the column header default cell style
        /// </summary>
		[Category("DropDown"), Bindable(true), Description("The drop-down's column headers default cell style")]
        public DataGridViewCellStyle ColumnHeadersDefaultCellStyle { get; set; } = new DataGridViewCellStyle();


        /// <summary>
        /// This is used to modify the alternating rows default cell style
        /// </summary>
		[Category("DropDown"), Bindable(true), Description("The drop-down's alternating rows default cell style")]
        public DataGridViewCellStyle AlternatingRowsDefaultCellStyle { get; set; } = new DataGridViewCellStyle();

        /// <summary>
        /// This is used to modify the default cell style
        /// </summary>
		[Category("DropDown"), Bindable(true), Description("The drop-down's default cell style")]
        public DataGridViewCellStyle DefaultCellStyle { get; set; } = new DataGridViewCellStyle();

        /// <summary>
        /// This is used to get or set whether or not column headers are visible
        /// </summary>
        [Category("DropDown"), Bindable(true), DefaultValue(false), Description("Indicate whether or not column " +
            "headers are visible")]
        public bool ColumnHeadersVisible { get; set; } = false;

        /// <summary>
        /// This is used to get or set whether or not row headers are visible
        /// </summary>
        [Category("DropDown"), Bindable(true), DefaultValue(false), Description("Indicate whether or not row " +
            "headers are visible")]
        public bool RowHeadersVisible { get; set; } = false;

        /// <summary>
        /// This is used to get or set the width of row headers if they are visible
        /// </summary>
        [Category("DropDown"), Bindable(true), DefaultValue(20), Description("Specify the width of the row headers")]
        public int RowHeadersWidth { get; set; } = 20;

        /// <summary>
        /// Gets or sets the width of the of the drop-down portion of the combo box
        /// </summary>
        /// <value>If set to zero, it will default to an appropriate width based on the column definitions</value>
        /// <exception cref="ArgumentException">This is thrown if the width is less than zero</exception>
        [Category("DropDown"), Bindable(true), DefaultValue(0), Description("The default width of the drop-down " +
          "portion of the combo box")]
        public int DropDownWidth
        {
            get => dropDownWidth;
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
        /// <value>This is a quick way to filter the drop-down to a specific set of columns without having to
        /// specify column definitions.  If empty, no filtering takes place.</value>
        /// <example>
        /// <code language="cs">
        /// cboVendor.DisplayMember = "VendorName";
        /// cboVendor.ValueMember = "VendorKey";
        /// cboVendor.ColumnFilter.AddRange(new[] { "VendorName", "Contact" });
        /// cboVendor.DataSource = GetVendors();
        /// </code>
        /// <code language="vbnet">
        /// cboVendor.DisplayMember = "VendorName"
        /// cboVendor.ValueMember = "VendorKey"
        /// cboVendor.ColumnFilter.AddRange(New() { "VendorName", "Contact" })
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
                scColumnFilter ??= [];

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
        public event EventHandler? DropDownWidthChanged;

        /// <summary>
        /// This raises the <see cref="DropDownWidthChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnDropDownWidthChanged(EventArgs e)
        {
            DropDownWidthChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the a dropdown column needs to be for formatted
        /// </summary>
		[Category("DropDown"), Description("Occurs when a drop-down column needs to be formatted")]
        public event EventHandler<DataGridViewColumnEventArgs>? FormatDropDownColumn;

        /// <summary>
        /// This raises the <see cref="FormatDropDownColumn"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnFormatDropDownColumn(DataGridViewColumnEventArgs e)
        {
            FormatDropDownColumn?.Invoke(this, e);
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
        }
        #endregion

        #region Methods and event handlers
        //=====================================================================

        /// <summary>
        /// Determine whether or not to serialize the alternating rows default cell style
        /// </summary>
        internal bool ShouldSerializeAlternatingRowsDefaultCellStyle()
        {
            return !this.AlternatingRowsDefaultCellStyle.Equals(new DataGridViewCellStyle());
        }

        /// <summary>
        /// Determine whether or not to serialize the column headers default cell style
        /// </summary>
        internal bool ShouldSerializeColumnHeadersDefaultCellStyle()
        {
            return !this.ColumnHeadersDefaultCellStyle.Equals(new DataGridViewCellStyle());
        }

        /// <summary>
        /// Determine whether or not to serialize the default cell style
        /// </summary>
        internal bool ShouldSerializeDefaultCellStyle()
        {
            return !this.DefaultCellStyle.Equals(new DataGridViewCellStyle());
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
                ((MultiColumnDropDown)this.DropDownInterface!).SelectItem(this.SelectedIndex);
        }

        /// <summary>
        /// This can be called to force the drop-down portion of the combo box to refresh its format settings
        /// </summary>
        /// <remarks>This method will raise the <see cref="BaseListControl.SubControlsRefreshed"/> event when
        /// called.  You can add an event handler to it to reload your preferred column definitions whenever the
        /// drop-down is refreshed and the column collection has been cleared.</remarks>
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
            if(e == null)
                throw new ArgumentNullException(nameof(e));

            if(this.DroppedDown && this.DropDownStyle != ComboBoxStyle.Simple)
            {
                this.DropDownInterface!.ScrollDropDown(e.Delta / SystemInformation.MouseWheelScrollDelta *
                    SystemInformation.MouseWheelScrollLines * -1);
            }
            else
            {
                if(e.Delta > 0)
                    this.HandleKeys(Keys.Up);
                else
                    this.HandleKeys(Keys.Down);
            }

            if(e is HandledMouseEventArgs mea)
                mea.Handled = true;

            base.OnMouseWheel(e);
        }
        #endregion
    }
}
