using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using GenFx.Validation;
using System;
using System.Collections.Generic;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides the variable length types of <see cref="IListEntityBase"/> with variable single-point crossover support.
    /// </summary>
    /// <remarks>
    /// Variable single-point crossover chooses an element position -- potentially different -- within both of the 
    /// <see cref="IListEntityBase"/> objects and swaps the elements on either side of those
    /// points.  For example, if
    /// two <see cref="IListEntityBase"/> objects represented by 00110101 and 100011 were to
    /// be crossed over at position 2 in the first entity and position 4 in the second entity, the resulting offspring
    /// would be 0011 and 1000110101.
    /// </remarks>
    [RequiredEntity(typeof(IListEntityBase))]
    public class VariableSinglePointCrossoverOperator : CrossoverOperatorBase
    {
        /// <summary>
        /// Executes a single-point crossover between two <see cref="IListEntityBase"/> objects.
        /// </summary>
        /// <param name="entity1"><see cref="IGeneticEntity"/> to be crossed over with <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="IGeneticEntity"/> to be crossed over with <paramref name="entity1"/>.</param>
        /// <returns>
        /// Collection of the <see cref="IListEntityBase"/> objects resulting from the crossover.  If no
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

            int crossoverLocus1 = RandomNumberService.Instance.GetRandomValue(listEntity1.Length);
            int crossoverLocus2 = RandomNumberService.Instance.GetRandomValue(listEntity2.Length);

            IList<IGeneticEntity> crossoverOffspring = new List<IGeneticEntity>();

            List<object> entity1Elements = GetEntityElements(listEntity1);
            List<object> entity2Elements = GetEntityElements(listEntity2);

            ReplaceBits(listEntity1, entity2Elements, crossoverLocus1, crossoverLocus2);
            ReplaceBits(listEntity2, entity1Elements, crossoverLocus2, crossoverLocus1);

            crossoverOffspring.Add(listEntity1);
            crossoverOffspring.Add(listEntity2);

            return crossoverOffspring;
        }

        /// <summary>
        /// Replaces the elements in <paramref name="entity"/>, starting at <paramref name="targetCrossoverLocus"/>,
        /// with the elements located in <paramref name="sourceElements"/> starting at <paramref name="sourceCrossoverLocus"/>.
        /// </summary>
        /// <param name="entity"><see cref="IListEntityBase"/> whose bits are to be replaced.</param>
        /// <param name="sourceElements">List of elements to replace with.</param>
        /// <param name="targetCrossoverLocus">Element position at which to begin replacement.</param>
        /// <param name="sourceCrossoverLocus">Element position of the source elements to begin copying from.</param>
        private static void ReplaceBits(IListEntityBase entity, List<object> sourceElements, int targetCrossoverLocus, int sourceCrossoverLocus)
        {
            entity.Length = targetCrossoverLocus + sourceElements.Count - sourceCrossoverLocus;
            for (int sourceBitIndex = sourceCrossoverLocus, targetBitIndex = targetCrossoverLocus; sourceBitIndex < sourceElements.Count; sourceBitIndex++, targetBitIndex++)
            {
                entity[targetBitIndex] = sourceElements[sourceBitIndex];
            }
        }

        /// <summary>
        /// Returns the list of bits contained in <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="IListEntityBase"/> whose bits are to be returned.</param>
        /// <returns>List of bits contained in <paramref name="entity"/>.</returns>
        private static List<object> GetEntityElements(IListEntityBase entity)
        {
            List<object> elements = new List<object>();
            for (int i = 0; i < entity.Length; i++)
            {
                elements.Add(entity[i]);
            }
            return elements;
        }
    }
}
