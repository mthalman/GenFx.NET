using GenFx.Validation;
using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Operates upon a <see cref="ListEntityBase"/> by shifting a random segment of the list to the left or right by one position.
    /// </summary>
    [RequiredGeneticEntity(typeof(ListEntityBase))]
    public class ListShiftMutationOperator : MutationOperator
    {
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
            if (RandomNumberService.Instance.GetDouble() <= this.MutationRate)
            {
                int firstPosition = RandomNumberService.Instance.GetRandomValue(listEntity.Length);
                int secondPosition;
                do
                {
                    secondPosition = RandomNumberService.Instance.GetRandomValue(listEntity.Length);
                } while (secondPosition == firstPosition);

                if (firstPosition < secondPosition)
                {
                    object currentMovingValue = listEntity.GetValue(firstPosition);
                    for (int i = firstPosition + 1; i <= secondPosition; i++)
                    {
                        object savedValue = listEntity.GetValue(i);
                        listEntity.SetValue(i, currentMovingValue);
                        currentMovingValue = savedValue;
                    }

                    listEntity.SetValue(firstPosition, currentMovingValue);
                }
                else
                {
                    object currentMovingValue = listEntity.GetValue(firstPosition);

                    for (int i = firstPosition - 1; i >= secondPosition; i--)
                    {
                        object savedValue = listEntity.GetValue(i);
                        listEntity.SetValue(i, currentMovingValue);
                        currentMovingValue = savedValue;
                    }

                    listEntity.SetValue(firstPosition, currentMovingValue);
                }
                
                return true;
            }

            return false;
        }
    }
}
