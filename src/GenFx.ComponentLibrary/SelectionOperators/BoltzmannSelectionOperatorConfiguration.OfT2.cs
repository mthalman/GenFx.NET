using GenFx.ComponentLibrary.Base;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Represents the configuration of <see cref="BoltzmannSelectionOperator{TSelection, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TSelection">Type of the associated selection operator class.</typeparam>
    public abstract class BoltzmannSelectionOperatorConfiguration<TConfiguration, TSelection> : SelectionOperatorConfigurationBase<TConfiguration, TSelection>
        where TConfiguration : BoltzmannSelectionOperatorConfiguration<TConfiguration, TSelection> 
        where TSelection : BoltzmannSelectionOperator<TSelection, TConfiguration>
    {
        private double initialTemperature;

        /// <summary>
        /// Gets or sets the initial temperature of the selection operator.
        /// </summary>
        public double InitialTemperature
        {
            get { return this.initialTemperature; }
            set { this.SetProperty(ref this.initialTemperature, value); }
        }
    }
}
