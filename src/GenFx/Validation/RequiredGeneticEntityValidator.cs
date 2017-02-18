using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Validates a component such that when it is used in a genetic algorithm, the genetic algorithm is also configured
    /// to use the required genetic entity type.
    /// </summary>
    public sealed class RequiredGeneticEntityValidator : RequiredComponentTypeValidator
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="requiredGeneticEntityType">The genetic algorithm type that is required by the component in context.</param>
        public RequiredGeneticEntityValidator(Type requiredGeneticEntityType)
            : base(requiredGeneticEntityType, typeof(GeneticEntity))
        {
        }

        /// <summary>
        /// Gets the friendly name of the component type.
        /// </summary>
        protected override string ComponentFriendlyName { get { return Resources.EntityCommonName; } }

        /// <summary>
        /// Returns a value indicating whether the genetic algorithm is configured with the required component.
        /// </summary>
        /// <param name="algorithmContext">The <see cref="GeneticAlgorithm"/> currently in context.</param>
        /// <returns>true if the genetic algorithm is configured with the required component; otherwise, false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        protected override bool HasRequiredComponent(GeneticAlgorithm algorithmContext)
        {
            return this.IsEquivalentType(algorithmContext.GeneticEntitySeed);
        }
    }
}
