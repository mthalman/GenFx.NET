using System;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Represents a generic node in a <see cref="TreeEntityBase"/>.
    /// </summary>
    /// <typeparam name="T">Type of value contained by the node.</typeparam>
    public class TreeNode<T> : TreeNode
        where T : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode{T}"/> class.
        /// </summary>
        /// <param name="fixedSizeCount">Number of sub-node this node contains.</param>
        public TreeNode(int fixedSizeCount)
            : base(fixedSizeCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeNode{T}"/> class.
        /// </summary>
        public TreeNode()
            : base()
        {
        }

        /// <summary>
        /// Gets or sets the data value contained by this node.
        /// </summary>
        /// <value>The data value contained by this node.</value>
        public new T Value
        {
            get { return (T)base.Value; }
            set { base.Value = value; }
        }

        /// <summary>
        /// Returns a clone of this node.
        /// </summary>
        /// <param name="newTree"><see cref="TreeEntityBase"/> to which the cloned node should be assigned.</param>
        /// <param name="newParentNode"><see cref="TreeNode"/> to be assigned as the parent node of the cloned node.</param>
        /// <returns>A clone of this node.</returns>
        /// <remarks>
        /// <b>Notes to implementers:</b> When this method is overriden, it is suggested that the
        /// <see cref="TreeNode.CopyTo(TreeNode, TreeEntityBase, TreeNode)"/> method is also overriden.  
        /// In that case, the suggested implementation of this method is the following:
        /// <code>
        /// <![CDATA[
        /// public override object Clone(TreeEntity newTree, TreeNode newParentNode)
        /// {
        ///     MyTreeNode node = new MyTreeNode();
        ///     this.CopyTo(node, newTree, newParentNode);
        ///     return node;
        /// }
        /// ]]>
        /// </code>
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="newTree"/> is null.</exception>
        public override TreeNode Clone(TreeEntityBase newTree, TreeNode newParentNode)
        {
            if (newTree == null)
            {
                throw new ArgumentNullException(nameof(newTree));
            }

            TreeNode<T> node = new TreeNode<T>();
            this.CopyTo(node, newTree, newParentNode);
            return node;
        }
    }
}
