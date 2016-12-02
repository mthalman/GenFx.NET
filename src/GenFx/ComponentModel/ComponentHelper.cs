using System;
using System.Diagnostics;
using System.Reflection;
using GenFx.Properties;
using System.Collections.Generic;
using GenFx.Validation;

namespace GenFx.ComponentModel
{
    /// <summary>
    /// Contains helper methods for the component model.
    /// </summary>
    public static class ComponentHelper
    {
        /// <summary>
        /// Verifies that all the user-defined properties of the configuration have valid values.
        /// </summary>
        /// <param name="componentConfiguration">The <see cref="ComponentConfiguration"/> to validate.</param>
        /// <param name="externalValidationMapping">Mapping of component configuration properties to <see cref="Validator"/> objects as described by external components.</param>
        internal static void Validate(ComponentConfiguration componentConfiguration, Dictionary<PropertyInfo, List<Validator>> externalValidationMapping)
        {
            PropertyInfo[] properties = componentConfiguration.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            foreach (PropertyInfo propertyInfo in properties)
            {
                // Don't bother validating properties on the base class since these aren't user-defined.
                if (propertyInfo.DeclaringType == typeof(ComponentConfiguration))
                {
                    return;
                }

                object propValue = propertyInfo.GetValue(componentConfiguration, null);

                // Check that the property is valid using the validators attached directly to the property.
                componentConfiguration.ValidateProperty(propValue, propertyInfo.Name);

                // Check that the property is valid using the validators described be external components.
                List<Validator> externalValidators;
                if (externalValidationMapping.TryGetValue(propertyInfo, out externalValidators))
                {
                    foreach (Validator validator in externalValidators)
                    {
                        ComponentHelper.CheckValidation(validator, componentConfiguration.GetType().Name + Type.Delimiter + propertyInfo.Name, propValue, componentConfiguration);
                    }
                }
            }
        }

        /// <summary>
        /// Checks whether the property is valid.
        /// </summary>
        /// <param name="validator"><see cref="Validator"/> to perform the validation.</param>
        /// <param name="propertyName">Name of the property being validated.</param>
        /// <param name="value">Property value to check.</param>
        /// <param name="owner">The object that owns the property being validated.</param>
        internal static void CheckValidation(Validator validator, string propertyName, object value, object owner)
        {
            string errorMessage;
            if (!validator.IsValid(value, propertyName, owner, out errorMessage))
            {
                throw new ValidationException(errorMessage);
            }
        }
    }
}
