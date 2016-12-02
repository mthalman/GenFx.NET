using System;
using System.ComponentModel;
using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides a <see cref="ListEntityBase"/> with inversion operator support.
    /// </summary>
    /// <remarks>
    /// Inversion operates upon a list, causing the values of two list positions to become swapped.
    /// </remarks>
    [RequiredEntity(typeof(ListEntityBase))]
    public abstract class InversionOperator : MutationOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InversionOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="InversionOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected InversionOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Mutates each bit of a <see cref="ListEntityBase"/> if it meets a certain
        /// probability.
        /// </summary>
        /// <param name="entity"><see cref="ListEntityBase"/> to be mutated.</param>
        /// <returns>True if a mutation occurred; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        protected override bool GenerateMutation(GeneticEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            ListEntityBase listEntity = (ListEntityBase)entity;
            if (RandomHelper.Instance.GetRandomRatio() <= this.MutationRate)
            {
                int firstPosition = RandomHelper.Instance.GetRandomValue(listEntity.Length - 1);
                int secondPosition;
                do
                {
                    secondPosition = RandomHelper.Instance.GetRandomValue(listEntity.Length - 1);
                } while (secondPosition == firstPosition);

                object firstValue = listEntity[firstPosition];
                listEntity[firstPosition] = listEntity[secondPosition];
                listEntity[secondPosition] = firstValue;
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="InversionOperator"/>.
    /// </summary>
    [Component(typeof(InversionOperator))]
    public abstract class InversionOperatorConfiguration : MutationOperatorConfiguration
    {
    }
}
