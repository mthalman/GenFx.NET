using System;

namespace GenFx.ComponentModel
{
    /// <summary>
    /// Base class for all classes containing configuration settings for a component.
    /// </summary>
    public abstract class ComponentConfiguration<TConfiguration, TComponent> : ComponentConfiguration
        where TConfiguration : ComponentConfiguration<TConfiguration, TComponent>
        where TComponent : GeneticComponent<TComponent, TConfiguration>
    {
        /// <summary>
        /// Gets the type of the component this configuration is associated with.
        /// </summary>
        public override Type ComponentType { get { return typeof(TComponent); } }

        /// <summary>
        /// Returns a new instance of the <typeparamref name="TComponent"/> associated with this configuration.
        /// </summary>
        /// <param name="algorithm">The algorithm associated with the component.</param>
        public new TComponent CreateComponent(IGeneticAlgorithm algorithm)
        {
            return (TComponent)base.CreateComponent(algorithm);
        }
    }
}
