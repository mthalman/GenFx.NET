using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GenFx.Components.SelectionOperators
{
    /// <summary>
    /// Provides a selection technique whereby a <see cref="GeneticEntity"/> object's probability of being
    /// selected is directly proportional to its fitness value compared to the rest of the <see cref="Population"/>
    /// to which it belongs.
    /// </summary>
    [DataContract]
    public class FitnessProportionateSelectionOperator : SelectionOperator
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

            this.AssertIsInitialized();

            FitnessEvaluationMode evaluationMode = this.Algorithm!.FitnessEvaluator!.EvaluationMode;

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
                        new TemporaryWheelSlice(
                            entities[i],
                            entities[descendingIndex].GetFitnessValue(this.SelectionBasedOnFitnessType)
                        ));
                    descendingIndex--;
                }
            }
            else
            {
                // calculate percentage ranges
                foreach (GeneticEntity entity in population.Entities)
                {
                    tempSlices.Add(
                        new TemporaryWheelSlice(
                            entity,
                            entity.GetFitnessValue(this.SelectionBasedOnFitnessType)
                        ));
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

            List<GeneticEntity> result = new List<GeneticEntity>();
            for (int i = 0; i < entityCount; i++)
            {
                result.Add(RouletteWheelSampler.GetEntity(wheelSlices));
            }

            return result;
        }

        private class TemporaryWheelSlice
        {
            public TemporaryWheelSlice(GeneticEntity entity, double size)
            {
                this.Entity = entity;
                this.Size = size;
            }

            public GeneticEntity Entity { get; }
            public double Size { get; set; }
        }
    }
}
