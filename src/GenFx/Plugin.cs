using System;
using GenFx.ComponentModel;

namespace GenFx
{
    /// <summary>
    /// Plugin component that provides custom extension functionality.
    /// </summary>
    public abstract class Plugin : GeneticComponentWithAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Plugin"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="Plugin"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected Plugin(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
        
        /// <summary>
        /// Gets the <see cref="ComponentConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        public override sealed ComponentConfiguration Configuration
        {
            get { return this.Algorithm.ConfigurationSet.Plugins[this.GetType()]; }
        }

        /// <summary>
        /// Handles the event when a generation has been created.
        /// </summary>
        /// <param name="environment">The environment representing the generation that was created.</param>
        /// <param name="generationIndex">Index value of the generation that was created.</param>
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

    /// <summary>
    /// Represents the configuration of <see cref="PluginConfiguration"/>.
    /// </summary>
    [Component(typeof(Plugin))]
    public abstract class PluginConfiguration : ComponentConfiguration
    {
    }
}
