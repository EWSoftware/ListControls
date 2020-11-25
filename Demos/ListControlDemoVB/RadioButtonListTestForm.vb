'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : RadioButtonListTestForm.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 10/02/2014
' Note    : Copyright 2005-2014, Eric Woodruff, All rights reserved
' Compiler: Microsoft Visual Basic .NET
'
' This is used to demonstrate the RadioButtonList control
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 10/27/2005  EFW  Created the code
'================================================================================================================

Imports System
Imports System.Collections
Imports System.Data
Imports System.Data.OleDb
Imports System.Windows.Forms

Imports EWSoftware.ListControls

Public Partial Class RadioButtonListTestForm
    Inherits System.Windows.Forms.Form

    Private demoData, productData As DataSet

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        demoData = New DataSet()
        productData = New DataSet()

        Try
            Using dbConn As New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\TestData.mdb")
                ' Load some data for the demo
                Dim cmd As New OleDbCommand("Select * From DemoTable Order By Label", dbConn)
                cmd.CommandType = CommandType.Text
                Dim adapter As New OleDbDataAdapter(cmd)

                adapter.Fill(demoData)

                ' Use a named table for this one
                adapter.TableMappings.Add("Table", "ProductInfo")
                cmd.CommandText = "Select * From ProductInfo Order By ProductName"
                adapter.Fill(productData)
            End Using

        Catch ex As OleDbException
            MessageBox.Show(ex.Message)

        End Try

        cboDataSource.SelectedIndex = 0

        pgProps.SelectedObject = rblDemo
        pgProps.Refresh()

    End Sub

    ' Refresh the display and the radio button list settings after they have changed
    Private Sub pgProps_PropertyValueChanged(s As object, e As System.Windows.Forms.PropertyValueChangedEventArgs) _
      Handles pgProps.PropertyValueChanged
        rblDemo.Invalidate()
        rblDemo.Update()
    End Sub

    ' Change the data source for the radio button list
    Private Sub cboDataSource_SelectedIndexChanged(sender As Object, e As System.EventArgs) _
        Handles cboDataSource.SelectedIndexChanged

        Dim dr As DataRow, c As DataColumn

        ' Suspend updates to the radio button list to speed it up and prevent flickering
        rblDemo.BeginInit()

        ' Clear out the prior definitions
        cboColumns.Items.Clear()
        cboColumns.SelectedIndex = -1

        rblDemo.DataSource = Nothing
        rblDemo.DisplayMember = String.Empty
        rblDemo.ValueMember = String.Empty
        rblDemo.Items.Clear()

        ' We must have the data
        If demoData.Tables.Count = 0 Or productData.Tables.Count = 0 Then
            MessageBox.Show("Database not found.  It must be located in the demo project's folder.", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Stop)

            rblDemo.EndInit()
            Return
        End If

        Select Case cboDataSource.SelectedIndex
            Case 0      ' Data Table
                rblDemo.DisplayMember = "Label"
                rblDemo.ValueMember = "ListKey"
                rblDemo.DataSource = demoData.Tables(0)

            Case 1      ' Data View
                rblDemo.DisplayMember = "Label"
                rblDemo.ValueMember = "ListKey"
                rblDemo.DataSource = demoData.Tables(0).DefaultView

            Case 2      ' Data Set
                ' Use a named table for this one
                rblDemo.DisplayMember = "ProductInfo.ProductName"
                rblDemo.ValueMember = "ProductInfo.ProductID"
                rblDemo.DataSource = productData

            Case 3      ' Array List
                Dim al As New ArrayList(100)

                For Each dr In productData.Tables(0).Rows
                    al.Add(New ListItem(dr("ProductID"), CType(dr("ProductName"), String)))
                Next

                rblDemo.DisplayMember = "Display"   ' ListItem description
                rblDemo.ValueMember = "Value"       ' ListItem value
                rblDemo.DataSource = al

            Case 4      ' Item collection strings
                ' Like the above but we add the strings directly to the radio button list's Items collection
                For Each dr In productData.Tables(0).Rows
                    rblDemo.Items.Add(dr("ProductName"))
                Next

                ' The item collection is the data source for this one.  It's a simple string list so there are no
                ' display or value members.

        End Select

        ' Resume updates to the radio button list and display the new set of radio buttons
        rblDemo.EndInit()

        ' Load the column names
        If Not (rblDemo.DataSource Is Nothing) Then
            If TypeOf(rblDemo.DataSource) Is ArrayList Then
                cboColumns.Items.Add("Display")
                cboColumns.Items.Add("Value")
            Else
                Dim tbl As DataTable

                If TypeOf(rblDemo.DataSource) Is DataSet Then
                    tbl = productData.Tables(0)
                Else
                    tbl = demoData.Tables(0)
                End If

                For Each c in tbl.Columns
                    cboColumns.Items.Add(c.ColumnName)
                Next
            End If
        End If

        If cboColumns.Items.Count = 0 Then
            cboColumns.Enabled = False
            txtRowNumber.Enabled = False
            btnGetValue.Enabled = False
        Else
            cboColumns.Enabled = True
            txtRowNumber.Enabled = True
            btnGetValue.Enabled = True
        End If
    End Sub

    ' Get a value from the radio button list
    Private Sub btnGetValue_Click(sender As Object, e As System.EventArgs) _
      Handles btnGetValue.Click
        ' This can be any column from the data source regardless of whether or not it is displayed.  Note that
        ' you can also use rblDemo("ColName") to get a column value from the item indicated by the SelectedIndex
        ' property.
        txtValue.Text = String.Format("{0} = {1}", cboColumns.Text, rblDemo(CType(txtRowNumber.Value, Integer),
            cboColumns.Text))
    End Sub

    ' Show the current item info when the selected index changes
    Private Sub rblDemo_SelectedIndexChanged(sender As Object, e As System.EventArgs) _
      Handles rblDemo.SelectedIndexChanged
        ' Note that SelectedValue is only valid if there is a data source
        txtValue.Text = String.Format("Index = {0}, Value = {1}, Text = {2}", rblDemo.SelectedIndex,
            rblDemo.SelectedValue, rblDemo.Text)
    End Sub

    ' Use or clear the image list
    Private Sub chkShowImages_CheckedChanged(sender As Object, e As System.EventArgs) _
      Handles chkShowImages.CheckedChanged
        If chkShowImages.Checked Then
            ' The image list is not usable with themed radio buttons under .NET 1.1
            If rblDemo.FlatStyle = FlatStyle.System Then
                rblDemo.FlatStyle = FlatStyle.Standard
            End If

            rblDemo.ImageList = ilImages
        Else
            rblDemo.ImageList = Nothing
        End If
    End Sub

End Class
