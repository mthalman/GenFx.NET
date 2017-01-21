using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates that a class requires a specific <see cref="Population"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredPopulationAttribute : RequiredComponentTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredPopulationAttribute"/> class.
        /// </summary>
        /// <param name="populationType"><see cref="Population"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="populationType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="populationType"/> does not derive from <see cref="Population"/>.</exception>
        public RequiredPopulationAttribute(Type populationType)
            : base(populationType, typeof(Population))
        {
        }

        /// <summary>
        /// Returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected override ComponentValidator CreateValidator()
        {
            return new RequiredPopulationValidator(this.RequiredType);
        }
    }
}
