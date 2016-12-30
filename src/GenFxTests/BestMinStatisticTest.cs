using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Statistics;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Statistics.BestMinStatistic and is intended
    ///to contain all GenFx.ComponentLibrary.Statistics.BestMinStatistic Unit Tests
    ///</summary>
    [TestClass()]
    public class BestMinStatisticTest
    {
        /// <summary>
        /// Tests that the correct value is returned from BestMinStatistic.GetResultValue.
        /// </summary>
        [TestMethod()]
        public void GetResultValue()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Entity = new MockEntityConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                Population = new SimplePopulationConfiguration()
            };
            config.Statistics.Add(new BestMinimumFitnessStatisticConfiguration());

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            
            BestMinimumFitnessStatistic target = new BestMinimumFitnessStatistic(algorithm);

            SimplePopulation population1 = new SimplePopulation(algorithm);
            PrivateObject accessor1 = new PrivateObject(population1, new PrivateType(typeof(PopulationBase<SimplePopulation, SimplePopulationConfiguration>)));

            double expectedValue1 = 3;
            accessor1.SetField("scaledMin", expectedValue1);
            object actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue2 = 4;
            accessor1.SetField("scaledMin", expectedValue2);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue3 = 2;
            accessor1.SetField("scaledMin", expectedValue3);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            SimplePopulation population2 = new SimplePopulation(algorithm);
            PrivateObject accessor2 = new PrivateObject(population2, new PrivateType(typeof(PopulationBase<SimplePopulation, SimplePopulationConfiguration>)));
            accessor2.SetField("index",  1);

            double expectedValue4 = 8;
            accessor2.SetField("scaledMin", expectedValue4);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue4, actualValue);

            // check that the result of the first population is the same
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            double expectedValue5 = 2;
            accessor2.SetField("scaledMin", expectedValue5);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue5, actualValue);
        }

        /// <summary>
        /// Tests that the correct value is returned from BestMinStatistic.GetResultValue when every
        /// entity has a negative fitness value.
        /// </summary>
        [TestMethod()]
        public void GetResultValue_AllNegative()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Population = new SimplePopulationConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                Entity = new MockEntityConfiguration()
            };
            config.Statistics.Add(new BestMinimumFitnessStatisticConfiguration());

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            BestMinimumFitnessStatistic target = new BestMinimumFitnessStatistic(algorithm);

            SimplePopulation population1 = new SimplePopulation(algorithm);
            PrivateObject accessor1 = new PrivateObject(population1, new PrivateType(typeof(PopulationBase<SimplePopulation, SimplePopulationConfiguration>)));

            double expectedValue1 = -7;
            accessor1.SetField("scaledMin", expectedValue1);
            object actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue2 = -6;
            accessor1.SetField("scaledMin", expectedValue2);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue3 = -8;
            accessor1.SetField("scaledMin", expectedValue3);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            SimplePopulation population2 = new SimplePopulation(algorithm);
            PrivateObject accessor2 = new PrivateObject(population2, new PrivateType(typeof(PopulationBase<SimplePopulation, SimplePopulationConfiguration>)));
            accessor2.SetField("index", 1);

            double expectedValue4 = -9;
            accessor2.SetField("scaledMin", expectedValue4);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue4, actualValue);

            // check that the result of the first population is the same
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            double expectedValue5 = -10;
            accessor2.SetField("scaledMin", expectedValue5);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue5, actualValue);
        }

        /// <summary>
        /// Tests that an exception will be thrown when a null population1 is passed to BestMinStatistic.GetResultValue.
        /// </summary>
        [TestMethod()]
        public void GetResultValue_NullPopulation()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
            };
            config.Statistics.Add(new BestMinimumFitnessStatisticConfiguration());

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            BestMinimumFitnessStatistic target = new BestMinimumFitnessStatistic(algorithm);

            AssertEx.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }
    }
}
