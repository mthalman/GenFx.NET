using GenFx.ComponentLibrary.Base;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Terminators
{
    /// <summary>
    /// Represents a genetic algorithm terminator that stops the algorithm once a target generation
    /// has been reached.
    /// </summary>
    public class GenerationalTerminator : TerminatorBase
    {
        private const int DefaultFinalGeneration = 100;

        private int finalGeneration = DefaultFinalGeneration;

        /// <summary>
        /// Gets or sets the target generation that, when reached, will stop the algorithm.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [ConfigurationProperty]
        [IntegerValidator(MinValue = 1)]
        public int FinalGeneration
        {
            get { return this.finalGeneration; }
            set { this.SetProperty(ref this.finalGeneration, value); }
        }
        
        /// <summary>
        /// Calculates whether the target generation has been reached.
        /// </summary>
        /// <returns>true if the genetic algorithm is to stop executing; otherwise, false.</returns>
        public override bool IsComplete()
        {
            return (this.Algorithm.CurrentGeneration == this.FinalGeneration);
        }
    }
}
