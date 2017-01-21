using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates that a class requires a specific <see cref="SelectionOperator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredSelectionOperatorAttribute : RequiredComponentTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredSelectionOperatorAttribute"/> class.
        /// </summary>
        /// <param name="selectionOperatorType"><see cref="SelectionOperator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="selectionOperatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="selectionOperatorType"/> does not derive from <see cref="SelectionOperator"/>.</exception>
        public RequiredSelectionOperatorAttribute(Type selectionOperatorType)
            : base(selectionOperatorType, typeof(SelectionOperator))
        {
        }

        /// <summary>
        /// Returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected override ComponentValidator CreateValidator()
        {
            return new RequiredSelectionOperatorValidator(this.RequiredType);
        }
    }
}
