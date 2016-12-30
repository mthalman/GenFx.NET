using System;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Provides the abstract base class for a generic tree that is a type of <see cref="IGeneticEntity"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the deriving entity class.</typeparam>
    /// <typeparam name="TConfiguration">The type of the associated configuration class.</typeparam>
    /// <typeparam name="TNode">
    /// The type of nodes in the tree.  <typeparamref name="TNode"/> must be a 
    /// type of <see cref="TreeNode"/> and have a default public constructor.
    /// </typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes")]
    public abstract class TreeEntity<TEntity, TConfiguration, TNode> : TreeEntity<TEntity, TConfiguration>
        where TEntity : TreeEntity<TEntity, TConfiguration, TNode>
        where TConfiguration : TreeEntityConfiguration<TConfiguration, TEntity>
        where TNode : TreeNode, new()
    {
        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        /// <param name="algorithm"><see cref="IGeneticAlgorithm"/> using this object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected TreeEntity(IGeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Gets the <see cref="TreeNode"/> representing the root of the tree.
        /// </summary>
        /// <value>The <see cref="TreeNode"/> representing the root of the tree.</value>
        public new TNode RootNode
        {
            get { return (TNode)base.RootNode; }
        }
    }
}
