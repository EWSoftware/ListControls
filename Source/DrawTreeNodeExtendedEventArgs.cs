//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : DrawTreeNodeExtendedEventArgs.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2007-2024, Eric Woodruff, All rights reserved
//
// This file contains an event arguments class used to provide information for the ExtendedTreeView's
// TreeNodeDrawing and TreeNodeDrawn events.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 01/14/2007  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.ListControls
{
	/// <summary>
	/// This is used to provide information for the <see cref="ExtendedTreeView"/> events
    /// <see cref="ExtendedTreeView.TreeNodeDrawing"/> and <see cref="ExtendedTreeView.TreeNodeDrawn"/>.
	/// </summary>
    /// <remarks>When assigning new graphics objects to the event arguments, any old graphics object is disposed
    /// of automatically.  Likewise, the objects are disposed of when the event arguments are disposed of after
    /// the draw events.</remarks>
    /// <example>See <see cref="DrawTreeNodeExtendedEventArgs(Graphics , TreeNode , TreeNodeStates , Rectangle )"/>
    /// for an example of custom drawing the tree nodes.</example>
	public class DrawTreeNodeExtendedEventArgs : EventArgs, IDisposable
	{
        #region Private data members
        //=====================================================================

        private string text;

        private Font font = null!;
        private Pen linePen = null!;
        private Brush? bgBrush = null!, bgTextBrush = null!, fgBrush = null!;
        private StringFormat? sf = null!;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This read-only property returns the graphics object
        /// </summary>
        public Graphics Graphics { get; }

        /// <summary>
        /// This read-only property returns the tree node being drawn
        /// </summary>
        public TreeNode Node { get; }

        /// <summary>
        /// This read-only property returns the current state of the tree node to draw
        /// </summary>
        public TreeNodeStates State { get; }

        /// <summary>
        /// This is used to set or get the parts of the node to draw
        /// </summary>
        /// <value>The tree view will set this based on its current settings</value>
        /// <remarks>If you handle the <see cref="ExtendedTreeView.TreeNodeDrawing"/> event and custom draw one
        /// or more parts of the node, you should turn of the corresponding bit flags in this member so that the
        /// tree view will not draw them.</remarks>
        public NodeParts NodeParts { get; set; }

        /// <summary>
        /// This read-only property returns the overall bounds of the node to draw
        /// </summary>
        public Rectangle NodeBounds { get; }

        /// <summary>
        /// This is used to set or get the bounds of the expando image (+/-) if it is drawn
        /// </summary>
        public Rectangle ExpandoBounds { get; set; }

        /// <summary>
        /// This is used to set or get the bounds of the checkbox or state image if it is drawn
        /// </summary>
        public Rectangle StateBounds { get; set; }

        /// <summary>
        /// This is used to set or get the bounds of the node image if it is drawn
        /// </summary>
        public Rectangle ImageBounds { get; set; }

        /// <summary>
        /// This is used to set or get the bounds of the node text
        /// </summary>
        /// <value>This value is calculated automatically when the text is set if a font has been defined</value>
        public Rectangle TextBounds { get; set; }

        /// <summary>
        /// This is used to set or get the string format to use when drawing the text
        /// </summary>
        public StringFormat StringFormat
        {
            get => sf!;
            set
            {
                sf?.Dispose();
                sf = value;
            }
        }

        /// <summary>
        /// This is used to set or get the text to draw for the node
        /// </summary>
        /// <value>By default, it is set to the node's text.  As long as a font has been specified, the
        /// <see cref="TextBounds"/> property is recalculated automatically.</value>
        public string Text
        {
            get => text;
            set
            {
                text = value ?? String.Empty;

                // Reset the bounds based on the new text
                if(font != null)
                {
                    SizeF size = this.Graphics.MeasureString(text, font, new SizeF(999999F, this.NodeBounds.Height),
                        this.StringFormat);

                    this.TextBounds = new Rectangle(this.TextBounds.Left, this.NodeBounds.Top, (int)size.Width + 2,
                        this.NodeBounds.Height);
                }
            }
        }

        /// <summary>
        /// This is used to set or get the index of the image to draw on the node from the tree view's
        /// <see cref="TreeView.ImageList"/>.
        /// </summary>
        /// <value>If set to -1, no image is drawn</value>
        public int ImageIndex { get; set; }

        /// <summary>
        /// This is used to set or get the index of the state image to draw on the node from the tree view's
        /// <see cref="TreeView.StateImageList"/>.
        /// </summary>
        /// <value>If set to -1, no image is drawn.  This is also used to specify which image is drawn based on
        /// the node's checked state if the tree view's <see cref="TreeView.CheckBoxes"/> is set to true.</value>
        public int StateImageIndex { get; set; }

        /// <summary>
        /// This is used to set or get the font used to draw the node text
        /// </summary>
        public Font Font
        {
            get => font;
            set
            {
                // Only dispose of the font if it isn't the tree view's or the node's font
                if(font != null && font != this.Node.NodeFont && font != this.Node.TreeView.Font)
                    font.Dispose();

                font = value;
            }
        }

        /// <summary>
        /// This is used to set or get the pen used to draw the node lines
        /// </summary>
        public Pen LinePen
        {
            get => linePen;
            set
            {
                linePen?.Dispose();
                linePen = value;
            }
        }

        /// <summary>
        /// This is used to set or get the X coordinate of the inner most node line
        /// </summary>
        /// <value>This represents the position of the line closest to the node text.  The position of all outer
        /// lines can be determined by subtracting the tree view's <see cref="TreeView.Indent"/> value.</value>
        public int LinePosition { get; set; }

        /// <summary>
        /// This is used to set or get the width of the horizontal line connecting the vertical node line to the
        /// node image or text.
        /// </summary>
        public int LineWidth { get; set; }

        /// <summary>
        /// This is used to set or get the brush used to draw the node's background
        /// </summary>
        public Brush BackgroundBrush
        {
            get => bgBrush!;
            set
            {
                bgBrush?.Dispose();
                bgBrush = value;
            }
        }

        /// <summary>
        /// This is used to set or get the brush used to draw the node text's background if
        /// <see cref="TreeView.FullRowSelect"/> is set to false.
        /// </summary>
        /// <value>This will be null if <see cref="TreeView.FullRowSelect"/> is true</value>
        public Brush TextBackgroundBrush
        {
            get => bgTextBrush!;
            set
            {
                bgTextBrush?.Dispose();
                bgTextBrush = value;
            }
        }

        /// <summary>
        /// This is used to set or get the brush used to draw the node's foreground text
        /// </summary>
        public Brush TextForegroundBrush
        {
            get => fgBrush!;
            set
            {
                fgBrush?.Dispose();
                fgBrush = value;
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="g">The graphics object to use</param>
        /// <param name="treeNode">The tree node to draw</param>
        /// <param name="nodeState">The current node state</param>
        /// <param name="bounds">The node's bounds</param>
        /// <example>
        /// <code language="cs" source="..\Demos\ListControlDemoCS\ExtendedTreeViewTestForm.cs"
        ///   region="TreeNodeDrawing Example" title="C# - TreeNodeDrawing Event Handler Example" />
        /// <code language="cs" source="..\Demos\ListControlDemoCS\ExtendedTreeViewTestForm.cs"
        ///   region="TreeNodeDrawn Example" title="C# - TreeNodeDrawn Event Handler Example" />
        /// <code language="vbnet" source="..\Demos\ListControlDemoVB\ExtendedTreeViewTestForm.vb"
        ///   region="TreeNodeDrawing Example" title="VB.NET - TreeNodeDrawing Event Handler Example" />
        /// <code language="vbnet" source="..\Demos\ListControlDemoVB\ExtendedTreeViewTestForm.vb"
        ///   region="TreeNodeDrawn Example" title="VB.NET - TreeNodeDrawn Event Handler Example" />
        /// </example>
		public DrawTreeNodeExtendedEventArgs(Graphics g, TreeNode treeNode, TreeNodeStates nodeState,
          Rectangle bounds)
		{
            this.Graphics = g;
            this.Node = treeNode;
            this.State = nodeState;
            this.NodeBounds = bounds;
            this.NodeParts = NodeParts.Background;
            this.ImageIndex = this.StateImageIndex = -1;

            text = String.Empty;
		}
        #endregion

        #region IDisposable Members
        //=====================================================================

        /// <summary>
        /// This handles garbage collection to ensure proper disposal of the event arguments if not done
        /// explicitly with <see cref="Dispose()"/>.
        /// </summary>
        ~DrawTreeNodeExtendedEventArgs()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// This implements the Dispose() interface to properly dispose of the event arguments object
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// This can be overridden by derived classes to add their own disposal code if necessary
        /// </summary>
        /// <remarks>This is implemented to ensure that the graphics objects assigned to the event arguments are
        /// disposed of correctly.</remarks>
        /// <param name="disposing">Pass true to dispose of the managed and unmanaged resources or false to just
        /// dispose of the unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // There are no unmanaged resources in this class.  Just dispose of the graphics objects.
            this.Font = null!;

            linePen?.Dispose();
            bgBrush?.Dispose();
            bgTextBrush?.Dispose();
            fgBrush?.Dispose();
            sf?.Dispose();
        }
        #endregion
    }
}
