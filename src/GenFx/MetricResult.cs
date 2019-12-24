using System;
using System.Runtime.Serialization;

namespace GenFx
{
    /// <summary>
    /// Represents the result of a metrical calculation for a particular generation of a <see cref="Population"/>.
    /// </summary>
    [DataContract]
    public class MetricResult
    {
        [DataMember]
        private readonly int generationIndex;

        [DataMember]
        private readonly object resultValue;

        [DataMember]
        private readonly int populationIndex;

        [DataMember]
        private readonly Metric metric;

        /// <summary>
        /// Gets the <see cref="Metric"/> to which this result belongs.
        /// </summary>
        /// <value>The <see cref="Metric"/> to which this result belongs.</value>
        public Metric Metric
        {
            get { return this.metric; }
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
        /// Gets the value of the calculated metric.
        /// </summary>
        /// <value>The value of the calculated metric.</value>
        public object ResultValue
        {
            get { return this.resultValue; }
        }

        /// <summary>
        /// Gets the index of the population on which this metric result is calculated.
        /// </summary>
        /// <value>The index of the population on which this metric result is calculated.</value>
        public int PopulationIndex
        {
            get { return this.populationIndex; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetricResult"/> class.
        /// </summary>
        /// <param name="generationIndex">The index of the generation.</param>
        /// <param name="populationIndex">The index of the population on which this metric result is calculated.</param>
        /// <param name="resultValue">The value of the calculated metric.</param>
        /// <param name="metric">The <see cref="Metric"/> to which this result belongs.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="generationIndex"/> is less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="populationIndex"/> is less than zero.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="resultValue"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="metric"/> is null.</exception>
        public MetricResult(int generationIndex, int populationIndex, object resultValue, Metric metric)
        {
            if (generationIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(generationIndex), generationIndex, Resources.ErrorMsg_InvalidGenerationIndex);
            }

            if (populationIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(populationIndex), populationIndex, Resources.ErrorMsg_InvalidPopulationIndex);
            }

            this.generationIndex = generationIndex;
            this.populationIndex = populationIndex;
            this.resultValue = resultValue ?? throw new ArgumentNullException(nameof(resultValue));
            this.metric = metric ?? throw new ArgumentNullException(nameof(metric));
        }
    }
}
