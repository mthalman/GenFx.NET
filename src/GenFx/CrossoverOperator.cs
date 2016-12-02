using System;
using System.Collections.Generic;
using System.ComponentModel;
using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for a genetic algorithm crossover operator.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A <see cref="CrossoverOperator"/> acts upon two <see cref="GeneticEntity"/> objects that were
    /// selected by the <see cref="SelectionOperator"/>.  It exchanges subparts of the two <see cref="GeneticEntity"/> 
    /// objects to produce one or more new <see cref="GeneticEntity"/> objects.  It is intended to be similar
    /// to biological recombination between two chromosomes.
    /// </para>
    /// <para>
    /// <b>Notes to implementers:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by using the <see cref="ComponentConfigurationSet.CrossoverOperator"/> 
    /// property.
    /// </para>
    /// </remarks>
    public abstract class CrossoverOperator : GeneticComponentWithAlgorithm
    {
        /// <summary>
        /// Gets the probability that two <see cref="GeneticEntity"/> objects will crossover after being selected.
        /// </summary>
        public double CrossoverRate
        {
            get { return this.Algorithm.ConfigurationSet.CrossoverOperator.CrossoverRate; }
        }

        /// <summary>
        /// Gets the <see cref="ComponentConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        public override sealed ComponentConfiguration Configuration
        {
            get { return this.Algorithm.ConfigurationSet.CrossoverOperator; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrossoverOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="CrossoverOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected CrossoverOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Attempts to perform a crossover between <paramref name="entity1"/> and <paramref name="entity2"/>
        /// if a random value is within the range of the <see cref="CrossoverOperator.CrossoverRate"/>.
        /// </summary>
        /// <param name="entity1"><see cref="GeneticEntity"/> to be crossed over with <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="GeneticEntity"/> to be crossed over with <paramref name="entity1"/>.</param>
        /// <returns>
        /// Collection of the <see cref="GeneticEntity"/> objects resulting from the crossover.  If no
        /// crossover occurred, this collection contains the original values of <paramref name="entity1"/>
        /// and <paramref name="entity2"/>.
        /// </returns>
        public IList<GeneticEntity> Crossover(GeneticEntity entity1, GeneticEntity entity2)
        {
            if (entity1 == null)
            {
                throw new ArgumentNullException(nameof(entity1));
            }

            if (entity2 == null)
            {
                throw new ArgumentNullException(nameof(entity2));
            }

            IList<GeneticEntity> crossoverOffspring;
            if (RandomHelper.Instance.GetRandomRatio() <= this.CrossoverRate)
            {
                GeneticEntity clonedEntity1 = entity1.Clone();
                GeneticEntity clonedEntity2 = entity2.Clone();
                crossoverOffspring = this.GenerateCrossover(clonedEntity1, clonedEntity2);

                for (int i = 0; i < crossoverOffspring.Count; i++)
                {
                    crossoverOffspring[i].Age = 0;
                }
            }
            else
            {
                crossoverOffspring = new List<GeneticEntity>();
                crossoverOffspring.Add(entity1);
                crossoverOffspring.Add(entity2);
            }
            return crossoverOffspring;
        }

        /// <summary>
        /// When overriden in a derived class, generates a crossover between <paramref name="entity1"/> 
        /// and <paramref name="entity2"/>.
        /// </summary>
        /// <param name="entity1"><see cref="GeneticEntity"/> to be crossed over with <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="GeneticEntity"/> to be crossed over with <paramref name="entity1"/>.</param>
        /// <returns>
        /// Collection of the <see cref="GeneticEntity"/> objects resulting from the crossover.
        /// </returns>
        protected abstract IList<GeneticEntity> GenerateCrossover(GeneticEntity entity1, GeneticEntity entity2);
    }

    /// <summary>
    /// Represents the configuration of <see cref="CrossoverOperator"/>.
    /// </summary>
    [Component(typeof(CrossoverOperator))]
    public abstract class CrossoverOperatorConfiguration : ComponentConfiguration
    {
        private const double DefaultCrossoverRate = .7;
        private const double CrossoverRateMin = 0;
        private const double CrossoverRateMax = 1;

        private double crossoverRate = DefaultCrossoverRate;

        /// <summary>
        /// Gets or sets the probability that two <see cref="GeneticEntity"/> objects will crossover after being selected.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [DoubleValidatorAttribute(MinValue = CrossoverRateMin, MaxValue = CrossoverRateMax)]
        public double CrossoverRate
        {
            get { return this.crossoverRate; }
            set { this.SetProperty(ref this.crossoverRate, value); }
        }
    }
}
