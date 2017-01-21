using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates how the attributed <see cref="FitnessType"/> configuration property should be validated when set.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FitnessTypeValidatorAttribute : PropertyValidatorAttribute
    {
        /// <summary>
        /// Returns the associated <see cref="PropertyValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="PropertyValidator"/> object.</returns>
        protected override PropertyValidator CreateValidator()
        {
            return new FitnessTypeValidator();
        }
    }
}
