using System;
using System.Collections.Generic;
using System.Linq;

namespace GenFx
{
    /// <summary>
    /// Contains extension methods for sets of genetic entities.
    /// </summary>
    public static class GeneticEntityCollectionExtensions
    {
        /// <summary>
        /// Sorts the list in the order by the fitness type indicated, from worst to best.
        /// </summary>
        /// <param name="entities">The entities to sort.</param>
        /// <param name="sortBasis">Type of fitness value on which sorting is based.</param>
        /// <param name="evaluationMode">Mode which indicates whether the sorting will be based on higher or lower fitness values.</param>
        /// <exception cref="ArgumentException"><paramref name="sortBasis"/> value is undefined.</exception>
        /// <exception cref="ArgumentException"><paramref name="evaluationMode"/> value is undefined.</exception>
        public static IEnumerable<IGeneticEntity> GetEntitiesSortedByFitness(this IEnumerable<IGeneticEntity> entities,  FitnessType sortBasis, FitnessEvaluationMode evaluationMode)
        {
            if (!FitnessTypeHelper.IsDefined(sortBasis))
            {
                throw EnumHelper.CreateUndefinedEnumException(typeof(FitnessType), "sortBasis");
            }

            if (!FitnessEvaluationModeHelper.IsDefined(evaluationMode))
            {
                throw EnumHelper.CreateUndefinedEnumException(typeof(FitnessEvaluationMode), "evaluationMode");
            }

            FitnessValueComparer comparer = new FitnessValueComparer(sortBasis);
            Func<IGeneticEntity, IGeneticEntity> keySelector = entity => entity;

            if (evaluationMode == FitnessEvaluationMode.Maximize)
            {
                return entities.OrderBy(keySelector, comparer);
            }
            else
            {
                return entities.OrderByDescending(keySelector, comparer);
            }
        }

        /// <summary>
        /// Compares two <see cref="IGeneticEntity"/> objects based on their fitness values.
        /// </summary>
        private class FitnessValueComparer : IComparer<IGeneticEntity>
        {
            private FitnessType sortBasis;

            /// <summary>
            /// Initializes a new instance of the <see cref="FitnessValueComparer"/> class.
            /// </summary>
            /// <param name="sortBasis">Type of fitness value on which the comparison is based.</param>
            public FitnessValueComparer(FitnessType sortBasis)
            {
                this.sortBasis = sortBasis;
            }

            /// <summary>
            /// Compares the two <see cref="IGeneticEntity"/> objects based on their fitness values.
            /// </summary>
            /// <param name="x">The first <see cref="IGeneticEntity"/>.</param>
            /// <param name="y">The second <see cref="IGeneticEntity"/>.</param>
            /// <returns>
            /// Zero if both entities have the same fitness values.  Less than zero if the first entity
            /// has a fitness value less than the second entity.  Greater than zero if the first entity
            /// has a fitness value greater than the second entity.
            /// </returns>
            public int Compare(IGeneticEntity x, IGeneticEntity y)
            {
                if (x == null)
                {
                    throw new ArgumentNullException(nameof(x));
                }

                if (y == null)
                {
                    throw new ArgumentNullException(nameof(y));
                }

                return x.GetFitnessValue(this.sortBasis).CompareTo(y.GetFitnessValue(this.sortBasis));
            }
        }
    }
}
