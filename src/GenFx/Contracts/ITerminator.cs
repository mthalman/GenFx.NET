namespace GenFx.Contracts
{
    /// <summary>
    /// Represents a component which defines when a genetic algorithm should stop executing.
    /// </summary>
    public interface ITerminator : IGeneticComponentWithAlgorithm
    {
        /// <summary>
        /// Returns whether the genetic algorithm should stop executing.
        /// </summary>
        /// <returns>True if the genetic algorithm is to stop executing; otherwise, false.</returns>
        bool IsComplete();
    }
}
