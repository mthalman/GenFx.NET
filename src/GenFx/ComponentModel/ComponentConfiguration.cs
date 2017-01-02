using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GenFx.ComponentModel
{
    /// <summary>
    /// Base class for all classes containing configuration settings for a component.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TComponent">Type of the associated component class.</typeparam>
    public abstract class ComponentConfiguration<TConfiguration, TComponent> : IComponentConfiguration, INotifyPropertyChanged
        where TConfiguration : ComponentConfiguration<TConfiguration, TComponent>
        where TComponent : GeneticComponent<TComponent, TConfiguration>
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the type of the component this configuration is associated with.
        /// </summary>
        public Type ComponentType { get { return typeof(TComponent); } }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">The name of the property.</param>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is null or empty.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(Resources.ErrorMsg_StringNullOrEmpty, nameof(propertyName));
            }

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the state of a property.  This handles validation and event raising.
        /// </summary>
        /// <param name="fieldValue">Backing field value of the property.</param>
        /// <param name="newValue">New value being assigned to the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <typeparam name="T">Type of the property.</typeparam>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        protected void SetProperty<T>(ref T fieldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            this.ValidateProperty(newValue, propertyName);
            fieldValue = newValue;
            this.OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Validates the state of the configuration.
        /// </summary>
        public void Validate()
        {
            IEnumerable<PropertyInfo> properties = this.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy)
                .Where(p => p.DeclaringType != typeof(ComponentConfiguration<TConfiguration, TComponent>));
            foreach (PropertyInfo propertyInfo in properties)
            {
                object propValue = propertyInfo.GetValue(this, null);

                // Check that the property is valid using the validators attached directly to the property.
                this.ValidateProperty(propValue, propertyInfo.Name);
            }
        }

        /// <summary>
        /// Verifies that the value is a valid value for the property.
        /// </summary>
        /// <param name="value">Value being set to the property.</param>
        /// <param name="propertyName">Name of the property being set.</param>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is null or empty.</exception>
        /// <exception cref="ArgumentException">Property not found on configuration type.</exception>
        /// <exception cref="ValidationException">Property was set to an invalid value.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        private void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(Resources.ErrorMsg_StringNullOrEmpty, nameof(propertyName));
            }

            PropertyInfo propertyInfo = this.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            if (propertyInfo == null)
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                  Resources.ErrorMsg_ComponentConfigurationPropertyNotFound, propertyName, this.GetType().FullName));
            }

            ConfigurationValidatorAttribute[] attribs = (ConfigurationValidatorAttribute[])propertyInfo.GetCustomAttributes(typeof(ConfigurationValidatorAttribute), false);
            for (int i = 0; i < attribs.Length; i++)
            {
                attribs[i].Validator.EnsureIsValid(this.GetType().Name + Type.Delimiter + propertyInfo.Name, value, this);
            }
        }
    }
}
