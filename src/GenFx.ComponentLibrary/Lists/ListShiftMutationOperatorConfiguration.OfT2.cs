using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="ListShiftMutationOperator{TMutation, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TMutation">Type of the associated mutation operator class.</typeparam>
    public class ListShiftMutationOperatorConfiguration<TConfiguration, TMutation> : MutationOperatorConfigurationBase<TConfiguration, TMutation>
        where TConfiguration : ListShiftMutationOperatorConfiguration<TConfiguration, TMutation>
        where TMutation : ListShiftMutationOperator<TMutation, TConfiguration>
    {
    }
}
