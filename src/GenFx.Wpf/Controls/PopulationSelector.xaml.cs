using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GenFx.Wpf.Controls
{
    /// <summary>
    /// Control that provides a way to choose a population.
    /// </summary>
    public class PopulationSelector : Control
    {
        private bool isSelectedPopulationInitialized;

        /// <summary>
        /// Gets the <see cref="DependencyProperty"/> associated with <see cref="GeneticEnvironment"/>.
        /// </summary>
        public static readonly DependencyProperty EnvironmentProperty = DependencyProperty.Register(
            nameof(Environment), typeof(GeneticEnvironment), typeof(PopulationSelector),
            new PropertyMetadata(new PropertyChangedCallback(PopulationSelector.OnEnvironmentChanged)));

        /// <summary>
        /// Gets the <see cref="DependencyProperty"/> associated with <see cref="SelectedPopulationIndex"/>.
        /// </summary>
        public static readonly DependencyProperty SelectedPopulationIndexProperty = DependencyProperty.Register(
            nameof(SelectedPopulationIndex), typeof(int), typeof(PopulationSelector),
            new PropertyMetadata(-1, new PropertyChangedCallback(PopulationSelector.OnSelectedPopulationIndexChanged)));
        
        /// <summary>
        /// Gets the <see cref="DependencyPropertyKey"/> associated with <see cref="SelectedPopulation"/>.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyPropertyKey SelectedPopulationProperty = DependencyProperty.RegisterReadOnly(
            nameof(SelectedPopulation), typeof(Population), typeof(PopulationSelector), null);
        
        /// <summary>
        /// Gets or sets the <see cref="GeneticEnvironment"/> containing the populations from which to select.
        /// </summary>
        public GeneticEnvironment Environment
        {
            get
            {
                return ((GeneticEnvironment)(base.GetValue(PopulationSelector.EnvironmentProperty)));
            }
            set
            {
                base.SetValue(PopulationSelector.EnvironmentProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the index of the currently selected population. This value is less than zero if
        /// no population is currently selected.
        /// </summary>
        public int SelectedPopulationIndex
        {
            get
            {
                return ((int)(base.GetValue(PopulationSelector.SelectedPopulationIndexProperty)));
            }
            set
            {
                base.SetValue(PopulationSelector.SelectedPopulationIndexProperty, value);
            }
        }
        
        /// <summary>
        /// Gets the currently selected <see cref="Population"/>.
        /// </summary>
        public Population SelectedPopulation
        {
            get
            {
                return (Population)base.GetValue(PopulationSelector.SelectedPopulationProperty.DependencyProperty);
            }
            private set
            {
                base.SetValue(PopulationSelector.SelectedPopulationProperty, value);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static PopulationSelector()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopulationSelector), new FrameworkPropertyMetadata(typeof(PopulationSelector)));
        }
        
        /// <summary>
        /// Handles the event when the <see cref="Environment"/> property changes.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> that owns the property.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> associated with the event.</param>
        private static void OnEnvironmentChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            PopulationSelector selector = (PopulationSelector)obj;
            if (e.OldValue != null)
            {
                GeneticEnvironment environment = (GeneticEnvironment)e.OldValue;
                environment.Populations.CollectionChanged -= selector.Populations_CollectionChanged;
                selector.isSelectedPopulationInitialized = false;
                selector.SelectedPopulationIndex = -1;
            }

            if (e.NewValue != null)
            {
                GeneticEnvironment environment = (GeneticEnvironment)e.NewValue;
                environment.Populations.CollectionChanged += selector.Populations_CollectionChanged;
                if (environment.Populations.Any())
                {
                    selector.TryInitializeSelectedPopulation();
                }
            }
        }
        
        /// <summary>
        /// Handles the event when the <see cref="SelectedPopulationIndex"/> property changes.
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> that owns the property.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> associated with the event.</param>
        private static void OnSelectedPopulationIndexChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            PopulationSelector selector = (PopulationSelector)obj;

            int index = (int)e.NewValue;
            if (selector.Environment == null || index < 0 || index >= selector.Environment.Populations.Count)
            {
                selector.SelectedPopulation = null;
            }
            else
            {
                selector.SelectedPopulation = selector.Environment.Populations[(int)e.NewValue];
            }
        }
        
        /// <summary>
        /// Handles the event when the <see cref="GeneticEnvironment.Populations"/> collection has changed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> associated with the event.</param>
        private void Populations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                this.TryInitializeSelectedPopulation();

                if (e.Action == NotifyCollectionChangedAction.Remove && e.OldItems.Contains(this.SelectedPopulation))
                {
                    if (this.Environment.Populations.Any())
                    {
                        if (this.SelectedPopulationIndex != 0)
                        {
                            // This will implicity set the new selected population
                            this.SelectedPopulationIndex = 0;
                        }
                        else
                        {
                            // The index hasn't changed but the selected population associated with this index
                            // needs to change now that the collection is changed.
                            this.SelectedPopulation = this.Environment.Populations[this.SelectedPopulationIndex];
                        }
                    }
                    else
                    {
                        this.SelectedPopulationIndex = -1;
                    }
                }
            }));
        }

        /// <summary>
        /// Sets the <see cref="SelectedPopulation"/> if it hasn't yet been initialized.
        /// </summary>
        private void TryInitializeSelectedPopulation()
        {
            if (!this.isSelectedPopulationInitialized)
            {
                if (this.Environment.Populations.Any())
                {
                    if (this.SelectedPopulationIndex < 0)
                    {
                        // This will implicitly set the SelectedPopulation
                        this.SelectedPopulationIndex = 0;
                    }
                    else
                    {
                        this.SelectedPopulation = this.Environment.Populations[this.SelectedPopulationIndex];
                    }
                    
                    this.isSelectedPopulationInitialized = true;
                }
            }
        }
    }
}
