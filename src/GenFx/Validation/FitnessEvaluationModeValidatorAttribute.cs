using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates how the attributed <see cref="FitnessEvaluationMode"/> configuration property should be validated when set.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FitnessEvaluationModeValidatorAttribute : ConfigurationValidatorAttribute
    {
        /// <summary>
        /// Returns the associated <see cref="Validator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="Validator"/> object.</returns>
        protected override Validator CreateValidator()
        {
            return new FitnessEvaluationModeValidator();
        }
    }
}
