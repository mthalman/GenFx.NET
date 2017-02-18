using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Provides a way to select a <see cref="GeneticEntity"/> based on a range of values associated with
    /// it compared to other <see cref="GeneticEntity"/> objects.
    /// </summary>
    /// <remarks>
    /// Each <see cref="GeneticEntity"/> is assigned a particular range of values within a "roulette wheel".
    /// The higher range of values a <see cref="GeneticEntity"/> has, the greater the probability of it being
    /// selected.
    /// </remarks>
    public static class RouletteWheelSampler
    {
        /// <summary>
        /// Returns a <see cref="GeneticEntity"/> that was randomly selected from the <see cref="GeneticEntity"/>
        /// objects in <paramref name="wheelSlices"/> according to the range of values associated with it.
        /// </summary>
        /// <param name="wheelSlices"><see cref="WheelSlice"/> objects defining the
        /// structure of the "roulette wheel".</param>
        /// <returns>
        /// <see cref="GeneticEntity"/> that was randomly selected from the <see cref="GeneticEntity"/>
        /// objects in <paramref name="wheelSlices"/> according to the range of values associated with it.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="wheelSlices"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="wheelSlices"/> is empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="wheelSlices"/> contains a null item.</exception>
        public static GeneticEntity GetEntity(IList<WheelSlice> wheelSlices)
        {
            if (wheelSlices == null)
            {
                throw new ArgumentNullException(nameof(wheelSlices));
            }

            if (wheelSlices.Count == 0)
            {
                throw new ArgumentException(
                  StringUtil.GetFormattedString(Resources.ErrorMsg_EmptyList), nameof(wheelSlices));
            }

            double totalSize = 0;
            foreach (WheelSlice wheelSlice in wheelSlices)
            {
                if (wheelSlice == null)
                {
                    throw new ArgumentException(Resources.ErrorMsg_NullItemInList, nameof(wheelSlices));
                }

                totalSize += wheelSlice.Size;
            }

            if (totalSize == 0)
            {
                // none of the wheel slices have a size.  default to selecting a random entity
                return wheelSlices[RandomNumberService.Instance.GetRandomValue(wheelSlices.Count)].Entity;
            }

            List<EntityPercentageRange> percentageRanges = new List<EntityPercentageRange>();
            double lastMaxValue = 0;
            for (int i = 0; i < wheelSlices.Count; i++)
            {
                double percent = (wheelSlices[i].Size / totalSize) * 100;
                percentageRanges.Add(new EntityPercentageRange(wheelSlices[i].Entity, lastMaxValue, lastMaxValue + percent));
                lastMaxValue += percent;
            }

            return SpinWheel(percentageRanges);
        }

        /// <summary>
        /// Spins the roulette wheel to return a <see cref="GeneticEntity"/>.
        /// </summary>
        private static GeneticEntity SpinWheel(List<EntityPercentageRange> percentageRanges)
        {
            double percentTarget = RandomNumberService.Instance.GetDouble() * 100;

            GeneticEntity entity = null;

            foreach (EntityPercentageRange range in percentageRanges)
            {
                if ((percentTarget >= range.MinValue && percentTarget < range.MaxValue) || 100 == percentTarget && percentTarget == range.MaxValue)
                {
                    entity = range.Entity;
                    break;
                }
            }
            
            return entity;
        }

        /// <summary>
        /// Represents a percentage range corresponding to a segment of the roulette wheel.
        /// </summary>
        private class EntityPercentageRange
        {
            private double minValue;
            private double maxValue;
            private GeneticEntity entity;

            /// <summary>
            /// Gets the lower-bound percentage value in the range.
            /// </summary>
            public double MinValue
            {
                get { return this.minValue; }
            }

            /// <summary>
            /// Gets the upper-bound percentage value in the range.
            /// </summary>
            public double MaxValue
            {
                get { return this.maxValue; }
            }

            /// <summary>
            /// Gets the <see cref="GeneticEntity"/> assigned to the range.
            /// </summary>
            public GeneticEntity Entity
            {
                get { return this.entity; }
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="EntityPercentageRange"/> class.
            /// </summary>
            /// <param name="entity"><see cref="GeneticEntity"/> assigned to the range.</param>
            /// <param name="minValue">Lower-bound percentage value in the range.</param>
            /// <param name="maxValue">Upper-bound percentage value in the range.</param>
            public EntityPercentageRange(GeneticEntity entity, double minValue, double maxValue)
            {
                this.entity = entity;
                this.minValue = minValue;
                this.maxValue = maxValue;
            }
        }
    }

    /// <summary>
    /// Represents a slice of a "roulette wheel" used to indicate how much probability a <see cref="GeneticEntity"/>
    /// has of being selected using the <see cref="RouletteWheelSampler"/>.
    /// </summary>
    /// <seealso cref="RouletteWheelSampler"/>
    public class WheelSlice
    {
        private double size;
        private GeneticEntity entity;

        /// <summary>
        /// Gets the <see cref="GeneticEntity"/> belonging to the wheel slice.
        /// </summary>
        /// <value>The <see cref="GeneticEntity"/> belonging to the wheel slice.</value>
        public GeneticEntity Entity
        {
            get { return this.entity; }
        }

        /// <summary>
        /// Gets the value indicating how large the slice is on the wheel.
        /// </summary>
        /// <value>The value indicating how large the slice is on the wheel.</value>
        public double Size
        {
            get { return this.size; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WheelSlice"/> class.
        /// </summary>
        /// <param name="entity">The <see cref="GeneticEntity"/> belonging to the wheel slice.</param>
        /// <param name="size">The value indicating how large the slice is on the wheel.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="size"/> is less than.</exception>
        public WheelSlice(GeneticEntity entity, double size)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (size < 0)
            {
                throw new ArgumentException(Resources.ErrorMsg_InvalidWheelSliceSize, nameof(size));
            }

            this.entity = entity;
            this.size = size;
        }
    }
}
