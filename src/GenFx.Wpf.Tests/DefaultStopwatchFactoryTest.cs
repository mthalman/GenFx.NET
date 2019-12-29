
using Xunit;

namespace GenFx.Wpf.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="DefaultStopwatchFactory"/> class.
    /// </summary>
    public class DefaultStopwatchFactoryTest
    {
        /// <summary>
        /// Tests that the <see cref="DefaultStopwatchFactory.Create"/> method works correctly.
        /// </summary>
        [Fact]
        public void DefaultStopwatchFactory_Create()
        {
            DefaultStopwatchFactory factory = new DefaultStopwatchFactory();
            Assert.IsType<DefaultStopwatch>(factory.Create());
        }
    }
}
