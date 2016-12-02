using System;
using GenFx.ComponentModel;
using System.ComponentModel;
using GenFx.Validation;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for a fitness evaluator.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <b>FitnessEvaluator</b> calculates the <see cref="GeneticEntity.RawFitnessValue"/> of a <see cref="GeneticEntity"/>.  
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
    public abstract class FitnessEvaluator : GeneticComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the <see cref="ComponentConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        public override sealed ComponentConfiguration Configuration
        {
            get { return this.Algorithm.ConfigurationSet.FitnessEvaluator; }
        }

        /// <summary>
        /// Gets the mode which specifies whether to treat higher or lower fitness values as being better.
        /// </summary>
        public FitnessEvaluationMode EvaluationMode
        {
            get { return ((FitnessEvaluatorConfiguration)this.Configuration).EvaluationMode; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FitnessEvaluator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="GeneticEnvironment"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected FitnessEvaluator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// When overriden in a derived class, returns the calculated fitness value of the <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="GeneticEntity"/> whose calculated fitness value is to be returned.</param>
        /// <returns>
        /// The calculated fitness value of the <paramref name="entity"/>.
        /// </returns>
        public abstract Task<double> EvaluateFitnessAsync(GeneticEntity entity);
    }

    /// <summary>
    /// Represents the configuration of <see cref="FitnessEvaluator"/>.
    /// </summary>
    [Component(typeof(FitnessEvaluator))]
    public abstract class FitnessEvaluatorConfiguration : ComponentConfiguration
    {
        private const FitnessEvaluationMode DefaultEvaluationMode = FitnessEvaluationMode.Maximize;
        private FitnessEvaluationMode evaluationMode = FitnessEvaluatorConfiguration.DefaultEvaluationMode;

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
