using GenFx.ComponentModel;
using System;
using System.Reflection;

namespace GenFx.ComponentLibrary.ComponentModel
{
    /// <summary>
    /// The base class for the configuration of a component that references an <see cref="IGeneticAlgorithm"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">The type of the deriving configuration class.</typeparam>
    /// <typeparam name="TComponent">Type of the associated component class.</typeparam>
    public abstract class ConfigurationForComponentWithAlgorithm<TConfiguration, TComponent>
        : ComponentConfiguration<TConfiguration, TComponent>, IConfigurationForComponentWithAlgorithm
        where TConfiguration : ComponentConfiguration<TConfiguration, TComponent>
        where TComponent : GeneticComponent<TComponent, TConfiguration>
    {
        /// <summary>
        /// Returns a new instance of the <typeparamref name="TComponent"/> associated with this configuration.
        /// </summary>
        /// <param name="algorithm">The algorithm associated with the component.</param>
        /// <remarks>
        /// The associated component type must have a constructor which takes a single parameter of type <see cref="IGeneticAlgorithm"/>.
        /// </remarks>
        public TComponent CreateComponent(IGeneticAlgorithm algorithm)
        {
            try
            {
                return (TComponent)Activator.CreateInstance(this.ComponentType, new object[] { algorithm });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        IGeneticComponent IConfigurationForComponentWithAlgorithm.CreateComponent(IGeneticAlgorithm algorithm)
        {
            return this.CreateComponent(algorithm);
        }
    }
}
