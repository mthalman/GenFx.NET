using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFx.ComponentLibrary.Statistics;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenFx.ComponentLibrary.Tests
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };
            algorithm.Statistics.Add(new MaximumFitnessStatistic());

            MaximumFitnessStatistic target = new MaximumFitnessStatistic();
            target.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(population, new PrivateType(typeof(Population)));
            accessor.SetField("scaledMax", 21);
            object result = target.GetResultValue(population);

            Assert.AreEqual(population.ScaledMax, result, "Incorrect result value.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod()]
        public void MaxStatistic_GetResultValue_NullPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };
            algorithm.Statistics.Add(new MaximumFitnessStatistic());

            MaximumFitnessStatistic target = new MaximumFitnessStatistic();
            target.Initialize(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }
    }
}
