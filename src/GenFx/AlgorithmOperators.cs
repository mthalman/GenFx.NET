using System;

namespace GenFx
{
    /// <summary>
    /// Container of all the configurable operators of a genetic algorithm.
    /// </summary>
    public class AlgorithmOperators
    {
        private SelectionOperator selectionOperator;
        private MutationOperator mutationOperator;
        private CrossoverOperator crossoverOperator;
        private ElitismStrategy elitismStrategy;
        private FitnessScalingStrategy fitnessScalingStrategy;
        private Terminator terminator;
        private FitnessEvaluator fitnessEvaluator;

        /// <summary>
        /// Gets the <see cref="FitnessEvaluator"/> object that was specified in the 
        /// <see cref="ComponentConfigurationSet.FitnessEvaluator"/> property.
        /// </summary>
        public FitnessEvaluator FitnessEvaluator
        {
            get { return this.fitnessEvaluator; }
            internal set { this.fitnessEvaluator = value; }
        }

        /// <summary>
        /// Gets the <see cref="Terminator"/> object that was specified in the 
        /// <see cref="ComponentConfigurationSet.Terminator"/> property.
        /// </summary>
        public Terminator Terminator
        {
            get { return this.terminator; }
            internal set { this.terminator = value; }
        }

        /// <summary>
        /// Gets the <see cref="FitnessScalingStrategy"/> object that was specified in the 
        /// <see cref="ComponentConfigurationSet.FitnessScalingStrategy"/> property.
        /// </summary>
        public FitnessScalingStrategy FitnessScalingStrategy
        {
            get { return this.fitnessScalingStrategy; }
            internal set { this.fitnessScalingStrategy = value; }
        }

        /// <summary>
        /// Gets the <see cref="SelectionOperator"/> object that was specified in the 
        /// <see cref="ComponentConfigurationSet.SelectionOperator"/> property.
        /// </summary>
        public SelectionOperator SelectionOperator
        {
            get { return this.selectionOperator; }
            internal set { this.selectionOperator = value; }
        }

        /// <summary>
        /// Gets the <see cref="MutationOperator"/> object that was specified in the 
        /// <see cref="ComponentConfigurationSet.MutationOperator"/> property.
        /// </summary>
        public MutationOperator MutationOperator
        {
            get { return this.mutationOperator; }
            internal set { this.mutationOperator = value; }
        }

        /// <summary>
        /// Gets the <see cref="CrossoverOperator"/> object that was specified in the 
        /// <see cref="ComponentConfigurationSet.CrossoverOperator"/> property.
        /// </summary>
        public CrossoverOperator CrossoverOperator
        {
            get { return this.crossoverOperator; }
            internal set { this.crossoverOperator = value; }
        }

        /// <summary>
        /// Gets the <see cref="ElitismStrategy"/> object that was specified in the 
        /// <see cref="ComponentConfigurationSet.ElitismStrategy"/> property.
        /// </summary>
        public ElitismStrategy ElitismStrategy
        {
            get { return this.elitismStrategy; }
            internal set { this.elitismStrategy = value; }
        }
    }
}
