using System;
using GenFx.ComponentModel;

namespace GenFx.Validation
{
    /// <summary>
    /// Base class for classes that indicate that a configuration property is required to be set.
    /// </summary>
    public abstract class RequiredValidatorBaseAttribute : ConfigurationValidatorAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredValidatorBaseAttribute"/> class.
        /// </summary>
        protected RequiredValidatorBaseAttribute()
        {
        }

        /// <summary>
        /// Returns the associated <see cref="Validator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="Validator"/> object.</returns>
        protected override Validator CreateValidator()
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
    public sealed class RequiredExternalValidatorAttribute : RequiredValidatorBaseAttribute, IExternalConfigurationValidatorAttribute
    {
        private string targetProperty;
        private Type targetComponentConfigurationType;

        /// <summary>
        /// Gets the name of the property of the component configuration type to be validated.
        /// </summary>
        public string TargetProperty
        {
            get { return this.targetProperty; }
        }

        /// <summary>
        /// Gets the <see cref="Type"/> of the component configuration containing the property to be validated.
        /// </summary>
        public Type TargetComponentConfigurationType
        {
            get { return this.targetComponentConfigurationType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredExternalValidatorAttribute"/> class.
        /// </summary>
        /// <param name="targetComponentConfigurationType"><see cref="Type"/> of the component configuration containing the property to be validated. This type must be a derivative of <see cref="ComponentConfiguration"/>.</param>
        /// <param name="targetProperty">Property of the <paramref name="targetComponentConfigurationType"/> to be validated.</param>
        /// <exception cref="ArgumentNullException"><paramref name="targetComponentConfigurationType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetComponentConfigurationType"/> does not derive from <see cref="ComponentConfiguration"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> does not exist on <paramref name="targetComponentConfigurationType"/>.</exception>
        public RequiredExternalValidatorAttribute(Type targetComponentConfigurationType, string targetProperty)
        {
            ExternalValidatorAttributeHelper.ValidateArguments(targetComponentConfigurationType, targetProperty);

            this.targetComponentConfigurationType = targetComponentConfigurationType;
            this.targetProperty = targetProperty;
        }
    }
}
