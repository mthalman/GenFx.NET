using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates how an <see cref="Boolean"/> property should be validated when set.
    /// </summary>
    public abstract class BooleanValidatorBaseAttribute : PropertyValidatorAttribute
    {
        /// <summary>
        /// Gets the boolean value that the property must have.
        /// </summary>
        public bool RequiredValue
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanValidatorAttribute"/> class.
        /// </summary>
        /// <param name="requiredValue">The boolean value that the property must have.</param>
        protected BooleanValidatorBaseAttribute(bool requiredValue)
        {
            this.RequiredValue = requiredValue;
        }
        
        /// <summary>
        /// Returns the associated <see cref="PropertyValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="PropertyValidator"/> object.</returns>
        protected override PropertyValidator CreateValidator()
        {
            return new BooleanValidator(this.RequiredValue);
        }
    }

    /// <summary>
    /// Indicates how the attributed <see cref="System.Boolean"/> property should be validated when set.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class BooleanValidatorAttribute : BooleanValidatorBaseAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanValidatorAttribute"/> class.
        /// </summary>
        /// <param name="requiredValue">The boolean value that the property must have.</param>
        public BooleanValidatorAttribute(bool requiredValue)
            : base(requiredValue)
        {
        }
    }

    /// <summary>
    /// Indicates how the referenced target <see cref="System.Boolean"/> property should be validated when set.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class BooleanExternalValidatorAttribute : BooleanValidatorBaseAttribute, IExternalConfigurationPropertyValidatorAttribute
    {
        /// <summary>
        /// Gets the name of the property of the component configuration type to be validated.
        /// </summary>
        public string TargetPropertyName { get; }

        /// <summary>
        /// Gets the <see cref="Type"/> of the component configuration containing the property to be validated.
        /// </summary>
        public Type TargetComponentType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerExternalValidatorAttribute"/> class.
        /// </summary>
        /// <param name="targetComponentType"><see cref="Type"/> of the component containing the property to be validated. This type must derive from <see cref="GeneticComponent"/>.</param>
        /// <param name="targetPropertyName">Property of the <paramref name="targetComponentType"/> to be validated.</param>
        /// <param name="requiredValue">The boolean value that the property must have.</param>
        /// <exception cref="ArgumentNullException"><paramref name="targetComponentType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetPropertyName"/> is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetComponentType"/> does not implement <see cref="GeneticComponent"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetPropertyName"/> does not exist on <paramref name="targetComponentType"/>.</exception>
        public BooleanExternalValidatorAttribute(Type targetComponentType, string targetPropertyName, bool requiredValue)
            : base(requiredValue)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            ExternalValidatorAttributeHelper.ValidateArguments(targetComponentType, targetPropertyName);
#pragma warning restore CA1062 // Validate arguments of public methods

            this.TargetComponentType = targetComponentType;
            this.TargetPropertyName = targetPropertyName;
        }
    }
}
