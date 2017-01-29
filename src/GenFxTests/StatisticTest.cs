using GenFx;
using GenFx.ComponentLibrary.Populations;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                GeneticEntitySeed = new MockEntity(),
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
            };

            algorithm.Statistics.Add(new MockStatistic());

            Statistic stat = new MockStatistic();
            stat.Initialize(algorithm);
            PrivateObject accessor = new PrivateObject(stat, new PrivateType(typeof(Statistic)));
            Assert.AreSame(accessor.GetProperty("Algorithm"), algorithm, "Algorithm not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null algorithm is passed.
        /// </summary>
        [TestMethod]
        public void Statistic_Ctor_NullAlgorithm()
        {
            MockStatistic stat = new MockStatistic();
            AssertEx.Throws<ArgumentNullException>(() => stat.Initialize(null));
        }

        /// <summary>
        /// Tests that the Calculate method works correctly.
        /// </summary>
        [TestMethod]
        public async Task Statistic_Calculate()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                MinimumEnvironmentSize = 2,
                GeneticEntitySeed = new MockEntity(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                SelectionOperator = new MockSelectionOperator(),
                PopulationSeed = new SimplePopulation()
            };

            algorithm.Statistics.Add(new FakeStatistic());

            await algorithm.InitializeAsync();
            
            FakeStatistic stat = (FakeStatistic)algorithm.Statistics[0];
            stat.Calculate(algorithm.Environment, 1);

            Assert.AreEqual(4, stat.GetResultValueCallCount, "Statistic not called correctly.");

            ObservableCollection<StatisticResult> results = stat.GetResults(0);
            Assert.AreEqual(2, results.Count, "Incorrect number of results.");
            Assert.AreEqual(0, results[0].GenerationIndex, "Result's GenerationIndex not set correctly.");
            Assert.AreEqual(0, results[0].PopulationId, "Result's PopulationId not set correctly.");
            Assert.AreEqual(1, results[0].ResultValue, "Result's ResultValue not set correctly.");
            Assert.AreSame(stat, results[0].Statistic, "Result's Statistic not set correctly.");

            results = stat.GetResults(1);
            Assert.AreEqual(0, results[0].GenerationIndex, "Result's GenerationIndex not set correctly.");
            Assert.AreEqual(1, results[0].PopulationId, "Result's PopulationId not set correctly.");
            Assert.AreEqual(2, results[0].ResultValue, "Result's ResultValue not set correctly.");
            Assert.AreSame(stat, results[0].Statistic, "Result's Statistic not set correctly.");
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void Serialization()
        {
            MockStatistic stat = new MockStatistic();

            PrivateObject privObj = new PrivateObject(stat, new PrivateType(typeof(Statistic)));

            Dictionary<int, ObservableCollection<StatisticResult>> popResults = (Dictionary<int, ObservableCollection<StatisticResult>>)privObj.GetField("populationResults");

            ObservableCollection<StatisticResult> statResults = new ObservableCollection<StatisticResult>();
            statResults.Add(new StatisticResult(1, 2, 3, stat));
            popResults.Add(0, statResults);

            MockStatistic result = (MockStatistic)SerializationHelper.TestSerialization(stat, new Type[0]);

            PrivateObject resultPrivObj = new PrivateObject(result, new PrivateType(typeof(Statistic)));

            Dictionary<int, ObservableCollection<StatisticResult>> resultPopResults = (Dictionary<int, ObservableCollection<StatisticResult>>)resultPrivObj.GetField("populationResults");

            ObservableCollection<StatisticResult> resultStatResults = resultPopResults[0];
            Assert.AreEqual(statResults[0].GenerationIndex, resultStatResults[0].GenerationIndex);
            Assert.AreEqual(statResults[0].PopulationId, resultStatResults[0].PopulationId);
            Assert.AreEqual(statResults[0].ResultValue, resultStatResults[0].ResultValue);
            Assert.AreSame(result, resultStatResults[0].Statistic);
        }

        private class FakeStatistic : Statistic
        {
            internal int GetResultValueCallCount;
            
            public override object GetResultValue(Population population)
            {
                this.GetResultValueCallCount++;
                return this.GetResultValueCallCount;
            }
        }
        
        private class TestStat : Statistic
        {
            public override object GetResultValue(Population population)
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
    }
}
