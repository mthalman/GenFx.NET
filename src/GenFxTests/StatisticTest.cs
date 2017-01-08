using GenFx;
using GenFx.ComponentLibrary.Base;
using GenFx.ComponentLibrary.Populations;
using GenFx.Contracts;
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
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
            };
            config.Statistics.Add(new MockStatisticFactoryConfig());

            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(config);

            IStatistic stat = new MockStatistic(algorithm);
            PrivateObject accessor = new PrivateObject(stat, new PrivateType(typeof(StatisticBase<MockStatistic, MockStatisticFactoryConfig>)));
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
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                Entity = new MockEntityFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
            });
            AssertEx.Throws<InvalidOperationException>(() => new TestStat(algorithm));
        }

        /// <summary>
        /// Tests that the Calculate method works correctly.
        /// </summary>
        [TestMethod]
        public void Statistic_Calculate()
        {
            ComponentFactoryConfigSet config = new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig
                {
                    EnvironmentSize = 2
                },
                Entity = new MockEntityFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                Population = new SimplePopulationFactoryConfig()
            };
            config.Statistics.Add(new FakeStatisticFactoryConfig());

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

        private class FakeStatistic : StatisticBase<FakeStatistic, FakeStatisticFactoryConfig>
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

        private class FakeStatisticFactoryConfig : StatisticFactoryConfigBase<FakeStatisticFactoryConfig, FakeStatistic>
        {
        }

        private class TestStat : StatisticBase<TestStat, TestStatFactoryConfig>
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

        private class TestStatFactoryConfig : StatisticFactoryConfigBase<TestStatFactoryConfig, TestStat>
        {
        }
    }
}
