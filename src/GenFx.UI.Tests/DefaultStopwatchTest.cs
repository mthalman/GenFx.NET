using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="DefaultStopwatch"/> class.
    /// </summary>
    [TestClass]
    public class DefaultStopwatchTest
    {
        /// <summary>
        /// Tests that the <see cref="DefaultStopwatch.Start"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void DefaultStopwatch_Start()
        {
            DefaultStopwatch stopwatch = new DefaultStopwatch();
            stopwatch.Start();
            Thread.Sleep(1);
            Assert.IsTrue(stopwatch.Elapsed >= TimeSpan.FromMilliseconds(1));
        }

        /// <summary>
        /// Tests that the <see cref="DefaultStopwatch.Restart"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void DefaultStopwatch_Restart()
        {
            DefaultStopwatch stopwatch = new DefaultStopwatch();
            stopwatch.Start();
            Thread.Sleep(100);
            stopwatch.Restart();
            Thread.Sleep(1);
            Assert.IsTrue(stopwatch.Elapsed >= TimeSpan.FromMilliseconds(1) &&
                stopwatch.Elapsed < TimeSpan.FromMilliseconds(100));
        }
    }
}
