using GenFx.Contracts;
using GenFx.Validation;
using System;
using System.Reflection;

namespace GenFx
{
    /// <summary>
    /// Represents the configuration of <see cref="GeneticAlgorithm{TConfiguration, TAlgorithm}"/>.
    /// </summary>
    /// <typeparam name="TConfiguration">Type of the deriving configuration class.</typeparam>
    /// <typeparam name="TAlgorithm">Type of the associated algorithm class.</typeparam>
    public abstract class GeneticAlgorithmFactoryConfig<TConfiguration, TAlgorithm> : ComponentFactoryConfig<TConfiguration, TAlgorithm>, IGeneticAlgorithmFactoryConfig
        where TConfiguration : GeneticAlgorithmFactoryConfig<TConfiguration, TAlgorithm>
        where TAlgorithm : GeneticAlgorithm<TAlgorithm, TConfiguration>
    {
        private const int DefaultEnvironmentSize = 1;

        private int environmentSize = DefaultEnvironmentSize;

        /// <summary>
        /// Gets or sets the number of <see cref="IPopulation"/> objects that are contained by the <see cref="GeneticEnvironment"/>.
        /// </summary>
        /// <value>
        /// The number of populations that are contained by the <see cref="GeneticEnvironment"/>.
        /// This value is defaulted to 1 and must be greater or equal to 1.
        /// </value>
        /// <exception cref="ValidationException">Value is not valid.</exception>
        [IntegerValidator(MinValue = 1)]
        public int EnvironmentSize
        {
            get { return this.environmentSize; }
            set { this.SetProperty(ref this.environmentSize, value); }
        }

        /// <summary>
        /// Returns a new instance of the <typeparamref name="TAlgorithm"/> associated with this configuration.
        /// </summary>
        /// <param name="configurationSet">The <see cref="ComponentFactoryConfigSet"/> containing the configurations to use for the algorithm.</param>
        /// <remarks>
        /// If the associated algorithm type does not have a constructor which takes a single parameter of type <see cref="ComponentFactoryConfigSet"/>,
        /// the derived configuration class must override this method to provide an instance of the algorithm.
        /// </remarks>
        public virtual TAlgorithm CreateComponent(ComponentFactoryConfigSet configurationSet)
        {
            try
            {
                return (TAlgorithm)Activator.CreateInstance(this.ComponentType, new object[] { configurationSet });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
