//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : AssemblyInfo.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 11/25/2020
// Note    : Copyright 2005-2020, Eric Woodruff, All rights reserved
//
// EWSoftware Window Forms list control demo application
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/17/2005  EFW  Created the code
// 01/09/2007  EFW  Added the ExtendedTreeView control
// 04/23/2007  EFW  Added the DataGridView column controls
//===============================================================================================================

using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

// General assembly information
[assembly: AssemblyProduct("EWSoftware Windows Forms List Controls")]
[assembly: AssemblyTitle("EWSoftware Windows Forms List Controls Demo")]
[assembly: AssemblyDescription("This is used to demonstrate the capabilities of the EWSoftware List Controls.")]
[assembly: AssemblyCompany("Eric Woodruff")]
[assembly: AssemblyCopyright("Copyright \xA9 2005-2020, Eric Woodruff, All Rights Reserved")]
[assembly: AssemblyCulture("")]
#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

// The assembly is CLS compliant
[assembly: CLSCompliant(true)]

// Not visible to COM
[assembly: ComVisible(false)]

// Resources contained within the assembly are English
[assembly: NeutralResourcesLanguageAttribute("en")]

// Version numbers.  All version numbers for an assembly consists of the following four values:
//
//      Year of release
//      Month of release
//      Day of release
//      Revision (typically zero unless multiple releases are made on the same day)
//
[assembly: AssemblyVersion("2020.11.25.0")]
[assembly: AssemblyFileVersion("20.11.25.0")]
