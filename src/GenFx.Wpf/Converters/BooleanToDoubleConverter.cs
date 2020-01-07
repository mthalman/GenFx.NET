using System;
using System.Globalization;
using System.Windows.Data;

namespace GenFx.Wpf.Converters
{
    /// <summary>
    /// Represents a converter that converts a <see cref="Boolean"/> to a <see cref="Double"/> value.
    /// </summary>
    internal class BooleanToDoubleConverter : IValueConverter
    {
        /// <summary>
        /// Gets or sets the <see cref="Double"/> value to return for a false input value.
        /// </summary>
        public double ValueForFalse { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Double"/> value to return for a true input value.
        /// </summary>
        public double ValueForTrue { get; set; }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            if ((bool)value)
            {
                return this.ValueForTrue;
            }
            else
            {
                return this.ValueForFalse;
            }
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
