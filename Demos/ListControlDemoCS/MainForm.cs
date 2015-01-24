//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : MainForm.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 10/01/2014
// Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
// Compiler: Visual C#
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

using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace ListControlDemoCS
{
	/// <summary>
	/// This is the main form of the EWSoftware List Control demo
	/// </summary>
	public partial class MainForm : System.Windows.Forms.Form
	{
        #region Constructor
        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
		public MainForm()
		{
			InitializeComponent();

            if(!File.Exists(@".\TestData.mdb"))
            {
                MessageBox.Show("Unable to locate test database.  It should be in the main project folder " +
                    "two levels up from the location of this executable.", "List Control Demo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using(var dbConn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\TestData.mdb"))
                {
                    // Load the menu data
                    OleDbCommand cmd = new OleDbCommand("Select * From DemoInfo Order By DemoOrder, DemoName",
                        dbConn);
                    cmd.CommandType = CommandType.Text;
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

                    DataSet demoData = new DataSet();
                    adapter.Fill(demoData);

                    // Set the data list's data source and row template
                    dlMenu.SetDataBinding(demoData.Tables[0], null, typeof(MenuRow));
                }
            }
            catch(OleDbException ex)
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
        #endregion
    }
}
