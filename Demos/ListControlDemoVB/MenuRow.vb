'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : MenuRow.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 04/09/2023
' Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
'
' This is used as a row template for the main menu form's data list
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 03/06/2006  EFW  Created the code
' 03/02/2007  EFW  Added extended tree view control demo
'================================================================================================================

Imports System.Data
Imports System.Reflection

Imports EWSoftware.ListControls

Public Partial Class MenuRow
    Inherits EWSoftware.ListControls.TemplateControl

    Public Sub New()
        MyBase.New()

        ' At runtime, actual initialization is deferred until needed
        If Me.DesignMode Then
            InitializeComponent()
        End If
    End Sub

    ' Actual initialization is deferred until needed to save
    ' time and resources.
    Protected Overrides Sub InitializeTemplate()
        Me.InitializeComponent()
    End Sub

    ' Bind the controls to their data source
    Protected Overrides Sub Bind()
        Dim drv As DataRowView = CType(Me.RowSource, DataRowView)

        Me.AddBinding(lblDemoName, "Text", "DemoName")
        Me.AddBinding(lblDemoDesc, "Text", "DemoDesc")

        ' Hide the button if there is no demo
        btnDemo.Visible = CType(drv("HasDemoYN"), Boolean)

        ' Show the image for the related control if there is one
        If CType(drv("UseControlImageYN"), Boolean) = True Then
            Dim asm As [Assembly] = GetType(TemplateControl).Assembly

            Dim image As New Bitmap(asm.GetManifestResourceStream($"EWSoftware.ListControls.{drv("DemoName")}.bmp"))
            image.MakeTransparent()
            lblDemoImage.Image = image
        Else
            lblDemoImage.Visible = False
        End If
    End Sub

    ' View the demo for the selected item
    Private Sub btnDemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDemo.Click
        Dim drv As DataRowView = CType(Me.RowSource, DataRowView)

        Select Case CType(drv("DemoName"), String)
            Case "CheckBoxList"
                Using dlg As New CheckBoxListTestForm
                    dlg.ShowDialog()
                End Using

            Case "DataList"
                Using dlg As New DataListTestForm
                    dlg.ShowDialog()
                End Using

            Case "DataNavigator"
                Using dlg AS New DataNavigatorTestForm
                    dlg.ShowDialog()
                End Using

            Case "ExtendedTreeView"
                Using dlg As new ExtendedTreeViewTestForm()
                    dlg.ShowDialog()
                End Using

            Case "MultiColumnComboBox"
                Using dlg As New ComboBoxTestForm
                    dlg.ShowDialog()
                End Using

            Case "RadioButtonList"
                Using dlg As New RadioButtonListTestForm
                    dlg.ShowDialog()
                End Using

            Case "UserControlComboBox"
                Using dlg As New UserControlComboTestForm
                    dlg.ShowDialog()
                End Using

            Case "Relationship Test"
                Using dlg As New RelationTestForm
                    dlg.ShowDialog()
                End Using

            Case Else
                MessageBox.Show("Unknown demo.  Please contact tech support", "List Control Demo",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Select

    End Sub
End Class
