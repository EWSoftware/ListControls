'================================================================================================================
' System  : EWSoftware Data List Control Demonstration Applications
' File    : AssemblyInfo.vb
' Author  : Eric Woodruff  (Eric@EWoodruff.us)
' Updated : 01/19/2015
' Note    : Copyright 2005-2015, Eric Woodruff, All rights reserved
' Compiler: Microsoft Visual Basic .NET
'
' EWSoftware Window Forms list control demo application
'
' This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
' distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
' This notice, the author's name, and all copyright notices must remain intact in all applications,
' documentation, and source files.
'
'    Date     Who  Comments
' ===============================================================================================================
' 10/27/2005  EFW  Created the code
' 01/09/2007  EFW  Added the ExtendedTreeView control
' 04/23/2007  EFW  Added the DataGridView column controls
'================================================================================================================

Imports System
Imports System.Reflection
Imports System.Resources
Imports System.Runtime.InteropServices

' General assembly information
<Assembly: AssemblyProduct("EWSoftware Windows Forms List Controls")>
<Assembly: AssemblyTitle("EWSoftware Windows Forms List Controls Demo")>
<Assembly: AssemblyDescription("This is used to demonstrate the capabilities of the EWSoftware List Controls.")>
<Assembly: AssemblyCompany("Eric Woodruff")>
<Assembly: AssemblyCopyright("Copyright \xA9 2005-2015, Eric Woodruff, All Rights Reserved")>
<Assembly: AssemblyCulture("")>
#If DEBUG
<Assembly: AssemblyConfiguration("Debug")>
#Else
<Assembly: AssemblyConfiguration("Release")>
#End If

' The assembly is CLS compliant
<Assembly: CLSCompliant(true)>

' Not visible to COM
<Assembly: ComVisible(false)>

' Resources contained within the assembly are English
<Assembly: NeutralResourcesLanguageAttribute("en")>

' Version numbers.  All version numbers for an assembly consists of the following four values:
'
'      Year of release
'      Month of release
'      Day of release
'      Revision (typically zero unless multiple releases are made on the same day)
'
<Assembly: AssemblyVersion("2015.1.19.0")>
<Assembly: AssemblyFileVersion("15.1.19.0")>
