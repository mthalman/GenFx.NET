using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates that a class requires a specific <see cref="GeneticAlgorithm"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredGeneticAlgorithmAttribute : RequiredComponentTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredGeneticAlgorithmAttribute"/> class.
        /// </summary>
        /// <param name="geneticAlgorithmType"><see cref="GeneticAlgorithm"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="geneticAlgorithmType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="geneticAlgorithmType"/> does not derive from <see cref="GeneticAlgorithm"/>.</exception>
        public RequiredGeneticAlgorithmAttribute(Type geneticAlgorithmType)
            : base(geneticAlgorithmType, typeof(GeneticAlgorithm))
        {
        }

        /// <summary>
        /// Returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected override ComponentValidator CreateValidator()
        {
            return new RequiredGeneticAlgorithmValidator(this.RequiredType);
        }
    }
}
