using System;

namespace GenFx.ComponentModel
{
    /// <summary>
    /// Represents a configuration object for a <see cref="IGeneticComponent"/>.
    /// </summary>
    public interface IComponentConfiguration
    {
        /// <summary>
        /// Gets the type of the component this configuration is associated with.
        /// </summary>
        Type ComponentType { get; }

        /// <summary>
        /// Returns a new instance of the <see cref="IGeneticComponent"/> associated with this configuration.
        /// </summary>
        /// <param name="algorithm">The algorithm associated with the component.</param>
        IGeneticComponent CreateComponent(IGeneticAlgorithm algorithm);
    }
}
