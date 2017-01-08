using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists.BinaryStrings
{
    /// <summary>
    /// Represents the configuration of <see cref="UniformBitMutationOperator{TMutation, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TMutation">Type of the associated mutation operator class.</typeparam>
    public abstract class UniformBitMutationOperatorFactoryConfig<TConfiguration, TMutation> : MutationOperatorFactoryConfigBase<TConfiguration, TMutation>
        where TConfiguration : UniformBitMutationOperatorFactoryConfig<TConfiguration, TMutation>
        where TMutation : UniformBitMutationOperator<TMutation, TConfiguration>
    {
    }
}
