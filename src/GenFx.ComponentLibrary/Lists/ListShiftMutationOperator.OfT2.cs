using GenFx.ComponentLibrary.Base;
using GenFx.Validation;
using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Operates upon a <see cref="IListEntityBase"/> by shifting a random segment of the list to the left or right by one position.
    /// </summary>
    /// <typeparam name="TMutation">Type of the deriving mutation operator class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    [RequiredEntity(typeof(IListEntityBase))]
    public abstract class ListShiftMutationOperator<TMutation, TConfiguration> : MutationOperatorBase<TMutation, TConfiguration>
        where TMutation : ListShiftMutationOperator<TMutation, TConfiguration>
        where TConfiguration : ListShiftMutationOperatorConfiguration<TConfiguration, TMutation>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected ListShiftMutationOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

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
            if (RandomNumberService.Instance.GetRandomPercentRatio() <= this.Configuration.MutationRate)
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
