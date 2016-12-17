using GenFx.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents a collection of <see cref="IGeneticEntity"/> objects which interact locally with each other.  A population is 
    /// the unit from which the <see cref="ISelectionOperator"/> selects its genetic entities.
    /// </summary>
    /// <remarks>
    /// Populations can be isolated or interactive with one another through migration depending on
    /// which <see cref="IGeneticAlgorithm"/> is used.
    /// </remarks>
    /// <seealso cref="IGeneticAlgorithm"/>
    public abstract class Population<TPopulation, TConfiguration> : GeneticComponentWithAlgorithm<TPopulation, TConfiguration>, IPopulation
        where TPopulation : Population<TPopulation, TConfiguration>
        where TConfiguration : PopulationConfiguration<TConfiguration, TPopulation>
    {
        private ObservableCollection<IGeneticEntity> geneticEntities = new ObservableCollection<IGeneticEntity>();
        private int index;
        private double rawMean;
        private double rawStandardDeviation;
        private double rawMax;
        private double rawMin;
        private double scaledMean;
        private double scaledStandardDeviation;
        private double scaledMax;
        private double scaledMin;

        /// <summary>
        /// Gets the minimum <see cref="IGeneticEntity.ScaledFitnessValue"/> in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The minimum <see cref="IGeneticEntity.ScaledFitnessValue"/> in the entire population of genetic entities.
        /// </value>
        public double ScaledMin
        {
            get { return this.scaledMin; }
        }

        /// <summary>
        /// Gets the maximum <see cref="IGeneticEntity.ScaledFitnessValue"/> in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The maximum <see cref="IGeneticEntity.ScaledFitnessValue"/> in the entire population of genetic entities.
        /// </value>
        public double ScaledMax
        {
            get { return this.scaledMax; }
        }

        /// <summary>
        /// Gets the minimum <see cref="IGeneticEntity.RawFitnessValue"/> in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The minimum <see cref="IGeneticEntity.RawFitnessValue"/> in the entire population of genetic entities.
        /// </value>
        public double RawMin
        {
            get { return this.rawMin; }
        }

        /// <summary>
        /// Gets the maximum <see cref="IGeneticEntity.RawFitnessValue"/> in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The maximum <see cref="IGeneticEntity.RawFitnessValue"/> in the entire population of genetic entities.
        /// </value>
        public double RawMax
        {
            get { return this.rawMax; }
        }

        /// <summary>
        /// Gets the standard deviation of all the <see cref="IGeneticEntity.ScaledFitnessValue"/> values in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The standard deviation of all the <see cref="IGeneticEntity.ScaledFitnessValue"/> values in the entire population of genetic entities.
        /// </value>
        public double ScaledStandardDeviation
        {
            get { return this.scaledStandardDeviation; }
        }

        /// <summary>
        /// Gets the mean of all the <see cref="IGeneticEntity.ScaledFitnessValue"/> values in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The mean of all the <see cref="IGeneticEntity.ScaledFitnessValue"/> values in the entire population of genetic entities.
        /// </value>
        public double ScaledMean
        {
            get { return this.scaledMean; }
        }

        /// <summary>
        /// Gets the standard deviation of all the <see cref="IGeneticEntity.RawFitnessValue"/> values in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The standard deviation of all the <see cref="IGeneticEntity.RawFitnessValue"/> values in the entire population of genetic entities.
        /// </value>
        public double RawStandardDeviation
        {
            get { return this.rawStandardDeviation; }
        }

        /// <summary>
        /// Gets the mean of all the <see cref="IGeneticEntity.RawFitnessValue"/> values in the entire population of genetic entities.
        /// </summary>
        /// <value>
        /// The mean of all the <see cref="IGeneticEntity.RawFitnessValue"/> values in the entire population of genetic entities.
        /// </value>
        public double RawMean
        {
            get { return this.rawMean; }
        }

        /// <summary>
        /// Gets the collection of <see cref="IGeneticEntity"/> objects contained by the population.
        /// </summary>
        [Browsable(false)]
        public ObservableCollection<IGeneticEntity> Entities
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
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this <see cref="Population{TPopulation, TConfiguration}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected Population(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Evaluates the <see cref="IGeneticEntity.RawFitnessValue"/> of all the <see cref="IGeneticEntity"/> objects
        /// within the population followed by evaluation of the <see cref="IGeneticEntity.ScaledFitnessValue"/>
        /// using the <see cref="IFitnessScalingStrategy"/>.
        /// </summary>
        public async Task EvaluateFitnessAsync()
        {
            double rawSum = 0;

            List<Task> fitnessEvalTasks = new List<Task>();
            foreach (IGeneticEntity entity in this.geneticEntities)
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

            if (this.Algorithm.Operators.FitnessScalingStrategy != null)
            {
                // Scale the fitness values of the population.
                this.Algorithm.Operators.FitnessScalingStrategy.Scale(this);

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
        /// Creates the collection of <see cref="IGeneticEntity"/> objects contained by this population.
        /// </summary>
        public Task InitializeAsync()
        {
            return this.InitializeCoreAsync();
        }

        /// <summary>
        /// Creates the collection of <see cref="IGeneticEntity"/> objects contained by this population.
        /// </summary>
        /// <remarks>
        /// <para>The default implementation of this method creates X <see cref="IGeneticEntity"/> objects
        /// where X is equal to <see cref="PopulationConfiguration{TConfiguration, TPopulation}.PopulationSize"/>.</para>
        /// <para><b>Notes to implementers:</b> This method can be overriden in a derived class
        /// to customize how a <see cref="Population{TPopulation, TConfiguration}"/> is filled with <see cref="IGeneticEntity"/> objects
        /// or how those <see cref="IGeneticEntity"/> objects are created.</para>
        /// </remarks>
        protected virtual Task InitializeCoreAsync()
        {
            for (int i = 0; i < this.Algorithm.ConfigurationSet.Population.PopulationSize; i++)
            {
                this.geneticEntities.Add((IGeneticEntity)this.Algorithm.ConfigurationSet.Entity.CreateComponent(this.Algorithm));
            }

            return Task.FromResult(true);
        }

        /// <summary>
        /// Restores the state of this component.
        /// </summary>
        /// <param name="state">The state of the component to restore from.</param>
        public override void RestoreState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.RestoreState(state);

            this.index = (int)state[nameof(index)];
            this.rawMax = (double)state[nameof(rawMax)];
            this.rawMean = (double)state[nameof(rawMean)];
            this.rawMin = (double)state[nameof(rawMin)];
            this.scaledMax = (double)state[nameof(scaledMax)];
            this.scaledMean = (double)state[nameof(scaledMean)];
            this.scaledMin = (double)state[nameof(scaledMin)];
            this.scaledStandardDeviation = (double)state[nameof(scaledStandardDeviation)];

            this.Entities.Clear();

            KeyValueMapCollection entityStates = (KeyValueMapCollection)state[nameof(this.geneticEntities)];

            foreach (KeyValueMap entityState in entityStates)
            {
                IGeneticEntity entity = (IGeneticEntity)this.Algorithm.ConfigurationSet.Entity.CreateComponent(this.Algorithm);
                entity.RestoreState(entityState);
                this.Entities.Add(entity);
            }
        }

        /// <summary>
        /// Sets the serializable state of this component on the state object.
        /// </summary>
        /// <param name="state">The object containing the serializable state of this object.</param>
        public override void SetSaveState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            base.SetSaveState(state);

            state[nameof(index)] = this.index;
            state[nameof(rawMax)] = this.rawMax;
            state[nameof(rawMean)] = this.rawMean;
            state[nameof(rawMin)] = this.rawMin;
            state[nameof(rawStandardDeviation)] = this.rawStandardDeviation;
            state[nameof(scaledMax)] = this.scaledMax;
            state[nameof(scaledMean)] = this.scaledMean;
            state[nameof(scaledMin)] = this.scaledMin;
            state[nameof(scaledStandardDeviation)] = this.scaledStandardDeviation;
            state[nameof(geneticEntities)] = new KeyValueMapCollection(this.Entities.Select(e => e.SaveState()));
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
