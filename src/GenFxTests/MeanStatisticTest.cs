﻿using GenFx;
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
    /// This is a test class for GenFx.ComponentLibrary.Statistics.MeanStatistic and is intended
    /// to contain all GenFx.ComponentLibrary.Statistics.MeanStatistic Unit Tests
    /// </summary>
    [TestClass()]
    public class MeanStatisticTest
    {
        /// <summary>
        /// Tests that the GetResultValue method works correctly.
        /// </summary>
        [TestMethod()]
        public void MeanStatistic_GetResultValue()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
            };
            config.Statistics.Add(new MeanFitnessStatisticFactoryConfig());

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);

            MeanFitnessStatistic target = new MeanFitnessStatistic(algorithm);
            SimplePopulation population = new SimplePopulation(algorithm);
            PrivateObject accessor = new PrivateObject(population, new PrivateType(typeof(PopulationBase<SimplePopulation, SimplePopulationFactoryConfig>)));
            accessor.SetField("scaledMean", 21);
            object result = target.GetResultValue(population);

            Assert.AreEqual(population.ScaledMean, result, "Incorrect result value.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null population is passed.
        /// </summary>
        [TestMethod()]
        public void MeanStatistic_GetResultValue_NullPopulation()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new SimplePopulationFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
            };
            config.Statistics.Add(new MeanFitnessStatisticFactoryConfig());

            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);
            MeanFitnessStatistic target = new MeanFitnessStatistic(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }
    }
}
