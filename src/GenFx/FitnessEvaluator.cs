using GenFx.ComponentModel;
using GenFx.Validation;
using System;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Represents a component which evaluates the fitness of entities.
    /// </summary>
    public interface IFitnessEvaluator : IGeneticComponent
    {
        /// <summary>
        /// Returns the calculated fitness value of the <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="IGeneticEntity"/> whose calculated fitness value is to be returned.</param>
        /// <returns>
        /// The calculated fitness value of the <paramref name="entity"/>.
        /// </returns>
        Task<double> EvaluateFitnessAsync(IGeneticEntity entity);
    }

    /// <summary>
    /// Provides the abstract base class for a fitness evaluator.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <b>FitnessEvaluator</b> calculates the <see cref="IGeneticEntity.RawFitnessValue"/> of a <see cref="IGeneticEntity"/>.  
    /// The fitness value is a relative measurement of how close a entity is to meeting the goal
    /// of the genetic algorithm.  For example, a genetic algorithm that uses binary strings to reach
    /// a goal of a entity with all ones in its string might use a <b>FitnessEvaluator</b> 
    /// that uses the number of ones in a binary string as the fitness value.
    /// </para>
    /// <para>
    /// <b>Notes to implementers:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by using the <see cref="ComponentConfigurationSet.FitnessEvaluator"/> 
    /// property.
    /// </para>
    /// </remarks>
    public abstract class FitnessEvaluator<TFitness, TConfiguration> : GeneticComponentWithAlgorithm<TFitness, TConfiguration>, IFitnessEvaluator
        where TFitness : FitnessEvaluator<TFitness, TConfiguration>
        where TConfiguration : FitnessEvaluatorConfiguration<TConfiguration, TFitness>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this <see cref="GeneticEnvironment"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected FitnessEvaluator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// When overriden in a derived class, returns the calculated fitness value of the <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="IGeneticEntity"/> whose calculated fitness value is to be returned.</param>
        /// <returns>
        /// The calculated fitness value of the <paramref name="entity"/>.
        /// </returns>
        public abstract Task<double> EvaluateFitnessAsync(IGeneticEntity entity);
    }

    /// <summary>
    /// Represents the configuration of <see cref="IFitnessEvaluator"/>.
    /// </summary>
    public interface IFitnessEvaluatorConfiguration : IComponentConfiguration
    {
        /// <summary>
        /// Gets the mode which specifies whether to treat higher or lower fitness values as being better.
        /// </summary>
        FitnessEvaluationMode EvaluationMode { get; }
    }

    /// <summary>
    /// Represents the configuration of <see cref="FitnessEvaluator{TFitness, TConfiguration}"/>.
    /// </summary>
    public abstract class FitnessEvaluatorConfiguration<TConfiguration, TFitness> : ComponentConfiguration<TConfiguration, TFitness>, IFitnessEvaluatorConfiguration
        where TConfiguration : FitnessEvaluatorConfiguration<TConfiguration, TFitness>
        where TFitness : FitnessEvaluator<TFitness, TConfiguration>
    {
        private const FitnessEvaluationMode DefaultEvaluationMode = FitnessEvaluationMode.Maximize;
        private FitnessEvaluationMode evaluationMode = DefaultEvaluationMode;

        /// <summary>
        /// Gets or sets the mode which specifies whether to treat higher or lower fitness values as being better.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [FitnessEvaluationModeValidator]
        public FitnessEvaluationMode EvaluationMode
        {
            get { return this.evaluationMode; }
            set { this.SetProperty(ref this.evaluationMode, value); }
        }
    }
}
