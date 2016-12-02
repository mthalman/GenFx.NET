using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GenFx.ComponentLibrary.SelectionOperators
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
    public class RankSelectionOperator : SelectionOperator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RankSelectionOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="RankSelectionOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public RankSelectionOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Selects a <see cref="GeneticEntity"/> from <paramref name="population"/> according to its
        /// fitness rank within the <paramref name="population"/>.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/>
        /// objects from which to select.</param>
        /// <returns>The <see cref="GeneticEntity"/> object that was selected.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override GeneticEntity SelectEntityFromPopulation(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            GeneticEntity[] sortedEntities = population.Entities.GetEntitiesSortedByFitness(
                this.SelectionBasedOnFitnessType,
                this.Algorithm.Operators.FitnessEvaluator.EvaluationMode).ToArray();

            List<WheelSlice> wheelSlices = new List<WheelSlice>(sortedEntities.Length);
            for (int i = 0; i < sortedEntities.Length; i++)
            {
                wheelSlices.Add(new WheelSlice(sortedEntities[i], i + 1));
            }

            return RouletteWheelSampler.GetEntity(wheelSlices);
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="RankSelectionOperator"/>.
    /// </summary>
    [Component(typeof(RankSelectionOperator))]
    public class RankSelectionOperatorConfiguration : SelectionOperatorConfiguration
    {
    }
}
