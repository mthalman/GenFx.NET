using GenFx.Contracts;

namespace GenFx
{
    /// <summary>
    /// Represents a customizable component within the framework.
    /// </summary>
    /// <typeparam name="TComponent">Type of the deriving component class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    public abstract class GeneticComponent<TComponent, TConfiguration> : IGeneticComponent
        where TComponent : GeneticComponent<TComponent, TConfiguration>
        where TConfiguration : ComponentFactoryConfig<TConfiguration, TComponent>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="configuration">The <typeparamref name="TConfiguration"/> containing the configuration of this component.</param>
        protected GeneticComponent(TConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the <typeparamref name="TConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        public TConfiguration Configuration
        {
            get;
            private set;
        }

        IComponentFactoryConfig IGeneticComponent.Configuration { get { return this.Configuration; } }

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
