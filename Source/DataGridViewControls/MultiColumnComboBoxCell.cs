//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : MultiColumnComboBoxCell.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/05/2023
// Note    : Copyright 2007-2023, Eric Woodruff, All rights reserved
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
        private static readonly Type defaultEditType = typeof(MultiColumnComboBoxEditingControl);

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
            get => dropDownWidth;
            set
            {
                if(value < 0)
                    throw new ArgumentException(LR.GetString("ExInvalidDropDownWidth"));

                dropDownWidth = value;

                if(this.OwnsEditingComboBox(this.RowIndex))
                    ((MultiColumnComboBox)this.EditingComboBox).DropDownWidth = value;
            }
        }

        /// <summary>
        /// Gets the type of the cell's hosted editing control
        /// </summary>
        /// <value>This always returns <see cref="MultiColumnComboBoxEditingControl"/></value>
        public override Type EditType => defaultEditType;

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
            MultiColumnComboBoxColumn owner = (MultiColumnComboBoxColumn)this.OwningColumn;
            MultiColumnComboBoxEditingControl box;
            string text;

            if(dataGridViewCellStyle == null)
                throw new ArgumentNullException(nameof(dataGridViewCellStyle));

            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            box = this.DataGridView.EditingControl as MultiColumnComboBoxEditingControl;

            if(box != null)
            {
                if((this.GetInheritedState(rowIndex) & DataGridViewElementStates.Selected) ==
                  DataGridViewElementStates.Selected)
                    this.DataGridView.EditingPanel.BackColor = dataGridViewCellStyle.SelectionBackColor;

                box.DropDownStyle = ComboBoxStyle.DropDownList;
                box.MaxDropDownItems = this.MaxDropDownItems;
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

                box.DisplayMember = this.DisplayMember;
                box.ValueMember = this.ValueMember;
                box.DataSource = this.DataSource;

                if(this.HasItemCollection && this.DataSource == null && this.Items.Count > 0)
                    box.Items.AddRange(this.Items.InnerList.ToArray());

                box.SortOrder = this.SortOrder;
                box.FlatStyle = this.FlatStyle;

                text = initialFormattedValue as string;

                box.Text = text ?? String.Empty;

                if(box.SelectedIndex == -1 && box.Items.Count != 0)
                    box.SelectedIndex = 0;

                this.EditingComboBox = box;

                // If the height is greater than the standard control, erase the area below it
                if(this.OwningRow.Height > 21)
                {
                    Rectangle rc = this.DataGridView.GetCellDisplayRectangle(this.ColumnIndex, rowIndex, true);
                    rc.Y += 21;
                    rc.Height -= 21;
                    this.DataGridView.Invalidate(rc);
                }
            }
        }

        /// <summary>
        /// Convert the cell to its string form
        /// </summary>
        /// <returns>A description of the cell</returns>
        public override string ToString()
        {
            return $"MultiColumnComboBoxCell {{ ColumnIndex={this.ColumnIndex}, RowIndex={this.RowIndex} }}";
        }
        #endregion
    }
}
