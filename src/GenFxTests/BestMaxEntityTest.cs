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
    ///This is a test class for GenFx.ComponentLibrary.Statistics.BestMaxEntity and is intended
    ///to contain all GenFx.ComponentLibrary.Statistics.BestMaxEntity Unit Tests
    ///</summary>
    [TestClass()]
    public class BestMaxEntityTest
    {
        /// <summary>
        /// Tests that an exception will be thrown when a null population is passed to BestMaxEntity.GetResultValue.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetResultValue_NullPopulation()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new BestMaximumFitnessEntityStatisticConfiguration());
            BestMaximumFitnessEntityStatistic target = new BestMaximumFitnessEntityStatistic(algorithm);

            target.GetResultValue(null);
        }

        /// <summary>
        /// Tests that the correct value is returned from BestMaxEntity.GetResultValue.
        /// </summary>
        [TestMethod()]
        public void GetResultValue()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new MockEntityConfiguration();
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new BestMaximumFitnessEntityStatisticConfiguration());
            BestMaximumFitnessEntityStatistic target = new BestMaximumFitnessEntityStatistic(algorithm);

            Population population1 = new Population(algorithm);

            VerifyGetResultValue(2, target, population1, algorithm, "20");
            VerifyGetResultValue(1, target, population1, algorithm, "20");
            VerifyGetResultValue(3, target, population1, algorithm, "30");

            Population population2 = new Population(algorithm);
            VerifyGetResultValue(7, target, population2, algorithm, "70");
            VerifyGetResultValue(1, target, population1, algorithm, "30");
            VerifyGetResultValue(4, target, population2, algorithm, "70");
        }
        
        private static void VerifyGetResultValue(int multiplier, BestMaximumFitnessEntityStatistic stat, Population population, GeneticAlgorithm algorithm, string expectedReturnVal)
        {
            for (int i = 0; i < 5; i++)
            {
                MockEntity entity = new MockEntity(algorithm);
                entity.ScaledFitnessValue = i * multiplier;
                entity.Identifier = entity.ScaledFitnessValue.ToString();
                population.Entities.Add(entity);
            }

            for (int i = 10; i >= 5; i--)
            {
                MockEntity entity = new MockEntity(algorithm);
                entity.ScaledFitnessValue = i * multiplier;
                entity.Identifier = entity.ScaledFitnessValue.ToString();
                population.Entities.Add(entity);
            }

            object representation = stat.GetResultValue(population);

            Assert.AreEqual(expectedReturnVal, representation);
        }
    }
}
