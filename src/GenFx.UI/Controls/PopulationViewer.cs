using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GenFx.UI.Controls
{
    /// <summary>
    /// Control that provides that a view of a <see cref="GenFx.Population"/>.
    /// </summary>
    public class PopulationViewer : Control
    {
        /// <summary>
        /// Gets the <see cref="DependencyProperty"/> associated with <see cref="Population"/>.
        /// </summary>
        public static readonly DependencyProperty PopulationProperty = DependencyProperty.Register(
            nameof(Population), typeof(Population), typeof(PopulationViewer),
            new PropertyMetadata(new PropertyChangedCallback(PopulationViewer.OnSelectedPopulationChanged)));

        /// <summary>
        /// Gets the <see cref="DependencyProperty"/> associated with <see cref="ExecutionState"/>.
        /// </summary>
        public static readonly DependencyProperty ExecutionStateProperty = DependencyProperty.Register(
            nameof(ExecutionState), typeof(ExecutionState), typeof(PopulationViewer),
            new PropertyMetadata(ExecutionState.Idle, new PropertyChangedCallback(PopulationViewer.OnExecutionStateChanged)));

        /// <summary>
        /// Gets the <see cref="DependencyPropertyKey"/> associated with <see cref="SelectedPopulationEntities"/>.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyPropertyKey SelectedPopulationEntitiesProperty = DependencyProperty.RegisterReadOnly(
            nameof(SelectedPopulationEntities), typeof(IEnumerable<GeneticEntity>), typeof(PopulationViewer), null);

        /// <summary>
        /// Gets or sets the <see cref="GenFx.Population"/> to view.
        /// </summary>
        public Population Population
        {
            get
            {
                return ((Population)(base.GetValue(PopulationViewer.PopulationProperty)));
            }
            set
            {
                base.SetValue(PopulationViewer.PopulationProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the execution state of the <see cref="GeneticAlgorithm"/>.
        /// </summary>
        public ExecutionState ExecutionState
        {
            get
            {
                return ((ExecutionState)(base.GetValue(PopulationViewer.ExecutionStateProperty)));
            }
            set
            {
                base.SetValue(PopulationViewer.ExecutionStateProperty, value);
            }
        }
        
        /// <summary>
        /// Gets the <see cref="GeneticEntity"/> objects contained in <see cref="Population"/>.
        /// </summary>
        public IEnumerable<GeneticEntity> SelectedPopulationEntities
        {
            get
            {
                return (IEnumerable<GeneticEntity>)base.GetValue(PopulationViewer.SelectedPopulationEntitiesProperty.DependencyProperty);
            }
            private set
            {
                base.SetValue(PopulationViewer.SelectedPopulationEntitiesProperty, value);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static PopulationViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopulationViewer), new FrameworkPropertyMetadata(typeof(PopulationViewer)));
        }
        
        /// <summary>
        /// Handles the event when the <see cref="ExecutionState"/> property changes.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> that owns the property.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> associated with the event.</param>
        private static void OnExecutionStateChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            PopulationViewer viewer = (PopulationViewer)obj;

            ExecutionState newState = (ExecutionState)e.NewValue;
            ExecutionState oldState = (ExecutionState)e.OldValue;
            
            // No need to refresh if we're moving from paused to idle since no change to the entities would have occurred.
            if (newState == ExecutionState.Idle && oldState == ExecutionState.Paused)
            {
                return;
            }

            viewer.RefreshEntities(viewer.Population, newState);
        }
        
        /// <summary>
        /// Handles the event when the <see cref="Population"/> property changes.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> that owns the property.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> associated with the event.</param>
        private static void OnSelectedPopulationChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            PopulationViewer viewer = (PopulationViewer)obj;

            if (e.OldValue == null)
            {
                viewer.SelectedPopulationEntities = null;
                return;
            }

            viewer.RefreshEntities((Population)e.NewValue, viewer.ExecutionState);
        }

        /// <summary>
        /// Refreshes the state of <see cref="SelectedPopulationEntities"/> based on the value of <see cref="ExecutionState"/>.
        /// </summary>
        /// <param name="currentPopulation">The current <see cref="GenFx.Population"/>.</param>
        /// <param name="currentState">The current <see cref="ExecutionState"/>.</param>
        private void RefreshEntities(Population currentPopulation, ExecutionState currentState)
        {
            if (currentState == ExecutionState.Idle || currentState == ExecutionState.Paused)
            {
                if (currentPopulation == null)
                {
                    this.SelectedPopulationEntities = null;
                }
                else
                {
                    this.SelectedPopulationEntities = currentPopulation.Entities.ToList();
                }
            }
        }
    }
}
