﻿//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : DemoTable.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/11/2024
// Note    : Copyright 2024, Eric Woodruff, All rights reserved
//
// This is class is used to contain demo data from the test database for some of the example forms
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/02/2024  EFW  Created the code
//===============================================================================================================

// This class is used but is created dynamically when needed
#pragma warning disable CA1812

namespace ListControlDemoCS.Database
{
    /// <summary>
    /// This is used to contain demo data for some of the example forms
    /// </summary>
    internal sealed class DemoTable
    {
        /// <summary>
        /// The primary key
        /// </summary>
        [Key, DisplayName("Primary Key")]
        public int ListKey { get; set; }

        /// <summary>
        /// A label value
        /// </summary>
        public string Label { get; set; } = null!;

        /// <summary>
        /// A text value
        /// </summary>
        [DisplayName("Text")]
        public string TextValue { get; set; } = null!;

        /// <summary>
        /// A date value
        /// </summary>
        [DisplayName("Date/Time")]
        public DateTime DateValue { get; set; }

        /// <summary>
        /// A Boolean value
        /// </summary>
        [DisplayName("Boolean")]
        public bool BoolValue { get; set; }

        /// <summary>
        /// This is just a test column use to demonstrate the Browsable(false) attribute support
        /// </summary>
        [Browsable(false)]
        public bool HiddenColumn { get; set; }

        /// <summary>
        /// This is used to get or set the row time stamp (row version)
        /// </summary>
        [Timestamp, Browsable(false)]
        public byte[]? LastModified { get; set; }
    }
}
