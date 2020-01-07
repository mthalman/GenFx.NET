using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Provides validation for a required value.
    /// </summary>
    public sealed class RequiredValidator : PropertyValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredValidator"/> class.
        /// </summary>
        public RequiredValidator()
        {
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
        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        public override bool IsValid(object? value, string propertyName, object owner, out string? errorMessage)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(Resources.ErrorMsg_StringNullOrEmpty, nameof(propertyName));
            }

            errorMessage = null;

            if ((value is string && String.IsNullOrEmpty((string)value) || value == null))
            {
                errorMessage = StringUtil.GetFormattedString(Resources.ErrorMsg_RequiredPropertyNotSet, propertyName);
            }

            return errorMessage == null;
        }
    }
}
