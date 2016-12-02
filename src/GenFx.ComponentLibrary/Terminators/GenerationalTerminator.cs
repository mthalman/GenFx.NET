using System;
using System.ComponentModel;
using GenFx.ComponentModel;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Terminators
{
    /// <summary>
    /// Represents a genetic algorithm terminator that stops the algorithm once a target generation
    /// has been reached.
    /// </summary>
    public class GenerationalTerminator : Terminator
    {
        /// <summary>
        /// Gets the target generation that, when reached, will stop the algorithm.
        /// </summary>
        public int FinalGeneration
        {
            get { return ((GenerationalTerminatorConfiguration)this.Algorithm.ConfigurationSet.Terminator).FinalGeneration; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationalTerminator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="GenerationalTerminator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public GenerationalTerminator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
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

    /// <summary>
    /// Represents the configuration of <see cref="GenerationalTerminator"/>.
    /// </summary>
    [Component(typeof(GenerationalTerminator))]
    public class GenerationalTerminatorConfiguration : TerminatorConfiguration
    {
        private const int DefaultFinalGeneration = 100;

        private int finalGeneration = GenerationalTerminatorConfiguration.DefaultFinalGeneration;

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
