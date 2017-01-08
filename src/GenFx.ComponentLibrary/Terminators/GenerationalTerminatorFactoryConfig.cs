using GenFx.ComponentLibrary.Base;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Terminators
{
    /// <summary>
    /// Represents the configuration of <see cref="GenerationalTerminator"/>.
    /// </summary>
    public sealed class GenerationalTerminatorFactoryConfig : TerminatorFactoryConfigBase<GenerationalTerminatorFactoryConfig, GenerationalTerminator>
    {
        private const int DefaultFinalGeneration = 100;

        private int finalGeneration = GenerationalTerminatorFactoryConfig.DefaultFinalGeneration;

        /// <summary>
        /// Gets or sets the target generation that, when reached, will stop the algorithm.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [IntegerValidator(MinValue = 1)]
        public int FinalGeneration
        {
            get { return this.finalGeneration; }
            set { this.SetProperty(ref this.finalGeneration, value); }
        }
    }
}
