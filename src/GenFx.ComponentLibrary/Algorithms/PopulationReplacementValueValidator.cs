using GenFx.ComponentLibrary.Properties;
using GenFx.Validation;
using System;

namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// Validator for the <see cref="PopulationReplacementValue"/> type.
    /// </summary>
    internal class PopulationReplacementValueValidator : Validator
    {
        /// <summary>
        /// Returns whether <paramref name="value"/> is valid.
        /// </summary>
        /// <param name="value">Object to be validated.</param>
        /// <param name="propertyName">Name of the property being validated.</param>
        /// <param name="errorMessage">Error message that should be displayed if the property fails validation.</param>
        /// <param name="owner">The object that owns the property being validated.</param>
        /// <returns>true if <paramref name="value"/> is valid; otherwise, false.</returns>
        public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
        {
            bool isValid;

            if (!(value is PopulationReplacementValue))
            {
                isValid = false;
            }
            else
            {
                PopulationReplacementValue popReplacementVal = (PopulationReplacementValue)value;
                int maxValue;
                if (popReplacementVal.Kind == ReplacementValueKind.Percentage)
                {
                    maxValue = 100;
                }
                else
                {
                    maxValue = Int32.MaxValue;
                }

                IntegerValidator intValidator = new IntegerValidator(0, maxValue);
                string tempErrorMsg;
                isValid = intValidator.IsValid(popReplacementVal.Value, propertyName, owner, out tempErrorMsg);
            }

            if (!isValid)
            {
                errorMessage = StringUtil.GetFormattedString(LibResources.ErrorMsg_InvalidPopulationReplacementValue, propertyName);
            }
            else
            {
                errorMessage = null;
            }

            return isValid;
        }
    }
}
