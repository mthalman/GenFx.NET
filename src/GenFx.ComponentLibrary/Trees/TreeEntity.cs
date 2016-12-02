using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GenFx.ComponentLibrary.Properties;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Provides the abstract base class for a tree that is a type of <see cref="GeneticEntity"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <b>TreeEntity</b> is used to represent genetic entities that require a tree structure, such as
    /// expression trees.  Trees generally do not have a fixed size and can grow infinitely.
    /// </para>
    /// <para>
    /// <b>Notes to implementers:</b> When this base class is derived, the derived class can be used by
    /// the genetic algorithm by using the <see cref="ComponentConfigurationSet.Entity"/> 
    /// property.
    /// </para>
    /// </remarks>
    public abstract class TreeEntity : GeneticEntity
    {
        private TreeNode rootNode;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeEntity"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="TreeEntity"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected TreeEntity(GeneticAlgorithm algorithm)
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
        /// Copies the state from this <see cref="TreeEntity"/> to <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity"><see cref="TreeEntity"/> to which state is to be copied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        public override void CopyTo(GeneticEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            TreeEntity tree = (TreeEntity)entity;
            tree.rootNode = this.rootNode.Clone(tree, null);

            base.CopyTo(entity);
        }

        /// <summary>
        /// Returns the number of nodes contained in the tree.
        /// </summary>
        /// <returns>The number of nodes contained in the tree.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public int GetSize()
        {
            return this.GetSubtreeSize(this.rootNode);
        }

        /// <summary>
        /// Returns the number of nodes contained in the subtree of the given <paramref name="node"/>.
        /// </summary>
        private int GetSubtreeSize(TreeNode node)
        {
            if (node == null)
            {
                return 0;
            }

            int sum = 1; // Includes the node passed in.

            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                sum += this.GetSubtreeSize(node.ChildNodes[i]);
            }

            return sum;
        }

        /// <summary>
        /// Returns an enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in prefix order.
        /// </summary>
        /// <returns>
        /// An enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in prefix order.
        /// </returns>
        public IEnumerable<TreeNode> GetPrefixTree()
        {
            return this.GetPrefixTree(this.rootNode);
        }

        /// <summary>
        /// Returns an enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in prefix order.
        /// </summary>
        /// <param name="node"><see cref="TreeNode"/> to start at.</param>
        /// <returns>
        /// An enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in prefix order.
        /// </returns>
        private IEnumerable<TreeNode> GetPrefixTree(TreeNode node)
        {
            yield return node;
            foreach (TreeNode childNode in node.ChildNodes)
            {
                foreach (TreeNode subChildNode in this.GetPrefixTree(childNode))
                {
                    yield return subChildNode;
                }
            }
        }

        /// <summary>
        /// Returns an enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in postfix order.
        /// </summary>
        /// <returns>An enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in prefix order.</returns>
        public IEnumerable<TreeNode> GetPostfixTree()
        {
            return this.GetPostfixTree(this.rootNode);
        }

        /// <summary>
        /// Returns an enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in postfix order.
        /// </summary>
        /// <param name="node"><see cref="TreeNode"/> to start at.</param>
        /// <returns>An enumerable collection of <see cref="TreeNode"/> objects containing the nodes
        /// of the tree sorted in prefix order.</returns>
        private IEnumerable<TreeNode> GetPostfixTree(TreeNode node)
        {
            foreach (TreeNode childNode in node.ChildNodes)
            {
                foreach (TreeNode subChildNode in this.GetPostfixTree(childNode))
                {
                    yield return subChildNode;
                }
            }
            yield return node;
        }

        /// <summary>
        /// Sets the <see cref="TreeEntity.RootNode"/> property to <paramref name="node"/>.
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
            TreeUtil.SetTreeForChildNodes(node);
        }

        /// <summary>
        /// Swaps the position of the two nodes within their respective trees.
        /// </summary>
        /// <remarks><paramref name="node1"/> will be removed from its tree and be placed in the
        /// position that <paramref name="node2"/> existed in its tree; and vice versa.</remarks>
        /// <param name="node1"><see cref="TreeNode"/> to be swapped with <paramref name="node2"/>.</param>
        /// <param name="node2"><see cref="TreeNode"/> to be swapped with <paramref name="node1"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="node1"/> is null.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="node2"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="node1"/> is not contained by this tree.</exception>
        public void Swap(TreeNode node1, TreeNode node2)
        {
            if (node1 == null)
            {
                throw new ArgumentNullException(nameof(node1));
            }
            if (node2 == null)
            {
                throw new ArgumentNullException(nameof(node2));
            }

            if (node1.Tree != this)
            {
                throw new ArgumentException(LibResources.ErrorMsg_SwapNodeNotInTree, nameof(node1));
            }

            TreeNode node2ParentNode = node2.ParentNode;
            TreeEntity node2Tree = node2.Tree;
            TreeNode node1ParentNode = node1.ParentNode;
            TreeEntity node1Tree = node1.Tree;

            TreeUtil.MoveNodeToTree(node1, node2Tree, node2, node2ParentNode);
            TreeUtil.MoveNodeToTree(node2, node1Tree, node1, node1ParentNode);
        }
    }

    /// <summary>
    /// Represents the configuration of <see cref="TreeEntity"/>.
    /// </summary>
    [Component(typeof(TreeEntity))]
    public abstract class TreeEntityConfiguration : GeneticEntityConfiguration
    {
    }

    /// <summary>
    /// Provides the abstract base class for a generic tree that is a type of <see cref="GeneticEntity"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The type of nodes in the tree.  <typeparamref name="T"/> must be a 
    /// type of <see cref="TreeNode"/> and have a default public constructor.
    /// </typeparam>
    public abstract class TreeEntity<T> : TreeEntity
        where T : TreeNode, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeEntity{TNode}"/> class.
        /// </summary>
        /// <param name="algorithm"><see cref="GeneticAlgorithm"/> using this <see cref="TreeEntity{TNode}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="algorithm"/> is null.</exception>
        /// <exception cref="ValidationException">The component's configuration is in an invalid state.</exception>
        protected TreeEntity(GeneticAlgorithm algorithm)
            : base(algorithm)
        {
        }

        /// <summary>
        /// Gets the <see cref="TreeNode"/> representing the root of the tree.
        /// </summary>
        /// <value>The <see cref="TreeNode"/> representing the root of the tree.</value>
        public new T RootNode
        {
            get { return (T)base.RootNode; }
        }
    }
}
