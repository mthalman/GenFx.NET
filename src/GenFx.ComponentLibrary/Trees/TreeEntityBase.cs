using System;
using System.Diagnostics.CodeAnalysis;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Provides the abstract base class for a tree that is a type of <see cref="GeneticEntity"/>.
    /// </summary>
    /// <remarks>
    /// The <b>TreeEntity</b> is used to represent genetic entities that require a tree structure, such as
    /// expression trees.  Trees generally do not have a fixed size and can grow infinitely.
    /// </remarks>
    public abstract class TreeEntityBase : GeneticEntity
    {
        private TreeNode rootNode;
        
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
        /// <param name="entity"><see cref="GeneticEntity"/> to which state is to be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public override void CopyTo(GeneticEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            TreeEntityBase treeEntity = (TreeEntityBase)entity;

            treeEntity.rootNode = this.rootNode.Clone(treeEntity, null);

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
