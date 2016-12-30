using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Represents the configuration of <see cref="RankSelectionOperator{TSelection, TConfiguration}"/>.
    /// </summary>
    public abstract class RankSelectionOperatorConfiguration<TConfiguration, TSelection> : SelectionOperatorConfigurationBase<TConfiguration, TSelection>
        where TConfiguration : RankSelectionOperatorConfiguration<TConfiguration, TSelection>
        where TSelection : RankSelectionOperator<TSelection, TConfiguration>
    {
    }
}
