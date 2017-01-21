using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates that a class requires a specific <see cref="MutationOperator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredMutationOperatorAttribute : RequiredComponentTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredMutationOperatorAttribute"/> class.
        /// </summary>
        /// <param name="mutationOperatorType"><see cref="MutationOperator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="mutationOperatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="mutationOperatorType"/> does not derive from <see cref="MutationOperator"/>.</exception>
        public RequiredMutationOperatorAttribute(Type mutationOperatorType)
            : base(mutationOperatorType, typeof(MutationOperator))
        {
        }

        /// <summary>
        /// Returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected override ComponentValidator CreateValidator()
        {
            return new RequiredMutationOperatorValidator(this.RequiredType);
        }
    }
}
