using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Base class for configuration classes that indicate that a configuration property is required to be set.
    /// </summary>
    public abstract class RequiredValidatorBaseAttribute : PropertyValidatorAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredValidatorBaseAttribute"/> class.
        /// </summary>
        protected RequiredValidatorBaseAttribute()
        {
        }

        /// <summary>
        /// Returns the associated <see cref="PropertyValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="PropertyValidator"/> object.</returns>
        protected override PropertyValidator CreateValidator()
        {
            return new RequiredValidator();
        }
    }

    /// <summary>
    /// Indicates that the attributed configuration property is required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class RequiredValidatorAttribute : RequiredValidatorBaseAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredValidatorAttribute"/> class.
        /// </summary>
        public RequiredValidatorAttribute()
        {
        }
    }

    /// <summary>
    /// Indicates that the referenced target configuration property is required.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class RequiredExternalValidatorAttribute : RequiredValidatorBaseAttribute, IExternalConfigurationPropertyValidatorAttribute
    {
        private string targetPropertyName;
        private Type targetComponentType;

        /// <summary>
        /// Gets the name of the property of the component configuration type to be validated.
        /// </summary>
        public string TargetPropertyName
        {
            get { return this.targetPropertyName; }
        }

        /// <summary>
        /// Gets the <see cref="Type"/> of the component configuration containing the property to be validated.
        /// </summary>
        public Type TargetComponentType
        {
            get { return this.targetComponentType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredExternalValidatorAttribute"/> class.
        /// </summary>
        /// <param name="targetComponentType"><see cref="Type"/> of the component containing the property to be validated. This type must implement <see cref="GeneticComponent"/>.</param>
        /// <param name="targetPropertyName">Property of the <paramref name="targetComponentType"/> to be validated.</param>
        /// <exception cref="ArgumentNullException"><paramref name="targetComponentType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetPropertyName"/> is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetComponentType"/> does not implement <see cref="GeneticComponent"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetPropertyName"/> does not exist on <paramref name="targetComponentType"/>.</exception>
        public RequiredExternalValidatorAttribute(Type targetComponentType, string targetPropertyName)
        {
            ExternalValidatorAttributeHelper.ValidateArguments(targetComponentType, targetPropertyName);

            this.targetComponentType = targetComponentType;
            this.targetPropertyName = targetPropertyName;
        }
    }
}
