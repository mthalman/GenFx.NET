using System;
using System.Reflection;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates how to validate a <see cref="GeneticComponent"/>.
    /// </summary>
    /// <remarks>
    /// This class can be used when a custom <see cref="ComponentValidator"/> type has been defined.  Rather than defining
    /// a new <see cref="ComponentValidatorAttribute"/> type, this class can be used instead.
    /// </remarks>
    public abstract class CustomComponentValidatorBaseAttribute : ComponentValidatorAttribute
    {
        /// <summary>
        /// Gets the <see cref="Type"/> of validator for the <see cref="GeneticComponent"/>.
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
        /// Initializes a new instance of the <see cref="CustomComponentValidatorBaseAttribute"/> class.
        /// </summary>
        /// <param name="validatorType">
        /// <see cref="Type"/> of validator for the <see cref="GeneticComponent"/>. This type must derive from <see cref="ComponentValidator"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="ComponentValidator"/>.</exception>
        protected CustomComponentValidatorBaseAttribute(Type validatorType)
            : this(validatorType, new object[0])
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomComponentValidatorBaseAttribute"/> class.
        /// </summary>
        /// <param name="validatorType">
        /// <see cref="Type"/> of validator for the <see cref="GeneticComponent"/>. This type must derive from <see cref="ComponentValidator"/>.
        /// </param>
        /// <param name="validatorConstructorArguments">
        /// The arguments to pass to the constructor the associated <see cref="ComponentValidator"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="ComponentValidator"/>.</exception>
        protected CustomComponentValidatorBaseAttribute(Type validatorType, params object[] validatorConstructorArguments)
        {
            if (validatorType == null)
            {
                throw new ArgumentNullException(nameof(validatorType));
            }

            this.ValidatorType = validatorType;
            this.ValidatorConstructorArguments = validatorConstructorArguments;

            if (!this.ValidatorType.IsSubclassOf(typeof(ComponentValidator)))
            {
                throw new ArgumentException(StringUtil.GetFormattedString(
                  Resources.ErrorMsg_IncorrectDerivedType, typeof(ComponentValidator).FullName));
            }
        }

        /// <summary>
        /// Returns the associated <see cref="ComponentValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="ComponentValidator"/> object.</returns>
        protected override ComponentValidator CreateValidator()
        {
            try
            {
                return (ComponentValidator)Activator.CreateInstance(this.ValidatorType, this.ValidatorConstructorArguments);
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
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class CustomComponentValidatorAttribute : CustomComponentValidatorBaseAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomComponentValidatorAttribute"/> class.
        /// </summary>
        /// <param name="validatorType">
        /// <see cref="Type"/> of validator for the <see cref="GeneticComponent"/>. This type must derive from <see cref="ComponentValidator"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="ComponentValidator"/>.</exception>
        public CustomComponentValidatorAttribute(Type validatorType)
            : base(validatorType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomComponentValidatorAttribute"/> class.
        /// </summary>
        /// <param name="validatorType">
        /// <see cref="Type"/> of validator for the <see cref="GeneticComponent"/>. This type must derive from <see cref="ComponentValidator"/>.
        /// </param>
        /// <param name="validatorConstructorArguments">
        /// The arguments to pass to the constructor the associated <see cref="ComponentValidator"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="ComponentValidator"/>.</exception>
        public CustomComponentValidatorAttribute(Type validatorType, params object[] validatorConstructorArguments)
            : base(validatorType, validatorConstructorArguments)
        {
        }
    }

    /// <summary>
    /// Indicates how the referenced target configuration property should be validated when set.
    /// </summary>
    /// <remarks>
    /// This class can be used when a custom <see cref="ComponentValidator"/> type has been defined
    /// but no arguments need to be passed to it.  Rather than creating a new <see cref="ComponentValidatorAttribute"/>
    /// type, this class can be used instead.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class CustomExternalComponentValidatorAttribute : CustomComponentValidatorBaseAttribute, IExternalComponentValidatorAttribute
    {
        private Type targetComponentType;
        
        /// <summary>
        /// Gets the <see cref="Type"/> of the <see cref="GeneticComponent"/> to be validated.
        /// </summary>
        public Type TargetComponentType
        {
            get { return this.targetComponentType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomExternalComponentValidatorAttribute"/> class.
        /// </summary>
        /// <param name="targetComponentType"><see cref="Type"/> of the component containing the property to be validated. This type must implement <see cref="GeneticComponent"/>.</param>
        /// <param name="validatorType"><see cref="Type"/> of validator for the configuration property. This
        /// type must derive from <see cref="ComponentValidator"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="targetComponentType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetComponentType"/> does not implement <see cref="GeneticComponent"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="PropertyValidator"/>.</exception>
        public CustomExternalComponentValidatorAttribute(Type targetComponentType, Type validatorType)
            : this(targetComponentType, validatorType, new object[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomExternalComponentValidatorAttribute"/> class.
        /// </summary>
        /// <param name="targetComponentType"><see cref="Type"/> of the component containing the property to be validated. This type must implement <see cref="GeneticComponent"/>.</param>
        /// <param name="validatorType"><see cref="Type"/> of validator for the configuration property. This
        /// type must derive from <see cref="ComponentValidator"/>.</param>
        /// <param name="validatorConstructorArguments">
        /// The arguments to pass to the constructor the associated <see cref="ComponentValidator"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="validatorType"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="targetComponentType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetComponentType"/> does not implement <see cref="GeneticComponent"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="validatorType"/> does not derive from <see cref="PropertyValidator"/>.</exception>
        public CustomExternalComponentValidatorAttribute(Type targetComponentType, Type validatorType, params object[] validatorConstructorArguments)
            : base(validatorType, validatorConstructorArguments)
        {
            ExternalValidatorAttributeHelper.ValidateArguments(targetComponentType);

            this.targetComponentType = targetComponentType;
        }
    }
}
