//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : MultiColumnComboBoxCell.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 07/24/2014
// Note    : Copyright 2007-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a data grid view cell object that hosts a multi-column combo box
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
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This data grid view cell type displays a <see cref="MultiColumnComboBox"/>
    /// </summary>
    public class MultiColumnComboBoxCell : BaseComboBoxCell
    {
        #region Private data members
        //=====================================================================

        // Static type values and flags
        private static Type defaultEditType = typeof(MultiColumnComboBoxEditingControl);

        // Combo box properties
        private int dropDownWidth;
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// Gets or sets the width of the of the drop-down portion of the combo box
        /// </summary>
        /// <value>If set to zero, it will default to an appropriate width based on the
        /// <see cref="MultiColumnComboBox.DropDownFormat"/> options.</value>
        /// <exception cref="ArgumentException">This is thrown if the width is less than zero.</exception>
        public int DropDownWidth
        {
            get { return dropDownWidth; }
            set
            {
                if(value < 0)
                    throw new ArgumentException(LR.GetString("ExInvalidDropDownWidth"));

                dropDownWidth = value;

                if(base.OwnsEditingComboBox(base.RowIndex))
                    ((MultiColumnComboBox)base.EditingComboBox).DropDownWidth = value;
            }
        }

        /// <summary>
        /// Gets the type of the cell's hosted editing control
        /// </summary>
        /// <value>This always returns <see cref="MultiColumnComboBoxEditingControl"/></value>
        public override Type EditType
        {
            get { return defaultEditType; }
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Clone the cell
        /// </summary>
        /// <returns>A clone of the cell</returns>
        public override object Clone()
        {
            MultiColumnComboBoxCell clone = (MultiColumnComboBoxCell)base.Clone();
            clone.DropDownWidth = dropDownWidth;

            return clone;
        }

        /// <summary>
        /// This initializes the control used to edit the cell
        /// </summary>
        /// <param name="rowIndex">The zero-based row index of the cell's location.</param>
        /// <param name="initialFormattedValue">An object that represents the value displayed by the cell when
        /// editing is started.</param>
        /// <param name="dataGridViewCellStyle">The cell style.</param>
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue,
          DataGridViewCellStyle dataGridViewCellStyle)
        {
            MultiColumnComboBoxColumn owner = (MultiColumnComboBoxColumn)base.OwningColumn;
            MultiColumnComboBoxEditingControl box;
            string text;

            if(dataGridViewCellStyle == null)
                throw new ArgumentNullException("dataGridViewCellStyle");

            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            box = base.DataGridView.EditingControl as MultiColumnComboBoxEditingControl;

            if(box != null)
            {
                if((base.GetInheritedState(rowIndex) & DataGridViewElementStates.Selected) ==
                  DataGridViewElementStates.Selected)
                    base.DataGridView.EditingPanel.BackColor = dataGridViewCellStyle.SelectionBackColor;

                box.DropDownStyle = ComboBoxStyle.DropDownList;
                box.MaxDropDownItems = base.MaxDropDownItems;
                box.DropDownWidth = dropDownWidth;
                box.DataSource = null;
                box.DisplayMember = box.ValueMember = null;
                box.ColumnFilter.Clear();
                box.Items.Clear();

                // The default selection, format, and column filter values are stored at the column level
                box.EnforceDefaultSelection = owner.EnforceDefaultSelection;
                box.DefaultSelection = owner.DefaultSelection;
                box.DropDownFormat.ColumnHeadersVisible = owner.ColumnHeadersVisible;
                box.DropDownFormat.RowHeadersVisible = owner.RowHeadersVisible;
                box.DropDownFormat.DefaultNullText = owner.DefaultNullText;

                StringCollection columnFilter = owner.ColumnFilter;

                if(columnFilter.Count != 0)
                    box.ColumnFilter.AddRange(columnFilter);

                box.DisplayMember = base.DisplayMember;
                box.ValueMember = base.ValueMember;
                box.DataSource = base.DataSource;

                if(base.HasItemCollection && base.DataSource == null && base.Items.Count > 0)
                    box.Items.AddRange(base.Items.InnerList.ToArray());

                box.SortOrder = base.SortOrder;
                box.FlatStyle = base.FlatStyle;

                text = initialFormattedValue as string;

                if(text == null)
                    text = String.Empty;

                box.Text = text;

                if(box.SelectedIndex == -1 && box.Items.Count != 0)
                    box.SelectedIndex = 0;

                base.EditingComboBox = box;

                // If the height is greater than the standard control, erase the area below it
                if(base.OwningRow.Height > 21)
                {
                    Rectangle rc = base.DataGridView.GetCellDisplayRectangle(base.ColumnIndex, rowIndex, true);
                    rc.Y += 21;
                    rc.Height -= 21;
                    base.DataGridView.Invalidate(rc);
                }
            }
        }

        /// <summary>
        /// Convert the cell to its string form
        /// </summary>
        /// <returns>A description of the cell</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "MultiColumnComboBoxCell {{ ColumnIndex={0}, " +
                "RowIndex={1} }}", base.ColumnIndex, base.RowIndex);
        }
        #endregion
    }
}
