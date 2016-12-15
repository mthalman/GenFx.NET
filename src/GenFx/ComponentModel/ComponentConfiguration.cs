using GenFx.Properties;
using GenFx.Validation;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace GenFx.ComponentModel
{
    /// <summary>
    /// Represents a configuration object for a <see cref="IGeneticComponent"/>.
    /// </summary>
    public interface IComponentConfiguration
    {
        /// <summary>
        /// Gets the type of the component this configuration is associated with.
        /// </summary>
        Type ComponentType { get; }

        /// <summary>
        /// Returns a new instance of the <see cref="IGeneticComponent"/> associated with this configuration.
        /// </summary>
        /// <param name="algorithm">The algorithm associated with the component.</param>
        IGeneticComponent CreateComponent(IGeneticAlgorithm algorithm);
    }

    /// <summary>
    /// Base class for all classes containing configuration settings for a component.
    /// </summary>
    public abstract class ComponentConfiguration : IComponentConfiguration, INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// When overriden, gets the type of the component this configuration is associated with.
        /// </summary>
        public abstract Type ComponentType { get; }

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
        /// Returns a new instance of the <see cref="GeneticComponent"/> associated with this configuration.
        /// </summary>
        /// <param name="algorithm">The algorithm associated with the component.</param>
        public GeneticComponent CreateComponent(IGeneticAlgorithm algorithm)
        {
            try
            {
                return (GeneticComponent)Activator.CreateInstance(this.ComponentType, new object[] { algorithm });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        IGeneticComponent IComponentConfiguration.CreateComponent(IGeneticAlgorithm algorithm)
        {
            return this.CreateComponent(algorithm);
        }
    }

    /// <summary>
    /// Base class for all classes containing configuration settings for a component.
    /// </summary>
    public abstract class ComponentConfiguration<TConfiguration, TComponent> : ComponentConfiguration
        where TConfiguration : ComponentConfiguration<TConfiguration, TComponent>
        where TComponent : GeneticComponent<TComponent, TConfiguration>
    {
        /// <summary>
        /// Gets the type of the component this configuration is associated with.
        /// </summary>
        public override Type ComponentType { get { return typeof(TComponent); } }

        /// <summary>
        /// Returns a new instance of the <typeparamref name="TComponent"/> associated with this configuration.
        /// </summary>
        /// <param name="algorithm">The algorithm associated with the component.</param>
        public new TComponent CreateComponent(IGeneticAlgorithm algorithm)
        {
            return (TComponent)base.CreateComponent(algorithm);
        }
    }
}
