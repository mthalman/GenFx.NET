using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Contains extension methods for the <see cref="PropertyValidator"/> class.
    /// </summary>
    public static class PropertyValidatorExtensions
    {
        /// <summary>
        /// Checks whether the property is valid.
        /// </summary>
        /// <param name="validator"><see cref="PropertyValidator"/> to perform the validation.</param>
        /// <param name="propertyName">Name of the property being validated.</param>
        /// <param name="value">Property value to check.</param>
        /// <param name="owner">The object that owns the property being validated.</param>
        public static void EnsureIsValid(this PropertyValidator validator, string propertyName, object? value, object owner)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            string? errorMessage;
            if (!validator.IsValid(value, propertyName, owner, out errorMessage))
            {
                throw new ValidationException(errorMessage!);
            }
        }
    }
}
