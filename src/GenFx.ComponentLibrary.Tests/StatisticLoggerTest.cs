using GenFx.ComponentLibrary.Plugins;
using GenFx.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using TestCommon.Helpers;
using System;
using System.Linq;
using System.Text;
using TestCommon.Mocks;
using System.ComponentModel;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="StatisticLogger"/> class.
    /// </summary>
    [TestClass]
    public class StatisticLoggerTest
    {
        private TestTraceListener traceListener;

        [TestInitialize]
        public void TestInitialize()
        {
            this.traceListener = new TestTraceListener();
            Trace.Listeners.Add(this.traceListener);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Trace.Listeners.Remove(this.traceListener);
        }

        /// <summary>
        /// Tests that the <see cref="StatisticLogger"/> can be validated successfully.
        /// </summary>
        [TestMethod]
        public void StatisticLogger_Validation()
        {
            StatisticLogger logger = new StatisticLogger
            {
                TraceCategory = "test"
            };

            logger.Validate();
        }

        /// <summary>
        /// Tests that validation fails when no value is provided for <see cref="StatisticLogger.TraceCategory"/>.
        /// </summary>
        [TestMethod]
        public void StatisticLogger_Validation_MissingTraceCategory()
        {
            StatisticLogger logger = new StatisticLogger();
            AssertEx.Throws<ValidationException>(() => logger.Validate());
        }

        /// <summary>
        /// Tests that the <see cref="StatisticLogger.OnAlgorithmStarting"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void StatisticLogger_AlgorithmStarting()
        {
            StatisticLogger logger = new StatisticLogger
            {
                TraceCategory = "test"
            };

            Assert.AreEqual(String.Empty, this.traceListener.Output.ToString());

            PrivateObject accessor = new PrivateObject(logger);
            accessor.Invoke("OnAlgorithmStarting");

            Assert.IsTrue(this.traceListener.Output.ToString().StartsWith(logger.TraceCategory));
            Assert.IsTrue(this.traceListener.Output.ToString().Contains(Resources.StatisticLogger_AlgorithmStarted));
        }

        /// <summary>
        /// Tests that the <see cref="StatisticLogger.OnAlgorithmCompleted"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void StatisticLogger_AlgorithmCompleted()
        {
            StatisticLogger logger = new StatisticLogger
            {
                TraceCategory = "test"
            };

            Assert.AreEqual(String.Empty, this.traceListener.Output.ToString());

            PrivateObject accessor = new PrivateObject(logger);
            accessor.Invoke("OnAlgorithmCompleted");

            Assert.IsTrue(this.traceListener.Output.ToString().StartsWith(logger.TraceCategory));
            Assert.IsTrue(this.traceListener.Output.ToString().Contains(Resources.StatisticLogger_AlgorithmCompleted));
        }

        /// <summary>
        /// Tests that the <see cref="StatisticLogger.OnFitnessEvaluated"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void StatisticLogger_FitnessEvaluated()
        {
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.Statistics.Add(new TestStatistic1());
            algorithm.Statistics.Add(new TestStatistic2());
            algorithm.Statistics.Add(new TestStatistic3());

            StatisticLogger logger = new StatisticLogger
            {
                TraceCategory = "test"
            };
            logger.Initialize(algorithm);

            Assert.AreEqual(String.Empty, this.traceListener.Output.ToString());
            
            GeneticEnvironment environment = new GeneticEnvironment(algorithm);
            environment.Populations.Add(new MockPopulation { Index = 0 });
            environment.Populations.Add(new MockPopulation { Index = 1 });

            PrivateObject accessor = new PrivateObject(logger);
            accessor.Invoke("OnFitnessEvaluated", environment, 0);

            string[] lines = this.traceListener.Output.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(24, lines.Length);
            this.VerifyStatisticOutput(lines.Take(4).ToArray(), logger.TraceCategory, "Stat 1", "1:0", 0, 0);
            this.VerifyStatisticOutput(lines.Skip(4).Take(4).ToArray(), logger.TraceCategory, "Stat 2", "2:0", 0, 0);
            this.VerifyStatisticOutput(lines.Skip(8).Take(4).ToArray(), logger.TraceCategory, typeof(TestStatistic3).FullName, "3:0", 0, 0);
            this.VerifyStatisticOutput(lines.Skip(12).Take(4).ToArray(), logger.TraceCategory, "Stat 1", "1:1", 1, 0);
            this.VerifyStatisticOutput(lines.Skip(16).Take(4).ToArray(), logger.TraceCategory, "Stat 2", "2:1", 1, 0);
            this.VerifyStatisticOutput(lines.Skip(20).Take(4).ToArray(), logger.TraceCategory, typeof(TestStatistic3).FullName, "3:1", 1, 0);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null environment to <see cref="StatisticLogger.OnFitnessEvaluated"/>.
        /// </summary>
        [TestMethod]
        public void StatisticLogger_OnFitnessEvaluated_NullEnvironment()
        {
            StatisticLogger logger = new StatisticLogger();
            PrivateObject accessor = new PrivateObject(logger);
            AssertEx.Throws<ArgumentNullException>(() => accessor.Invoke("OnFitnessEvaluated", null, 0));
        }

        private void VerifyStatisticOutput(string[] statOutput, string traceCategory, string statName, string statValue, int populationIndex, int generationIndex)
        {
            Assert.IsTrue(statOutput[0].StartsWith(traceCategory));
            Assert.IsTrue(statOutput[0].Contains("Statistic Name: " + statName));
            Assert.AreEqual("Statistic Value: " + statValue, statOutput[1]);
            Assert.AreEqual("Population Index: " + populationIndex, statOutput[2]);
            Assert.AreEqual("Generation Index: " + generationIndex, statOutput[3]);
        }

        private class TestTraceListener : TraceListener
        {
            public StringBuilder Output = new StringBuilder();

            public override void Write(string message)
            {
                this.Output.Append(message);
            }

            public override void WriteLine(string message)
            {
                this.Output.AppendLine(message);
            }
        }

        [DisplayName("Stat 1")]
        private class TestStatistic1 : Statistic
        {
            public override object GetResultValue(Population population)
            {
                return "1:" + population.Index;
            }
        }

        private class TestStatistic2 : Statistic
        {
            public override object GetResultValue(Population population)
            {
                return "2:" + population.Index;
            }

            public override string ToString()
            {
                return "Stat 2";
            }
        }

        [DisplayName]
        private class TestStatistic3 : Statistic
        {
            public override object GetResultValue(Population population)
            {
                return "3:" + population.Index;
            }
        }
    }
}
