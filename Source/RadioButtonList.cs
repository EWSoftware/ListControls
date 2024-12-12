//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : RadioButtonList.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains a single-selection radio button list that supports data
// binding, layout options, and data source indexers.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/17/2005  EFW  Created the code
// 05/01/2006  EFW  Added support for UseMnemonic
//===============================================================================================================

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This control is a single-selection radio button list that supports data binding, layout options, and data
    /// source indexers.
    /// </summary>
    [Description("A single selection radio button list that supports data binding, layout options, and data " +
      "source indexers")]
    public class RadioButtonList : BaseButtonList
    {
        #region Private data members
        //====================================================================

        private bool inSelectedIndex;

        #endregion

        #region Properties
        //====================================================================

        /// <summary>
        /// Gets or sets the index specifying the currently selected item
        /// </summary>
        /// <value><para>This is a zero-based index into the items collection.  A value of -1 indicates that
        /// there is no current selection.</para>
        /// 
        /// <para>Setting a new index value will raise the <see cref="BaseListControl.SelectedIndexChanged"/>
        /// event.</para>
        /// 
        /// <para>If <see cref="BaseListControl.EnforceDefaultSelection"/> is true, a <see cref="SelectedIndex"/>
        /// of -1 (no selection) is not allowed.  Instead, the index specified by the
        /// <see cref="BaseListControl.DefaultSelection"/> property is used instead.  If the default value is
        /// outside the range of the data source, the last item is selected.</para></value>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index is less than -1 or greater
        /// than the number of items in the collection.</exception>
        public override int SelectedIndex
        {
            get => base.SelectedIndex;
            set
            {
                RadioButton rb;
                int oldValue;

                if(base.SelectedIndex != value)
                {
                    if(inSelectedIndex)
                        return;

                    if(value < -1 || value >= this.Items.Count)
                        throw new ArgumentOutOfRangeException(nameof(value), value, LR.GetString("ExItemIndexOutOfRange"));

                    // If a default selection is being enforced, use it as long as it is valid
                    if(value == -1 && this.EnforceDefaultSelection)
                    {
                        if(this.DefaultSelection < this.Items.Count && (this.DataManager == null ||
                          this.DataManager.Count != 0))
                        {
                            value = this.DefaultSelection;
                        }
                        else
                        {
                            if(this.DataManager != null)
                                value = this.DataManager.Count - 1;
                            else
                                value = this.Items.Count - 1;
                        }
                    }

                    inSelectedIndex = true;
                    oldValue = base.SelectedIndex;
                    base.SelectedIndex = value;

                    this.OnSelectedItemChanged(EventArgs.Empty);
                    this.OnSelectedValueChanged(EventArgs.Empty);
                    this.OnSelectedIndexChanged(EventArgs.Empty);

                    // Give focus to the selected radio button or clear the selected radio button's checked state
                    if(!this.IsInitializing)
                    {
                        if(value != -1)
                        {
                            rb = (RadioButton)this.ButtonPanel.Controls[value];
                            this.ButtonPanel.ScrollControlIntoView(rb);
                            rb.Checked = true;
                        }
                        else
                        {
                            if(oldValue != -1 && oldValue < this.Items.Count)
                            {
                                rb = (RadioButton)this.ButtonPanel.Controls[oldValue];
                                rb.Checked = false;
                            }
                        }
                    }

                    inSelectedIndex = false;
                }
            }
        }

        /// <summary>
        /// This is used to set or get the radio button appearance
        /// </summary>
        /// <value>The default is to show them as normal radio buttons.</value>
        public override Appearance Appearance
        {
            get => base.Appearance;
            set
            {
                foreach(Control c in this.ButtonPanel.Controls)
                    ((RadioButton)c).Appearance = value;

                base.Appearance = value;
            }
        }

        /// <summary>
        /// This is used to set or get the alignment on the radio button checkmark
        /// </summary>
        /// <value>The default alignment is <c>MiddleLeft</c></value>
        public override ContentAlignment CheckAlign
        {
            get => base.CheckAlign;
            set
            {
                foreach(Control c in this.ButtonPanel.Controls)
                    ((RadioButton)c).CheckAlign = value;

                base.CheckAlign = value;
            }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>By default, the radio button list will display all items in the data source in a single
        /// column, item parts are left aligned and the control will have an etched border.  A default selection
        /// (item zero) is enforced.</remarks>
        public RadioButtonList()
        {
        }
        #endregion

        #region Methods
        //=====================================================================

        /// <summary>
        /// This is handled to update the selected index whenever a radio button's checked state changes
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void RadioButton_CheckedChanged(object? sender, EventArgs e)
        {
            if(sender is RadioButton rb && rb.Checked)
                this.SelectedIndex = this.ButtonPanel.Controls.IndexOf(rb);
        }

        /// <summary>
        /// This is called to force the control to refresh the radio buttons with information from the data
        /// source.
        /// </summary>
        public override void RefreshSubControls()
        {
            Panel bp = this.ButtonPanel;
            RadioButton rb;
            int imageIdx = 0;

            ControlCollection cc = bp.Controls;
            EventHandler CheckedChanged = RadioButton_CheckedChanged;

            // Dispose of any prior radio buttons
            while(cc.Count > 0)
            {
                rb = (RadioButton)cc[0];
                rb.CheckedChanged -= CheckedChanged;
                cc.RemoveAt(0);
                rb.Dispose();
            }

            foreach(object oItem in this.Items)
            {
                rb = new RadioButton();

                // In .NET 2.0, we could use the UseMnemonic property but it doesn't work with FlatStyle.System
                // so we'll modify the text which works with it and with .NET 1.1.
                if(this.UseMnemonic)
                    rb.Text = this.GetItemText(oItem)?.Replace("&&", "&");
                else
                    rb.Text = this.GetItemText(oItem)?.Replace("&", "&&");

                rb.Appearance = this.Appearance;
                rb.FlatStyle = this.FlatStyle;
                rb.CheckAlign = this.CheckAlign;
                rb.TextAlign = this.TextAlign;
                rb.ImageAlign = this.ImageAlign;
                rb.ImageList = this.ImageList;

                // Don't hook up the event in design mode.  They are clickable.
                if(!this.DesignMode)
                    rb.CheckedChanged += CheckedChanged;

                if(this.ImageList != null)
                {
                    rb.ImageIndex = imageIdx;
                    imageIdx++;

                    // Wrap the index if there are more items than images
                    if(imageIdx == this.ImageList.Images.Count)
                        imageIdx = 0;
                }

                bp.Controls.Add(rb);
            }

            // Restore the currently selected item if necessary
            if(this.SelectedIndex != -1 && this.SelectedIndex < bp.Controls.Count)
                ((RadioButton)bp.Controls[this.SelectedIndex]).Checked = true;

            base.RefreshSubControls();
            this.PerformLayout();
        }
        #endregion
    }
}
