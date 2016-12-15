using System;
using System.Diagnostics.CodeAnalysis;
using GenFx.Properties;

namespace GenFx.ComponentModel
{
    /// <summary>
    /// Indicates that a class requires a specific component type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public abstract class RequiredComponentAttribute : Attribute
    {
        private Type requiredType;

        /// <summary>
        /// Gets the type which is required by the class.
        /// </summary>
        public Type RequiredType
        {
            get { return this.requiredType; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredComponentAttribute"/> class.
        /// </summary>
        /// <param name="requiredType">Type which is required by the class.</param>
        /// <param name="baseType">Type that <paramref name="requiredType"/> must be a type of.</param>
        /// <exception cref="ArgumentNullException"><paramref name="requiredType"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="baseType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="requiredType"/> does not derive from <paramref name="baseType"/>.</exception>
        protected RequiredComponentAttribute(Type requiredType, Type baseType)
        {
            if (requiredType == null)
            {
                throw new ArgumentNullException(nameof(requiredType));
            }

            this.requiredType = requiredType;

            if (baseType == null)
            {
                throw new ArgumentNullException(nameof(baseType));
            }

            if (!baseType.IsAssignableFrom(requiredType))
            {
                throw new ArgumentException(
                  StringUtil.GetFormattedString(FwkResources.ErrorMsg_InvalidType, baseType.FullName),
                  nameof(requiredType));
            }
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="ICrossoverOperator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredCrossoverOperatorAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredCrossoverOperatorAttribute"/> class.
        /// </summary>
        /// <param name="crossoverOperatorType"><see cref="ICrossoverOperator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="crossoverOperatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="crossoverOperatorType"/> does not derive from <see cref="ICrossoverOperator"/>.</exception>
        public RequiredCrossoverOperatorAttribute(Type crossoverOperatorType)
            : base(crossoverOperatorType, typeof(ICrossoverOperator))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="IElitismStrategy"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredElitismStrategyAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredElitismStrategyAttribute"/> class.
        /// </summary>
        /// <param name="elitismStrategyType"><see cref="IElitismStrategy"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="elitismStrategyType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="elitismStrategyType"/> does not derive from <see cref="IElitismStrategy"/>.</exception>
        public RequiredElitismStrategyAttribute(Type elitismStrategyType)
            : base(elitismStrategyType, typeof(IElitismStrategy))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="IFitnessEvaluator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredFitnessEvaluatorAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredFitnessEvaluatorAttribute"/> class.
        /// </summary>
        /// <param name="fitnessEvaluatorType"><see cref="IFitnessEvaluator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="fitnessEvaluatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="fitnessEvaluatorType"/> does not derive from <see cref="IFitnessEvaluator"/>.</exception>
        public RequiredFitnessEvaluatorAttribute(Type fitnessEvaluatorType)
            : base(fitnessEvaluatorType, typeof(IFitnessEvaluator))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="IFitnessScalingStrategy"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredFitnessScalingStrategyAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredFitnessScalingStrategyAttribute"/> class.
        /// </summary>
        /// <param name="fitnessScalingStrategyType"><see cref="IFitnessScalingStrategy"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="fitnessScalingStrategyType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="fitnessScalingStrategyType"/> does not derive from <see cref="IFitnessScalingStrategy"/>.</exception>
        public RequiredFitnessScalingStrategyAttribute(Type fitnessScalingStrategyType)
            : base(fitnessScalingStrategyType, typeof(IFitnessScalingStrategy))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="IGeneticAlgorithm"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredGeneticAlgorithmAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredGeneticAlgorithmAttribute"/> class.
        /// </summary>
        /// <param name="geneticAlgorithmType"><see cref="IGeneticAlgorithm"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="geneticAlgorithmType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="geneticAlgorithmType"/> does not derive from <see cref="IGeneticAlgorithm"/>.</exception>
        public RequiredGeneticAlgorithmAttribute(Type geneticAlgorithmType)
            : base(geneticAlgorithmType, typeof(IGeneticAlgorithm))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="IGeneticEntity"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredEntityAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredEntityAttribute"/> class.
        /// </summary>
        /// <param name="entityType"><see cref="IGeneticEntity"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entityType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="entityType"/> does not derive from <see cref="IGeneticEntity"/>.</exception>
        public RequiredEntityAttribute(Type entityType)
            : base(entityType, typeof(IGeneticEntity))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="IMutationOperator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredMutationOperatorAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredMutationOperatorAttribute"/> class.
        /// </summary>
        /// <param name="mutationOperatorType"><see cref="IMutationOperator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="mutationOperatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="mutationOperatorType"/> does not derive from <see cref="IMutationOperator"/>.</exception>
        public RequiredMutationOperatorAttribute(Type mutationOperatorType)
            : base(mutationOperatorType, typeof(IMutationOperator))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="IPopulation"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredPopulationAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredPopulationAttribute"/> class.
        /// </summary>
        /// <param name="populationType"><see cref="IPopulation"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="populationType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="populationType"/> does not derive from <see cref="IPopulation"/>.</exception>
        public RequiredPopulationAttribute(Type populationType)
            : base(populationType, typeof(IPopulation))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="ISelectionOperator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredSelectionOperatorAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredSelectionOperatorAttribute"/> class.
        /// </summary>
        /// <param name="selectionOperatorType"><see cref="ISelectionOperator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="selectionOperatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="selectionOperatorType"/> does not derive from <see cref="ISelectionOperator"/>.</exception>
        public RequiredSelectionOperatorAttribute(Type selectionOperatorType)
            : base(selectionOperatorType, typeof(ISelectionOperator))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="IStatistic"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class RequiredStatisticAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredStatisticAttribute"/> class.
        /// </summary>
        /// <param name="statisticType"><see cref="IStatistic"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="statisticType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="statisticType"/> does not derive from <see cref="IStatistic"/>.</exception>
        public RequiredStatisticAttribute(Type statisticType)
            : base(statisticType, typeof(IStatistic))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="ITerminator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredTerminatorAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredTerminatorAttribute"/> class.
        /// </summary>
        /// <param name="terminatorType"><see cref="ITerminator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="terminatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="terminatorType"/> does not derive from <see cref="ITerminator"/>.</exception>
        public RequiredTerminatorAttribute(Type terminatorType)
            : base(terminatorType, typeof(ITerminator))
        {
        }
    }
}
