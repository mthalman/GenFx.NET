using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates that a class requires a specific <see cref="CrossoverOperator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredCrossoverOperatorAttribute : RequiredComponentTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredCrossoverOperatorAttribute"/> class.
        /// </summary>
        /// <param name="crossoverOperatorType"><see cref="CrossoverOperator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="crossoverOperatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="crossoverOperatorType"/> does not derive from <see cref="CrossoverOperator"/>.</exception>
        public RequiredCrossoverOperatorAttribute(Type crossoverOperatorType)
            : base(crossoverOperatorType, typeof(CrossoverOperator))
        {
        }

        /// <summary>
        /// Returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected override ComponentValidator CreateValidator()
        {
            return new RequiredCrossoverOperatorValidator(this.RequiredType);
        }
    }
}
