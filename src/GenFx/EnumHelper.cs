using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenFx.Properties;

namespace GenFx
{
    /// <summary>
    /// Contains helper methods related to enums.
    /// </summary>
    internal static class EnumHelper
    {
        /// <summary>
        /// Returns an <see cref="ArgumentException"/> for an argument that has an invalid enum value.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <returns>An <see cref="ArgumentException"/> for an argument that has an invalid enum value.</returns>
        public static ArgumentException CreateUndefinedEnumException(Type enumType, string paramName)
        {
            return new ArgumentException(EnumHelper.GetInvalidEnumMessage(enumType), paramName);
        }

        /// <summary>
        /// Returns an error message for an invalid enum.
        /// </summary>
        /// <param name="enumType">Type of the enum.</param>
        /// <returns>Invalid enum error message.</returns>
        public static string GetInvalidEnumMessage(Type enumType)
        {
            string enumValues = Enum.GetNames(enumType)
                .Aggregate((val1, val2) => val1 + ", " + val2);

            return StringUtil.GetFormattedString(
                FwkResources.ErrorMsg_InvalidEnum, enumType.Name, enumValues);
        }
    }
}
