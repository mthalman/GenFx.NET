﻿namespace GenFx.ComponentModel
{
    /// <summary>
    /// Represents the configuration of a component that references an <see cref="IGeneticAlgorithm"/>.
    /// </summary>
    public interface IConfigurationForComponentWithAlgorithm : IComponentConfiguration
    {
        /// <summary>
        /// Returns a new instance of the <see cref="IGeneticComponent"/> associated with this configuration.
        /// </summary>
        /// <param name="algorithm">The algorithm associated with the component.</param>
        IGeneticComponent CreateComponent(IGeneticAlgorithm algorithm);
    }
}
