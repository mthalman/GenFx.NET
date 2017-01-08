using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.ComponentLibrary.Terminators
{
    /// <summary>
    /// Represents a genetic algorithm terminator that stops the algorithm once the specified <see cref="TimeDurationTerminatorFactoryConfig.TimeLimit"/>
    /// has been reached.
    /// </summary> 
    public sealed class TimeDurationTerminator : TerminatorBase<TimeDurationTerminator, TimeDurationTerminatorFactoryConfig>
    {
        private DateTime timeStarted;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeDurationTerminator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this <see cref="TimeDurationTerminator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public TimeDurationTerminator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
            algorithm.AlgorithmStarting += new EventHandler(this.AlgorithmStarting);
        }

        /// <summary>
        /// Subscription to the <see cref="IGeneticAlgorithm.AlgorithmStarting"/> event.
        /// Initializes the starting time.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void AlgorithmStarting(object sender, EventArgs e)
        {
            this.timeStarted = DateTime.Now;
        }

        /// <summary>
        /// Calculates whether the algorithm's time limit has been reached.
        /// </summary>
        /// <returns>true if the genetic algorithm is to stop executing; otherwise, false.</returns>
        public override bool IsComplete()
        {
            return (DateTime.Now >= this.timeStarted + this.Configuration.TimeLimit);
        }
    }
}
