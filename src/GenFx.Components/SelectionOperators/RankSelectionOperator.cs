using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx.Components.SelectionOperators
{
    /// <summary>
    /// Provides a selection technique whereby the <see cref="GeneticEntity"/> objects in a <see cref="Population"/>
    /// are selected according to their fitness rank in comparison to the result of the <see cref="Population"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Selection is then based on this ranking rather than on absolute fitness.  This technique avoids 
    /// selecting only a few of highly fit <see cref="GeneticEntity"/> objects and thus can prevent premature 
    /// convergence.  But it also loses the perhaps important distinguishment of absolute fitness values 
    /// later in a run.  Use of a <see cref="FitnessScalingStrategy"/> object does not have an impact 
    /// when <b>RankSelectionOperator</b> is being used since absolute differences in fitness are ignored.
    /// </para>
    /// </remarks>
    [DataContract]
    public class RankSelectionOperator : SelectionOperator
    {
        /// <summary>
        /// Selects the specified number of <see cref="GeneticEntity"/> objects from <paramref name="population"/>.
        /// </summary>
        /// <param name="entityCount">Number of <see cref="GeneticEntity"/> objects to select from the population.</param>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/> objects from which to select.
        /// objects from which to select.</param>
        /// <returns>The <see cref="GeneticEntity"/> object that was selected.</returns>
        protected override IEnumerable<GeneticEntity> SelectEntitiesFromPopulation(int entityCount, Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            GeneticEntity[] sortedEntities = population.Entities.GetEntitiesSortedByFitness(
                this.SelectionBasedOnFitnessType,
                this.Algorithm.FitnessEvaluator.EvaluationMode).ToArray();

            List<WheelSlice> wheelSlices = new List<WheelSlice>(sortedEntities.Length);
            for (int i = 0; i < sortedEntities.Length; i++)
            {
                wheelSlices.Add(new WheelSlice(sortedEntities[i], i + 1));
            }

            List<GeneticEntity> result = new List<GeneticEntity>();
            for (int i = 0; i < entityCount; i++)
            {
                result.Add(RouletteWheelSampler.GetEntity(wheelSlices));
            }

            return result;
        }
    }
}
