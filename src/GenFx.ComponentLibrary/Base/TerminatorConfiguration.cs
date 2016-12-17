using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="Terminator{TTerminator, TConfiguration}"/>.
    /// </summary>
    public abstract class TerminatorConfiguration<TConfiguration, TTerminator> : ComponentConfiguration<TConfiguration, TTerminator>, ITerminatorConfiguration
        where TConfiguration : TerminatorConfiguration<TConfiguration, TTerminator>
        where TTerminator : Terminator<TTerminator, TConfiguration>
    {
    }
}
