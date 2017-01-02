using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Represents the genetic algorithm responsible for execution of the algorithm logic.
    /// </summary>
    public interface IGeneticAlgorithm
    {
        /// <summary>
        /// Occurs when the fitness of an environment has been evaluated.
        /// </summary>
        event EventHandler<EnvironmentFitnessEvaluatedEventArgs> FitnessEvaluated;

        /// <summary>
        /// Occurs when a new generation has been created (but its fitness has not yet been evaluated).
        /// </summary>
        event EventHandler GenerationCreated;

        /// <summary>
        /// Occurs when execution of the algorithm completes.
        /// </summary>
        event EventHandler AlgorithmCompleted;

        /// <summary>
        /// Occurs when the algorithm is about to begin execution.
        /// </summary>
        /// <remarks>
        /// This event only occurs when the algorithm is first started after having been initialized.
        /// It does not occur when resuming execution after a pause.
        /// </remarks>
        event EventHandler AlgorithmStarting;

        /// <summary>
        /// Gets the <see cref="ComponentConfigurationSet"/> containing the configuration for the algorithm.
        /// </summary>
        ComponentConfigurationSet ConfigurationSet { get; }

        /// <summary>
        /// Gets the <see cref="AlgorithmOperators"/> to be used.
        /// </summary>
        AlgorithmOperators Operators { get; }

        /// <summary>
        /// Gets the <see cref="GeneticEnvironment"/> being used by this object.
        /// </summary>
        GeneticEnvironment Environment { get; }

        /// <summary>
        /// Gets the index of the generation reached so far during execution of the genetic algorithm.
        /// </summary>
        /// <value>
        /// The index of the generation reached so far during execution of the genetic algorithm.
        /// </value>
        int CurrentGeneration { get; }

        /// <summary>
        /// Gets the collection of statistics being calculated for the genetic algorithm.
        /// </summary>
        IEnumerable<IStatistic> Statistics { get; }

        /// <summary>
        /// Gets the collection of plugins being used by the genetic algorithm.
        /// </summary>
        IEnumerable<IPlugin> Plugins { get; }

        /// <summary>
        /// Initializes the genetic algorithm with a starting <see cref="GeneticEnvironment"/>.
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// Executes the genetic algorithm.
        /// </summary>
        /// <remarks>
        /// The execution will continue until the <see cref="ITerminator.IsComplete()"/> method returns
        /// true or <see cref="CancelEventArgs.Cancel"/> property is set to true.
        /// </remarks>
        Task RunAsync();

        /// <summary>
        /// Executes one generation of the genetic algorithm.
        /// </summary>
        /// <returns>True if the genetic algorithm has completed its execution; otherwise, false.</returns>
        /// <remarks>
        /// Subsequent calls to either <see cref="RunAsync()"/> or <see cref="StepAsync()"/> will resume execution from
        /// where the previous <see cref="StepAsync()"/> call left off.
        /// </remarks>
        Task<bool> StepAsync();
    }
}
