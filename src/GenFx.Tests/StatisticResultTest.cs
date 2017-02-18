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
    /// Contains unit tests for the <see cref="StatisticResult"/> class.
    /// </summary>
    [TestClass]
    public class StatisticResultTest
    {
        /// <summary>
        /// Tests that the <see cref="StatisticResult"/> constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void StatisticResult_Ctor()
        {
            MockStatistic stat = new MockStatistic();
            StatisticResult result = new StatisticResult(1, 2, 3, stat);

            Assert.AreEqual(1, result.GenerationIndex);
            Assert.AreEqual(2, result.PopulationIndex);
            Assert.AreEqual(3, result.ResultValue);
            Assert.AreSame(stat, result.Statistic);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid generation index to the <see cref="StatisticResult"/> constructor.
        /// </summary>
        [TestMethod]
        public void StatisticResult_Ctor_InvalidGenerationIndex()
        {
            AssertEx.Throws<ArgumentException>(() => new StatisticResult(-1, 0, 0, new MockStatistic()));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid population index to the <see cref="StatisticResult"/> constructor.
        /// </summary>
        [TestMethod]
        public void StatisticResult_Ctor_InvalidPopulationIndex()
        {
            AssertEx.Throws<ArgumentException>(() => new StatisticResult(0, -1, 0, new MockStatistic()));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null result value to the <see cref="StatisticResult"/> constructor.
        /// </summary>
        [TestMethod]
        public void StatisticResult_Ctor_NullResultValue()
        {
            AssertEx.Throws<ArgumentException>(() => new StatisticResult(0, 0, null, new MockStatistic()));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null statistic to the <see cref="StatisticResult"/> constructor.
        /// </summary>
        [TestMethod]
        public void StatisticResult_Ctor_NullStatistic()
        {
            AssertEx.Throws<ArgumentException>(() => new StatisticResult(0, 0, 0, null));
        }
    }
}
