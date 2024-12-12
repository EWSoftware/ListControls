//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : Phone.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/08/2024
// Note    : Copyright 2024, Eric Woodruff, All rights reserved
//
// This is class is used to contain phone number information
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/08/2024  EFW  Created the code
//===============================================================================================================

// This class is used but is created dynamically when needed
#pragma warning disable CA1812

using System.Runtime.CompilerServices;

namespace ListControlDemoCS.Database
{
    /// <summary>
    /// This is used to contain phone number information
    /// </summary>
    internal sealed class Phone : INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to get or set the phone key
        /// </summary>
        [Key]
        public int PhoneKey
        {
            get;
            set => this.SetWithNotify(value, ref field);
        }

        /// <summary>
        /// This is used to get or set the related address ID
        /// </summary>
        public int ID
        {
            get;
            set => this.SetWithNotify(value, ref field);
        }

        /// <summary>
        /// This is used to get or set the phone number
        /// </summary>
        public string PhoneNumber
        {
            get;
            set => this.SetWithNotify(value, ref field);
        } = null!;

        /// <summary>
        /// This is used to get or set the row time stamp (row version)
        /// </summary>
        [Timestamp]
        public byte[]? LastModified { get; set; }

        /// <summary>
        /// The parent address of this phone number
        /// </summary>
        public Address Address { get; set; } = null!;

        #endregion

        #region Change notification interface implementations
        //=====================================================================

        /// <inheritdoc />
        public event PropertyChangingEventHandler? PropertyChanging;

        /// <inheritdoc />
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// This is used to raise the <see cref="PropertyChanging"/> and <see cref="PropertyChanged"/> events
        /// if the new value does not equal the current property's value
        /// </summary>
        /// <typeparam name="T">The property type</typeparam>
        /// <param name="value">The new value</param>
        /// <param name="field">A reference to the field containing the current value that will receive the
        /// new value</param>
        /// <param name="propertyName">The property name that changed.  This defaults to the calling member's
        /// name if not specified</param>
        private void SetWithNotify<T>(T value, ref T field, [CallerMemberName] string propertyName = "")
        {
            if(!Equals(field, value))
            {
                PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
