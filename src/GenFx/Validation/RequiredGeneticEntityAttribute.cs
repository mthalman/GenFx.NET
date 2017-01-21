using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates that a class requires a specific <see cref="GeneticEntity"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredGeneticEntityAttribute : RequiredComponentTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredGeneticEntityAttribute"/> class.
        /// </summary>
        /// <param name="entityType"><see cref="GeneticEntity"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entityType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="entityType"/> does not derive from <see cref="GeneticEntity"/>.</exception>
        public RequiredGeneticEntityAttribute(Type entityType)
            : base(entityType, typeof(GeneticEntity))
        {
        }

        /// <summary>
        /// Returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected override ComponentValidator CreateValidator()
        {
            return new RequiredGeneticEntityValidator(this.RequiredType);
        }
    }
}
