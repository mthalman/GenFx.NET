using GenFx.ComponentLibrary.Base;
using System;

namespace GenFx.ComponentLibrary.Terminators
{
    /// <summary>
    /// Represents the configuration of <see cref="TimeDurationTerminator"/>.
    /// </summary>
    public sealed class TimeDurationTerminatorConfiguration : TerminatorConfigurationBase<TimeDurationTerminatorConfiguration, TimeDurationTerminator>
    {
        private TimeSpan timeLimit;

        /// <summary>
        /// Gets or sets the <see cref="TimeSpan"/> which the algorithm will be allowed to run for.
        /// </summary>
        public TimeSpan TimeLimit
        {
            get { return this.timeLimit; }
            set { this.SetProperty(ref this.timeLimit, value); }
        }
    }
}
