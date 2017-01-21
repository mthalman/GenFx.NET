using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Represents an <see cref="Attribute"/> that describes how a target <see cref="GeneticComponent"/> should be validated.
    /// </summary>
    /// <remarks>
    /// Attributes that implement this interface can be attached to components that need to describe
    /// how validation should be done for some other component when the attributed component is being used.
    /// Note to developers: if creating your own <see cref="ComponentValidatorAttribute"/>, it is a best
    /// practice to also create a version of the attribute that implements the <see cref="IExternalComponentValidatorAttribute"/>
    /// interface.  This allows third-party developers to use your attribute like they would a built-in GenFx attribute.
    /// </remarks>
    [SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix")]
    public interface IExternalComponentValidatorAttribute
    {
        /// <summary>
        /// Gets the <see cref="Type"/> of the component to be validated.
        /// </summary>
        Type TargetComponentType
        {
            get;
        }

        /// <summary>
        /// Gets the validator used to verify the <see cref="GeneticComponent"/>.
        /// </summary>
        ComponentValidator Validator
        {
            get;
        }
    }
}
