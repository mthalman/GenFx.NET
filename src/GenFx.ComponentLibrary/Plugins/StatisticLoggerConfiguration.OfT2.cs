using GenFx.ComponentLibrary.Base;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Plugins
{
    /// <summary>
    /// Configuration for the <see cref="StatisticLogger{TPlugin, TConfiguration}"/> component.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TPlugin">Type of the associated plugin class.</typeparam>
    public abstract class StatisticLoggerConfiguration<TConfiguration, TPlugin> : PluginConfigurationBase<TConfiguration, TPlugin>
        where TConfiguration : StatisticLoggerConfiguration<TConfiguration, TPlugin>
        where TPlugin : StatisticLogger<TPlugin, TConfiguration>
    {
        private string traceCategory;

        /// <summary>
        /// Gets or sets the category to associate with the tracing information.
        /// </summary>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [RequiredValidator]
        public string TraceCategory
        {
            get { return this.traceCategory; }
            set { this.SetProperty(ref this.traceCategory, value); }
        }
    }
}
