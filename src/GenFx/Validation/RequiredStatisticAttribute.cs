using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates that a class requires a specific <see cref="Statistic"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class RequiredStatisticAttribute : RequiredComponentTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredStatisticAttribute"/> class.
        /// </summary>
        /// <param name="statisticType"><see cref="Statistic"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="statisticType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="statisticType"/> does not derive from <see cref="Statistic"/>.</exception>
        public RequiredStatisticAttribute(Type statisticType)
            : base(statisticType, typeof(Statistic))
        {
        }

        /// <summary>
        /// Returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected override ComponentValidator CreateValidator()
        {
            return new RequiredStatisticValidator(this.RequiredType);
        }
    }
}
