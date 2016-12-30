using System;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Provides a <see cref="ITreeEntity"/> with single-node tree crossover support.
    /// </summary>
    /// <remarks>
    /// Single-node tree crossover chooses a node position in two trees and swaps those nodes, including their children.
    /// For example, if two <see cref="ITreeEntity"/> objects represented as "(A OR B) AND C" and
    /// "B OR C" were to be crossed over at the "OR" node in the first tree and the "C" node in the second tree,
    /// the resulting offspring would be "C AND C" and "B OR (A OR B)".
    /// </remarks>
    public sealed class SingleNodeTreeCrossoverOperator : SingleNodeTreeCrossoverOperator<SingleNodeTreeCrossoverOperator, SingleNodeTreeCrossoverOperatorConfiguration>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        public SingleNodeTreeCrossoverOperator(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }
}
