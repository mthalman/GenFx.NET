using GenFx.UI.Controls;
using Moq;
using Xunit;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="EnvironmentViewer"/> class.
    /// </summary>
    public class EnvironmentViewerTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [StaFact]
        public void EnvironmentViewer_Ctor()
        {
            EnvironmentViewer viewer = new EnvironmentViewer();
            Assert.Equal(ExecutionState.Idle, viewer.ExecutionState);
            Assert.Null(viewer.Environment);
        }

        /// <summary>
        /// Tests that the <see cref="EnvironmentViewer.Environment"/> property works correctly.
        /// </summary>
        [StaFact]
        public void EnvironmentViewer_EnvironmentProperty()
        {
            EnvironmentViewer viewer = new EnvironmentViewer();
            GeneticEnvironment environment = new GeneticEnvironment(Mock.Of<GeneticAlgorithm>());
            viewer.Environment = environment;
            Assert.Same(environment, viewer.Environment);
        }

        /// <summary>
        /// Tests that the <see cref="EnvironmentViewer.ExecutionState"/> property works correctly.
        /// </summary>
        [StaFact]
        public void EnvironmentViewer_ExecutionStateProperty()
        {
            EnvironmentViewer viewer = new EnvironmentViewer
            {
                ExecutionState = ExecutionState.PausePending
            };
            Assert.Equal(ExecutionState.PausePending, viewer.ExecutionState);
        }
    }
}
