using GenFx.Validation;
using System;
using System.Collections.Generic;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides the variable length types of <see cref="ListEntityBase"/> with variable single-point crossover support.
    /// </summary>
    /// <remarks>
    /// Variable single-point crossover chooses an element position -- potentially different -- within both of the 
    /// <see cref="ListEntityBase"/> objects and swaps the elements on either side of those
    /// points.  For example, if
    /// two <see cref="ListEntityBase"/> objects represented by 00110101 and 100011 were to
    /// be crossed over at position 2 in the first entity and position 4 in the second entity, the resulting offspring
    /// would be 0011 and 1000110101.
    /// </remarks>
    [RequiredEntity(typeof(ListEntityBase))]
    public class VariableSinglePointCrossoverOperator : CrossoverOperator
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public VariableSinglePointCrossoverOperator()
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

            int crossoverLocus1 = RandomNumberService.Instance.GetRandomValue(listEntity1.Length);
            int crossoverLocus2 = RandomNumberService.Instance.GetRandomValue(listEntity2.Length);

            IList<GeneticEntity> crossoverOffspring = new List<GeneticEntity>();

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
        /// <param name="entity"><see cref="ListEntityBase"/> whose bits are to be replaced.</param>
        /// <param name="sourceElements">List of elements to replace with.</param>
        /// <param name="targetCrossoverLocus">Element position at which to begin replacement.</param>
        /// <param name="sourceCrossoverLocus">Element position of the source elements to begin copying from.</param>
        private static void ReplaceBits(ListEntityBase entity, List<object> sourceElements, int targetCrossoverLocus, int sourceCrossoverLocus)
        {
            entity.Length = targetCrossoverLocus + sourceElements.Count - sourceCrossoverLocus;
            for (int sourceBitIndex = sourceCrossoverLocus, targetBitIndex = targetCrossoverLocus; sourceBitIndex < sourceElements.Count; sourceBitIndex++, targetBitIndex++)
            {
                entity.SetValue(targetBitIndex, sourceElements[sourceBitIndex]);
            }
        }

        /// <summary>
        /// Returns the list of bits contained in <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="ListEntityBase"/> whose bits are to be returned.</param>
        /// <returns>List of bits contained in <paramref name="entity"/>.</returns>
        private static List<object> GetEntityElements(ListEntityBase entity)
        {
            List<object> elements = new List<object>();
            for (int i = 0; i < entity.Length; i++)
            {
                elements.Add(entity.GetValue(i));
            }
            return elements;
        }
    }
}
