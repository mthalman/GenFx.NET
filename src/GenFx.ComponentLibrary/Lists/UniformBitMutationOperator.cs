using GenFx.Validation;
using System;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides bit list entities with uniform bit mutation operator support.
    /// </summary>
    /// <remarks>
    /// Uniform bit mutation operates upon a binary string, causing each bit of the string to
    /// mutate if it meets a certain probability.
    /// </remarks>
    [RequiredGeneticEntity(typeof(ListEntityBase<bool>))]
    public class UniformBitMutationOperator : MutationOperator
    {
        /// <summary>
        /// Mutates each bit of a <see cref="ListEntityBase{T}"/> if it meets a certain
        /// probability.
        /// </summary>
        /// <param name="entity"><see cref="ListEntityBase{T}"/> to be mutated.</param>
        /// <returns>True if a mutation occurred; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        protected override bool GenerateMutation(GeneticEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            bool isMutated = false;
            ListEntityBase<bool> bsEntity = (ListEntityBase<bool>)entity;
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
