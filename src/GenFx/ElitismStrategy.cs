using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using GenFx.ComponentModel;
using GenFx.Properties;
using GenFx.Validation;

namespace GenFx
{
    /// <summary>
    /// Provides the base class for elitism in a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Elitism in genetic algorithms is an addition to the selection operator.  It causes the
    /// genetic algorithm to have some number of genetic entities remain unchanged and brought forth to the
    /// next generation.  An <see cref="ElitismStrategy"/> acts upon a <see cref="Population"/> to
    /// select those <see cref="GeneticEntity"/> objects which are determined to be "elite".  The number
    /// of genetic entities chosen is based on the <see cref="ElitismStrategy.ElitistRatio"/> property value.
    /// </para>
    /// <para>
    /// <b>Notes to inheritors:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by using the <see cref="ComponentConfigurationSet.ElitismStrategy"/> 
    /// property.
    /// </para>
    /// </remarks>
    public class ElitismStrategy : GeneticComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the ratio of <see cref="GeneticEntity"/> objects that will be selected as elite and move on 
        /// to the next generation unchanged.
        /// </summary>
        public double ElitistRatio
        {
            get { return this.Algorithm.ConfigurationSet.ElitismStrategy.ElitistRatio; }
        }

        /// <summary>
        /// Gets the <see cref="ComponentConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        public override sealed ComponentConfiguration Configuration
        {
            get { return this.Algorithm.ConfigurationSet.ElitismStrategy; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElitismStrategy"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="ElitismStrategy"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public ElitismStrategy(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Returns the collection of <see cref="GeneticEntity"/> objects from the <paramref name="population"/>
        /// that are to be treated as elite and move on to the next generation unchanged.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/> objects
        /// from which to select.</param>
        /// <returns>
        /// The collection of <see cref="GeneticEntity"/> objects from the <paramref name="population"/>
        /// that are to be treated as elite.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="population"/> contains no entities.</exception>
        public IList<GeneticEntity> GetElitistGeneticEntities(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            if (population.Entities.Count == 0)
            {
                throw new ArgumentException(FwkResources.ErrorMsg_EntityListEmpty, nameof(population));
            }

            return this.GetElitistGeneticEntitiesCore(population);
        }

        /// <summary>
        /// Returns the collection of <see cref="GeneticEntity"/> objects from the <paramref name="population"/>
        /// that are to be treated as elite and move on to the next generation unchanged.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/> objects
        /// from which to select.</param>
        /// <returns>The collection of <see cref="GeneticEntity"/> objects from the <paramref name="population"/>
        /// that are to be treated as elite.</returns>
        /// <remarks>
        /// <para>
        /// The default implementation of this method is to use the <see cref="ElitismStrategy.ElitistRatio"/>
        /// property to determine how many <see cref="GeneticEntity"/> objects are chosen to be elite.  Those <see cref="GeneticEntity"/>
        /// objects with the highest <see cref="GeneticEntity.ScaledFitnessValue"/> are chosen.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected virtual IList<GeneticEntity> GetElitistGeneticEntitiesCore(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            int elitistCount = Convert.ToInt32(Math.Round(this.ElitistRatio * population.Entities.Count));

            List<GeneticEntity> geneticEntities = new List<GeneticEntity>();
            if (elitistCount > 0)
            {

                GeneticEntity[] sorted = population.Entities.GetEntitiesSortedByFitness(
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

    /// <summary>
    /// Represents the configuration of <see cref="ElitismStrategy"/>.
    /// </summary>
    [Component(typeof(ElitismStrategy))]
    public class ElitismStrategyConfiguration : ComponentConfiguration
    {
        private const double DefaultElitistRatio = .1;
        private double elitistRatio = DefaultElitistRatio;

        /// <summary>
        /// Gets or sets the ratio of <see cref="GeneticEntity"/> objects that will be selected as elite and move on 
        /// to the next generation unchanged.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [DoubleValidator(MinValue = 0, MaxValue = 1)]
        public double ElitistRatio
        {
            get { return this.elitistRatio; }
            set { this.SetProperty(ref this.elitistRatio, value); }
        }
    }
}
