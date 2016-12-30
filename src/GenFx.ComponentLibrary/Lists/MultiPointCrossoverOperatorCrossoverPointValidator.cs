using GenFx.ComponentLibrary.Properties;
using GenFx.Validation;

namespace GenFx.ComponentLibrary.Lists
{
    internal class MultiPointCrossoverOperationCrossoverPointValidator : Validator
    {
        public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
        {
            errorMessage = null;

            IMultiPointCrossoverOperatorConfiguration config = (IMultiPointCrossoverOperatorConfiguration)owner;
            if (config.UsePartiallyMatchedCrossover && config.CrossoverPointCount > 2)
            {
                errorMessage = StringUtil.GetFormattedString(LibResources.ErrorMsg_MultiPointCrossoverOperationCrossoverPointValidator_ValidationError);
            }

            return errorMessage == null;
        }
    }
}
