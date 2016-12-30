using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="ListShiftMutationOperator{TMutation, TConfiguration}"/>.
    /// </summary>
    public class ListShiftMutationOperatorConfiguration<TConfiguration, TMutation> : MutationOperatorConfigurationBase<TConfiguration, TMutation>
        where TConfiguration : ListShiftMutationOperatorConfiguration<TConfiguration, TMutation>
        where TMutation : ListShiftMutationOperator<TMutation, TConfiguration>
    {
    }
}
