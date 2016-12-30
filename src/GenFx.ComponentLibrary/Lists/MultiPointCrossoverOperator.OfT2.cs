using GenFx.ComponentLibrary.Base;
using GenFx.ComponentModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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
    [RequiredEntity(typeof(IListEntityBase))]
    public abstract class MultiPointCrossoverOperator<TCrossover, TConfiguration> : CrossoverOperatorBase<TCrossover, TConfiguration>
        where TCrossover : MultiPointCrossoverOperator<TCrossover, TConfiguration>
        where TConfiguration : MultiPointCrossoverOperatorConfiguration<TConfiguration, TCrossover>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected MultiPointCrossoverOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Executes a single-point crossover between two list-based entities.
        /// </summary>
        /// <param name="entity1"><see cref="IGeneticEntity"/> to be crossed over with <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="IGeneticEntity"/> to be crossed over with <paramref name="entity1"/>.</param>
        /// <returns>
        /// Collection of the list-based entities resulting from the crossover.  If no
        /// crossover occurred, this collection contains the original values of <paramref name="entity1"/>
        /// and <paramref name="entity2"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity1"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="entity2"/> is null.</exception>
        protected override IList<IGeneticEntity> GenerateCrossover(IGeneticEntity entity1, IGeneticEntity entity2)
        {
            if (entity1 == null)
            {
                throw new ArgumentNullException(nameof(entity1));
            }

            if (entity2 == null)
            {
                throw new ArgumentNullException(nameof(entity2));
            }

            IListEntityBase listEntity1 = (IListEntityBase)entity1;
            IListEntityBase listEntity2 = (IListEntityBase)entity2;

            int entity1Length = listEntity1.Length;
            int entity2Length = listEntity2.Length;

            List<int> crossoverLoci = new List<int>();

            // Generate the set of crossover points.
            for (int i = 0; i < this.Configuration.CrossoverPointCount; i++)
            {
                int crossoverLocus;
                do
                {
                    crossoverLocus = RandomHelper.Instance.GetRandomValue(Math.Min(entity1Length, entity2Length));
                } while (crossoverLoci.Contains(crossoverLocus));

                crossoverLoci.Add(crossoverLocus);
            }

            crossoverLoci.Sort();

            IList<IGeneticEntity> crossoverOffspring = new List<IGeneticEntity>();

            int maxLength = Math.Max(entity1Length, entity2Length);

            // Normalize the lists into a common length
            if (entity1Length != entity2Length)
            {
                listEntity1.Length = maxLength;
                listEntity2.Length = maxLength;
            }

            IListEntityBase originalEntity1 = (IListEntityBase)listEntity1.Clone();
            IListEntityBase originalEntity2 = (IListEntityBase)listEntity2.Clone();

            IListEntityBase entity1Source = listEntity1;
            IListEntityBase entity2Source = listEntity2;

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

                if (this.Configuration.UsePartiallyMatchedCrossover)
                {
                    if (listEntity1 != entity1Source && listEntity1.Contains(entity1SourceVal))
                    {
                        int index = originalEntity2.IndexOf(entity1SourceVal);
                        entity1SourceVal = originalEntity1[index];
                    }

                    if (listEntity2 != entity2Source && listEntity2.Contains(entity2SourceVal))
                    {
                        int index = originalEntity1.IndexOf(entity2SourceVal);
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
}
