using System;
using System.Linq;

namespace GenFx
{
    /// <summary>
    /// Contains helper methods related to enums.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Returns an <see cref="ArgumentException"/> for an argument that has an invalid enum value.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <returns>An <see cref="ArgumentException"/> for an argument that has an invalid enum value.</returns>
        public static ArgumentException CreateUndefinedEnumException(Type enumType, string parameterName)
        {
            return new ArgumentException(EnumHelper.GetInvalidEnumMessage(enumType), parameterName);
        }

        /// <summary>
        /// Returns an error message for an invalid enum.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns>Invalid enum error message.</returns>
        public static string GetInvalidEnumMessage(Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException(nameof(enumType));
            }

            string enumValues = Enum.GetNames(enumType)
                .Aggregate((val1, val2) => val1 + ", " + val2);

            return StringUtil.GetFormattedString(
                Resources.ErrorMsg_InvalidEnum, enumType.Name, enumValues);
        }
    }
}
