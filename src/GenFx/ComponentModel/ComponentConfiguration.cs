using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using GenFx.Properties;
using GenFx.Validation;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GenFx.ComponentModel
{
    /// <summary>
    /// Base class for all classes containing configuration settings for a component.
    /// </summary>
    public abstract class ComponentConfiguration : INotifyPropertyChanged
    {
        internal const string ComponentTypeProperty = "ComponentType";

        private Type componentType;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the <see cref="Type"/> of the component associated with these settings.
        /// </summary>
        public Type ComponentType
        {
            get
            {
                if (this.componentType == null)
                {
                    this.LoadComponentType();
                }

                return this.componentType;
            }
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
                throw new ArgumentException(FwkResources.ErrorMsg_StringNullOrEmpty, nameof(propertyName));
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
        /// Verifies that the value is a valid value for the property.
        /// </summary>
        /// <param name="value">Value being set to the property.</param>
        /// <param name="propertyName">Name of the property being set.</param>
        /// <exception cref="ArgumentException"><paramref name="propertyName"/> is null or empty.</exception>
        /// <exception cref="ArgumentException">Property not found on configuration type.</exception>
        /// <exception cref="ValidationException">Property was set to an invalid value.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        protected internal void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(FwkResources.ErrorMsg_StringNullOrEmpty, nameof(propertyName));
            }

            PropertyInfo propertyInfo = this.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            if (propertyInfo == null)
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                  FwkResources.ErrorMsg_ComponentConfigurationPropertyNotFound, propertyName, this.GetType().FullName));
            }

            ConfigurationValidatorAttribute[] attribs = (ConfigurationValidatorAttribute[])propertyInfo.GetCustomAttributes(typeof(ConfigurationValidatorAttribute), false);
            for (int i = 0; i < attribs.Length; i++)
            {
                ComponentHelper.CheckValidation(attribs[i].Validator, this.GetType().Name + Type.Delimiter + propertyInfo.Name, value, this);
            }
        }

        /// <summary>
        /// Loads the related component type of this configuration object.
        /// </summary>
        private void LoadComponentType()
        {
            ComponentAttribute[] attribs = (ComponentAttribute[])this.GetType().GetCustomAttributes(typeof(ComponentAttribute), false);
            if (attribs.Length == 0)
            {
                throw new ComponentException(StringUtil.GetFormattedString(
                    StringUtil.GetFormattedString(
                        FwkResources.ErrorMsg_Component_MissingComponentAttribute,
                        this.GetType().FullName,
                        typeof(ComponentAttribute).FullName)));
            }

            Debug.Assert(attribs.Length == 1);

            this.componentType = attribs[0].ComponentType;
        }
    }
}
