using System;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="MetricResult"/> class.
    /// </summary>
    public class MetricResultTest
    {
        /// <summary>
        /// Tests that the <see cref="MetricResult"/> constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void MetricResult_Ctor()
        {
            MockMetric metric = new MockMetric();
            MetricResult result = new MetricResult(1, 2, 3, metric);

            Assert.Equal(1, result.GenerationIndex);
            Assert.Equal(2, result.PopulationIndex);
            Assert.Equal(3, result.ResultValue);
            Assert.Same(metric, result.Metric);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid generation index to the <see cref="MetricResult"/> constructor.
        /// </summary>
        [Fact]
        public void MetricResult_Ctor_InvalidGenerationIndex()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MetricResult(-1, 0, 0, new MockMetric()));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing an invalid population index to the <see cref="MetricResult"/> constructor.
        /// </summary>
        [Fact]
        public void MetricResult_Ctor_InvalidPopulationIndex()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new MetricResult(0, -1, 0, new MockMetric()));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null result value to the <see cref="MetricResult"/> constructor.
        /// </summary>
        [Fact]
        public void MetricResult_Ctor_NullResultValue()
        {
            Assert.Throws<ArgumentNullException>(() => new MetricResult(0, 0, null, new MockMetric()));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null metric to the <see cref="MetricResult"/> constructor.
        /// </summary>
        [Fact]
        public void MetricResult_Ctor_NullMetric()
        {
            Assert.Throws<ArgumentNullException>(() => new MetricResult(0, 0, 0, null));
        }
    }
}
