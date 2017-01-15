using GenFx.Contracts;
using System;
using System.Reflection;

namespace GenFx.Validation
{
    /// <summary>
    /// Helper class to provide functionality for classes that implement <see cref="IExternalConfigurationValidatorAttribute"/>.
    /// </summary>
    internal static class ExternalValidatorAttributeHelper
    {
        /// <summary>
        /// Validates that the arguments are correctly set.
        /// </summary>
        /// <param name="targetComponentType"><see cref="Type"/> of the component configuration containing the property to be validated.</param>
        /// <param name="targetProperty">Property of the <paramref name="targetComponentType"/> to be validated.</param>
        /// <exception cref="ArgumentNullException"><paramref name="targetComponentType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetComponentType"/> does not implement <see cref="IGeneticComponent"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> does not exist on <paramref name="targetComponentType"/>.</exception>
        public static void ValidateArguments(Type targetComponentType, string targetProperty)
        {
            if (targetComponentType == null)
            {
                throw new ArgumentNullException(nameof(targetComponentType));
            }

            if (String.IsNullOrEmpty(targetProperty))
            {
                throw new ArgumentException(Resources.ErrorMsg_StringNullOrEmpty, nameof(targetProperty));
            }

            if (!typeof(IGeneticComponent).IsAssignableFrom(targetComponentType))
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                    Resources.ErrorMsg_ExternalValidator_InvalidTargetType, targetComponentType.FullName, typeof(IGeneticComponent).FullName), nameof(targetComponentType));
            }

            if (ExternalValidatorAttributeHelper.GetTargetPropertyInfo(targetComponentType, targetProperty) == null)
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                    Resources.ErrorMsg_ExternalValidator_PropertyDoesNotExist, targetProperty, targetComponentType.FullName), nameof(targetProperty));
            }
        }

        /// <summary>
        /// Returns the <see cref="PropertyInfo"/> for the target property.
        /// </summary>
        /// <param name="targetComponentConfigurationType"><see cref="Type"/> of the component configuration containing the property to be validated.</param>
        /// <param name="targetProperty">Property of the <paramref name="targetComponentConfigurationType"/> to be validated.</param>
        /// <returns><see cref="PropertyInfo"/> for the target property.</returns>
        internal static PropertyInfo GetTargetPropertyInfo(Type targetComponentConfigurationType, string targetProperty)
        {
            return targetComponentConfigurationType.GetProperty(targetProperty, BindingFlags.Instance | BindingFlags.Public);
        }
    }
}
