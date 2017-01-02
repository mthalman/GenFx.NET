using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Represents the configuration of <see cref="RankSelectionOperator{TSelection, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TSelection">Type of the associated selection operator class.</typeparam>
    public abstract class RankSelectionOperatorConfiguration<TConfiguration, TSelection> : SelectionOperatorConfigurationBase<TConfiguration, TSelection>
        where TConfiguration : RankSelectionOperatorConfiguration<TConfiguration, TSelection>
        where TSelection : RankSelectionOperator<TSelection, TConfiguration>
    {
    }
}
