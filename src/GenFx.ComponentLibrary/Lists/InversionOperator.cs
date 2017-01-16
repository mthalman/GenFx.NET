using GenFx.Validation;
using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides a <see cref="ListEntityBase"/> with inversion operator support.
    /// </summary>
    /// <remarks>
    /// Inversion operates upon a list, causing the values of two list positions to become swapped.
    /// </remarks>
    [RequiredEntity(typeof(ListEntityBase))]
    public class InversionOperator : MutationOperator
    {
        /// <summary>
        /// Mutates each element of a <see cref="ListEntityBase"/> if it meets a certain
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
            if (RandomNumberService.Instance.GetDouble() <= this.MutationRate)
            {
                int firstPosition = RandomNumberService.Instance.GetRandomValue(listEntity.Length - 1);
                int secondPosition;
                do
                {
                    secondPosition = RandomNumberService.Instance.GetRandomValue(listEntity.Length - 1);
                } while (secondPosition == firstPosition);

                object firstValue = listEntity.GetValue(firstPosition);
                listEntity.SetValue(firstPosition, listEntity.GetValue(secondPosition));
                listEntity.SetValue(secondPosition, firstValue);
                return true;
            }

            return false;
        }
    }
}
