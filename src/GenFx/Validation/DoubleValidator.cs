using System;
using GenFx.Properties;

namespace GenFx.Validation
{
    /// <summary>
    /// Provides validation for a <see cref="System.Double"/> value.
    /// </summary>
    public sealed class DoubleValidator : Validator
    {
        private double minValue;
        private double maxValue;
        private bool isMinValueInclusive;
        private bool isMaxValueInclusive;

        /// <summary>
        /// Gets the maximum value the <see cref="System.Double"/> value can have in order to be valid.
        /// </summary>
        public double MaxValue
        {
            get { return this.maxValue; }
        }

        /// <summary>
        /// Gets the minimum value the <see cref="System.Double"/> value must have in order to be valid.
        /// </summary>
        public double MinValue
        {
            get { return this.minValue; }
        }

        /// <summary>
        /// Gets whether the <see cref="MinValue"/> should be regarded as an inclusive value 
        /// when validating the range.
        /// </summary>
        /// <remarks>The default value is true.</remarks>
        public bool IsMinValueInclusive
        {
            get { return this.isMinValueInclusive; }
        }

        /// <summary>
        /// Gets whether the <see cref="MaxValue"/> should be regarded as an inclusive value 
        /// when validating the range.
        /// </summary>
        /// <remarks>The default value is true.</remarks>
        public bool IsMaxValueInclusive
        {
            get { return this.isMaxValueInclusive; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleValidator"/> class.
        /// </summary>
        /// <param name="minValue">The minimum value the <see cref="System.Double"/> value must have in order to be valid.</param>
        /// <param name="isMinValueInclusive">
        /// Whether the <see cref="MinValue"/> should be regarded as an inclusive value when validating the range.
        /// </param>
        /// <param name="maxValue">The maximum value the <see cref="System.Double"/> value can be in order to be valid.</param>
        /// <param name="isMaxValueInclusive">
        /// Whether the <see cref="MaxValue"/> should be regarded as an inclusive value when validating the range.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minValue"/> is greater than <paramref name="maxValue"/>.</exception>
        /// <exception cref="InvalidOperationException">
        /// <paramref name="minValue"/> and <paramref name="maxValue"/> are equal but <paramref name="isMinValueInclusive"/> and 
        /// <paramref name="isMaxValueInclusive"/> are not both set to true.
        /// </exception>
        public DoubleValidator(double minValue, bool isMinValueInclusive, double maxValue, bool isMaxValueInclusive)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(FwkResources.ErrorMsg_InvalidMinMaxRange);
            }
            else if (minValue == maxValue && (!isMinValueInclusive || !isMaxValueInclusive))
            {
                throw new InvalidOperationException(FwkResources.ErrorMsg_InvalidDoubleProperty_EqualMinMaxButNotInclusive);
            }

            this.minValue = minValue;
            this.isMinValueInclusive = isMinValueInclusive;
            this.maxValue = maxValue;
            this.isMaxValueInclusive = isMaxValueInclusive;
        }

        /// <summary>
        /// Returns whether <paramref name="value"/> is valid.
        /// </summary>
        /// <param name="value">Object to be validated.</param>
        /// <param name="propertyName">Name of the property being validated.</param>
        /// <param name="errorMessage">Error message that should be displayed if the property fails validation.</param>
        /// <param name="owner">The object that owns the property being validated.</param>
        /// <returns>true if <paramref name="value"/> is valid; otherwise, false.</returns>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is null.</exception>
        public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(FwkResources.ErrorMsg_StringNullOrEmpty, nameof(propertyName));
            }

            bool isValid = true;

            double dblValue;
            if (!ConvertUtil.TryConvert<double>(value, out dblValue))
            {
                isValid = false;
            }

            if (this.isMinValueInclusive)
            {
                if (dblValue < this.minValue)
                {
                    isValid = false;
                }
            }
            else
            {
                if (dblValue <= this.minValue)
                {
                    isValid = false;
                }
            }

            if (this.isMaxValueInclusive)
            {
                if (dblValue > this.maxValue)
                {
                    isValid = false;
                }
            }
            else
            {
                if (dblValue >= this.maxValue)
                {
                    isValid = false;
                }
            }

            if (!isValid)
            {
                if (this.minValue == this.maxValue)
                {
                    errorMessage = StringUtil.GetFormattedString(FwkResources.ErrorMsg_InvalidProperty_Exact, propertyName, this.minValue);
                }
                else
                {
                    errorMessage = StringUtil.GetFormattedString(FwkResources.ErrorMsg_InvalidDoubleProperty,
                      propertyName, this.minValue, GetInclusiveLabel(this.isMinValueInclusive), this.maxValue, GetInclusiveLabel(this.isMaxValueInclusive));
                }
            }
            else
            {
                errorMessage = null;
            }

            return isValid;
        }

        /// <summary>
        /// Returns the label to use to represent whether a value is inclusive in a range.
        /// </summary>
        /// <param name="isInclusive">Whether a value is inclusive in a range.</param>
        /// <returns>Label to use to represent whether a value is inclusive in a range.</returns>
        private static string GetInclusiveLabel(bool isInclusive)
        {
            if (isInclusive)
            {
                return FwkResources.InclusiveLabel;
            }
            else
            {
                return FwkResources.ExclusiveLabel;
            }
        }
    }
}
