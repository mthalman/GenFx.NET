using GenFx.Validation;
using System;
using System.Collections.Generic;
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
        private const int DefaultCrossoverPointCount = CrossoverRateMin;
        private const int CrossoverRateMin = 2;

        private int crossoverPointCount = DefaultCrossoverPointCount;
        private bool usePartiallyMatchedCrossover;

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public MultiPointCrossoverOperator()
            : base(2)
        {
        }

        /// <summary>
        /// Gets or sets the number of crossover points to use.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [IntegerValidator(MinValue = CrossoverRateMin)]
        [CustomValidator(typeof(MultiPointCrossoverOperatorCrossoverPointValidator))]
        public int CrossoverPointCount
        {
            get { return this.crossoverPointCount; }
            set { this.SetProperty(ref this.crossoverPointCount, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use partially matched crossover.
        /// </summary>
        [ConfigurationProperty]
        [CustomValidator(typeof(MultiPointCrossoverOperatorCrossoverPointValidator))]
        public bool UsePartiallyMatchedCrossover
        {
            get { return this.usePartiallyMatchedCrossover; }
            set { this.SetProperty(ref this.usePartiallyMatchedCrossover, value); }
        }

        /// <summary>
        /// Executes a single-point crossover between two list-based entities.
        /// </summary>
        /// <param name="parents">The <see cref="GeneticEntity"/> objects to be operated upon.</param>
        /// <returns>
        /// Collection of the list-based entities resulting from the crossover.
        /// </returns>
        protected override IEnumerable<GeneticEntity> GenerateCrossover(IList<GeneticEntity> parents)
        {
            if (parents == null)
            {
                throw new ArgumentNullException(nameof(parents));
            }
            
            ListEntityBase listEntity1 = (ListEntityBase)parents[0];
            ListEntityBase listEntity2 = (ListEntityBase)parents[1];

            int entity1Length = listEntity1.Length;
            int entity2Length = listEntity2.Length;

            List<int> crossoverLoci = new List<int>();

            // Generate the set of crossover points.
            for (int i = 0; i < this.CrossoverPointCount; i++)
            {
                int crossoverLocus;
                do
                {
                    crossoverLocus = RandomNumberService.Instance.GetRandomValue(Math.Min(entity1Length, entity2Length));
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

                object entity1SourceVal = entity1Source.GetValue(i);
                object entity2SourceVal = entity2Source.GetValue(i);

                if (this.UsePartiallyMatchedCrossover)
                {
                    if (listEntity1 != entity1Source && listEntity1.Contains(entity1SourceVal))
                    {
                        int index = originalEntity2.IndexOf(entity1SourceVal);
                        entity1SourceVal = originalEntity1.GetValue(index);
                    }

                    if (listEntity2 != entity2Source && listEntity2.Contains(entity2SourceVal))
                    {
                        int index = originalEntity1.IndexOf(entity2SourceVal);
                        entity2SourceVal = originalEntity2.GetValue(index);
                    }
                }

                listEntity1.SetValue(i, entity1SourceVal);
                listEntity2.SetValue(i, entity2SourceVal);
            }

            // Set the length based on their swapped length
            listEntity1.Length = entity2Length;
            listEntity2.Length = entity1Length;

            crossoverOffspring.Add(listEntity1);
            crossoverOffspring.Add(listEntity2);

            return crossoverOffspring;
        }
    }
}
