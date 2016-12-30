using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Represents the configuration of <see cref="UniformSelectionOperator{TSelection, TConfiguration}"/>.
    /// </summary>
    public abstract class UniformSelectionOperatorConfiguration<TConfiguration, TSelection> : SelectionOperatorConfigurationBase<TConfiguration, TSelection>
        where TConfiguration : UniformSelectionOperatorConfiguration<TConfiguration, TSelection>
        where TSelection : UniformSelectionOperator<TSelection, TConfiguration>
    {
    }
}
