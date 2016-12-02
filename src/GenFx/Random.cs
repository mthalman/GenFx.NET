using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx
{
    /// <summary>
    /// Helper class to produce random numbers.
    /// </summary>
    public class RandomHelper : IRandomHelper
    {
        private static IRandomHelper instance = new RandomHelper();

        private Random randomizer = new Random();

        /// <summary>
        /// Gets or sets the <see cref="IRandomHelper"/> object used to produce random numbers.
        /// </summary>
        /// <remarks>
        /// This field can be set for testing purposes to a custom class that implements the
        /// <see cref="IRandomHelper"/> interface.  This allows tests to run with known "random" values.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public static IRandomHelper Instance
        {
            get { return instance; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                instance = value;
            }
        }

        /// <summary>
        /// Returns a nonnegative random integer less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">Maximum value that can be returned.</param>
        /// <returns>Nonnegative random integer less than the specified maximum.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxValue"/> is less than zero.</exception>
        public int GetRandomValue(int maxValue)
        {
            return this.randomizer.Next(maxValue);
        }

        /// <summary>
        /// Returns a nonnegative random integer less than the specified maximum.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned.</param>
        /// <returns>Nonnegative random integer less than the specified maximum.</returns>
        /// <remarks><paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>.</remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minValue"/> is greater than <paramref name="maxValue"/>.</exception>
        public int GetRandomValue(int minValue, int maxValue)
        {
            return this.randomizer.Next(minValue, maxValue);
        }

        /// <summary>
        /// Returns a number between 0 and 1.
        /// </summary>
        /// <returns>A number between 0 and 1.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public double GetRandomRatio()
        {
            return this.randomizer.NextDouble();
        }
    }

    /// <summary>
    /// Interface that defines the methods for a helper class that produces random numbers.
    /// </summary>
    /// <remarks>
    /// This interface can be implemented for testing purposes and set on <see cref="RandomHelper.Instance"/>
    /// to produce known "random" values for tests.
    /// </remarks>
    public interface IRandomHelper
    {
        /// <summary>
        /// Returns a nonnegative random integer less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">Maximum value that can be returned.</param>
        /// <returns>Nonnegative random integer less than the specified maximum.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxValue"/> is less than zero.</exception>
        int GetRandomValue(int maxValue);

        /// <summary>
        /// Returns a nonnegative random integer less than the specified maximum.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned.</param>
        /// <returns>Nonnegative random integer less than the specified maximum.</returns>
        /// <remarks><paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>.</remarks>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minValue"/> is greater than <paramref name="maxValue"/>.</exception>
        int GetRandomValue(int minValue, int maxValue);

        /// <summary>
        /// Returns a number between 0 and 1.
        /// </summary>
        /// <returns>A number between 0 and 1.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        double GetRandomRatio();
    }
}
