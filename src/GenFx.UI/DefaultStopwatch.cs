using System;
using System.Diagnostics;

namespace GenFx.UI
{
    /// <summary>
    /// Provides a set of methods and properties that can be used to accurately measure elapsed time.
    /// </summary>
    internal class DefaultStopwatch : IStopwatch
    {
        private Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// Gets the total elapsed time measured by the current instance.
        /// </summary>
        public TimeSpan Elapsed { get { return this.stopwatch.Elapsed; } }

        /// <summary>
        /// Starts, or resumes, measuring elapsed time for an interval.
        /// </summary>
        public void Start()
        {
            this.stopwatch.Start();
        }

        /// <summary>
        /// Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.
        /// </summary>
        public void Restart()
        {
            this.stopwatch.Restart();
        }
    }
}
