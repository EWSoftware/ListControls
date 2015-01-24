//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : UserControlComboBoxCell.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 07/24/2014
// Note    : Copyright 2007-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a data grid view cell object that hosts a user control combo box
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 06/08/2007  EFW  Created the code
//===============================================================================================================

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This data grid view cell type displays a <see cref="UserControlComboBox"/>
    /// </summary>
    public class UserControlComboBoxCell : BaseComboBoxCell
    {
        #region Private data members
        //=====================================================================

        // Static type values and flags
        private static Type defaultEditType = typeof(UserControlComboBoxEditingControl);

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// Gets the type of the cell's hosted editing control
        /// </summary>
        /// <value>This always returns <see cref="UserControlComboBoxEditingControl"/></value>
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
            return (UserControlComboBoxCell)base.Clone();
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
            UserControlComboBoxColumn owner = (UserControlComboBoxColumn)base.OwningColumn;
            UserControlComboBoxEditingControl box;
            string text;

            if(dataGridViewCellStyle == null)
                throw new ArgumentNullException("dataGridViewCellStyle");

            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            box = base.DataGridView.EditingControl as UserControlComboBoxEditingControl;

            if(box != null)
            {
                if((base.GetInheritedState(rowIndex) & DataGridViewElementStates.Selected) ==
                  DataGridViewElementStates.Selected)
                    base.DataGridView.EditingPanel.BackColor = dataGridViewCellStyle.SelectionBackColor;

                box.DropDownStyle = ComboBoxStyle.DropDownList;
                box.MaxDropDownItems = base.MaxDropDownItems;
                box.DataSource = null;
                box.DisplayMember = box.ValueMember = null;
                box.Items.Clear();

                // The default selection values and drop down control type are stored at the column level
                box.DropDownControl = owner.DropDownControl;
                box.EnforceDefaultSelection = owner.EnforceDefaultSelection;
                box.DefaultSelection = owner.DefaultSelection;

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
            return String.Format(CultureInfo.CurrentCulture, "UserControlComboBoxCell {{ ColumnIndex={0}, " +
                "RowIndex={1} }}", base.ColumnIndex, base.RowIndex);
        }
        #endregion
    }
}
