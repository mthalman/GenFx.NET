using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates that a class requires a specific <see cref="FitnessScalingStrategy"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredFitnessScalingStrategyAttribute : RequiredComponentTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredFitnessScalingStrategyAttribute"/> class.
        /// </summary>
        /// <param name="fitnessScalingStrategyType"><see cref="FitnessScalingStrategy"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="fitnessScalingStrategyType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="fitnessScalingStrategyType"/> does not derive from <see cref="FitnessScalingStrategy"/>.</exception>
        public RequiredFitnessScalingStrategyAttribute(Type fitnessScalingStrategyType)
            : base(fitnessScalingStrategyType, typeof(FitnessScalingStrategy))
        {
        }

        /// <summary>
        /// Returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected override ComponentValidator CreateValidator()
        {
            return new RequiredFitnessScalingStrategyValidator(this.RequiredType);
        }
    }
}
