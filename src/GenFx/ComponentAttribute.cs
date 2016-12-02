using System;
using GenFx.ComponentModel;
using GenFx.Properties;

namespace GenFx
{
    /// <summary>
    /// Specifies the component class associated with the attributed component configuration class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class ComponentAttribute : Attribute
    {
        /// <summary>
        /// Gets the type of the component class associated with the attributed component configuration class.
        /// </summary>
        public Type ComponentType { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentAttribute"/> class.
        /// </summary>
        /// <param name="componentType">Type of the component class associated with the attributed component configuration class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="componentType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="componentType"/> does not derive from <see cref="GeneticComponent"/>.</exception>
        public ComponentAttribute(Type componentType)
        {
            if (componentType == null)
            {
                throw new ArgumentNullException(nameof(componentType));
            }

            if (!typeof(GeneticComponent).IsAssignableFrom(componentType))
            {
                throw new ArgumentException(StringUtil.GetFormattedString(FwkResources.ErrorMsg_InvalidType, typeof(GeneticComponent).FullName), nameof(componentType));
            }

            this.ComponentType = componentType;
        }
    }
}
