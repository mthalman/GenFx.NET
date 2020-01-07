using System;
using System.Globalization;
using System.Windows.Data;

namespace GenFx.Wpf.Converters
{
    /// <summary>
    /// Represents a converter that converts an <see cref="ExecutionState"/> to a string.
    /// </summary>
    internal class ExecutionStateToStringConverter : IValueConverter
    {
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

            string? displayValue;
            switch ((ExecutionState)value)
            {
                case ExecutionState.Idle:
                    displayValue = Resources.ExecutionState_Idle;
                    break;
                case ExecutionState.IdlePending:
                    displayValue = Resources.ExecutionState_IdlePending;
                    break;
                case ExecutionState.Paused:
                    displayValue = Resources.ExecutionState_Paused;
                    break;
                case ExecutionState.PausePending:
                    displayValue = Resources.ExecutionState_PausePending;
                    break;
                case ExecutionState.Running:
                    displayValue = Resources.ExecutionState_Running;
                    break;
                default:
                    displayValue = null;
                    break;
            }

            return displayValue;
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
