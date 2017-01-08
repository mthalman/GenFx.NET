using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Scaling;
using GenFx.Contracts;
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
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod()]
        public void ExponentialScalingStrategy_Ctor()
        {
            double scalingPower = 2;
            IGeneticAlgorithm algorithm = GetAlgorithm(2);

            ExponentialScalingStrategy strategy = new ExponentialScalingStrategy(algorithm);
            Assert.IsInstanceOfType(strategy.Configuration, typeof(ExponentialScalingStrategyFactoryConfig));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod()]
        public void ExponentialScalingStrategy_Ctor_NullAlgorithm()
        {
            AssertEx.Throws<ArgumentNullException>(() => new ExponentialScalingStrategy(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when a required setting is missing.
        /// </summary>
        [TestMethod()]
        public void ExponentialScalingStrategy_Ctor_MissingSetting()
        {
            AssertEx.Throws<ArgumentException>(() => new ExponentialScalingStrategy(new MockGeneticAlgorithm(new ComponentFactoryConfigSet())));
        }

        /// <summary>
        /// Tests that the Scale method works correctly.
        /// </summary>
        [TestMethod()]
        public void ExponentialScalingStrategy_Scale()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(2);

            ExponentialScalingStrategy target = new ExponentialScalingStrategy(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);
            MockEntity entity1 = new MockEntity(algorithm);
            PrivateObject entity1Accessor = new PrivateObject(entity1, new PrivateType(typeof(GeneticEntity<MockEntity, MockEntityFactoryConfig>)));
            entity1Accessor.SetField("rawFitnessValue", 5);
            MockEntity entity2 = new MockEntity(algorithm);
            PrivateObject entity2Accessor = new PrivateObject(entity2, new PrivateType(typeof(GeneticEntity<MockEntity, MockEntityFactoryConfig>)));
            entity2Accessor.SetField("rawFitnessValue", 7);
            population.Entities.Add(entity1);
            population.Entities.Add(entity2);
            target.Scale(population);

            Assert.AreEqual((double)25, entity1.ScaledFitnessValue, "ScaledFitnessValue not set correctly.");
            Assert.AreEqual((double)49, entity2.ScaledFitnessValue, "ScaledFitnessValue not set correctly.");
        }

        private static IGeneticAlgorithm GetAlgorithm(double scalingPower)
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig(),
                FitnessScalingStrategy = new ExponentialScalingStrategyFactoryConfig
                {
                    ScalingPower = scalingPower
                }
            });
            return algorithm;
        }
    }


}
