using GenFx.ComponentLibrary.Base;
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
    /// <typeparam name="TMutation">Type of the deriving mutation operator class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    [RequiredEntity(typeof(IIntegerListEntity))]
    public abstract class UniformIntegerMutationOperator<TMutation, TConfiguration> : MutationOperatorBase<TMutation, TConfiguration>
        where TMutation : UniformIntegerMutationOperator<TMutation, TConfiguration>
        where TConfiguration : UniformIntegerMutationOperatorConfiguration<TConfiguration, TMutation>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected UniformIntegerMutationOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

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
                if (RandomNumberService.Instance.GetRandomPercentRatio() <= this.Configuration.MutationRate)
                {
                    IIntegerListEntityConfiguration config = (IIntegerListEntityConfiguration)this.Algorithm.ConfigurationSet.Entity;
                    int currentValue = listEntity[i];
                    int randomValue = currentValue;

                    while (randomValue == currentValue)
                    {
                        randomValue = RandomNumberService.Instance.GetRandomValue(config.MinElementValue, config.MaxElementValue + 1);
                    }

                    listEntity[i] = randomValue;

                    isMutated = true;
                }
            }
            return isMutated;
        }
    }
}
