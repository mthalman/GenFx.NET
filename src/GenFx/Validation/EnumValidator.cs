using System;

namespace GenFx.Validation
{
    /// <summary>
    /// Base class for classes that validate enum values.
    /// </summary>
    public abstract class EnumValidator : PropertyValidator
    {
        private Type enumType;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumValidator"/> class.
        /// </summary>
        /// <param name="enumType">Type of the enum to validate.</param>
        /// <exception cref="ArgumentNullException"><paramref name="enumType"/> is null.</exception>
        protected EnumValidator(Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException(nameof(enumType));
            }

            if (!enumType.IsEnum)
            {
                throw new ArgumentException(StringUtil.GetFormattedString(Resources.ErrorMsg_EnumValidator_NotEnumType, enumType));
            }

            this.enumType = enumType;
        }
        
        /// <summary>
        /// Returns whether <paramref name="value"/> is valid.
        /// </summary>
        /// <param name="value">Object to be validated.</param>
        /// <param name="propertyName">Name of the property being validated.</param>
        /// <param name="owner">The object that owns the property being validated.</param>
        /// <param name="errorMessage">Error message that should be displayed if the property fails validation.</param>
        /// <returns>True if <paramref name="value"/> is valid; otherwise, false.</returns>
        public override bool IsValid(object? value, string propertyName, object owner, out string? errorMessage)
        {
            if (String.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(Resources.ErrorMsg_StringNullOrEmpty, nameof(propertyName));
            }

            if (value == null)
            {
                errorMessage = EnumHelper.GetInvalidEnumMessage(this.enumType);
                return false;
            }

            try
            {
                Enum enumValue = (Enum)Enum.ToObject(this.enumType, value);
                if (!Enum.IsDefined(this.enumType, enumValue))
                {
                    errorMessage = EnumHelper.GetInvalidEnumMessage(this.enumType);
                    return false;
                }
            }
            catch (ArgumentException)
            {
                errorMessage = EnumHelper.GetInvalidEnumMessage(this.enumType);
                return false;
            }

            errorMessage = null;
            return true;
        }
    }
}
