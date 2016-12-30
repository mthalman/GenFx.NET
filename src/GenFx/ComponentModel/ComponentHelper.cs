using GenFx.Validation;

namespace GenFx.ComponentModel
{
    /// <summary>
    /// Contains helper methods for the component model.
    /// </summary>
    internal static class ComponentHelper
    {
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
