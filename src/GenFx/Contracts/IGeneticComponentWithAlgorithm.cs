namespace GenFx.Contracts
{
    /// <summary>
    /// Represents a customizable component within the framework.
    /// </summary>
    public interface IGeneticComponentWithAlgorithm : IGeneticComponent
    {
        /// <summary>
        /// Gets the <see cref="IGeneticAlgorithm"/>.
        /// </summary>
        IGeneticAlgorithm Algorithm { get; }

        /// <summary>
        /// Initializes the component to ensure its readiness for algorithm execution.
        /// </summary>
        /// <param name="algorithm">The algorithm that is to use this component.</param>
        void Initialize(IGeneticAlgorithm algorithm);
    }
}
