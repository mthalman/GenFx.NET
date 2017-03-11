using GenFx.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace GenFx
{
    /// <summary>
    /// Represents a customizable component within the framework.
    /// </summary>
    [DataContract]
    public abstract class GeneticComponent : ObservableObject
    {
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
        /// Sets the state of a property.
        /// </summary>
        /// <param name="fieldValue">Backing field value of the property.</param>
        /// <param name="newValue">New value being assigned to the property.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <typeparam name="T">Type of the property.</typeparam>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        protected override void SetProperty<T>(ref T fieldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            this.ValidateProperty(newValue, propertyName);
            base.SetProperty(ref fieldValue, newValue, propertyName);
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
                this.ValidateProperty(propValue, propertyInfo);
            }

            ComponentValidatorAttribute[] attribs = (ComponentValidatorAttribute[])this.GetType().GetCustomAttributes(typeof(ComponentValidatorAttribute), true);
            foreach (ComponentValidatorAttribute attrib in attribs)
            {
                attrib.Validator.EnsureIsValid(this);
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
        protected void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
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

            this.ValidateProperty(value, propertyInfo);
        }

        /// <summary>
        /// Verifies that the value is a valid value for the property.
        /// </summary>
        /// <param name="value">Value being set to the property.</param>
        /// <param name="propertyInfo">The <see cref="PropertyInfo"/> of the property to validate.</param>
        /// <exception cref="ArgumentException"><paramref name="propertyInfo"/> is null.</exception>
        /// <exception cref="ArgumentException">Property not found on configuration type.</exception>
        /// <exception cref="ValidationException">Property was set to an invalid value.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        protected void ValidateProperty(object value, PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }

            PropertyValidatorAttribute[] attribs = (PropertyValidatorAttribute[])propertyInfo.GetCustomAttributes(typeof(PropertyValidatorAttribute), false);
            for (int i = 0; i < attribs.Length; i++)
            {
                attribs[i].Validator.EnsureIsValid(this.GetType().Name + Type.Delimiter + propertyInfo.Name, value, this);
            }
        }
    }
}
