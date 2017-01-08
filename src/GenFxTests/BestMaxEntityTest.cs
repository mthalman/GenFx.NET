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
            config.Statistics.Add(new BestMaximumFitnessEntityStatisticFactoryConfig());
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            BestMaximumFitnessEntityStatistic target = new BestMaximumFitnessEntityStatistic(algorithm);

            AssertEx.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }

        /// <summary>
        /// Tests that the correct value is returned from BestMaxEntity.GetResultValue.
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
            config.Statistics.Add(new BestMaximumFitnessEntityStatisticFactoryConfig());

            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            BestMaximumFitnessEntityStatistic target = new BestMaximumFitnessEntityStatistic(algorithm);

            SimplePopulation population1 = new SimplePopulation(algorithm)
            {
                Index = 0
            };

            VerifyGetResultValue(2, target, population1, algorithm, "20");
            VerifyGetResultValue(1, target, population1, algorithm, "20");
            VerifyGetResultValue(3, target, population1, algorithm, "30");

            SimplePopulation population2 = new SimplePopulation(algorithm)
            {
                Index = 1
            };
            VerifyGetResultValue(7, target, population2, algorithm, "70");
            VerifyGetResultValue(1, target, population1, algorithm, "30");
            VerifyGetResultValue(4, target, population2, algorithm, "70");
        }
        
        private static void VerifyGetResultValue(int multiplier, BestMaximumFitnessEntityStatistic stat, SimplePopulation population, IGeneticAlgorithm algorithm, string expectedReturnVal)
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

            Assert.AreEqual(expectedReturnVal, representation.ToString());
        }
    }
}
