using GenFx;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Mocks;

namespace GenFx.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="Plugin"/> class.
    /// </summary>
    [TestClass]
    public class PluginTest
    {
        /// <summary>
        /// Tests that the <see cref="Plugin.OnAlgorithmCompleted"/> method is called.
        /// </summary>
        [TestMethod]
        public void Plugin_OnAlgorithmCompleted()
        {
            TestPlugin plugin = new TestPlugin();
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            plugin.Initialize(algorithm);

            PrivateObject accessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            accessor.Invoke("OnAlgorithmCompleted");

            Assert.IsTrue(plugin.OnAlgorithmCompletedCalled);
        }

        /// <summary>
        /// Tests that the <see cref="Plugin.OnAlgorithmStarting"/> method is called.
        /// </summary>
        [TestMethod]
        public void Plugin_OnAlgorithmStarting()
        {
            TestPlugin plugin = new TestPlugin();
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            plugin.Initialize(algorithm);

            PrivateObject accessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            accessor.Invoke("OnAlgorithmStarting");

            Assert.IsTrue(plugin.OnAlgorithmStartingCalled);
        }

        /// <summary>
        /// Tests that the <see cref="Plugin.OnFitnessEvaluated"/> method is called.
        /// </summary>
        [TestMethod]
        public void Plugin_OnFitnessEvaluated()
        {
            TestPlugin plugin = new TestPlugin();
            MockGeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            plugin.Initialize(algorithm);

            PrivateObject accessor = new PrivateObject(algorithm, new PrivateType(typeof(GeneticAlgorithm)));
            accessor.Invoke("OnFitnessEvaluated", new EnvironmentFitnessEvaluatedEventArgs(new GeneticEnvironment(algorithm), 0));

            Assert.IsTrue(plugin.OnFitnessEvaluatedCalled);
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
