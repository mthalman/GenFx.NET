using GenFx.Wpf.Controls;
using GenFx.Wpf.Tests.Helpers;
using Moq;
using Polly;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using TestCommon;
using TestCommon.Helpers;
using TestCommon.Mocks;
using Xunit;

namespace GenFx.Wpf.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="ExecutionPanel"/> class.
    /// </summary>
    public class ExecutionPanelTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_Ctor()
        {
            ExecutionPanel panel = new ExecutionPanel();
            Assert.Equal(4, panel.CommandBindings.Count);
            Assert.Equal(
                new RoutedCommand[]
                {
                    ExecutionPanel.StartExecutionCommand,
                    ExecutionPanel.StepExecutionCommand,
                    ExecutionPanel.PauseExecutionCommand,
                    ExecutionPanel.StopExecutionCommand,
                }
                .OrderBy(c => c.Name),
                panel.CommandBindings
                    .Cast<CommandBinding>()
                    .Select(c => c.Command)
                    .Cast<RoutedCommand>()
                    .OrderBy(c => c.Name)
                    .ToList());            
        }

        /// <summary>
        /// Tests that the control responds correctly to the <see cref="ExecutionPanel.ExecutionContext"/> property being set.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_ExecutionContext()
        {
            ExecutionPanel panel = new ExecutionPanel();

            ExecutionContext context = new ExecutionContext(Mock.Of<GeneticAlgorithm>())
            {
                ExecutionState = ExecutionState.Running
            };
            panel.ExecutionContext = context;

            Assert.False(panel.CanStart);
            Assert.True(panel.CanStop);
            Assert.True(panel.CanPause);

            // Verify an event handler was added to respond to the context changing its ExecutionState
            context.ExecutionState = ExecutionState.Idle;
            Assert.True(panel.CanStart);
            Assert.False(panel.CanStop);
            Assert.False(panel.CanPause);
        }

        /// <summary>
        /// Tests that the control responds correctly to the <see cref="ExecutionPanel.ExecutionContext"/> property being set.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_ExecutionContext_Overwrite()
        {
            ExecutionPanel panel = new ExecutionPanel();

            GeneticAlgorithm algorithm = CreateTestAlgorithm(runsInfinitely: true);

            ExecutionContext context = new ExecutionContext(algorithm);
            panel.ExecutionContext = context;

            // Start execution to force event handlers to be added in order to 
            // verify they are removed when replacing the ExecutionContext.
            ExecutionPanel.StartExecutionCommand.Execute(null, panel);
            DispatcherHelper.DoEvents();

            TestHelper.WaitForResult(ExecutionState.Running, () => panel.ExecutionContext.ExecutionState);

            Mock<GeneticAlgorithm> algorithmMock2 = new Mock<GeneticAlgorithm>();
            ExecutionContext context2 = new ExecutionContext(algorithmMock2.Object)
            {
                ExecutionState = ExecutionState.Running
            };

            panel.ExecutionContext = context2;

            Assert.False(panel.CanStart);
            Assert.True(panel.CanStop);
            Assert.True(panel.CanPause);

            // Verify the panel does not respond to the original context changing
            context.ExecutionState = ExecutionState.Idle;
            Assert.False(panel.CanStart);
            Assert.True(panel.CanStop);
            Assert.True(panel.CanPause);

            // Verify the panel does respond to the new context changing
            context2.ExecutionState = ExecutionState.Idle;
            Assert.True(panel.CanStart);
            Assert.False(panel.CanStop);
            Assert.False(panel.CanPause);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StartExecutionCommand"/> works correctly.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_StartExecutionCommand_CanExecute_NoContext()
        {
            ExecutionPanel panel = new ExecutionPanel();
            bool result = ExecutionPanel.StartExecutionCommand.CanExecute(null, panel);
            Assert.False(result);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StartExecutionCommand"/> works correctly.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_StartExecutionCommand_CanExecute_WithContext()
        {
            ExecutionPanel panel = new ExecutionPanel
            {
                ExecutionContext = new ExecutionContext(Mock.Of<GeneticAlgorithm>())
            };

            foreach (ExecutionState enumVal in Enum.GetValues(typeof(ExecutionState)))
            {
                panel.ExecutionContext.ExecutionState = enumVal;
                bool result = ExecutionPanel.StartExecutionCommand.CanExecute(null, panel);

                switch (enumVal)
                {
                    case ExecutionState.Idle:
                    case ExecutionState.Paused:
                        Assert.True(result);
                        break;
                    default:
                        Assert.False(result);
                        break;
                }
            }
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StartExecutionCommand"/> works correctly.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_StartExecutionCommand_Execute()
        {
            ExecutionPanel panel = new ExecutionPanel
            {
                ExecutionContext = new ExecutionContext(CreateTestAlgorithm(true))
            };

            ExecutionPanel.StartExecutionCommand.Execute(null, panel);
            DispatcherHelper.DoEvents();

            TestHelper.WaitForResult(ExecutionState.Running, () => panel.ExecutionContext.ExecutionState);

            // Trigger the algorithm to complete
            ((TestTerminator)panel.ExecutionContext.GeneticAlgorithm.Terminator).IsCompleteValue = true;
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StepExecutionCommand"/> works correctly.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_StepExecutionCommand_CanExecute_NoContext()
        {
            ExecutionPanel panel = new ExecutionPanel();
            bool result = ExecutionPanel.StepExecutionCommand.CanExecute(null, panel);
            Assert.False(result);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StepExecutionCommand"/> works correctly.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_StepExecutionCommand_CanExecute_WithContext()
        {
            ExecutionPanel panel = new ExecutionPanel
            {
                ExecutionContext = new ExecutionContext(Mock.Of<GeneticAlgorithm>())
            };

            foreach (ExecutionState enumVal in Enum.GetValues(typeof(ExecutionState)))
            {
                panel.ExecutionContext.ExecutionState = enumVal;
                bool result = ExecutionPanel.StepExecutionCommand.CanExecute(null, panel);

                switch (enumVal)
                {
                    case ExecutionState.Idle:
                    case ExecutionState.Paused:
                        Assert.True(result);
                        break;
                    default:
                        Assert.False(result);
                        break;
                }
            }
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StepExecutionCommand"/> works correctly.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_StepExecutionCommand_Execute()
        {
            using SemaphoreSlim algorithmPausedSemaphore = new SemaphoreSlim(0);
            Dispatcher.CurrentDispatcher.Invoke(async () =>
            {
                ExecutionPanel panel = new ExecutionPanel
                {
                    ExecutionContext = new ExecutionContext(CreateTestAlgorithm(true))
                };

                using var algorithmPausedObservable = Observable
                    .FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                        ev => panel.ExecutionContext.PropertyChanged += ev,
                        ev => panel.ExecutionContext.PropertyChanged -= ev)
                    .Where(eventPattern => ((ExecutionContext)eventPattern.Sender).ExecutionState == ExecutionState.Paused)
                    .Subscribe(_ => algorithmPausedSemaphore.Release());

                await panel.ViewModel.StepExecutionAsync();
            });

            DispatcherHelper.DoEvents();

            Assert.True(algorithmPausedSemaphore.Wait(TimeSpan.FromSeconds(10)));
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.PauseExecutionCommand"/> works correctly.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_PauseExecutionCommand_CanExecute_NoContext()
        {
            ExecutionPanel panel = new ExecutionPanel();
            bool result = ExecutionPanel.PauseExecutionCommand.CanExecute(null, panel);
            Assert.False(result);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.PauseExecutionCommand"/> works correctly.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_PauseExecutionCommand_CanExecute_WithContext()
        {
            ExecutionPanel panel = new ExecutionPanel
            {
                ExecutionContext = new ExecutionContext(Mock.Of<GeneticAlgorithm>())
            };

            foreach (ExecutionState enumVal in Enum.GetValues(typeof(ExecutionState)))
            {
                panel.ExecutionContext.ExecutionState = enumVal;
                bool result = ExecutionPanel.PauseExecutionCommand.CanExecute(null, panel);

                switch (enumVal)
                {
                    case ExecutionState.Running:
                        Assert.True(result);
                        break;
                    default:
                        Assert.False(result);
                        break;
                }
            }
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.PauseExecutionCommand"/> works correctly.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_PauseExecutionCommand_Execute()
        {
            ExecutionPanel panel = new ExecutionPanel
            {
                ExecutionContext = new ExecutionContext(Mock.Of<GeneticAlgorithm>())
            };
            panel.ExecutionContext.ExecutionState = ExecutionState.Running;
            panel.ViewModel.PauseExecution();

            Assert.Equal(ExecutionState.PausePending, panel.ExecutionContext.ExecutionState);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StopExecutionCommand"/> works correctly.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_StopExecutionCommand_CanExecute_NoContext()
        {
            ExecutionPanel panel = new ExecutionPanel();
            bool result = ExecutionPanel.StopExecutionCommand.CanExecute(null, panel);
            Assert.False(result);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StopExecutionCommand"/> works correctly.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_StopExecutionCommand_CanExecute_WithContext()
        {
            ExecutionPanel panel = new ExecutionPanel
            {
                ExecutionContext = new ExecutionContext(Mock.Of<GeneticAlgorithm>())
            };

            foreach (ExecutionState enumVal in Enum.GetValues(typeof(ExecutionState)))
            {
                panel.ExecutionContext.ExecutionState = enumVal;
                bool result = ExecutionPanel.StopExecutionCommand.CanExecute(null, panel);

                switch (enumVal)
                {
                    case ExecutionState.Running:
                    case ExecutionState.Paused:
                        Assert.True(result);
                        break;
                    default:
                        Assert.False(result);
                        break;
                }
            }
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StopExecutionCommand"/> works correctly.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_StopExecutionCommand_Execute_FromRunning()
        {
            ExecutionPanel panel = new ExecutionPanel
            {
                ExecutionContext = new ExecutionContext(Mock.Of<GeneticAlgorithm>())
            };
            panel.ExecutionContext.ExecutionState = ExecutionState.Running;
            panel.ViewModel.StopExecution();

            Assert.Equal(ExecutionState.IdlePending, panel.ExecutionContext.ExecutionState);
        }

        /// <summary>
        /// Tests that the <see cref="ExecutionPanel.StopExecutionCommand"/> works correctly.
        /// </summary>
        [StaFact]
        public void ExecutionPanel_StopExecutionCommand_Execute_FromPaused()
        {
            ExecutionPanel panel = new ExecutionPanel
            {
                ExecutionContext = new ExecutionContext(Mock.Of<GeneticAlgorithm>())
            };
            panel.ExecutionContext.ExecutionState = ExecutionState.Paused;
            panel.ViewModel.StopExecution();

            Assert.Equal(ExecutionState.Idle, panel.ExecutionContext.ExecutionState);
        }

        private static GeneticAlgorithm CreateTestAlgorithm(bool runsInfinitely = false)
        {
            TestAlgorithm algorithm = new TestAlgorithm
            {
                CrossoverOperator = new Mock<CrossoverOperator>(2).Object,
                FitnessEvaluator = Mock.Of<FitnessEvaluator>(),
                GeneticEntitySeed = new TestEntity(),
                PopulationSeed = new TestPopulation(),
                SelectionOperator = Mock.Of<SelectionOperator>(),

                Terminator = new TestTerminator
                {
                    IsCompleteValue = !runsInfinitely
                }
            };

            return algorithm;
        }

        private class TestTerminator : Terminator
        {
            public bool IsCompleteValue { get; set; }

            public override bool IsComplete()
            {
                return this.IsCompleteValue;
            }
        }

        private class TestAlgorithm : GeneticAlgorithm
        {
            protected override Task CreateNextGenerationAsync(Population population)
            {
                return Task.FromResult(0);
            }
        }

        private class TestPopulation : Population
        {
        }

        private class TestEntity : GeneticEntity
        {
            public override string Representation
            {
                get
                {
                    return null;
                }
            }

            public override int CompareTo(GeneticEntity other)
            {
                throw new NotImplementedException();
            }
        }
    }
}
