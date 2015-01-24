//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : NumericTextBox.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 09/19/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a simple numeric textbox control used for the row number textbox in the DataList and
// DataNavigator controls.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/20/2005  EFW  Created the code
//===============================================================================================================

using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EWSoftware.ListControls
{
	/// <summary>
	/// This is a simple numeric textbox control used for the row number textbox in the <see cref="DataList"/>
    /// and <see cref="DataNavigator"/> controls.
	/// </summary>
	[ToolboxItem(false)]
	internal class NumericTextBox : System.Windows.Forms.TextBox
	{
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		internal NumericTextBox()
		{
            this.MaxLength = 7;
            this.TextAlign = HorizontalAlignment.Right;
            this.Text = "0";
		}
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Only allow digits to be entered and backspace to be pressed.
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '\b')
                base.OnKeyPress(e);
            else
                e.Handled = true;
        }

        /// <summary>
        /// This is overridden to validate pasted text
        /// </summary>
        /// <param name="m">The message</param>
        /// <remarks>If the pasted text contains any non-numeric characters it will not be pasted into the control</remarks>
        protected override void WndProc(ref Message m)
        {
            if(m.Msg == 0x0302)     // WM_PASTE
            {
                string strText = (string)Clipboard.GetDataObject().GetData(typeof(string));

                if(Regex.IsMatch(strText, "[^0-9]"))
                    return;
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// This is overridden to treat Enter like Tab
        /// </summary>
        /// <param name="keyData">The dialog key to process</param>
        /// <returns>True if handled, false if not</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if((keyData & Keys.KeyCode) == Keys.Enter && this.Parent.SelectNextControl(this,
              (keyData & Keys.Shift) == Keys.None, true, true, true))
                return true;

            return base.ProcessDialogKey(keyData);
        }
        #endregion
    }
}
