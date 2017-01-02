using System;

namespace GenFx.ComponentModel
{
    /// <summary>
    /// Represents a configuration object for a <see cref="IGeneticComponent"/>.
    /// </summary>
    /// <remarks>
    /// A configuration object defines the state available for the component to consume
    /// once it is instantiated and executed as part of the genetic algorithm.
    /// </remarks>
    public interface IComponentConfiguration
    {
        /// <summary>
        /// Gets the type of the component this configuration is associated with.
        /// </summary>
        Type ComponentType { get; }

        /// <summary>
        /// Validates the state of the configuration.
        /// </summary>
        void Validate();
    }
}
