using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Terminators
{
    /// <summary>
    /// Represents a genetic algorithm terminator that stops the algorithm once a target generation
    /// has been reached.
    /// </summary>
    public sealed class GenerationalTerminator : TerminatorBase<GenerationalTerminator, GenerationalTerminatorFactoryConfig>
    {
        /// <summary>
        /// Gets the target generation that, when reached, will stop the algorithm.
        /// </summary>
        public int FinalGeneration
        {
            get { return ((GenerationalTerminatorFactoryConfig)this.Algorithm.ConfigurationSet.Terminator).FinalGeneration; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerationalTerminator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this <see cref="GenerationalTerminator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public GenerationalTerminator(IGeneticAlgorithm algorithm)
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
}
