using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Statistics;
using GenFx.Contracts;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Statistics.BestMaxStatistic and is intended
    ///to contain all GenFx.ComponentLibrary.Statistics.BestMaxStatistic Unit Tests
    ///</summary>
    [TestClass()]
    public class BestMaxStatisticTest
    {
        /// <summary>
        /// Tests that the correct value is returned from BestMaxStatistic.GetResultValue.
        /// </summary>
        [TestMethod()]
        public void GetResultValue()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig(),
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
            };
            config.Statistics.Add(new BestMaximumFitnessStatisticFactoryConfig());

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            
            BestMaximumFitnessStatistic target = new BestMaximumFitnessStatistic(algorithm);

            SimplePopulation population1 = new SimplePopulation(algorithm);
            PrivateObject accessor1 = new PrivateObject(population1, new PrivateType(typeof(PopulationBase<SimplePopulation, SimplePopulationFactoryConfig>)));

            double expectedValue1 = 3;
            accessor1.SetField("scaledMax", expectedValue1);
            object actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue2 = 2;
            accessor1.SetField("scaledMax", expectedValue2);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue3 = 4;
            accessor1.SetField("scaledMax", expectedValue3);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            SimplePopulation population2 = new SimplePopulation(algorithm);
            PrivateObject accessor2 = new PrivateObject(population2, new PrivateType(typeof(PopulationBase<SimplePopulation, SimplePopulationFactoryConfig>)));
            accessor2.SetField("index", 1);

            double expectedValue4 = 7;
            accessor2.SetField("scaledMax", expectedValue4);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue4, actualValue);

            // Check that the first populations max hasn't changed
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue4, actualValue);

            double expectedValue5 = 9;
            accessor2.SetField("scaledMax", expectedValue5);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue5, actualValue);
        }

        /// <summary>
        /// Tests that the correct value is returned from BestMaxStatistic.GetResultValue when every
        /// entity has a negative fitness value.
        /// </summary>
        [TestMethod()]
        public void GetResultValue_AllNegative()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig()
            };
            config.Statistics.Add(new BestMaximumFitnessStatisticFactoryConfig());

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            
            BestMaximumFitnessStatistic target = new BestMaximumFitnessStatistic(algorithm);

            SimplePopulation population1 = new SimplePopulation(algorithm);
            PrivateObject accessor1 = new PrivateObject(population1, new PrivateType(typeof(PopulationBase<SimplePopulation, SimplePopulationFactoryConfig>)));

            double expectedValue1 = -7;
            accessor1.SetField("scaledMax", expectedValue1);
            object actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue2 = -8;
            accessor1.SetField("scaledMax", expectedValue2);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue3 = -6;
            accessor1.SetField("scaledMax", expectedValue3);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            SimplePopulation population2 = new SimplePopulation(algorithm);
            PrivateObject accessor2 = new PrivateObject(population2, new PrivateType(typeof(PopulationBase<SimplePopulation, SimplePopulationFactoryConfig>)));
            accessor2.SetField("index", 1);

            double expectedValue4 = -5;
            accessor2.SetField("scaledMax", expectedValue4);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue4, actualValue);

            // Check that the first populations max hasn't changed
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue4, actualValue);

            double expectedValue5 = -4;
            accessor2.SetField("scaledMax", expectedValue5);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue5, actualValue);
        }

        /// <summary>
        /// Tests that an exception will be thrown when a null population1 is passed to BestMaxStatistic.GetResultValue.
        /// </summary>
        [TestMethod()]
        public void GetResultValue_NullPopulation()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
            };
            config.Statistics.Add(new BestMaximumFitnessStatisticFactoryConfig());

            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            
            BestMaximumFitnessStatistic target = new BestMaximumFitnessStatistic(algorithm);

            AssertEx.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }
    }
}
