using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates that a class requires a specific <see cref="ElitismStrategy"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredElitismStrategyAttribute : RequiredComponentTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredElitismStrategyAttribute"/> class.
        /// </summary>
        /// <param name="elitismStrategyType"><see cref="ElitismStrategy"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="elitismStrategyType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="elitismStrategyType"/> does not derive from <see cref="ElitismStrategy"/>.</exception>
        public RequiredElitismStrategyAttribute(Type elitismStrategyType)
            : base(elitismStrategyType, typeof(ElitismStrategy))
        {
        }

        /// <summary>
        /// Returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected override ComponentValidator CreateValidator()
        {
            return new RequiredElitismStrategyValidator(this.RequiredType);
        }
    }
}
