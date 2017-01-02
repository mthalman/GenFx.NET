using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="UniformIntegerMutationOperator{TMutation, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TMutation">Type of the associated mutation operator class.</typeparam>
    public abstract class UniformIntegerMutationOperatorConfiguration<TConfiguration, TMutation> : MutationOperatorConfigurationBase<TConfiguration, TMutation>
        where TConfiguration : UniformIntegerMutationOperatorConfiguration<TConfiguration, TMutation> 
        where TMutation : UniformIntegerMutationOperator<TMutation, TConfiguration>
    {
    }
}
