using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;
using System.Collections.Generic;

namespace GenFx.ComponentLibrary.SelectionOperators
{
    /// <summary>
    /// Provides the abstract base class for Boltzmann selection for a genetic algorithm.
    /// </summary>
    /// <remarks>
    /// Boltzmann selection is a technique whereby selection pressure gradually increases as the generation count increases.
    /// With Boltzmann selection, the selection pressure is low at the beginning of the run, meaning
    /// that every entity has a reasonable probability of being selected.  But as the
    /// generations increase, so does the selection pressure which causes the entity
    /// objects with the better fitness to stand out even more.  This technique allows the population
    /// time to adequately search the fitness landscape before prematurely converging on a solution.
    /// Boltzmann selection uses a term called temperature which controls the selection pressure.  A high
    /// temperature means a low selection pressure.
    /// </remarks>
    public abstract class BoltzmannSelectionOperator : SelectionOperatorBase
    {
        private double initialTemperature;
        private double currentTemperature;

        /// <summary>
        /// Gets or sets the initial temperature of the selection operator.
        /// </summary>
        [ConfigurationProperty]
        public double InitialTemperature
        {
            get { return this.initialTemperature; }
            set { this.SetProperty(ref this.initialTemperature, value); }
        }
        
        /// <summary>
        /// Gets or sets the current temperature of the selection operator.
        /// </summary>
        public double CurrentTemperature
        {
            get { return this.currentTemperature; }
            set { this.SetProperty(ref this.currentTemperature, value); }
        }

        /// <summary>
        /// Initializes the component to ensure its readiness for algorithm execution.
        /// </summary>
        /// <param name="algorithm">The algorithm that is to use this component.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public override void Initialize(IGeneticAlgorithm algorithm)
        {
            base.Initialize(algorithm);
            algorithm.GenerationCreated += new EventHandler(this.Algorithm_GenerationCreated);
            this.CurrentTemperature = this.InitialTemperature;
        }

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
                totalSubVals += Math.Pow(Math.E, entity.GetFitnessValue(this.SelectionBasedOnFitnessType) / this.CurrentTemperature);

                if (Double.IsInfinity(totalSubVals))
                {
                    throw new OverflowException(StringUtil.GetFormattedString(Resources.ErrorMsg_BoltzmannTotalOverflow, this.GetType().Name));
                }
            }

            double meanSubVals = totalSubVals / population.Entities.Count;

            List<WheelSlice> wheelSlices = new List<WheelSlice>();

            foreach (IGeneticEntity entity in population.Entities)
            {
                double expectedValue = Math.Pow(Math.E, entity.GetFitnessValue(this.SelectionBasedOnFitnessType) / this.CurrentTemperature) / meanSubVals;
                wheelSlices.Add(new WheelSlice(entity, expectedValue));
            }

            return RouletteWheelSampler.GetEntity(wheelSlices);
        }

        /// <summary>
        /// When overriden in a derived class, sets the <see cref="CurrentTemperature"/>
        /// property according to an annealing schedule.
        /// </summary>
        /// <remarks>
        /// An annealing schedule is an algorithm that determines how much to decrease the 
        /// temperature for each generation.
        /// </remarks>
        public abstract void AdjustTemperature();
    }
}
