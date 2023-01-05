//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : AssemblyInfo.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 01/04/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
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

using System;
using System.Reflection;
using System.Runtime.InteropServices;

// General assembly information
[assembly: AssemblyTitle("EWSoftware Windows Forms List Controls")]

// The assembly is CLS compliant
[assembly: CLSCompliant(true)]

// Not visible to COM
[assembly: ComVisible(false)]

#if NET6_0_OR_GREATER
[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]
#endif

// Version numbers.  All version numbers for an assembly consists of the following four values:
//
//      Year of release
//      Month of release
//      Day of release
//      Revision (typically zero unless multiple releases are made on the same day)
//

// Common assembly strong name version - DO NOT CHANGE UNLESS NECESSARY.
//
// This is used to set the assembly version in the strong name.  This should remain unchanged to maintain binary
// compatibility with prior releases.  It should only be changed if a breaking change is made that requires
// assemblies that reference older versions to be recompiled against the newer version.
[assembly: AssemblyVersion("2023.1.4.0")]

// Common assembly file version
//
// This is used to set the assembly file version.  This will change with each new release.  MSIs only support a
// Major value between 0 and 255 so we drop the century from the year on this one.
[assembly: AssemblyFileVersion("23.1.4.0")]

// Common product version
//
// This may contain additional text to indicate Alpha or Beta states.  The version number will always match the
// file version above but includes the century on the year.
[assembly: AssemblyInformationalVersion("2023.1.4.0")]
