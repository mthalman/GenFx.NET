using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace GenFx
{
    /// <summary>
    /// Helper class used to convert values.
    /// </summary>
    internal static class ConvertUtil
    {
        /// <summary>
        /// Returns whether <paramref name="value"/> can be converted to the type indicated by <paramref name="conversionType"/>.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="conversionType">Type the value should be converted to.</param>
        /// <returns>true if the value can be converted; otherwise, false.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public static bool CanConvert(object value, Type conversionType)
        {
            object convertedValue;
            return TryConvert(value, conversionType, out convertedValue);
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a type of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type that <paramref name="value"/> should be converted to.</typeparam>
        /// <param name="value">Value to be converted.</param>
        /// <param name="convertedValue">Value the converted value is set to.</param>
        /// <returns>True if the conversion was successful; otherwise, false.</returns>
        public static bool TryConvert<T>(object value, out T convertedValue)
        {
            object objVal;
            if (TryConvert(value, typeof(T), out objVal))
            {
                convertedValue = (T)objVal;
                return true;
            }
            else
            {
                convertedValue = default(T);
                return false;
            }
        }

        /// <summary>
        /// Converts <paramref name="value"/> to a type of <paramref name="conversionType"/>.
        /// </summary>
        /// <param name="value">Value to be converted.</param>
        /// <param name="conversionType">Type the value should be converted to.</param>
        /// <param name="convertedValue">Value the converted value is set to.</param>
        /// <returns>True if the conversion was successful; otherwise, false.</returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static bool TryConvert(object value, Type conversionType, out object convertedValue)
        {
            if (value == null)
            {
                convertedValue = null;
                if (!conversionType.IsValueType)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            if (value.GetType() == conversionType)
            {
                convertedValue = value;
                return true;
            }

            if (conversionType.IsEnum)
            {
                if (Enum.IsDefined(conversionType, value))
                {
                    string valueString = value as string;
                    if (valueString != null)
                    {
                        convertedValue = Enum.Parse(conversionType, valueString);
                    }
                    else
                    {
                        convertedValue = Enum.ToObject(conversionType, value);
                    }
                    return true;
                }
                else
                {
                    convertedValue = null;
                    return false;
                }
            }

            if (value is IConvertible && Type.GetTypeCode(conversionType) != TypeCode.Object)
            {
                try
                {
                    BasicType sourceBasicType = GetBasicType(value.GetType());
                    BasicType targetBasicType = GetBasicType(conversionType);

                    if (sourceBasicType == BasicType.Real && targetBasicType == BasicType.Integral)
                    {
                        convertedValue = null;
                        return false;
                    }

                    convertedValue = Convert.ChangeType(value, conversionType, CultureInfo.InvariantCulture);
                    return true;
                }
                catch (InvalidCastException)
                {
                }
                catch (FormatException)
                {
                }
            }

            try
            {
                TypeConverter converter = TypeDescriptor.GetConverter(conversionType);
                if (converter != null && converter.CanConvertFrom(value.GetType()) && converter.IsValid(value))
                {
                    convertedValue = converter.ConvertFrom(value);
                    return true;
                }

                converter = TypeDescriptor.GetConverter(value);
                if (converter != null && converter.CanConvertTo(conversionType))
                {
                    convertedValue = converter.ConvertTo(value, conversionType);
                    return true;
                }
            }
            catch (Exception)
            {
            }

            convertedValue = null;
            return false;
        }

        /// <summary>
        /// Returns the <see cref="BasicType"/> corresponding to <paramref name="type"/>.
        /// </summary>
        /// <param name="type"><see cref="Type"/> whose corresponding <see cref="BasicType"/> value should be returned.</param>
        /// <returns><see cref="BasicType"/> corresponding to <paramref name="type"/>.</returns>
        private static BasicType GetBasicType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return BasicType.Integral;
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return BasicType.Real;
                default:
                    return BasicType.Other;
            }
        }

        /// <summary>
        /// Represents a basic type of value.
        /// </summary>
        private enum BasicType
        {
            /// <summary>
            /// Indicates a value that is an integral value.
            /// </summary>
            Integral,

            /// <summary>
            /// Indicates a value that is a real value.
            /// </summary>
            Real,

            /// <summary>
            /// Indicates some other kind of value.
            /// </summary>
            Other
        }
    }
}
