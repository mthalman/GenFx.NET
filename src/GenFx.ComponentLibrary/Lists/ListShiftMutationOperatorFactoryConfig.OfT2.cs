using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="ListShiftMutationOperator{TMutation, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TMutation">Type of the associated mutation operator class.</typeparam>
    public class ListShiftMutationOperatorFactoryConfig<TConfiguration, TMutation> : MutationOperatorFactoryConfigBase<TConfiguration, TMutation>
        where TConfiguration : ListShiftMutationOperatorFactoryConfig<TConfiguration, TMutation>
        where TMutation : ListShiftMutationOperator<TMutation, TConfiguration>
    {
    }
}
