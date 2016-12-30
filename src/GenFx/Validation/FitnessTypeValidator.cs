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
    }
}
