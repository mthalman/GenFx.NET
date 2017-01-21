using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.Validation
{
    /// <summary>
    /// Indicates that a component class requires a specific component type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    public abstract class RequiredComponentTypeAttribute : ComponentValidatorAttribute
    {
        private Type requiredType;

        /// <summary>
        /// Gets the component type which is required by the class.
        /// </summary>
        public Type RequiredType
        {
            get { return this.requiredType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredComponentTypeAttribute"/> class.
        /// </summary>
        /// <param name="requiredType">Type which is required by the class.</param>
        /// <param name="baseType">Type that <paramref name="requiredType"/> must be a type of.</param>
        /// <exception cref="ArgumentNullException"><paramref name="requiredType"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="baseType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="requiredType"/> does not derive from <paramref name="baseType"/>.</exception>
        protected RequiredComponentTypeAttribute(Type requiredType, Type baseType)
        {
            if (requiredType == null)
            {
                throw new ArgumentNullException(nameof(requiredType));
            }

            this.requiredType = requiredType;

            if (baseType == null)
            {
                throw new ArgumentNullException(nameof(baseType));
            }

            if (!baseType.IsAssignableFrom(requiredType))
            {
                throw new ArgumentException(
                  StringUtil.GetFormattedString(Resources.ErrorMsg_InvalidType, baseType.FullName),
                  nameof(requiredType));
            }
        }
    }
}
