using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Scaling;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GenFxTests
{
    /// <summary>
    /// This is a test class for GenFx.ComponentLibrary.Scaling.ExponentialScalingStrategy and is intended
    /// to contain all GenFx.ComponentLibrary.Scaling.ExponentialScalingStrategy Unit Tests
    /// </summary>
    [TestClass()]
    public class ExponentialScalingStrategyTest
    {
        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod()]
        public void ExponentialScalingStrategy_Ctor_NullAlgorithm()
        {
            ExponentialScalingStrategy strategy = new ExponentialScalingStrategy();
            AssertEx.Throws<ArgumentNullException>(() => strategy.Initialize(null));
        }

        /// <summary>
        /// Tests that the Scale method works correctly.
        /// </summary>
        [TestMethod()]
        public void ExponentialScalingStrategy_Scale()
        {
            GeneticAlgorithm algorithm = GetAlgorithm(2);

            ExponentialScalingStrategy target = (ExponentialScalingStrategy)algorithm.FitnessScalingStrategy;
            target.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            MockEntity entity1 = new MockEntity();
            entity1.Initialize(algorithm);
            PrivateObject entity1Accessor = new PrivateObject(entity1, new PrivateType(typeof(GeneticEntity)));
            entity1Accessor.SetField("rawFitnessValue", 5);
            MockEntity entity2 = new MockEntity();
            entity2.Initialize(algorithm);
            PrivateObject entity2Accessor = new PrivateObject(entity2, new PrivateType(typeof(GeneticEntity)));
            entity2Accessor.SetField("rawFitnessValue", 7);
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            target.Scale(population);

            Assert.AreEqual((double)25, entity1.ScaledFitnessValue, "ScaledFitnessValue not set correctly.");
            Assert.AreEqual((double)49, entity2.ScaledFitnessValue, "ScaledFitnessValue not set correctly.");
        }

        private static GeneticAlgorithm GetAlgorithm(double scalingPower)
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                FitnessScalingStrategy = new ExponentialScalingStrategy
                {
                    ScalingPower = scalingPower
                }
            };
            return algorithm;
        }
    }


}
