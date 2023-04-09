//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : ClickableLabel.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 07/25/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
//
// A standard label control with the ability to focus the first selectable control following it (by tab order)
// or the parent if not.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 09/23/2005  EFW  Created the code
// 12/10/2005  EFW  Made changes for use with .NET 2.0
//===============================================================================================================

using System;
using System.ComponentModel;

namespace EWSoftware.ListControls
{
	/// <summary>
	/// A standard label control with the ability to focus the first selectable control following it when clicked
    /// (by tab order) or the parent if not.
	/// </summary>
	/// <remarks><para>When clicked, this label control will attempt to find the first selectable control
    /// following it (by tab order) and give it the focus.  If no controls are found or the first control found
    /// is disabled, the label gives the focus to the parent control.</para>
    /// 
    /// <para>This label is useful in <see cref="TemplateControl"/> controls.  Clicking a standard label control
    /// in the template fails to focus the row because it ignores all mouse clicks.  Clicking this label control
    /// will cause the row to gain the focus even when there are no selectable controls in the row.</para></remarks>
    [Description("A standard label control with the ability to focus the first selectable control following " +
      "it when clicked")]
	public class ClickableLabel : System.Windows.Forms.Label
	{
        /// <summary>
        /// This is overridden to focus the control following the label or the parent if one cannot be found
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnClick(EventArgs e)
        {
            int idx, tabIdx, selIdx = -1;

            ControlCollection cc = this.Parent.Controls;

            // Find the first control with a tab index greater than this one and give it the focus if possible
            for(idx = 0, tabIdx = 32767; idx < cc.Count; idx++)
                if(cc[idx].TabStop && cc[idx].TabIndex > this.TabIndex && cc[idx].TabIndex < tabIdx)
                {
                    tabIdx = cc[idx].TabIndex;

                    // If it can't be focused, clear the selected item so that it doesn't focus one further on in
                    // the tab order.
                    if(cc[idx].CanFocus && cc[idx].CanSelect && !cc[idx].Focused)
                        selIdx = idx;
                    else
                        selIdx = -1;
                }

            if(selIdx != -1)
                cc[selIdx].Focus();

            // If the above didn't find anything, just give the parent the focus
            if(!this.Parent.ContainsFocus)
                this.Parent.Focus();

            base.OnClick(e);
        }
	}
}
