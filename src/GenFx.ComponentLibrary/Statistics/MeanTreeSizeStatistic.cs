using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Trees;
using GenFx.Contracts;
using GenFx.Validation;
using System;

namespace GenFx.ComponentLibrary.Statistics
{
    /// <summary>
    /// Provides the calculation to determine the mean of the tree size of <see cref="ITreeEntity"/> 
    /// objects contained by a <see cref="IPopulation"/>.
    /// </summary>
    [RequiredEntity(typeof(ITreeEntity))]
    public class MeanTreeSizeStatistic : StatisticBase
    {
        /// <summary>
        /// Calculates the mean of the tree size of <see cref="ITreeEntity"/> objects contained by the
        /// <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> from which to derive the statistic.</param>
        /// <returns>Mean of the tree size of the <see cref="ITreeEntity"/> objects.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        public override object GetResultValue(IPopulation population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            int sum = 0;
            for (int i = 0; i < population.Entities.Count; i++)
            {
                ITreeEntity tree = (ITreeEntity)population.Entities[i];
                sum += tree.GetSize();
            }

            return (double)sum / population.Entities.Count;
        }
    }
}
