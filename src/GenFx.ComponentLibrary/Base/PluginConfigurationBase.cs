using GenFx.ComponentLibrary.ComponentModel;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="PluginBase{TPlugin, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TPlugin">Type of the associated plugin class.</typeparam>
    public abstract class PluginConfigurationBase<TConfiguration, TPlugin> : ConfigurationForComponentWithAlgorithm<TConfiguration, TPlugin>, IPluginConfiguration
        where TConfiguration : PluginConfigurationBase<TConfiguration, TPlugin>
        where TPlugin : PluginBase<TPlugin, TConfiguration>
    {
    }
}
