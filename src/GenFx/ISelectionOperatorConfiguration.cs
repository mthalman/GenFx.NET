using GenFx.ComponentModel;

namespace GenFx
{
    /// <summary>
    /// Represents the configuration of <see cref="ISelectionOperator"/>.
    /// </summary>
    public interface ISelectionOperatorConfiguration : IConfigurationForComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the <see cref="FitnessType"/> to base selection of <see cref="IGeneticEntity"/> objects on.
        /// </summary>
        FitnessType SelectionBasedOnFitnessType { get; }
    }
}
