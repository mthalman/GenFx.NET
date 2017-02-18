using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Statistics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestCommon.Helpers;
using TestCommon.Mocks;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="StandardDeviationFitnessStatistic"/> class.
    ///</summary>
    [TestClass]
    public class StandardDeviationFitnessStatisticTest
    {

        /// <summary>
        /// Tests that the GetResultValue method works correctly.
        /// </summary>
        [TestMethod]
        public void StdDevStatistic_GetResultValue()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };
            algorithm.Statistics.Add(new StandardDeviationFitnessStatistic());

            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(population, new PrivateType(typeof(Population)));
            accessor.SetField("scaledStandardDeviation", 1234);

            StandardDeviationFitnessStatistic stat = new StandardDeviationFitnessStatistic();
            stat.Initialize(algorithm);
            object result = stat.GetResultValue(population);

            Assert.AreEqual(population.ScaledStandardDeviation, result, "Incorrect result returned.");
        }

        /// <summary>
        /// Tests that an exception is throw when a null population is passed.
        /// </summary>
        [TestMethod]
        public void StdDevStatistic_GetResultValue_NullPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };
            algorithm.Statistics.Add(new StandardDeviationFitnessStatistic());

            StandardDeviationFitnessStatistic stat = new StandardDeviationFitnessStatistic();
            stat.Initialize(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => stat.GetResultValue(null));
        }
    }
}
