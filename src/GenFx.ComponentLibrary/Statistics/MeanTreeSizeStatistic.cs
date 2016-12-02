using System;
using System.ComponentModel;
using GenFx.ComponentLibrary.Trees;
using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the mean of the tree size of <see cref="TreeEntity"/> 
    /// objects contained by a <see cref="Population"/>.
    /// </summary>
    [RequiredEntity(typeof(TreeEntity))]
    public class MeanTreeSizeStatistic : Statistic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MeanTreeSizeStatistic"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="MeanTreeSizeStatistic"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public MeanTreeSizeStatistic(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Calculates the mean of the tree size of <see cref="TreeEntity"/> objects contained by the
        /// <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive the statistic.</param>
        /// <returns>Mean of the tree size of the <see cref="TreeEntity"/> objects.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            int sum = 0;
            for (int i = 0; i < population.Entities.Count; i++)
            {
                TreeEntity tree = (TreeEntity)population.Entities[i];
                sum += tree.GetSize();
            }

            return (double)sum / population.Entities.Count;
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="MeanTreeSizeStatistic"/>.
    /// </summary>
    [Component(typeof(MeanTreeSizeStatistic))]
    public class MeanTreeSizeStatisticConfiguration : StatisticConfiguration
    {
    }
}
