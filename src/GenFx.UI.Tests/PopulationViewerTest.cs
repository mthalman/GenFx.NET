using GenFx.UI.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace GenFx.UI.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="PopulationViewer"/> class.
    /// </summary>
    [TestClass]
    public class PopulationViewerTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void PopulationViewer_Ctor()
        {
            PopulationViewer viewer = new PopulationViewer();
            Assert.AreEqual(ExecutionState.Idle, viewer.ExecutionState);
            Assert.IsNull(viewer.Population);
            Assert.IsNull(viewer.SelectedPopulationEntities);
        }

        /// <summary>
        /// Tests that the entities are updated when the state is changed.
        /// </summary>
        [TestMethod]
        public void PopulationViewer_OnExecutionStateChanged()
        {
            TestStateTransition(ExecutionState.Running, ExecutionState.Paused, true);
            TestStateTransition(ExecutionState.Running, ExecutionState.Idle, true);
            TestStateTransition(ExecutionState.Running, ExecutionState.PausePending, false);
            TestStateTransition(ExecutionState.Idle, ExecutionState.Running, false);
            TestStateTransition(ExecutionState.Idle, ExecutionState.PausePending, false);
            TestStateTransition(ExecutionState.Idle, ExecutionState.IdlePending, false);
            TestStateTransition(ExecutionState.Paused, ExecutionState.Idle, false);
        }

        /// <summary>
        /// Tests that the entities are updated when the population is changed if the state is idle or paused.
        /// </summary>
        [TestMethod]
        public void PopulationViewer_OnSelectedPopulationChanged()
        {
            TestPopulationChange(ExecutionState.Idle, true);
            TestPopulationChange(ExecutionState.IdlePending, false);
            TestPopulationChange(ExecutionState.Paused, true);
            TestPopulationChange(ExecutionState.PausePending, false);
            TestPopulationChange(ExecutionState.Running, false);
        }

        /// <summary>
        /// Tests that the entities are not updated when the population is replacing a null poulation.
        /// </summary>
        [TestMethod]
        public void PopulationViewer_OnSelectedPopulationChanged_InitialPopulation()
        {
            PopulationViewer viewer = new PopulationViewer();
            viewer.ExecutionState = ExecutionState.Idle;

            TestPopulation population = new TestPopulation();
            population.Entities.Add(Mock.Of<GeneticEntity>());
            population.Entities.Add(Mock.Of<GeneticEntity>());

            viewer.Population = population;
            Assert.IsNull(viewer.SelectedPopulationEntities);
        }

        /// <summary>
        /// Tests that the entities are updated when the population is set to null.
        /// </summary>
        [TestMethod]
        public void PopulationViewer_OnSelectedPopulationChanged_NullPopulation()
        {
            PopulationViewer viewer = new PopulationViewer();
            viewer.ExecutionState =  ExecutionState.Idle;

            viewer.Population = new TestPopulation();

            TestPopulation population = new TestPopulation();
            population.Entities.Add(Mock.Of<GeneticEntity>());
            population.Entities.Add(Mock.Of<GeneticEntity>());

            viewer.Population = population;
            CollectionAssert.AreEqual(population.Entities, viewer.SelectedPopulationEntities.ToList());

            viewer.Population = null;
            Assert.IsNull(viewer.SelectedPopulationEntities);
        }

        private static void TestStateTransition(ExecutionState fromState, ExecutionState toState, bool expectEntitiesToUpdate)
        {
            PopulationViewer viewer = new PopulationViewer();
            viewer.ExecutionState = fromState;

            TestPopulation population = new TestPopulation();
            population.Entities.Add(Mock.Of<GeneticEntity>());
            population.Entities.Add(Mock.Of<GeneticEntity>());

            viewer.Population = population;

            viewer.ExecutionState = toState;

            if (expectEntitiesToUpdate)
            {
                CollectionAssert.AreEqual(population.Entities, viewer.SelectedPopulationEntities.ToList());
            }
            else
            {
                Assert.IsNull(viewer.SelectedPopulationEntities);
            }
        }

        private static void TestPopulationChange(ExecutionState state, bool expectEntitiesToUpdate)
        {
            PopulationViewer viewer = new PopulationViewer();
            viewer.ExecutionState = state;

            TestPopulation population = new TestPopulation();
            population.Entities.Add(Mock.Of<GeneticEntity>());
            population.Entities.Add(Mock.Of<GeneticEntity>());

            viewer.Population = population;

            TestPopulation population2 = new TestPopulation();
            population2.Entities.Add(Mock.Of<GeneticEntity>());
            population2.Entities.Add(Mock.Of<GeneticEntity>());

            viewer.Population = population2;

            if (expectEntitiesToUpdate)
            {
                CollectionAssert.AreEqual(population2.Entities, viewer.SelectedPopulationEntities.ToList());
            }
            else
            {
                Assert.IsNull(viewer.SelectedPopulationEntities);
            }
        }

        private class TestPopulation : Population
        {
        }
    }
}
