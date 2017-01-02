using GenFx.ComponentModel;
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
        /// <param name="targetComponentConfigurationType"><see cref="Type"/> of the component configuration containing the property to be validated.</param>
        /// <param name="targetProperty">Property of the <paramref name="targetComponentConfigurationType"/> to be validated.</param>
        /// <exception cref="ArgumentNullException"><paramref name="targetComponentConfigurationType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetComponentConfigurationType"/> does not implement <see cref="IComponentConfiguration"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> does not exist on <paramref name="targetComponentConfigurationType"/>.</exception>
        public static void ValidateArguments(Type targetComponentConfigurationType, string targetProperty)
        {
            if (targetComponentConfigurationType == null)
            {
                throw new ArgumentNullException(nameof(targetComponentConfigurationType));
            }

            if (String.IsNullOrEmpty(targetProperty))
            {
                throw new ArgumentException(Resources.ErrorMsg_StringNullOrEmpty, nameof(targetProperty));
            }

            if (!typeof(IComponentConfiguration).IsAssignableFrom(targetComponentConfigurationType))
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                    Resources.ErrorMsg_ExternalValidator_InvalidTargetType, targetComponentConfigurationType.FullName, typeof(IComponentConfiguration).FullName), nameof(targetComponentConfigurationType));
            }

            if (ExternalValidatorAttributeHelper.GetTargetPropertyInfo(targetComponentConfigurationType, targetProperty) == null)
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                    Resources.ErrorMsg_ExternalValidator_PropertyDoesNotExist, targetProperty, targetComponentConfigurationType.FullName), nameof(targetProperty));
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
