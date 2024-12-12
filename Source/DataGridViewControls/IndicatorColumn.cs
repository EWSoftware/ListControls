//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : IndicatorColumn.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/09/2024
// Note    : Copyright 2007-2024, Eric Woodruff, All rights reserved
//
// This file contains a data grid view column object that contains IndicatorCell objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 05/11/2007  EFW  Created the code
//===============================================================================================================

using System.Globalization;

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This data grid view column contains <see cref="IndicatorCell"/> objects
    /// </summary>
    [ToolboxBitmap(typeof(IndicatorColumn), "IndicatorColumn.bmp")]
    public class IndicatorColumn : DataGridViewImageColumn
    {
        #region Private data members
        //=====================================================================

        private ImageList? images;
        private Bitmap? cellImage;

        private List<bool> imageStates = null!;

        private Cursor? originalCursor;

        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This property is not used and is hidden in the designer
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
          EditorBrowsable(EditorBrowsableState.Never)]
        public new Image Image
        {
            get => base.Image;
            set => base.Image = value;
        }

        /// <summary>
        /// This property is not used and is hidden in the designer
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
          EditorBrowsable(EditorBrowsableState.Never)]
        public new DataGridViewImageCellLayout ImageLayout
        {
            get => base.ImageLayout;
            set { }
        }

        /// <summary>
        /// This is used to get or set the image list used for the column's cells
        /// </summary>
        [Category("Behavior"), DefaultValue(null), Description("The image list to use for the column")]
        public ImageList ?ImageList
        {
            get => images;
            set
            {
                if(images != value)
                {
                    EventHandler recreateHandle = this.ImageList_RecreateHandle;
                    EventHandler disposeList = this.ImageList_Disposed;

                    if(images != null)
                    {
                        images.RecreateHandle -= recreateHandle;
                        images.Disposed -= disposeList;
                    }

                    images = value;
                    cellImage = null;

                    if(images != null)
                    {
                        images!.RecreateHandle += recreateHandle;
                        images.Disposed += disposeList;
                    }

                    if(this.DataGridView != null)
                        DataGridViewHelper.OnColumnCommonChange(this.DataGridView, this.Index);
                }
            }
        }

        /// <summary>
        /// This is used to specify the horizontal spacing between images
        /// </summary>
        /// <value>By default, there is no spacing between images</value>
        [Category("Appearance"), DefaultValue(0), Description("The horizontal spacing between images")]
        public int ImageSpacing { get; set; }

        /// <summary>
        /// This is used to indicate that the bound field represents a set of bit flags used to indicate the
        /// on/off states of each image.
        /// </summary>
        /// <value>The column must be bound to a 16-bit or 32-bit integer for this to be used.  If set to false,
        /// the default, the <see cref="MapValueToIndicators"/> event must be used to specify the image states.</value>
        [Category("Data"), DefaultValue(false), Description("Indicate that the bound field specifies a set of " +
          "bit flags to use for the image states")]
        public bool IsBitFlags { get; set; }

        /// <summary>
        /// This is used to get or set whether the images are clickable and will raise the
        /// <see cref="IndicatorClicked"/> event.
        /// </summary>
        /// <value>If true, the default, the images will raise the <see cref="IndicatorClicked"/> event and will
        /// display the hand cursor as the mouse moves over each image.  If false, the event is not raised when
        /// an image is clicked and the mouse cursor is not changed.</value>
        [Category("Behavior"), DefaultValue(true), Description("Indicates whether or not to raise the " +
              "IndicatorClicked event when an image is clicked")]
        public bool IsClickable
        {
            get
            {
                if(base.CellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return ((IndicatorCell)base.CellTemplate).IsClickable;
            }
            set
            {
                DataGridViewRowCollection rows;
                int count, idx;

                var cell = (IndicatorCell)base.CellTemplate ??
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                if(cell.IsClickable != value)
                {
                    cell.IsClickable = value;

                    if(this.DataGridView != null)
                    {
                        rows = this.DataGridView.Rows;
                        count = rows.Count;

                        for(idx = 0; idx < count; idx++)
                        {
                            if(rows.SharedRow(idx).Cells[this.Index] is IndicatorCell c)
                                c.IsClickable = value;
                        }
                    }
                }
            }
        }
        #endregion

        #region Events
        //=====================================================================

        /// <summary>
        /// This event is raised when a cell needs to map a cell value to a
        /// set of image states.
        /// </summary>
        [Category("Behavior"), Description("Occurs when a cell needs to map a cell value to a set of image state")]
        public event EventHandler<MapIndicatorEventArgs>? MapValueToIndicators;

        /// <summary>
        /// This raises the <see cref="MapValueToIndicators"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnMapValueToIndicators(MapIndicatorEventArgs e)
        {
            MapValueToIndicators?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when an image in the cell is clicked
        /// </summary>
        [Category("Behavior"), Description("Occurs when an image in the cell is clicked")]
        public event EventHandler<IndicatorClickEventArgs>? IndicatorClicked;

        /// <summary>
        /// This raises the <see cref="IndicatorClicked"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnIndicatorClicked(IndicatorClickEventArgs e)
        {
            if(IndicatorClicked != null)
            {
                // Reset the cursor.  If the user goes to another form, the cursor doesn't get reset after
                // returning to the one containing the grid view.
                if(this.DataGridView!.Cursor != originalCursor)
                    this.DataGridView.Cursor = originalCursor;

                IndicatorClicked.Invoke(this, e);
            }
        }
        #endregion

        #region Private methods
        //=====================================================================

        /// <summary>
        /// This returns the original data grid view cursor
        /// </summary>
        /// <remarks>This has to be at the column level as cells don't always get the correct cursor due to the
        /// way they are managed internally by the grid.</remarks>
        internal Cursor OriginalCursor
        {
            get
            {
                if(originalCursor == null && this.DataGridView != null)
                    originalCursor = this.DataGridView.Cursor;

                return originalCursor ?? Cursors.Default;
            }
        }

        /// <summary>
        /// Invalidate the column when the image list handle is recreated
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void ImageList_RecreateHandle(object? sender, EventArgs e)
        {
            this.DataGridView?.InvalidateColumn(this.Index);
        }

        /// <summary>
        /// Detach the image list when it is disposed
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void ImageList_Disposed(object? sender, EventArgs e)
        {
            this.ImageList = null;
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>The value type is <see cref="Object"/>.  The bits in a 16-bit or 32-bit integer value can be
        /// used as the on/off states for each image in the image list.</remarks>
        public IndicatorColumn()
        {
            this.ValueType = typeof(object);
            this.CellTemplate = new IndicatorCell();
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is used to clone the column
        /// </summary>
        /// <returns>A clone of the column</returns>
        public override object Clone()
        {
            IndicatorColumn clone = (IndicatorColumn)base.Clone();
            clone.ImageList = images;
            clone.ImageSpacing = this.ImageSpacing;
            clone.IsBitFlags = this.IsBitFlags;
            clone.IsClickable = this.IsClickable;

            return clone;
        }

        /// <summary>
        /// Convert the column to its string description
        /// </summary>
        /// <returns>A string description of the column</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "IndicatorColumn {{ Name={0}, Index={1} }}",
                base.Name, this.Index);
        }

        /// <summary>
        /// This is used to draw the cell image on behalf of the cell
        /// </summary>
        /// <param name="value">The current cell value</param>
        /// <param name="rowIndex">The cell's row index</param>
        /// <returns>The image to display or null if there is no image</returns>
        protected internal Image? DrawImage(object? value, int rowIndex)
        {
            if(images == null)
                return null;

            int cellValue, bit, maxBit, idx, x = 0, count = images.Images.Count, width = images.ImageSize.Width;

            // Create the cell bitmap on first use
            if(cellImage == null)
            {
                cellImage = new Bitmap(((width + this.ImageSpacing) * count) - this.ImageSpacing,
                    images.ImageSize.Height);
                imageStates = [];
            }

            // Reset the current states.  Assume they are on by default.
            for(idx = 0; idx < count; idx++)
            {
                if(idx < imageStates.Count - 1)
                    imageStates[idx] = true;
                else
                    imageStates.Add(true);
            }

            // If the underlying value is a set of bit flags, use them to set the image state
            if(this.IsBitFlags && (value is int || value is short))
            {
                if(value is int iv)
                {
                    cellValue = iv;
                    maxBit = 32;
                }
                else
                {
                    cellValue = (short)value;
                    maxBit = 16;
                }

                // Don't exceed the number of images in the list
                if(maxBit > count)
                    maxBit = count;

                for(idx = 0, bit = 1; idx < maxBit; idx++, bit <<= 1)
                {
                    if((cellValue & bit) == 0)
                        imageStates[idx] = false;
                }
            }

            MapIndicatorEventArgs e = new(this.Index, rowIndex, value, imageStates);
            OnMapValueToIndicators(e);

            using(Graphics g = Graphics.FromImage(cellImage))
            {
                // Draw each image using the appropriate state
                for(idx = 0; idx < count; idx++, x += width + this.ImageSpacing)
                {
                    if(imageStates[idx])
                        g.DrawImage(images.Images[idx], x, 0);
                    else
                        ControlPaint.DrawImageDisabled(g, images.Images[idx], x, 0, Color.Transparent);
                }
            }

            return cellImage;
        }
        #endregion
    }
}
