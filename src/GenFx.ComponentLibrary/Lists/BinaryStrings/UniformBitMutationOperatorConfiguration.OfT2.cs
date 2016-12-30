using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// Represents the configuration of <see cref="UniformBitMutationOperator{TMutation, TConfiguration}"/>.
    /// </summary>
    public abstract class UniformBitMutationOperatorConfiguration<TConfiguration, TMutation> : MutationOperatorConfigurationBase<TConfiguration, TMutation>
        where TConfiguration : UniformBitMutationOperatorConfiguration<TConfiguration, TMutation>
        where TMutation : UniformBitMutationOperator<TMutation, TConfiguration>
    {
    }
}
