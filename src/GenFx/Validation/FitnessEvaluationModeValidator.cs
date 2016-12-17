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
    }
}
