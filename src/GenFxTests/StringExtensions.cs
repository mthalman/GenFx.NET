using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenFxTests
{
    internal static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            return str[0].ToString().ToLower() + str.Substring(1);
        }
    }
}
