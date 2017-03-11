namespace GenFx.UI
{
    /// <summary>
    /// Represents a factory class which creates <see cref="IStopwatch"/> objects.
    /// </summary>
    public interface IStopwatchFactory
    {
        /// <summary>
        /// Creates an <see cref="IStopwatch"/> object.
        /// </summary>
        /// <returns>A new <see cref="IStopwatch"/>.</returns>
        IStopwatch Create();
    }
}
