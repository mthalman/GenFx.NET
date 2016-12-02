using System;
using GenFx.Properties;

namespace GenFx.Validation
{
    /// <summary>
    /// Provides validation for a <see cref="System.Int32"/> value.
    /// </summary>
    public sealed class IntegerValidator : Validator
    {
        private int minValue;
        private int maxValue;

        /// <summary>
        /// Gets the maximum value the integer can have in order to be valid.
        /// </summary>
        public int MaxValue
        {
            get { return this.maxValue; }
        }

        /// <summary>
        /// Gets the minimum value the integer must have in order to be valid.
        /// </summary>
        public int MinValue
        {
            get { return this.minValue; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerValidator"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value the integer must have in order to be valid.</param>
        /// <param name="maxValue">The maximum value the integer must have in order to be valid.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minValue"/> is greater than <paramref name="maxValue"/>.</exception>
        public IntegerValidator(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(FwkResources.ErrorMsg_InvalidMinMaxRange);
            }

            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        /// <summary>
        /// Returns whether <paramref name="value"/> is valid.
        /// </summary>
        /// <param name="value">Object to be validated.</param>
        /// <param name="propertyName">Name of the property being validated.</param>
        /// <param name="owner">The object that owns the property being validated.</param>
        /// <param name="errorMessage">Error message that should be displayed if the property fails validation.</param>
        /// <returns>true if <paramref name="value"/> is valid; otherwise, false.</returns>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is null or empty.</exception>
        public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(FwkResources.ErrorMsg_StringNullOrEmpty, nameof(propertyName));
            }

            bool isValid;

            int intValue;
            if (!ConvertUtil.TryConvert<int>(value, out intValue))
            {
                isValid = false;
            }

            if (intValue >= this.minValue && intValue <= this.maxValue)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }

            if (!isValid)
            {
                if (this.minValue == this.maxValue)
                {
                    errorMessage = StringUtil.GetFormattedString(FwkResources.ErrorMsg_InvalidProperty_Exact, propertyName, this.minValue);
                }
                else
                {
                    errorMessage = StringUtil.GetFormattedString(FwkResources.ErrorMsg_InvalidIntegerProperty,
                      propertyName, this.minValue, this.maxValue);
                }
            }
            else
            {
                errorMessage = null;
            }

            return isValid;
        }
    }
}
