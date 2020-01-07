using GenFx.Wpf.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GenFx.Wpf.Controls
{
    /// <summary>
    /// Panel that contains controls for managing the execution of a <see cref="ExecutionContext"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class ExecutionPanel : Control
    {
        /// <summary>
        /// Gets the <see cref="DependencyProperty"/> associated with <see cref="ExecutionContext"/>.
        /// </summary>
        public static readonly DependencyProperty ExecutionContextProperty = DependencyProperty.Register(
            nameof(ExecutionContext), typeof(ExecutionContext), typeof(ExecutionPanel),
            new PropertyMetadata(new PropertyChangedCallback(ExecutionPanel.OnExecutionContextChanged)));

        /// <summary>
        /// Gets the <see cref="DependencyPropertyKey"/> associated with <see cref="CanStart"/>.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyPropertyKey CanStartProperty = DependencyProperty.RegisterReadOnly(
            nameof(CanStart), typeof(bool), typeof(ExecutionPanel), null);

        /// <summary>
        /// Gets the <see cref="DependencyPropertyKey"/> associated with <see cref="CanPause"/>.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyPropertyKey CanPauseProperty = DependencyProperty.RegisterReadOnly(
            nameof(CanPause), typeof(bool), typeof(ExecutionPanel), null);

        /// <summary>
        /// Gets the <see cref="DependencyPropertyKey"/> associated with <see cref="CanStop"/>.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyPropertyKey CanStopProperty = DependencyProperty.RegisterReadOnly(
            nameof(CanStop), typeof(bool), typeof(ExecutionPanel), null);

        private static readonly RoutedCommand startExecutionCommand = new RoutedCommand(
            nameof(StartExecutionCommand), typeof(ExecutionPanel));

        private static readonly RoutedCommand stepExecutionCommand = new RoutedCommand(
            nameof(StepExecutionCommand), typeof(ExecutionPanel));

        private static readonly RoutedCommand stopExecutionCommand = new RoutedCommand(
            nameof(StopExecutionCommand), typeof(ExecutionPanel));

        private static readonly RoutedCommand pauseExecutionCommand = new RoutedCommand(
            nameof(PauseExecutionCommand), typeof(ExecutionPanel));

        /// <summary>
        /// Gets a <see cref="RoutedCommand"/> that, when executed, starts the execution of the associated <see cref="GeneticAlgorithm"/>.
        /// </summary>
        public static RoutedCommand StartExecutionCommand
        {
            get { return ExecutionPanel.startExecutionCommand; }
        }

        /// <summary>
        /// Gets a <see cref="RoutedCommand"/> that, when executed, executes one generation of the associated <see cref="GeneticAlgorithm"/>.
        /// </summary>
        public static RoutedCommand StepExecutionCommand
        {
            get { return ExecutionPanel.stepExecutionCommand; }
        }

        /// <summary>
        /// Gets a <see cref="RoutedCommand"/> that, when executed, pauses the execution of the associated <see cref="GeneticAlgorithm"/>.
        /// </summary>
        public static RoutedCommand PauseExecutionCommand
        {
            get { return ExecutionPanel.pauseExecutionCommand; }
        }

        /// <summary>
        /// Gets a <see cref="RoutedCommand"/> that, when executed, stops the execution of the associated <see cref="GeneticAlgorithm"/>.
        /// </summary>
        public static RoutedCommand StopExecutionCommand
        {
            get { return ExecutionPanel.stopExecutionCommand; }
        }
        
        /// <summary>
        /// Gets or sets the <see cref="ExecutionContext"/> providing the context for executing a <see cref="GeneticAlgorithm"/>.
        /// </summary>
        public ExecutionContext ExecutionContext
        {
            get
            {
                return ((ExecutionContext)(base.GetValue(ExecutionPanel.ExecutionContextProperty)));
            }
            set
            {
                base.SetValue(ExecutionPanel.ExecutionContextProperty, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the associated <see cref="GeneticAlgorithm"/> can have its execution started.
        /// </summary>
        public bool CanStart
        {
            get
            {
                return (bool)base.GetValue(ExecutionPanel.CanStartProperty.DependencyProperty);
            }
            private set
            {
                base.SetValue(ExecutionPanel.CanStartProperty, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the associated <see cref="GeneticAlgorithm"/> can have its execution paused.
        /// </summary>
        public bool CanPause
        {
            get
            {
                return (bool)base.GetValue(ExecutionPanel.CanPauseProperty.DependencyProperty);
            }
            private set
            {
                base.SetValue(ExecutionPanel.CanPauseProperty, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the associated <see cref="GeneticAlgorithm"/> can have its execution stopped.
        /// </summary>
        public bool CanStop
        {
            get
            {
                return (bool)base.GetValue(ExecutionPanel.CanStopProperty.DependencyProperty);
            }
            private set
            {
                base.SetValue(ExecutionPanel.CanStopProperty, value);
            }
        }

        internal ExecutionPanelViewModel? ViewModel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static ExecutionPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ExecutionPanel), new FrameworkPropertyMetadata(typeof(ExecutionPanel)));
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public ExecutionPanel()
        {
            this.CommandBindings.Add(new CommandBinding(
                ExecutionPanel.StartExecutionCommand, ExecutionPanel.OnStartExecution, ExecutionPanel.CanStartExecution));
            this.CommandBindings.Add(new CommandBinding(
                ExecutionPanel.StepExecutionCommand, ExecutionPanel.OnStepExecution, ExecutionPanel.CanStartExecution));
            this.CommandBindings.Add(new CommandBinding(
                ExecutionPanel.StopExecutionCommand, ExecutionPanel.OnStopExecution, ExecutionPanel.CanStopExecution));
            this.CommandBindings.Add(new CommandBinding(
                ExecutionPanel.PauseExecutionCommand, ExecutionPanel.OnPauseExecution, ExecutionPanel.CanPauseExecution));
        }
        
        /// <summary>
        /// Handles the event when the <see cref="ExecutionContext"/> property changes.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> that owns the property.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> associated with the event.</param>
        private static void OnExecutionContextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ExecutionPanel panel = (ExecutionPanel)obj;
            if (e.OldValue != null)
            {
                ((ExecutionContext)e.OldValue).PropertyChanged -= panel.ExecutionContext_PropertyChanged;
                panel.ViewModel?.Dispose();
            }

            if (e.NewValue != null)
            {
                ExecutionContext executionContext = (ExecutionContext)e.NewValue;
                panel.ViewModel = new ExecutionPanelViewModel(executionContext);
                executionContext.PropertyChanged += panel.ExecutionContext_PropertyChanged;
                panel.UpdateCanExecuteProperties();
            }
        }

        /// <summary>
        /// Handles the event when a property within <see cref="ExecutionContext"/> changes.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> associated with the event.</param>
        private void ExecutionContext_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ExecutionContext.ExecutionState))
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    this.UpdateCanExecuteProperties();
                    CommandManager.InvalidateRequerySuggested();
                }));
            }
        }

        /// <summary>
        /// Updates the state of the <see cref="CanStart"/>, <see cref="CanPause"/>, and <see cref="CanStop"/> properties.
        /// </summary>
        private void UpdateCanExecuteProperties()
        {
            this.CanStart = this.ViewModel?.CanStartExecution() == true;
            this.CanPause = this.ViewModel?.CanPauseExecution() == true;
            this.CanStop = this.ViewModel?.CanStopExecution() == true;
        }

        /// <summary>
        /// Handles the event when the system queries whether <see cref="StartExecutionCommand"/> can be executed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="CanExecuteRoutedEventArgs"/> associated with the event.</param>
        private static void CanStartExecution(object sender, CanExecuteRoutedEventArgs e)
        {
            ExecutionPanel panel = (ExecutionPanel)sender;
            if (panel.ViewModel == null)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = panel.ViewModel.CanStartExecution();
            }
        }

        /// <summary>
        /// Handles the event when the system queries whether <see cref="PauseExecutionCommand"/> can be executed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="CanExecuteRoutedEventArgs"/> associated with the event.</param>
        private static void CanPauseExecution(object sender, CanExecuteRoutedEventArgs e)
        {
            ExecutionPanel panel = (ExecutionPanel)sender;
            if (panel.ViewModel == null)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = panel.ViewModel.CanPauseExecution();
            }
        }

        /// <summary>
        /// Handles the event when the system queries whether <see cref="StopExecutionCommand"/> can be executed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="CanExecuteRoutedEventArgs"/> associated with the event.</param>
        private static void CanStopExecution(object sender, CanExecuteRoutedEventArgs e)
        {
            ExecutionPanel panel = (ExecutionPanel)sender;
            if (panel.ViewModel == null)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = panel.ViewModel.CanStopExecution();
            }
        }

        /// <summary>
        /// Handles the event when the <see cref="StartExecutionCommand"/> is executed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="CanExecuteRoutedEventArgs"/> associated with the event.</param>
        private static void OnStartExecution(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = ((ExecutionPanel)sender).ViewModel;
            if (viewModel != null)
            {
                Task.Run(viewModel.StartExecutionAsync);
            }
        }

        /// <summary>
        /// Handles the event when the <see cref="StepExecutionCommand"/> is executed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="CanExecuteRoutedEventArgs"/> associated with the event.</param>
        private static void OnStepExecution(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = ((ExecutionPanel)sender).ViewModel;
            if (viewModel != null)
            {
                Task.Run(viewModel.StepExecutionAsync);
            }
        }

        /// <summary>
        /// Handles the event when the <see cref="StopExecutionCommand"/> is executed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="CanExecuteRoutedEventArgs"/> associated with the event.</param>
        private static void OnStopExecution(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = ((ExecutionPanel)sender).ViewModel;
            if (viewModel != null)
            {
                Task.Run(viewModel.StopExecution);
            }
        }

        /// <summary>
        /// Handles the event when the <see cref="PauseExecutionCommand"/> is executed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="CanExecuteRoutedEventArgs"/> associated with the event.</param>
        private static void OnPauseExecution(object sender, ExecutedRoutedEventArgs e)
        {
            var viewModel = ((ExecutionPanel)sender).ViewModel;
            if (viewModel != null)
            {
                Task.Run(viewModel.PauseExecution);
            }
        }
    }
}
