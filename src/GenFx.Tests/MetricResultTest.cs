using GenFx;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="MetricResult"/> class.
    /// </summary>
    [TestClass]
    public class MetricResultTest
    {
        /// <summary>
        /// Tests that the <see cref="MetricResult"/> constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void MetricResult_Ctor()
        {
            MockMetric metric = new MockMetric();
            MetricResult result = new MetricResult(1, 2, 3, metric);

            Assert.AreEqual(1, result.GenerationIndex);
            Assert.AreEqual(2, result.PopulationIndex);
            Assert.AreEqual(3, result.ResultValue);
            Assert.AreSame(metric, result.Metric);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid generation index to the <see cref="MetricResult"/> constructor.
        /// </summary>
        [TestMethod]
        public void MetricResult_Ctor_InvalidGenerationIndex()
        {
            AssertEx.Throws<ArgumentException>(() => new MetricResult(-1, 0, 0, new MockMetric()));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid population index to the <see cref="MetricResult"/> constructor.
        /// </summary>
        [TestMethod]
        public void MetricResult_Ctor_InvalidPopulationIndex()
        {
            AssertEx.Throws<ArgumentException>(() => new MetricResult(0, -1, 0, new MockMetric()));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null result value to the <see cref="MetricResult"/> constructor.
        /// </summary>
        [TestMethod]
        public void MetricResult_Ctor_NullResultValue()
        {
            AssertEx.Throws<ArgumentException>(() => new MetricResult(0, 0, null, new MockMetric()));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null metric to the <see cref="MetricResult"/> constructor.
        /// </summary>
        [TestMethod]
        public void MetricResult_Ctor_NullMetric()
        {
            AssertEx.Throws<ArgumentException>(() => new MetricResult(0, 0, 0, null));
        }
    }
}
