using GenFx;
using GenFx.ComponentLibrary.Algorithms;
using GenFx.ComponentLibrary.Lists;
using GenFx.ComponentLibrary.Lists.BinaryStrings;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.SelectionOperators;
using GenFx.ComponentLibrary.Terminators;
using GenFx.Contracts;
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
            SimpleGeneticAlgorithm algorithm = new SimpleGeneticAlgorithm
            {
                EnvironmentSize = 1,
                FitnessEvaluator = new FitnessEvaluator(),
                GeneticEntitySeed = new BinaryStringEntity
                {
                    MinimumStartingLength = 20,
                    MaximumStartingLength = 20
                },
                PopulationSeed = new SimplePopulation
                {
                    PopulationSize = 100
                },
                SelectionOperator = new FitnessProportionateSelectionOperator
                {
                    SelectionBasedOnFitnessType = FitnessType.Raw
                },
                CrossoverOperator = new SinglePointCrossoverOperator
                {
                    CrossoverRate = 0.8
                },
                MutationOperator = new UniformBitMutationOperator
                {
                    MutationRate = 0.01
                },
                Terminator = new FitnessTargetTerminator
                {
                    FitnessTarget = 20,
                    FitnessValueType = FitnessType.Raw
                }
            };

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
