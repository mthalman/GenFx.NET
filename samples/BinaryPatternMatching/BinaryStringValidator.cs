using GenFx.Validation;
using System;
using System.Linq;

namespace BinaryPatternMatching
{
    /// <summary>
    /// Validator used to validate that the value of a component configuration property is a binary string.
    /// </summary>
    public class BinaryStringValidator : PropertyValidator
    {
        public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
        {
            string binaryString = value as string;
            if (!String.IsNullOrEmpty(binaryString))
            {
                if (binaryString.All(c => c == '0' || c == '1'))
                {
                    errorMessage = null;
                    return true;
                }
            }

            errorMessage = "Must be a binary string that contains only 0's and 1's.";
            return false;
        }
    }
}
