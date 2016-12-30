namespace GenFx
{
    /// <summary>
    /// Container of all the configurable operators of a genetic algorithm.
    /// </summary>
    public class AlgorithmOperators
    {
        /// <summary>
        /// Gets the <see cref="FitnessEvaluator"/> object that was specified in the 
        /// <see cref="ComponentConfigurationSet.FitnessEvaluator"/> property.
        /// </summary>
        public IFitnessEvaluator FitnessEvaluator
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the <see cref="Terminator"/> object that was specified in the 
        /// <see cref="ComponentConfigurationSet.Terminator"/> property.
        /// </summary>
        public ITerminator Terminator
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the <see cref="FitnessScalingStrategy"/> object that was specified in the 
        /// <see cref="ComponentConfigurationSet.FitnessScalingStrategy"/> property.
        /// </summary>
        public IFitnessScalingStrategy FitnessScalingStrategy
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the <see cref="SelectionOperator"/> object that was specified in the 
        /// <see cref="ComponentConfigurationSet.SelectionOperator"/> property.
        /// </summary>
        public ISelectionOperator SelectionOperator
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the <see cref="MutationOperator"/> object that was specified in the 
        /// <see cref="ComponentConfigurationSet.MutationOperator"/> property.
        /// </summary>
        public IMutationOperator MutationOperator
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the <see cref="CrossoverOperator"/> object that was specified in the 
        /// <see cref="ComponentConfigurationSet.CrossoverOperator"/> property.
        /// </summary>
        public ICrossoverOperator CrossoverOperator
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets the <see cref="ElitismStrategy"/> object that was specified in the 
        /// <see cref="ComponentConfigurationSet.ElitismStrategy"/> property.
        /// </summary>
        public IElitismStrategy ElitismStrategy
        {
            get;
            internal set;
        }
    }
}
