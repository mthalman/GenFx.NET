using System;
using System.Collections.Generic;
using System.ComponentModel;
using GenFx.ComponentModel;
using GenFx.Validation;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides a list-based entity with multi-point crossover support.
    /// </summary>
    /// <remarks>
    /// Multi-point crossover chooses multiple list element positions and alternately swaps the elements on
    /// for each of the points between two list-based entities.  For example, if two entities represented
    /// by ADCFBE and BFEACD were to be crossed over at position 2 and 4, the resulting offspring would
    /// be AFEFBE and BDCACD.
    /// 
    /// An option of multi-point crossover is partially-matched crossover support.  With this option, it assumes
    /// each of the elements in the list is unique for that entity and the resulting offspring must share that
    /// unique characteristic.  Using the example above, the two parent entities have unique elements but the 
    /// offspring do not (offspring 1, for example, has 2 F's and 2 E's).  By using the partially matched crossover
    /// option, it would ensure the offspring have unique elements.  It does this by swapping out the original element
    /// that is a duplicate and swapping it out with the element at the same position of the duplicate on the other parent.
    /// So for the example above, this would result in the following offspring: AFEDBC and BDCAEF.  This option is only
    /// available when using two crossover points.
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint")]
    [RequiredEntity(typeof(ListEntityBase))]
    public class MultiPointCrossoverOperator : CrossoverOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPointCrossoverOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="MultiPointCrossoverOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public MultiPointCrossoverOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Gets the number of crossover points to use.
        /// </summary>
        public int CrossoverPointCount
        {
            get { return ((MultiPointCrossoverOperatorConfiguration)this.Algorithm.ConfigurationSet.CrossoverOperator).CrossoverPointCount; }
        }

        /// <summary>
        /// Gets a value indicating whether to use partially matched crossover.
        /// </summary>
        public bool UsePartiallyMatchedCrossover
        {
            get { return ((MultiPointCrossoverOperatorConfiguration)this.Algorithm.ConfigurationSet.CrossoverOperator).UsePartiallyMatchedCrossover; }
        }

        /// <summary>
        /// Executes a single-point crossover between two list-based entities.
        /// </summary>
        /// <param name="entity1"><see cref="GeneticEntity"/> to be crossed over with <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="GeneticEntity"/> to be crossed over with <paramref name="entity1"/>.</param>
        /// <returns>
        /// Collection of the list-based entities resulting from the crossover.  If no
        /// crossover occurred, this collection contains the original values of <paramref name="entity1"/>
        /// and <paramref name="entity2"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity1"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="entity2"/> is null.</exception>
        protected override IList<GeneticEntity> GenerateCrossover(GeneticEntity entity1, GeneticEntity entity2)
        {
            if (entity1 == null)
            {
                throw new ArgumentNullException(nameof(entity1));
            }

            if (entity2 == null)
            {
                throw new ArgumentNullException(nameof(entity2));
            }

            ListEntityBase listEntity1 = (ListEntityBase)entity1;
            ListEntityBase listEntity2 = (ListEntityBase)entity2;

            int entity1Length = listEntity1.Length;
            int entity2Length = listEntity2.Length;

            List<int> crossoverLoci = new List<int>();

            for (int i = 0; i < this.CrossoverPointCount; i++)
            {
                int crossoverLocus;
                do
                {
                    crossoverLocus = RandomHelper.Instance.GetRandomValue(Math.Min(entity1Length, entity2Length));
                } while (crossoverLoci.Contains(crossoverLocus));

                crossoverLoci.Add(crossoverLocus);
            }

            crossoverLoci.Sort();

            IList<GeneticEntity> crossoverOffspring = new List<GeneticEntity>();

            int maxLength = Math.Max(entity1Length, entity2Length);

            // Normalize the lists into a common length
            if (entity1Length != entity2Length)
            {
                listEntity1.Length = maxLength;
                listEntity2.Length = maxLength;
            }

            ListEntityBase originalEntity1 = (ListEntityBase)listEntity1.Clone();
            ListEntityBase originalEntity2 = (ListEntityBase)listEntity2.Clone();

            ListEntityBase entity1Source = listEntity1;
            ListEntityBase entity2Source = listEntity2;

            for (int i = 0; i < maxLength; i++)
            {
                // If this is a crossover point, swap the source entities
                if (crossoverLoci.Contains(i))
                {
                    if (listEntity1 == entity1Source)
                    {
                        entity1Source = listEntity2;
                        entity2Source = listEntity1;
                    }
                    else
                    {
                        entity1Source = listEntity1;
                        entity2Source = listEntity2;
                    }
                }

                object entity1SourceVal = entity1Source[i];
                object entity2SourceVal = entity2Source[i];

                if (this.UsePartiallyMatchedCrossover)
                {
                    if (listEntity1 != entity1Source && listEntity1.ToList().Contains(entity1SourceVal))
                    {
                        int index = originalEntity2.ToList().IndexOf(entity1SourceVal);
                        entity1SourceVal = originalEntity1[index];
                    }

                    if (listEntity2 != entity2Source && listEntity2.ToList().Contains(entity2SourceVal))
                    {
                        int index = originalEntity1.ToList().IndexOf(entity2SourceVal);
                        entity2SourceVal = originalEntity2[index];
                    }
                }

                listEntity1[i] = entity1SourceVal;
                listEntity2[i] = entity2SourceVal;
            }

            // Set the length based on their swapped length
            listEntity1.Length = entity2Length;
            listEntity2.Length = entity1Length;

            crossoverOffspring.Add(entity1);
            crossoverOffspring.Add(entity2);

            return crossoverOffspring;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="MultiPointCrossoverOperator"/>.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint")]
    [Component(typeof(MultiPointCrossoverOperator))]
    public class MultiPointCrossoverOperatorConfiguration : CrossoverOperatorConfiguration
    {
        private const int DefaultCrossoverPointCount = CrossoverRateMin;
        private const int CrossoverRateMin = 2;

        private int crossoverPointCount = DefaultCrossoverPointCount;
        private bool usePartiallyMatchedCrossover;

        /// <summary>
        /// Gets or sets the number of crossover points to use.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [IntegerValidator(MinValue = CrossoverRateMin)]
        [CustomValidator(typeof(CrossoverPointCountValidator))]
        public int CrossoverPointCount
        {
            get { return this.crossoverPointCount; }
            set { this.SetProperty(ref this.crossoverPointCount, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use partially matched crossover.
        /// </summary>
        [CustomValidator(typeof(CrossoverPointCountValidator))]
        public bool UsePartiallyMatchedCrossover
        {
            get { return this.usePartiallyMatchedCrossover; }
            set { this.SetProperty(ref this.usePartiallyMatchedCrossover, value); }
        }

        private class CrossoverPointCountValidator : Validator
        {
            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                errorMessage = null;

                MultiPointCrossoverOperatorConfiguration config = (MultiPointCrossoverOperatorConfiguration)owner;
                if (config.UsePartiallyMatchedCrossover && config.CrossoverPointCount > 2)
                {
                    errorMessage = "Using partially matched crossover is only allowed for two crossover points.";
                }

                return errorMessage == null;
            }
        }
    }
}
