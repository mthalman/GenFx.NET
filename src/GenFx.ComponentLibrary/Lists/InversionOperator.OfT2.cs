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
    /// <typeparam name="TInversion">Type of the deriving mutation operator class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    [RequiredEntity(typeof(IListEntityBase))]
    public abstract class InversionOperator<TInversion, TConfiguration> : MutationOperatorBase<TInversion, TConfiguration>
        where TInversion : InversionOperator<TInversion, TConfiguration>
        where TConfiguration : InversionOperatorFactoryConfig<TConfiguration, TInversion>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected InversionOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

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
            if (RandomNumberService.Instance.GetRandomPercentRatio() <= this.Configuration.MutationRate)
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
