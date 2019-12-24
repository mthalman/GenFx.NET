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
        public static IEnumerable<GeneticEntity> GetEntitiesSortedByFitness(this IEnumerable<GeneticEntity> entities,  FitnessType sortBasis, FitnessEvaluationMode evaluationMode)
        {
            if (!Enum.IsDefined(typeof(FitnessType), sortBasis))
            {
                throw EnumHelper.CreateUndefinedEnumException(typeof(FitnessType), "sortBasis");
            }

            if (!Enum.IsDefined(typeof(FitnessEvaluationMode), evaluationMode))
            {
                throw EnumHelper.CreateUndefinedEnumException(typeof(FitnessEvaluationMode), "evaluationMode");
            }

            FitnessValueComparer comparer = new FitnessValueComparer(sortBasis);
            GeneticEntity keySelector(GeneticEntity entity) => entity;

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
        /// Compares two <see cref="GeneticEntity"/> objects based on their fitness values.
        /// </summary>
        private class FitnessValueComparer : IComparer<GeneticEntity>
        {
            private readonly FitnessType sortBasis;

            /// <summary>
            /// Initializes a new instance of the <see cref="FitnessValueComparer"/> class.
            /// </summary>
            /// <param name="sortBasis">Type of fitness value on which the comparison is based.</param>
            public FitnessValueComparer(FitnessType sortBasis)
            {
                this.sortBasis = sortBasis;
            }

            /// <summary>
            /// Compares the two <see cref="GeneticEntity"/> objects based on their fitness values.
            /// </summary>
            /// <param name="x">The first <see cref="GeneticEntity"/>.</param>
            /// <param name="y">The second <see cref="GeneticEntity"/>.</param>
            /// <returns>
            /// Zero if both entities have the same fitness values.  Less than zero if the first entity
            /// has a fitness value less than the second entity.  Greater than zero if the first entity
            /// has a fitness value greater than the second entity.
            /// </returns>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
            public int Compare(GeneticEntity x, GeneticEntity y)
            {
                return x.GetFitnessValue(this.sortBasis).CompareTo(y.GetFitnessValue(this.sortBasis));
            }
        }
    }
}
