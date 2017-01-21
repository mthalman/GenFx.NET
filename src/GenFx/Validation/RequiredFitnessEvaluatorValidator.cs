using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Validates a component such that when it is used in a genetic algorithm, the genetic algorithm is also configured
    /// to use the required fitness evaluator type.
    /// </summary>
    public sealed class RequiredFitnessEvaluatorValidator : RequiredComponentTypeValidator
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="requiredFitnessEvaluatorType">The fitness evaluator type that is required by the component in context.</param>
        public RequiredFitnessEvaluatorValidator(Type requiredFitnessEvaluatorType)
            : base(requiredFitnessEvaluatorType, typeof(FitnessEvaluator))
        {
        }

        /// <summary>
        /// Gets the friendly name of the component type.
        /// </summary>
        protected override string ComponentFriendlyName { get { return Resources.FitnessEvaluatorCommonName; } }

        /// <summary>
        /// Returns a value indicating whether the genetic algorithm is configured with the required component.
        /// </summary>
        /// <param name="algorithmContext">The <see cref="GeneticAlgorithm"/> currently in context.</param>
        /// <returns>true if the genetic algorithm is configured with the required component; otherwise, false.</returns>
        protected override bool HasRequiredComponent(GeneticAlgorithm algorithmContext)
        {
            if (algorithmContext == null)
            {
                throw new ArgumentNullException(nameof(algorithmContext));
            }

            return this.IsEquivalentType(algorithmContext.FitnessEvaluator);
        }
    }
}
