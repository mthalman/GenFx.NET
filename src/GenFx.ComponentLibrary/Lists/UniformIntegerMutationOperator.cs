using System;
using System.ComponentModel;
using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Provides the <see cref="IntegerListEntity"/> with uniform integer mutation operator support.
    /// </summary>
    /// <remarks>
    /// Uniform integer mutation operates upon an integer list, causing each integer of the list to
    /// mutate if it meets a certain probability.
    /// </remarks>
    [RequiredEntity(typeof(IntegerListEntity))]
    public class UniformIntegerMutationOperator : MutationOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniformIntegerMutationOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="UniformIntegerMutationOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public UniformIntegerMutationOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Mutates each bit of a <see cref="IntegerListEntity"/> if it meets a certain
        /// probability.
        /// </summary>
        /// <param name="entity"><see cref="IntegerListEntity"/> to be mutated.</param>
        /// <returns>True if a mutation occurred; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        protected override bool GenerateMutation(GeneticEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            bool isMutated = false;
            IntegerListEntity listEntity = (IntegerListEntity)entity;
            for (int i = 0; i < listEntity.Length; i++)
            {
                if (RandomHelper.Instance.GetRandomRatio() <= this.MutationRate)
                {
                    IntegerListEntityConfiguration config = (IntegerListEntityConfiguration)this.Algorithm.ConfigurationSet.Entity;
                    int currentValue = listEntity[i];
                    int randomValue = currentValue;

                    while (randomValue == currentValue)
                    {
                        randomValue = RandomHelper.Instance.GetRandomValue(config.MinElementValue, config.MaxElementValue + 1);
                    }

                    listEntity[i] = randomValue;

                    isMutated = true;
                }
            }
            return isMutated;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="UniformIntegerMutationOperator"/>.
    /// </summary>
    [Component(typeof(UniformIntegerMutationOperator))]
    public class UniformIntegerMutationOperatorConfiguration : MutationOperatorConfiguration
    {
    }
}
