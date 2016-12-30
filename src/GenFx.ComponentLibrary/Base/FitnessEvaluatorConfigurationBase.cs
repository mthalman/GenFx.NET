using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="FitnessEvaluatorBase{TFitness, TConfiguration}"/>.
    /// </summary>
    public abstract class FitnessEvaluatorConfigurationBase<TConfiguration, TFitness> : ComponentConfiguration<TConfiguration, TFitness>, IFitnessEvaluatorConfiguration
        where TConfiguration : FitnessEvaluatorConfigurationBase<TConfiguration, TFitness>
        where TFitness : FitnessEvaluatorBase<TFitness, TConfiguration>
    {
        private const FitnessEvaluationMode DefaultEvaluationMode = FitnessEvaluationMode.Maximize;
        private FitnessEvaluationMode evaluationMode = DefaultEvaluationMode;

        /// <summary>
        /// Gets or sets the mode which specifies whether to treat higher or lower fitness values as being better.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [FitnessEvaluationModeValidator]
        public FitnessEvaluationMode EvaluationMode
        {
            get { return this.evaluationMode; }
            set { this.SetProperty(ref this.evaluationMode, value); }
        }
    }
}
