namespace GenFx.Contracts
{
    /// <summary>
    /// Represents the configuration of <see cref="ISelectionOperator"/>.
    /// </summary>
    public interface ISelectionOperatorFactoryConfig : IFactoryConfigForComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the <see cref="FitnessType"/> to base selection of <see cref="IGeneticEntity"/> objects on.
        /// </summary>
        FitnessType SelectionBasedOnFitnessType { get; }
    }
}
