using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Provides validation for a <see cref="System.Boolean"/> value.
    /// </summary>
    public sealed class BooleanValidator : PropertyValidator
    {
        /// <summary>
        /// Gets the boolean value that the property must have.
        /// </summary>
        public bool RequiredValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanValidator"/> class.
        /// </summary>
        /// <param name="requiredValue">The boolean value that the property must have.</param>
        public BooleanValidator(bool requiredValue)
        {
            this.RequiredValue = requiredValue;
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public override bool IsValid(object? value, string propertyName, object owner, out string? errorMessage)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(Resources.ErrorMsg_StringNullOrEmpty, nameof(propertyName));
            }

            bool isValid;

            if (!(value is bool))
            {
                isValid = false;
            }
            else
            {
                isValid = (bool)value == this.RequiredValue;
            }

            if (!isValid)
            {
                errorMessage = StringUtil.GetFormattedString(Resources.ErrorMsg_InvalidBooleanProperty,
                    propertyName, this.RequiredValue);
            }
            else
            {
                errorMessage = null;
            }

            return isValid;
        }
    }
}
