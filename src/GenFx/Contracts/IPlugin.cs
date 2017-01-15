namespace GenFx.Contracts
{
    /// <summary>
    /// Represents a component that provides custom extension functionality.
    /// </summary>
    public interface IPlugin : IGeneticComponentWithAlgorithm
    {
        /// <summary>
        /// Handles the event when a genetic algorithm is about to start execution.
        /// </summary>
        void OnAlgorithmStarting();

        /// <summary>
        /// Handles the event when a genetic algorithm has finished executing.
        /// </summary>
        void OnAlgorithmCompleted();

        /// <summary>
        /// Handles the event when the fitness of an environment has been evaluated.
        /// </summary>
        /// <param name="environment">The environment which has had its fitness evaluated..</param>
        /// <param name="generationIndex">Index value of the current generation in the environment.</param>
        void OnFitnessEvaluated(GeneticEnvironment environment, int generationIndex);
    }
}
