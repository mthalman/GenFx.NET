using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Base class for classes that indicate how a <see cref="System.Double"/> property should be validated when set.
    /// </summary>
    public abstract class DoubleValidatorBaseAttribute : PropertyValidatorAttribute
    {
        private double minValue = Double.MinValue;
        private double maxValue = Double.MaxValue;
        private bool isMinValueInclusive = true;
        private bool isMaxValueInclusive = true;

        /// <summary>
        /// Gets or sets the maximum value the <see cref="System.Double"/> property can have in order to be valid.
        /// </summary>
        public double MaxValue
        {
            get { return this.maxValue; }
            set { this.maxValue = value; }
        }

        /// <summary>
        /// Gets or sets the minimum value the <see cref="System.Double"/> property must have in order to be valid.
        /// </summary>
        public double MinValue
        {
            get { return this.minValue; }
            set { this.minValue = value; }
        }

        /// <summary>
        /// Gets or sets whether the <see cref="MinValue"/> should be regarded as an inclusive value 
        /// when validating the range.
        /// </summary>
        /// <remarks>The default value is true.</remarks>
        public bool IsMinValueInclusive
        {
            get { return this.isMinValueInclusive; }
            set { this.isMinValueInclusive = value; }
        }

        /// <summary>
        /// Gets or sets whether the <see cref="MaxValue"/> should be regarded as an inclusive value 
        /// when validating the range.
        /// </summary>
        /// <remarks>The default value is true.</remarks>
        public bool IsMaxValueInclusive
        {
            get { return this.isMaxValueInclusive; }
            set { this.isMaxValueInclusive = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleValidatorBaseAttribute"/> class.
        /// </summary>
        protected DoubleValidatorBaseAttribute()
        {
        }

        /// <summary>
        /// Returns the associated <see cref="PropertyValidator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="PropertyValidator"/> object.</returns>
        protected override PropertyValidator CreateValidator()
        {
            return new DoubleValidator(this.minValue, this.isMinValueInclusive, this.maxValue, this.isMaxValueInclusive);
        }
    }

    /// <summary>
    /// Indicates how the attributed <see cref="System.Double"/> property should be validated when set.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DoubleValidatorAttribute : DoubleValidatorBaseAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleValidatorAttribute"/> class.
        /// </summary>
        public DoubleValidatorAttribute()
        {
        }
    }

    /// <summary>
    /// Indicates how the referenced target <see cref="System.Double"/> property should be validated when set.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class DoubleExternalValidatorAttribute : DoubleValidatorBaseAttribute, IExternalConfigurationPropertyValidatorAttribute
    {
        /// <summary>
        /// Gets the name of the property of the component configuration type to be validated.
        /// </summary>
        public string TargetPropertyName { get; }

        /// <summary>
        /// Gets the <see cref="Type"/> of the component containing the property to be validated.
        /// </summary>
        public Type TargetComponentType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleExternalValidatorAttribute"/> class.
        /// </summary>
        /// <param name="targetComponentType"><see cref="Type"/> of the component configuration containing the property to be validated. This type must implement <see cref="GeneticComponent"/>.</param>
        /// <param name="targetPropertyName">Property of the <paramref name="targetComponentType"/> to be validated.</param>
        /// <exception cref="ArgumentNullException"><paramref name="targetComponentType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetPropertyName"/> is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetComponentType"/> does not implement <see cref="GeneticComponent"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetPropertyName"/> does not exist on <paramref name="targetComponentType"/>.</exception>
        public DoubleExternalValidatorAttribute(Type targetComponentType, string targetPropertyName)
        {
#pragma warning disable CA1062 // Validate arguments of public methods
            ExternalValidatorAttributeHelper.ValidateArguments(targetComponentType, targetPropertyName);
#pragma warning restore CA1062 // Validate arguments of public methods

            this.TargetComponentType = targetComponentType;
            this.TargetPropertyName = targetPropertyName;
        }
    }
}
