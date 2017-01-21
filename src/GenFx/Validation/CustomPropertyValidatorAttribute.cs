using System;
using System.Reflection;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates how a configuration property should be validated when set.
    /// </summary>
    /// <remarks>
    /// This class can be used when a custom <see cref="PropertyValidator"/> type has been defined.  Rather than creating
    /// a new <see cref="PropertyValidatorAttribute"/> type, this class can be used instead.
    /// </remarks>
    public abstract class CustomPropertyValidatorBaseAttribute : PropertyValidatorAttribute
    {
        /// <summary>
        /// Gets the <see cref="Type"/> of validator for the component property.
        /// </summary>
        public Type ValidatorType
        {
            get;
        }

        /// <summary>
        /// Gets the arguments to pass to the constructor the associated <see cref="ComponentValidator"/>.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public object[] ValidatorConstructorArguments
        {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPropertyValidatorBaseAttribute"/> class.
        /// </summary>
        /// <param name="validatorType">
        /// <see cref="Type"/> of validator for the configuration property. This type must derive from <see cref="PropertyValidator"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="PropertyValidator"/>.</exception>
        protected CustomPropertyValidatorBaseAttribute(Type validatorType)
            : this(validatorType, new object[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPropertyValidatorBaseAttribute"/> class.
        /// </summary>
        /// <param name="validatorType">
        /// <see cref="Type"/> of validator for the configuration property. This type must derive from <see cref="PropertyValidator"/>.
        /// </param>
        /// <param name="validatorConstructorArguments">
        /// The arguments to pass to the constructor the associated <see cref="ComponentValidator"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="PropertyValidator"/>.</exception>
        protected CustomPropertyValidatorBaseAttribute(Type validatorType, params object[] validatorConstructorArguments)
        {
            if (validatorType == null)
            {
                throw new ArgumentNullException(nameof(validatorType));
            }

            this.ValidatorType = validatorType;
            this.ValidatorConstructorArguments = validatorConstructorArguments;

            if (!this.ValidatorType.IsSubclassOf(typeof(PropertyValidator)))
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                  Resources.ErrorMsg_IncorrectDerivedType, typeof(PropertyValidator).FullName));
            }
        }

        /// <summary>
        /// Returns the associated <see cref="PropertyValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="PropertyValidator"/> object.</returns>
        /// <exception cref="Exception">An exception occurred while creating an instance of <see cref="ValidatorType"/>.</exception>
        protected override PropertyValidator CreateValidator()
        {
            try
            {
                return (PropertyValidator)Activator.CreateInstance(this.ValidatorType, this.ValidatorConstructorArguments);
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
    /// This class can be used when a custom <see cref="PropertyValidator"/> type has been defined
    /// but no arguments need to be passed to it.  Rather than creating a new <see cref="PropertyValidatorAttribute"/>
    /// type, this class can be used instead.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class CustomPropertyValidatorAttribute : CustomPropertyValidatorBaseAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPropertyValidatorAttribute"/> class.
        /// </summary>
        /// <param name="validatorType">
        /// <see cref="Type"/> of validator for the configuration property. This type must derive from <see cref="PropertyValidator"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="PropertyValidator"/>.</exception>
        public CustomPropertyValidatorAttribute(Type validatorType)
            : base(validatorType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPropertyValidatorAttribute"/> class.
        /// </summary>
        /// <param name="validatorType">
        /// <see cref="Type"/> of validator for the configuration property. This type must derive from <see cref="PropertyValidator"/>.
        /// </param>
        /// <param name="validatorConstructorArguments">
        /// The arguments to pass to the constructor the associated <see cref="ComponentValidator"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="PropertyValidator"/>.</exception>
        public CustomPropertyValidatorAttribute(Type validatorType, params object[] validatorConstructorArguments)
            : base(validatorType, validatorConstructorArguments)
        {
        }
    }

    /// <summary>
    /// Indicates how the referenced target configuration property should be validated when set.
    /// </summary>
    /// <remarks>
    /// This class can be used when a custom <see cref="PropertyValidator"/> type has been defined
    /// but no arguments need to be passed to it.  Rather than creating a new <see cref="PropertyValidatorAttribute"/>
    /// type, this class can be used instead.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class CustomExternalValidatorAttribute : CustomPropertyValidatorBaseAttribute, IExternalConfigurationPropertyValidatorAttribute
    {
        private string targetProperty;
        private Type targetComponentType;

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
        public Type TargetComponentType
        {
            get { return this.targetComponentType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomExternalValidatorAttribute"/> class.
        /// </summary>
        /// <param name="targetComponentType"><see cref="Type"/> of the component containing the property to be validated. This type must implement <see cref="GeneticComponent"/>.</param>
        /// <param name="targetProperty">Property of the <paramref name="targetComponentType"/> to be validated.</param>
        /// <param name="validatorType"><see cref="Type"/> of validator for the configuration property. This
        /// type must derive from <see cref="PropertyValidator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="targetComponentType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetComponentType"/> does not implement <see cref="GeneticComponent"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> does not exist on <paramref name="targetComponentType"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="PropertyValidator"/>.</exception>
        public CustomExternalValidatorAttribute(Type targetComponentType, string targetProperty, Type validatorType)
            : this(targetComponentType, targetProperty, validatorType, new object[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomExternalValidatorAttribute"/> class.
        /// </summary>
        /// <param name="targetComponentType"><see cref="Type"/> of the component containing the property to be validated. This type must implement <see cref="GeneticComponent"/>.</param>
        /// <param name="targetProperty">Property of the <paramref name="targetComponentType"/> to be validated.</param>
        /// <param name="validatorType"><see cref="Type"/> of validator for the configuration property. This
        /// type must derive from <see cref="PropertyValidator"/>.</param>
        /// <param name="validatorConstructorArguments">
        /// The arguments to pass to the constructor the associated <see cref="ComponentValidator"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="targetComponentType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetComponentType"/> does not implement <see cref="GeneticComponent"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> does not exist on <paramref name="targetComponentType"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="PropertyValidator"/>.</exception>
        public CustomExternalValidatorAttribute(Type targetComponentType, string targetProperty, Type validatorType, params object[] validatorConstructorArguments)
            : base(validatorType, validatorConstructorArguments)
        {
            ExternalValidatorAttributeHelper.ValidateArguments(targetComponentType, targetProperty);

            this.targetComponentType = targetComponentType;
            this.targetProperty = targetProperty;
        }
    }
}
