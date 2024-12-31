'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : MenuRow.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 12/02/2024
' Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
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
        Dim demoInfo As DemoInfo = CType(Me.RowSource, DemoInfo)

        Me.AddBinding(Me.lblDemoName, NameOf(Control.Text), NameOf(Database.DemoInfo.DemoName))
        Me.AddBinding(Me.lblDemoDesc, NameOf(Control.Text), NameOf(Database.DemoInfo.DemoDesc))

        ' Hide the button if there is no demo
        btnDemo.Visible = demoInfo.HasDemoYN

        ' Show the image for the related control if there is one
        If demoInfo.UseControlImageYN Then
            Dim asm As [Assembly] = GetType(TemplateControl).Assembly

            Dim stream As Stream = asm.GetManifestResourceStream($"EWSoftware.ListControls.{demoInfo.DemoName}.png")

            If stream IsNot Nothing Then
                Dim image As new BitMap(stream)

                lblDemoImage.Image = image
            End If
        Else
            lblDemoImage.Visible = False
        End If
    End Sub

    ' View the demo for the selected item
    Private Sub btnDemo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDemo.Click
        Dim demoInfo As DemoInfo = CType(Me.RowSource, DemoInfo)

        Select Case demoInfo.DemoName
            Case NameOf(CheckBoxList)
                Using dlg As New CheckBoxListTestForm
                    dlg.ShowDialog()
                End Using

            Case NameOf(DataList)
                Using dlg As New DataListTestForm
                    dlg.ShowDialog()
                End Using

            Case NameOf(DataNavigator)
                Using dlg AS New DataNavigatorTestForm
                    dlg.ShowDialog()
                End Using

            Case NameOf(ExtendedTreeView)
                Using dlg As new ExtendedTreeViewTestForm()
                    dlg.ShowDialog()
                End Using

            Case NameOf(MultiColumnComboBox)
                Using dlg As New ComboBoxTestForm
                    dlg.ShowDialog()
                End Using

            Case NameOf(RadioButtonList)
                Using dlg As New RadioButtonListTestForm
                    dlg.ShowDialog()
                End Using

            Case NameOf(UserControlComboBox)
                Using dlg As New UserControlComboTestForm
                    dlg.ShowDialog()
                End Using

            Case "Relationship Test"
                Using dlg As New RelationTestForm
                    dlg.ShowDialog()
                End Using

            Case Else
                MessageBox.Show("Unknown demo.  Please contact tech support.", "List Control Demo",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

        End Select

    End Sub
End Class
