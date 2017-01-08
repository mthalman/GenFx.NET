using GenFx.Validation;

namespace GenFx.ComponentLibrary.Lists
{
    /// <summary>
    /// Validates the crossover point-related properties of <see cref="MultiPointCrossoverOperatorFactoryConfig{TConfiguration, TCrossover}"/>.
    /// </summary>
    /// <remarks>
    /// Ensures that there can be no more than two crossover points if <see cref="MultiPointCrossoverOperatorFactoryConfig{TConfiguration, TCrossover}.UsePartiallyMatchedCrossover"/> is true.
    /// </remarks>
    internal class MultiPointCrossoverOperatorCrossoverPointValidator : Validator
    {
        public override bool IsValid(object value, string propertyName, object owner, out string errorMessage)
        {
            errorMessage = null;

            IMultiPointCrossoverOperatorFactoryConfig config = (IMultiPointCrossoverOperatorFactoryConfig)owner;
            if (config.UsePartiallyMatchedCrossover && config.CrossoverPointCount > 2)
            {
                errorMessage = StringUtil.GetFormattedString(Resources.ErrorMsg_MultiPointCrossoverOperationCrossoverPointValidator_ValidationError);
            }

            return errorMessage == null;
        }
    }
}
