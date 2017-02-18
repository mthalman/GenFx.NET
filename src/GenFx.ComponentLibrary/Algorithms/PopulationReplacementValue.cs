using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// Represents the value indicating how many <see cref="GeneticEntity"/> objects are to 
    /// be replaced with the offspring of the previous generation.
    /// </summary>
    /// <seealso cref="SteadyStateGeneticAlgorithm"/>
    [DataContract]
    public struct PopulationReplacementValue
    {
        [DataMember]
        private int replacementValue;

        [DataMember]
        private ReplacementValueKind kind;

        /// <summary>
        /// Gets the kind of value being used to indicate the number of <see cref="GeneticEntity"/> objects
        /// to replace.
        /// </summary>
        public ReplacementValueKind Kind
        {
            get { return this.kind; }
        }

        /// <summary>
        /// Gets the number of <see cref="GeneticEntity"/> objects to be replaced.
        /// </summary>
        public int Value
        {
            get { return this.replacementValue; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PopulationReplacementValue"/> class.
        /// </summary>
        /// <param name="value">Number of <see cref="GeneticEntity"/> objects to be replaced.</param>
        /// <param name="kind">
        /// Kind of value being used to indicate the number of <see cref="GeneticEntity"/> objects to replace.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="value"/> is less than zero.</exception>
        public PopulationReplacementValue(int value, ReplacementValueKind kind)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("value", value, StringUtil.GetFormattedString(
                  Resources.ErrorMsg_PopulationReplacementValue_LessThanZero, value));
            }
            
            if (!Enum.IsDefined(typeof(ReplacementValueKind), kind))
            {
                throw EnumHelper.CreateUndefinedEnumException(typeof(ReplacementValueKind), "kind");
            }

            this.replacementValue = value;
            this.kind = kind;
        }

        /// <summary>
        /// Indictates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns>true if this instance and the specified object are equal; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(PopulationReplacementValue))
            {
                return false;
            }

            PopulationReplacementValue val = (PopulationReplacementValue)obj;

            if (this.kind == val.kind && this.replacementValue == val.replacementValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>Hash code for this instance.</returns>
        public override int GetHashCode()
        {
            string combinedVal = this.replacementValue.ToString(CultureInfo.InvariantCulture) + Enum.GetName(typeof(ReplacementValueKind), this.kind);
            return combinedVal.GetHashCode();
        }

        /// <summary>
        /// Returns the string representation of this object.
        /// </summary>
        /// <returns>String representation of this object.</returns>
        public override string ToString()
        {
            if (this.Kind == ReplacementValueKind.Percentage)
            {
                return this.Value.ToString(CultureInfo.InvariantCulture) + "%";
            }
            else
            {
                return this.Value.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Indictates whether <paramref name="value1"/> is equal to <paramref name="value2"/>.
        /// </summary>
        /// <param name="value1"><see cref="PopulationReplacementValue"/> to be compared against <paramref name="value2"/>.</param>
        /// <param name="value2"><see cref="PopulationReplacementValue"/> to be compared against <paramref name="value1"/>.</param>
        /// <returns>true if the two objects are equal; otherwise, false.</returns>
        public static bool operator ==(PopulationReplacementValue value1, PopulationReplacementValue value2)
        {
            return value1.Equals(value2);
        }

        /// <summary>
        /// Indictates whether <paramref name="value1"/> is not equal to <paramref name="value2"/>.
        /// </summary>
        /// <param name="value1"><see cref="PopulationReplacementValue"/> to be compared against <paramref name="value2"/>.</param>
        /// <param name="value2"><see cref="PopulationReplacementValue"/> to be compared against <paramref name="value1"/>.</param>
        /// <returns>true if the two objects are not equal; otherwise, false.</returns>
        public static bool operator !=(PopulationReplacementValue value1, PopulationReplacementValue value2)
        {
            return !value1.Equals(value2);
        }
    }
}
