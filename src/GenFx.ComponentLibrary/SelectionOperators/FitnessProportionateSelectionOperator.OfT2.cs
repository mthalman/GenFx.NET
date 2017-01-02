using GenFx.ComponentLibrary.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Provides a selection technique whereby a <see cref="IGeneticEntity"/> object's probability of being
    /// selected is directly proportional to its fitness value compared to the rest of the <see cref="IPopulation"/>
    /// to which it belongs.
    /// </summary>
    /// <typeparam name="TSelection">Type of the deriving selection operator class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    public abstract class FitnessProportionateSelectionOperator<TSelection, TConfiguration> : SelectionOperatorBase<TSelection, TConfiguration>
        where TSelection : FitnessProportionateSelectionOperator<TSelection, TConfiguration>
        where TConfiguration : FitnessProportionateSelectionOperatorConfiguration<TConfiguration, TSelection>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected FitnessProportionateSelectionOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Selects a <see cref="IGeneticEntity"/> from <paramref name="population"/> using the <see cref="RouletteWheelSampler"/>
        /// based on the fitness values of the <see cref="IGeneticEntity"/> objects.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/>
        /// objects from which to select.</param>
        /// <returns>The <see cref="IGeneticEntity"/> object that was selected.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override IGeneticEntity SelectEntityFromPopulation(IPopulation population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            FitnessEvaluationMode evaluationMode = this.Algorithm.ConfigurationSet.FitnessEvaluator.EvaluationMode;

            List<TemporaryWheelSlice> tempSlices = new List<TemporaryWheelSlice>();

            // If smaller fitness values are better, we need to inverse the wheel slice size distribution so that
            // the entity with the smallest fitness value gets the largest wheel slice size.  We do this by
            // using the largest fitness value as the wheel slice size for the entity with the smallest fitness
            // value.
            if (evaluationMode == FitnessEvaluationMode.Minimize)
            {
                IGeneticEntity[] entities = population.Entities.GetEntitiesSortedByFitness(this.Configuration.SelectionBasedOnFitnessType, evaluationMode).ToArray();

                int descendingIndex = entities.Length - 1;
                for (int i = 0; i < entities.Length; i++)
                {
                    tempSlices.Add(
                        new TemporaryWheelSlice {
                            Entity = entities[i],
                            Size = entities[descendingIndex].GetFitnessValue(this.Configuration.SelectionBasedOnFitnessType)
                        });
                    descendingIndex--;
                }
            }
            else
            {
                // calculate percentage ranges
                foreach (IGeneticEntity entity in population.Entities)
                {
                    tempSlices.Add(
                        new TemporaryWheelSlice {
                            Entity = entity,
                            Size = entity.GetFitnessValue(this.Configuration.SelectionBasedOnFitnessType)
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
            public IGeneticEntity Entity { get; set; }
            public double Size { get; set; }
        }
    }
}
