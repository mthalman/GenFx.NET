using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Provides validation for a <see cref="FitnessType"/> value.
    /// </summary>
    public class FitnessTypeValidator : EnumValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FitnessTypeValidator"/> class.
        /// </summary>
        public FitnessTypeValidator()
            : base(typeof(FitnessType))
        {
        }

        /// <summary>
        /// Returns whether the enum value is a defined value.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>True if the enum value is defined; otherwise, false.</returns>
        protected override bool IsDefined(Enum value)
        {
            return FitnessTypeHelper.IsDefined((FitnessType)value);
        }
    }
}
