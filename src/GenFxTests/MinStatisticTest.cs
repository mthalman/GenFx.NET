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
    /// This is a test class for GenFx.ComponentLibrary.Statistics.MinStatistic and is intended
    /// to contain all GenFx.ComponentLibrary.Statistics.MinStatistic Unit Tests
    /// </summary>
    [TestClass()]
    public class MinStatisticTest
    {
        /// <summary>
        /// Tests that the GetResultValue method works correctly.
        /// </summary>
        [TestMethod()]
        public void MinStatistic_GetResultValue()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new SimplePopulationConfiguration(),
            };
            config.Statistics.Add(new MinimumFitnessStatisticConfiguration());

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            MinimumFitnessStatistic target = new MinimumFitnessStatistic(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);
            PrivateObject accessor = new PrivateObject(population, new PrivateType(typeof(PopulationBase<SimplePopulation, SimplePopulationConfiguration>)));
            accessor.SetField("scaledMin", 21);
            object result = target.GetResultValue(population);

            Assert.AreEqual(population.ScaledMin, result, "Incorrect result value.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod()]
        public void MinStatistic_GetResultValue_NullPopulation()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new SimplePopulationConfiguration(),
            };
            config.Statistics.Add(new MinimumFitnessStatisticConfiguration());

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            MinimumFitnessStatistic target = new MinimumFitnessStatistic(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }
    }
}
