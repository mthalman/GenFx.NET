using GenFx.Contracts;
using System;

namespace GenFx
{
    /// <summary>
    /// Represents the configuration of <see cref="DefaultTerminator"/>.
    /// </summary>
    internal class DefaultTerminatorFactoryConfig : ITerminatorFactoryConfig
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
