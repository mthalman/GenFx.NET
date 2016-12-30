using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="PluginBase{TPlugin, TConfiguration}"/>.
    /// </summary>
    public abstract class PluginConfigurationBase<TConfiguration, TPlugin> : ComponentConfiguration<TConfiguration, TPlugin>, IPluginConfiguration
        where TConfiguration : PluginConfigurationBase<TConfiguration, TPlugin>
        where TPlugin : PluginBase<TPlugin, TConfiguration>
    {
    }
}
