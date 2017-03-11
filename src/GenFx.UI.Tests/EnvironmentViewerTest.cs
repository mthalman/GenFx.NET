using GenFx.UI.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="EnvironmentViewer"/> class.
    /// </summary>
    [TestClass]
    public class EnvironmentViewerTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void EnvironmentViewer_Ctor()
        {
            EnvironmentViewer viewer = new EnvironmentViewer();
            Assert.AreEqual(ExecutionState.Idle, viewer.ExecutionState);
            Assert.IsNull(viewer.Environment);
        }

        /// <summary>
        /// Tests that the <see cref="EnvironmentViewer.Environment"/> property works correctly.
        /// </summary>
        [TestMethod]
        public void EnvironmentViewer_EnvironmentProperty()
        {
            EnvironmentViewer viewer = new EnvironmentViewer();
            GeneticEnvironment environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            viewer.Environment = environment;
            Assert.AreSame(environment, viewer.Environment);
        }

        /// <summary>
        /// Tests that the <see cref="EnvironmentViewer.ExecutionState"/> property works correctly.
        /// </summary>
        [TestMethod]
        public void EnvironmentViewer_ExecutionStateProperty()
        {
            EnvironmentViewer viewer = new EnvironmentViewer();
            viewer.ExecutionState = ExecutionState.PausePending;
            Assert.AreEqual(ExecutionState.PausePending, viewer.ExecutionState);
        }
    }
}
