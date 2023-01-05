//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : AutoCompleteTextBoxCell.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2023
// Note    : Copyright 2008-2023, Eric Woodruff, All rights reserved
//
// This file contains a data grid view cell object that hosts a textbox with the auto-complete properties
// exposed.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 02/07/2008  EFW  Created the code
//===============================================================================================================

using System;
using System.Windows.Forms;

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This file contains a data grid view cell object that hosts a textbox with the auto-complete properties
    /// exposed.
    /// </summary>
    public class AutoCompleteTextBoxCell : DataGridViewTextBoxCell
    {
        #region Private data members
        //=====================================================================

        private AutoCompleteMode mode;
        private AutoCompleteSource source;
        private AutoCompleteStringCollection customSource;

        private TextBox editingTextBox;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// Gets or sets a value that determines the auto-complete mode
        /// </summary>
        /// <value>The default is <c>None</c>.</value>
        public AutoCompleteMode AutoCompleteMode
        {
            get => mode;
            set
            {
                mode = value;

                if(this.OwnsEditingTextBox(base.RowIndex))
                    editingTextBox.AutoCompleteMode = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that determines the auto-complete source
        /// </summary>
        /// <value>The default is <c>None</c>.</value>
        public AutoCompleteSource AutoCompleteSource
        {
            get => source;
            set
            {
                source = value;

                if(this.OwnsEditingTextBox(base.RowIndex))
                    editingTextBox.AutoCompleteSource = value;
            }
        }

        /// <summary>
        /// Gets or sets a custom auto-completion source.
        /// </summary>
        public AutoCompleteStringCollection AutoCompleteCustomSource
        {
            get
            {
                if(customSource == null)
                    customSource = new AutoCompleteStringCollection();

                return customSource;
            }
            set
            {
                customSource = value;

                if(this.OwnsEditingTextBox(base.RowIndex))
                    editingTextBox.AutoCompleteCustomSource = value;
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public AutoCompleteTextBoxCell()
        {
            source = AutoCompleteSource.None;
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is used to determine whether or not the given row owns the editing textbox control
        /// </summary>
        /// <param name="rowIndex">The index of the row to check</param>
        /// <returns>True if it owns the control, false if it does not</returns>
        protected bool OwnsEditingTextBox(int rowIndex)
        {
            return rowIndex != -1 && editingTextBox is IDataGridViewEditingControl editControl &&
                rowIndex == editControl.EditingControlRowIndex;
        }

        /// <summary>
        /// This is used to clone the cell.
        /// </summary>
        /// <returns>A clone of the cell</returns>
        public override object Clone()
        {
            AutoCompleteTextBoxCell clone = (AutoCompleteTextBoxCell)base.Clone();
            clone.AutoCompleteMode = mode;
            clone.AutoCompleteSource = source;
            clone.AutoCompleteCustomSource = customSource;

            return clone;
        }

        /// <summary>
        /// This removes the cell's editing control from the data grid view.
        /// </summary>
        public override void DetachEditingControl()
        {
            editingTextBox = null;

            base.DetachEditingControl();
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
            if(dataGridViewCellStyle == null)
                throw new ArgumentNullException(nameof(dataGridViewCellStyle));

            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            editingTextBox = base.DataGridView.EditingControl as TextBox;

            if(editingTextBox != null)
            {
                editingTextBox.AutoCompleteMode = mode;
                editingTextBox.AutoCompleteSource = source;
                editingTextBox.AutoCompleteCustomSource = customSource;
            }
        }
        #endregion
    }
}
