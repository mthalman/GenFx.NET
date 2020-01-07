using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Validates a component such that when it is used in a genetic algorithm, the genetic algorithm is also configured
    /// to use the required mutation operator type.
    /// </summary>
    public sealed class RequiredMutationOperatorValidator : RequiredComponentTypeValidator
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="requiredMutationOperatorType">The mutation operator type that is required by the component in context.</param>
        public RequiredMutationOperatorValidator(Type requiredMutationOperatorType)
            : base(requiredMutationOperatorType, typeof(MutationOperator))
        {
        }

        /// <summary>
        /// Gets the friendly name of the component type.
        /// </summary>
        protected override string ComponentFriendlyName { get { return Resources.MutationCommonName; } }

        /// <summary>
        /// Returns a value indicating whether the genetic algorithm is configured with the required component.
        /// </summary>
        /// <param name="algorithmContext">The <see cref="GeneticAlgorithm"/> currently in context.</param>
        /// <returns>true if the genetic algorithm is configured with the required component; otherwise, false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        protected override bool HasRequiredComponent(GeneticAlgorithm algorithmContext)
        {
            if (algorithmContext is null)
            {
                throw new ArgumentNullException(nameof(algorithmContext));
            }

            return this.IsEquivalentType(algorithmContext.MutationOperator);
        }
    }
}
