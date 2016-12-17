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
            Configuration = configuration;
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        internal GeneticComponent()
            : base()
        {
        }

        /// <summary>
        /// Gets the <typeparamref name="TConfiguration"/> containing the configuration of this component instance.
        /// </summary>
        public new TConfiguration Configuration
        {
            get { return (TConfiguration)base.Configuration; }
            internal set { base.Configuration = value; }
        }
    }
}
