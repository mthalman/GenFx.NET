using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="InversionOperator{TInversion, TConfiguration}"/>.
    /// </summary>
    public abstract class InversionOperatorConfiguration<TConfiguration, TInversion> : MutationOperatorConfigurationBase<TConfiguration, TInversion>
        where TConfiguration : InversionOperatorConfiguration<TConfiguration, TInversion>
        where TInversion : InversionOperator<TInversion, TConfiguration>
    {
    }
}
