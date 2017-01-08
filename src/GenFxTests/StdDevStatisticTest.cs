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
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
            };
            config.Statistics.Add(new StandardDeviationFitnessStatisticFactoryConfig());

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            SimplePopulation population = new SimplePopulation(algorithm);
            PrivateObject accessor = new PrivateObject(population, new PrivateType(typeof(PopulationBase<SimplePopulation, SimplePopulationFactoryConfig>)));
            accessor.SetField("scaledStandardDeviation", 1234);

            StandardDeviationFitnessStatistic stat = new StandardDeviationFitnessStatistic(algorithm);
            object result = stat.GetResultValue(population);

            Assert.AreEqual(population.ScaledStandardDeviation, result, "Incorrect result returned.");
        }

        /// <summary>
        /// Tests that an exception is throw when a null population is passed.
        /// </summary>
        [TestMethod]
        public void StdDevStatistic_GetResultValue_NullPopulation()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
            };
            config.Statistics.Add(new StandardDeviationFitnessStatisticFactoryConfig());

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            StandardDeviationFitnessStatistic stat = new StandardDeviationFitnessStatistic(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => stat.GetResultValue(null));
        }
    }
}
