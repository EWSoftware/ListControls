//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : AssemblyInfo.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/07/2024
// Note    : Copyright 2005-2024, Eric Woodruff, All rights reserved
//
// Window Forms list controls library attributes
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 03/01/2005  EFW  Created the code
// 12/09/2005  EFW  Various improvements, fixes, and modifications
// 05/01/2006  EFW  Added some new properties
// 01/09/2007  EFW  Added the ExtendedTreeView control
// 04/23/2007  EFW  Added DataGridView column controls
// 07/22/2014  EFW  Updated for use with .NET 4.0 and converted the project to open source
//===============================================================================================================

using System.Runtime.InteropServices;

// The assembly is CLS compliant
[assembly: CLSCompliant(true)]

// Not visible to COM
[assembly: ComVisible(false)]

#if NET8_0_OR_GREATER
[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]
#endif

//
// PLEASE NOTE:
// Version information for an assembly consists of the following four values based on the current date:
//
//      Year of release     4 digit year
//      Month of release    1 or 2 digit month
//      Day of release      1 or 2 digit day
//      Revision            Typically zero unless multiple releases are made on the same day.  In such cases,
//                          increment the revision by one with each same day release.
//
// Set the version in the Version and FileVersion properties in project file.
