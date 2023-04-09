//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : MenuRow.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/01/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
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

using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;

using EWSoftware.ListControls;

namespace ListControlDemoCS
{
	/// <summary>
	/// This is used as a row template for the main menu form's data list
	/// </summary>
	public partial class MenuRow : EWSoftware.ListControls.TemplateControl
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
            DataRowView drv = (DataRowView)this.RowSource;

            this.AddBinding(lblDemoName, "Text", "DemoName");
            this.AddBinding(lblDemoDesc, "Text", "DemoDesc");

            // Hide the button if there is no demo
            btnDemo.Visible = (bool)drv["HasDemoYN"];

            // Show the image for the related control if there is one
            if((bool)drv["UseControlImageYN"])
            {
                Assembly asm = typeof(TemplateControl).Assembly;

                Bitmap image = new Bitmap(asm.GetManifestResourceStream(String.Format(
                    CultureInfo.InvariantCulture, "EWSoftware.ListControls.{0}.bmp", drv["DemoName"])));
                image.MakeTransparent();
                lblDemoImage.Image = image;
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
        private void btnDemo_Click(object sender, System.EventArgs e)
        {
            DataRowView drv = (DataRowView)this.RowSource;

            switch((string)drv["DemoName"])
            {
                case "CheckBoxList":
                    using(CheckBoxListTestForm dlg = new CheckBoxListTestForm())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                case "DataList":
                    using(DataListTestForm dlg = new DataListTestForm())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                case "DataNavigator":
                    using(DataNavigatorTestForm dlg = new DataNavigatorTestForm())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                case "ExtendedTreeView":
                    using(ExtendedTreeViewTestForm dlg = new ExtendedTreeViewTestForm())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                case "MultiColumnComboBox":
                    using(ComboBoxTestForm dlg = new ComboBoxTestForm())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                case "RadioButtonList":
                    using(RadioButtonListTestForm dlg = new RadioButtonListTestForm())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                case "UserControlComboBox":
                    using(UserControlComboTestForm dlg = new UserControlComboTestForm())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                case "Relationship Test":
                    using(RelationTestForm dlg = new RelationTestForm())
                    {
                        dlg.ShowDialog();
                    }
                    break;

                default:
                    MessageBox.Show("Unknown demo.  Please contact tech support", "List Control Demo",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }
        }
        #endregion
    }
}
