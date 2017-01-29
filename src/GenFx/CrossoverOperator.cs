using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for a genetic algorithm crossover operator.
    /// </summary>
    /// <remarks>
    /// A <see cref="CrossoverOperator"/> acts upon two <see cref="GeneticEntity"/> objects that were
    /// selected by the <see cref="SelectionOperator"/>.  It exchanges subparts of the two <see cref="GeneticEntity"/> 
    /// objects to produce one or more new <see cref="GeneticEntity"/> objects.  It is intended to be similar
    /// to biological recombination between two chromosomes.
    /// </remarks>
    [DataContract]
    public abstract class CrossoverOperator : GeneticComponentWithAlgorithm
    {
        private const double DefaultCrossoverRate = .7;
        private const double CrossoverRateMin = 0;
        private const double CrossoverRateMax = 1;

        [DataMember]
        private int requiredParentCount;

        [DataMember]
        private double crossoverRate = DefaultCrossoverRate;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="requiredParentCount">The number of parent entities required for input in order to execute the crossover operation.</param>
        protected CrossoverOperator(int requiredParentCount)
        {
            this.requiredParentCount = requiredParentCount;
        }

        /// <summary>
        /// Gets the number of parent entities required for input in order to execute the crossover operation
        /// </summary>
        /// <remarks>
        /// The value of this property determines the number of entities passed to the <see cref="GenerateCrossover"/> method.
        /// </remarks>
        public int RequiredParentCount { get { return this.requiredParentCount; } }

        /// <summary>
        /// Gets or sets the probability that two <see cref="GeneticEntity"/> objects will crossover after being selected.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [DoubleValidator(MinValue = CrossoverRateMin, MaxValue = CrossoverRateMax)]
        public double CrossoverRate
        {
            get { return this.crossoverRate; }
            set { this.SetProperty(ref this.crossoverRate, value); }
        }

        /// <summary>
        /// Attempts to perform a crossover between the parent entities if a random value is within
        /// the range of the <see cref="CrossoverRate"/>.
        /// </summary>
        /// <param name="parents">The <see cref="GeneticEntity"/> objects to be operated upon.</param>
        /// <returns>
        /// Collection of the <see cref="GeneticEntity"/> objects resulting from the crossover.  If no
        /// crossover occurred, this collection contains the original values contained in <paramref name="parents"/>.
        /// </returns>
        public IEnumerable<GeneticEntity> Crossover(IList<GeneticEntity> parents)
        {
            if (parents == null)
            {
                throw new ArgumentNullException(nameof(parents));
            }
            
            if (RandomNumberService.Instance.GetDouble() <= this.CrossoverRate)
            {
                IList<GeneticEntity> clones = parents.Select(p => p.Clone()).ToList();
                IEnumerable<GeneticEntity> result = this.GenerateCrossover(clones);
                if (result == null)
                {
                    throw new InvalidOperationException(
                        StringUtil.GetFormattedString(Resources.ErrorMsg_NullReturnValue, this.GetType(), nameof(GenerateCrossover)));
                }

                IList<GeneticEntity> crossoverOffspring = result.ToList();

                for (int i = 0; i < crossoverOffspring.Count; i++)
                {
                    crossoverOffspring[i].Age = 0;
                }

                return crossoverOffspring;
            }
            else
            {
                return parents;
            }
        }

        /// <summary>
        /// When overriden in a derived class, generates a crossover based on the parent entities.
        /// </summary>
        /// <param name="parents">The <see cref="GeneticEntity"/> objects to be operated upon.</param>
        /// <returns>
        /// Collection of the <see cref="GeneticEntity"/> objects resulting from the crossover.
        /// </returns>
        protected abstract IEnumerable<GeneticEntity> GenerateCrossover(IList<GeneticEntity> parents);
    }
}
