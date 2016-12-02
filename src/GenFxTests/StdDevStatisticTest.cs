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
    ///This is a test class for GenFx.ComponentLibrary.Statistics.StdDevStatistic and is intended
    ///to contain all GenFx.ComponentLibrary.Statistics.StdDevStatistic Unit Tests
    ///</summary>
    [TestClass()]
    public class StdDevStatisticTest
    {

        /// <summary>
        /// Tests that the GetResultValue method works correctly.
        /// </summary>
        [TestMethod]
        public void StdDevStatistic_GetResultValue()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new StandardDeviationFitnessStatisticConfiguration());
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            Population population = new Population(algorithm);
            PrivateObject accessor = new PrivateObject(population);
            accessor.SetField("scaledStandardDeviation", 1234);

            StandardDeviationFitnessStatistic stat = new StandardDeviationFitnessStatistic(algorithm);
            object result = stat.GetResultValue(population);

            Assert.AreEqual(population.ScaledStandardDeviation, result, "Incorrect result returned.");
        }

        /// <summary>
        /// Tests that an exception is throw when a null population is passed.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StdDevStatistic_GetResultValue_NullPopulation()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new StandardDeviationFitnessStatisticConfiguration());
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            StandardDeviationFitnessStatistic stat = new StandardDeviationFitnessStatistic(algorithm);
            stat.GetResultValue(null);
        }

    }
}
