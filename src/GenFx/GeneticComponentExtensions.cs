using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GenFx
{
    /// <summary>
    /// Contains extension methods for <see cref="GeneticComponent"/>.
    /// </summary>
    public static class GeneticComponentExtensions
    {
        /// <summary>
        /// Returns an objects that contains the serializable state of this component.
        /// </summary>
        public static KeyValueMap SaveState(this GeneticComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException("component");
            }

            KeyValueMap state = new KeyValueMap();
            component.SetSaveState(state);
            return state;
        }

        /// <summary>
        /// Creates a new version of the component, copies the configuration state, and initializes it.
        /// </summary>
        /// <param name="component">The component from which to create a new component and the source of the configuration state to be copied.</param>
        /// <returns>A new version of the component.</returns>
        public static GeneticComponent CreateNewAndInitialize(this GeneticComponent component)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            GeneticComponent newComponent = component.CreateNew();
            if (newComponent == null)
            {
                throw new InvalidOperationException(
                    StringUtil.GetFormattedString(Resources.ErrorMsg_CreateNewComponentNull, component.GetType(), nameof(component.CreateNew)));
            }

            if (newComponent.GetType() != component.GetType())
            {
                throw new InvalidOperationException(
                    StringUtil.GetFormattedString(Resources.ErrorMsg_CreateNewComponentWrongType, component.GetType(), nameof(component.CreateNew), newComponent.GetType()));
            }

            component.CopyConfigurationStateTo(newComponent);

            GeneticComponentWithAlgorithm newComponentWithAlg = newComponent as GeneticComponentWithAlgorithm;
            if (newComponentWithAlg != null)
            {
                newComponentWithAlg.Initialize(((GeneticComponentWithAlgorithm)component).Algorithm);
            }

            return newComponent;
        }

        /// <summary>
        /// Copies the state of all configuration properties defined on the source component to the target component.
        /// </summary>
        /// <param name="source">The <see cref="GeneticComponent"/> from which to copy the configuration state.</param>
        /// <param name="target">The <see cref="GeneticComponent"/> to which the state is to be copied.</param>
        public static void CopyConfigurationStateTo(this GeneticComponent source, GeneticComponent target)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            BindingFlags binding = BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy;
            IEnumerable<PropertyInfo> properties = source.GetType().GetProperties(binding)
                .Where(p => p.GetCustomAttribute<ConfigurationPropertyAttribute>() != null);

            foreach (PropertyInfo property in properties)
            {
                if (!property.CanRead || !property.CanWrite)
                {
                    throw new InvalidOperationException(
                        StringUtil.GetFormattedString(Resources.ErrorMsg_ConfigurationPropertyNoGetterNoSetter, source.GetType(), property.Name, typeof(ConfigurationPropertyAttribute)));
                }

                property.SetValue(target, property.GetValue(source));
            }
        }
    }
}
