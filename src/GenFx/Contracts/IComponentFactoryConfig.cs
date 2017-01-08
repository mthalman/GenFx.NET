using System;

namespace GenFx.Contracts
{
    /// <summary>
    /// Represents a configuration object for a <see cref="IGeneticComponent"/>.
    /// </summary>
    /// <remarks>
    /// A configuration object defines the state available for the component to consume
    /// once it is instantiated and executed as part of the genetic algorithm.  It also acts
    /// as a factory object, providing instances of the component when needed.
    /// </remarks>
    public interface IComponentFactoryConfig
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
