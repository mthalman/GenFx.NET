using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates that a class requires a specific <see cref="Terminator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredPluginAttribute : RequiredComponentTypeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredPluginAttribute"/> class.
        /// </summary>
        /// <param name="pluginType"><see cref="Plugin"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="pluginType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="pluginType"/> does not derive from <see cref="Terminator"/>.</exception>
        public RequiredPluginAttribute(Type pluginType)
            : base(pluginType, typeof(Plugin))
        {
        }

        /// <summary>
        /// Returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected override ComponentValidator CreateValidator()
        {
            return new RequiredPluginValidator(this.RequiredType);
        }
    }
}
