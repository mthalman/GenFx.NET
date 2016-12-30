namespace GenFx.ComponentModel
{
    /// <summary>
    /// Represents a customizable component within the framework.
    /// </summary>
    public abstract class GeneticComponent : IGeneticComponent
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="configuration">The <see cref="ComponentConfiguration"/> containing the configuration of this component.</param>
        protected GeneticComponent(ComponentConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the <see cref="ComponentConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        public IComponentConfiguration Configuration
        {
            get;
            private set;
        }

        /// <summary>
        /// Restores the state of the component.
        /// </summary>
        /// <param name="state">The state to restore from.</param>
        public virtual void RestoreState(KeyValueMap state)
        {
        }

        /// <summary>
        /// Sets the serializable state of this component on the state object.
        /// </summary>
        /// <param name="state">The object containing the serializable state of this object.</param>
        public virtual void SetSaveState(KeyValueMap state)
        {
        }
    }
}
