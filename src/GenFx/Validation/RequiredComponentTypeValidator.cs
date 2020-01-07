using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace GenFx.Validation
{
    /// <summary>
    /// Validates a component such that when it is used in a genetic algorithm, the genetic algorithm is also configured
    /// to use the required component type.
    /// </summary>
    public abstract class RequiredComponentTypeValidator : ComponentValidator
    {
        /// <summary>
        /// Gets the component type that is required by the component in context.
        /// </summary>
        public Type RequiredComponentType { get; }

        /// <summary>
        /// When overriden by a derived class, gets the friendly name of the component type.
        /// </summary>
        protected abstract string ComponentFriendlyName { get; }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="requiredComponentType">The component type that is required by the component in context.</param>
        /// <param name="baseType">Type that <paramref name="requiredComponentType"/> must be a type of.</param>
        protected RequiredComponentTypeValidator(Type requiredComponentType, Type baseType)
        {
            if (requiredComponentType == null)
            {
                throw new ArgumentNullException(nameof(requiredComponentType));
            }

            if (baseType == null)
            {
                throw new ArgumentNullException(nameof(baseType));
            }

            if (!baseType.IsAssignableFrom(requiredComponentType))
            {
                throw new ArgumentException(
                  StringUtil.GetFormattedString(Resources.ErrorMsg_InvalidType, baseType.FullName),
                  nameof(requiredComponentType));
            }

            this.RequiredComponentType = requiredComponentType;
        }

        /// <summary>
        /// Returns whether <paramref name="component"/> is valid.
        /// </summary>
        /// <param name="component"><see cref="GeneticComponent"/> to be validated.</param>
        /// <param name="errorMessage">Error message that should be displayed if the component fails validation.</param>
        /// <returns>True if <paramref name="component"/> is valid; otherwise, false.</returns>
        [SuppressMessage("Microsoft.Design", "CA1021:AvoidOutParameters", MessageId = "3#")]
        public override sealed bool IsValid(GeneticComponent component, out string? errorMessage)
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            GeneticAlgorithm? algorithmContext;
            if (component is GeneticComponentWithAlgorithm componentWithAlg)
            {
                algorithmContext = componentWithAlg.Algorithm;
            }
            else
            {
                algorithmContext = component as GeneticAlgorithm;
            }

            if (algorithmContext is null)
            {
                throw new ArgumentException(
                    StringUtil.GetFormattedString(Resources.ErrorMsg_RequiredComponentValidator_NoAlgorithm,
                        typeof(GeneticAlgorithm), typeof(GeneticComponentWithAlgorithm)),
                    nameof(component));
            }

            if (!this.HasRequiredComponent(algorithmContext))
            {
                errorMessage = StringUtil.GetFormattedString(Resources.ErrorMsg_NoRequiredConfigurableType,
                    component.GetType().FullName, this.ComponentFriendlyName.ToLower(CultureInfo.CurrentCulture), this.RequiredComponentType.FullName);

                return false;
            }

            errorMessage = null;
            return true;
        }

        /// <summary>
        /// When overriden by a derived class, returns a value indicating whether the genetic algorithm is configured with the required component.
        /// </summary>
        /// <param name="algorithmContext">The <see cref="GeneticAlgorithm"/> currently in context.</param>
        /// <returns>true if the genetic algorithm is configured with the required component; otherwise, false.</returns>
        protected abstract bool HasRequiredComponent(GeneticAlgorithm algorithmContext);

        /// <summary>
        /// Returns a value indicating whether the specified component has an equivalent type to the required component type.
        /// </summary>
        /// <param name="configuredComponent"><see cref="GeneticComponent"/> to check.</param>
        /// <returns>true if the specified component has an equivalent type to the required component type; otherwise, false.</returns>
        protected bool IsEquivalentType(GeneticComponent? configuredComponent)
        {
            if (configuredComponent == null)
            {
                return false;
            }

            return this.RequiredComponentType.IsAssignableFrom(configuredComponent.GetType());
        }
    }
}
