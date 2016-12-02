using System;
using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Operates upon a <see cref="ListEntityBase"/> by shifting a random segment of the list to the left or right by one position.
    /// </summary>
    [RequiredEntity(typeof(ListEntityBase))]
    public class ListShiftMutationOperator : MutationOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListShiftMutationOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="ListShiftMutationOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public ListShiftMutationOperator(GeneticAlgorithm algorithm)
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
                int firstPosition = RandomHelper.Instance.GetRandomValue(listEntity.Length);
                int secondPosition;
                do
                {
                    secondPosition = RandomHelper.Instance.GetRandomValue(listEntity.Length);
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

    /// <summary>
    /// Represents the configuration of <see cref="ListShiftMutationOperator"/>.
    /// </summary>
    [Component(typeof(ListShiftMutationOperator))]
    public class ListShiftMutationOperatorConfiguration : MutationOperatorConfiguration
    {
    }
}
