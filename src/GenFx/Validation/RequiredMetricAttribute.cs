using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates that a class requires a specific <see cref="Metric"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class RequiredMetricAttribute : RequiredComponentTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredMetricAttribute"/> class.
        /// </summary>
        /// <param name="metricType"><see cref="Metric"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="metricType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="metricType"/> does not derive from <see cref="Metric"/>.</exception>
        public RequiredMetricAttribute(Type metricType)
            : base(metricType, typeof(Metric))
        {
        }

        /// <summary>
        /// Returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected override ComponentValidator CreateValidator()
        {
            return new RequiredMetricValidator(this.RequiredType);
        }
    }
}
