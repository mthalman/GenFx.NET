using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Terminators;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="FitnessTargetTerminator"/> class.
    ///</summary>
    [TestClass()]
    public class FitnessTargetTerminatorTest
    {
        /// <summary>
        /// Tests that the <see cref="FitnessTargetTerminator.IsComplete"/> method works correctly.
        /// </summary>
        [TestMethod]
        public async Task FitnessTargetTerminator_IsComplete_ScaledFitness()
        {
            double fitnessTarget = 15;
            GeneticAlgorithm algorithm = GetAlgorithm(fitnessTarget, FitnessType.Scaled);
            await algorithm.InitializeAsync();

            FitnessTargetTerminator terminator = (FitnessTargetTerminator)algorithm.Terminator;
            terminator.Initialize(algorithm);

            // Check with no populations
            Assert.IsFalse(terminator.IsComplete(), "No genetic entities have the fitness target.");

            MockEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            population.Entities.Add(entity);
            algorithm.Environment.Populations.Add(population);

            // Check with a population with one entity
            Assert.IsFalse(terminator.IsComplete(), "No genetic entities have the fitness target.");

            entity.ScaledFitnessValue = 15;
            Assert.IsTrue(terminator.IsComplete(), "A entity does have the fitness target.");
        }

        /// <summary>
        /// Tests that the <see cref="FitnessTargetTerminator.IsComplete"/> method works correctly.
        /// </summary>
        [TestMethod]
        public async Task FitnessTargetTerminator_IsComplete_RawFitness()
        {
            double fitnessTarget = 15;
            GeneticAlgorithm algorithm = GetAlgorithm(fitnessTarget, FitnessType.Raw);
            await algorithm.InitializeAsync();

            FitnessTargetTerminator terminator = (FitnessTargetTerminator)algorithm.Terminator;
            terminator.Initialize(algorithm);

            // Check with no populations
            Assert.IsFalse(terminator.IsComplete(), "No genetic entities have the fitness target.");

            MockEntity entity = new MockEntity();
            entity.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            population.Entities.Add(entity);
            algorithm.Environment.Populations.Add(population);

            // Check with a population with one entity
            Assert.IsFalse(terminator.IsComplete(), "No genetic entities have the fitness target.");

            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetField("rawFitnessValue", 15);
            Assert.IsTrue(terminator.IsComplete(), "A entity does have the fitness target.");
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void FitnessTargetTerminator_Serialization()
        {
            FitnessTargetTerminator terminator = new FitnessTargetTerminator();
            terminator.FitnessTarget = 20;
            terminator.FitnessType = FitnessType.Raw;

            FitnessTargetTerminator result = (FitnessTargetTerminator)SerializationHelper.TestSerialization(terminator, new Type[0]);

            Assert.AreEqual(terminator.FitnessTarget, result.FitnessTarget);
            Assert.AreEqual(terminator.FitnessType, result.FitnessType);
        }

        private static GeneticAlgorithm GetAlgorithm(double fitnessTarget, FitnessType fitnessType)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                PopulationSeed = new SimplePopulation(),
                GeneticEntitySeed = new MockEntity(),
                Terminator = new FitnessTargetTerminator
                {
                    FitnessTarget = fitnessTarget,
                    FitnessType = fitnessType
                }
            };
            return algorithm;
        }
    }


}
