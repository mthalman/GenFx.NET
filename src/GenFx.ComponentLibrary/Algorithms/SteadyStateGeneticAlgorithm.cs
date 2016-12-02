using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using GenFx.ComponentLibrary.Properties;
using GenFx.ComponentModel;
using GenFx.Validation;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// A type of genetic algorithm that replaces the weakest members of a <see cref="Population"/>
    /// with the offspring of the previous generation.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Usage of an <see cref="ElitismStrategy"/> type with this algorithm will result in elitism being ignored
    /// since all high-fitness <see cref="GeneticEntity"/> objects will be moved to the next generation anyways.
    /// </para>
    /// </remarks>
    public class SteadyStateGeneticAlgorithm : GeneticAlgorithm
    {
        private bool isValidated;

        /// <summary>
        /// Gets the value indicating how many members of the the <see cref="Population"/> are to 
        /// be replaced with the offspring of the previous generation.
        /// </summary>
        /// <value>
        /// A value representing a fixed amount of <see cref="GeneticEntity"/> objects to be replaced
        /// or the percentage that is to be replaced.
        /// </value>
        /// <exception cref="ArgumentException">Algorithm does not contain a configuration object.</exception>
        /// <exception cref="ArgumentException">Algorithm contains a configuration object that is not associated with that component.</exception>
        /// <exception cref="ValidationException">The algorithm configuration is in an invalid state.</exception>
        public PopulationReplacementValue PopulationReplacementValue
        {
            get
            {
                if (!this.isValidated)
                {
                    base.ValidateComponentConfiguration(this);
                    this.isValidated = true;
                }
                return ((SteadyStateGeneticAlgorithmConfiguration)this.ConfigurationSet.GeneticAlgorithm).PopulationReplacementValue;
            }
        }

        /// <summary>
        /// Modifies <paramref name="population"/> to become the next generation of <see cref="GeneticEntity"/> objects.
        /// </summary>
        /// <param name="population">The current <see cref="Population"/> to be modified.</param>
        /// <exception cref="ArgumentNullException"><paramref name="population"/> is null.</exception>
        protected override Task CreateNextGenerationAsync(Population population)
        {
            if (population == null)
            {
                throw new ArgumentNullException(nameof(population));
            }

            int populationCount = population.Entities.Count;
            PopulationReplacementValue replacementValue = this.PopulationReplacementValue;
            int replacementCount;
            if (replacementValue.Kind == ReplacementValueKind.Percentage)
            {
                replacementCount = Convert.ToInt32(
                    Math.Round(
                        populationCount * ((double)replacementValue.Value / 100)
                    ));
            }
            else
            {
                replacementCount = replacementValue.Value;
            }

            // Add a select number of potentially modified Entities to the new generation.
            for (int i = 0; i < replacementCount; i++)
            {
                IList<GeneticEntity> childEntities = this.SelectGeneticEntitiesAndApplyCrossoverAndMutation(population);

                for (int entityIndex = 0; entityIndex < childEntities.Count; entityIndex++)
                {
                    population.Entities.Add(childEntities[entityIndex]);
                }
            }

            // Remove the weakest Entities from the population.
            ObservableCollection<GeneticEntity> workingGeneticEntities = new ObservableCollection<GeneticEntity>(population.Entities);
            GeneticEntity[] sortedEntities = workingGeneticEntities.GetEntitiesSortedByFitness(
                this.Operators.SelectionOperator.SelectionBasedOnFitnessType,
                this.Operators.FitnessEvaluator.EvaluationMode).ToArray();

            population.Entities.Clear();
            for (int i = sortedEntities.Length - 1; i >= sortedEntities.Length - populationCount; i--)
            {
                population.Entities.Add(sortedEntities[i]);
            }

            return Task.FromResult(true);
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="SteadyStateGeneticAlgorithm"/>.
    /// </summary>
    [Component(typeof(SteadyStateGeneticAlgorithm))]
    public class SteadyStateGeneticAlgorithmConfiguration : GeneticAlgorithmConfiguration
    {
        private PopulationReplacementValue replacementValue = new PopulationReplacementValue(10, ReplacementValueKind.Percentage);

        /// <summary>
        /// Gets or sets the value indicating how many members of the the <see cref="Population"/> are to 
        /// be replaced with the offspring of the previous generation.
        /// </summary>
        /// <value>
        /// A value representing a fixed amount of <see cref="GeneticEntity"/> objects to be replaced
        /// or the percentage that is to be replaced.
        /// </value>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [CustomValidator(typeof(PopulationReplacementValueValidator))]
        public PopulationReplacementValue PopulationReplacementValue
        {
            get { return this.replacementValue; }
            set { this.SetProperty(ref this.replacementValue, value); }
        }

        /// <summary>
        /// Validator for the <see cref="PopulationReplacementValue"/> type.
        /// </summary>
        private class PopulationReplacementValueValidator : Validator
        {
            /// <summary>
            /// Returns whether <paramref name="value"/> is valid.
            /// </summary>
            /// <param name="value">Object to be validated.</param>
            /// <param name="propertyName">Name of the property being validated.</param>
            /// <param name="errorMessage">Error message that should be displayed if the property fails validation.</param>
            /// <param name="owner">The object that owns the property being validated.</param>
            /// <returns>true if <paramref name="value"/> is valid; otherwise, false.</returns>
            public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
            {
                bool isValid;

                if (!(value is PopulationReplacementValue))
                {
                    isValid = false;
                }
                else
                {
                    PopulationReplacementValue popReplacementVal = (PopulationReplacementValue)value;
                    int maxValue;
                    if (popReplacementVal.Kind == ReplacementValueKind.Percentage)
                    {
                        maxValue = 100;
                    }
                    else
                    {
                        maxValue = Int32.MaxValue;
                    }

                    IntegerValidator intValidator = new IntegerValidator(0, maxValue);
                    string tempErrorMsg;
                    isValid = intValidator.IsValid(popReplacementVal.Value, propertyName, owner, out tempErrorMsg);
                }

                if (!isValid)
                {
                    errorMessage = StringUtil.GetFormattedString(LibResources.ErrorMsg_InvalidPopulationReplacementValue, propertyName);
                }
                else
                {
                    errorMessage = null;
                }

                return isValid;
            }
        }
    }
}
