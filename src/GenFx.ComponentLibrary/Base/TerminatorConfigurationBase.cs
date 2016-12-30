using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="TerminatorBase{TTerminator, TConfiguration}"/>.
    /// </summary>
    public abstract class TerminatorConfigurationBase<TConfiguration, TTerminator> : ComponentConfiguration<TConfiguration, TTerminator>, ITerminatorConfiguration
        where TConfiguration : TerminatorConfigurationBase<TConfiguration, TTerminator>
        where TTerminator : TerminatorBase<TTerminator, TConfiguration>
    {
    }
}
