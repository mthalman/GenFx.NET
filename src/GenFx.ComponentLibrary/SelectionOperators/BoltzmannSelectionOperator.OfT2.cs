using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Provides the abstract base class for Boltzmann selection for a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Boltzmann selection is a technique whereby selection pressure gradually increases as the generation count increases.
    /// With Boltzmann selection, the selection pressure is low at the beginning of the run, meaning
    /// that every entity has a reasonable probability of being selected.  But as the
    /// generations increase, so does the selection pressure which causes the entity
    /// objects with the better fitness to stand out even more.  This technique allows the population
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
    public abstract class BoltzmannSelectionOperator<TSelection, TConfiguration> : SelectionOperatorBase<TSelection, TConfiguration>
        where TSelection : BoltzmannSelectionOperator<TSelection, TConfiguration>
        where TConfiguration : BoltzmannSelectionOperatorConfiguration<TConfiguration, TSelection>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        protected BoltzmannSelectionOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
            algorithm.GenerationCreated += new EventHandler(this.Algorithm_GenerationCreated);
            this.Temperature = this.Configuration.InitialTemperature;
        }

        /// <summary>
        /// Gets or sets the current temperature of the selection operator.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// Adjusts the temperature whenever a new generation has been created.
        /// </summary>
        private void Algorithm_GenerationCreated(object sender, EventArgs e)
        {
            this.AdjustTemperature();
        }

        /// <summary>
        /// Selects a <see cref="IGeneticEntity"/> from <paramref name="population"/> according to the
        /// Boltzmann selection algorithm.
        /// </summary>
        /// <param name="population"><see cref="IPopulation"/> containing the <see cref="IGeneticEntity"/>
        /// objects from which to select.</param>
        /// <returns>The <see cref="IGeneticEntity"/> object that was selected.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        /// <exception cref="OverflowException">Sum of the entity fitness values has exceeded the range of <see cref="Double"/>.</exception>
        protected override IGeneticEntity SelectEntityFromPopulation(IPopulation population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            double totalSubVals = 0;
            foreach (IGeneticEntity entity in population.Entities)
            {
                totalSubVals += Math.Pow(Math.E, entity.GetFitnessValue(this.Configuration.SelectionBasedOnFitnessType) / this.Configuration.InitialTemperature);

                if (Double.IsInfinity(totalSubVals))
                {
                    throw new OverflowException(StringUtil.GetFormattedString(LibResources.ErrorMsg_BoltzmannTotalOverflow, this.GetType().Name));
                }
            }

            double meanSubVals = totalSubVals / population.Entities.Count;

            List<WheelSlice> wheelSlices = new List<WheelSlice>();

            foreach (IGeneticEntity entity in population.Entities)
            {
                double expectedValue = Math.Pow(Math.E, entity.GetFitnessValue(this.Configuration.SelectionBasedOnFitnessType) / this.Configuration.InitialTemperature) / meanSubVals;
                wheelSlices.Add(new WheelSlice(entity, expectedValue));
            }

            return RouletteWheelSampler.GetEntity(wheelSlices);
        }

        /// <summary>
        /// When overriden in a derived class, sets the <see cref="BoltzmannSelectionOperatorConfiguration{TConfiguration, TSelection}.InitialTemperature"/>
        /// property according to an annealing schedule.
        /// </summary>
        /// <remarks>
        /// An annealing schedule is an algorithm that determines how much to decrease the 
        /// temperature for each generation.
        /// </remarks>
        public abstract void AdjustTemperature();
    }
}
