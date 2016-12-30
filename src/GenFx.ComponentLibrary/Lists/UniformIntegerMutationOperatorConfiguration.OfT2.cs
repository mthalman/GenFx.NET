using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="UniformIntegerMutationOperator{TMutation, TConfiguration}"/>.
    /// </summary>
    public abstract class UniformIntegerMutationOperatorConfiguration<TConfiguration, TMutation> : MutationOperatorConfigurationBase<TConfiguration, TMutation>
        where TConfiguration : UniformIntegerMutationOperatorConfiguration<TConfiguration, TMutation> 
        where TMutation : UniformIntegerMutationOperator<TMutation, TConfiguration>
    {
    }
}
