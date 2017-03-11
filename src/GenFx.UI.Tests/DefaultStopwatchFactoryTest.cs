using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="DefaultStopwatchFactory"/> class.
    /// </summary>
    [TestClass]
    public class DefaultStopwatchFactoryTest
    {
        /// <summary>
        /// Tests that the <see cref="DefaultStopwatchFactory.Create"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void DefaultStopwatchFactory_Create()
        {
            DefaultStopwatchFactory factory = new DefaultStopwatchFactory();
            Assert.IsInstanceOfType(factory.Create(), typeof(DefaultStopwatch));
        }
    }
}
