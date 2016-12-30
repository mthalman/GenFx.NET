using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Scaling;
using GenFx.ComponentModel;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Scaling.SigmaScalingStrategy and is intended
    ///to contain all GenFx.ComponentLibrary.Scaling.SigmaScalingStrategy Unit Tests
    ///</summary>
    [TestClass()]
    public class SigmaScalingStrategyTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void SigmaScalingStrategy_Ctor()
        {
            int multiplier = 3;
            IGeneticAlgorithm algorithm = GetAlgorithm(multiplier);
            SigmaScalingStrategy strategy = new SigmaScalingStrategy(algorithm);
            Assert.IsInstanceOfType(strategy.Configuration, typeof(SigmaScalingStrategyConfiguration));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void SigmaScalingStrategy_Ctor_NullAlgorithm()
        {
            AssertEx.Throws<ArgumentNullException>(() => new SigmaScalingStrategy(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when a required config is missing.
        /// </summary>
        [TestMethod]
        public void SigmaScalingStrategy_Ctor_MissingConfig()
        {
            AssertEx.Throws<ArgumentException>(() => new SigmaScalingStrategy(new MockGeneticAlgorithm(new ComponentConfigurationSet())));
        }

        /// <summary>
        /// Tests that an exception is thrown when an invalid value is used for the SigmaScalingMultiplier setting.
        /// </summary>
        [TestMethod]
        public void SigmaScalingStrategy_Ctor_InvalidSetting()
        {
            SigmaScalingStrategyConfiguration config = new SigmaScalingStrategyConfiguration();
            AssertEx.Throws<ValidationException>(() => config.Multiplier = -2);
        }

        /// <summary>
        /// Tests that the Scale method works correctly.
        /// </summary>
        [TestMethod]
        public void SigmaScalingStrategy_Scale()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(5);
            SigmaScalingStrategy strategy = new SigmaScalingStrategy(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);
            PrivateObject populationAccessor = new PrivateObject(population, new PrivateType(typeof(PopulationBase<SimplePopulation, SimplePopulationConfiguration>)));
            AddEntity(algorithm, 4, population);
            AddEntity(algorithm, 10, population);
            AddEntity(algorithm, 20, population);
            AddEntity(algorithm, 0, population);

            populationAccessor.SetField("rawMean", (double)(4 + 10 + 20) / 4);
            populationAccessor.SetField("rawStandardDeviation", populationAccessor.Invoke("GetStandardDeviation", population.RawMean, false));

            strategy.Scale(population);

            Assert.AreEqual(33.17, Math.Round(population.Entities[0].ScaledFitnessValue, 2), "ScaledFitnessValue not calculated correctly.");
            Assert.AreEqual(39.17, Math.Round(population.Entities[1].ScaledFitnessValue, 2), "ScaledFitnessValue not calculated correctly.");
            Assert.AreEqual(49.17, Math.Round(population.Entities[2].ScaledFitnessValue, 2), "ScaledFitnessValue not calculated correctly.");
            Assert.AreEqual(29.17, Math.Round(population.Entities[3].ScaledFitnessValue, 2), "ScaledFitnessValue not calculated correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when an empty population is passed.
        /// </summary>
        [TestMethod]
        public void SigmaScalingStrategy_Scale_EmptyPopulation()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(10);
            SigmaScalingStrategy op = new SigmaScalingStrategy(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);
            AssertEx.Throws<ArgumentException>(() => op.Scale(population));
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod]
        public void SigmaScalingStrategy_Scale_NullPopulation()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm(10);
            SigmaScalingStrategy op = new SigmaScalingStrategy(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => op.Scale(null));
        }

        private void AddEntity(IGeneticAlgorithm algorithm, double fitness, IPopulation population)
        {
            MockEntity entity = new MockEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity<MockEntity, MockEntityConfiguration>)));
            accessor.SetField("rawFitnessValue", fitness);
            population.Entities.Add(entity);
        }

        private IGeneticAlgorithm GetAlgorithm(int multiplier)
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new SimplePopulationConfiguration(),
                FitnessScalingStrategy = new SigmaScalingStrategyConfiguration
                {
                    Multiplier = multiplier
                }
            });
            return algorithm;
        }
    }


}
