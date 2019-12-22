using GenFx.UI.Controls;
using Moq;
using Xunit;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="AlgorithmStatusBar"/> class.
    /// </summary>
    public class AlgorithmStatusBarTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [StaFact]
        public void AlgorithmStatusBar_Ctor()
        {
            AlgorithmStatusBar statusBar = new AlgorithmStatusBar();
            Assert.Null(statusBar.ExecutionContext);
        }

        /// <summary>
        /// Tests that the <see cref="AlgorithmStatusBar.ExecutionContext"/> property works correctly.
        /// </summary>
        [StaFact]
        public void AlgorithmStatusBar_ExecutionContextProperty()
        {
            AlgorithmStatusBar statusBar = new AlgorithmStatusBar();
            ExecutionContext context = new ExecutionContext(Mock.Of<GeneticAlgorithm>());
            statusBar.ExecutionContext = context;
            Assert.Same(context, statusBar.ExecutionContext);
        }
    }
}
