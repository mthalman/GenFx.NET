using System;
using GenFx.ComponentModel;

namespace GenFx
{
    /// <summary>
    /// Provides the abstract base class for a genetic algorithm terminator.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <b>Terminator</b> class defines when a genetic algorithm should stop executing.
    /// </para>
    /// <para>
    /// <b>Notes to implementers:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by using the <see cref="ComponentConfigurationSet.Terminator"/> property
    /// </para>
    /// </remarks>
    public abstract class Terminator : GeneticComponentWithAlgorithm
    { 
        /// <summary>
        /// Gets the <see cref="ComponentConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        public override sealed ComponentConfiguration Configuration
        {
            get { return this.Algorithm.ConfigurationSet.Terminator; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Terminator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="Terminator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected Terminator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// When overriden in a derived class, returns whether the genetic algorithm should stop
        /// executing.
        /// </summary>
        /// <returns>True if the genetic algorithm is to stop executing; otherwise, false.</returns>
        public abstract bool IsComplete();
    }

    /// <summary>
    /// Represents the configuration of <see cref="Terminator"/>.
    /// </summary>
    [Component(typeof(Terminator))]
    public abstract class TerminatorConfiguration : ComponentConfiguration
    {
    }

    /// <summary>
    /// Represents a <see cref="Terminator"/> that never completes.
    /// </summary>
    internal class EmptyTerminator : Terminator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyTerminator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="EmptyTerminator"/>.</param>
        public EmptyTerminator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Returns whether the genetic algorithm should stop executing.
        /// </summary>
        /// <returns>Always returns false.</returns>
        public override bool IsComplete()
        {
            return false;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="EmptyTerminator"/>.
    /// </summary>
    [Component(typeof(EmptyTerminator))]
    internal class EmptyTerminatorConfiguration : TerminatorConfiguration
    {
    }
}
