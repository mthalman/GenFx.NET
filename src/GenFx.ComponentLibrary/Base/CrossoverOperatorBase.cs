using GenFx.ComponentLibrary.ComponentModel;
using System;
using System.Collections.Generic;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Provides the abstract base class for a genetic algorithm crossover operator.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A <see cref="CrossoverOperatorBase{TCrossover, TConfiguration}"/> acts upon two <see cref="IGeneticEntity"/> objects that were
    /// selected by the <see cref="ISelectionOperator"/>.  It exchanges subparts of the two <see cref="IGeneticEntity"/> 
    /// objects to produce one or more new <see cref="IGeneticEntity"/> objects.  It is intended to be similar
    /// to biological recombination between two chromosomes.
    /// </para>
    /// <para>
    /// <b>Notes to implementers:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by using the <see cref="ComponentConfigurationSet.CrossoverOperator"/> 
    /// property.
    /// </para>
    /// </remarks>
    /// <typeparam name="TCrossover">Type of the deriving crossover operator class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    public abstract class CrossoverOperatorBase<TCrossover, TConfiguration> : GeneticComponentWithAlgorithm<TCrossover, TConfiguration>, ICrossoverOperator
        where TCrossover : CrossoverOperatorBase<TCrossover, TConfiguration>
        where TConfiguration : CrossoverOperatorConfigurationBase<TConfiguration, TCrossover>
    {        
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this instance.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected CrossoverOperatorBase(IGeneticAlgorithm algorithm)
            : base(algorithm, GetConfiguration(algorithm, c => c.CrossoverOperator))
        {
        }

        /// <summary>
        /// Attempts to perform a crossover between <paramref name="entity1"/> and <paramref name="entity2"/>
        /// if a random value is within the range of the <see cref="ICrossoverOperatorConfiguration.CrossoverRate"/>.
        /// </summary>
        /// <param name="entity1"><see cref="IGeneticEntity"/> to be crossed over with <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="IGeneticEntity"/> to be crossed over with <paramref name="entity1"/>.</param>
        /// <returns>
        /// Collection of the <see cref="IGeneticEntity"/> objects resulting from the crossover.  If no
        /// crossover occurred, this collection contains the original values of <paramref name="entity1"/>
        /// and <paramref name="entity2"/>.
        /// </returns>
        public IList<IGeneticEntity> Crossover(IGeneticEntity entity1, IGeneticEntity entity2)
        {
            if (entity1 == null)
            {
                throw new ArgumentNullException(nameof(entity1));
            }

            if (entity2 == null)
            {
                throw new ArgumentNullException(nameof(entity2));
            }

            IList<IGeneticEntity> crossoverOffspring;
            if (RandomNumberService.Instance.GetRandomPercentRatio() <= this.Configuration.CrossoverRate)
            {
                IGeneticEntity clonedEntity1 = entity1.Clone();
                IGeneticEntity clonedEntity2 = entity2.Clone();
                crossoverOffspring = this.GenerateCrossover(clonedEntity1, clonedEntity2);

                for (int i = 0; i < crossoverOffspring.Count; i++)
                {
                    crossoverOffspring[i].Age = 0;
                }
            }
            else
            {
                crossoverOffspring = new List<IGeneticEntity>();
                crossoverOffspring.Add(entity1);
                crossoverOffspring.Add(entity2);
            }
            return crossoverOffspring;
        }

        /// <summary>
        /// When overriden in a derived class, generates a crossover between <paramref name="entity1"/> 
        /// and <paramref name="entity2"/>.
        /// </summary>
        /// <param name="entity1"><see cref="IGeneticEntity"/> to be crossed over with <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="IGeneticEntity"/> to be crossed over with <paramref name="entity1"/>.</param>
        /// <returns>
        /// Collection of the <see cref="IGeneticEntity"/> objects resulting from the crossover.
        /// </returns>
        protected abstract IList<IGeneticEntity> GenerateCrossover(IGeneticEntity entity1, IGeneticEntity entity2);
    }
}
