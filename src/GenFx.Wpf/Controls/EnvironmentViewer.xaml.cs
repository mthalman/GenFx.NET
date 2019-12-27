using System.Windows;
using System.Windows.Controls;

namespace GenFx.Wpf.Controls
{
    /// <summary>
    /// Control that provides that a view of the <see cref="GeneticEnvironment"/>.
    /// </summary>
    public class EnvironmentViewer : Control
    {
        /// <summary>
        /// Gets the <see cref="DependencyProperty"/> associated with <see cref="GeneticEnvironment"/>.
        /// </summary>
        public static readonly DependencyProperty EnvironmentProperty = DependencyProperty.Register(
            nameof(Environment), typeof(GeneticEnvironment), typeof(EnvironmentViewer));
        
        /// <summary>
        /// Gets the <see cref="DependencyProperty"/> associated with <see cref="ExecutionState"/>.
        /// </summary>
        public static readonly DependencyProperty ExecutionStateProperty = DependencyProperty.Register(
            nameof(ExecutionState), typeof(ExecutionState), typeof(EnvironmentViewer));
        
        /// <summary>
        /// Gets or sets the <see cref="GeneticEnvironment"/> which this control should provide a view of.
        /// </summary>
        public GeneticEnvironment Environment
        {
            get
            {
                return ((GeneticEnvironment)(base.GetValue(EnvironmentViewer.EnvironmentProperty)));
            }
            set
            {
                base.SetValue(EnvironmentViewer.EnvironmentProperty, value);
            }
        }
        
        /// <summary>
        /// Gets or sets the execution state of the <see cref="GeneticAlgorithm"/>.
        /// </summary>
        public ExecutionState ExecutionState
        {
            get
            {
                return ((ExecutionState)(base.GetValue(EnvironmentViewer.ExecutionStateProperty)));
            }
            set
            {
                base.SetValue(EnvironmentViewer.ExecutionStateProperty, value);
            }
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static EnvironmentViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EnvironmentViewer), new FrameworkPropertyMetadata(typeof(EnvironmentViewer)));
        }
    }
}
