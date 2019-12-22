using TestCommon;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="Plugin"/> class.
    /// </summary>
    public class PluginTest
    {
        /// <summary>
        /// Tests that the <see cref="Plugin.OnAlgorithmCompleted"/> method is called.
        /// </summary>
        [Fact]
        public void Plugin_OnAlgorithmCompleted()
        {
            TestPlugin plugin = new TestPlugin();
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            plugin.Initialize(algorithm);

            PrivateObject accessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            accessor.Invoke("OnAlgorithmCompleted");

            Assert.True(plugin.OnAlgorithmCompletedCalled);
        }

        /// <summary>
        /// Tests that the <see cref="Plugin.OnAlgorithmStarting"/> method is called.
        /// </summary>
        [Fact]
        public void Plugin_OnAlgorithmStarting()
        {
            TestPlugin plugin = new TestPlugin();
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            plugin.Initialize(algorithm);

            PrivateObject accessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            accessor.Invoke("OnAlgorithmStarting");

            Assert.True(plugin.OnAlgorithmStartingCalled);
        }

        /// <summary>
        /// Tests that the <see cref="Plugin.OnFitnessEvaluated"/> method is called.
        /// </summary>
        [Fact]
        public void Plugin_OnFitnessEvaluated()
        {
            TestPlugin plugin = new TestPlugin();
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            plugin.Initialize(algorithm);

            PrivateObject accessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            accessor.Invoke("OnFitnessEvaluated", new EnvironmentFitnessEvaluatedEventArgs(new GeneticEnvironment(algorithm), 0));

            Assert.True(plugin.OnFitnessEvaluatedCalled);
        }

        private class TestPlugin : Plugin
        {
            public bool OnAlgorithmCompletedCalled;
            public bool OnAlgorithmStartingCalled;
            public bool OnFitnessEvaluatedCalled;

            protected override void OnAlgorithmCompleted()
            {
                this.OnAlgorithmCompletedCalled = true;
                base.OnAlgorithmCompleted();
            }

            protected override void OnAlgorithmStarting()
            {
                this.OnAlgorithmStartingCalled = true;
                base.OnAlgorithmStarting();
            }

            protected override void OnFitnessEvaluated(GeneticEnvironment environment, int generationIndex)
            {
                this.OnFitnessEvaluatedCalled = true;
                base.OnFitnessEvaluated(environment, generationIndex);
            }
        }
    }
}
