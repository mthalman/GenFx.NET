using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Populations;
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
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
            };
            config.Statistics.Add(new MockStatisticConfiguration());

            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);

            IStatistic stat = new MockStatistic(algorithm);
            PrivateObject accessor = new PrivateObject(stat, new PrivateType(typeof(StatisticBase<MockStatistic, MockStatisticConfiguration>)));
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
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration(),
                Entity = new MockEntityConfiguration(),
                Population = new MockPopulationConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
            });
            AssertEx.Throws<InvalidOperationException>(() => new TestStat(algorithm));
        }

        /// <summary>
        /// Tests that the Calculate method works correctly.
        /// </summary>
        [TestMethod]
        public void Statistic_Calculate()
        {
            ComponentConfigurationSet config = new ComponentConfigurationSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmConfiguration
                {
                    EnvironmentSize = 2
                },
                Entity = new MockEntityConfiguration(),
                FitnessEvaluator = new MockFitnessEvaluatorConfiguration(),
                SelectionOperator = new MockSelectionOperatorConfiguration(),
                Population = new SimplePopulationConfiguration()
            };
            config.Statistics.Add(new FakeStatisticConfiguration());

            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);

            FakeStatistic stat = new FakeStatistic(algorithm);
            algorithm.Environment.Populations.Add(new SimplePopulation(algorithm));
            algorithm.Environment.Populations[0].Index = 0;
            algorithm.Environment.Populations.Add(new SimplePopulation(algorithm));
            algorithm.Environment.Populations[1].Index = 1;
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

        private class FakeStatistic : StatisticBase<FakeStatistic, FakeStatisticConfiguration>
        {
            internal int GetResultValueCallCount;

            public FakeStatistic(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {

            }

            public override object GetResultValue(IPopulation population)
            {
                this.GetResultValueCallCount++;
                return this.GetResultValueCallCount;
            }
        }

        private class FakeStatisticConfiguration : StatisticConfigurationBase<FakeStatisticConfiguration, FakeStatistic>
        {
        }

        private class TestStat : StatisticBase<TestStat, TestStatConfiguration>
        {
            public TestStat(IGeneticAlgorithm algorithm)
                : base(algorithm)
            {

            }

            public override object GetResultValue(IPopulation population)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        private class TestStatConfiguration : StatisticConfigurationBase<TestStatConfiguration, TestStat>
        {
        }
    }
}
