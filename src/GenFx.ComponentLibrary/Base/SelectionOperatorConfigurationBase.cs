using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="SelectionOperatorBase{TSelection, TConfiguration}"/>.
    /// </summary>
    public abstract class SelectionOperatorConfigurationBase<TConfiguration, TSelection> : ComponentConfiguration<TConfiguration, TSelection>, ISelectionOperatorConfiguration
        where TSelection : SelectionOperatorBase<TSelection, TConfiguration>
        where TConfiguration : SelectionOperatorConfigurationBase<TConfiguration, TSelection>
    {
        private const FitnessType DefaultSelectionBasedOnFitnessType = FitnessType.Scaled;

        private FitnessType selectionBasedOnFitnessType = DefaultSelectionBasedOnFitnessType;

        /// <summary>
        /// Gets or sets the <see cref="FitnessType"/> to base selection of <see cref="IGeneticEntity"/> objects on.
        /// </summary>
        /// <exception cref="ValidationException">Value is undefined.</exception>
        [FitnessTypeValidator]
        public FitnessType SelectionBasedOnFitnessType
        {
            get { return this.selectionBasedOnFitnessType; }
            set { this.SetProperty(ref this.selectionBasedOnFitnessType, value); }
        }
    }
}
