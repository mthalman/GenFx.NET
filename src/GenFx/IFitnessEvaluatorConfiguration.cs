using GenFx.ComponentModel;

namespace GenFx
{
    /// <summary>
    /// Represents the configuration of <see cref="IFitnessEvaluator"/>.
    /// </summary>
    public interface IFitnessEvaluatorConfiguration : IConfigurationForComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the mode which specifies whether to treat higher or lower fitness values as being better.
        /// </summary>
        FitnessEvaluationMode EvaluationMode { get; }
    }
}
