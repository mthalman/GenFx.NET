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
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new BestMaximumFitnessStatisticConfiguration());
            BestMaximumFitnessStatistic target = new BestMaximumFitnessStatistic(algorithm);

            Population population1 = new Population(algorithm);
            PrivateObject accessor1 = new PrivateObject(population1);

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

            Population population2 = new Population(algorithm);
            PrivateObject accessor2 = new PrivateObject(population2);
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
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new BestMaximumFitnessStatisticConfiguration());
            BestMaximumFitnessStatistic target = new BestMaximumFitnessStatistic(algorithm);

            Population population1 = new Population(algorithm);
            PrivateObject accessor1 = new PrivateObject(population1);

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

            Population population2 = new Population(algorithm);
            PrivateObject accessor2 = new PrivateObject(population2);
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetResultValue_NullPopulation()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new BestMaximumFitnessStatisticConfiguration());
            BestMaximumFitnessStatistic target = new BestMaximumFitnessStatistic(algorithm);

            target.GetResultValue(null);
        }
    }
}
