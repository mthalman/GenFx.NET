using GenFx;
using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentLibrary.Lists;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.SelectionOperators;
using GenFx.ComponentLibrary.Terminators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBinaryString
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAlgorithmAsync().Wait();
        }

        private static async Task RunAlgorithmAsync()
        {
            ComponentConfigurationSet configSet = new ComponentConfigurationSet
            {
                GeneticAlgorithm = new SimpleGeneticAlgorithmConfiguration
                {
                    EnvironmentSize = 1
                },
                FitnessEvaluator = new FitnessEvaluatorConfiguration(),
                Entity = new FixedLengthBinaryStringEntityConfiguration
                {
                    Length = 20
                },
                Population = new SimplePopulationConfiguration
                {
                    PopulationSize = 100
                },
                SelectionOperator = new FitnessProportionateSelectionOperatorConfiguration
                {
                    SelectionBasedOnFitnessType = FitnessType.Raw
                },
                CrossoverOperator = new SinglePointCrossoverOperatorConfiguration
                {
                    CrossoverRate = 0.8
                },
                MutationOperator = new UniformBitMutationOperatorConfiguration
                {
                    MutationRate = 0.01
                },
                Terminator = new FitnessTargetTerminatorConfiguration
                {
                    FitnessTarget = 20,
                    FitnessValueType = FitnessType.Raw
                }
            };

            SimpleGeneticAlgorithm algorithm = new SimpleGeneticAlgorithm(configSet);
            algorithm.GenerationCreated += Algorithm_GenerationCreated;
            await algorithm.InitializeAsync();
            await algorithm.RunAsync();

            IEnumerable<IGeneticEntity> top10Entities =
                algorithm.Environment.Populations[0].Entities.GetEntitiesSortedByFitness(
                    FitnessType.Raw, FitnessEvaluationMode.Maximize)
                .Reverse()
                .Take(10);

            Console.WriteLine();
            Console.WriteLine("Top 10 entities:");
            foreach (IGeneticEntity entity in top10Entities)
            {
                Console.WriteLine("Bits: " + entity.Representation + ", Fitness: " + entity.RawFitnessValue);
            }

            Console.ReadLine();
        }

        private static void Algorithm_GenerationCreated(object sender, EventArgs e)
        {
            Console.WriteLine("Generation: {0}", ((IGeneticAlgorithm)sender).CurrentGeneration);
        }
    }
}
