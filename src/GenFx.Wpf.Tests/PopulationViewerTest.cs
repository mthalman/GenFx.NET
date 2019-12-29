using GenFx.Wpf.Controls;
using Moq;
using System.Linq;
using Xunit;

namespace GenFx.Wpf.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="PopulationViewer"/> class.
    /// </summary>
    public class PopulationViewerTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [StaFact]
        public void PopulationViewer_Ctor()
        {
            PopulationViewer viewer = new PopulationViewer();
            Assert.Equal(ExecutionState.Idle, viewer.ExecutionState);
            Assert.Null(viewer.Population);
            Assert.Null(viewer.SelectedPopulationEntities);
        }

        /// <summary>
        /// Tests that the entities are updated when the state is changed.
        /// </summary>
        [StaFact]
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
        [StaFact]
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
        [StaFact]
        public void PopulationViewer_OnSelectedPopulationChanged_InitialPopulation()
        {
            PopulationViewer viewer = new PopulationViewer
            {
                ExecutionState = ExecutionState.Idle
            };

            TestPopulation population = new TestPopulation();
            population.Entities.Add(Mock.Of<GeneticEntity>());
            population.Entities.Add(Mock.Of<GeneticEntity>());

            viewer.Population = population;
            Assert.Null(viewer.SelectedPopulationEntities);
        }

        /// <summary>
        /// Tests that the entities are updated when the population is set to null.
        /// </summary>
        [StaFact]
        public void PopulationViewer_OnSelectedPopulationChanged_NullPopulation()
        {
            PopulationViewer viewer = new PopulationViewer
            {
                ExecutionState = ExecutionState.Idle,
                Population = new TestPopulation()
            };

            TestPopulation population = new TestPopulation();
            population.Entities.Add(Mock.Of<GeneticEntity>());
            population.Entities.Add(Mock.Of<GeneticEntity>());

            viewer.Population = population;
            Assert.Equal(population.Entities, viewer.SelectedPopulationEntities.ToList());

            viewer.Population = null;
            Assert.Null(viewer.SelectedPopulationEntities);
        }

        private static void TestStateTransition(ExecutionState fromState, ExecutionState toState, bool expectEntitiesToUpdate)
        {
            PopulationViewer viewer = new PopulationViewer
            {
                ExecutionState = fromState
            };

            TestPopulation population = new TestPopulation();
            population.Entities.Add(Mock.Of<GeneticEntity>());
            population.Entities.Add(Mock.Of<GeneticEntity>());

            viewer.Population = population;

            viewer.ExecutionState = toState;

            if (expectEntitiesToUpdate)
            {
                Assert.Equal(population.Entities, viewer.SelectedPopulationEntities.ToList());
            }
            else
            {
                Assert.Null(viewer.SelectedPopulationEntities);
            }
        }

        private static void TestPopulationChange(ExecutionState state, bool expectEntitiesToUpdate)
        {
            PopulationViewer viewer = new PopulationViewer
            {
                ExecutionState = state
            };

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
                Assert.Equal(population2.Entities, viewer.SelectedPopulationEntities.ToList());
            }
            else
            {
                Assert.Null(viewer.SelectedPopulationEntities);
            }
        }

        private class TestPopulation : Population
        {
        }
    }
}
