using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using GenFx.Validation;
using System;

namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// Provides bit list entities with uniform bit mutation operator support.
    /// </summary>
    /// <remarks>
    /// Uniform bit mutation operates upon a binary string, causing each bit of the string to
    /// mutate if it meets a certain probability.
    /// </remarks>
    [RequiredEntity(typeof(IListEntityBase<bool>))]
    public class UniformBitMutationOperator : MutationOperatorBase
    {
        /// <summary>
        /// Mutates each bit of a <see cref="IListEntityBase{T}"/> if it meets a certain
        /// probability.
        /// </summary>
        /// <param name="entity"><see cref="IListEntityBase{T}"/> to be mutated.</param>
        /// <returns>True if a mutation occurred; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        protected override bool GenerateMutation(IGeneticEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            bool isMutated = false;
            IListEntityBase<bool> bsEntity = (IListEntityBase<bool>)entity;
            for (int i = 0; i < bsEntity.Length; i++)
            {
                if (RandomNumberService.Instance.GetDouble() <= this.MutationRate)
                {
                    isMutated = true;
                    if (!bsEntity[i])
                    {
                        bsEntity[i] = true;
                    }
                    else
                    {
                        bsEntity[i] = false;
                    }
                }
            }
            return isMutated;
        }
    }
}
