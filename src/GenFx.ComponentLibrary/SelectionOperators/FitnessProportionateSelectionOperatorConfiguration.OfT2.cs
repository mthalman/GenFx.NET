using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Represents the configuration of <see cref="FitnessProportionateSelectionOperator{TSelection, TConfiguration}"/>.
    /// </summary>
    public abstract class FitnessProportionateSelectionOperatorConfiguration<TConfiguration, TSelection> : SelectionOperatorConfigurationBase<TConfiguration, TSelection>
        where TConfiguration : FitnessProportionateSelectionOperatorConfiguration<TConfiguration, TSelection> 
        where TSelection : FitnessProportionateSelectionOperator<TSelection, TConfiguration>
    {
    }
}
