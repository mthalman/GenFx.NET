using GenFx.Validation;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for a fitness evaluator.
    /// </summary>
    /// <remarks>
    /// The <b>FitnessEvaluator</b> calculates the <see cref="GeneticEntity.RawFitnessValue"/> of a <see cref="GeneticEntity"/>.  
    /// The fitness value is a relative measurement of how close a entity is to meeting the goal
    /// of the genetic algorithm.  For example, a genetic algorithm that uses binary strings to reach
    /// a goal of a entity with all ones in its string might use a <b>FitnessEvaluator</b> 
    /// that uses the number of ones in a binary string as the fitness value.
    /// </remarks>
    public abstract class FitnessEvaluator : GeneticComponentWithAlgorithm
    {
        private const FitnessEvaluationMode DefaultEvaluationMode = FitnessEvaluationMode.Maximize;
        private FitnessEvaluationMode evaluationMode = DefaultEvaluationMode;

        /// <summary>
        /// Gets or sets the mode which specifies whether to treat higher or lower fitness values as being better.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [FitnessEvaluationModeValidator]
        public FitnessEvaluationMode EvaluationMode
        {
            get { return this.evaluationMode; }
            set { this.SetProperty(ref this.evaluationMode, value); }
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
}
