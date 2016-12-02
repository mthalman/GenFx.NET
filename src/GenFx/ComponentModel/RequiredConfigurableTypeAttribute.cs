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
    /// Indicates that a class requires a specific <see cref="CrossoverOperator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredCrossoverOperatorAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredCrossoverOperatorAttribute"/> class.
        /// </summary>
        /// <param name="crossoverOperatorType"><see cref="CrossoverOperator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="crossoverOperatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="crossoverOperatorType"/> does not derive from <see cref="CrossoverOperator"/>.</exception>
        public RequiredCrossoverOperatorAttribute(Type crossoverOperatorType)
            : base(crossoverOperatorType, typeof(CrossoverOperator))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="ElitismStrategy"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredElitismStrategyAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredElitismStrategyAttribute"/> class.
        /// </summary>
        /// <param name="elitismStrategyType"><see cref="ElitismStrategy"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="elitismStrategyType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="elitismStrategyType"/> does not derive from <see cref="ElitismStrategy"/>.</exception>
        public RequiredElitismStrategyAttribute(Type elitismStrategyType)
            : base(elitismStrategyType, typeof(ElitismStrategy))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="FitnessEvaluator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredFitnessEvaluatorAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredFitnessEvaluatorAttribute"/> class.
        /// </summary>
        /// <param name="fitnessEvaluatorType"><see cref="FitnessEvaluator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="fitnessEvaluatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="fitnessEvaluatorType"/> does not derive from <see cref="FitnessEvaluator"/>.</exception>
        public RequiredFitnessEvaluatorAttribute(Type fitnessEvaluatorType)
            : base(fitnessEvaluatorType, typeof(FitnessEvaluator))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="FitnessScalingStrategy"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredFitnessScalingStrategyAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredFitnessScalingStrategyAttribute"/> class.
        /// </summary>
        /// <param name="fitnessScalingStrategyType"><see cref="FitnessScalingStrategy"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="fitnessScalingStrategyType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="fitnessScalingStrategyType"/> does not derive from <see cref="FitnessScalingStrategy"/>.</exception>
        public RequiredFitnessScalingStrategyAttribute(Type fitnessScalingStrategyType)
            : base(fitnessScalingStrategyType, typeof(FitnessScalingStrategy))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="GeneticAlgorithm"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredGeneticAlgorithmAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredGeneticAlgorithmAttribute"/> class.
        /// </summary>
        /// <param name="geneticAlgorithmType"><see cref="GeneticAlgorithm"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="geneticAlgorithmType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="geneticAlgorithmType"/> does not derive from <see cref="GeneticAlgorithm"/>.</exception>
        public RequiredGeneticAlgorithmAttribute(Type geneticAlgorithmType)
            : base(geneticAlgorithmType, typeof(GeneticAlgorithm))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="GeneticEntity"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredEntityAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredEntityAttribute"/> class.
        /// </summary>
        /// <param name="entityType"><see cref="GeneticEntity"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entityType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="entityType"/> does not derive from <see cref="GeneticEntity"/>.</exception>
        public RequiredEntityAttribute(Type entityType)
            : base(entityType, typeof(GeneticEntity))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="MutationOperator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredMutationOperatorAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredMutationOperatorAttribute"/> class.
        /// </summary>
        /// <param name="mutationOperatorType"><see cref="MutationOperator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="mutationOperatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="mutationOperatorType"/> does not derive from <see cref="MutationOperator"/>.</exception>
        public RequiredMutationOperatorAttribute(Type mutationOperatorType)
            : base(mutationOperatorType, typeof(MutationOperator))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="Population"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredPopulationAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredPopulationAttribute"/> class.
        /// </summary>
        /// <param name="populationType"><see cref="Population"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="populationType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="populationType"/> does not derive from <see cref="Population"/>.</exception>
        public RequiredPopulationAttribute(Type populationType)
            : base(populationType, typeof(Population))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="SelectionOperator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredSelectionOperatorAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredSelectionOperatorAttribute"/> class.
        /// </summary>
        /// <param name="selectionOperatorType"><see cref="SelectionOperator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="selectionOperatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="selectionOperatorType"/> does not derive from <see cref="SelectionOperator"/>.</exception>
        public RequiredSelectionOperatorAttribute(Type selectionOperatorType)
            : base(selectionOperatorType, typeof(SelectionOperator))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="Statistic"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class RequiredStatisticAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredStatisticAttribute"/> class.
        /// </summary>
        /// <param name="statisticType"><see cref="Statistic"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="statisticType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="statisticType"/> does not derive from <see cref="Statistic"/>.</exception>
        public RequiredStatisticAttribute(Type statisticType)
            : base(statisticType, typeof(Statistic))
        {
        }
    }

    /// <summary>
    /// Indicates that a class requires a specific <see cref="Terminator"/> type in order to function correctly.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments")]
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class RequiredTerminatorAttribute : RequiredComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredTerminatorAttribute"/> class.
        /// </summary>
        /// <param name="terminatorType"><see cref="Terminator"/> type which is required by the class.</param>
        /// <exception cref="ArgumentNullException"><paramref name="terminatorType"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="terminatorType"/> does not derive from <see cref="Terminator"/>.</exception>
        public RequiredTerminatorAttribute(Type terminatorType)
            : base(terminatorType, typeof(Terminator))
        {
        }
    }
}
