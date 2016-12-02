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
    /// This is a test class for GenFx.ComponentLibrary.Statistics.MaxStatistic and is intended
    /// to contain all GenFx.ComponentLibrary.Statistics.MaxStatistic Unit Tests
    /// </summary>
    [TestClass()]
    public class MaxStatisticTest
    {
        /// <summary>
        /// Tests that the GetResultValue method works correctly.
        /// </summary>
        [TestMethod()]
        public void MaxStatistic_GetResultValue()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new MaximumFitnessStatisticConfiguration());
            MaximumFitnessStatistic target = new MaximumFitnessStatistic(algorithm);
            Population population = new Population(algorithm);
            PrivateObject accessor = new PrivateObject(population);
            accessor.SetField("scaledMax", 21);
            object result = target.GetResultValue(population);

            Assert.AreEqual(population.ScaledMax, result, "Incorrect result value.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MaxStatistic_GetResultValue_NullPopulation()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new MaximumFitnessStatisticConfiguration());
            MaximumFitnessStatistic target = new MaximumFitnessStatistic(algorithm);
            object result = target.GetResultValue(null);
        }
    }
}
