using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

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
    /// If the entity makes use of unique elements (<see cref="ListEntityBase.RequiresUniqueElementValues"/> is set to true),
    /// then a technique called partially-matched crossover is used.  With this technique, ensures that the resulting offspring
    /// also contains a unique set of element values.  Using the example above, the two parent entities have unique elements but the 
    /// offspring do not (offspring 1, for example, has 2 F's and 2 E's).  By using the partially matched crossover,
    /// it would ensure the offspring have unique elements.  It does this by swapping out the original element
    /// that is a duplicate and swapping it out with the element at the same position of the duplicate on the other parent.
    /// So for the example above, this would result in the following offspring: AFEDBC and BDCAEF.  This technique is only
    /// used when there are two crossover points.
    /// </remarks>
    [DataContract]
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Multi")]
    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "MultiPoint")]
    [RequiredGeneticEntity(typeof(ListEntityBase))]
    [CustomComponentValidator(typeof(MultiPointCrossoverOperatorCrossoverPointValidator))]
    public class MultiPointCrossoverOperator : CrossoverOperator
    {
        private const int DefaultCrossoverPointCount = CrossoverRateMin;
        private const int CrossoverRateMin = 2;

        [DataMember]
        private int crossoverPointCount = DefaultCrossoverPointCount;

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
        public int CrossoverPointCount
        {
            get { return this.crossoverPointCount; }
            set { this.SetProperty(ref this.crossoverPointCount, value); }
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

            int minLength = Math.Min(entity1Length, entity2Length);

            // Generate the set of crossover points.
            for (int i = 0; i < this.CrossoverPointCount; i++)
            {
                int crossoverLocus;
                do
                {
                    crossoverLocus = RandomNumberService.Instance.GetRandomValue(minLength);
                } while (crossoverLoci.Contains(crossoverLocus));

                crossoverLoci.Add(crossoverLocus);
            }

            crossoverLoci.Sort();

            IList<GeneticEntity> crossoverOffspring = new List<GeneticEntity>();

            // If the number of crossover points is odd and the lengths of the entities are not the
            // same, the crossover will cause the lengths of the entities to be swapped.
            bool entityLengthsAreSwapped = (this.CrossoverPointCount % 2 != 0 && entity1Length != entity2Length);

            int maxLength = Math.Max(entity1Length, entity2Length);

            if (entityLengthsAreSwapped)
            {
                // Normalize the lists into a common length
                if (entity1Length != entity2Length)
                {
                    listEntity1.Length = maxLength;
                    listEntity2.Length = maxLength;
                }

            }

            ListEntityBase originalEntity1 = (ListEntityBase)listEntity1.Clone();
            ListEntityBase originalEntity2 = (ListEntityBase)listEntity2.Clone();

            ListEntityBase entity1Source = listEntity1;
            ListEntityBase entity2Source = listEntity2;

            for (int i = crossoverLoci[0]; i < maxLength; i++)
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

                object entity1SourceVal = null;
                object entity2SourceVal = null;

                if (i < listEntity1.Length)
                {
                    entity1SourceVal = this.GetEntityValue(i, listEntity1, entity1Source, originalEntity1, originalEntity2);
                }
                
                if (i < listEntity2.Length)
                {
                    entity2SourceVal = this.GetEntityValue(i, listEntity2, entity2Source, originalEntity2, originalEntity1);
                }
                
                if (i < listEntity1.Length)
                {
                    listEntity1.SetValue(i, entity1SourceVal);
                }
                
                if (i < listEntity2.Length)
                {
                    listEntity2.SetValue(i, entity2SourceVal);
                }
            }

            if (entityLengthsAreSwapped)
            {
                // Set the length based on their swapped length
                listEntity1.Length = entity2Length;
                listEntity2.Length = entity1Length;
            }

            crossoverOffspring.Add(listEntity1);
            crossoverOffspring.Add(listEntity2);

            return crossoverOffspring;
        }

        private object GetEntityValue(int index, ListEntityBase entity, ListEntityBase sourceEntity, ListEntityBase originalEntity, ListEntityBase originalOtherEntity)
        {
            object entitySourceVal = sourceEntity.GetValue(index);

            if (((ListEntityBase)this.Algorithm.GeneticEntitySeed).RequiresUniqueElementValues)
            {
                while (ContainsValue(entity, entitySourceVal, index - 1))
                {
                    int duplicateValueIndex = originalOtherEntity.IndexOf(entitySourceVal);
                    entitySourceVal = originalEntity.GetValue(duplicateValueIndex);
                }
            }

            return entitySourceVal;
        }

        /// <summary>
        /// Returns whether the entity contains the specified value.
        /// </summary>
        /// <param name="entity">Entity to use.</param>
        /// <param name="value">Value to search for.</param>
        /// <param name="maxIndex">The max index to iterate to when searching for the value.</param>
        /// <returns>True if the value was found; otherwise, false.</returns>
        private static bool ContainsValue(ListEntityBase entity, object value, int maxIndex)
        {
            for (int i = 0; i <= maxIndex; i++)
            {
                if (Object.Equals(entity.GetValue(i), value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
