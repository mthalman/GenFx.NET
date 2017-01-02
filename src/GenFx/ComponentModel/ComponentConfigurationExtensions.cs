using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenFx.ComponentModel
{
    /// <summary>
    /// Contains extension methods for <see cref="IComponentConfiguration"/>.
    /// </summary>
    public static class ComponentConfigurationExtensions
    {
        /// <summary>
        /// Saves the state of <paramref name="configuration"/> into a <see cref="KeyValueMap"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="IComponentConfiguration"/> whose state should be saved.</param>
        /// <returns>A <see cref="KeyValueMap"/> containing the state of the <paramref name="configuration"/>.</returns>
        public static KeyValueMap SaveState(this IComponentConfiguration configuration)
        {
            if (configuration == null)
            {
                return null;
            }

            KeyValueMap state = new KeyValueMap();
            state["$type"] = configuration.GetType().AssemblyQualifiedName;

            IEnumerable<PropertyInfo> properties = configuration.GetType().GetProperties().Where(p => p.CanRead && p.CanWrite);
            foreach (PropertyInfo property in properties)
            {
                object val = property.GetValue(configuration);
                if (val is Enum)
                {
                    val = val.ToString();
                }

                state[property.Name] = val;
            }

            return state;
        }

        /// <summary>
        /// Restores the state of a <see cref="IComponentConfiguration"/>.
        /// </summary>
        /// <param name="state">The <see cref="KeyValueMap"/> containing the state of the <see cref="IComponentConfiguration"/>.</param>
        /// <returns>A <see cref="IComponentConfiguration"/> whose state has been restored.</returns>
        public static IComponentConfiguration RestoreComponentConfiguration(KeyValueMap state)
        {
            if (state == null)
            {
                return null;
            }

            IComponentConfiguration config = (IComponentConfiguration)Activator.CreateInstance(Type.GetType((string)state["$type"]));

            IEnumerable<PropertyInfo> properties = config.GetType().GetProperties().Where(p => p.CanRead && p.CanWrite);
            foreach (PropertyInfo property in properties)
            {
                object val = state[property.Name];

                if (property.PropertyType.IsEnum)
                {
                    val = Enum.Parse(property.PropertyType, (string)val);
                }

                property.SetValue(config, val);
            }

            return config;
        }
    }
}
