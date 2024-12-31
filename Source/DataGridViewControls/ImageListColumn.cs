//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : ImageListColumn.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/09/2024
// Note    : Copyright 2007-2024, Eric Woodruff, All rights reserved
//
// This file contains a data grid view column object that contains ImageListCell objects
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 05/03/2007  EFW  Created the code
//===============================================================================================================

namespace EWSoftware.ListControls.DataGridViewControls
{
    /// <summary>
    /// This data grid view column contains <see cref="ImageListCell"/> objects
    /// </summary>
    public class ImageListColumn : DataGridViewImageColumn
    {
        #region Private data members
        //=====================================================================

        private ImageList? images;
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
        /// This is used to get or set the image list used for the column's cells
        /// </summary>
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

        /// <summary>
        /// This is used to get or set the image to display for null cells
        /// </summary>
        /// <value>If not set and <see cref="NullImageIndex"/> is set to -1, the cell will appear blank.  This
        /// property is ignored if <c>NullImageIndex</c> is set to a value other than -1.</value>
        [Category("Behavior"), DefaultValue(null), Description("The image to use for null values (none for blank)")]
        public Image? NullImage { get; set; }

        /// <summary>
        /// This is used to get or set the image index to use for null values
        /// </summary>
        /// <value>If set to -1 (the default), the <see cref="NullImage"/> is used if it has been set.  If it is
        /// not set, the cell will appear blank.  If this is set to a value other than -1, the indicated image
        /// from the image list is used for null values.</value>
        [Category("Behavior"), DefaultValue(-1), Description("The image index to use for null values (-1 for none)")]
        public int NullImageIndex { get; set; }

        /// <summary>
        /// This is used to get or set the image index to show for the new row cells
        /// </summary>
        /// <value>If set to -1 (the default), new rows will show the <see cref="NullImage"/> if it has been set
        /// or a blank cell if not.  If set to a value other than -1, the image at the specified index will be
        /// shown in the new row.</value>
        [Category("Behavior"), DefaultValue(-1), Description("The image index to show in the new row (-1 = none)")]
        public int NewRowImageIndex
        {
            get
            {
                if(base.CellTemplate == null)
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                return ((ImageListCell)base.CellTemplate).NewRowImageIndex;
            }
            set
            {
                DataGridViewRowCollection rows;
                int count, idx;

                var cell = (ImageListCell)base.CellTemplate ??
                    throw new InvalidOperationException(LR.GetString("ExCellTemplateRequired"));

                if(cell.NewRowImageIndex != value)
                {
                    cell.NewRowImageIndex = value;

                    if(this.DataGridView != null)
                    {
                        rows = this.DataGridView.Rows;
                        count = rows.Count;

                        for(idx = 0; idx < count; idx++)
                        {
                            if(rows.SharedRow(idx).Cells[this.Index] is ImageListCell c)
                                c.NewRowImageIndex = value;
                        }
                    }
                }
            }
        }
        #endregion

        #region Events
        //=====================================================================

        /// <summary>
        /// This event is raised when a cell needs to map a cell value to an image index
        /// </summary>
        [Category("Behavior"), Description("Occurs when a cell needs to map a cell value to an image index")]
        public event EventHandler<MapIndexEventArgs>? MapValueToIndex;

        /// <summary>
        /// This raises the <see cref="MapValueToIndex"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnMapValueToIndex(MapIndexEventArgs e)
        {
            MapValueToIndex?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when a cell needs to map an image index to a cell value
        /// </summary>
        [Category("Behavior"), Description("Occurs when a cell needs to map an image index to a cell value")]
        public event EventHandler<MapIndexEventArgs>? MapIndexToValue;

        /// <summary>
        /// This raises the <see cref="MapIndexToValue"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected internal virtual void OnMapIndexToValue(MapIndexEventArgs e)
        {
            MapIndexToValue?.Invoke(this, e);
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
        /// <remarks>The value type is <see cref="Object"/>.  16-bit and 32-bit integer values are assumed to be
        /// actual index values unless mapped to a different index.  Boolean values are mapped to 0 (false) and 1
        /// (true) unless mapped to a different index.  All other types must be mapped to an index using the
        /// <see cref="MapValueToIndex"/> event.</remarks>
        public ImageListColumn()
        {
            this.ValueType = typeof(object);
            this.CellTemplate = new ImageListCell();
            this.NullImageIndex = -1;
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
            ImageListColumn clone = (ImageListColumn)base.Clone();
            
            clone.ImageList = images;
            clone.NullImage = this.NullImage;
            clone.NullImageIndex = this.NullImageIndex;
            clone.NewRowImageIndex = this.NewRowImageIndex;

            return clone;
        }

        /// <summary>
        /// Convert the column to its string description
        /// </summary>
        /// <returns>A string description of the column</returns>
        public override string ToString()
        {
            return $"ImageListColumn {{ Name={this.Name}, Index={this.Index} }}";
        }
        #endregion
    }
}
