using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace GenFx
{
    /// <summary>
    /// Helper class providing string-related functionality.
    /// </summary>
    internal static class StringUtil
    {
        /// <summary>
        /// Fixes a string from the resource file by replacing literal escape strings with actual escape strings.
        /// </summary>
        /// <param name="val">String value from resource file.</param>
        /// <returns>Fixed resource file string.</returns>
        private static string GetFixedResourceString(string val)
        {
            return val.Replace(@"\n", "\n").Replace(@"\t", "\t").Replace(@"\\", "\\");
        }

        /// <summary>
        /// Formats <paramref name="format"/> with <paramref name="args"/> using the appropriate culture.
        /// </summary>
        /// <param name="format">Format string to be replaced with values from <paramref name="args"/>.</param>
        /// <param name="args">Values to apply to <paramref name="format"/>.</param>
        /// <returns>Formatted string.</returns>
        internal static string GetFormattedString(string format, params object[] args)
        {
            return String.Format(CultureInfo.CurrentCulture, GetFixedResourceString(format), args);
        }
    }
}
