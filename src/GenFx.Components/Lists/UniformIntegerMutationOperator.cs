using GenFx.Validation;
using System;
using System.Runtime.Serialization;

namespace GenFx.Components.Lists
{
    /// <summary>
    /// Provides the <see cref="IntegerListEntity"/> with uniform integer mutation operator support.
    /// </summary>
    /// <remarks>
    /// Uniform integer mutation operates upon an integer list, causing each integer of the list to
    /// mutate if it meets a certain probability.
    /// </remarks>
    [DataContract]
    [RequiredGeneticEntity(typeof(IntegerListEntity))]
    public class UniformIntegerMutationOperator : MutationOperator
    {
        /// <summary>
        /// Mutates each bit of a <see cref="IntegerListEntity"/> if it meets a certain
        /// probability.
        /// </summary>
        /// <param name="entity"><see cref="IntegerListEntity"/> to be mutated.</param>
        /// <returns>True if a mutation occurred; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        protected override bool GenerateMutation(GeneticEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            bool isMutated = false;
            IntegerListEntity listEntity = (IntegerListEntity)entity;
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
