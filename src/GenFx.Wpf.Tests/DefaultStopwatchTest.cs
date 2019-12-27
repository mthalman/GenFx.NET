using System;
using System.Threading;
using Xunit;

namespace GenFx.Wpf.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="DefaultStopwatch"/> class.
    /// </summary>
    public class DefaultStopwatchTest
    {
        /// <summary>
        /// Tests that the <see cref="DefaultStopwatch.Start"/> method works correctly.
        /// </summary>
        [Fact]
        public void DefaultStopwatch_Start()
        {
            DefaultStopwatch stopwatch = new DefaultStopwatch();
            stopwatch.Start();
            Thread.Sleep(1);
            Assert.True(stopwatch.Elapsed >= TimeSpan.FromMilliseconds(1));
        }

        /// <summary>
        /// Tests that the <see cref="DefaultStopwatch.Restart"/> method works correctly.
        /// </summary>
        [Fact]
        public void DefaultStopwatch_Restart()
        {
            DefaultStopwatch stopwatch = new DefaultStopwatch();
            stopwatch.Start();
            Thread.Sleep(100);
            stopwatch.Restart();
            Thread.Sleep(1);
            Assert.True(stopwatch.Elapsed >= TimeSpan.FromMilliseconds(1) &&
                stopwatch.Elapsed < TimeSpan.FromMilliseconds(100));
        }
    }
}
