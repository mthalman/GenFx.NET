using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GenFx.Contracts
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
        /// Gets or sets the number of <see cref="IPopulation"/> objects that are contained by the <see cref="GeneticEnvironment"/>.
        /// </summary>
        int EnvironmentSize { get; }

        /// <summary>
        /// Gets the <see cref="IFitnessEvaluator"/> to be used by the algorithm.
        /// </summary>
        IFitnessEvaluator FitnessEvaluator { get; set; }

        /// <summary>
        /// Gets the <see cref="ITerminator"/> to be used by the algorithm.
        /// </summary>
        ITerminator Terminator { get; set; }

        /// <summary>
        /// Gets the <see cref="IFitnessScalingStrategy"/> to be used by the algorithm.
        /// </summary>
        IFitnessScalingStrategy FitnessScalingStrategy { get; set; }

        /// <summary>
        /// Gets the <see cref="ISelectionOperator"/> to be used by the algorithm.
        /// </summary>
        ISelectionOperator SelectionOperator { get; set; }

        /// <summary>
        /// Gets the <see cref="IMutationOperator"/> to be used by the algorithm.
        /// </summary>
        IMutationOperator MutationOperator { get; set; }

        /// <summary>
        /// Gets the <see cref="ICrossoverOperator"/> to be used by the algorithm.
        /// </summary>
        ICrossoverOperator CrossoverOperator { get; set; }

        /// <summary>
        /// Gets the <see cref="IElitismStrategy"/> to be used by the algorithm.
        /// </summary>
        IElitismStrategy ElitismStrategy { get; set; }

        /// <summary>
        /// Gets the <see cref="IGeneticEntity"/> to be used by the algorithm.
        /// </summary>
        /// <remarks>
        /// This instance is only used for its configuration property values and to generate
        /// additional genetic entities.
        /// </remarks>
        IGeneticEntity GeneticEntitySeed { get; set; }

        /// <summary>
        /// Gets the <see cref="IPopulation"/> to be used by the algorithm.
        /// </summary>
        /// <remarks>
        /// This instance is only used for its configuration property values and to generate
        /// additional populations.
        /// </remarks>
        IPopulation PopulationSeed { get; set; }

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
        /// Gets the collection of statistics to be calculated for the genetic algorithm.
        /// </summary>
        IList<IStatistic> Statistics { get; }

        /// <summary>
        /// Gets the collection of plugins to be used by the genetic algorithm.
        /// </summary>
        IList<IPlugin> Plugins { get; }

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
