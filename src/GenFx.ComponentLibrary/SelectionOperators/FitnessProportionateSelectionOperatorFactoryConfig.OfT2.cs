using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Represents the configuration of <see cref="FitnessProportionateSelectionOperator{TSelection, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TSelection">Type of the associated selection operator class.</typeparam>
    public abstract class FitnessProportionateSelectionOperatorFactoryConfig<TConfiguration, TSelection> : SelectionOperatorFactoryConfigBase<TConfiguration, TSelection>
        where TConfiguration : FitnessProportionateSelectionOperatorFactoryConfig<TConfiguration, TSelection> 
        where TSelection : FitnessProportionateSelectionOperator<TSelection, TConfiguration>
    {
    }
}
