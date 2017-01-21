using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GenFx
{
    /// <summary>
    /// Represents a customizable component within the framework.
    /// </summary>
    public abstract class GeneticComponent : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Creates a new instance of the same component type as this object.
        /// </summary>
        /// <remarks>
        /// This method acts as a factory to create new versions of the component.  By default,
        /// it uses reflection to create an instance of the type using the default parameterless
        /// constructor.  If the derived class does not have a parameterless constructor, this
        /// method must be overriden.
        /// </remarks>
        /// <returns>A new instance of the same component type as this object.</returns>
        public virtual GeneticComponent CreateNew()
        {
            return (GeneticComponent)Activator.CreateInstance(this.GetType());
        }
        
        /// <summary>
        /// Restores the state of the component.
        /// </summary>
        /// <param name="state">The state to restore from.</param>
        public virtual void RestoreState(KeyValueMap state)
        {
        }

        /// <summary>
        /// Sets the serializable state of this component on the state object.
        /// </summary>
        /// <param name="state">The object containing the serializable state of this object.</param>
        public virtual void SetSaveState(KeyValueMap state)
        {
        }

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
                .Where(p => p.GetIndexParameters().Length == 0);
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

            PropertyValidatorAttribute[] attribs = (PropertyValidatorAttribute[])propertyInfo.GetCustomAttributes(typeof(PropertyValidatorAttribute), false);
            for (int i = 0; i < attribs.Length; i++)
            {
                attribs[i].Validator.EnsureIsValid(this.GetType().Name + Type.Delimiter + propertyInfo.Name, value, this);
            }
        }
    }
}
