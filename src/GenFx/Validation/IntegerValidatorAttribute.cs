using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates how an <see cref="Int32"/> configuration property should be validated when set.
    /// </summary>
    public abstract class IntegerValidatorBaseAttribute : ConfigurationValidatorAttribute
    {
        /// <summary>
        /// Gets or sets the maximum value the integer property can have in order to be valid.
        /// </summary>
        public int MaxValue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the minimum value the integer property must have in order to be valid.
        /// </summary>
        public int MinValue
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerValidatorBaseAttribute"/> class.
        /// </summary>
        protected IntegerValidatorBaseAttribute()
        {
            this.MinValue = Int32.MinValue;
            this.MaxValue = Int32.MaxValue;
        }

        /// <summary>
        /// Returns the associated <see cref="Validator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="Validator"/> object.</returns>
        protected override Validator CreateValidator()
        {
            return new IntegerValidator(this.MinValue, this.MaxValue);
        }
    }

    /// <summary>
    /// Indicates how the attributed <see cref="System.Int32"/> configuration property should be validated when set.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IntegerValidatorAttribute : IntegerValidatorBaseAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntegerValidatorAttribute"/> class.
        /// </summary>
        public IntegerValidatorAttribute()
        {
        }
    }

    /// <summary>
    /// Indicates how the referenced target <see cref="System.Int32"/> property should be validated when set.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class IntegerExternalValidatorAttribute : IntegerValidatorBaseAttribute, IExternalConfigurationValidatorAttribute
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
        /// Initializes a new instance of the <see cref="IntegerExternalValidatorAttribute"/> class.
        /// </summary>
        /// <param name="targetComponentType"><see cref="Type"/> of the component containing the property to be validated. This type must implement <see cref="GeneticComponent"/>.</param>
        /// <param name="targetProperty">Property of the <paramref name="targetComponentType"/> to be validated.</param>
        /// <exception cref="ArgumentNullException"><paramref name="targetComponentType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetComponentType"/> does not implement <see cref="GeneticComponent"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> does not exist on <paramref name="targetComponentType"/>.</exception>
        public IntegerExternalValidatorAttribute(Type targetComponentType, string targetProperty)
        {
            ExternalValidatorAttributeHelper.ValidateArguments(targetComponentType, targetProperty);

            this.targetComponentType = targetComponentType;
            this.targetProperty = targetProperty;
        }
    }
}
