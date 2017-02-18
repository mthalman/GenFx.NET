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
    ///This is a test class for GenFx.ComponentLibrary.Statistics.BestMaxStatistic and is intended
    ///to contain all GenFx.ComponentLibrary.Statistics.BestMaxStatistic Unit Tests
    ///</summary>
    [TestClass()]
    public class BestMaxStatisticTest
    {
        /// <summary>
        /// Tests that the correct value is returned from BestMaxStatistic.GetResultValue.
        /// </summary>
        [TestMethod()]
        public void GetResultValue()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation(),
            };
            algorithm.Statistics.Add(new BestMaximumFitnessStatistic());

            BestMaximumFitnessStatistic target = new BestMaximumFitnessStatistic();
            target.Initialize(algorithm);

            SimplePopulation population1 = new SimplePopulation();
            population1.Initialize(algorithm);
            PrivateObject accessor1 = new PrivateObject(population1, new PrivateType(typeof(Population)));

            double expectedValue1 = 3;
            accessor1.SetField("scaledMax", expectedValue1);
            object actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue2 = 2;
            accessor1.SetField("scaledMax", expectedValue2);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue3 = 4;
            accessor1.SetField("scaledMax", expectedValue3);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            SimplePopulation population2 = new SimplePopulation();
            population2.Initialize(algorithm);
            PrivateObject accessor2 = new PrivateObject(population2, new PrivateType(typeof(Population)));
            accessor2.SetField("index", 1);

            double expectedValue4 = 7;
            accessor2.SetField("scaledMax", expectedValue4);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue4, actualValue);

            // Check that the first populations max hasn't changed
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue4, actualValue);

            double expectedValue5 = 9;
            accessor2.SetField("scaledMax", expectedValue5);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue5, actualValue);
        }

        /// <summary>
        /// Tests that the correct value is returned from BestMaxStatistic.GetResultValue when every
        /// entity has a negative fitness value.
        /// </summary>
        [TestMethod()]
        public void GetResultValue_AllNegative()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new SimplePopulation()
            };
            algorithm.Statistics.Add(new BestMaximumFitnessStatistic());

            BestMaximumFitnessStatistic target = new BestMaximumFitnessStatistic();
            target.Initialize(algorithm);

            SimplePopulation population1 = new SimplePopulation();
            population1.Initialize(algorithm);
            PrivateObject accessor1 = new PrivateObject(population1, new PrivateType(typeof(Population)));

            double expectedValue1 = -7;
            accessor1.SetField("scaledMax", expectedValue1);
            object actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue2 = -8;
            accessor1.SetField("scaledMax", expectedValue2);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue1, actualValue);

            double expectedValue3 = -6;
            accessor1.SetField("scaledMax", expectedValue3);
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            SimplePopulation population2 = new SimplePopulation();
            population2.Initialize(algorithm);
            PrivateObject accessor2 = new PrivateObject(population2, new PrivateType(typeof(Population)));
            accessor2.SetField("index", 1);

            double expectedValue4 = -5;
            accessor2.SetField("scaledMax", expectedValue4);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue4, actualValue);

            // Check that the first populations max hasn't changed
            actualValue = target.GetResultValue(population1);
            Assert.AreEqual(expectedValue3, actualValue);

            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue4, actualValue);

            double expectedValue5 = -4;
            accessor2.SetField("scaledMax", expectedValue5);
            actualValue = target.GetResultValue(population2);
            Assert.AreEqual(expectedValue5, actualValue);
        }

        /// <summary>
        /// Tests that an exception will be thrown when a null population1 is passed to BestMaxStatistic.GetResultValue.
        /// </summary>
        [TestMethod()]
        public void GetResultValue_NullPopulation()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                SelectionOperator = new MockSelectionOperator(),
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };
            algorithm.Statistics.Add(new BestMaximumFitnessStatistic());

            BestMaximumFitnessStatistic target = new BestMaximumFitnessStatistic();
            target.Initialize(algorithm);

            AssertEx.Throws<ArgumentNullException>(() => target.GetResultValue(null));
        }

        /// <summary>
        /// Tests that the component can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void BestMaximumFitnessStatistic_Serialization()
        {
            BestMaximumFitnessStatistic stat = new BestMaximumFitnessStatistic();
            PrivateObject privObj = new PrivateObject(stat);
            Dictionary<int, double> bestMaxValues = (Dictionary<int, double>)privObj.GetField("bestMaxValues");
            bestMaxValues.Add(10, 2.4);

            BestMaximumFitnessStatistic result = (BestMaximumFitnessStatistic)SerializationHelper.TestSerialization(stat, new Type[] { typeof(MockEntity) });

            PrivateObject resultPrivObj = new PrivateObject(result);
            Dictionary<int, double> resultBestMaxValues = (Dictionary<int, double>)resultPrivObj.GetField("bestMaxValues");
            Assert.AreEqual(2.4, resultBestMaxValues[10]);

        }
    }
}
