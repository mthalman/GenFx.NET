using GenFx.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Container of all the <see cref="IPopulation"/> objects for a genetic algorithm.  This class cannot be inherited.
    /// </summary>
    public sealed class GeneticEnvironment
    {
        private ObservableCollection<IPopulation> populations = new ObservableCollection<IPopulation>();
        private IGeneticAlgorithm algorithm;

        /// <summary>
        /// Gets the collection of <see cref="IPopulation"/> objects contained by this <see cref="GeneticEnvironment"/>.
        /// </summary>
        public ObservableCollection<IPopulation> Populations
        {
            get { return this.populations; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticEnvironment"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this <see cref="GeneticEnvironment"/>.</param>
        internal GeneticEnvironment(IGeneticAlgorithm algorithm)
        {
            this.algorithm = algorithm;
        }

        /// <summary>
        /// Saves the state of the environment.
        /// </summary>
        public KeyValueMap SaveState()
        {
            KeyValueMap state = new KeyValueMap();
            state[nameof(this.populations)] = new KeyValueMapCollection(this.Populations.Select(p => p.SaveState()).Cast<KeyValueMap>());
            return state;
        }

        /// <summary>
        /// Restores the state of the environment.
        /// </summary>
        /// <param name="state">State from which to restore.</param>
        public void RestoreState(KeyValueMap state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            this.Populations.Clear();

            foreach (KeyValueMap populationState in (KeyValueMapCollection)state[nameof(this.populations)])
            {
                IPopulation population = (IPopulation)this.algorithm.ConfigurationSet.Population.CreateComponent(this.algorithm);
                population.RestoreState(populationState);
                this.Populations.Add(population);
            }
        }

        /// <summary>
        /// Evaluates the fitness of all the <see cref="IPopulation"/> objects contained by this <see cref="GeneticEnvironment"/>
        /// </summary>
        internal Task EvaluateFitnessAsync()
        {
            List<Task> fitnessEvalTasks = new List<Task>();

            foreach (IPopulation population in this.populations)
            {
                fitnessEvalTasks.Add(population.EvaluateFitnessAsync());
            }

            return Task.WhenAll(fitnessEvalTasks);
        }

        /// <summary>
        /// Populates the collection of <see cref="IPopulation"/> objects.
        /// </summary>
        internal Task InitializeAsync()
        {
            List<Task> generatePopulationTasks = new List<Task>();

            for (int i = 0; i < this.algorithm.ConfigurationSet.GeneticAlgorithm.EnvironmentSize; i++)
            {
                IPopulation newPopulation = (IPopulation)this.algorithm.ConfigurationSet.Population.CreateComponent(this.algorithm);
                newPopulation.Index = i;
                this.populations.Add(newPopulation);

                generatePopulationTasks.Add(newPopulation.InitializeAsync());
            }

            return Task.WhenAll(generatePopulationTasks);
        }
    }
}
