using GenFx.Contracts;

namespace GenFx
{
    /// <summary>
    /// Container of all the configurable operators of a genetic algorithm.
    /// </summary>
    public class AlgorithmOperators
    {
        /// <summary>
        /// Gets the <see cref="FitnessEvaluator"/> object associated with the configuration specified in the 
        /// <see cref="ComponentFactoryConfigSet.FitnessEvaluator"/> property.
        /// </summary>
        public IFitnessEvaluator FitnessEvaluator
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the <see cref="Terminator"/> object associated with the configuration specified in thes
        /// <see cref="ComponentFactoryConfigSet.Terminator"/> property.
        /// </summary>
        public ITerminator Terminator
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the <see cref="FitnessScalingStrategy"/> object associated with the configuration specified in the
        /// <see cref="ComponentFactoryConfigSet.FitnessScalingStrategy"/> property.
        /// </summary>
        public IFitnessScalingStrategy FitnessScalingStrategy
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the <see cref="SelectionOperator"/> object associated with the configuration specified in the
        /// <see cref="ComponentFactoryConfigSet.SelectionOperator"/> property.
        /// </summary>
        public ISelectionOperator SelectionOperator
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the <see cref="MutationOperator"/> object associated with the configuration specified in the
        /// <see cref="ComponentFactoryConfigSet.MutationOperator"/> property.
        /// </summary>
        public IMutationOperator MutationOperator
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the <see cref="CrossoverOperator"/> object associated with the configuration specified in the
        /// <see cref="ComponentFactoryConfigSet.CrossoverOperator"/> property.
        /// </summary>
        public ICrossoverOperator CrossoverOperator
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the <see cref="ElitismStrategy"/> object associated with the configuration specified in the
        /// <see cref="ComponentFactoryConfigSet.ElitismStrategy"/> property.
        /// </summary>
        public IElitismStrategy ElitismStrategy
        {
            get;
            internal set;
        }
    }
}
