using GenFx.Validation;

namespace GenFx.Components.Lists
{
    /// <summary>
    /// Validates the crossover point-related properties of <see cref="MultiPointCrossoverOperator"/>.
    /// </summary>
    /// <remarks>
    /// Ensures that there can be no more than two crossover points if <see cref="ListEntityBase.RequiresUniqueElementValues"/> is true.
    /// </remarks>
    internal class MultiPointCrossoverOperatorCrossoverPointValidator : ComponentValidator
    {
        /// <summary>
        /// Returns whether <paramref name="component"/> is valid.
        /// </summary>
        /// <param name="component"><see cref="GeneticComponent"/> to be validated.</param>
        /// <param name="errorMessage">Error message that should be displayed if the component fails validation.</param>
        /// <returns>True if <paramref name="component"/> is valid; otherwise, false.</returns>
        public override bool IsValid(GeneticComponent component, out string? errorMessage)
        {
            errorMessage = null;

            MultiPointCrossoverOperator crossoverOp = (MultiPointCrossoverOperator)component;

            if (((ListEntityBase?)crossoverOp.Algorithm?.GeneticEntitySeed)?.RequiresUniqueElementValues == true &&
                crossoverOp.CrossoverPointCount > 2)
            {
                errorMessage = StringUtil.GetFormattedString(Resources.ErrorMsg_MultiPointCrossoverOperationCrossoverPointValidator_ValidationError);
            }

            return errorMessage == null;
        }
    }
}
