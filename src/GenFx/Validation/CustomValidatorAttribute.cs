using GenFx.ComponentModel;
using System;
using System.Reflection;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates how a configuration property should be validated when set.
    /// </summary>
    /// <remarks>
    /// This class can be used when a custom <see cref="Validator"/> type has been defined
    /// but no arguments need to be passed to it.  Rather than creating a new <see cref="ConfigurationValidatorAttribute"/>
    /// type, this class can be used instead.
    /// </remarks>
    public abstract class CustomValidatorBaseAttribute : ConfigurationValidatorAttribute
    {
        private Type validatorType;

        /// <summary>
        /// Gets the <see cref="Type"/> of validator for the configuration property.
        /// </summary>
        public Type ValidatorType
        {
            get { return this.validatorType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomValidatorBaseAttribute"/> class.
        /// </summary>
        /// <param name="validatorType">
        /// <see cref="Type"/> of validator for the configuration property. This type must derive from <see cref="Validator"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="Validator"/>.</exception>
        protected CustomValidatorBaseAttribute(Type validatorType)
        {
            if (validatorType == null)
            {
                throw new ArgumentNullException(nameof(validatorType));
            }

            this.validatorType = validatorType;

            if (!this.validatorType.IsSubclassOf(typeof(Validator)))
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                  Resources.ErrorMsg_IncorrectDerivedType, typeof(Validator).FullName));
            }
        }

        /// <summary>
        /// Returns the associated <see cref="Validator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="Validator"/> object.</returns>
        /// <exception cref="Exception">An exception occurred while creating an instance of <see cref="ValidatorType"/>.</exception>
        protected override Validator CreateValidator()
        {
            try
            {
                return (Validator)Activator.CreateInstance(this.validatorType);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }
    }

    /// <summary>
    /// Indicates how the attributed configuration property should be validated when set.
    /// </summary>
    /// <remarks>
    /// This class can be used when a custom <see cref="Validator"/> type has been defined
    /// but no arguments need to be passed to it.  Rather than creating a new <see cref="ConfigurationValidatorAttribute"/>
    /// type, this class can be used instead.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class CustomValidatorAttribute : CustomValidatorBaseAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomValidatorAttribute"/> class.
        /// </summary>
        /// <param name="validatorType">
        /// <see cref="Type"/> of validator for the configuration property. This type must derive from <see cref="Validator"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="Validator"/>.</exception>
        public CustomValidatorAttribute(Type validatorType)
            : base(validatorType)
        {
        }
    }

    /// <summary>
    /// Indicates how the referenced target configuration property should be validated when set.
    /// </summary>
    /// <remarks>
    /// This class can be used when a custom <see cref="Validator"/> type has been defined
    /// but no arguments need to be passed to it.  Rather than creating a new <see cref="ConfigurationValidatorAttribute"/>
    /// type, this class can be used instead.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class CustomExternalValidatorAttribute : CustomValidatorBaseAttribute, IExternalConfigurationValidatorAttribute
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
        /// Initializes a new instance of the <see cref="CustomExternalValidatorAttribute"/> class.
        /// </summary>
        /// <param name="validatorType"><see cref="Type"/> of validator for the configuration property. This
        /// type must derive from <see cref="Validator"/>.</param>
        /// <param name="targetComponentConfigurationType"><see cref="Type"/> of the component configuration containing the property to be validated. This type must implement <see cref="IComponentConfiguration"/>.</param>
        /// <param name="targetProperty">Property of the <paramref name="targetComponentConfigurationType"/> to be validated.</param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="targetComponentConfigurationType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetComponentConfigurationType"/> does not implement <see cref="IComponentConfiguration"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> does not exist on <paramref name="targetComponentConfigurationType"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="Validator"/>.</exception>
        public CustomExternalValidatorAttribute(Type validatorType, Type targetComponentConfigurationType, string targetProperty)
            : base(validatorType)
        {
            ExternalValidatorAttributeHelper.ValidateArguments(targetComponentConfigurationType, targetProperty);

            this.targetComponentConfigurationType = targetComponentConfigurationType;
            this.targetProperty = targetProperty;
        }
    }
}
