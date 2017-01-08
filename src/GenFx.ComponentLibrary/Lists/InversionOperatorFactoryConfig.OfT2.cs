using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Represents the configuration of <see cref="InversionOperator{TInversion, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TInversion">Type of the associated mutation operator class.</typeparam>
    public abstract class InversionOperatorFactoryConfig<TConfiguration, TInversion> : MutationOperatorFactoryConfigBase<TConfiguration, TInversion>
        where TConfiguration : InversionOperatorFactoryConfig<TConfiguration, TInversion>
        where TInversion : InversionOperator<TInversion, TConfiguration>
    {
    }
}
