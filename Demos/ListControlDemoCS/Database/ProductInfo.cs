//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : ProductInfo.cs
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
    internal sealed class ProductInfo
    {
        /// <summary>
        /// The primary key
        /// </summary>
        [Key]
        public int ProductID { get; set; }

        /// <summary>
        /// The product name
        /// </summary>
        public string ProductName { get; set; } = null!;

        /// <summary>
        /// The category name
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// The company name
        /// </summary>
        public string? CompanyName { get; set; }

        /// <summary>
        /// The quantity per unit
        /// </summary>
        public string? QuantityPerUnit { get; set; }

        /// <summary>
        /// The unit price
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// The units in stock
        /// </summary>
        public short UnitsInStock { get; set; }

        /// <summary>
        /// The units on order
        /// </summary>
        public short UnitsOnOrder { get; set; }

        /// <summary>
        /// The reorder level
        /// </summary>
        public short ReorderLevel { get; set; }

        /// <summary>
        /// The discontinued flag
        /// </summary>
        [DisplayName("Discontinued?")]
        public bool Discontinued { get; set; }

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
