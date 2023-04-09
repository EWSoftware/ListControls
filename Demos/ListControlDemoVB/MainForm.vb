'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : MainForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 04/09/2023
' Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
'
' This application is used to demonstrate various features of the EWSoftware List Control classes
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 10/27/2005  EFW  Created the code
' 03/06/2006  EFW  Reworked main menu form to use a DataList
'================================================================================================================

Imports System.Data
Imports System.Data.OleDb
Imports System.IO

 #Disable Warning CA2000

Public Partial Class MainForm
    Inherits Form

    Public Sub New()
        MyBase.New()

        ' Required for Windows Form Designer support
        InitializeComponent()

        If Not File.Exists(".\TestData.mdb") Then
            MessageBox.Show("Unable to locate test database.  It should be in the main project folder one " &
                "level up from the location of this executable.", "List Control Demo",
                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Try
            Using dbConn As New OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\TestData.mdb")
                ' Load the menu data
                Using cmd As New OleDbCommand("Select * From DemoInfo Order By DemoOrder, DemoName", dbConn) With {
                    .CommandType = CommandType.Text
                }
                    Using adapter As New OleDbDataAdapter(cmd)
                        Dim demoData As New DataSet

                        adapter.Fill(demoData)

                        ' Set the data list's data source and row template
                        dlMenu.SetDataBinding(demoData.Tables(0), Nothing, GetType(MenuRow))
                    End Using
                End Using
            End Using

        Catch ex As OleDbException
            MessageBox.Show(ex.Message)

        End Try

    End Sub

    ' The main entry point for the application
    <STAThread> _
    Shared Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
    	Application.Run(New MainForm())
    End Sub

End Class

#Enable Warning CA2000
