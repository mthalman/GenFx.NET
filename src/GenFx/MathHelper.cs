using System;
using System.Collections.Generic;
using System.Linq;

namespace GenFx
{
    /// <summary>
    /// Contains helper methods for mathematical functions.
    /// </summary>
    internal static class MathHelper
    {
        /// <summary>
        /// Returns the standard deviation of the raw fitness value for the <paramref name="entities"/>.
        /// </summary>
        /// <param name="entities">The entities whose fitness values should be used.</param>
        /// <param name="mean">Mean fitness value for the population.</param>
        /// <param name="fitnessType">The type of fitness value to use.</param>
        /// <returns>The standard deviation of the raw fitness value for the population.</returns>
        public static double GetStandardDeviation(IEnumerable<GeneticEntity> entities, double mean, FitnessType fitnessType)
        {
            int count = 0;
            double diffSums = entities.Sum(e =>
            {
                count++;
                double val;
                if (fitnessType == FitnessType.Scaled)
                {
                    val = e.ScaledFitnessValue - mean;
                }
                else
                {
                    val = e.RawFitnessValue - mean;
                }

                return Math.Pow(val, 2);
            });

            return Math.Sqrt(diffSums / count);
        }
    }
}
