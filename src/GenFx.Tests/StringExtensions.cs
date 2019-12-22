namespace GenFx.Tests
{
    internal static class StringExtensions
    {
        public static string ToCamelCase(this string str)
        {
            return str[0].ToString().ToLower() + str.Substring(1);
        }
    }
}
