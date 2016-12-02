using System;
using GenFx.ComponentModel;

namespace GenFx.Validation
{
    /// <summary>
    /// Base class for classes that indicate how a <see cref="System.Double"/> configuration property should be validated when set.
    /// </summary>
    public abstract class DoubleValidatorBaseAttribute : ConfigurationValidatorAttribute
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
        /// Returns the associated <see cref="Validator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="Validator"/> object.</returns>
        protected override Validator CreateValidator()
        {
            return new DoubleValidator(this.minValue, this.isMinValueInclusive, this.maxValue, this.isMaxValueInclusive);
        }
    }

    /// <summary>
    /// Indicates how the attributed <see cref="System.Double"/> configuration property should be validated when set.
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
    /// Indicates how the referenced target <see cref="System.Double"/> configuration property should be validated when set.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class DoubleExternalValidatorAttribute : DoubleValidatorBaseAttribute, IExternalConfigurationValidatorAttribute
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
        /// Initializes a new instance of the <see cref="DoubleExternalValidatorAttribute"/> class.
        /// </summary>
        /// <param name="targetComponentConfigurationType"><see cref="Type"/> of the component configuration containing the property to be validated. This type must be a derivative of <see cref="ComponentConfiguration"/>.</param>
        /// <param name="targetProperty">Property of the <paramref name="targetComponentConfigurationType"/> to be validated.</param>
        /// <exception cref="ArgumentNullException"><paramref name="targetComponentConfigurationType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> is null or empty.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetComponentConfigurationType"/> does not derive from <see cref="ComponentConfiguration"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="targetProperty"/> does not exist on <paramref name="targetComponentConfigurationType"/>.</exception>
        public DoubleExternalValidatorAttribute(Type targetComponentConfigurationType, string targetProperty)
        {
            ExternalValidatorAttributeHelper.ValidateArguments(targetComponentConfigurationType, targetProperty);

            this.targetComponentConfigurationType = targetComponentConfigurationType;
            this.targetProperty = targetProperty;
        }
    }
}
