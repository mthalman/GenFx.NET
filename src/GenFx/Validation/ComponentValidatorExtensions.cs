using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Contains extension methods for the <see cref="ComponentValidator"/> class.
    /// </summary>
    public static class ComponentValidatorExtensions
    {
        /// <summary>
        /// Checks whether the property is valid.
        /// </summary>
        /// <param name="validator"><see cref="ComponentValidator"/> to perform the validation.</param>
        /// <param name="component">The <see cref="GeneticComponent"/> to be validated.</param>
        public static void EnsureIsValid(this ComponentValidator validator, GeneticComponent component)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            string? errorMessage;
            if (!validator.IsValid(component, out errorMessage))
            {
                throw new ValidationException(errorMessage!);
            }
        }
    }
}
