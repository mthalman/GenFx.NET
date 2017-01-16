using System;

namespace GenFx
{
    /// <summary>
    /// Represents the result of a statistical calculation for a particular generation of a <see cref="Population"/>.
    /// </summary>
    public class StatisticResult
    {
        private int generationIndex;
        private object resultValue;
        private int populationId;
        private Statistic statistic;

        /// <summary>
        /// Gets the <see cref="Statistic"/> to which this result belongs.
        /// </summary>
        /// <value>The <see cref="Statistic"/> to which this result belongs.</value>
        public Statistic Statistic
        {
            get { return this.statistic; }
        }

        /// <summary>
        /// Gets the index of the generation.
        /// </summary>
        /// <value>The index of the generation.</value>
        public int GenerationIndex
        {
            get { return this.generationIndex; }
        }

        /// <summary>
        /// Gets the value of the calculated statistic.
        /// </summary>
        /// <value>The value of the calculated statistic.</value>
        public object ResultValue
        {
            get { return this.resultValue; }
        }

        /// <summary>
        /// Gets the ID of the population on which this statistic result is calculated.
        /// </summary>
        /// <value>The ID of the population on which this statistic result is calculated.</value>
        public int PopulationId
        {
            get { return this.populationId; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticResult"/> class.
        /// </summary>
        /// <param name="generationIndex">The index of the generation.</param>
        /// <param name="populationId">The ID of the population on which this statistic result is calculated.</param>
        /// <param name="resultValue">The value of the calculated statistic.</param>
        /// <param name="statistic">The <see cref="Statistic"/> to which this result belongs.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="generationIndex"/> is less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="populationId"/> is less than zero.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="resultValue"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="statistic"/> is null.</exception>
        public StatisticResult(int generationIndex, int populationId, object resultValue, Statistic statistic)
        {
            if (generationIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(generationIndex), generationIndex, Resources.ErrorMsg_InvalidGenerationIndex);
            }

            if (populationId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(populationId), populationId, Resources.ErrorMsg_InvalidPopulationID);
            }

            if (resultValue == null)
            {
                throw new ArgumentNullException(nameof(resultValue));
            }

            if (statistic == null)
            {
                throw new ArgumentNullException(nameof(statistic));
            }

            this.generationIndex = generationIndex;
            this.populationId = populationId;
            this.resultValue = resultValue;
            this.statistic = statistic;
        }
    }
}
