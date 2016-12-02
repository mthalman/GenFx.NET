using GenFx;
using GenFx.ComponentLibrary.Statistics;
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
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new BestMinimumFitnessStatisticConfiguration());
            BestMinimumFitnessStatistic target = new BestMinimumFitnessStatistic(algorithm);

            Population population1 = new Population(algorithm);
            PrivateObject accessor1 = new PrivateObject(population1);

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

            Population population2 = new Population(algorithm);
            PrivateObject accessor2 = new PrivateObject(population2);
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
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new BestMinimumFitnessStatisticConfiguration());
            BestMinimumFitnessStatistic target = new BestMinimumFitnessStatistic(algorithm);

            Population population1 = new Population(algorithm);
            PrivateObject accessor1 = new PrivateObject(population1);

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

            Population population2 = new Population(algorithm);
            PrivateObject accessor2 = new PrivateObject(population2);
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetResultValue_NullPopulation()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new BestMinimumFitnessStatisticConfiguration());
            BestMinimumFitnessStatistic target = new BestMinimumFitnessStatistic(algorithm);

            target.GetResultValue(null);
        }
    }
}
