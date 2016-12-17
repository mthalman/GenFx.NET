using GenFx.ComponentModel;
using System;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Plugin component that provides custom extension functionality.
    /// </summary>
    public abstract class Plugin<TPlugin, TConfiguration> : GeneticComponentWithAlgorithm<TPlugin, TConfiguration>, IPlugin
        where TPlugin : Plugin<TPlugin, TConfiguration>
        where TConfiguration : PluginConfiguration<TConfiguration, TPlugin>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected Plugin(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

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
