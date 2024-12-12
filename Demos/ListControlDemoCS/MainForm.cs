//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : MainForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/02/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// This application is used to demonstrate various features of the EWSoftware List Control classes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/17/2005  EFW  Created the code
// 03/06/2006  EFW  Reworked main menu form to use a DataList
//===============================================================================================================


namespace ListControlDemoCS
{
	/// <summary>
	/// This is the main form of the EWSoftware List Control demo
	/// </summary>
	internal sealed partial class MainForm : Form
	{
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public MainForm()
		{
			InitializeComponent();

            DemoDataContext.DatabaseLocation = Path.GetFullPath(@"..\..\..\..\DemoData.mdf");

            if(!File.Exists(DemoDataContext.DatabaseLocation))
            {
                MessageBox.Show("Unable to locate test database.  It should be in the main project folder",
                    "List Control Demo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Load the menu data
                using var dc = new DemoDataContext();

                // Set the data list's data source and row template
                dlMenu.SetDataBinding(dc.DemoInfo.OrderBy(d => d.DemoOrder).ThenBy(d => d.DemoName).ToList(),
                    null, typeof(MenuRow));
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Main program entry point
        //=====================================================================

		/// <summary>
		/// The main entry point for the application
		/// </summary>
		[STAThread]
		static void Main()
		{
#if NET48
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#else
            ApplicationConfiguration.Initialize();
#endif
            Application.Run(new MainForm());
		}
        #endregion
    }
}
