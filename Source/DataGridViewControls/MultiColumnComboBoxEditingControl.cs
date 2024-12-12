//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : MultiColumnComboBoxEditingControl.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/09/2024
// Note    : Copyright 2007-2024, Eric Woodruff, All rights reserved
//
// This file contains a multi-column combo box control that is hosted within a data grid view cell
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

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This derived version of the multi-column combo box is hosted within the <see cref="MultiColumnComboBoxCell"/>
    /// </summary>
    [ToolboxItem(false)]
    public class MultiColumnComboBoxEditingControl : MultiColumnComboBox, IDataGridViewEditingControl
    {
        #region Private data members
        //=====================================================================

        private bool firstKeySeen;

        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        public MultiColumnComboBoxEditingControl()
        {
            this.TabStop = false;   // It cannot be a tab stop
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to notify the data grid view of value changes on the first key press
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <remarks>If the first key auto-completes the default entry, it won't see the change so we must force
        /// it.</remarks>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if(!firstKeySeen && e?.KeyChar != 27)
            {
                this.OnSelectedIndexChanged(e!);

                firstKeySeen = true;

                // The first key press isn't forwarded in DropDown mode so force the text to the selected
                // character which gets it going.
                if(this.DropDownStyle == ComboBoxStyle.DropDown)
                {
                    this.Text = e!.KeyChar.ToString();
                    this.Select(1, 0);
                }
            }
        }

        /// <summary>
        /// This is overridden to notify the data grid view of value changes
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);

            if(this.SelectedIndex != -1)
            {
                this.EditingControlValueChanged = true;
                this.EditingControlDataGridView!.NotifyCurrentCellDirty(true);
            }
        }
        #endregion

        #region IDataGridViewEditingControl Members
        //=====================================================================

        /// <summary>
        /// This applies the cell style to the editing control so that they are consistent
        /// </summary>
        /// <param name="dataGridViewCellStyle">The cell style to apply</param>
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            if(dataGridViewCellStyle == null)
                throw new ArgumentNullException(nameof(dataGridViewCellStyle));

            this.Font = dataGridViewCellStyle.Font;

            if(dataGridViewCellStyle.BackColor.A < 0xFF)
            {
                Color color = Color.FromArgb(0xFF, dataGridViewCellStyle.BackColor);
                this.BackColor = color;
                this.EditingControlDataGridView!.EditingPanel.BackColor = color;
            }
            else
                this.BackColor = dataGridViewCellStyle.BackColor;

            this.ForeColor = dataGridViewCellStyle.ForeColor;

            firstKeySeen = false;
        }

        /// <summary>
        /// Gets or sets the data grid view that contains the owning cell.
        /// </summary>
        public DataGridView? EditingControlDataGridView { get; set; }

        /// <summary>
        /// Gets or sets the formatted value of the cell being modified by the editor
        /// </summary>
        public object EditingControlFormattedValue
        {
            get => this.GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting);
            set
            {
                if(value is string formattedText)
                {
                    this.Text = formattedText;

                    if(!String.Equals(formattedText, this.Text, StringComparison.OrdinalIgnoreCase))
                        this.SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// Gets or sets the index of the hosting cell's parent row
        /// </summary>
        public int EditingControlRowIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the value of the editing control differs from the value of
        /// the hosting cell.
        /// </summary>
        public bool EditingControlValueChanged { get; set; }

        /// <summary>
        /// This determines whether the specified key is a regular input key that the editing control should
        /// process or a special key that the <see cref="DataGridView"/> should process.
        /// </summary>
        /// <param name="keyData">The key that was pressed.</param>
        /// <param name="dataGridViewWantsInputKey">True when the <see cref="DataGridView"/> wants to process the
        /// key; otherwise, false.</param>
        /// <returns>True if the specified key is a regular input key that should be handled by the editing
        /// control; otherwise, false.</returns>
        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            if((keyData & Keys.KeyCode) != Keys.Down && (keyData & Keys.KeyCode) != Keys.Up &&
              (!this.DroppedDown || (keyData & Keys.KeyCode) != Keys.Escape) &&
              (keyData & Keys.KeyCode) != Keys.Return)
            {
                return !dataGridViewWantsInputKey;
            }

            return true;
        }

        /// <summary>
        /// Gets the cursor used when the mouse pointer is over the <see cref="DataGridView.EditingPanel"/> but
        /// not over the editing control.
        /// </summary>
        /// <value>This always returns the default cursor</value>
        public Cursor EditingPanelCursor => Cursors.Default;

        /// <summary>
        /// Retrieves the formatted value of the cell
        /// </summary>
        /// <param name="context">A bitwise combination of <see cref="DataGridViewDataErrorContexts" /> values
        /// that specifies the context in which the data is needed.</param>
        /// <returns>An object that represents the formatted version of the cell contents</returns>
        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.Text;
        }

        /// <summary>
        /// Prepares the currently selected cell for editing
        /// </summary>
        /// <param name="selectAll">True to select all text in the cell, false to not select all text</param>
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            if(selectAll)
                this.SelectAll();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the cell contents need to be repositioned whenever the value
        /// changes.
        /// </summary>
        /// <remarks>This always returns false</remarks>
        public bool RepositionEditingControlOnValueChange => false;

        #endregion
    }
}
