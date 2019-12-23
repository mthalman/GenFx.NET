using GenFx.Components.Trees;
using GenFx.Validation;
using System;
using System.Runtime.Serialization;

namespace GenFx.Components.Metrics
{
    /// <summary>
    /// Provides the calculation to determine the mean of the tree size of <see cref="TreeEntityBase"/> 
    /// objects contained by a <see cref="Population"/>.
    /// </summary>
    [DataContract]
    [RequiredGeneticEntity(typeof(TreeEntityBase))]
    public class MeanTreeSize : Metric
    {
        /// <summary>
        /// Calculates the mean of the tree size of <see cref="TreeEntityBase"/> objects contained by the
        /// <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> from which to derive the metric.</param>
        /// <returns>Mean of the tree size of the <see cref="TreeEntityBase"/> objects.</returns>
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
                TreeEntityBase tree = (TreeEntityBase)population.Entities[i];
                sum += tree.GetSize();
            }

            return (double)sum / population.Entities.Count;
        }
    }
}
