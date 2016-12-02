using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using GenFx.ComponentLibrary.Properties;
using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Terminators
{
    /// <summary>
    /// Represents a genetic algorithm terminator that stops the algorithm once the specified <see cref="TimeDurationTerminator.TimeLimit"/>
    /// has been reached.
    /// </summary> 
    public class TimeDurationTerminator : Terminator
    {
        private DateTime timeStarted;

        /// <summary>
        /// Gets the <see cref="TimeSpan"/> which the algorithm will be allowed to run for.
        /// </summary>
        public TimeSpan TimeLimit
        {
            get { return ((TimeDurationTerminatorConfiguration)this.Algorithm.ConfigurationSet.Terminator).TimeLimit; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeDurationTerminator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="TimeDurationTerminator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public TimeDurationTerminator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
            algorithm.AlgorithmStarting += new EventHandler(this.AlgorithmStarting);
        }

        /// <summary>
        /// Subscription to the <see cref="GeneticAlgorithm.AlgorithmStarting"/> event.
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
            if (DateTime.Now >= this.timeStarted + this.TimeLimit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="TimeDurationTerminator"/>.
    /// </summary>
    [Component(typeof(TimeDurationTerminator))]
    public class TimeDurationTerminatorConfiguration : TerminatorConfiguration
    {
        internal const string TimeLimitProperty = "TimeLimit";

        private TimeSpan timeLimit;

        /// <summary>
        /// Gets or sets the <see cref="TimeSpan"/> which the algorithm will be allowed to run for.
        /// </summary>
        public TimeSpan TimeLimit
        {
            get { return this.timeLimit; }
            set
            {
                this.timeLimit = value;
                this.OnPropertyChanged(TimeDurationTerminatorConfiguration.TimeLimitProperty);
            }
        }
    }
}
