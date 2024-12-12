//===============================================================================================================
// System  : EWSoftware Data List Control Demonstration Applications
// File    : Address.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 12/06/2024
// Note    : Copyright 2024, Eric Woodruff, All rights reserved
//
// This is class is used to contain name and address information
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 12/06/2024  EFW  Created the code
//===============================================================================================================

// This class is used but is created dynamically when needed
#pragma warning disable CA1812

using System.Runtime.CompilerServices;

namespace ListControlDemoCS.Database
{
    /// <summary>
    /// This is used to contain name and address information
    /// </summary>
    internal sealed class Address : INotifyPropertyChanging, INotifyPropertyChanged
    {
        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to get or set the ID
        /// </summary>
        [Key]
        public int ID
        {
            get;
            set => this.SetWithNotify(value, ref field);
        }

        /// <summary>
        /// This is used to get or set the first name
        /// </summary>
        public string FirstName
        {
            get;
            set => this.SetWithNotify(value, ref field);
        } = null!;

        /// <summary>
        /// This is used to get or set the last name
        /// </summary>
        public string LastName
        {
            get;
            set => this.SetWithNotify(value, ref field);
        } = null!;

        /// <summary>
        /// This is used to get or set the street address
        /// </summary>
        public string? StreetAddress
        {
            get;
            set
            {
                if(String.IsNullOrWhiteSpace(value))
                    value = null;

                this.SetWithNotify(value, ref field);
            }
        }

        /// <summary>
        /// This is used to get or set the city
        /// </summary>
        public string? City
        {
            get;
            set
            {
                if(String.IsNullOrWhiteSpace(value))
                    value = null;

                this.SetWithNotify(value, ref field);
            }
        }

        /// <summary>
        /// This is used to get or set the state
        /// </summary>
        public string? State
        {
            get;
            set
            {
                if(String.IsNullOrWhiteSpace(value))
                    value = null;

                this.SetWithNotify(value, ref field);
            }
        }

        /// <summary>
        /// This is used to get or set the ZIP code
        /// </summary>
        public string? Zip
        {
            get;
            set
            {
                if(String.IsNullOrWhiteSpace(value))
                    value = null;

                this.SetWithNotify(value, ref field);
            }
        }

        /// <summary>
        /// This is used to get or set the sum value
        /// </summary>
        public int? SumValue
        {
            get;
            set => this.SetWithNotify(value, ref field);
        }

        /// <summary>
        /// This is used to get or set the domestic address flag
        /// </summary>
        public bool Domestic
        {
            get;
            set => this.SetWithNotify(value, ref field);
        }

        /// <summary>
        /// This is used to get or set the international address flag
        /// </summary>
        public bool International
        {
            get;
            set => this.SetWithNotify(value, ref field);
        }

        /// <summary>
        /// This is used to get or set the postal address flag
        /// </summary>
        public bool Postal
        {
            get;
            set => this.SetWithNotify(value, ref field);
        }

        /// <summary>
        /// This is used to get or set the parcel address flag
        /// </summary>
        public bool Parcel
        {
            get;
            set => this.SetWithNotify(value, ref field);
        }

        /// <summary>
        /// This is used to get or set the home address flag
        /// </summary>
        public bool Home
        {
            get;
            set => this.SetWithNotify(value, ref field);
        }

        /// <summary>
        /// This is used to get or set the business address flag
        /// </summary>
        public bool Business
        {
            get;
            set => this.SetWithNotify(value, ref field);
        }

        /// <summary>
        /// This is used to get or set the contact type
        /// </summary>
        public char? ContactType
        {
            get;
            set => this.SetWithNotify(value, ref field);
        }

        /// <summary>
        /// This is used to get or set the row time stamp (row version)
        /// </summary>
        [Timestamp]
        public byte[]? LastModified { get; set; }

        /// <summary>
        /// The phone numbers related to this address
        /// </summary>
        public ICollection<Phone> PhoneNumbers { get; set; } = [];

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
