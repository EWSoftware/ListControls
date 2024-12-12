//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : RatingColumn.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2007-2024, Eric Woodruff, All rights reserved
//
// This file contains a data grid view column object that contains RatingCell objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 06/04/2007  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This data grid view column contains <see cref="RatingCell"/> objects
    /// </summary>
    [ToolboxBitmap(typeof(RatingColumn), "RatingColumn.bmp")]
    public class RatingColumn : DataGridViewImageColumn
    {
        #region Private data members
        //=====================================================================

        // The path to the embedded resources
        private const string ResourcePath = "EWSoftware.ListControls.Images.";

        // The internal image list
        private static readonly ImageList ilStars;

        // The cell image and custom image list
        private Bitmap? cellImage;
        private ImageList? images;

        private int maxRating;

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
        /// This gets or sets the maximum rating and thus the of images drawn in the cells
        /// </summary>
        /// <value>The default is five.</value>
        [Category("Appearance"), DefaultValue(5), Description("The maximum rating and thus images drawn in the cells")]
        public int MaximumRating
        {
            get => maxRating;
            set
            {
                maxRating = value;
                cellImage?.Dispose();
                cellImage = null;

                if(this.DataGridView != null)
                    DataGridViewHelper.OnColumnCommonChange(this.DataGridView, this.Index);
            }
        }

        /// <summary>
        /// This is used to get or set the image list used for the column's cells
        /// </summary>
        /// <value>If not set, a default set of star images is used.  If set, the image list should contain
        /// three images.  The first is for an empty/unused image, the second is for a filled/used image, and
        /// the third is for a hot image drawn when the mouse is over the cell images.</value>
        [Category("Behavior"), DefaultValue(null), Description("The image list to use for the column")]
        public ImageList? ImageList
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
                    cellImage?.Dispose();
                    cellImage = null;

                    if(images != null)
                    {
                        images.RecreateHandle += recreateHandle;
                        images.Disposed += disposeList;
                    }

                    if(this.DataGridView != null)
                        DataGridViewHelper.OnColumnCommonChange(this.DataGridView, this.Index);
                }
            }
        }
        #endregion

        #region Events
        //=====================================================================

        /// <summary>
        /// This event is raised when a cell needs to map a cell value to a rating
        /// </summary>
        [Category("Behavior"), Description("Occurs when a cell needs to map a cell value to a rating")]
        public event EventHandler<MapRatingEventArgs>? MapValueToRating;

        /// <summary>
        /// This raises the <see cref="MapValueToRating"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnMapValueToRating(MapRatingEventArgs e)
        {
            MapValueToRating?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when a cell needs to map a rating to a cell value
        /// </summary>
        [Category("Behavior"), Description("Occurs when a cell needs to map a rating to a cell value")]
        public event EventHandler<MapRatingEventArgs>? MapRatingToValue;

        /// <summary>
        /// This raises the <see cref="MapRatingToValue"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnMapRatingToValue(MapRatingEventArgs e)
        {
            MapRatingToValue?.Invoke(this, e);
        }
        #endregion

        #region Private methods
        //=====================================================================

        /// <summary>
        /// Static constructor.  This loads the default images.
        /// </summary>
        static RatingColumn()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            ilStars = new ImageList { ImageSize = new Size(14, 14) };

            ilStars.Images.Add(new Bitmap(asm.GetManifestResourceStream(ResourcePath + "RatingStarEmpty.png")!),
                Color.Magenta);
            ilStars.Images.Add(new Bitmap(asm.GetManifestResourceStream(ResourcePath + "RatingStarFilled.png")!),
                Color.Magenta);
            ilStars.Images.Add(new Bitmap(asm.GetManifestResourceStream(ResourcePath + "RatingStarHot.png")!),
                Color.Magenta);
        }

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
        /// Return the user-supplied image list if not null or the default image list if it is null
        /// </summary>
        internal ImageList ImageListInternal => images ?? ilStars;

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
        /// <remarks>The value type is <see cref="Object"/>.  16-bit and 32-bit integer values are assumed to be
        /// actual rating values between zero and <see cref="MaximumRating" /> unless mapped to a different
        /// rating value.  All other types must be mapped to a rating using the <see cref="MapValueToRating"/>
        /// event.</remarks>
        public RatingColumn()
        {
            this.ValueType = typeof(object);
            this.CellTemplate = new RatingCell();

            maxRating = 5;
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
            RatingColumn clone = (RatingColumn)base.Clone();
            clone.ImageList = images;
            clone.MaximumRating = maxRating;

            return clone;
        }

        /// <summary>
        /// Convert the column to its string description
        /// </summary>
        /// <returns>A string description of the column</returns>
        public override string ToString()
        {
            return $"RatingColumn {{ Name={this.Name}, Index={this.Index} }}";
        }

        /// <summary>
        /// This is used to draw the cell image on behalf of the cell
        /// </summary>
        /// <param name="value">The current cell value</param>
        /// <param name="rowIndex">The cell's row index</param>
        /// <param name="mouseIndex">The index under the mouse or -1 if the mouse isn't over an index</param>
        /// <returns>The image to display or null if there is no image</returns>
        protected internal Image DrawImage(object? value, int rowIndex, int mouseIndex)
        {
            ImageList imageList = this.ImageListInternal;

            int idx, imageIdx, x = 0, cellValue = 0, width = imageList.ImageSize.Width;

            // Create the cell bitmap on first use
            cellImage ??= new Bitmap(width * maxRating, imageList.ImageSize.Height);

            if(value is int iv)
                cellValue = iv;
            else
            {
                if(value is short sv)
                    cellValue = sv;
            }

            // Let the user map the value to a rating
            MapRatingEventArgs mapArgs = new(this.Index, rowIndex, value, cellValue);
            this.OnMapValueToRating(mapArgs);
            cellValue = mapArgs.Rating;

            if(cellValue < 0)
                cellValue = 0;
            else
                if(cellValue > maxRating)
                    cellValue = maxRating;

            using(Graphics g = Graphics.FromImage(cellImage))
            {
                // Draw each image using the appropriate state
                for(idx = 0; idx < maxRating; idx++, x += width)
                {
                    if(mouseIndex != -1)
                    {
                        if(idx <= mouseIndex)
                            imageIdx = 2;   // Hot
                        else
                            imageIdx = 0;   // Empty
                    }
                    else
                        if(idx < cellValue)
                            imageIdx = 1;   // Filled
                        else
                            imageIdx = 0;   // Empty

                    g.DrawImage(imageList.Images[imageIdx], x, 0);
                }
            }

            return cellImage;
        }
        #endregion
    }
}
