using System.ComponentModel;
using System.Linq;

namespace GenFx.UI
{
    /// <summary>
    /// Contains helper methods related to display names.
    /// </summary>
    internal static class DisplayNameHelper
    {
        /// <summary>
        /// Returns a display name for the specified object.
        /// </summary>
        /// <param name="obj">The object to get a display name for.</param>
        /// <returns>A display name for the specified object.</returns>
        public static string GetDisplayName(object obj)
        {
            DisplayNameAttribute attrib = obj.GetType().GetCustomAttributes(typeof(DisplayNameAttribute), true)
                .Cast<DisplayNameAttribute>()
                .FirstOrDefault();
            if (attrib != null)
            {
                return attrib.DisplayName;
            }

            string toString = obj.ToString();
            if (toString != obj.GetType().FullName)
            {
                return toString;
            }

            return obj.GetType().Name;
        }

        /// <summary>
        /// Returns a display name for the specified object that includes the type info.
        /// </summary>
        /// <param name="obj">The object to get a display name for.</param>
        /// <returns>A display name for the specified object.</returns>
        public static string GetDisplayNameWithTypeInfo(object obj)
        {
            return StringUtil.GetFormattedString("{0} [{1}]",
                DisplayNameHelper.GetDisplayName(obj), obj.GetType().FullName);
        }
    }
}
