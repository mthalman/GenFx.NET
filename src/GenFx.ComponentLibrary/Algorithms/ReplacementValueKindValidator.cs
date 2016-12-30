using GenFx.Validation;

namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// Provides validation for a <see cref="ReplacementValueKind"/> value.
    /// </summary>
    public class ReplacementValueKindValidator : EnumValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReplacementValueKindValidator"/> class.
        /// </summary>
        public ReplacementValueKindValidator()
            : base(typeof(ReplacementValueKind))
        {
        }
    }
}
