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
        /// <param name="algorithmContext">The <see cref="GeneticAlgorithm"/> currently in context.</param>
        public static void EnsureIsValid(this ComponentValidator validator, GeneticComponent component, GeneticAlgorithm algorithmContext)
        {
            if (validator == null)
            {
                throw new ArgumentNullException(nameof(validator));
            }

            string errorMessage;
            if (!validator.IsValid(component, algorithmContext, out errorMessage))
            {
                throw new ValidationException(errorMessage);
            }
        }
    }
}
