using System;
using System.ComponentModel;
using System.Globalization;

namespace GenFx.ComponentLibrary.Algorithms
{
    /// <summary>
    /// Represents the value indicating how many <see cref="GeneticEntity"/> objects are to 
    /// be replaced with the offspring of the previous generation.
    /// </summary>
    /// <seealso cref="SteadyStateGeneticAlgorithm"/>
    [TypeConverter(typeof(PopulationReplacementValueTypeConverter))]
    public struct PopulationReplacementValue
    {
        private int replacementValue;
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
            PopulationReplacementValueTypeConverter converter = new PopulationReplacementValueTypeConverter();
            return converter.ConvertToString(this);
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

    /// <summary>
    /// Type converter used to convert other types into a <see cref="PopulationReplacementValue"/>.
    /// </summary>
    public class PopulationReplacementValueTypeConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// Returns whether this converter can convert the object to the specified type,
        /// using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="destinationType">A <see cref="Type"/> that represents the type you want to convert to.</param>
        /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(PopulationReplacementValue))
            {
                return true;
            }
            else
            {
                return base.CanConvertTo(context, destinationType);
            }
        }

        /// <summary>
        /// Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="sourceType">A type that represents the type you want to convert from.</param>
        /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string) || sourceType == typeof(int))
            {
                return true;
            }
            else
            {
                return base.CanConvertFrom(context, sourceType);
            }
        }

        /// <summary>
        /// Converts the given object to the type of this converter, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">The <see cref="System.Globalization.CultureInfo"/> to use as the current culture.</param>
        /// <param name="value">The object to convert.</param>
        /// <returns>An object that represents the converted value.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is null.</exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            string stringVal = value as string;

            if (stringVal != null)
            {
                ReplacementValueKind replacementKind;
                int populationReplacementValue = ParsePopulationReplacementValue(stringVal, out replacementKind);
                return new PopulationReplacementValue(populationReplacementValue, replacementKind);
            }
            else // it's an integer
            {
                return new PopulationReplacementValue((int)value, ReplacementValueKind.FixedCount);
            }
        }

        /// <summary>
        /// Converts the given value object to the specified type, using the specified
        /// context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="culture">A <see cref="CultureInfo"/>. If null is passed, the current culture
        /// is assumed.</param>
        /// <param name="value">The object to convert.</param>
        /// <param name="destinationType">The <see cref="Type"/> to convert the value parameter to.</param>
        /// <returns>An object that represents the converted value.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                ReplacementValueKindValidator validator = new ReplacementValueKindValidator();
                PopulationReplacementValue replacementValue = (PopulationReplacementValue)value;

                string msg;
                if (validator.IsValid(replacementValue.Kind, "Empty", null, out msg))
                {
                    if (replacementValue.Kind == ReplacementValueKind.Percentage)
                    {
                        return replacementValue.Value.ToString(CultureInfo.InvariantCulture) + "%";
                    }
                    else
                    {
                        return replacementValue.Value.ToString(CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    throw EnumHelper.CreateUndefinedEnumException(typeof(PopulationReplacementValue), "value");
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>
        /// Parses the population replacement value into its separate parts.
        /// </summary>
        /// <param name="value">Value to be parsed.</param>
        /// <param name="replacementKind">The <see cref="ReplacementValueKind"/> derived from <paramref name="value"/>.</param>
        /// <returns>Integer value derived from the parsed <paramref name="value"/>.</returns>
        private static int ParsePopulationReplacementValue(string value, out ReplacementValueKind replacementKind)
        {
            if (value.Contains("%"))
            {
                replacementKind = ReplacementValueKind.Percentage;
                value = value.Substring(0, value.Length - 1);
            }
            else
            {
                replacementKind = ReplacementValueKind.FixedCount;
            }
            return Int32.Parse(value, CultureInfo.InvariantCulture);
        }
    }
}
