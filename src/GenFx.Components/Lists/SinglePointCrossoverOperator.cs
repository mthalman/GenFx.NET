using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GenFx.Components.Lists
{
    /// <summary>
    /// Provides a list-based entity with single-point crossover support.
    /// </summary>
    /// <remarks>
    /// Single-point element crossover chooses a single list element position and swaps the elements on
    /// either side of that point between two list-based entities.  For example, if two entities represented
    /// by 001101 and 100011 were to be crossed over at position 2, the resulting offspring would
    /// be 000011 and 101101.
    /// </remarks>
    [DataContract]
    [RequiredGeneticEntity(typeof(ListEntityBase))]
    [BooleanExternalValidator(typeof(ListEntityBase), nameof(ListEntityBase.RequiresUniqueElementValues), false)]
    public class SinglePointCrossoverOperator : CrossoverOperator
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public SinglePointCrossoverOperator()
            : base(2)
        {
        }

        /// <summary>
        /// When overriden in a derived class, generates a crossover based on the parent entities.
        /// </summary>
        /// <param name="parents">The <see cref="GeneticEntity"/> objects to be operated upon.</param>
        /// <returns>
        /// Collection of the <see cref="GeneticEntity"/> objects resulting from the crossover.
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

            int crossoverLocus = RandomNumberService.Instance.GetRandomValue(Math.Min(entity1Length, entity2Length));

            IList<GeneticEntity> crossoverOffspring = new List<GeneticEntity>();

            int maxLength = Math.Max(entity1Length, entity2Length);

            // Normalize the lists into a common length
            if (entity1Length != entity2Length)
            {
                listEntity1.Length = maxLength;
                listEntity2.Length = maxLength;
            }

            // swap the elements of the lists starting at the crossover locus
            for (int i = crossoverLocus; i < maxLength; i++)
            {
                object tempGeneValue = listEntity1.GetValue(i);
                listEntity1.SetValue(i, listEntity2.GetValue(i));
                listEntity2.SetValue(i, tempGeneValue);
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
