using GenFx.Contracts;
using System;
using System.Threading.Tasks;

namespace GenFx.ComponentLibrary.Base
{
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
    /// the genetic algorithm by using the <see cref="ComponentFactoryConfigSet.FitnessEvaluator"/> 
    /// property.
    /// </para>
    /// </remarks>
    /// <typeparam name="TFitness">Type of the deriving fitness evaluator class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    public abstract class FitnessEvaluatorBase<TFitness, TConfiguration> : GeneticComponentWithAlgorithm<TFitness, TConfiguration>, IFitnessEvaluator
        where TFitness : FitnessEvaluatorBase<TFitness, TConfiguration>
        where TConfiguration : FitnessEvaluatorFactoryConfigBase<TConfiguration, TFitness>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this <see cref="GeneticEnvironment"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected FitnessEvaluatorBase(IGeneticAlgorithm algorithm)
            : base(algorithm, GetConfiguration(algorithm, c => c.FitnessEvaluator))
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
}
