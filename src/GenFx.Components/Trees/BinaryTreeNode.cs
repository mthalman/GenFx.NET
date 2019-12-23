using System;
using System.Runtime.Serialization;

namespace GenFx.Components.Trees
{
    /// <summary>
    /// Represents a generic node in a binary tree with two possible child nodes.
    /// </summary>
    /// <typeparam name="T">Type of value contained by the node.</typeparam>
    [DataContract]
    public class BinaryTreeNode<T> : TreeNode<T>
        where T : IComparable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BinaryTreeNode{T}"/> class.
        /// </summary>
        public BinaryTreeNode()
            : base(2)
        {
        }

        /// <summary>
        /// Gets or sets the left child node of this <b>BinaryTreeNode{T}</b>.
        /// </summary>
        public BinaryTreeNode<T> LeftChildNode
        {
            get { return (BinaryTreeNode<T>)this.ChildNodes[0]; }
            set { this.ChildNodes[0] = value; }
        }

        /// <summary>
        /// Gets or sets the right child node of this <b>BinaryTreeNode{T}</b>.
        /// </summary>
        public BinaryTreeNode<T> RightChildNode
        {
            get { return (BinaryTreeNode<T>)this.ChildNodes[1]; }
            set { this.ChildNodes[1] = value; }
        }
    }
}
