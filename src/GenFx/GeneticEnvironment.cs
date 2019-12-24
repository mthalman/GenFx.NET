using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GenFx
{
    /// <summary>
    /// Container of all the <see cref="Population"/> objects for a genetic algorithm.  This class cannot be inherited.
    /// </summary>
    [DataContract]
    public sealed class GeneticEnvironment
    {
        [DataMember]
        private readonly ObservableCollection<Population> populations = new ObservableCollection<Population>();

        [DataMember]
        private readonly GeneticAlgorithm algorithm;

        /// <summary>
        /// Gets the collection of <see cref="Population"/> objects contained by this <see cref="GeneticEnvironment"/>.
        /// </summary>
        public ObservableCollection<Population> Populations
        {
            get { return this.populations; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneticEnvironment"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="GeneticEnvironment"/>.</param>
        internal GeneticEnvironment(GeneticAlgorithm algorithm)
        {
            this.algorithm = algorithm;
        }
        
        /// <summary>
        /// Evaluates the fitness of all the <see cref="Population"/> objects contained by this <see cref="GeneticEnvironment"/>
        /// </summary>
        internal Task EvaluateFitnessAsync()
        {
            List<Task> fitnessEvalTasks = new List<Task>();

            foreach (Population population in this.populations)
            {
                fitnessEvalTasks.Add(population.EvaluateFitnessAsync());
            }

            return Task.WhenAll(fitnessEvalTasks);
        }

        /// <summary>
        /// Populates the collection of <see cref="Population"/> objects.
        /// </summary>
        internal Task InitializeAsync()
        {
            List<Task> generatePopulationTasks = new List<Task>();

            for (int i = 0; i < this.algorithm.MinimumEnvironmentSize; i++)
            {
                Population newPopulation = (Population)this.algorithm.PopulationSeed.CreateNewAndInitialize();
                newPopulation.Index = i;
                this.populations.Add(newPopulation);

                generatePopulationTasks.Add(newPopulation.InitializeAsync());
            }

            return Task.WhenAll(generatePopulationTasks);
        }
    }
}
