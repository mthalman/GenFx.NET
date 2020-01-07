using GenFx.Validation;
using System;
using System.Runtime.Serialization;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for a genetic algorithm mutation operator.
    /// </summary>
    /// <remarks>
    /// Mutations in genetic algorithms involve the altering of a single data component in a entity.
    /// For example, a binary string entity has a chance of mutating one of its bits before progressing
    /// to the next generation.  Genetic algorithm mutation is intended to be similar to gene copying 
    /// errors in nature.  Mutations are the driver of randomness in a population.
    /// </remarks>
    [DataContract]
    public abstract class MutationOperator : GeneticComponentWithAlgorithm
    {
        private const double DefaultMutationRate = .001;

        [DataMember]
        private double mutationRate = DefaultMutationRate;

        /// <summary>
        /// Gets or sets the probability that a data segment within a <see cref="GeneticEntity"/> will become mutated.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [DoubleValidator(MinValue = 0, MaxValue = 1)]
        public double MutationRate
        {
            get { return this.mutationRate; }
            set { this.SetProperty(ref this.mutationRate, value); }
        }

        /// <summary>
        /// Attempts to mutate the <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="GeneticEntity"/> to be mutated.</param>
        /// <returns>
        /// A potentially mutated clone of the <paramref name="entity"/>.
        /// </returns>
        /// <remarks>
        /// If the <see cref="GeneticEntity"/> was mutated, its <see cref="GeneticEntity.Age"/> property will be set to zero.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public GeneticEntity Mutate(GeneticEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            GeneticEntity clonedEntity = entity.Clone();
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
        /// <param name="entity"><see cref="GeneticEntity"/> to be mutated.</param>
        /// <returns>true if a mutation occurred; otherwise, false.</returns>
        /// <remarks>
        /// <b>Notes to implementers:</b> When this method is overriden, each segment of data making up the 
        /// representation of the <see cref="GeneticEntity"/> should be attempted to be mutated.  Use the 
        /// <see cref="MutationRate"/> property to determine whether a component
        /// of the <see cref="GeneticEntity"/> should be mutated or not.  
        /// </remarks>
        protected abstract bool GenerateMutation(GeneticEntity entity);
    }
}
