using System;
using System.Reflection;
using GenFx.ComponentModel;
using GenFx.Properties;

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
        /// <exception cref="ArgumentException"><paramref name="targetComponentConfigurationType"/> does not derive from <see cref="ComponentConfiguration"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> does not exist on <paramref name="targetComponentConfigurationType"/>.</exception>
        public static void ValidateArguments(Type targetComponentConfigurationType, string targetProperty)
        {
            if (targetComponentConfigurationType == null)
            {
                throw new ArgumentNullException(nameof(targetComponentConfigurationType));
            }

            if (String.IsNullOrEmpty(targetProperty))
            {
                throw new ArgumentException(FwkResources.ErrorMsg_StringNullOrEmpty, nameof(targetProperty));
            }

            if (!typeof(ComponentConfiguration).IsAssignableFrom(targetComponentConfigurationType))
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                    FwkResources.ErrorMsg_ExternalValidator_InvalidTargetType, targetComponentConfigurationType.FullName, typeof(ComponentConfiguration).FullName), nameof(targetComponentConfigurationType));
            }

            if (ExternalValidatorAttributeHelper.GetTargetPropertyInfo(targetComponentConfigurationType, targetProperty) == null)
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                    FwkResources.ErrorMsg_ExternalValidator_PropertyDoesNotExist, targetProperty, targetComponentConfigurationType.FullName), nameof(targetProperty));
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
