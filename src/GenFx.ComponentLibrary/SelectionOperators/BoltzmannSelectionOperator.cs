using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GenFx.ComponentLibrary.Properties;
using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Provides the abstract base class for Boltzmann selection for a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Boltzmann selection is a technique whereby selection pressure gradually increases as the generation count increases.
    /// With Boltzmann selection, the selection pressure is low at the beginning of the run, meaning
    /// that every <see cref="GeneticEntity"/> has a reasonable probability of being selected.  But as the
    /// generations increase, so does the selection pressure which causes the <see cref="GeneticEntity"/>
    /// objects with the better fitness to stand out even more.  This technique allows the <see cref="Population"/>
    /// time to adequately search the fitness landscape before prematurely converging on a solution.
    /// Boltzmann selection uses a term called temperature which controls the selection pressure.  A high
    /// temperature means a low selection pressure.
    /// </para>
    /// <para>
    /// <b>Notes to implementers:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by using the <see cref="ComponentConfigurationSet.SelectionOperator"/> 
    /// property.
    /// </para>
    /// </remarks>
    public abstract class BoltzmannSelectionOperator : SelectionOperator
    {
        /// <summary>
        /// Gets the temperature which adjusts the selection pressure.
        /// </summary>
        /// <value>The temperature of the <see cref="BoltzmannSelectionOperator"/>.</value>
        public double Temperature
        {
            get { return ((BoltzmannSelectionOperatorConfiguration)this.Algorithm.ConfigurationSet.SelectionOperator).Temperature; }
            set { ((BoltzmannSelectionOperatorConfiguration)this.Algorithm.ConfigurationSet.SelectionOperator).Temperature = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BoltzmannSelectionOperator"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="BoltzmannSelectionOperator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        protected BoltzmannSelectionOperator(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
            algorithm.FitnessEvaluated += new EventHandler<EnvironmentFitnessEvaluatedEventArgs>(this.Algorithm_GenerationCreated);
        }

        /// <summary>
        /// Adjusts the temperature whenever a new generation has been created.
        /// </summary>
        private void Algorithm_GenerationCreated(object sender, EnvironmentFitnessEvaluatedEventArgs e)
        {
            this.AdjustTemperature();
        }

        /// <summary>
        /// Selects a <see cref="GeneticEntity"/> from <paramref name="population"/> according to the
        /// Boltzmann selection algorithm.
        /// </summary>
        /// <param name="population"><see cref="Population"/> containing the <see cref="GeneticEntity"/>
        /// objects from which to select.</param>
        /// <returns>The <see cref="GeneticEntity"/> object that was selected.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        /// <exception cref="OverflowException">Sum of the entity fitness values has exceeded the range of <see cref="Double"/>.</exception>
        protected override GeneticEntity SelectEntityFromPopulation(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            double totalSubVals = 0;
            foreach (GeneticEntity entity in population.Entities)
            {
                totalSubVals += Math.Pow(Math.E, entity.GetFitnessValue(this.SelectionBasedOnFitnessType) / this.Temperature);

                if (Double.IsInfinity(totalSubVals))
                {
                    throw new OverflowException(StringUtil.GetFormattedString(LibResources.ErrorMsg_BoltzmannTotalOverflow, typeof(BoltzmannSelectionOperator).Name));
                }
            }

            double meanSubVals = totalSubVals / population.Entities.Count;

            List<WheelSlice> wheelSlices = new List<WheelSlice>();

            foreach (GeneticEntity entity in population.Entities)
            {
                double expectedValue = Math.Pow(Math.E, entity.GetFitnessValue(this.SelectionBasedOnFitnessType) / this.Temperature) / meanSubVals;
                wheelSlices.Add(new WheelSlice(entity, expectedValue));
            }

            return RouletteWheelSampler.GetEntity(wheelSlices);
        }

        /// <summary>
        /// When overriden in a derived class, sets the <see cref="BoltzmannSelectionOperator.Temperature"/>
        /// property according to an annealing schedule.
        /// </summary>
        /// <remarks>
        /// An annealing schedule is an algorithm that determines how much to decrease the 
        /// temperature for each generation.
        /// </remarks>
        public abstract void AdjustTemperature();
    }

    /// <summary>
    /// Represents the configuration of <see cref="BoltzmannSelectionOperator"/>.
    /// </summary>
    [Component(typeof(BoltzmannSelectionOperator))]
    public abstract class BoltzmannSelectionOperatorConfiguration : SelectionOperatorConfiguration
    {
        internal const string TemperatureProperty = "Temperature";

        private double temperature;

        /// <summary>
        /// Gets or sets the temperature which adjusts the selection pressure.
        /// </summary>
        /// <value>The temperature of the <see cref="BoltzmannSelectionOperator"/>.</value>
        public double Temperature
        {
            get { return this.temperature; }
            set
            {
                this.temperature = value;
                this.OnPropertyChanged(BoltzmannSelectionOperatorConfiguration.TemperatureProperty);
            }
        }
    }
}
