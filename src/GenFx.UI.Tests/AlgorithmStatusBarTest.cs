using GenFx.UI.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="AlgorithmStatusBar"/> class.
    /// </summary>
    [TestClass]
    public class AlgorithmStatusBarTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void AlgorithmStatusBar_Ctor()
        {
            AlgorithmStatusBar statusBar = new AlgorithmStatusBar();
            Assert.IsNull(statusBar.ExecutionContext);
        }

        /// <summary>
        /// Tests that the <see cref="AlgorithmStatusBar.ExecutionContext"/> property works correctly.
        /// </summary>
        [TestMethod]
        public void AlgorithmStatusBar_ExecutionContextProperty()
        {
            AlgorithmStatusBar statusBar = new AlgorithmStatusBar();
            ExecutionContext context = new ExecutionContext(Mock.Of<GeneticAlgorithm>());
            statusBar.ExecutionContext = context;
            Assert.AreSame(context, statusBar.ExecutionContext);
        }
    }
}
