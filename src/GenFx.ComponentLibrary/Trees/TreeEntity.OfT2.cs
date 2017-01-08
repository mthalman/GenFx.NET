using GenFx.ComponentLibrary.Base;
using GenFx.Contracts;
using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Provides the abstract base class for a tree that is a type of <see cref="IGeneticEntity"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <b>TreeEntity</b> is used to represent genetic entities that require a tree structure, such as
    /// expression trees.  Trees generally do not have a fixed size and can grow infinitely.
    /// </para>
    /// <para>
    /// <b>Notes to implementers:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by using the <see cref="ComponentFactoryConfigSet.Entity"/> 
    /// property.
    /// </para>
    /// </remarks>
    /// <typeparam name="TEntity">Type of the deriving entity class.</typeparam>
    /// <typeparam name="TConfiguration">Type of the associated configuration class.</typeparam>
    public abstract class TreeEntity<TEntity, TConfiguration> : GeneticEntity<TEntity, TConfiguration>, ITreeEntity
        where TEntity : TreeEntity<TEntity, TConfiguration>
        where TConfiguration : TreeEntityFactoryConfig<TConfiguration, TEntity>
    {
        private TreeNode rootNode;

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
        public TreeNode RootNode
        {
            get { return this.rootNode; }
        }

        /// <summary>
        /// Copies the state from this object> to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="TreeEntity{TEntity, TConfiguration}"/> to which state is to be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public override void CopyTo(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.rootNode = this.rootNode.Clone(entity, null);

            base.CopyTo(entity);
        }

        /// <summary>
        /// Sets the <see cref="RootNode"/> property to <paramref name="node"/>.
        /// </summary>
        /// <param name="node"><see cref="TreeNode"/> to be set as the root node of this tree.</param>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is null.</exception>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public void SetRootNode(TreeNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            this.rootNode = node;
            this.rootNode.ParentNode = null;
            node.Tree = this;
            TreeHelper.SetTreeForChildNodes(node);
        }
    }
}
