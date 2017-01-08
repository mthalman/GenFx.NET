using GenFx.Contracts;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="FitnessEvaluatorBase{TFitness, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TFitness">Type of the associated fitness evaluator class.</typeparam>
    public abstract class FitnessEvaluatorFactoryConfigBase<TConfiguration, TFitness> : ConfigurationForComponentWithAlgorithm<TConfiguration, TFitness>, IFitnessEvaluatorFactoryConfig
        where TConfiguration : FitnessEvaluatorFactoryConfigBase<TConfiguration, TFitness>
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
