//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : AutoCompleteComboBox.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
//
// This file contains a standard combo box control that supplies an auto-complete feature that selects the best
// match as the user types text into it.  Auto-completion works for all combo box styles.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/14/2005  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace EWSoftware.ListControls
{
	/// <summary>
    /// This is a standard combo box control that supplies an auto-complete feature that selects the best match
    /// as the user types text into it.  Auto-completion works for all combo box styles.
	/// </summary>
    [Description("A basic combo box with auto-completion support")]
	public class AutoCompleteComboBox : ComboBox
	{
        #region Private data members
        //=====================================================================

        private string autoCompleteText;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This can be used to get the value of the specified column in the currently selected item
        /// </summary>
        /// <param name="columnName">The column name of the item to get.  This can be any column in the data
        /// source, not just those displayed by the control.</param>
        /// <value>Returns the entry at the specified column in the currently selected item.  If
        /// <see cref="ComboBox.SelectedIndex"/> equals -1 (no selection) or the column cannot be found, this
        /// returns null.</value>
        /// <overloads>There are two overloads for this property</overloads>
        [Browsable(false), Description("Get the specified column from the current row")]
        public object this[string columnName] => this[this.SelectedIndex, columnName];

        /// <summary>
        /// This can be used to get the value of the specified column in the specified row of the list control's
        /// data source.
        /// </summary>
        /// <param name="rowIndex">The row index of the item.</param>
        /// <param name="columnName">The column name of the item to get.  This can be any column in the data
        /// source, not just those displayed by the control.</param>
        /// <value>Returns the entry at the specified column in the specified row.  If the row is out of bounds
        /// or if the column cannot be found, this will return null.</value>
        [Browsable(false), Description("Get the specified column from the specified row")]
        public object this[int rowIndex, string columnName]
        {
            get
            {
                if(rowIndex < 0 || rowIndex >= this.Items.Count || columnName == null || columnName.Length == 0)
                    return null;

                object item = this.Items[rowIndex];

                if(item != null)
                {
                    PropertyDescriptor pd;

                    if(this.DataManager != null)
                        pd = this.DataManager.GetItemProperties().Find(columnName, true);
                    else
                        pd = TypeDescriptor.GetProperties(item).Find(columnName, true);

                    if(pd != null)
                        item = pd.GetValue(item);
                    else
                        item = null;
                }

                return item;
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public AutoCompleteComboBox()
		{
            autoCompleteText = String.Empty;
		}
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is overridden to clear any existing auto-completion tracking text when the style changes
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnDropDownStyleChanged(EventArgs e)
        {
            autoCompleteText = String.Empty;

            base.OnDropDownStyleChanged(e);
        }

        /// <summary>
        /// This is overridden to handle certain keys differently for the auto-completion combo box
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            bool clearAutoText = false;

            if(e == null)
                throw new ArgumentNullException(nameof(e));

            switch(e.KeyData)
            {
                case Keys.Enter:
                case Keys.Escape:
                case Keys.Down:
                case Keys.Up:
                case Keys.PageDown:
                case Keys.PageUp:
                case Keys.F4:
                case (Keys.Alt | Keys.Down):
                case (Keys.Alt | Keys.Up):
                    clearAutoText = true;
                    break;

                case Keys.Back:
                    // Strip a character off the auto-complete text?
                    if(this.DropDownStyle == ComboBoxStyle.DropDownList && autoCompleteText.Length > 0)
                    {
                        autoCompleteText = autoCompleteText.Substring(0, autoCompleteText.Length - 1);

                        if(autoCompleteText.Length > 0)
                            this.AutoComplete();

                        e.Handled = true;
                    }
                    break;

                case Keys.Left:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                    if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                        clearAutoText = true;
                    break;

                default:
                    break;
            }

            if(clearAutoText)
                autoCompleteText = String.Empty;

            if(!e.Handled)
                base.OnKeyDown(e);
        }

        /// <summary>
        /// This is overridden to allow auto-completion to occur
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            // Auto-complete if it's an appropriate key
            if(e != null && ((Char.IsLetterOrDigit((char)((ushort)e.KeyCode)) &&
              (e.KeyCode < Keys.F1 || e.KeyCode > Keys.F24)) ||
              (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.Divide) ||
              (e.KeyCode >= Keys.OemSemicolon && e.KeyCode <= Keys.OemBackslash) ||
              (e.KeyCode == Keys.Space && !e.Shift)) && !e.Shift && !e.Control && !e.Alt)
            {
                this.AutoComplete();
            }
        }

        /// <summary>
        /// This is overridden to add characters to the auto-complete tracking string in <c>DropDownList</c> mode
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if(e == null)
                throw new ArgumentNullException(nameof(e));

            if(!Char.IsControl(e.KeyChar) && this.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                autoCompleteText += e.KeyChar;
                e.Handled = true;
            }

            if(!e.Handled)
                base.OnKeyPress(e);
        }

        /// <summary>
        /// This is overridden to clear the auto-complete text when the control gains the focus
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnEnter(EventArgs e)
        {
            autoCompleteText = String.Empty;

            base.OnEnter(e);
        }

        /// <summary>
        /// This is overridden to ensure that the selected item is chosen if the control is left while the
        /// drop-down is shown.
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <remarks>There is an odd problem that happens every so often where if the drop-down is displayed,
        /// you type letters to use auto-completion to select an entry, and then tab out, it doesn't save the
        /// selected item.  If this event is fired while the drop-down is shown, it saves the item selected,
        /// hides the drop-down, and restores the chosen item.</remarks>
        protected override void OnLeave(EventArgs e)
        {
            if(this.DroppedDown)
            {
                int selIdx = this.SelectedIndex;
                this.DroppedDown = false;
                this.SelectedIndex = selIdx;
            }

            base.OnLeave(e);
        }

        /// <summary>
        /// This handles auto-completion when the user types text
        /// </summary>
        private void AutoComplete()
        {
            string find;
            int idx;

            if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                find = autoCompleteText;
            else
                find = this.Text.Substring(0, this.SelectionStart);

            if(find.Length != 0)
            {
                idx = this.FindStringExact(find, -1);

                if(idx == -1)
                    idx = this.FindString(find, -1);

                if(idx != -1)
                {
                    this.SelectedIndex = idx;

                    // In DropDownList mode, partial text selections are not supported
                    if(this.DropDownStyle != ComboBoxStyle.DropDownList)
                    {
                        this.SelectionStart = find.Length;
                        this.SelectionLength = this.Text.Length - this.SelectionStart;
                    }
                }
                else  // Remove last typed char in DropDownList mode if not found
                    if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                        autoCompleteText = find.Substring(0, find.Length - 1);
            }
        }
        #endregion
    }
}
