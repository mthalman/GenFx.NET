using GenFx.ComponentModel;
using System;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Provides the abstract base class for a genetic algorithm mutation operator.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Mutations in genetic algorithms involve the altering of a single data component in a entity.
    /// For example, a binary string entity has a chance of mutating one of its bits before progressing
    /// to the next generation.  Genetic algorithm mutation is intended to be similar to gene copying 
    /// errors in nature.  Mutations are the driver of randomness in a population.
    /// </para>
    /// <para>
    /// <b>Notes to implementers:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by setting the <see cref="ComponentConfigurationSet.MutationOperator"/> 
    /// property.
    /// </para>
    /// </remarks>
    public abstract class MutationOperatorBase<TMutation, TConfiguration> : GeneticComponentWithAlgorithm<TMutation, TConfiguration>, IMutationOperator
        where TMutation : MutationOperatorBase<TMutation, TConfiguration>
        where TConfiguration : MutationOperatorConfigurationBase<TConfiguration, TMutation>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected MutationOperatorBase(IGeneticAlgorithm algorithm)
            : base(algorithm, GetConfiguration(algorithm, c => c.MutationOperator))
        {
        }

        /// <summary>
        /// Attempts to mutate the <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="IGeneticEntity"/> to be mutated.</param>
        /// <returns>
        /// A potentially mutated clone of the <paramref name="entity"/>.
        /// </returns>
        /// <remarks>
        /// If the <see cref="IGeneticEntity"/> was mutated, its <see cref="IGeneticEntity.Age"/> property will be set to zero.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public IGeneticEntity Mutate(IGeneticEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            IGeneticEntity clonedEntity = entity.Clone();
            bool isMutated = this.GenerateMutation(clonedEntity);
            if (isMutated)
            {
                clonedEntity.Age = 0;
            }

            return clonedEntity;
        }

        /// <summary>
        /// When overriden in a derived class, attempts to mutate the <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="IGeneticEntity"/> to be mutated.</param>
        /// <returns>true if a mutation occurred; otherwise, false.</returns>
        /// <remarks>
        /// <b>Notes to implementers:</b> When this method is overriden, each segment of data making up the 
        /// representation of the <see cref="IGeneticEntity"/> should be attempted to be mutated.  Use the 
        /// <see cref="MutationOperatorConfigurationBase{TConfiguration, TMutation}.MutationRate"/> property to determine whether a component
        /// of the <see cref="IGeneticEntity"/> should be mutated or not.  
        /// </remarks>
        protected abstract bool GenerateMutation(IGeneticEntity entity);
    }
}
