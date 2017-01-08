using GenFx.Contracts;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="TerminatorBase{TTerminator, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TTerminator">Type of the associated terminator class.</typeparam>
    public abstract class TerminatorFactoryConfigBase<TConfiguration, TTerminator> : ConfigurationForComponentWithAlgorithm<TConfiguration, TTerminator>, ITerminatorFactoryConfig
        where TConfiguration : TerminatorFactoryConfigBase<TConfiguration, TTerminator>
        where TTerminator : TerminatorBase<TTerminator, TConfiguration>
    {
    }
}
