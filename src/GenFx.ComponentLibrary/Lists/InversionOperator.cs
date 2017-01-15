using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using GenFx.Validation;
using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides a <see cref="IListEntityBase"/> with inversion operator support.
    /// </summary>
    /// <remarks>
    /// Inversion operates upon a list, causing the values of two list positions to become swapped.
    /// </remarks>
    [RequiredEntity(typeof(IListEntityBase))]
    public class InversionOperator : MutationOperatorBase
    {
        /// <summary>
        /// Mutates each element of a <see cref="IListEntityBase"/> if it meets a certain
        /// probability.
        /// </summary>
        /// <param name="entity"><see cref="IListEntityBase"/> to be mutated.</param>
        /// <returns>True if a mutation occurred; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        protected override bool GenerateMutation(IGeneticEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            IListEntityBase listEntity = (IListEntityBase)entity;
            if (RandomNumberService.Instance.GetDouble() <= this.MutationRate)
            {
                int firstPosition = RandomNumberService.Instance.GetRandomValue(listEntity.Length - 1);
                int secondPosition;
                do
                {
                    secondPosition = RandomNumberService.Instance.GetRandomValue(listEntity.Length - 1);
                } while (secondPosition == firstPosition);

                object firstValue = listEntity[firstPosition];
                listEntity[firstPosition] = listEntity[secondPosition];
                listEntity[secondPosition] = firstValue;
                return true;
            }

            return false;
        }
    }
}
