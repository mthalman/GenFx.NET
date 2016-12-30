using GenFx.ComponentModel;
using System;

namespace GenFx
{
    /// <summary>
    /// Represents the configuration of <see cref="DefaultTerminator"/>.
    /// </summary>
    internal class DefaultTerminatorConfiguration : ITerminatorConfiguration
    {
        public Type ComponentType { get { return typeof(DefaultTerminator); } }

        public IGeneticComponent CreateComponent(IGeneticAlgorithm algorithm)
        {
            return new DefaultTerminator(algorithm, this);
        }

        public void Validate()
        {
        }
    }
}
