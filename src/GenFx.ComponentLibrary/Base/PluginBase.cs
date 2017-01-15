using GenFx.Contracts;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Plugin component that provides custom extension functionality.
    /// </summary>
    public abstract class PluginBase : GeneticComponentWithAlgorithm, IPlugin
    {
        /// <summary>
        /// Handles the event when the fitness of an environment has been evaluated.
        /// </summary>
        /// <param name="environment">The environment which has had its fitness evaluated..</param>
        /// <param name="generationIndex">Index value of the current generation in the environment.</param>
        public virtual void OnFitnessEvaluated(GeneticEnvironment environment, int generationIndex)
        {
        }

        /// <summary>
        /// Handles the event when a genetic algorithm is about to start execution.
        /// </summary>
        public virtual void OnAlgorithmStarting()
        {
        }

        /// <summary>
        /// Handles the event when a genetic algorithm has finished executing.
        /// </summary>
        public virtual void OnAlgorithmCompleted()
        {
        }
    }
}
