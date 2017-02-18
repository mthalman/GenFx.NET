using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Represents an <see cref="Attribute"/> that describes how a target component configuration property should be validated when set.
    /// </summary>
    /// <remarks>
    /// Attributes that implement this interface can be attached to components that need to describe
    /// how validation should be done for a configuration property external to the component when the component is being used.
    /// For example, a <see cref="FitnessEvaluator"/> type may require that a binary string entity
    /// have a specific length.<br />
    /// Note to developers: if creating your own <see cref="PropertyValidatorAttribute"/>, it is a best
    /// practice to also create a version of the attribute that implements the <see cref="IExternalConfigurationPropertyValidatorAttribute"/>
    /// interface.  This allows third-party developers to use your attribute like they would a built-in GenFx attribute.
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    public interface IExternalConfigurationPropertyValidatorAttribute
    {
        /// <summary>
        /// Gets the name of the property of the component configuration type to be validated.
        /// </summary>
        /// <seealso cref="TargetComponentType"/>
        string TargetPropertyName
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
        PropertyValidator Validator
        {
            get;
        }
    }
}
