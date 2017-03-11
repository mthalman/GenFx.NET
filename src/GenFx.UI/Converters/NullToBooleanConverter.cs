using System;
using System.Globalization;
using System.Windows.Data;

namespace GenFx.UI.Converters
{
    /// <summary>
    /// Represents a converter that converts a null value (or lack of) to a boolean value.
    /// </summary>
    internal class NullToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets the value to return if the input value is null.
        /// </summary>
        public bool ValueForNull { get; set; }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return this.ValueForNull;
            }

            return !this.ValueForNull;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
