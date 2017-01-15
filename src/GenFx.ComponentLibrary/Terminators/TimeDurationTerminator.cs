using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Terminators
{
    /// <summary>
    /// Represents a genetic algorithm terminator that stops the algorithm once the specified <see cref="TimeLimit"/>
    /// has been reached.
    /// </summary> 
    public class TimeDurationTerminator : TerminatorBase
    {
        private DateTime timeStarted;
        private TimeSpan timeLimit;

        /// <summary>
        /// Gets or sets the <see cref="TimeSpan"/> which the algorithm will be allowed to run for.
        /// </summary>
        [ConfigurationProperty]
        public TimeSpan TimeLimit
        {
            get { return this.timeLimit; }
            set { this.SetProperty(ref this.timeLimit, value); }
        }

        /// <summary>
        /// Initializes the component to ensure its readiness for algorithm execution.
        /// </summary>
        /// <param name="algorithm">The algorithm that is to use this component.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public override void Initialize(IGeneticAlgorithm algorithm)
        {
            base.Initialize(algorithm);

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
            return (DateTime.Now >= this.timeStarted + this.TimeLimit);
        }
    }
}
