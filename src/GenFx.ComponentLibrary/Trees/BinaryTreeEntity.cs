using System;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Provides the abstract base class for a binary tree.
    /// </summary>
    /// <typeparam name="T">The type of nodes in the tree.</typeparam>
    public abstract class BinaryTreeEntity<T> : NAryTreeEntity<BinaryTreeNode<T>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTreeEntity{T}"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="NAryTreeEntity{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected BinaryTreeEntity(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="BinaryTreeEntity{T}"/>.
    /// </summary>
    [Component(typeof(BinaryTreeEntity<>))]
    public abstract class BinaryTreeEntityConfiguration : NAryTreeEntityConfiguration
    {
    }
}
