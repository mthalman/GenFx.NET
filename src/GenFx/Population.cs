using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Represents a collection of <see cref="GeneticEntity"/> objects which interact locally with each other.  A population is 
    /// the unit from which the <see cref="SelectionOperator"/> selects its genetic entities.
    /// </summary>
    /// <remarks>
    /// Populations can be isolated or interactive with one another through migration depending on
    /// which <see cref="GeneticAlgorithm"/> is used.
    /// </remarks>
    [DataContract]
    public abstract class Population : GeneticComponentWithAlgorithm
    {
        private const int DefaultPopulationSize = 1;

        [DataMember]
        private ObservableCollection<GeneticEntity> geneticEntities = new ObservableCollection<GeneticEntity>();

        [DataMember]
        private int index;

        [DataMember]
        private double rawMean;

        [DataMember]
        private double rawStandardDeviation;

        [DataMember]
        private double rawMax;

        [DataMember]
        private double rawMin;

        [DataMember]
        private double scaledMean;

        [DataMember]
        private double scaledStandardDeviation;

        [DataMember]
        private double scaledMax;

        [DataMember]
        private double scaledMin;

        [DataMember]
        private int minimumPopulationSize = DefaultPopulationSize;

        /// <summary>
        /// Gets or sets the minimum number of <see cref="GeneticEntity"/> objects that are contained by a population.
        /// </summary>
        /// <remarks>
        /// This value is defaulted to 1 and must be greater or equal to 1 to be valid for executing
        /// a genetic algorithm.
        /// </remarks>
        /// <exception cref="ValidationException">Value is invalid.</exception>
        [ConfigurationProperty]
        [IntegerValidator(MinValue = 1)]
        public int MinimumPopulationSize
        {
            get { return this.minimumPopulationSize; }
            set { this.SetProperty(ref this.minimumPopulationSize, value); }
        }

        /// <summary>
        /// Gets the minimum <see cref="GeneticEntity.ScaledFitnessValue"/> in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The minimum <see cref="GeneticEntity.ScaledFitnessValue"/> in the entire population of genetic entities.
        /// </value>
        public double ScaledMin
        {
            get { return this.scaledMin; }
        }

        /// <summary>
        /// Gets the maximum <see cref="GeneticEntity.ScaledFitnessValue"/> in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The maximum <see cref="GeneticEntity.ScaledFitnessValue"/> in the entire population of genetic entities.
        /// </value>
        public double ScaledMax
        {
            get { return this.scaledMax; }
        }

        /// <summary>
        /// Gets the minimum <see cref="GeneticEntity.RawFitnessValue"/> in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The minimum <see cref="GeneticEntity.RawFitnessValue"/> in the entire population of genetic entities.
        /// </value>
        public double RawMin
        {
            get { return this.rawMin; }
        }

        /// <summary>
        /// Gets the maximum <see cref="GeneticEntity.RawFitnessValue"/> in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The maximum <see cref="GeneticEntity.RawFitnessValue"/> in the entire population of genetic entities.
        /// </value>
        public double RawMax
        {
            get { return this.rawMax; }
        }

        /// <summary>
        /// Gets the standard deviation of all the <see cref="GeneticEntity.ScaledFitnessValue"/> values in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The standard deviation of all the <see cref="GeneticEntity.ScaledFitnessValue"/> values in the entire population of genetic entities.
        /// </value>
        public double ScaledStandardDeviation
        {
            get { return this.scaledStandardDeviation; }
        }

        /// <summary>
        /// Gets the mean of all the <see cref="GeneticEntity.ScaledFitnessValue"/> values in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The mean of all the <see cref="GeneticEntity.ScaledFitnessValue"/> values in the entire population of genetic entities.
        /// </value>
        public double ScaledMean
        {
            get { return this.scaledMean; }
        }

        /// <summary>
        /// Gets the standard deviation of all the <see cref="GeneticEntity.RawFitnessValue"/> values in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The standard deviation of all the <see cref="GeneticEntity.RawFitnessValue"/> values in the entire population of genetic entities.
        /// </value>
        public double RawStandardDeviation
        {
            get { return this.rawStandardDeviation; }
        }

        /// <summary>
        /// Gets the mean of all the <see cref="GeneticEntity.RawFitnessValue"/> values in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The mean of all the <see cref="GeneticEntity.RawFitnessValue"/> values in the entire population of genetic entities.
        /// </value>
        public double RawMean
        {
            get { return this.rawMean; }
        }

        /// <summary>
        /// Gets the collection of <see cref="GeneticEntity"/> objects contained by the population.
        /// </summary>
        [Browsable(false)]
        public ObservableCollection<GeneticEntity> Entities
        {
            get { return this.geneticEntities; }
        }

        /// <summary>
        /// Gets or sets the index of this population in the <see cref="GeneticEnvironment"/>.
        /// </summary>
        public int Index
        {
            get { return this.index; }
            set { this.index = value; }
        }

        /// <summary>
        /// Gets the size of the population.
        /// </summary>
        public int Size
        {
            get { return this.Entities.Count; }
        }

        /// <summary>
        /// Evaluates the <see cref="GeneticEntity.RawFitnessValue"/> of all the <see cref="GeneticEntity"/> objects
        /// within the population followed by evaluation of the <see cref="GeneticEntity.ScaledFitnessValue"/>
        /// using the <see cref="FitnessScalingStrategy"/>.
        /// </summary>
        public virtual async Task EvaluateFitnessAsync()
        {
            double rawSum = 0;

            List<Task> fitnessEvalTasks = new List<Task>();
            foreach (GeneticEntity entity in this.geneticEntities)
            {
                fitnessEvalTasks.Add(entity.EvaluateFitnessAsync());
            }

            // Wait for all entities to evaluate their fitness values
            await Task.WhenAll(fitnessEvalTasks);

            for (int i = 0; i < this.geneticEntities.Count; i++)
            {
                // Calculate the stats based on raw fitness value
                rawSum += this.geneticEntities[i].RawFitnessValue;

                if (i == 0 || this.geneticEntities[i].RawFitnessValue > this.rawMax)
                {
                    this.rawMax = this.scaledMax = this.geneticEntities[i].RawFitnessValue;
                }
                if (i == 0 || this.geneticEntities[i].RawFitnessValue < this.rawMin)
                {
                    this.rawMin = this.scaledMin = this.geneticEntities[i].RawFitnessValue;
                }
            }

            // Calculate the stats based on raw fitness value
            this.rawMean = this.scaledMean = rawSum / this.geneticEntities.Count;
            this.rawStandardDeviation = this.scaledStandardDeviation = this.GetStandardDeviation(this.rawMean, false);

            if (this.Algorithm.FitnessScalingStrategy != null)
            {
                // Scale the fitness values of the population.
                this.Algorithm.FitnessScalingStrategy.Scale(this);

                // Calculate the stats based on scaled fitness value
                double scaledSum = 0;
                for (int i = 0; i < this.geneticEntities.Count; i++)
                {
                    scaledSum += this.geneticEntities[i].ScaledFitnessValue;

                    if (i == 0 || this.geneticEntities[i].ScaledFitnessValue > this.scaledMax)
                    {
                        this.scaledMax = this.geneticEntities[i].ScaledFitnessValue;
                    }
                    if (i == 0 || this.geneticEntities[i].ScaledFitnessValue < this.scaledMin)
                    {
                        this.scaledMin = this.geneticEntities[i].ScaledFitnessValue;
                    }

                    this.scaledMean = scaledSum / this.geneticEntities.Count;
                    this.scaledStandardDeviation = this.GetStandardDeviation(this.scaledMean, true);
                }
            }
        }

        /// <summary>
        /// Creates the collection of <see cref="GeneticEntity"/> objects contained by this population.
        /// </summary>
        public Task InitializeAsync()
        {
            return this.InitializeCoreAsync();
        }

        /// <summary>
        /// Creates the collection of <see cref="GeneticEntity"/> objects contained by this population.
        /// </summary>
        /// <remarks>
        /// <para>The default implementation of this method creates X <see cref="GeneticEntity"/> objects
        /// where X is equal to <see cref="MinimumPopulationSize"/>.</para>
        /// <para><b>Notes to implementers:</b> This method can be overriden in a derived class
        /// to customize how a population is filled with <see cref="GeneticEntity"/> objects
        /// or how those <see cref="GeneticEntity"/> objects are created.</para>
        /// </remarks>
        protected virtual Task InitializeCoreAsync()
        {
            for (int i = 0; i < this.Algorithm.PopulationSeed.MinimumPopulationSize; i++)
            {
                GeneticEntity entity = (GeneticEntity)this.Algorithm.GeneticEntitySeed.CreateNewAndInitialize();
                this.geneticEntities.Add(entity);
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Returns the standard deviation of the raw fitness value for the population.
        /// </summary>
        /// <param name="mean">Mean raw fitness value for the population.</param>
        /// <param name="useScaledValues">Whether to use scaled fitness values as opposed to raw.</param>
        /// <returns>The standard deviation of the raw fitness value for the population.</returns>
        private double GetStandardDeviation(double mean, bool useScaledValues)
        {
            double diffSums = 0;
            for (int i = 0; i < this.geneticEntities.Count; i++)
            {
                double val;
                if (useScaledValues)
                {
                    val = this.geneticEntities[i].ScaledFitnessValue - mean;
                }
                else
                {
                    val = this.geneticEntities[i].RawFitnessValue - mean;
                }
                val = Math.Pow(val, 2);
                diffSums += val;
            }
            return Math.Sqrt(diffSums / this.geneticEntities.Count);
        }
    }
}
