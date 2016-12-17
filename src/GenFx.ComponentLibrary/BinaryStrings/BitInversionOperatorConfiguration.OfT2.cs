using GenFx.ComponentLibrary.Lists;

namespace GenFx.ComponentLibrary.BinaryStrings
{
    /// <summary>
    /// Represents the configuration of <see cref="BitInversionOperator{TInversion, TConfiguration}"/>.
    /// </summary>
    public abstract class BitInversionOperatorConfiguration<TConfiguration, TInversion> : InversionOperatorConfiguration<TConfiguration, TInversion>
        where TConfiguration : InversionOperatorConfiguration<TConfiguration, TInversion>
        where TInversion : InversionOperator<TInversion, TConfiguration>
    {
    }
}
