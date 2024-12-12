//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : MenuRow.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/02/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This is used as a row template for the main menu form's data list
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/06/2006  EFW  Created the code
// 01/19/2007  EFW  Added extended tree view control demo
//===============================================================================================================

namespace ListControlDemoCS
{
    /// <summary>
    /// This is used as a row template for the main menu form's data list
    /// </summary>
    public partial class MenuRow : TemplateControl
    {
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public MenuRow()
        {
            // At runtime, actual initialization is deferred until needed
            if(this.DesignMode)
                InitializeComponent();
        }
        #endregion

        #region Method overrides
        //=====================================================================

        /// <summary>
        /// Actual initialization is deferred until needed to save time and resources
        /// </summary>
        protected override void InitializeTemplate()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Bind the controls to their data source
        /// </summary>
        protected override void Bind()
        {
            var demoInfo = (DemoInfo)this.RowSource!;

            this.AddBinding(lblDemoName, nameof(Control.Text), nameof(DemoInfo.DemoName));
            this.AddBinding(lblDemoDesc, nameof(Control.Text), nameof(DemoInfo.DemoDesc));

            // Hide the button if there is no demo
            btnDemo.Visible = demoInfo.HasDemoYN;

            // Show the image for the related control if there is one
            if(demoInfo.UseControlImageYN)
            {
                Assembly asm = typeof(TemplateControl).Assembly;

                var stream = asm.GetManifestResourceStream($"EWSoftware.ListControls.{demoInfo.DemoName}.bmp");

                if(stream != null)
                {
                    Bitmap image = new(stream);
                    image.MakeTransparent();

                    lblDemoImage.Image = image;
                }
            }
            else
                lblDemoImage.Visible = false;
        }
        #endregion

        #region Event handlers
        //=====================================================================

        /// <summary>
        /// View the demo for the selected item
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event arguments</param>
        private void btnDemo_Click(object sender, EventArgs e)
        {
            var demoInfo = (DemoInfo)this.RowSource!;

            switch(demoInfo.DemoName)
            {
                case nameof(CheckBoxList):
                    using(CheckBoxListTestForm dlg = new())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                case nameof(DataList):
                    using(DataListTestForm dlg = new())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                case nameof(DataNavigator):
                    using(DataNavigatorTestForm dlg = new())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                case nameof(ExtendedTreeView):
                    using(ExtendedTreeViewTestForm dlg = new())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                case nameof(MultiColumnComboBox):
                    using(ComboBoxTestForm dlg = new())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                case nameof(RadioButtonList):
                    using(RadioButtonListTestForm dlg = new())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                case nameof(UserControlComboBox):
                    using(UserControlComboTestForm dlg = new())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                case "Relationship Test":
                    using(RelationTestForm dlg = new())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                default:
                    MessageBox.Show("Unknown demo.  Please contact tech support.", "List Control Demo",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }
        }
        #endregion
    }
}
