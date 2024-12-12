//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : DemoTable.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/02/2024
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
        [Key]
        public int ListKey { get; set; }

        /// <summary>
        /// A label value
        /// </summary>
        public string Label { get; set; } = null!;

        /// <summary>
        /// A text value
        /// </summary>
        public string TextValue { get; set; } = null!;

        /// <summary>
        /// A date value
        /// </summary>
        public DateTime DateValue { get; set; }

        /// <summary>
        /// A Boolean value
        /// </summary>
        public bool BoolValue { get; set; }
    }
}
