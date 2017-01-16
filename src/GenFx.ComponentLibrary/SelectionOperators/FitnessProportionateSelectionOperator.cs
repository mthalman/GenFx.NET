using System;
using System.Collections.Generic;
using System.Linq;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Provides a selection technique whereby a <see cref="GeneticEntity"/> object's probability of being
    /// selected is directly proportional to its fitness value compared to the rest of the <see cref="Population"/>
    /// to which it belongs.
    /// </summary>
    public class FitnessProportionateSelectionOperator : SelectionOperator
    {
        /// <summary>
        /// Selects a <see cref="GeneticEntity"/> from <paramref name="population"/> using the <see cref="RouletteWheelSampler"/>
        /// based on the fitness values of the <see cref="GeneticEntity"/> objects.
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

            FitnessEvaluationMode evaluationMode = this.Algorithm.FitnessEvaluator.EvaluationMode;

            List<TemporaryWheelSlice> tempSlices = new List<TemporaryWheelSlice>();

            // If smaller fitness values are better, we need to inverse the wheel slice size distribution so that
            // the entity with the smallest fitness value gets the largest wheel slice size.  We do this by
            // using the largest fitness value as the wheel slice size for the entity with the smallest fitness
            // value.
            if (evaluationMode == FitnessEvaluationMode.Minimize)
            {
                GeneticEntity[] entities = population.Entities.GetEntitiesSortedByFitness(this.SelectionBasedOnFitnessType, evaluationMode).ToArray();

                int descendingIndex = entities.Length - 1;
                for (int i = 0; i < entities.Length; i++)
                {
                    tempSlices.Add(
                        new TemporaryWheelSlice {
                            Entity = entities[i],
                            Size = entities[descendingIndex].GetFitnessValue(this.SelectionBasedOnFitnessType)
                        });
                    descendingIndex--;
                }
            }
            else
            {
                // calculate percentage ranges
                foreach (GeneticEntity entity in population.Entities)
                {
                    tempSlices.Add(
                        new TemporaryWheelSlice {
                            Entity = entity,
                            Size = entity.GetFitnessValue(this.SelectionBasedOnFitnessType)
                        });
                }
            }

            double minSize = tempSlices.Min(slice => slice.Size);

            // Ensure that all wheel slice sizes are above 0 by shifting all values to be above 0.
            if (minSize <= 0)
            {
                double offset = Math.Abs(minSize);
                foreach (TemporaryWheelSlice slice in tempSlices)
                {
                    slice.Size += offset + 1;
                }
            }

            List<WheelSlice> wheelSlices = new List<WheelSlice>(tempSlices.Select(slice => new WheelSlice(slice.Entity, slice.Size)));

            return RouletteWheelSampler.GetEntity(wheelSlices);
        }

        private class TemporaryWheelSlice
        {
            public GeneticEntity Entity { get; set; }
            public double Size { get; set; }
        }
    }
}
