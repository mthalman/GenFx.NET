using GenFx;
using GenFx.ComponentModel;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.Statistic and is intended
    ///to contain all GenFx.Statistic Unit Tests
    ///</summary>
    [TestClass()]
    public class StatisticTest
    {
        /// <summary>
        /// Tests that the constructor correctly initializes the state.
        /// </summary>
        [TestMethod]
        public void Statistic_Ctor()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new MockStatisticConfiguration());
            Statistic stat = new MockStatistic(algorithm);
            PrivateObject accessor = new PrivateObject(stat, new PrivateType(typeof(Statistic)));
            Assert.AreSame(accessor.GetProperty("Algorithm"), algorithm, "Algorithm not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void Statistic_Ctor_NullAlgorithm()
        {
            AssertEx.Throws<ArgumentNullException>(() => new MockStatistic(null));
        }

        /// <summary>
        /// Tests that an exception is thrown when a required config class is missing.
        /// </summary>
        [TestMethod]
        public void Statistic_Ctor_MissingConfig()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            AssertEx.Throws<ArgumentException>(() => new TestStat(algorithm));
        }

        /// <summary>
        /// Tests that the Calculate method works correctly.
        /// </summary>
        [TestMethod]
        public void Statistic_Calculate()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.GeneticAlgorithm = new MockGeneticAlgorithmConfiguration();
            algorithm.ConfigurationSet.GeneticAlgorithm.EnvironmentSize = 2;
            algorithm.ConfigurationSet.Population = new PopulationConfiguration();
            algorithm.ConfigurationSet.Statistics.Add(new FakeStatisticConfiguration());
            FakeStatistic stat = new FakeStatistic(algorithm);
            algorithm.Environment.Populations.Add(new Population(algorithm));
            PrivateObject popAccessor = new PrivateObject(algorithm.Environment.Populations[0]);
            popAccessor.SetField("index", 0);
            algorithm.Environment.Populations.Add(new Population(algorithm));
            popAccessor = new PrivateObject(algorithm.Environment.Populations[1]);
            popAccessor.SetField("index", 1);
            stat.Calculate(algorithm.Environment, 1);

            Assert.AreEqual(2, stat.GetResultValueCallCount, "Statistic not called correctly.");

            ObservableCollection<StatisticResult> results = stat.GetResults(0);
            Assert.AreEqual(1, results.Count, "Incorrect number of results.");
            Assert.AreEqual(1, results[0].GenerationIndex, "Result's GenerationIndex not set correctly.");
            Assert.AreEqual(0, results[0].PopulationId, "Result's PopulationId not set correctly.");
            Assert.AreEqual(1, results[0].ResultValue, "Result's ResultValue not set correctly.");
            Assert.AreSame(stat, results[0].Statistic, "Result's Statistic not set correctly.");

            results = stat.GetResults(1);
            Assert.AreEqual(1, results[0].GenerationIndex, "Result's GenerationIndex not set correctly.");
            Assert.AreEqual(1, results[0].PopulationId, "Result's PopulationId not set correctly.");
            Assert.AreEqual(2, results[0].ResultValue, "Result's ResultValue not set correctly.");
            Assert.AreSame(stat, results[0].Statistic, "Result's Statistic not set correctly.");
        }

        private class FakeStatistic : Statistic
        {
            internal int GetResultValueCallCount;

            public FakeStatistic(GeneticAlgorithm algorithm)
                : base(algorithm)
            {

            }

            public override object GetResultValue(Population population)
            {
                this.GetResultValueCallCount++;
                return this.GetResultValueCallCount;
            }
        }

        [Component(typeof(FakeStatistic))]
        private class FakeStatisticConfiguration : StatisticConfiguration
        {
        }

        private class TestStat : Statistic
        {
            public TestStat(GeneticAlgorithm algorithm)
                : base(algorithm)
            {

            }

            public override object GetResultValue(Population population)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [Component(typeof(TestStat))]
        private class TestStatConfiguration : StatisticConfiguration
        {
        }
    }
}
