//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : BaseComboBoxColumn.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 07/22/2014
// Note    : Copyright 2007-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a data grid view column object that acts as an abstract base class for the combo box
// columns derived from it.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 06/07/2007  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This data grid view column acts as an abstract base class for the combo
    /// box columns derived from it.
    /// </summary>
    public abstract class BaseComboBoxColumn : DataGridViewColumn
    {
        #region Private data members
        //=====================================================================

        private int defaultSelection;
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used internally to get a reference to the cell template as the actual type
        /// </summary>
        protected BaseComboBoxCell ComboBoxCellTemplate
        {
            get { return (BaseComboBoxCell)base.CellTemplate; }
        }

        /// <summary>
        /// Gets or sets the template used to create new cells
        /// </summary>
        /// <exception cref="InvalidCastException">This is thrown if the specified cell template is not a
        /// <see cref="BaseComboBoxCell"/>.</exception>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set
            {
                BaseComboBoxCell cell = value as BaseComboBoxCell;

                if(value != null && cell == null)
                    throw new InvalidCastException(LR.GetString("ExWrongCellTemplateType", "BaseComboBoxCell"));

                base.CellTemplate = value;

                if(value != null)
                    cell.TemplateComboBoxColumn = this;
            }
        }

        /// <summary>
        /// Gets or sets a value that determines how the combo box is displayed when not editing
        /// </summary>
        /// <value>The default is to show a drop-down button.</value>
        [Category("Appearance"), DefaultValue(DataGridViewComboBoxDisplayStyle.DropDownButton),
          Description("Determines how the combo box is displayed when not editing")]
        public DataGridViewComboBoxDisplayStyle DisplayStyle
        {
            get
            {
                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return this.ComboBoxCellTemplate.DisplayStyle;
            }
            set
            {
                DataGridViewRowCollection rows;
                BaseComboBoxCell cell;
                int count, idx;

                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                this.ComboBoxCellTemplate.DisplayStyle = value;

                if(base.DataGridView != null)
                {
                    rows = base.DataGridView.Rows;
                    count = rows.Count;

                    for(idx = 0; idx < count; idx++)
                    {
                        cell = rows.SharedRow(idx).Cells[base.Index] as BaseComboBoxCell;

                        if(cell != null)
                            cell.DisplayStyleInternal = value;
                    }

                    base.DataGridView.InvalidateColumn(base.Index);
                }
            }
        }

        /// <summary>
        /// Get or set whether or not the display style applies only to the current cell
        /// </summary>
        /// <value>The default is false and the style applies to all cells</value>
        [Category("Appearance"), DefaultValue(false),
          Description("Indicates if the display style only applies to the current cell")]
        public bool DisplayStyleForCurrentCellOnly
        {
            get
            {
                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return this.ComboBoxCellTemplate.DisplayStyleForCurrentCellOnly;
            }
            set
            {
                DataGridViewRowCollection rows;
                BaseComboBoxCell cell;
                int count, idx;

                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                this.ComboBoxCellTemplate.DisplayStyleForCurrentCellOnly = value;

                if(base.DataGridView != null)
                {
                    rows = base.DataGridView.Rows;
                    count = rows.Count;

                    for(idx = 0; idx < count; idx++)
                    {
                        cell = rows.SharedRow(idx).Cells[base.Index] as BaseComboBoxCell;

                        if(cell != null)
                            cell.DisplayStyleForCurrentCellOnlyInternal = value;
                    }

                    base.DataGridView.InvalidateColumn(base.Index);
                }
            }
        }

        /// <summary>
        /// Get or set the flat style appearance of the cells
        /// </summary>
        [Category("Appearance"), DefaultValue(FlatStyle.Standard),
          Description("The flat style appearance of the column's cells")]
        public FlatStyle FlatStyle
        {
            get
            {
                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return this.ComboBoxCellTemplate.FlatStyle;
            }
            set
            {
                DataGridViewRowCollection rows;
                BaseComboBoxCell cell;
                int count, idx;

                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                if(this.FlatStyle != value)
                {
                    this.ComboBoxCellTemplate.FlatStyle = value;

                    if(base.DataGridView != null)
                    {
                        rows = base.DataGridView.Rows;
                        count = rows.Count;

                        for(idx = 0; idx < count; idx++)
                        {
                            cell = rows.SharedRow(idx).Cells[base.Index] as BaseComboBoxCell;

                            if(cell != null)
                                cell.FlatStyleInternal = value;
                        }

                        // Notify the grid view of the change so that it can resize the column and its rows if
                        // necessary.
                        DataGridViewHelper.OnColumnCommonChange(base.DataGridView, base.Index);
                    }
                }
            }
        }

        /// <summary>
        /// Get the maximum number of items to display in the drop-down by default
        /// </summary>
        [Category("Behavior"), DefaultValue(8),
          Description("The maximum number of items in the drop-down list by default")]
        public int MaxDropDownItems
        {
            get
            {
                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return this.ComboBoxCellTemplate.MaxDropDownItems;
            }
            set
            {
                DataGridViewRowCollection rows;
                BaseComboBoxCell cell;
                int count, idx;

                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                if(this.MaxDropDownItems != value)
                {
                    this.ComboBoxCellTemplate.MaxDropDownItems = value;

                    if(base.DataGridView != null)
                    {
                        rows = base.DataGridView.Rows;
                        count = rows.Count;

                        for(idx = 0; idx < count; idx++)
                        {
                            cell = rows.SharedRow(idx).Cells[base.Index] as BaseComboBoxCell;

                            if(cell != null)
                                cell.MaxDropDownItems = value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the items in the list control are sorted and, if so, in what
        /// order.
        /// </summary>
        /// <value>If set to <c>None</c> (the default), the items in the list control are not sorted.  If set to
        /// <c>Ascending</c> or <c>Descending</c> the list control sorts existing entries and adds new entries to
        /// the appropriate sorted position in the list. You can use this property to automatically sort items in
        /// a list control.  As items are added to a sorted list control, the items are moved to the appropriate
        /// location in the sorted list. When you set the property to <c>None</c>, new items are added to the end
        /// of the existing list. The sort is case-insensitive.</value>
        /// <exception cref="ArgumentException">This is thrown if an attempt is made to set a sort order other
        /// than <c>None</c> when a data source is in use.</exception>
        [Category("Behavior"), DefaultValue(ListSortOrder.None),
          Description("This defines the sort order for the list control items")]
        public ListSortOrder SortOrder
        {
            get
            {
                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return this.ComboBoxCellTemplate.SortOrder;
            }
            set
            {
                DataGridViewRowCollection rows;
                BaseComboBoxCell cell;
                int count, idx;

                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                if(this.SortOrder != value)
                {
                    this.ComboBoxCellTemplate.SortOrder = value;

                    if(base.DataGridView != null)
                    {
                        rows = base.DataGridView.Rows;
                        count = rows.Count;

                        for(idx = 0; idx < count; idx++)
                        {
                            cell = rows.SharedRow(idx).Cells[base.Index] as BaseComboBoxCell;

                            if(cell != null)
                                cell.SortOrder = value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a string that specifies the property or column from which to retrieve strings for
        /// display in the combo boxes.
        /// </summary>
        [Category("Data"), DefaultValue(""),
          Description("A string that specifies the property or column from which to retrieve strings for " +
              "display in the combo boxes"),
          Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)),
          TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string DisplayMember
        {
            get
            {
                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return this.ComboBoxCellTemplate.DisplayMember;
            }
            set
            {
                DataGridViewRowCollection rows;
                BaseComboBoxCell cell;
                int count, idx;

                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                this.ComboBoxCellTemplate.DisplayMember = value;

                if(base.DataGridView != null)
                {
                    rows = base.DataGridView.Rows;
                    count = rows.Count;

                    for(idx = 0; idx < count; idx++)
                    {
                        cell = rows.SharedRow(idx).Cells[base.Index] as BaseComboBoxCell;

                        if(cell != null)
                            cell.DisplayMember = value;
                    }

                    // Notify the grid view of the change so that it can resize the column and its rows if
                    // necessary.
                    DataGridViewHelper.OnColumnCommonChange(base.DataGridView, base.Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets a string that specifies the property or column from which to get values that correspond
        /// to the selections in the drop-down list.
        /// </summary>
        [Category("Data"), DefaultValue(""),
          Description("A string that specifies the property or column from which to get values that " +
              "correspond to the selections in the drop-down list"),
          TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
          Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string ValueMember
        {
            get
            {
                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return this.ComboBoxCellTemplate.ValueMember;
            }
            set
            {
                DataGridViewRowCollection rows;
                BaseComboBoxCell cell;
                int count, idx;

                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                this.ComboBoxCellTemplate.ValueMember = value;

                if(base.DataGridView != null)
                {
                    rows = base.DataGridView.Rows;
                    count = rows.Count;

                    for(idx = 0; idx < count; idx++)
                    {
                        cell = rows.SharedRow(idx).Cells[base.Index] as BaseComboBoxCell;

                        if(cell != null)
                            cell.ValueMember = value;
                    }

                    // Notify the grid view of the change so that it can resize
                    // the column and its rows if necessary.
                    DataGridViewHelper.OnColumnCommonChange(base.DataGridView, base.Index);
                }
            }
        }

        /// <summary>
        /// Gets or sets the data source that populates the selections for the combo boxes
        /// </summary>
        [Category("Data"), DefaultValue(null), RefreshProperties(RefreshProperties.Repaint),
          AttributeProvider(typeof(IListSource)), Description("The data source that populates the selections " +
            "for the combo boxes")]
        public object DataSource
        {
            get
            {
                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return this.ComboBoxCellTemplate.DataSource;
            }
            set
            {
                DataGridViewRowCollection rows;
                BaseComboBoxCell cell;
                int count, idx;

                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                this.ComboBoxCellTemplate.DataSource = value;

                if(base.DataGridView != null)
                {
                    rows = base.DataGridView.Rows;
                    count = rows.Count;

                    for(idx = 0; idx < count; idx++)
                    {
                        cell = rows.SharedRow(idx).Cells[base.Index] as BaseComboBoxCell;

                        if(cell != null)
                            cell.DataSource = value;
                    }

                    // Notify the grid view of the change so that it can resize the column and its rows if
                    // necessary.
                    DataGridViewHelper.OnColumnCommonChange(base.DataGridView, base.Index);
                }
            }
        }

        /// <summary>
        /// This is used to get or set whether or not the combo box should enforce a default selection
        /// </summary>
        /// <value>In <c>DropDownList</c> mode, if this property is true, a <see cref="BaseListControl.SelectedIndex"/>
        /// of -1 (no selection) is not allowed.  Instead, the index specified by the <see cref="DefaultSelection"/>
        /// property is used instead. For <c>DropDown</c> mode, this property is ignored as values can be entered
        /// that are not in the list of valid items.  The default is false to mimic the behavior of the normal
        /// <see cref="DataGridViewComboBoxColumn"/>.</value>
		[Category("Behavior"), DefaultValue(false),
         Description("Specify whether or not to enforce a default selection")]
        public bool EnforceDefaultSelection { get; set; }

        /// <summary>
        /// This property is used to set or get the default selection's index
        /// </summary>
        /// <value>If <see cref="EnforceDefaultSelection"/> is true, a <see cref="BaseListControl.SelectedIndex"/>
        /// of -1 (no selection) is not allowed.  Instead, the index specified by this property is used.</value>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index is less than zero</exception>
		[Category("Behavior"), DefaultValue(0), Description("The index to use as a default selection")]
        public int DefaultSelection
        {
            get { return defaultSelection; }
            set
            {
                if(value < 0)
                    throw new ArgumentOutOfRangeException("value", value, LR.GetString("ExNegativeDefSel"));

                defaultSelection = value;
            }
        }

        /// <summary>
        /// Gets the collection of objects used as selections in the combo boxes
        /// </summary>
        [Category("Data"), Description("The collection of objects used as selection in the combo boxes"),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
          Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public BaseComboBoxCell.ObjectCollection Items
        {
            get
            {
                if(this.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return this.ComboBoxCellTemplate.GetItems(base.DataGridView);
            }
        }
        #endregion

        #region Private methods
        //=====================================================================

        /// <summary>
        /// This is used to refresh the item collection in each cell when the template column's collection
        /// changes.
        /// </summary>
        internal void OnItemsCollectionChanged()
        {
            DataGridViewRowCollection rows;
            BaseComboBoxCell cell;
            int count, idx;
            object[] items;

            if(base.DataGridView != null)
            {
                rows = base.DataGridView.Rows;
                count = rows.Count;
                items = this.ComboBoxCellTemplate.Items.InnerList.ToArray();

                for(idx = 0; idx < count; idx++)
                {
                    cell = rows.SharedRow(idx).Cells[base.Index] as BaseComboBoxCell;

                    if(cell != null)
                    {
                        cell.Items.ClearInternal();
                        cell.Items.AddRangeInternal(items);
                    }
                }

                // Notify the grid view of the change so that it can resize the column and its rows if necessary
                DataGridViewHelper.OnColumnCommonChange(base.DataGridView, base.Index);
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cellTemplate">The cell template to use</param>
        protected BaseComboBoxColumn(BaseComboBoxCell cellTemplate) : base(cellTemplate)
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is used to clone the column.
        /// </summary>
        /// <returns>A clone of the column</returns>
        public override object Clone()
        {
            BaseComboBoxColumn clone = (BaseComboBoxColumn)base.Clone();
            clone.ComboBoxCellTemplate.TemplateComboBoxColumn = clone;
            clone.EnforceDefaultSelection = this.EnforceDefaultSelection;
            clone.DefaultSelection = defaultSelection;

            return clone;
        }
        #endregion
    }
}
