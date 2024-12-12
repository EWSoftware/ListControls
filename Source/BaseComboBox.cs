//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : BaseComboBox.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/10/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This file contains an abstract base class for use in creating the MultiColumnComboBox and UserControlComboBox
// controls.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 09/09/2005  EFW  Created the code
// 12/12/2005  EFW  Added support for Windows XP themes
// 09/22/2010  EFW  Added designer to show snap lines
//===============================================================================================================

using EWSoftware.ListControls.Design;
using EWSoftware.ListControls.UnsafeNative;

#pragma warning disable CA2216

namespace EWSoftware.ListControls
{
    /// <summary>
    /// This is an abstract base class for use in creating the <see cref="MultiColumnComboBox"/> and
    /// <see cref="UserControlComboBox"/> controls.
    /// </summary>
    [Designer(typeof(BaseComboBoxDesigner))]
    public abstract class BaseComboBox : BaseListControl
    {
        #region Combo box text control
        //=====================================================================

        /// <summary>
        /// This is a custom text box class that is hosted within the user control to act as the text box portion
        /// of the combo box.
        /// </summary>
        internal sealed class ComboTextBox : TextBox
        {
            private readonly BaseComboBox owner;

            /// <summary>
            /// Constructor
            /// </summary>
            public ComboTextBox(BaseComboBox cb)
            {
                owner = cb;
                this.BorderStyle = BorderStyle.None;
                this.Text = String.Empty;
            }

            /// <summary>
            /// This is overridden to handle certain keys differently for the combo box
            /// </summary>
            /// <param name="e">The event arguments</param>
            protected override void OnKeyDown(KeyEventArgs e)
            {
                e.Handled = owner.HandleKeys(e.KeyData);
                base.OnKeyDown(e);
            }

            /// <summary>
            /// This is overridden to allow auto-completion to occur
            /// </summary>
            protected override void OnKeyUp(KeyEventArgs e)
            {
                base.OnKeyUp(e);

                if(owner.AllowAutoCompletion)
                {
                    // Clear SelectedIndex on Backspace or Delete
                    if(e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                    {
                        owner.AutoCompleteBackspace();
                        return;
                    }

                    // Auto-complete if it's an appropriate key
                    if(((Char.IsLetterOrDigit((char)((ushort)e.KeyCode)) &&
                      (e.KeyCode < Keys.F1 || e.KeyCode > Keys.F24)) ||
                      (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.Divide) ||
                      (e.KeyCode >= Keys.OemSemicolon && e.KeyCode <= Keys.OemBackslash) ||
                      (e.KeyCode == Keys.Space && !e.Shift)) && !e.Shift && !e.Control && !e.Alt)
                        owner.AutoComplete();
                }
            }
        }
        #endregion

        #region Private data members
        //====================================================================

        // This is used to size the drop down button
        internal static int DropDownButtonWidth = SystemInformation.VerticalScrollBarWidth;

        // The textbox control
        internal ComboTextBox txtValue;

        // Layout information
        private Rectangle rectBackground, rectBorder, rectButton, rectButtonBorder, rectImage, rectText;
        private readonly Point[] arrow = new Point[3];

        // Combo box state information
        private ComboBoxStyle dropDownStyle;

        private int maxDropDownItems, simpleHeight;
        private string autoCompleteText;
        private bool autoCompleteEnabled, settingText, drawImage, inDroppedDown;

        // The drop-down list and its properties
        private Font dropDownFont;
        private Color dropDownBackColor;

        // Windows XP theme support
        private IntPtr hTheme;
        private bool drawThemed;
        private int themeState;

        #endregion

        #region Properties
        //====================================================================

        /// <summary>
        /// This is used to set or get whether auto-completion is enabled
        /// </summary>
        /// <remarks>The default is true.  As the user types in the textbox portion of the control, the best
        /// match is selected from the data source as the selected item.</remarks>
        [Category("Behavior"), DefaultValue(true), Bindable(true),
          Description("This determines whether or not auto-completion is enabled")]
        public bool AllowAutoCompletion
        {
            get => autoCompleteEnabled;
            set
            {
                autoCompleteEnabled = value;
                autoCompleteText = String.Empty;
                OnAllowAutoCompletionChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// This is used to set or get the background color of the text portion of the combo box control
        /// </summary>
        [DefaultValue(typeof(Color), "Window")]
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;

                if(txtValue != null)
                    txtValue.BackColor = value;
            }
        }

        /// <summary>
        /// This is used to set or get the foreground color of the text portion of the combo box control
        /// </summary>
        [DefaultValue(typeof(Color), "WindowText")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set
            {
                base.ForeColor = value;

                if(txtValue != null)
                    txtValue.ForeColor = value;
            }
        }

        /// <summary>
        /// This is used to set or get the text in the textbox portion of the combo box control
        /// </summary>
        /// <remarks>When set to null, the <see cref="SelectedIndex"/> is set to -1.  If not null, an attempt is
        /// made to set the selected index to the item matching the specified text.  If no match is found, the
        /// selected index is set to -1.</remarks>
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get => txtValue.Text;
            set
            {
                settingText = true;
                base.Text = txtValue.Text = value;
                settingText = false;
            }
        }

        /// <summary>
        /// Gets or sets whether or not the combo box has an image drawn to the left of the text value
        /// </summary>
        /// <value>If true, the combo box raises the <see cref="DrawItemImage"/> event so that an image can be
        /// drawn to the left of the text box.</value>
        [Category("Appearance"), DefaultValue(false), Bindable(true), RefreshProperties(RefreshProperties.Repaint),
          Description("Set to true to raise the DrawItemImage event")]
        public bool DrawImage
        {
            get => drawImage;
            set
            {
                if(drawImage != value)
                {
                    drawImage = value;
                    this.PerformLayout();
                    this.OnDrawImageChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value specifying the style of the combo box
        /// </summary>
        /// <value>The default is the <c>DropDown</c> style with an editable textbox area and a drop down list</value>
        /// <exception cref="InvalidEnumArgumentException">This is thrown if the value is not one of the valid
        /// combo box styles</exception>
        [Category("DropDown"), DefaultValue(ComboBoxStyle.DropDown), RefreshProperties(RefreshProperties.Repaint),
          Description("This controls the appearance and functionality of the combo box")]
        public ComboBoxStyle DropDownStyle
        {
            get => dropDownStyle;
            set
            {
                if(dropDownStyle != value)
                {
                    if(!Enum.IsDefined(typeof(ComboBoxStyle), value))
                        throw new InvalidEnumArgumentException("value", (int)value, typeof(ComboBoxStyle));

                    // Adjust height based on setting.  Also add or dispose of the drop-down for simple mode as
                    // needed.
                    if(value == ComboBoxStyle.Simple)
                    {
                        dropDownStyle = value;
                        this.Height = simpleHeight;
                        this.RefreshSubControls();
                    }
                    else
                    {
                        if(dropDownStyle == ComboBoxStyle.Simple)
                        {
                            dropDownStyle = value;
                            simpleHeight = this.Height;
                            this.RefreshSubControls();
                        }
                        else
                            dropDownStyle = value;
                    }

                    autoCompleteText = String.Empty;

                    // We draw the text in DropDownList mode
                    txtValue.Visible = !(dropDownStyle == ComboBoxStyle.DropDownList);
                    this.UpdateText();
                    this.PerformLayout();
                    this.OnDropDownStyleChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// This is used to set or get the background color of the drop-down portion of the combo box control
        /// </summary>
        [Category("DropDown"), Bindable(true), DefaultValue(typeof(Color), "Control"),
          Description("The background color to use in the drop-down portion of the combo box")]
        public Color DropDownBackColor
        {
            get => dropDownBackColor;
            set
            {
                dropDownBackColor = value;
                OnDropDownBackColorChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// This property is used to set or get the drop-down's font
        /// </summary>
        [Category("DropDown"), Bindable(true),
          Description("The font to use in the drop-down portion of the combo box")]
        public Font DropDownFont
        {
            get => dropDownFont;
            set
            {
                if(value == null)
                    dropDownFont = new Font(Control.DefaultFont, Control.DefaultFont.Style);
                else
                    dropDownFont = value;

                OnDropDownFontChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the combo box is displaying its drop-down portion
        /// </summary>
        /// <value>Returns true if the drop-down is currently displayed or false if not.  If set to true, the
        /// drop down portion is displayed if not already visible and the <see cref="DropDown"/> event is raised.
        /// If set to false, the drop down is hidden if it is currently visible and the <see cref="CloseUp"/>
        /// event is raised.  After closing, the <see cref="SelectionChangeCommitted"/> or
        /// <see cref="SelectionChangeCanceled" /> event is raised depending on whether or not the selected index
        /// changed while the drop-down was visible.  When using the <c>Simple</c> drop-down style, this always
        /// returns true and it cannot be set to false.</value>
        [Browsable(false), Description("Get or set whether or not the drop-down is visible"),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DroppedDown
        {
            get => this.DropDownInterface?.Visible ?? false;
            set
            {
                if(dropDownStyle != ComboBoxStyle.Simple && this.Enabled && this.Visible)
                {
                    inDroppedDown = true;

                    if(value)
                    {
                        if(this.DropDownInterface == null)
                            this.CreateDropDown();

                        this.DropDownInterface!.ShowDropDown();

                        // An odd sequence of events related to updates to bound controls can cause the drop-down
                        // to get disposed.  If that happens, we'll do it again.
                        if(this.DropDownInterface == null)
                        {
                            this.CreateDropDown();
                            this.DropDownInterface!.ShowDropDown();
                        }

                        OnDropDown(EventArgs.Empty);
                    }
                    else
                    {
                        if(this.DropDownInterface?.Visible ?? false)
                        {
                            this.DropDownInterface.Visible = false;
                            OnCloseUp(EventArgs.Empty);

                            // Fire the appropriate event
                            if(this.SelectedIndex != this.DropDownInterface.StartIndex)
                                OnSelectionChangeCommitted(EventArgs.Empty);
                            else
                                OnSelectionChangeCanceled(EventArgs.Empty);
                        }
                    }

                    inDroppedDown = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of items to be shown in the drop-down portion of the combo box when
        /// first displayed.  It also controls how many items are skipped when paging up and down.
        /// </summary>
        /// <value>The value must be between 1 and 100.  The value is only used the first time the drop-down
        /// portion is shown.  The drop-down is resizable and it will remember the last used size until it is
        /// disposed.</value>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the value is not between 1 and 100</exception>
        [Category("DropDown"), DefaultValue(8), Localizable(true), Description("The maximum number of entries " +
            "to display in the drop-down list when first displayed and the page up/down size")]
        public int MaxDropDownItems
        {
            get => maxDropDownItems;
            set
            {
                if(value < 1 || value > 100)
                    throw new ArgumentOutOfRangeException(nameof(value), value, LR.GetString("ExInvalidMaxDropDownItems"));

                maxDropDownItems = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of characters allowed in the editable portion of a combo box
        /// </summary>
        /// <value>The default is 32,767 characters.  Values less than zero are reset to zero.</value>
        [Category("Behavior"), DefaultValue(32767), Localizable(true),
          Description("Specify the maximum number of character that can be entered into the combo box")]
        public int MaxLength
        {
            get => txtValue.MaxLength;
            set
            {
                if(value < 0)
                    value = 0;

                txtValue.MaxLength = value;
            }
        }

        /// <summary>
        /// This property is used to set or get the default selection behavior
        /// </summary>
        /// <value>In <c>DropDownList</c> mode, if this property is true (the default), a <see cref="SelectedIndex"/>
        /// of -1 (no selection) is not allowed.  Instead, the index specified by the <see cref="DefaultSelection"/>
        /// property is used instead.  For the <c>DropDown</c> and <c>Simple</c> modes, this property is ignored
        /// as values can be entered that are not in the list of valid items.</value>
        public override bool EnforceDefaultSelection
        {
            get
            {
                if(dropDownStyle == ComboBoxStyle.DropDownList || this.DesignMode)
                    return base.EnforceDefaultSelection;

                return false;
            }
            set
            {
                base.EnforceDefaultSelection = value;

                if(value && this.SelectedIndex == -1 && dropDownStyle == ComboBoxStyle.DropDownList &&
                  this.Items.Count != 0 && !this.DesignMode)
                    this.SelectedIndex = this.DefaultSelection;
            }
        }

        /// <summary>
        /// This property is used to set or get the default selection's index
        /// </summary>
        /// <value>In <c>DropDownList</c> mode, if <see cref="EnforceDefaultSelection"/> is true, a
        /// <see cref="SelectedIndex"/> of -1 (no selection) is not allowed.  Instead, the index specified by
        /// this property is used instead.  For the <c>DropDown</c> and <c>Simple</c> modes, this property is
        /// ignored as values can be entered that are not in the list of valid items.</value>
        public override int DefaultSelection
        {
            get => base.DefaultSelection;
            set
            {
                base.DefaultSelection = value;

                if(this.EnforceDefaultSelection && this.SelectedIndex == -1 &&
                  dropDownStyle == ComboBoxStyle.DropDownList && this.Items.Count != 0 && !this.DesignMode)
                    this.SelectedIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the index specifying the currently selected item
        /// </summary>
        /// <value><para>This is a zero-based index into the items collection.  A value of -1 indicates that
        /// there is no current selection.</para>
        /// 
        /// <para>Setting a new index value will raise the <see cref="Control.TextChanged"/>,
        /// <see cref="BaseListControl.SelectedItemChanged"/>, and
        /// <see cref="BaseListControl.SelectedIndexChanged"/> events in that order.</para>
        /// 
        /// <para>In <c>DropDownList</c> mode, if <see cref="BaseListControl.EnforceDefaultSelection"/> is
        /// true, a <see cref="SelectedIndex"/> of -1 (no selection) is not allowed.  Instead, the index
        /// specified by the <see cref="BaseListControl.DefaultSelection"/> property is used instead.  If the
        /// default value is outside the range of the data source, the last item is selected.  For the
        /// <c>DropDown</c> and <c>Simple</c> modes, the <c>EnforceDefaultSelection</c> property is ignored as
        /// values can be entered that are not in the list of valid items.</para></value>
        /// <seealso cref="BaseListControl.SelectedIndexChanged"/>
        /// <exception cref="ArgumentOutOfRangeException">This is thrown if the index is less than -1 or greater
        /// than the number of items in the collection.</exception>
        [Browsable(false), Description("The index of the currently selected item in the combo box"),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override int SelectedIndex
        {
            get => base.SelectedIndex;
            set
            {
                if(this.SelectedIndex != value)
                {
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

                    base.SelectedIndex = value;

                    this.UpdateText();
                    this.OnTextChanged(EventArgs.Empty);
                    this.OnSelectedItemChanged(EventArgs.Empty);
                    this.OnSelectedValueChanged(EventArgs.Empty);
                    this.OnSelectedIndexChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the text that is selected in the editable portion of a combo box
        /// </summary>
        /// <value><para>If the <see cref="DropDownStyle"/> is set to <c>DropDownList</c> this always returns the
        /// full text of the selected item.  Attempts to set the selected text in that mode are ignored.</para>
        /// 
        /// <para>If there is no selected text, this property returns a zero-length string.</para></value>
        [Browsable(false), Description("The selected text from the edit portion of the combo box"),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get
            {
                if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                    return this.Text;

                return txtValue.SelectedText;
            }
            set
            {
                if(this.DropDownStyle != ComboBoxStyle.DropDownList)
                {
                    txtValue.Text = value;
                    txtValue.SelectAll();
                }
            }
        }

        /// <summary>
        /// Gets or sets the number of characters selected in the editable portion of the combo box
        /// </summary>
        /// <value>You can use this property to determine whether any characters are currently selected in the
        /// combo box control before performing operations on the selected text. When the value of the property
        /// is set to a value that is larger than the number of characters within the text of the control, the
        /// value of the property is set to the entire length of text within the control minus the value of the
        /// <see cref="SelectionStart"/> property (if any value is specified for it).</value>
        [Browsable(false), Description("Get the length of any selected text in the edit portion of the control"),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get
            {
                if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                    return txtValue.Text.Length;

                return txtValue.SelectionLength;
            }
            set
            {
                if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                    txtValue.SelectAll();
                else
                    txtValue.Select(txtValue.SelectionStart, value);
            }
        }

        /// <summary>
        /// Gets or sets the starting index of text selected in the combo box
        /// </summary>
        /// <value>If no text is selected in the control, this property indicates the insertion point for new
        /// text. If you set this property to a location beyond the length of the text in the control, the
        /// selection start position is placed after the last character. When text is selected in the text box
        /// control, changing this property can change the value of the <see cref="SelectionLength"/> property.
        /// If the remaining text in the control after the position indicated by this property is less than the
        /// value of the <see cref="SelectionLength"/> property, the value of the <see cref="SelectionLength"/>
        /// property is automatically decreased. The value of this property never causes an increase in the
        /// <see cref="SelectionLength"/> property.</value>
        [Browsable(false), Description("Get the starting index of any selected text in the edit portion of the control"),
          DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get
            {
                if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                    return 0;

                return txtValue.SelectionStart;
            }
            set
            {
                if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                    txtValue.SelectAll();
                else
                    txtValue.Select(value, txtValue.SelectionLength);
            }
        }

        /// <summary>
        /// This allows the derived classes access to the drop-down interface
        /// </summary>
        internal IDropDown? DropDownInterface { get; set; }

        #endregion

        #region Events
        //=====================================================================

        /// <summary>
        /// This event is raised when the <see cref="DrawImage"/> property changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the DrawImage property changes")]
        public event EventHandler? DrawImageChanged;

        /// <summary>
        /// This raises the <see cref="DrawImageChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnDrawImageChanged(EventArgs e)
        {
            DrawImageChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="AllowAutoCompletion"/> property changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the auto-completion option changes")]
        public event EventHandler? AllowAutoCompletionChanged;

        /// <summary>
        /// This raises the <see cref="AllowAutoCompletionChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnAllowAutoCompletionChanged(EventArgs e)
        {
            AllowAutoCompletionChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="DropDownBackColor"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the drop-down background color changes")]
        public event EventHandler? DropDownBackColorChanged;

        /// <summary>
        /// This raises the <see cref="DropDownBackColorChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnDropDownBackColorChanged(EventArgs e)
        {
            DropDownBackColorChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the control needs to draw the image to the left of the text value
        /// </summary>
        /// <remarks><para>The event is passed a <see cref="DrawItemEventArgs"/> object.  The following
        /// properties in it are set:</para>
        /// 
        /// <list type="table">
        ///    <listheader>
        ///       <term>Property</term>
        ///       <description>Description</description>
        ///    </listheader>
        ///    <item>
        ///       <term>BackColor</term>
        ///       <description>The background color for the combo box.</description>
        ///    </item>
        ///    <item>
        ///       <term>Bounds</term>
        ///       <description>The bounds of the area for the image.</description>
        ///    </item>
        ///    <item>
        ///       <term>Font</term>
        ///       <description>The font being used by the combo box.</description>
        ///    </item>
        ///    <item>
        ///       <term>ForeColor</term>
        ///       <description>The foreground color for the combo box.</description>
        ///    </item>
        ///    <item>
        ///       <term>Graphics</term>
        ///       <description>The graphics object that can be used to draw
        ///       the image.</description>
        ///    </item>
        ///    <item>
        ///       <term>Index</term>
        ///       <description>The index of the currently selected item.</description>
        ///    </item>
        ///    <item>
        ///       <term>State</term>
        ///       <description>The current item state(s) for the combo box.
        ///       This can be <c>None</c> or one or more of the values
        ///       <c>Disabled</c>, <c>Focus</c>, and/or <c>Selected</c>.
        ///       </description>
        ///    </item>
        /// </list></remarks>
        [Category("Behavior"),
          Description("Occurs when the control needs to draw the image to the left of the text value")]
        public event DrawItemEventHandler? DrawItemImage;

        /// <summary>
        /// This raises the <see cref="DrawItemImage"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <remarks>See <see cref="DrawItemImage"/> for more information</remarks>
        protected virtual void OnDrawItemImage(DrawItemEventArgs e)
        {
            DrawItemImage?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="DropDownStyle"/> changes
        /// </summary>
        [Category("Behavior"), Description("Occurs when the control's drop down style changes")]
        public event EventHandler? DropDownStyleChanged;

        /// <summary>
        /// This raises the <see cref="DropDownStyleChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnDropDownStyleChanged(EventArgs e)
        {
            DropDownStyleChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the selected item has changed in the drop-down list and that change is
        /// committed when the drop-down closes.
        /// </summary>
        /// <remarks>This event always occurs after the <see cref="CloseUp"/> event</remarks>
        [Category("Behavior"), Description("Occurs when the item changes in the drop-down list " +
            "and the change is committed when the drop-down closes")]
        public event EventHandler? SelectionChangeCommitted;

        /// <summary>
        /// This raises the <see cref="SelectionChangeCommitted"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnSelectionChangeCommitted(EventArgs e)
        {
            autoCompleteText = String.Empty;

            SelectionChangeCommitted?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the selected item has not changed in the drop-down list and the drop-down
        /// closes.
        /// </summary>
        /// <remarks>This event always occurs after the <see cref="CloseUp"/> event</remarks>
        [Category("Behavior"), Description("Occurs when the item does not change in the " +
            "drop-down list and the drop-down closes")]
        public event EventHandler? SelectionChangeCanceled;

        /// <summary>
        /// This raises the <see cref="SelectionChangeCanceled"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnSelectionChangeCanceled(EventArgs e)
        {
            autoCompleteText = String.Empty;

            SelectionChangeCanceled?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the drop-down is shown
        /// </summary>
        [Category("Behavior"), Description("Occurs when the drop down is displayed")]
        public event EventHandler? DropDown;

        /// <summary>
        /// This raises the <see cref="DropDown"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnDropDown(EventArgs e)
        {
            DropDown?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the drop-down is closed
        /// </summary>
        [Category("Behavior"), Description("Occurs when the drop down is closed")]
        public event EventHandler? CloseUp;

        /// <summary>
        /// This raises the <see cref="CloseUp"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnCloseUp(EventArgs e)
        {
            CloseUp?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised when the <see cref="DropDownFont"/> changes
        /// </summary>
        [Category("Property Changed"), Description("Occurs when the drop-down font changes")]
        public event EventHandler? DropDownFontChanged;

        /// <summary>
        /// This raises the <see cref="DropDownFontChanged"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnDropDownFontChanged(EventArgs e)
        {
            DropDownFontChanged?.Invoke(this, e);
        }

        /// <summary>
        /// This event is raised during the validation phase if the text in the control does not match an item in
        /// the list.
        /// </summary>
        /// <remarks>This event occurs before the <see cref="Control.Validating"/> event so that you have a
        /// chance to take action on entered text that does not appear in the item list (i.e. add it to the list
        /// or reject it).  Canceling this event is equivalent to canceling the <c>Validating</c> event.  If not
        /// canceled, the <c>Validating</c> event is called as usual.</remarks>
        [Category("Behavior"), Description("Occurs during validation when the text is not in the item list")]
        public event CancelEventHandler? NotInList;

        /// <summary>
        /// This raises the <see cref="NotInList"/> event
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected virtual void OnNotInList(CancelEventArgs e)
        {
            NotInList?.Invoke(this, e);
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseComboBox()
        {
            maxDropDownItems = 8;
            dropDownStyle = ComboBoxStyle.DropDown;
            dropDownBackColor = SystemColors.Control;
            autoCompleteText = String.Empty;
            autoCompleteEnabled = true;
            simpleHeight = 48;
            hTheme = IntPtr.Zero;
            themeState = UnsafeNativeMethods.ComboBoxNormal;

            dropDownFont = new Font(Control.DefaultFont, Control.DefaultFont.Style);

            this.SuspendLayout();

            txtValue = new ComboTextBox(this)
            {
                AutoSize = false,
                Location = new Point(1, 1),
                Size = new Size(128, 15),
                TabIndex = 0
            };

            this.Controls.Add(this.txtValue);
            this.Size = new Size(150, 23);
            this.ResumeLayout(false);

            txtValue.TextChanged += txtValue_TextChanged;
            txtValue.GotFocus += txtValue_GotFocus;
            txtValue.LostFocus += txtValue_LostFocus;
            txtValue.Click += txtValue_Click;
            txtValue.DoubleClick += txtValue_DoubleClick;
            txtValue.MouseUp += txtValue_MouseUp;
            txtValue.MouseDown += txtValue_MouseDown;
            txtValue.KeyDown += txtValue_KeyDown;
            txtValue.KeyPress += txtValue_KeyPress;
            txtValue.KeyUp += txtValue_KeyUp;
            txtValue.PreviewKeyDown += txtValue_PreviewKeyDown;

            base.ForeColor = txtValue.ForeColor;
            base.BackColor = txtValue.BackColor;
        }
        #endregion

        #region Private class methods and event handlers
        //=====================================================================

        /// <summary>
        /// Determine whether or not to serialize the dropdown font property value
        /// </summary>
        internal bool ShouldSerializeDropDownFont()
        {
            return this.Parent != null && !this.Parent.Font.Equals(this.DropDownFont);
        }

        /// <summary>
        /// This is called by the drop-down control to hide the drop-down and select an item by index
        /// </summary>
        /// <param name="idx">The index to select</param>
        /// <remarks>This will guarantee that the drop-down is closed prior to setting the new value so that the
        /// drop-down doesn't block or hide dialog boxes that may get displayed in a user's event handler.
        /// </remarks>
        /// <overloads>There are two overloads for this method</overloads>
        internal void CommitSelection(int idx)
        {
            if(dropDownStyle != ComboBoxStyle.Simple)
            {
                this.DropDownInterface!.Visible = false;
                OnCloseUp(EventArgs.Empty);
            }

            this.SelectedIndex = idx;

            OnSelectionChangeCommitted(EventArgs.Empty);
        }

        /// <summary>
        /// This is called by the drop-down control to hide the drop-down and select an item by value
        /// </summary>
        /// <remarks>This will guarantee that the drop-down is closed prior to setting the new value so that the
        /// drop-down doesn't block or hide dialog boxes that may get displayed in a user's event handler.</remarks>
        /// <param name="item">The item to select</param>
        internal void CommitSelection(object item)
        {
            if(dropDownStyle != ComboBoxStyle.Simple)
            {
                this.DropDownInterface!.Visible = false;
                OnCloseUp(EventArgs.Empty);
            }

            this.SelectedValue = item;

            OnSelectionChangeCommitted(EventArgs.Empty);
        }

        /// <summary>
        /// This is handled to forward Text Changed events to the user control
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtValue_TextChanged(object? sender, EventArgs e)
        {
            this.OnTextChanged(e);
        }

        /// <summary>
        /// This is handled to forward Got Focus events to the user control
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtValue_GotFocus(object? sender, EventArgs e)
        {
            this.OnGotFocus(e);
        }

        /// <summary>
        /// This is handled to forward Lost Focus events to the user control
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtValue_LostFocus(object? sender, EventArgs e)
        {
            this.OnLostFocus(e);
        }

        /// <summary>
        /// This is handled to forward click events to the user control
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtValue_Click(object? sender, EventArgs e)
        {
            this.OnClick(e);
        }

        /// <summary>
        /// This is handled to forward double-click events to the user control
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtValue_DoubleClick(object? sender, EventArgs e)
        {
            this.OnDoubleClick(e);
        }

        /// <summary>
        /// This is handled to forward mouse down events to the user control
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtValue_MouseDown(object? sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        /// <summary>
        /// This is handled to forward mouse up events to the user control
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtValue_MouseUp(object? sender, MouseEventArgs e)
        {
            this.OnMouseUp(e);
        }

        /// <summary>
        /// This is handled to forward key down events to the user control
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtValue_KeyDown(object? sender, KeyEventArgs e)
        {
            // Call the base version as the text box already called HandleKeys
            base.OnKeyDown(e);
        }

        /// <summary>
        /// This is handled to forward key press events to the user control
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtValue_KeyPress(object? sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }

        /// <summary>
        /// This is handled to forward key up events to the user control
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void txtValue_KeyUp(object? sender, KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }

        /// <summary>
        /// This is handled to forward preview key down events to the user control
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        void txtValue_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
        {
            this.OnPreviewKeyDown(e);
        }

        /// <summary>
        /// This handles backspaces in DropDown and Simple mode
        /// </summary>
        private void AutoCompleteBackspace()
        {
            // For Simple and Dropdown, set the index to -1 and raise the SelectedIndexChanged event via the base
            // class so that we don't change the text.
            if(base.SelectedIndex != -1)
            {
                base.SelectedIndex = -1;

                // This goes through the current instance though
                this.OnSelectedIndexChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// This handles auto-completion when the user types text
        /// </summary>
        private void AutoComplete()
        {
            string findText;
            int idx, start = (autoCompleteEnabled) ? -1 : this.SelectedIndex;

            if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                findText = autoCompleteText;
            else
                findText = txtValue.Text.Substring(0, this.SelectionStart);

            if(findText.Length != 0)
            {
                idx = this.FindStringExact(findText, start);

                if(idx == -1 )
                    idx = this.FindString(findText, start);

                if(idx != -1)
                {
                    this.SelectedIndex = idx;

                    // In DropDownList mode, partial text selections are not supported
                    if(this.DropDownStyle != ComboBoxStyle.DropDownList)
                    {
                        this.UpdateText();
                        this.SelectionStart = findText.Length;
                        this.SelectionLength = this.Text.Length - this.SelectionStart;
                    }
                }
                else  // Remove last typed char in DropDownList mode if not found
                    if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                        autoCompleteText = findText.Substring(0, findText.Length - 1);
                    else
                    {
                        // For Simple and Dropdown, set the index to -1 and raise the SelectedIndexChanged event
                        // via the base class so that we don't change the text.
                        if(base.SelectedIndex != -1)
                        {
                            base.SelectedIndex = -1;

                            // This goes through the current instance though
                            this.OnSelectedIndexChanged(EventArgs.Empty);
                        }
                    }
            }
        }

        /// <summary>
        /// This is used to check for Windows XP theme support and to create the theme handle when needed
        /// </summary>
        /// <returns>True if themed, false if not</returns>
        private bool IsThemed
        {
            get
            {
                if(this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup)
                    return false;

                if(hTheme != IntPtr.Zero)
                    return true;

                if(Application.RenderWithVisualStyles)
					hTheme = UnsafeNativeMethods.OpenThemeData(this.Handle, UnsafeNativeMethods.ComboBoxClass);

                return (hTheme != IntPtr.Zero);
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
                if(hTheme != IntPtr.Zero)
                {
                    UnsafeNativeMethods.CloseThemeData(hTheme);
                    hTheme = IntPtr.Zero;
                }

                ((Control?)this.DropDownInterface)?.Dispose();

                dropDownFont?.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// This is used to create the drop-down control when needed
        /// </summary>
        protected abstract void CreateDropDown();

        /// <summary>
        /// This is called by derived classes to give the textbox the focus
        /// </summary>
        protected internal void FocusTextBox()
        {
            txtValue.SelectAll();
            txtValue.Focus();
        }

        /// <summary>
        /// This is used to handle key presses for the textbox and for the combo box control in all drop-down
        /// styles.
        /// </summary>
        /// <param name="key">The key to process</param>
        /// <returns>True if handled, false if not</returns>
        protected bool HandleKeys(Keys key)
        {
            bool handled = false;
            int idx;

            switch(key)
            {
                case Keys.Enter:
                case Keys.Escape:
                    if(this.DropDownStyle != ComboBoxStyle.Simple && this.DroppedDown)
                    {
                        this.DroppedDown = false;
                        handled = true;
                    }
                    break;

                case Keys.Back:
                    // Strip a character off the auto-complete text?
                    if(this.DropDownStyle == ComboBoxStyle.DropDownList && autoCompleteEnabled &&
                      autoCompleteText.Length > 0)
                    {
                        autoCompleteText = autoCompleteText.Substring(0, autoCompleteText.Length - 1);

                        if(autoCompleteText.Length > 0)
                            AutoComplete();

                        handled = true;
                    }
                    break;

                case Keys.F4:
                    if(this.DropDownStyle != ComboBoxStyle.Simple)
                    {
                        this.DroppedDown = !this.DroppedDown;
                        handled = true;
                    }
                    break;

                case (Keys.Alt | Keys.Down):
                    if(this.DropDownStyle != ComboBoxStyle.Simple && !this.DroppedDown)
                    {
                        this.DroppedDown = true;
                        handled = true;
                    }
                    break;

                case (Keys.Alt | Keys.Up):
                    if(this.DropDownStyle != ComboBoxStyle.Simple && this.DroppedDown)
                    {
                        this.DroppedDown = false;
                        handled = true;
                    }
                    break;

                case Keys.Left:
                case Keys.Up:
                    if(key == Keys.Up || this.DropDownStyle == ComboBoxStyle.DropDownList)
                    {
                        handled = true;

                        if(this.SelectedIndex > 0 && this.Items.Count > 0)
                            this.SelectedIndex--;
                    }
                    break;

                case Keys.Right:
                case Keys.Down:
                    if(key == Keys.Down || this.DropDownStyle == ComboBoxStyle.DropDownList)
                    {
                        handled = true;

                        if(this.SelectedIndex < this.Items.Count - 1)
                            this.SelectedIndex++;
                    }
                    break;

                case Keys.PageUp:
                    idx = this.SelectedIndex - maxDropDownItems + 1;

                    if(idx < 0)
                        idx = 0;

                    this.SelectedIndex = idx;

                    if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                        handled = true;
                    break;

                case Keys.PageDown:
                    idx = this.SelectedIndex + maxDropDownItems - 1;

                    if(idx >= this.Items.Count)
                        idx = this.Items.Count - 1;

                    this.SelectedIndex = idx;

                    if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                        handled = true;
                    break;

                case Keys.Home:
                    if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                    {
                        handled = true;
                        if(this.Items.Count > 0)
                            this.SelectedIndex = 0;
                    }
                    break;

                case Keys.End:
                    if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                    {
                        handled = true;

                        if(this.Items.Count > 0)
                            this.SelectedIndex = this.Items.Count - 1;
                    }
                    break;

                default:
                    break;
            }

            if(handled && key != Keys.Back)
                autoCompleteText = String.Empty;

            return handled;
        }

        /// <summary>
        /// This is called to update the text in the text box control
        /// </summary>
        protected override void UpdateText()
        {
            // If setting text and not in Drop Down List mode, keep the text that is being assigned
            if(this.SelectedIndex != -1)
            {
                object? item = this.Items[this.SelectedIndex];

                if(item != null)
                    this.Text = this.GetItemText(item)!;
            }
            else
            {
                if(!settingText || this.DropDownStyle == ComboBoxStyle.DropDownList)
                    this.Text = null!;
            }

            txtValue.SelectAll();
            this.Invalidate();
        }

        /// <summary>
        /// This is overridden so that in <c>DropDownList</c> mode, we get to handle all input characters for
        /// auto-completion.
        /// </summary>
        /// <param name="charCode">The character code to check</param>
        /// <returns>True if it's an input character, false if not.</returns>
        protected override bool IsInputChar(char charCode)
        {
            if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                return true;

            return base.IsInputChar(charCode);
        }

        /// <summary>
        /// This is overridden to handle special keys
        /// </summary>
        /// <param name="keyData">The key to process</param>
        /// <returns>True if the key was processed, false if not</returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if(this.HandleKeys(keyData))
            {
                if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    base.OnKeyDown(new KeyEventArgs(keyData));
                    return true;
                }

                return (keyData == Keys.Enter || keyData == Keys.Escape || keyData == Keys.F4);
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Invalidate on data source changed to refresh any image item
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnDataSourceChanged(EventArgs e)
        {
            base.OnDataSourceChanged(e);

            if(drawImage)
                this.Invalidate();
        }

        /// <summary>
        /// This is overridden to perform layout at design-time
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <remarks>Layout doesn't always occur at design-time when the control is initially dropped on the
        /// form.</remarks>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if(this.DesignMode)
                this.PerformLayout();
        }

        /// <summary>
        /// This is overridden to handle laying out the control elements
        /// </summary>
        /// <param name="e">The event parameters</param>
        protected override void OnLayout(LayoutEventArgs e)
        {
            int height, borderWidth, minWidth;

            Graphics g = Graphics.FromHwnd(this.Handle);
            SizeF size = g.MeasureString("0", this.Font);
            g.Dispose();

            height = (int)size.Height + SystemInformation.Border3DSize.Height +
                (SystemInformation.FixedFrameBorderSize.Height * 2);

            txtValue.Top = 3;
            txtValue.Height = height - 6;

            drawThemed = this.IsThemed;

            if(drawThemed || this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup)
            {
                borderWidth = 1;

                if(this.RightToLeft == RightToLeft.No)
                    txtValue.Left = ((drawImage) ? txtValue.Height : 1) + borderWidth + 1;
                else
                    txtValue.Left = BaseComboBox.DropDownButtonWidth + borderWidth + 3;
            }
            else
            {
                borderWidth = 2;

                if(this.RightToLeft == RightToLeft.No)
                    txtValue.Left = ((drawImage) ? txtValue.Height : 1) + borderWidth;
                else
                    txtValue.Left = BaseComboBox.DropDownButtonWidth + borderWidth + 1;
            }

            minWidth = (BaseComboBox.DropDownButtonWidth * 2) + (borderWidth * 2);

            if(this.Width < minWidth)
                this.Width = minWidth;

            if(dropDownStyle != ComboBoxStyle.Simple)
            {
                this.Height = height;

                if(drawThemed || this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup)
                    txtValue.Width = this.Width - (borderWidth * 2) - BaseComboBox.DropDownButtonWidth -
                        ((drawImage) ? txtValue.Height + borderWidth + 2 : 4);
                else
                    txtValue.Width = this.Width - (borderWidth * 2) - BaseComboBox.DropDownButtonWidth -
                        ((drawImage) ? txtValue.Height + borderWidth - 1 : 2);
            }
            else
            {
                txtValue.Width = this.Width - (borderWidth * 2) - ((drawImage) ? txtValue.Height + borderWidth : 2);

                if(this.Height < height * 2)
                    this.Height = height * 2;

                if(this.DropDownInterface != null)
                {
                    this.DropDownInterface.Top = txtValue.Height + (borderWidth * 2) + 2;
                    this.DropDownInterface.Width = this.Width;
                    this.DropDownInterface.Height = this.Height - txtValue.Height - (borderWidth * 2) - 2;
                }
            }

            // Determine element positions
            rectBackground = this.ClientRectangle;
            rectBorder = rectBackground;

            if(this.DropDownStyle == ComboBoxStyle.Simple)
                rectBorder.Height = txtValue.Height + (borderWidth * 2) + 2;

            if(this.RightToLeft == RightToLeft.No)
            {
                rectButton = new Rectangle(this.Width - borderWidth - BaseComboBox.DropDownButtonWidth,
                    borderWidth, BaseComboBox.DropDownButtonWidth, this.Height - (borderWidth * 2));

                if(this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup)
                {
                    rectButton.Offset(1, 0);
                    rectButton.Width -= 1;

                    rectButtonBorder = rectButton;
                    rectButtonBorder.Inflate(1, 1);

                    arrow[2] = new Point(rectButtonBorder.Left + (rectButtonBorder.Width / 2),
                        rectButtonBorder.Top + (rectButtonBorder.Height / 2) + 2);
                    arrow[0] = new Point(arrow[2].X - 2, arrow[2].Y - 3);
                    arrow[1] = new Point(arrow[2].X + 3, arrow[2].Y - 3);
                }

                rectImage = new Rectangle(borderWidth + 1, borderWidth + 1, txtValue.Height - borderWidth,
                    txtValue.Height - borderWidth + 2);
            }
            else
            {
                rectButton = new Rectangle(borderWidth, borderWidth, BaseComboBox.DropDownButtonWidth,
                    this.Height - (borderWidth * 2));

                if(this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup)
                {
                    rectButtonBorder = rectButton;
                    rectButtonBorder.Inflate(1, 1);

                    arrow[2] = new Point(rectButtonBorder.Left + (rectButtonBorder.Width / 2),
                        rectButtonBorder.Top + (rectButtonBorder.Height / 2) + 2);
                    arrow[0] = new Point(arrow[2].X - 2, arrow[2].Y - 3);
                    arrow[1] = new Point(arrow[2].X + 3, arrow[2].Y - 3);
                }

                if(!drawThemed)
                    rectImage = new Rectangle(this.Width - txtValue.Height - borderWidth + 1, borderWidth + 1,
                        txtValue.Height - borderWidth, txtValue.Height - borderWidth + 2);
                else
                {
                    txtValue.Width -= 2;
                    rectImage = new Rectangle(this.Width - txtValue.Height - borderWidth, borderWidth + 1,
                        txtValue.Height - borderWidth, txtValue.Height - borderWidth + 1);
                }
            }

            rectText = new Rectangle(txtValue.Location, txtValue.Size);
            rectText.Width -= 1;

            // Enforce the default selection
            if(this.EnforceDefaultSelection && this.SelectedIndex == -1 &&
              dropDownStyle == ComboBoxStyle.DropDownList && !this.DesignMode)
                if(this.DefaultSelection < this.Items.Count)
                    this.SelectedIndex = this.DefaultSelection;
                else
                    this.SelectedIndex = this.Items.Count - 1;

            base.OnLayout(e);
        }

        /// <summary>
        /// This is overridden to draw the control with the appropriate style
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if(e == null)
                throw new ArgumentNullException(nameof(e));

            Color backColor;
            DrawItemState state;
            IntPtr hDC;
            Graphics g = e.Graphics;
            RECT rbg, rbtn;

            if(this.Enabled)
            {
                state = DrawItemState.None;
                backColor = this.BackColor;
            }
            else
            {
                state = DrawItemState.Disabled;
                backColor = (this.BackColor == SystemColors.Window) ? SystemColors.Control : this.BackColor;
            }

            if(drawThemed)
            {
				hDC = g.GetHdc();

			    rbg = new RECT(rectBackground);
                rbtn = new RECT(rectButton);

                if(UnsafeNativeMethods.IsThemeBackgroundPartiallyTransparent(hTheme,
                  UnsafeNativeMethods.DropDownButton, UnsafeNativeMethods.ComboBoxNormal))
			        UnsafeNativeMethods.DrawThemeParentBackground(this.Handle, hDC, ref rbg);

			    if(state == DrawItemState.None)
                {
			        UnsafeNativeMethods.DrawThemeBackground(hTheme, hDC, UnsafeNativeMethods.ComboBoxText,
                        UnsafeNativeMethods.ComboBoxNormal, ref rbg, ref rbg);

                    if(dropDownStyle != ComboBoxStyle.Simple)
			            UnsafeNativeMethods.DrawThemeBackground(hTheme, hDC, UnsafeNativeMethods.DropDownButton,
                            themeState, ref rbtn, ref rbtn);
                }
                else
                {
				    UnsafeNativeMethods.DrawThemeBackground(hTheme, hDC, UnsafeNativeMethods.ComboBoxText,
                        UnsafeNativeMethods.ComboBoxDisabled, ref rbg, ref rbg);

                    if(dropDownStyle != ComboBoxStyle.Simple)
				        UnsafeNativeMethods.DrawThemeBackground(hTheme, hDC, UnsafeNativeMethods.DropDownButton,
                            UnsafeNativeMethods.ComboBoxDisabled, ref rbtn, ref rbtn);
                }

                g.ReleaseHdc(hDC);

                // Fill the text area with the background color
                using Brush bb = new SolidBrush(backColor);
                Rectangle textBg = new(rectBackground.Location, rectBackground.Size);

                textBg.Inflate(-2, -2);

                if(dropDownStyle != ComboBoxStyle.Simple)
                    textBg.Width -= rectButton.Width;
                else
                {
                    textBg.Height -= 1;
                    textBg.Width -= 1;
                }

                g.FillRectangle(bb, textBg);
            }
            else
            {
                using(Brush bb = new SolidBrush(backColor))
                {
                    g.FillRectangle(bb, rectBackground);
                }

                switch(this.FlatStyle)
                {
                    case FlatStyle.Flat:
                    case FlatStyle.Popup:
                        ControlPaint.DrawBorder(g, rectBorder, (this.Enabled) ? SystemColors.Window :
                            SystemColors.ControlDark, ButtonBorderStyle.Solid);
                        break;

                    default:
                        ControlPaint.DrawBorder3D(g, rectBorder, Border3DStyle.Sunken);
                        break;
                }

                if(dropDownStyle != ComboBoxStyle.Simple)
                {
                    if(this.FlatStyle == FlatStyle.Flat || this.FlatStyle == FlatStyle.Popup)
                    {
                        g.FillRectangle(SystemBrushes.Control, rectButton);

                        if(this.Enabled)
                        {
                            ControlPaint.DrawBorder(g, rectButtonBorder, SystemColors.Window, ButtonBorderStyle.Solid);

                            using Brush b = new SolidBrush(Color.Black);
                            g.FillPolygon(b, arrow);
                        }
                        else
                            g.FillPolygon(SystemBrushes.ControlDark, arrow);
                    }
                    else
                        ControlPaint.DrawComboButton(g, rectButton, (this.Enabled) ? ButtonState.Normal :
                            ButtonState.Inactive);
                }
            }

            // Draw the text if in DropDownList mode
            if(dropDownStyle == ComboBoxStyle.DropDownList)
            {
                if(this.Focused)
                {
                    state |= DrawItemState.Selected;
                    rectText.Inflate(-1, -1);
                    g.FillRectangle(SystemBrushes.Highlight, rectText);
                    rectText.Inflate(1, 1);
                    rectText.Location = new Point(rectText.Left - 1, rectText.Top + 1);
                    g.DrawString(txtValue.Text, txtValue.Font, SystemBrushes.HighlightText, rectText);

                    rectText.Location = new Point(rectText.Left + 1, rectText.Top - 1);
                    ControlPaint.DrawFocusRectangle(g, rectText);
                }
                else
                {
                    rectText.Location = new Point(rectText.Left - 1, rectText.Top + 1);

                    if(state == DrawItemState.None)
                    {
                        using Brush fb = new SolidBrush(this.ForeColor);
                        g.DrawString(txtValue.Text, txtValue.Font, fb, rectText);
                    }
                    else
                    {
                        using Brush fb = new SolidBrush(SystemColors.ControlDark);
                        g.DrawString(txtValue.Text, txtValue.Font, fb, rectText);
                    }

                    rectText.Location = new Point(rectText.Left + 1, rectText.Top - 1);
                }
            }

            if(drawImage)
            {
                if(this.Focused)
                    state |= DrawItemState.Focus;

                OnDrawItemImage(new DrawItemEventArgs(g, txtValue.Font, rectImage, this.SelectedIndex, state,
                    this.ForeColor, backColor));
            }

            base.OnPaint(e);
        }

        /// <summary>
        /// This is overridden to invalidate the control when it gains focus
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnGotFocus(EventArgs e)
        {
            autoCompleteText = String.Empty;

            this.Invalidate();
            this.Update();
            base.OnGotFocus(e);
        }

        /// <summary>
        /// This is overridden to invalidate the control when it loses focus
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnLostFocus(EventArgs e)
        {
            // The user control combo box loses focus when dropped down so ignore it in that case or the drop
            // down doesn't show up.
            if(!inDroppedDown)
                this.DroppedDown = false;

            this.Invalidate();
            this.Update();
            base.OnLostFocus(e);
        }

        /// <summary>
        /// This is overridden to fire the <see cref="NotInList"/> event before the normal validation method so
        /// that you have a chance to take action on entered text that does not appear in the item list.
        /// </summary>
        /// <param name="e">The event arguments</param>
        /// <remarks>If the text is found, the <see cref="SelectedIndex"/> property is set to match the entry
        /// containing the text.  If it is not found, the <see cref="NotInList"/> event is raised so that you
        /// can control what happens.</remarks>
        protected override void OnValidating(CancelEventArgs e)
        {
            string currentText = this.Text;
            int idx = this.FindStringExact(currentText);

            if(e == null)
                throw new ArgumentNullException(nameof(e));

            if(idx == -1)
                OnNotInList(e);
            else
            {
                if(dropDownStyle != ComboBoxStyle.DropDownList)
                {
                    if(this.SelectedIndex != idx)
                        this.SelectedIndex = idx;
                    else
                    {
                        if(currentText != this.GetItemText(this.Items[idx]))
                            this.UpdateText();
                    }
                }
            }

            if(!e.Cancel)
                base.OnValidating(e);
        }

        /// <summary>
        /// This is overridden to handle special keys
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if(e == null)
                throw new ArgumentNullException(nameof(e));

            e.Handled = this.HandleKeys(e.KeyData);
            base.OnKeyDown(e);
        }

        /// <summary>
        /// This is overridden to handle auto-completion if enabled
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if(e == null)
                throw new ArgumentNullException(nameof(e));

            if(this.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                // If auto-complete is enabled, match text entered so far.  If disabled, match the item with the
                // same first letter.
                if(autoCompleteEnabled)
                    autoCompleteText += e.KeyChar;
                else
                    autoCompleteText = String.Empty + e.KeyChar;

                this.AutoComplete();
            }
            else
                base.OnKeyPress(e);
        }

        /// <summary>
        /// This is overridden to see if the drop-down button has been clicked
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if(e == null)
                throw new ArgumentNullException(nameof(e));

            base.OnMouseDown(e);

            if(dropDownStyle != ComboBoxStyle.Simple)
                if(this.DroppedDown)
                    this.DroppedDown = false;
                else
                    if(this.ContainsFocus)
                    {
                        Point p = this.PointToClient(Cursor.Position);

                        if(e.Button == MouseButtons.Left && (rectButton.Contains(p) ||
                          this.DropDownStyle == ComboBoxStyle.DropDownList))
                            this.DroppedDown = true;
                    }
        }

        /// <summary>
        /// This is overridden to highlight the drop-down button when using themed drawing
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if(drawThemed && dropDownStyle != ComboBoxStyle.Simple)
            {
                Point p = this.PointToClient(Cursor.Position);
                Rectangle r;

                if(dropDownStyle == ComboBoxStyle.DropDownList)
                    r = rectBorder;
                else
                    r = rectButton;

                if(r.Contains(p))
                {
                    themeState = UnsafeNativeMethods.ComboBoxHot;
                    this.Invalidate();
                }
                else
                    if(themeState == UnsafeNativeMethods.ComboBoxHot)
                    {
                        themeState = UnsafeNativeMethods.ComboBoxNormal;
                        this.Invalidate();
                    }
            }
        }

        /// <summary>
        /// This is overridden to turn off highlighting when using themed drawing
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            if(themeState == UnsafeNativeMethods.ComboBoxHot)
            {
                themeState = UnsafeNativeMethods.ComboBoxNormal;
                this.Invalidate();
            }

            base.OnMouseLeave(e);
        }

        /// <summary>
        /// This is overridden to set the font in the contained text box control
        /// </summary>
        /// <param name="e">The event arguments</param>
        protected override void OnFontChanged(EventArgs e)
        {
            txtValue.Font = this.Font;

            base.OnFontChanged(e);
            base.PerformLayout();
        }

        /// <summary>
        /// This is overridden to handle updates to the Windows XP theme
        /// </summary>
        /// <param name="m">The message</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if(m.Msg == UnsafeNativeMethods.ThemeChanged)
            {
                if(hTheme != IntPtr.Zero)
                {
                    UnsafeNativeMethods.CloseThemeData(hTheme);
                    hTheme = IntPtr.Zero;
                }

                base.PerformLayout();
            }
        }

        /// <summary>
        /// This is used to select a part of the text within the textbox portion of the combo box
        /// </summary>
        /// <param name="start">The starting position of the selection</param>
        /// <param name="len">The length of the selection</param>
        /// <remarks>In <see cref="ComboBoxStyle.DropDownList"/> mode, all text is selected and the start and
        /// length parameters are ignored.</remarks>
        public void Select(int start, int len)
        {
            if(this.DropDownStyle == ComboBoxStyle.DropDownList)
                txtValue.SelectAll();
            else
                txtValue.Select(start, len);
        }

        /// <summary>
        /// This is used to select all text within the textbox portion of the combo box
        /// </summary>
        public void SelectAll()
        {
            txtValue.SelectAll();
        }
        #endregion
    }
}
