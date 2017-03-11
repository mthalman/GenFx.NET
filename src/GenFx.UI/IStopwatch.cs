using System;

namespace GenFx.UI
{
    /// <summary>
    /// Provides a set of methods and properties that can be used to accurately measure elapsed time.
    /// </summary>
    public interface IStopwatch
    {
        /// <summary>
        /// Gets the total elapsed time measured by the current instance.
        /// </summary>
        TimeSpan Elapsed { get; }

        /// <summary>
        /// Starts, or resumes, measuring elapsed time for an interval.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops time interval measurement, resets the elapsed time to zero, and starts measuring elapsed time.
        /// </summary>
        void Restart();
    }
}
