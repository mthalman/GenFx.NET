using System.Windows;
using System.Windows.Controls;

namespace GenFx.UI.Controls
{
    /// <summary>
    /// Control that provides the status of the execution of a <see cref="GeneticAlgorithm"/>.
    /// </summary>
    public class AlgorithmStatusBar : Control
    {
        /// <summary>
        /// Gets the <see cref="DependencyProperty"/> associated with <see cref="ExecutionContext"/>.
        /// </summary>
        public static readonly DependencyProperty ExecutionContextProperty = DependencyProperty.Register(
            nameof(ExecutionContext), typeof(ExecutionContext), typeof(AlgorithmStatusBar));

        /// <summary>
        /// Gets or sets the <see cref="ExecutionContext"/> which this control provides the status of.
        /// </summary>
        public ExecutionContext ExecutionContext
        {
            get
            {
                return ((ExecutionContext)(base.GetValue(AlgorithmStatusBar.ExecutionContextProperty)));
            }
            set
            {
                base.SetValue(AlgorithmStatusBar.ExecutionContextProperty, value);
            }
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static AlgorithmStatusBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AlgorithmStatusBar), new FrameworkPropertyMetadata(typeof(AlgorithmStatusBar)));
        }
    }
}
