using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Provides validation for a <see cref="FitnessEvaluationMode"/> value.
    /// </summary>
    public class FitnessEvaluationModeValidator : EnumValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FitnessEvaluationModeValidator"/> class.
        /// </summary>
        public FitnessEvaluationModeValidator()
            : base(typeof(FitnessEvaluationMode))
        {
        }

        /// <summary>
        /// Returns whether the enum value is a defined value.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>True if the enum value is defined; otherwise, false.</returns>
        protected override bool IsDefined(Enum value)
        {
            return FitnessEvaluationModeHelper.IsDefined((FitnessEvaluationMode)value);
        }
    }
}
