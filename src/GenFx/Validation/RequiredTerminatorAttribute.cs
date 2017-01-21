using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates that a class requires a specific <see cref="Terminator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredTerminatorAttribute : RequiredComponentTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredTerminatorAttribute"/> class.
        /// </summary>
        /// <param name="terminatorType"><see cref="Terminator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="terminatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="terminatorType"/> does not derive from <see cref="Terminator"/>.</exception>
        public RequiredTerminatorAttribute(Type terminatorType)
            : base(terminatorType, typeof(Terminator))
        {
        }

        /// <summary>
        /// Returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected override ComponentValidator CreateValidator()
        {
            return new RequiredTerminatorValidator(this.RequiredType);
        }
    }
}
