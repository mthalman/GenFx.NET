using System;
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

        /// <summary>
        /// Returns whether the enum value is a defined value.
        /// </summary>
        /// <param name="value">Value to check.</param>
        /// <returns>True if the enum value is defined; otherwise, false.</returns>
        protected override bool IsDefined(Enum value)
        {
            return ReplacementValueKindHelper.IsDefined((ReplacementValueKind)value);
        }
    }
}
