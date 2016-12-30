namespace GenFx.ComponentModel
{
    /// <summary>
    /// Represents a customizable component within the framework.
    /// </summary>
    public abstract class GeneticComponent<TComponent, TConfiguration> : GeneticComponent
        where TComponent : GeneticComponent<TComponent, TConfiguration>
        where TConfiguration : ComponentConfiguration<TConfiguration, TComponent>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="configuration">The <typeparamref name="TConfiguration"/> containing the configuration of this component.</param>
        protected GeneticComponent(TConfiguration configuration)
            : base(configuration)
        {
        }
        
        /// <summary>
        /// Gets the <typeparamref name="TConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        public new TConfiguration Configuration
        {
            get { return (TConfiguration)base.Configuration; }
        }
    }
}
