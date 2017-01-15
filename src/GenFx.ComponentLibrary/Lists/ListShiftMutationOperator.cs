using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using GenFx.Validation;
using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Operates upon a <see cref="IListEntityBase"/> by shifting a random segment of the list to the left or right by one position.
    /// </summary>
    [RequiredEntity(typeof(IListEntityBase))]
    public class ListShiftMutationOperator : MutationOperatorBase
    {
        /// <summary>
        /// Mutates each bit of a <see cref="IListEntityBase"/> if it meets a certain
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
                int firstPosition = RandomNumberService.Instance.GetRandomValue(listEntity.Length);
                int secondPosition;
                do
                {
                    secondPosition = RandomNumberService.Instance.GetRandomValue(listEntity.Length);
                } while (secondPosition == firstPosition);

                if (firstPosition < secondPosition)
                {
                    object currentMovingValue = listEntity[firstPosition];
                    for (int i = firstPosition + 1; i <= secondPosition; i++)
                    {
                        object savedValue = listEntity[i];
                        listEntity[i] = currentMovingValue;
                        currentMovingValue = savedValue;
                    }

                    listEntity[firstPosition] = currentMovingValue;
                }
                else
                {
                    object currentMovingValue = listEntity[firstPosition];

                    for (int i = firstPosition - 1; i >= secondPosition; i--)
                    {
                        object savedValue = listEntity[i];
                        listEntity[i] = currentMovingValue;
                        currentMovingValue = savedValue;
                    }

                    listEntity[firstPosition] = currentMovingValue;
                }
                
                return true;
            }

            return false;
        }
    }
}
