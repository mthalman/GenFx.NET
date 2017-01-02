using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Provides validation for a <see cref="System.Int32"/> value.
    /// </summary>
    public sealed class IntegerValidator : Validator
    {
        /// <summary>
        /// Gets the maximum value the integer can have in order to be valid.
        /// </summary>
        public int MaxValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the minimum value the integer must have in order to be valid.
        /// </summary>
        public int MinValue
        {
            get;
            private set;
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
                throw new ArgumentOutOfRangeException(Resources.ErrorMsg_InvalidMinMaxRange);
            }

            this.MinValue = minValue;
            this.MaxValue = maxValue;
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
                throw new ArgumentException(Resources.ErrorMsg_StringNullOrEmpty, nameof(propertyName));
            }

            bool isValid;

            int intValue;
            if (!ConvertUtil.TryConvert<int>(value, out intValue))
            {
                isValid = false;
            }

            if (intValue >= this.MinValue && intValue <= this.MaxValue)
            {
                isValid = true;
            }
            else
            {
                isValid = false;
            }

            if (!isValid)
            {
                if (this.MinValue == this.MaxValue)
                {
                    errorMessage = StringUtil.GetFormattedString(Resources.ErrorMsg_InvalidProperty_Exact, propertyName, this.MinValue);
                }
                else
                {
                    errorMessage = StringUtil.GetFormattedString(Resources.ErrorMsg_InvalidIntegerProperty,
                      propertyName, this.MinValue, this.MaxValue);
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
