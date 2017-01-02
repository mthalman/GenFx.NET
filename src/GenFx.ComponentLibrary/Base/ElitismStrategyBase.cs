using GenFx.ComponentLibrary.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Provides the base class for elitism in a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Elitism in genetic algorithms is an addition to the selection operator.  It causes the
    /// genetic algorithm to have some number of genetic entities remain unchanged and brought forth to the
    /// next generation.  An <see cref="ElitismStrategyBase{TElitism, TConfiguration}"/> acts upon a <see cref="IPopulation"/> to
    /// select those <see cref="IGeneticEntity"/> objects which are determined to be "elite".  The number
    /// of genetic entities chosen is based on the <see cref="ElitismStrategyConfigurationBase{TConfiguration, TElitism}.ElitistRatio"/> property value.
    /// </para>
    /// <para>
    /// <b>Notes to inheritors:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by using the <see cref="ComponentConfigurationSet.ElitismStrategy"/> 
    /// property.
    /// </para>
    /// </remarks>
    /// <typeparam name="TElitism">Type of the deriving elitism strategy class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    public abstract class ElitismStrategyBase<TElitism, TConfiguration> : GeneticComponentWithAlgorithm<TElitism, TConfiguration>, IElitismStrategy
        where TElitism : ElitismStrategyBase<TElitism, TConfiguration>
        where TConfiguration : ElitismStrategyConfigurationBase<TConfiguration, TElitism>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected ElitismStrategyBase(IGeneticAlgorithm algorithm)
            : base(algorithm, GetConfiguration(algorithm, c => c.ElitismStrategy))
        {
        }

        /// <summary>
        /// Returns the collection of <see cref="IGeneticEntity"/> objects from the <paramref name="population"/>
        /// that are to be treated as elite and move on to the next generation unchanged.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/> objects
        /// from which to select.</param>
        /// <returns>
        /// The collection of <see cref="IGeneticEntity"/> objects from the <paramref name="population"/>
        /// that are to be treated as elite.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="population"/> contains no entities.</exception>
        public IList<IGeneticEntity> GetEliteEntities(IPopulation population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            if (population.Entities.Count == 0)
            {
                throw new ArgumentException(Resources.ErrorMsg_EntityListEmpty, nameof(population));
            }

            return this.GetEliteGeneticEntitiesCore(population);
        }

        /// <summary>
        /// Returns the collection of <see cref="IGeneticEntity"/> objects from the <paramref name="population"/>
        /// that are to be treated as elite and move on to the next generation unchanged.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/> objects
        /// from which to select.</param>
        /// <returns>The collection of <see cref="IGeneticEntity"/> objects from the <paramref name="population"/>
        /// that are to be treated as elite.</returns>
        /// <remarks>
        /// <para>
        /// The default implementation of this method is to use the <see cref="ElitismStrategyConfigurationBase{TConfiguration, TElitism}.ElitistRatio"/>
        /// property to determine how many <see cref="IGeneticEntity"/> objects are chosen to be elite.  Those <see cref="IGeneticEntity"/>
        /// objects with the highest <see cref="IGeneticEntity.ScaledFitnessValue"/> are chosen.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected virtual IList<IGeneticEntity> GetEliteGeneticEntitiesCore(IPopulation population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            int elitistCount = Convert.ToInt32(Math.Round(this.Configuration.ElitistRatio * population.Entities.Count));

            List<IGeneticEntity> geneticEntities = new List<IGeneticEntity>();
            if (elitistCount > 0)
            {
                IGeneticEntity[] sorted = population.Entities.GetEntitiesSortedByFitness(
                    this.Algorithm.ConfigurationSet.SelectionOperator.SelectionBasedOnFitnessType,
                    this.Algorithm.ConfigurationSet.FitnessEvaluator.EvaluationMode).ToArray();

                for (int i = sorted.Length - elitistCount; i < sorted.Length; i++)
                {
                    geneticEntities.Add(sorted[i]);
                }
            }

            return geneticEntities;
        }
    }
}
