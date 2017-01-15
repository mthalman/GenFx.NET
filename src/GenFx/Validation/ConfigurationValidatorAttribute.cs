using GenFx.Contracts;
using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates how a configuration property should be validated when set.
    /// </summary>
    public abstract class ConfigurationValidatorAttribute : Attribute
    {
        private Validator validator;

        /// <summary>
        /// Gets the validator used to verify the value of the property.
        /// </summary>
        public Validator Validator
        {
            get
            {
                if (this.validator == null)
                {
                    this.validator = this.CreateValidator();
                }
                return this.validator;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationValidatorAttribute"/> class.
        /// </summary>
        protected ConfigurationValidatorAttribute()
        {
        }

        /// <summary>
        /// When overriden by a derived class, returns the associated <see cref="Validator"/> object.
        /// </summary>
        /// <returns>The associated <see cref="Validator"/> object.</returns>
        protected abstract Validator CreateValidator();
    }

    /// <summary>
    /// Represents an <see cref="Attribute"/> that describes how a target component configuration property should be validated when set.
    /// </summary>
    /// <remarks>
    /// Attributes that implement this interface can be attached to components that need to describe
    /// how validation should be done for a configuration property external to the component when the component is being used.
    /// For example, a <see cref="IFitnessEvaluator"/> type may require that a binary string entity
    /// have a specific length.<br />
    /// Note to developers: if creating your own <see cref="ConfigurationValidatorAttribute"/>, it is a best
    /// practice to also create a version of the attribute that implements the <see cref="IExternalConfigurationValidatorAttribute"/>
    /// interface.  This allows third-party developers to use your attribute like they would a built-in GenFx attribute.
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    public interface IExternalConfigurationValidatorAttribute
    {
        /// <summary>
        /// Gets the name of the property of the component configuration type to be validated.
        /// </summary>
        /// <seealso cref="TargetComponentType"/>
        string TargetProperty
        {
            get;
        }

        /// <summary>
        /// Gets the <see cref="Type"/> of the component containing the property to be validated.
        /// </summary>
        Type TargetComponentType
        {
            get;
        }

        /// <summary>
        /// Gets the validator used to verify the value of the property.
        /// </summary>
        Validator Validator
        {
            get;
        }
    }
}
