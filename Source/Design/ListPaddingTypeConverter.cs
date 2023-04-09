//===============================================================================================================
// System  : EWSoftware Windows Forms List Controls
// File    : ListPaddingTypeConverter.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 04/09/2023
// Note    : Copyright 2005-2023, Eric Woodruff, All rights reserved
//
// This contains a type converter for the padding class so that it can be used in the designer and can be
// serialized to code.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code and can be found at the project website: https://github.com/EWSoftware/ListControls
// This notice, the author's name, and all copyright notices must remain intact in all applications,
// documentation, and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 04/18/2005  EFW  Created the code
// 12/09/2005  EFW  Renamed to avoid conflict with .NET 2.0 class
//===============================================================================================================

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace EWSoftware.ListControls.Design
{
    /// <summary>
    /// This contains a type converter for the <see cref="ListPadding"/> class so that it can be used in the
    /// designer and can be serialized to code.
    /// </summary>
    public class ListPaddingTypeConverter : TypeConverter
    {
        /// <summary>
        /// Determines if this converter can convert an object in the given source type to the native type of the
        /// converter.
        /// </summary>
        /// <param name="context">The format context</param>
        /// <param name="sourceType">The type from which to convert</param>
        /// <returns>Returns true if it can perform the conversion or false if it cannot</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if(sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Gets a value indicating whether this converter can convert an object to the given destination type
        /// using the context.
        /// </summary>
        /// <param name="context">The format context</param>
        /// <param name="destinationType">The type to which to convert</param>
        /// <returns>Returns true if it can perform the conversion or false if it cannot</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if(destinationType == typeof(InstanceDescriptor))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts the given object to a <see cref="ListPadding"/> object.
        /// </summary>
        /// <param name="context">A formatter context</param>
        /// <param name="culture">Culture-specific information</param>
        /// <param name="value">The object to convert</param>
        /// <returns>The converted object</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if(value is string stringValue)
            {
                string[] strParts = stringValue.Split(',');

                if(strParts.Length == 6)
                {
                    return new ListPadding(
                        Convert.ToInt32(strParts[0], CultureInfo.InvariantCulture),
                        Convert.ToInt32(strParts[1], CultureInfo.InvariantCulture),
                        Convert.ToInt32(strParts[2], CultureInfo.InvariantCulture),
                        Convert.ToInt32(strParts[3], CultureInfo.InvariantCulture),
                        Convert.ToInt32(strParts[4], CultureInfo.InvariantCulture),
                        Convert.ToInt32(strParts[5], CultureInfo.InvariantCulture));
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// Convert the specified object to the specified type
        /// </summary>
        /// <param name="context">A formatter context</param>
        /// <param name="culture">Culture-specific information</param>
        /// <param name="value">The object to convert</param>
        /// <param name="destinationType">The type to which to convert</param>
        /// <returns>The converted object</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
          Type destinationType)
        {
            if(destinationType != null)
            {
                if(destinationType == typeof(string) && value != null)
                {
                    ListPadding lp = (ListPadding)value;
                    return String.Format(culture, "{0}, {1}, {2}, {3}, {4}, {5}", lp.Top, lp.Left, lp.Bottom,
                        lp.Right, lp.Column, lp.Row);
                }

                if(destinationType == typeof(InstanceDescriptor) && value != null)
                {
                    ListPadding lp = (ListPadding)value;

                    Type[] ctorParams = new Type[6] { typeof(int), typeof(int), typeof(int), typeof(int),
                        typeof(int), typeof(int) } ;

                    ConstructorInfo ci = typeof(ListPadding).GetConstructor(ctorParams);

                    if(ci != null)
                    {
                        object[] oParams = new object[6] { lp.Top, lp.Left, lp.Bottom, lp.Right, lp.Column, lp.Row };

                        return new InstanceDescriptor(ci, oParams);
                    }
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Creates an instance of this type given a set of property values for the object
        /// </summary>
        /// <param name="context">A formatter context</param>
        /// <param name="propertyValues">A dictionary of new property values</param>
        /// <returns>A new <see cref="ListPadding"/> instance</returns>
        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if(propertyValues == null)
                return null;

            return new ListPadding((int)propertyValues["Top"], (int)propertyValues["Left"],
                (int)propertyValues["Bottom"], (int)propertyValues["Right"], (int)propertyValues["Column"],
                (int)propertyValues["Row"]);
        }

        /// <summary>
        /// Returns a collection of properties for the type of object specified by the value parameter using the
        /// specified context and attributes.
        /// </summary>
        /// <param name="context">A formatter context</param>
        /// <param name="value">The object for which to get properties</param>
        /// <param name="attributes">An array of attributes that describe the properties</param>
        /// <returns>The set of properties that should be exposed for this data type</returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value,
          Attribute[] attributes)
        {
            PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(typeof(ListPadding), attributes);

            string[] props = new[] { "Top", "Left", "Bottom", "Right", "Column", "Row" };

            return pdc.Sort(props);
        }

        /// <summary>
        /// Determines if changing a value on this object should require a call to <see cref="CreateInstance"/>
        /// to create a new value.
        /// </summary>
        /// <param name="context">A formatter context</param>
        /// <returns>Always returns true</returns>
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        /// <summary>
        /// Determines if this object supports properties
        /// </summary>
        /// <param name="context">A formatter context</param>
        /// <returns>Always returns true</returns>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}
