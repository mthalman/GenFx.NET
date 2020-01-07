using GenFx.Validation;

namespace GenFx.Components.Lists
{
    /// <summary>
    /// Represents a component validator for <see cref="ListEntityBase"/> that validates
    /// the <see cref="ListEntityBase.MinimumStartingLength"/> and <see cref="ListEntityBase.MaximumStartingLength"/> values.
    /// </summary>
    internal class ListStartingLengthValidator : ComponentValidator
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

            ListEntityBase entity = (ListEntityBase)component;
            if (entity.MinimumStartingLength > entity.MaximumStartingLength)
            {
                errorMessage = StringUtil.GetFormattedString(
                    Resources.ErrorMsg_MismatchedMinMaxValues,
                    nameof(ListEntityBase.MinimumStartingLength),
                    nameof(ListEntityBase.MaximumStartingLength));
            }

            return errorMessage == null;
        }
    }
}
