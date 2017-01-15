using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using GenFx.Validation;
using System;
using System.Collections.Generic;

namespace GenFx.ComponentLibrary.Lists
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
    [RequiredEntity(typeof(IListEntityBase))]
    public class SinglePointCrossoverOperator : CrossoverOperatorBase
    {
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

            int crossoverLocus = RandomNumberService.Instance.GetRandomValue(Math.Min(entity1Length, entity2Length));

            IList<IGeneticEntity> crossoverOffspring = new List<IGeneticEntity>();

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
                object tempGeneValue = listEntity1[i];
                listEntity1[i] = listEntity2[i];
                listEntity2[i] = tempGeneValue;
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
