//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : BaseButtonList.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 07/24/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains an abstract base button list control that supports data binding, layout options, and data
// source indexers and serves as the base class for the RadioButtonList and CheckBoxList controls.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 06/25/2005  EFW  Created the code
// 12/09/2005  EFW  Various improvements, fixes, and modifications
// 05/01/2006  EFW  Added support for UseMnemonic
//===============================================================================================================

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This is an abstract base button list control that supports data binding, layout options, and data source
    /// indexers and serves as the base class for the <see cref="RadioButtonList"/> and <see cref="CheckBoxList"/>
    /// controls.
    /// </summary>
    public abstract class BaseButtonList : BaseListControl
    {
        #region Private data members
        //====================================================================

        // This is for laying out the controls and indicates the size of the checkmark part of the control
        private static int CheckmarkSize = 20;

        // Alignment combinations
        private static int LeftAlignments = (int)(ContentAlignment.TopLeft | ContentAlignment.MiddleLeft |
            ContentAlignment.BottomLeft);

        private static int RightAlignments = (int)(ContentAlignment.TopRight | ContentAlignment.MiddleRight |
            ContentAlignment.BottomRight);

        private System.Windows.Forms.Panel pnlButtons;

        // Title properties
        private SolidBrush brBackground, brTitleBack, brTitleFore;
        private Pen penTitleBorder;
        private Font fontTitle;
        private StringFormat sfTitle;
        private string titleText;

        // Border style, border width, and image list
        private Border3DStyle borderStyle;
        private int borderTop;
        private ImageList ilImages;

        // Layout
        private ContentAlignment checkAlign, imageAlign, textAlign;
        private Appearance appearance;
        private LayoutMethod layoutMethod;
        private ListPadding padding;
        private bool sizeAllToWidest, useMnemonic;

        #endregion

        #region Properties
        //====================================================================

        /// <summary>
        /// This is overridden to pass the value to the contained button panel
        /// </summary>
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set
            {
                pnlButtons.AutoScroll = !value;
                base.AutoSize = pnlButtons.AutoSize = value;
            }
        }

        /// <summary>
        /// This is used to pass the value to the contained button panel
        /// </summary>
        public new AutoSizeMode AutoSizeMode
        {
            get { return base.AutoSizeMode; }
            set
            {
                base.AutoSizeMode = pnlButtons.AutoSizeMode = value;
            }
        }

        /// <summary>
        /// This returns a reference to the panel control that contains the button controls
        /// </summary>
        /// <remarks>This can be used to customize the appearance of the individual button controls.  This is
        /// usually accomplished by handling the <see cref="BaseListControl.SubControlsRefreshed"/> event.</remarks>
        [Browsable(false), Description("The panel containing the button controls")]
        public Panel ButtonPanel
        {
            get { return pnlButtons; }
        }

        /// <summary>
        /// This property is used to set or get the border style
        /// </summary>
        /// <remarks>The default is to use an etched border</remarks>
        [Category("Appearance"), DefaultValue(Border3DStyle.Etched), RefreshProperties(RefreshProperties.Repaint),
          Bindable(true), Description("The border style for the button list")]
        public new Border3DStyle BorderStyle
        {
            get { return borderStyle; }
            set
            {
                if(borderStyle != value)
                {
                    borderStyle = value;
                    OnBorderStyleChanged(EventArgs.Empty);
                    base.PerformLayout();
                    base.Invalidate(true);
                }
            }
        }

        /// <summary>
        /// This is used to set or get the flat drawing style to use for the control
        /// </summary>
        public override FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
            set
            {
                foreach(Control c in pnlButtons.Controls)
                    ((ButtonBase)c).FlatStyle = value;

                base.FlatStyle = value;
                base.Invalidate(true);
            }
        }

        /// <summary>
        /// In order to draw a partial transparency above the border when <see cref="TitleText"/> is specified,
        /// this property is replaced by the <see cref="ListBackColor"/> property.  Use it to set the background
        /// color instead.
        /// </summary>
        [Browsable(false), Bindable(false)]
        public override Color BackColor
        {
            get { return base.BackColor; }
        }

        /// <summary>
        /// This property is used to set or get the background color
        /// </summary>
        /// In order to draw a partial transparency above the border when <see cref="TitleText"/> is specified,
        /// this property is used to take the place of the <see cref="BackColor"/> property.
        [Category("Appearance"), Bindable(true), DefaultValue(typeof(Color), "Control"),
         Description("The control's background color")]
        public Color ListBackColor
        {
            get { return brBackground.Color; }
            set
            {
                brBackground.Color = pnlButtons.BackColor = value;
                OnListBackColorChanged(EventArgs.Empty);
                base.Invalidate(true);
            }
        }

        /// <summary>
        /// This is used to set or get the button appearance
        /// </summary>
        /// <value>The default is to show them as normal radio buttons or checkbox buttons</value>
        [Category("Appearance"), DefaultValue(Appearance.Normal), Description("The appearance of the buttons")]
        public virtual Appearance Appearance
        {
            get { return appearance; }
            set
            {
                appearance = value;
                base.PerformLayout();
            }
        }

        /// <summary>
        /// This is used to set or get the image list for the buttons
        /// </summary>
        /// <value>If set, the buttons are assigned an image index equal to their position in the data source.
        /// The index will wrap around if there are more items than there are images thus repeating images for
        /// those items that are outside the range of the image list.</value>
        [Category("Appearance"), DefaultValue(null), Description("The image list for the buttons")]
        public ImageList ImageList
        {
            get { return ilImages; }
            set
            {
                if(ilImages != value)
                {
                    int imageIdx = 0;

                    ButtonBase bb;
                    EventHandler ListRecreated = ImageList_RecreateHandle;
                    EventHandler ListDisposed = ImageList_Disposed;

                    if(ilImages != null)
                    {
                        ilImages.RecreateHandle -= ListRecreated;
                        ilImages.Disposed -= ListDisposed;
                    }

                    ilImages = value;

                    if(value != null)
                    {
                        ilImages.RecreateHandle += ListRecreated;
                        ilImages.Disposed += ListDisposed;
                    }

                    // Clear the old images and text padding and set the new images if specified
                    foreach(Control c in pnlButtons.Controls)
                    {
                        bb = (ButtonBase)c;
                        bb.Text = bb.Text.Trim();

                        if(value == null)
                        {
                            bb.ImageIndex = -1;
                            bb.ImageList = null;
                        }
                        else
                        {
                            bb.ImageList = ilImages;
                            bb.ImageIndex = imageIdx++;

                            // Wrap index if there are more items than images
                            if(imageIdx == ilImages.Images.Count)
                                imageIdx = 0;
                        }
                    }

                    base.PerformLayout();
                }
            }
        }

        /// <summary>
        /// This is used to set or get the alignment on the button images
        /// </summary>
        /// <value>The default alignment is <c>MiddleLeft</c>.  Note that right-aligned images with right-aligned
        /// text will not work as the text will always overlap the image.  Centered text with right-aligned
        /// images will work but there may be some overlap on the longest item.</value>
        [Category("Appearance"), DefaultValue(ContentAlignment.MiddleLeft),
          Description("The alignment for the button images")]
        public ContentAlignment ImageAlign
        {
            get { return imageAlign; }
            set
            {
                imageAlign = value;

                foreach(Control c in pnlButtons.Controls)
                    ((ButtonBase)c).ImageAlign = value;

                base.PerformLayout();
            }
        }

        /// <summary>
        /// This is used to set or get the alignment on the button checkmark
        /// </summary>
        /// <value>The default alignment is <c>MiddleLeft</c></value>
        [Category("Appearance"), DefaultValue(ContentAlignment.MiddleLeft),
          Description("The alignment for the button checkmark")]
        public virtual ContentAlignment CheckAlign
        {
            get { return checkAlign; }
            set
            {
                checkAlign = value;
                base.PerformLayout();
            }
        }

        /// <summary>
        /// This is used to set or get the alignment on the button text
        /// </summary>
        /// <value>The default alignment is <c>MiddleLeft</c>.  Note that right-aligned images with right-aligned
        /// text will not work as the text will always overlap the image.  Centered text with right-aligned
        /// images will work but there may be some overlap on the longest item.</value>
        [Category("Appearance"), DefaultValue(ContentAlignment.MiddleLeft),
          Description("The alignment for the button text")]
        public ContentAlignment TextAlign
        {
            get { return textAlign; }
            set
            {
                textAlign = value;

                foreach(Control c in pnlButtons.Controls)
                    ((ButtonBase)c).TextAlign = value;

                base.PerformLayout();
            }
        }

        /// <summary>
        /// This property is used to set or get whether or not mnemonics are used in the display text
        /// </summary>
        /// <remarks>If true (the default), the first character preceded by an ampersand (&amp;) character in the
        /// display text will be used as the button's mnemonic key.</remarks>
        [Category("Appearance"), RefreshProperties(RefreshProperties.Repaint),
          DefaultValue(true), Description("If true, the first character preceded by an ampersand (&) " +
            "character in the display text will be used as the button's mnemonic key.")]
        public bool UseMnemonic
        {
            get { return useMnemonic; }
            set
            {
                if(value != useMnemonic)
                {
                    useMnemonic = value;

                    // In .NET 2.0 and later, we could use the UseMnemonic property but it doesn't work with
                    // FlatStyle.System so we'll modify the text which works with it and with .NET 1.1.
                    foreach(Control c in pnlButtons.Controls)
                        if(value)
                            c.Text = c.Text.Replace("&&", "&");
                        else
                            c.Text = c.Text.Replace("&", "&&");

                    base.PerformLayout();
                }
            }
        }

        /// <summary>
        /// This is used to set or get the layout method for the buttons
        /// </summary>
        /// <value>The default alignment is <c>SingleColumn</c></value>
        /// <seealso cref="ListPadding"/>
        /// <seealso cref="SizeAllToWidest"/>
        [Category("Layout"), DefaultValue(LayoutMethod.SingleColumn),
          Description("The layout method for the buttons")]
        public LayoutMethod LayoutMethod
        {
            get { return layoutMethod; }
            set
            {
                layoutMethod = value;
                base.PerformLayout();
            }
        }

        /// <summary>
        /// This is used to set or get whether or not all columns in the button list are sized to the widest
        /// item.
        /// </summary>
        /// <value>This option affects all <see cref="LayoutMethod"/> options except <c>SingleColumn</c>.  When
        /// set to false (the default) each column is sized to the widest item in that column.  In
        /// <c>SingleColumn</c> mode, this is the normal behavior.  When set to true, all columns are sized to
        /// the widest entry in the list thus making all columns the same width.</value>
        /// <seealso cref="LayoutMethod"/>
        /// <seealso cref="ListPadding"/>
        [Category("Appearance"), DefaultValue(false),
          Description("Determines whether or not to size all columns to the widest item")]
        public bool SizeAllToWidest
        {
            get { return sizeAllToWidest; }
            set
            {
                sizeAllToWidest = value;
                base.PerformLayout();
            }
        }

        /// <summary>
        /// Get or set the padding values (in pixels) for the space around the button list and between its
        /// columns and rows.
        /// </summary>
        /// <value><para>The <c>Top</c> and <c>Left</c> values are always applied to the left most column and top
        /// row.  The <c>Bottom</c> value is used when <see cref="LayoutMethod"/> is set to <c>DownThenAcross</c>
        /// to determine how close an item can come to the bottom border before forcing a new column.  The
        /// <c>Right</c> value is used when <see cref="LayoutMethod"/> is set to <c>AcrossThenDown</c> to
        /// determine how close an item can come to the right border before forcing a new row.  The <c>Column</c>
        /// and <c>Row</c> values are used for the spacing between columns and rows.</para>
        /// 
        /// <para>The defaults are four pixels for the top and bottom, eight pixels for the left and right
        /// borders, four pixels between columns and no extra space between rows.</para></value>
        /// <seealso cref="LayoutMethod"/>
        /// <seealso cref="SizeAllToWidest"/>
        [Category("Layout"), Description("Padding values for the various layout methods")]
        public ListPadding ListPadding
        {
            get { return padding; }
            set
            {
                padding = value;
                base.PerformLayout();
            }
        }

        /// <summary>
        /// The stock padding property is not supported.  Use <see cref="ListPadding"/> instead.
        /// </summary>
        /// <exclude/>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding
        {
            get { throw new NotSupportedException(LR.GetString("ExPropNotSupported")); }
        }

        /// <summary>
        /// This is used to specify the title displayed on the top border
        /// </summary>
        /// <value>If set to null or an empty string (the default), no title is displayed</value>
        [Category("Title"), DefaultValue(null), Browsable(true), RefreshProperties(RefreshProperties.Repaint),
          Bindable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public string TitleText
        {
            get { return titleText; }
            set
            {
                if(value != null && value.Trim().Length == 0)
                    value = null;

                titleText = value;

                OnTitleTextChanged(EventArgs.Empty);
                base.PerformLayout();
                base.Invalidate(true);
            }
        }

        /// <summary>
        /// This property is used to set or get the color of the title's border
        /// </summary>
        [Category("Title"), Bindable(true), DefaultValue(typeof(Color), "Black"),
         Description("The color of the border around the title")]
        public Color TitleBorderColor
        {
            get { return penTitleBorder.Color; }
            set
            {
                if(penTitleBorder != null)
                    penTitleBorder.Dispose();

                penTitleBorder = new Pen(value);
                OnTitleBorderColorChanged(EventArgs.Empty);
                base.Invalidate(true);
            }
        }

        /// <summary>
        /// This property is used to set or get the title's background color
        /// </summary>
        [Category("Title"), Bindable(true), DefaultValue(typeof(Color), "Control"),
         Description("The title's background color")]
        public Color TitleBackColor
        {
            get { return brTitleBack.Color; }
            set
            {
                brTitleBack.Color = value;
                OnTitleBackColorChanged(EventArgs.Empty);
                base.Invalidate(true);
            }
        }

        /// <summary>
        /// This property is used to set or get the title's foreground color
        /// </summary>
        [Category("Title"), Bindable(true), DefaultValue(typeof(Color), "ControlText"),
         Description("The title's foreground color")]
        public Color TitleForeColor
        {
            get { return brTitleFore.Color; }
            set
            {
                brTitleFore.Color = value;
                OnTitleForeColorChanged(EventArgs.Empty);
                base.Invalidate(true);
            }
        }

        /// <summary>
        /// This property is used to set or get the title's font
        /// </summary>
        [Category("Title"), Bindable(true), RefreshProperties(RefreshProperties.Repaint),
          DefaultValue(typeof(Font), "Microsoft Sans Serif, 7.8pt"), Description("The title's font")]
        public Font TitleFont
        {
            get { return fontTitle; }
            set
            {
                if(value == null)
                    fontTitle = new Font(Control.DefaultFont, Control.DefaultFont.Style);
                else
                    fontTitle = value;

                OnTitleFontChanged(EventArgs.Empty);
                base.PerformLayout();
                base.Invalidate(true);
            }
        }
        #endregion

        #region Events
        //=====================================================================

        /// <summary>
        /// This event is raised when the <see cref="ListBackColor"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the list background color changes")]
        public event EventHandler ListBackColorChanged;

        /// <summary>
        /// This raises the <see cref="ListBackColorChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnListBackColorChanged(EventArgs e)
        {
            var handler = ListBackColorChanged;

            if(handler != null)
                handler(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="BorderStyle"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the border style changes")]
        public event EventHandler BorderStyleChanged;

        /// <summary>
        /// This raises the <see cref="BorderStyleChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnBorderStyleChanged(EventArgs e)
        {
            var handler = BorderStyleChanged;

            if(handler != null)
                handler(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="TitleText"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the title text changes")]
        public event EventHandler TitleTextChanged;

        /// <summary>
        /// This raises the <see cref="TitleTextChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnTitleTextChanged(EventArgs e)
        {
            var handler = TitleTextChanged;

            if(handler != null)
                handler(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="TitleBorderColor"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the title border color changes")]
        public event EventHandler TitleBorderColorChanged;

        /// <summary>
        /// This raises the <see cref="TitleBorderColorChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnTitleBorderColorChanged(EventArgs e)
        {
            var handler = TitleBorderColorChanged;

            if(handler != null)
                handler(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="TitleBackColor"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the title background color changes")]
        public event EventHandler TitleBackColorChanged;

        /// <summary>
        /// This raises the <see cref="TitleBackColorChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnTitleBackColorChanged(EventArgs e)
        {
            var handler = TitleBackColorChanged;

            if(handler != null)
                handler(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="TitleForeColor"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the title foreground color changes")]
        public event EventHandler TitleForeColorChanged;

        /// <summary>
        /// This raises the <see cref="TitleForeColorChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnTitleForeColorChanged(EventArgs e)
        {
            var handler = TitleForeColorChanged;

            if(handler != null)
                handler(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="TitleFont"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the title font changes")]
        public event EventHandler TitleFontChanged;

        /// <summary>
        /// This raises the <see cref="TitleFontChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnTitleFontChanged(EventArgs e)
        {
            var handler = TitleFontChanged;

            if(handler != null)
                handler(this, e);
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>By default, the button list will display all items in the data source in a single column,
        /// item parts are left aligned and the control will have an etched border.  A default selection (item
        /// zero) is enforced.</remarks>
        protected BaseButtonList()
        {
            checkAlign = imageAlign = textAlign = ContentAlignment.MiddleLeft;
            appearance = Appearance.Normal;
            borderStyle = Border3DStyle.Etched;
            useMnemonic = true;

            // NOTE: If you change this, change it in the designer methods too!
            padding = new ListPadding(4, 8, 4, 8, 4, 0);

            brBackground = new SolidBrush(SystemColors.Control);
            brTitleBack = new SolidBrush(SystemColors.Control);
            brTitleFore = new SolidBrush(SystemColors.ControlText);
            penTitleBorder = new Pen(Color.Black);
            fontTitle = new Font(Control.DefaultFont, Control.DefaultFont.Style);
            sfTitle = new StringFormat();
			sfTitle.Alignment = StringAlignment.Center;
			sfTitle.LineAlignment = StringAlignment.Center;

            this.InitializeComponent();
        }
        #endregion

		#region Component Designer generated code
        //=====================================================================

		/// <summary>
		/// Required method for Designer support - do not modify the contents of this method with the code
        /// editor.
		/// </summary>
        private void InitializeComponent()
        {
            this.pnlButtons = new System.Windows.Forms.Panel();
            //
            // pnlButtons
            //
            this.pnlButtons.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            this.pnlButtons.AutoScroll = true;
            this.pnlButtons.Location = new System.Drawing.Point(0, 0);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(150, 23);
            this.pnlButtons.TabIndex = 0;
            this.pnlButtons.MouseDown += pnlButtons_MouseDown;
            //
            // BaseButtonList
            //
            this.Controls.Add(this.pnlButtons);
            this.Size = new System.Drawing.Size(150, 23);
        }
		#endregion

        #region Private designer methods
        //=====================================================================

        // Private designer methods.  These are used because the default values for these properties don't work
        // with the DefaultValue attribute.

        /// <summary>
        /// Determine whether or not to serialize the padding values
        /// </summary>
        internal bool ShouldSerializeListPadding()
        {
            return !padding.Equals(new ListPadding(4, 8, 4, 8, 4, 0));
        }

        /// <summary>
        /// Reset the padding values
        /// </summary>
        internal void ResetListPadding()
        {
            this.ListPadding = new ListPadding(4, 8, 4, 8, 4, 0);
        }
        #endregion

        #region Private class methods
        //=====================================================================

        /// <summary>
        /// This is handled to focus a button when the mouse is clicked in an area outside of one of the buttons
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void pnlButtons_MouseDown(object sender, MouseEventArgs e)
        {
            if(this.SelectedIndex != -1 && pnlButtons.Controls.Count != 0)
            {
                Control c = pnlButtons.Controls[this.SelectedIndex];
                pnlButtons.ScrollControlIntoView(c);

                if(!c.Focused)
                    c.Focus();
            }
        }

        /// <summary>
        /// This is called when the associated image list handle is recreated
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void ImageList_RecreateHandle(object sender, EventArgs e)
        {
            if(base.IsHandleCreated)
                base.Invalidate(true);
        }

        /// <summary>
        /// This is called when the associated image list is destroyed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void ImageList_Disposed(object sender, EventArgs e)
        {
            this.ImageList = null;
        }

        /// <summary>
        /// This is used to lay out the buttons in a single column
        /// </summary>
        /// <param name="itemHeight">The height of a button item</param>
        /// <param name="commonWidth">All items are sized to the same width</param>
        private void LayoutSingleColumn(int itemHeight, int commonWidth)
        {
            int top = padding.Top;

            // Adjust button size and positioning
            foreach(Control c in pnlButtons.Controls)
            {
                c.Location = new Point(padding.Left, top);
                c.Height = itemHeight;
                c.Width = commonWidth;

                top += itemHeight + padding.Row;
            }
        }

        /// <summary>
        /// This is used to lay out the buttons in a single row
        /// </summary>
        /// <param name="itemHeight">The height of a button item.</param>
        /// <param name="extraWidth">The extra width to apply to each button to account for the checkmark and
        /// image.</param>
        /// <param name="commonWidth">If not -1, all items are sized to the same width.  If -1, items are sized
        /// based on their settings.</param>
        private void LayoutSingleRow(int itemHeight, int extraWidth, int commonWidth)
        {
            Size proposed = new Size(Int32.MaxValue, Int32.MaxValue);
            SizeF size;
            int left = padding.Left;

            // Adjust button size and positioning
            foreach(Control c in pnlButtons.Controls)
            {
                c.Location = new Point(left, padding.Top);
                c.Height = itemHeight;

                if(commonWidth == -1)
                {
                    if(c.Text.Length != 0)
                    {
                        size = TextRenderer.MeasureText(c.Text, this.Font, proposed, TextFormatFlags.NoPrefix);
                        left += (int)size.Width;
                        c.Width = (int)size.Width + extraWidth;
                    }
                    else
                        c.Width = extraWidth;

                    left += extraWidth + padding.Column;
                }
                else
                {
                    c.Width = commonWidth;
                    left += commonWidth + padding.Column;
                }
            }
        }

        /// <summary>
        /// This is used to lay out the buttons down and then across the client area
        /// </summary>
        /// <param name="itemHeight">The height of a button item.</param>
        /// <param name="extraWidth">The extra width to apply to each button to account for the checkmark and
        /// image.</param>
        /// <param name="commonWidth">If not -1, all columns are sized to the same width.  If -1, columns are
        /// sized to the widest item in that column.</param>
        private void LayoutDownThenAcross(int itemHeight, int extraWidth, int commonWidth)
        {
            Size proposed = new Size(Int32.MaxValue, Int32.MaxValue);
            Control ctl;
            SizeF size;
            int first, top, maxWidth, current = 0, left = padding.Left, bottom = pnlButtons.Height - padding.Bottom;

            // If auto-sizing, use the parent's height as the maximum height.  The size of the panel will vary
            // based on the number of items so we can't use it.
            if(base.AutoSize)
                if(this.Parent != null)
                    bottom = this.Parent.ClientSize.Height - this.Top - pnlButtons.Top - padding.Bottom;
                else
                    bottom = padding.Top + itemHeight;

            if(bottom <= padding.Top + itemHeight)
                bottom = padding.Top + itemHeight + 1;

            // If the vertical scrollbar will appear, we need to subtract the horizontal scrollbar's height
            while(current < pnlButtons.Controls.Count)
            {
                top = padding.Top;
                maxWidth = (commonWidth == -1) ? 0 : commonWidth;

                // Add a column
                while(current < pnlButtons.Controls.Count && top + itemHeight < bottom)
                {
                    ctl = pnlButtons.Controls[current];

                    // Find the maximum width
                    if(commonWidth == -1 && ctl.Text.Length != 0)
                    {
                        size = TextRenderer.MeasureText(ctl.Text, this.Font, proposed, TextFormatFlags.NoPrefix);

                        if((int)size.Width + extraWidth > maxWidth)
                            maxWidth = (int)size.Width + extraWidth;
                    }

                    top += itemHeight + padding.Row;
                    current++;
                }

                if(left + maxWidth > pnlButtons.Width)
                {
                    bottom -= SystemInformation.HorizontalScrollBarHeight;
                    break;
                }

                left += maxWidth + padding.Column;
            }

            // Adjust button size and positioning
            current = 0;
            left = padding.Left;

            // Don't get stuck
            if(bottom <= padding.Top + itemHeight)
                bottom = padding.Top + itemHeight + 1;

            while(current < pnlButtons.Controls.Count)
            {
                top = padding.Top;
                maxWidth = (commonWidth == -1) ? 0 : commonWidth;
                first = current;

                // Add a column
                while(current < pnlButtons.Controls.Count && top + itemHeight < bottom)
                {
                    ctl = pnlButtons.Controls[current];
                    ctl.Location = new Point(left, top);
                    ctl.Height = itemHeight;

                    // Find the maximum width
                    if(commonWidth == -1 && ctl.Text.Length != 0)
                    {
                        size = TextRenderer.MeasureText(ctl.Text, this.Font, proposed, TextFormatFlags.NoPrefix);

                        if((int)size.Width + extraWidth > maxWidth)
                            maxWidth = (int)size.Width + extraWidth;
                    }

                    top += itemHeight + padding.Row;
                    current++;
                }

                // Set the width on all items in this column to the same thing
                while(first < current)
                    pnlButtons.Controls[first++].Width = maxWidth;

                left += maxWidth + padding.Column;
            }
        }

        /// <summary>
        /// This is used to lay out the buttons across and then down the client area
        /// </summary>
        /// <param name="itemHeight">The height of a button item.</param>
        /// <param name="extraWidth">The extra width to apply to each button to account for the checkmark and
        /// image.</param>
        /// <param name="commonWidth">If not -1, all columns are sized to the same width.  If -1, columns are
        /// sized to the widest item in that column.</param>
        private void LayoutAcrossThenDown(int itemHeight, int extraWidth, int commonWidth)
        {
            Size proposed = new Size(Int32.MaxValue, Int32.MaxValue);
            Control ctl;
            SizeF size;

            int actualRight, colCount = 0, maxCols = 32767, current = 0,
                top = padding.Top - itemHeight - padding.Row, left = padding.Left,
                right = pnlButtons.Width - padding.Right;

            int[] maxWidths = new int[pnlButtons.Controls.Count];

            // If auto-sizing, use the parent's width as the maximum width.  The size of the panel will vary
            // based on the number of items so we can't use it.
            if(base.AutoSize)
            {
                actualRight = (commonWidth == -1) ? 25 : commonWidth;

                if(this.Parent != null)
                    right = this.Parent.ClientSize.Width - this.Left - pnlButtons.Left - padding.Right;
                else
                    right = padding.Right + 100;

                if(right < actualRight)
                    right = actualRight;
            }

            // Find out how many will fit across
            actualRight = right;

            while(current <  pnlButtons.Controls.Count && maxCols > 1)
            {
                if(colCount >= maxCols)
                {
                    left = padding.Left;
                    colCount = 0;
                }

                ctl = pnlButtons.Controls[current];

                if(commonWidth != -1)
                    maxWidths[colCount] = commonWidth;
                else
                    if(ctl.Text.Length != 0)
                    {
                        size = TextRenderer.MeasureText(ctl.Text, this.Font, proposed, TextFormatFlags.NoPrefix);

                        if(maxWidths[colCount] < (int)size.Width + extraWidth)
                            maxWidths[colCount] = (int)size.Width + extraWidth;
                    }
                    else
                        if(maxWidths[colCount] < extraWidth)
                            maxWidths[colCount] = extraWidth;

                left += maxWidths[colCount];

                // If we hit the end, add in the remaining column widths too to see if the last row would push
                // the width out too far.
                if(maxCols != 32767 && current == maxWidths.Length - 1)
                    while(colCount < maxCols)
                    {
                        left += maxWidths[++colCount];

                        if(colCount < maxCols - 1)
                            left += padding.Column;
                    }

                // If a set won't fit, subtract a column and try again
                if(left > right)
                {
                    current = 0;

                    if(maxCols == 32767)
                        maxCols = colCount;
                    else
                    {
                        maxCols--;
                        colCount = maxCols;
                    }

                    for(int w = 0; w < maxWidths.Length; w++)
                        maxWidths[w] = 0;

                    // If the vertical scrollbar will appear, subtract its width
                    if(maxCols > 0 && maxWidths.Length / maxCols * (itemHeight + padding.Row) > pnlButtons.Bottom)
                        right = actualRight - SystemInformation.VerticalScrollBarWidth;

                    continue;
                }

                left += padding.Column;
                current++;
                colCount++;
            }

            if(maxCols < 2)
            {
                maxCols = 1;

                // Get the width if it wasn't set
                if(maxWidths[0] == 0)
                    if(commonWidth != -1)
                        maxWidths[0] = commonWidth;
                    else
                        foreach(Control c in pnlButtons.Controls)
                        {
                            size = TextRenderer.MeasureText(c.Text, this.Font, proposed, TextFormatFlags.NoPrefix);

                            if(maxWidths[0] < (int)size.Width + extraWidth)
                                maxWidths[0] = (int)size.Width + extraWidth;
                        }
            }

            // Okay, now that we know how many columns we have, we can set the positions and sizes of the buttons
            current = 0;
            colCount = maxCols;

            while(current < pnlButtons.Controls.Count)
            {
                if(colCount >= maxCols)
                {
                    left = padding.Left;
                    colCount = 0;
                    top += itemHeight + padding.Row;
                }

                ctl = pnlButtons.Controls[current];
                ctl.Location = new Point(left, top);
                ctl.Height = itemHeight;
                ctl.Width = maxWidths[colCount];

                left += maxWidths[colCount] + padding.Column;
                current++;
                colCount++;
            }
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// Clean up any resources being used
        /// </summary>
        /// <param name="disposing">True to release both managed and unmanaged resources, false to just release
        /// unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                if(penTitleBorder != null)
                    penTitleBorder.Dispose();

                if(brBackground != null)
                    brBackground.Dispose();

                if(brTitleBack != null)
                    brTitleBack.Dispose();

                if(brTitleFore != null)
                    brTitleFore.Dispose();

                if(fontTitle != null)
                    fontTitle.Dispose();

                if(sfTitle != null)
                    sfTitle.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// This is overridden to adjust the background area painted by the base class when the title text is
        /// used.
        /// </summary>
        /// <param name="pevent">The event arguments</param>
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Draw transparent?
            if(brBackground.Color.A < 0xFF)
                base.OnPaintBackground(pevent);
            else
            {
                Rectangle r = pevent.ClipRectangle;

                // Draw the transparent part if necessary
                if(borderTop != 0 && r.Top < borderTop)
                {
                    r.Height = borderTop - r.Top;

                    // Do not dispose of the PaintEventArgs instance as it isn't our graphics context
                    base.OnPaintBackground(new PaintEventArgs(pevent.Graphics, r));
                }

                pevent.Graphics.FillRectangle(brBackground, 0, borderTop,
                    base.Width, base.Height - borderTop);
            }
        }

        /// <summary>
        /// This is overridden to draw the border and title text
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            if(titleText == null || titleText.Length == 0)
                ControlPaint.DrawBorder3D(g, base.ClientRectangle, borderStyle);
            else
            {
                Rectangle r;

                r = new Rectangle(0, borderTop, base.Width, base.Height - borderTop);

                ControlPaint.DrawBorder3D(g, r, borderStyle);

                // NOTE: Don't use TextRender.MeasureText or it gets too wide as the text gets longer.
                SizeF size = g.MeasureString(titleText, fontTitle, base.Width, sfTitle);

                r = new Rectangle(8, 0, (int)size.Width + 18, (int)size.Height + 4);
                g.FillRectangle(brTitleBack, r);
                g.DrawString(titleText, fontTitle, brTitleFore, r, sfTitle);
                g.DrawRectangle(penTitleBorder, r);
            }
        }

        /// <summary>
        /// This is overridden to handle laying out the control elements
        /// </summary>
        /// <param name="levent">The event parameters</param>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            Size proposed = new Size(Int32.MaxValue, Int32.MaxValue);

            // Start out with 4 extra pixels to provide padding for variations in the actual text widths
            int borderWidth, itemHeight, spaceWidth = 0, commonWidth = -1, extraWidth = 4;
            string padText;
            SizeF size;

            switch(borderStyle)
            {
                case Border3DStyle.Adjust:
                    borderWidth = 0;
                    break;

                case Border3DStyle.Flat:
                    borderWidth = 1;
                    break;

                default:
                    borderWidth = 2;
                    break;
            }

            // Must reset the scroll position or the controls drift based on the scrolled position
            pnlButtons.AutoScrollPosition = new Point(0, 0);

            if(titleText == null || titleText.Length == 0)
            {
                borderTop = 0;
                pnlButtons.Location = new Point(borderWidth * 2, borderWidth * 2);
                pnlButtons.Size = new Size(base.Width - (borderWidth * 4), base.Height - (borderWidth * 4));
            }
            else
            {
                size = TextRenderer.MeasureText(titleText, fontTitle, new Size(base.Width, Int32.MaxValue),
                    TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.NoPrefix);
                borderTop = (int)((size.Height + 4) / 2.5);

                pnlButtons.Location = new Point(borderWidth * 2, (borderWidth * 2) + (int)(size.Height + 4));
                pnlButtons.Size = new Size(base.Width - (borderWidth * 4),
                    base.Height - (borderWidth * 4) - (int)(size.Height + 4));
            }

            // Get the item height
            itemHeight = TextRenderer.MeasureText("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghigklmnopqrstuvwxyz",
                this.Font).Height + 4;

            if(itemHeight < BaseButtonList.CheckmarkSize)
                itemHeight = BaseButtonList.CheckmarkSize;

            // If the button is centered above or below, add the button's size to the height.  If not, increase
            // the width by the checkmark's size.
            if(appearance == Appearance.Button)
            {
                extraWidth += 20;
                itemHeight += 4;
            }
            else
                if(checkAlign == ContentAlignment.TopCenter || checkAlign == ContentAlignment.BottomCenter)
                {
                    itemHeight += 8;
                    extraWidth += 4;
                }
                else
                    if(checkAlign == ContentAlignment.MiddleCenter)
                        extraWidth += 4;
                    else
                        extraWidth += 20;

            // Increase the width and height by the image size if used
            if(ilImages != null)
            {
                if(itemHeight < ilImages.ImageSize.Height)
                    itemHeight = ilImages.ImageSize.Height;

                if(((int)imageAlign & BaseButtonList.LeftAlignments) == 0 ||
                  ((int)textAlign & BaseButtonList.LeftAlignments) == 0)
                    extraWidth += ilImages.ImageSize.Width;

                // The buttons don't layout the text correctly when an image is used.  Pad the text on each item
                // to compensate.
                spaceWidth = (ilImages.ImageSize.Width / TextRenderer.MeasureText(" ", this.Font).Width) + 2;
            }

            if(ilImages != null || sizeAllToWidest || layoutMethod == LayoutMethod.SingleColumn)
                foreach(Control c in pnlButtons.Controls)
                {
                    // Pad text if necessary
                    if(ilImages != null)
                    {
                        padText = c.Text.Trim();

                        // NOTE: Right aligned images with right aligned text does not work.  It refuses to pad
                        // the text on the right side for some reason.
                        if(padText.Length != 0 && ((int)imageAlign & BaseButtonList.LeftAlignments) != 0 &&
                          ((int)textAlign & BaseButtonList.RightAlignments) == 0)
                        {
                            if(((int)textAlign & BaseButtonList.LeftAlignments) != 0)
                                padText = padText.PadLeft(padText.Length + spaceWidth);
                            else
                            {
                                padText = padText.PadLeft(padText.Length + (spaceWidth / 2));
                                padText = padText.PadRight(padText.Length + (spaceWidth / 2));
                            }
                        }

                        c.Text = padText;
                    }

                    // Find the maximum width if necessary
                    if(sizeAllToWidest || layoutMethod == LayoutMethod.SingleColumn)
                        if(c.Text.Length != 0)
                        {
                            size = TextRenderer.MeasureText(c.Text, this.Font, proposed, TextFormatFlags.NoPrefix);

                            if((int)size.Width + extraWidth > commonWidth)
                                commonWidth = (int)size.Width + extraWidth;
                        }
                        else
                            commonWidth = extraWidth;
                }

            switch(layoutMethod)
            {
                case LayoutMethod.SingleColumn:
                    LayoutSingleColumn(itemHeight, commonWidth);
                    break;

                case LayoutMethod.SingleRow:
                    LayoutSingleRow(itemHeight, extraWidth, commonWidth);
                    break;

                case LayoutMethod.DownThenAcross:
                    LayoutDownThenAcross(itemHeight, extraWidth, commonWidth);
                    break;

                default:
                    LayoutAcrossThenDown(itemHeight, extraWidth, commonWidth);
                    break;
            }

            // Enforce the default selection
            if(this.SelectedIndex == -1 && this.EnforceDefaultSelection && !base.DesignMode)
                if(this.DefaultSelection < this.Items.Count)
                    this.SelectedIndex = this.DefaultSelection;
                else
                    this.SelectedIndex = this.Items.Count - 1;

            // Make sure the focused button is visible and selected
            if(this.SelectedIndex != -1 && this.SelectedIndex < pnlButtons.Controls.Count)
            {
                Control c = pnlButtons.Controls[this.SelectedIndex];
                pnlButtons.ScrollControlIntoView(c);
            }

            base.OnLayout(levent);
        }

        /// <summary>
        /// This is overridden to invalidate the control when it gains focus
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            base.Invalidate(true);
        }

        /// <summary>
        /// This is overridden to invalidate the control when it loses focus
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnLeave(EventArgs e)
        {
            base.Invalidate(true);
            base.OnLeave(e);
        }

        /// <summary>
        /// This is overridden to adjust the layout when the font changes
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.PerformLayout();
            base.OnFontChanged(e);
        }
        #endregion
    }
}
