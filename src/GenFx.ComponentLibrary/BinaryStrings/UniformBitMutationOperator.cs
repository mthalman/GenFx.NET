using System;
using System.ComponentModel;
using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.BinaryStrings
{
    /// <summary>
    /// Provides the <see cref="BinaryStringEntity"/> with uniform bit mutation operator support.
    /// </summary>
    /// <remarks>
    /// Uniform bit mutation operates upon a binary string, causing each bit of the string to
    /// mutate if it meets a certain probability.
    /// </remarks>
    [RequiredEntity(typeof(BinaryStringEntity))]
    public class UniformBitMutationOperator : MutationOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniformBitMutationOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="UniformBitMutationOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public UniformBitMutationOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Mutates each bit of a <see cref="BinaryStringEntity"/> if it meets a certain
        /// probability.
        /// </summary>
        /// <param name="entity"><see cref="BinaryStringEntity"/> to be mutated.</param>
        /// <returns>True if a mutation occurred; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        protected override bool GenerateMutation(GeneticEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            bool isMutated = false;
            BinaryStringEntity bsEntity = (BinaryStringEntity)entity;
            for (int i = 0; i < bsEntity.Length; i++)
            {
                if (RandomHelper.Instance.GetRandomRatio() <= this.MutationRate)
                {
                    isMutated = true;
                    if (bsEntity[i] == 0)
                        bsEntity[i] = 1;
                    else
                        bsEntity[i] = 0;
                }
            }
            return isMutated;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="UniformBitMutationOperator"/>.
    /// </summary>
    [Component(typeof(UniformBitMutationOperator))]
    public class UniformBitMutationOperatorConfiguration : MutationOperatorConfiguration
    {
    }
}
