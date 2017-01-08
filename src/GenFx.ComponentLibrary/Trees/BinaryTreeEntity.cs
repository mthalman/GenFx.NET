using GenFx.Contracts;
using System;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Provides the abstract base class for a binary tree.
    /// </summary>
    /// <typeparam name="TEntity">The type of the deriving entity class.</typeparam>
    /// <typeparam name="TConfiguration">The type of the associated configuration class.</typeparam>
    /// <typeparam name="TValue">The type of values in the nodes of the tree.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class BinaryTreeEntity<TEntity, TConfiguration, TValue> : TreeEntity<TEntity, TConfiguration, BinaryTreeNode<TValue>>
        where TEntity : BinaryTreeEntity<TEntity, TConfiguration, TValue>
        where TConfiguration : BinaryTreeEntityConfiguration<TConfiguration, TEntity, TValue>
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected BinaryTreeEntity(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="BinaryTreeEntity{TEntity, TConfiguration, TValue}"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class BinaryTreeEntityConfiguration<TConfiguration, TEntity, TValue> : TreeEntityFactoryConfig<TConfiguration, TEntity, BinaryTreeNode<TValue>>
        where TConfiguration : BinaryTreeEntityConfiguration<TConfiguration, TEntity, TValue>
        where TEntity : BinaryTreeEntity<TEntity, TConfiguration, TValue>
    {
    }
}
