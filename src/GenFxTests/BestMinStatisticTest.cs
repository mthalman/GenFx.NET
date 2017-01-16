using GenFx;
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
    ///This is a test class for GenFx.ComponentLibrary.Statistics.BestMinStatistic and is intended
    ///to contain all GenFx.ComponentLibrary.Statistics.BestMinStatistic Unit Tests
    ///</summary>
    [TestClass()]
    public class BestMinStatisticTest
    {
        /// <summary>
        /// Tests that the correct value is returned from BestMinStatistic.GetResultValue.
        /// </summary>
        [TestMethod()]
        public void GetResultValue()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                SelectionOperator = new MockSelectionOperator(),
                PopulationSeed = new SimplePopulation()
            };
            algorithm.Statistics.Add(new BestMinimumFitnessStatistic());

            BestMinimumFitnessStatistic target = new BestMinimumFitnessStatistic();
            target.Initialize(algorithm);

            SimplePopulation population1 = new SimplePopulation();
            population1.Initialize(algorithm);
            PrivateObject accessor1 = new PrivateObject(population1, new PrivateType(typeof(Population)));

            double expectedValue1 = 3;
            accessor1.SetField("scaledMin", expectedValue1);
            object actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue2 = 4;
            accessor1.SetField("scaledMin", expectedValue2);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue3 = 2;
            accessor1.SetField("scaledMin", expectedValue3);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            SimplePopulation population2 = new SimplePopulation();
            population2.Initialize(algorithm);
            PrivateObject accessor2 = new PrivateObject(population2, new PrivateType(typeof(Population)));
            accessor2.SetField("index",  1);

            double expectedValue4 = 8;
            accessor2.SetField("scaledMin", expectedValue4);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue4, actualValue);

            // check that the result of the first population is the same
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            double expectedValue5 = 2;
            accessor2.SetField("scaledMin", expectedValue5);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue5, actualValue);
        }

        /// <summary>
        /// Tests that the correct value is returned from BestMinStatistic.GetResultValue when every
        /// entity has a negative fitness value.
        /// </summary>
        [TestMethod()]
        public void GetResultValue_AllNegative()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new SimplePopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity()
            };
            algorithm.Statistics.Add(new BestMinimumFitnessStatistic());

            BestMinimumFitnessStatistic target = new BestMinimumFitnessStatistic();
            target.Initialize(algorithm);

            SimplePopulation population1 = new SimplePopulation();
            population1.Initialize(algorithm);
            PrivateObject accessor1 = new PrivateObject(population1, new PrivateType(typeof(Population)));

            double expectedValue1 = -7;
            accessor1.SetField("scaledMin", expectedValue1);
            object actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue2 = -6;
            accessor1.SetField("scaledMin", expectedValue2);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue3 = -8;
            accessor1.SetField("scaledMin", expectedValue3);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            SimplePopulation population2 = new SimplePopulation();
            population2.Initialize(algorithm);
            PrivateObject accessor2 = new PrivateObject(population2, new PrivateType(typeof(Population)));
            accessor2.SetField("index", 1);

            double expectedValue4 = -9;
            accessor2.SetField("scaledMin", expectedValue4);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue4, actualValue);

            // check that the result of the first population is the same
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            double expectedValue5 = -10;
            accessor2.SetField("scaledMin", expectedValue5);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue5, actualValue);
        }

        /// <summary>
        /// Tests that an exception will be thrown when a null population1 is passed to BestMinStatistic.GetResultValue.
        /// </summary>
        [TestMethod()]
        public void GetResultValue_NullPopulation()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };
            algorithm.Statistics.Add(new BestMinimumFitnessStatistic());

            BestMinimumFitnessStatistic target = new BestMinimumFitnessStatistic();
            target.Initialize(algorithm);

            AssertEx.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }
    }
}
