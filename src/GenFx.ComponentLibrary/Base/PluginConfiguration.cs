using GenFx.ComponentModel;

namespace GenFx.ComponentLibrary.Base
{
    /// <summary>
    /// Represents the configuration of <see cref="Plugin{TPlugin, TConfiguration}"/>.
    /// </summary>
    public abstract class PluginConfiguration<TConfiguration, TPlugin> : ComponentConfiguration<TConfiguration, TPlugin>, IPluginConfiguration
        where TConfiguration : PluginConfiguration<TConfiguration, TPlugin>
        where TPlugin : Plugin<TPlugin, TConfiguration>
    {
    }
}
