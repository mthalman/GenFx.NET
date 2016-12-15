using System;
using GenFx.ComponentModel;

namespace GenFx
{
    /// <summary>
    /// Represents a component which defines when a genetic algorithm should stop executing.
    /// </summary>
    public interface ITerminator : IGeneticComponent
    {
        /// <summary>
        /// Returns whether the genetic algorithm should stop executing.
        /// </summary>
        /// <returns>True if the genetic algorithm is to stop executing; otherwise, false.</returns>
        bool IsComplete();
    }

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
    public abstract class Terminator<TTerminator, TConfiguration> : GeneticComponentWithAlgorithm<TTerminator, TConfiguration>, ITerminator
        where TTerminator : Terminator<TTerminator, TConfiguration>
        where TConfiguration : TerminatorConfiguration<TConfiguration, TTerminator>
    { 
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected Terminator(IGeneticAlgorithm algorithm)
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
    /// Represents the configuration of <see cref="ITerminator"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces")]
    public interface ITerminatorConfiguration : IComponentConfiguration
    {
    }

    /// <summary>
    /// Represents the configuration of <see cref="Terminator{TTerminator, TConfiguration}"/>.
    /// </summary>
    public abstract class TerminatorConfiguration<TConfiguration, TTerminator> : ComponentConfiguration<TConfiguration, TTerminator>, ITerminatorConfiguration
        where TConfiguration : TerminatorConfiguration<TConfiguration, TTerminator>
        where TTerminator : Terminator<TTerminator, TConfiguration>
    {
    }

    /// <summary>
    /// Represents a <see cref="ITerminator"/> that never completes.
    /// </summary>
    internal sealed class EmptyTerminator : Terminator<EmptyTerminator, EmptyTerminatorConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptyTerminator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this <see cref="EmptyTerminator"/>.</param>
        public EmptyTerminator(IGeneticAlgorithm algorithm)
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
    internal class EmptyTerminatorConfiguration : TerminatorConfiguration<EmptyTerminatorConfiguration, EmptyTerminator>
    {
    }
}
