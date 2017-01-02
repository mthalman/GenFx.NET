using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Represents the configuration of <see cref="UniformSelectionOperator{TSelection, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TSelection">Type of the associated selection operator class.</typeparam>
    public abstract class UniformSelectionOperatorConfiguration<TConfiguration, TSelection> : SelectionOperatorConfigurationBase<TConfiguration, TSelection>
        where TConfiguration : UniformSelectionOperatorConfiguration<TConfiguration, TSelection>
        where TSelection : UniformSelectionOperator<TSelection, TConfiguration>
    {
    }
}
