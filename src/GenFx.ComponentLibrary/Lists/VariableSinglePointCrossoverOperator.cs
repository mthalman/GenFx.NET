using System;
using System.Collections.Generic;
using System.ComponentModel;
using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides the variable length types of <see cref="ListEntityBase"/> with variable single-point bit crossover support.
    /// </summary>
    /// <remarks>
    /// Variable single-point bit crossover chooses a bit position -- potentially different -- within both of the 
    /// <see cref="ListEntityBase"/> objects and swaps the bits on either side of those
    /// points.  For example, if
    /// two <see cref="ListEntityBase"/> objects represented by 00110101 and 100011 were to
    /// be crossed over at position 2 in the first entity and position 4 in the second entity, the resulting offspring
    /// would be 0011 and 1000110101.
    /// </remarks>
    [RequiredEntity(typeof(ListEntityBase))]
    public abstract class VariableSinglePointCrossoverOperator : CrossoverOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VariableSinglePointCrossoverOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="VariableSinglePointCrossoverOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected VariableSinglePointCrossoverOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Executes a single-point crossover between two <see cref="ListEntityBase"/> objects.
        /// </summary>
        /// <param name="entity1"><see cref="GeneticEntity"/> to be crossed over with <paramref name="entity2"/>.</param>
        /// <param name="entity2"><see cref="GeneticEntity"/> to be crossed over with <paramref name="entity1"/>.</param>
        /// <returns>
        /// Collection of the <see cref="ListEntityBase"/> objects resulting from the crossover.  If no
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

            int crossoverLocus1 = RandomHelper.Instance.GetRandomValue(listEntity1.Length);
            int crossoverLocus2 = RandomHelper.Instance.GetRandomValue(listEntity2.Length);

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
                entity[targetBitIndex] = sourceElements[sourceBitIndex];
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
                elements.Add(entity[i]);
            }
            return elements;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="VariableSinglePointCrossoverOperator"/>.
    /// </summary>
    [Component(typeof(VariableSinglePointCrossoverOperator))]
    public abstract class VariableSinglePointCrossoverOperatorConfiguration : CrossoverOperatorConfiguration
    {
    }
}
