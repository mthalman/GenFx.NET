using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates that a class requires a specific <see cref="FitnessEvaluator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredFitnessEvaluatorAttribute : RequiredComponentTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredFitnessEvaluatorAttribute"/> class.
        /// </summary>
        /// <param name="fitnessEvaluatorType"><see cref="FitnessEvaluator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="fitnessEvaluatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="fitnessEvaluatorType"/> does not derive from <see cref="FitnessEvaluator"/>.</exception>
        public RequiredFitnessEvaluatorAttribute(Type fitnessEvaluatorType)
            : base(fitnessEvaluatorType, typeof(FitnessEvaluator))
        {
        }

        /// <summary>
        /// Returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected override ComponentValidator CreateValidator()
        {
            return new RequiredFitnessEvaluatorValidator(this.RequiredType);
        }
    }
}
