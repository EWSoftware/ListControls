---
uid: a3a3d71f-2d20-4506-bab1-638af4a36f4b
alt-uid: UsageNotes
title: Usage Notes
keywords: usage notes
---

<autoOutline lead="This topic contains general usage notes for the control library." />

## Installing the Assemblies in the Global Assembly Cache
The `EWSoftware.ListControls` assembly has a strong name.  As such, you can install the assembly in the Global
Assembly Cache (GAC) if so desired.  To do so, use the **GACUTIL** command line tool as follows replacing the
path to the assemblies to match the installed location on your system:


``` none
gacutil -i \GH\ListControls\Deployment\EWSoftware.ListControls.dll
```

To remove the assembly from the Global Assembly Cache, use the **GACUTIL** command line tool as follows:

``` none
gacutil -u EWSoftware.ListControls
```

## Installing the Controls in the Visual Studio Toolbox
Installing the controls in the Visual Studio toolbox will allow you to use the controls at design time by
dragging them from the toolbox and dropping them on to your form.  To install them for the first time or to
refresh the toolbox after the control assemblies have been updated, do the following:

- Open the toolbox window.  Right click on it and select the **Show All** option.
- If you do not currently have a tab for the control library, create one by right clicking on the toolbox window,
  selecting **Add Tab**, and then typing in a name for the tab such as **EWSoftware List Controls**.
- Click on the tab for the control library so that it is selected.
- If the tab is not empty, remove the existing controls by doing the following:
  - Right click on the toolbox tab and select **Choose Items...**.
  - In the dialog box that opens, select the **.NET Framework Components** tab.  Click on the **Namespace**
    header to sort the list of controls by namespace.
  - Locate the `EWSoftware.ListControls` namespaces and uncheck all of the controls in those namespaces.
  - Click the **OK** button and the controls will be removed from the toolbox.  You are now ready to add the new
    version of the control library.
- To add the controls to the toolbox tab, do the following:
  - Right click on the toolbox tab and select **Choose Items...**.
  - In the dialog box that opens, select the **.NET Framework Components** tab.  Click on the **Namespace**
    header to sort the list of controls by namespace.
  - Click on the **Browse** button and navigate to where the *EWSoftware.ListControls.dll* assembly is stored.
    Select it and click the **Open** button.  This will add the controls in the assembly to the list in the
    dialog box.  They will all be selected and checked by default.
  - Click the **OK** button to complete the process and the new controls will appear in the toolbox tab.

## Using the Assembly in Your Projects
> [!TIP]
> A NuGet package is available for the controls.  Search for the package name *EWSoftware.ListControls*.

The classes can be found in the assembly *EWSoftware.ListControls.dll*.  In order to use the classes, add a
reference to the assembly in your project.  To add the reference, open your project in Visual Studio, open the
**Solution Explorer** pane of the project, right click on the **References** folder, select **Add Reference...**,
click the **Browse** button and navigate to the folder in which you placed the assembly, and finally select the
*EWSoftware.ListControls.dll* assembly and click **OK** to close the dialogs and add the reference.

In code modules that use classes from the `EWSoftware.ListControls` namespace, you will have to add a `using`
statement (`Imports` in VB.NET) for the `EWSoftware.ListControls` namespace.

## Design-time Support
All controls provide rich design-time support.  They appear on the design surface as they do at runtime.  Their
properties can be edited using the normal **Properties** window.

## Demonstration Applications
Windows Forms applications in C# and VB.NET are provided that demonstrate the basic use of the various list
controls. For the combo box demos, the form presents an auto-complete and a multi-column or user control combo
box control.  A property grid can be used to modify the multi-column/user control combo box's properties at
runtime to try out its various features.  The radio button list and checkbox list demos work in a similar
fashion.  The data list demo provide a simple template for editing rows in the supplied name and address database
and provides an example of how to implement templates for your own data list needs.  It also contains examples of
drag and drop operations utilizing the data list.  The data navigator demo uses the same name and address
database but displays the information one row at a time using the data navigator control.  An example is also
provided that shows the use of the data navigator in conjunction with a data list to edit names and addresses and
related phone number information.  The extended tree view demo contains examples of the various drawing
enhancements and its additional features such as node enumeration.

For additional information on the features of each control and how to use them, see the
[](@ce5cb77a-44a9-4629-aec3-6a8435cd8e53) section of this help file.

## See Also
**Other Resources**  
[](@ca3b503d-654e-4fe6-8ca3-ec3ea8fb9a1a)  
[](@ce5cb77a-44a9-4629-aec3-6a8435cd8e53)  
