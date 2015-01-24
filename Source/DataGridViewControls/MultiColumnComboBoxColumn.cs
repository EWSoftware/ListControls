//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : MultiColumnComboBoxColumn.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 07/24/2014
// Note    : Copyright 2007-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a data grid view column object that hosts multi-column combo box cells
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/21/2007  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Windows.Forms;

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This data grid view column contains <see cref="MultiColumnComboBoxCell"/> objects
    /// </summary>
    public class MultiColumnComboBoxColumn : BaseComboBoxColumn
    {
        #region Private data members
        //=====================================================================

        private StringCollection columnFilter;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// Set or get whether column headers are visible
        /// </summary>
        /// <value>They are not visible by default</value>
        [Category("Appearance"), DefaultValue(false), Description("Indicates whether or not column headers are visible")]
        public bool ColumnHeadersVisible { get; set; }

        /// <summary>
        /// Set or get whether row headers are visible
        /// </summary>
        /// <value>They are not visible by default</value>
        [Category("Appearance"), DefaultValue(false), Description("Indicates whether or not row headers are visible")]
        public bool RowHeadersVisible { get; set; }

        /// <summary>
        /// Set or get the default null text for all columns
        /// </summary>
        /// <value>The default is an empty string.  The value is applied at runtime and its effect is not visible
        /// on columns during design time.  It replaces the null text for any column with its <c>NullText</c>
        /// property set to "(null)".</value>
        [Category("Appearance"), DefaultValue(""), Localizable(true),
          Description("The default text to display for columns with null values")]
        public string DefaultNullText { get; set; }

        /// <summary>
        /// Gets or sets the width of the of the drop-down portion of the combo box
        /// </summary>
        /// <value>If set to zero, it will default to an appropriate width based on the formatting options</value>
        [Category("Appearance"), DefaultValue(0), Description("The width of the drop-down lists of the combo " +
            "boxes.  Leave set to zero to auto-size the drop-down width")]
        public int DropDownWidth
        {
            get
            {
                if(base.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return ((MultiColumnComboBoxCell)base.ComboBoxCellTemplate).DropDownWidth;
            }
            set
            {
                DataGridViewRowCollection rows;
                MultiColumnComboBoxCell cell;
                int count, idx;

                if(base.ComboBoxCellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                if(this.DropDownWidth != value)
                {
                    ((MultiColumnComboBoxCell)base.ComboBoxCellTemplate).DropDownWidth = value;

                    if(base.DataGridView != null)
                    {
                        rows = base.DataGridView.Rows;
                        count = rows.Count;

                        for(idx = 0; idx < count; idx++)
                        {
                            cell = rows.SharedRow(idx).Cells[base.Index] as MultiColumnComboBoxCell;

                            if(cell != null)
                                cell.DropDownWidth = value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This gets the <see cref="StringCollection"/> used to filter the columns displayed by the drop-down
        /// portion of the combo box.
        /// </summary>
        /// <value>This is a quick way to filter the drop-down to a specific set of columns without defining
        /// column definitions using the <see cref="MultiColumnComboBox.DropDownFormat">DropDownFormat.GridColumnStyles</see>
        /// property collection.  If empty, no filtering takes place.</value>
        /// <example>
        /// <code language="cs">
        /// cbocVendor.DisplayMember = "VendorName";
        /// cbocVendor.ValueMember = "VendorKey";
        /// cbocVendor.ColumnFilter.AddRange(new string[] { "VendorName", "Contact" });
        /// cbocVendor.DataSource = GetVendors();
        /// </code>
        /// <code language="vbnet">
        /// cbocVendor.DisplayMember = "VendorName"
        /// cbocVendor.ValueMember = "VendorKey"
        /// cbocVendor.ColumnFilter.AddRange(New String() { "VendorName", "Contact" })
        /// cbocVendor.DataSource = GetVendors()
        /// </code>
        /// </example>
        [Category("Appearance"), Description("This defines a column name filter for the drop-down"),
          Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public StringCollection ColumnFilter
        {
            get
            {
                if(columnFilter == null)
                    columnFilter = new StringCollection();

                return columnFilter;
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public MultiColumnComboBoxColumn() : base(new MultiColumnComboBoxCell())
        {
            base.ComboBoxCellTemplate.TemplateComboBoxColumn = this;
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
            MultiColumnComboBoxColumn clone = (MultiColumnComboBoxColumn)base.Clone();
            clone.ColumnHeadersVisible = this.ColumnHeadersVisible;
            clone.RowHeadersVisible = this.RowHeadersVisible;
            clone.DefaultNullText = this.DefaultNullText;

            if(this.ColumnFilter.Count != 0)
                clone.ColumnFilter.AddRange(this.ColumnFilter);

            return clone;
        }

        /// <summary>
        /// Convert the column to its string description
        /// </summary>
        /// <returns>A string description of the column</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "MultiColumnComboBoxColumn {{ Name={0}, Index={1} }}",
                base.Name, base.Index);
        }
        #endregion
    }
}
