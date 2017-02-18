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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
            };
            algorithm.Statistics.Add(new MinimumFitnessStatistic());

            MinimumFitnessStatistic target = new MinimumFitnessStatistic();
            target.Initialize(algorithm);
            SimplePopulation population = new SimplePopulation();
            population.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(population, new PrivateType(typeof(Population)));
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
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
            };
            algorithm.Statistics.Add(new MinimumFitnessStatistic());

            MinimumFitnessStatistic target = new MinimumFitnessStatistic();
            target.Initialize(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }
    }
}
