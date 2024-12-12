//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : Separator.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/02/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains a simple control used to display a separator between rows in the DataList control
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

namespace EWSoftware.ListControls
{
	/// <summary>
	/// This is a simple control used to display a separator between rows in the <see cref="DataList"/> control
	/// </summary>
	[ToolboxItem(false)]
	internal sealed class Separator : System.Windows.Forms.Control
	{
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="color">The background color to use</param>
        /// <param name="height">The height of the separator</param>
        /// <param name="width">The initial width of the separator</param>
        /// <param name="left">The left position of the separator</param>
        /// <param name="top">The top position of the separator</param>
		internal Separator(Color color, int height, int width, int left, int top)
		{
            this.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.BackColor = color;
            this.Size = new Size(width, height);
            this.Location = new Point(left, top);
            this.SetStyle(ControlStyles.Selectable, false);
		}
	}
}
