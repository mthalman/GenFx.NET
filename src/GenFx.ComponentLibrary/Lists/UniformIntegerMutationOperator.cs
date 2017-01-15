using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using GenFx.Validation;
using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides the <see cref="IIntegerListEntity"/> with uniform integer mutation operator support.
    /// </summary>
    /// <remarks>
    /// Uniform integer mutation operates upon an integer list, causing each integer of the list to
    /// mutate if it meets a certain probability.
    /// </remarks>
    [RequiredEntity(typeof(IIntegerListEntity))]
    public class UniformIntegerMutationOperator : MutationOperatorBase
    {
        /// <summary>
        /// Mutates each bit of a <see cref="IIntegerListEntity"/> if it meets a certain
        /// probability.
        /// </summary>
        /// <param name="entity"><see cref="IIntegerListEntity"/> to be mutated.</param>
        /// <returns>True if a mutation occurred; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        protected override bool GenerateMutation(IGeneticEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            bool isMutated = false;
            IIntegerListEntity listEntity = (IIntegerListEntity)entity;
            for (int i = 0; i < listEntity.Length; i++)
            {
                if (RandomNumberService.Instance.GetDouble() <= this.MutationRate)
                {
                    int currentValue = listEntity[i];
                    int randomValue = currentValue;

                    while (randomValue == currentValue)
                    {
                        randomValue = RandomNumberService.Instance.GetRandomValue(listEntity.MinElementValue, listEntity.MaxElementValue + 1);
                    }

                    listEntity[i] = randomValue;

                    isMutated = true;
                }
            }
            return isMutated;
        }
    }
}
