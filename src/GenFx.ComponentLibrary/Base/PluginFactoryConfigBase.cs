using GenFx.Contracts;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="PluginBase{TPlugin, TConfiguration}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TPlugin">Type of the associated plugin class.</typeparam>
    public abstract class PluginFactoryConfigBase<TConfiguration, TPlugin> : ConfigurationForComponentWithAlgorithm<TConfiguration, TPlugin>, IPluginFactoryConfig
        where TConfiguration : PluginFactoryConfigBase<TConfiguration, TPlugin>
        where TPlugin : PluginBase<TPlugin, TConfiguration>
    {
    }
}
