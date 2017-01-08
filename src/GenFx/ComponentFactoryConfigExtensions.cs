using GenFx.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenFx
{
    /// <summary>
    /// Contains extension methods for <see cref="IComponentFactoryConfig"/>.
    /// </summary>
    public static class ComponentFactoryConfigExtensions
    {
        /// <summary>
        /// Saves the state of <paramref name="configuration"/> into a <see cref="KeyValueMap"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="IComponentFactoryConfig"/> whose state should be saved.</param>
        /// <returns>A <see cref="KeyValueMap"/> containing the state of the <paramref name="configuration"/>.</returns>
        public static KeyValueMap SaveState(this IComponentFactoryConfig configuration)
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
        /// Restores the state of a <see cref="IComponentFactoryConfig"/>.
        /// </summary>
        /// <param name="state">The <see cref="KeyValueMap"/> containing the state of the <see cref="IComponentFactoryConfig"/>.</param>
        /// <returns>A <see cref="IComponentFactoryConfig"/> whose state has been restored.</returns>
        public static IComponentFactoryConfig RestoreComponentConfiguration(KeyValueMap state)
        {
            if (state == null)
            {
                return null;
            }

            IComponentFactoryConfig config = (IComponentFactoryConfig)Activator.CreateInstance(Type.GetType((string)state["$type"]));

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
