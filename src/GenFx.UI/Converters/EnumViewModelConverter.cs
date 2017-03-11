using GenFx.UI.ViewModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace GenFx.UI.Converters
{
    /// <summary>
    /// Represents a converter that converts an <see cref="EnumViewModel"/> to an enum value.
    /// </summary>
    internal class EnumViewModelConverter : IValueConverter
    {
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
                return null;
            }

            object returnValue = null;

            if (value.GetType() == typeof(FitnessType))
            {
                FitnessType fitnessType = (FitnessType)value;
                switch (fitnessType)
                {
                    case FitnessType.Scaled:
                        returnValue = EnumsViewModel.FitnessTypeScaled;
                        break;
                    case FitnessType.Raw:
                        returnValue = EnumsViewModel.FitnessTypeRaw;
                        break;
                    default:
                        returnValue = null;
                        break;
                }
            }
            else if (value.GetType() == typeof(FitnessSortOption))
            {
                FitnessSortOption fitnessSortOption = (FitnessSortOption)value;
                switch (fitnessSortOption)
                {
                    case FitnessSortOption.Entity:
                        returnValue = EnumsViewModel.FitnessSortByEntity;
                        break;
                    case FitnessSortOption.Fitness:
                        returnValue = EnumsViewModel.FitnessSortByFitness;
                        break;
                    default:
                        returnValue = null;
                        break;
                }
            }

            return returnValue;
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
            if (value == null)
            {
                return null;
            }

            return ((EnumViewModel)value).Value;
        }
    }
}
