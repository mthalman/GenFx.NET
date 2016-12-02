using System;
using System.Collections;
using System.Collections.Generic;

namespace GenFx.ComponentLibrary.Trees
{
    /// <summary>
    /// Represents an ordered collection of tree nodes.
    /// </summary>
    /// <remarks><b>TreeNodeList</b> supports iteration and indexed access.</remarks>
    public sealed class TreeNodeCollection : IEnumerable, IEnumerable<TreeNode>
    {
        private List<TreeNode> nodes = new List<TreeNode>();

        /// <summary>
        /// Gets the number of nodes contained in the list.
        /// </summary>
        /// <value>The number of nodes contained in the list.</value>
        public int Count
        {
            get { return this.nodes.Count; }
        }

        /// <summary>
        /// Gets the <see cref="TreeNode"/> at the given index.
        /// </summary>
        /// <param name="index">Position of the <see cref="TreeNode"/> to retrieve.</param>
        /// <returns><see cref="TreeNode"/> at the given index.</returns>
        public TreeNode this[int index]
        {
            get { return this.nodes[index]; }
            set { this.nodes[index] = value; }
        }

        /// <summary>
        /// Gets the index of the <paramref name="node"/>.
        /// </summary>
        /// <param name="node"><see cref="TreeNode"/> whose index should be returned.</param>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is null.</exception>
        internal int IndexOf(TreeNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return this.nodes.IndexOf(node);
        }

        /// <summary>
        /// Provides a simple "foreach" style iteration over the collection of nodes in the <see cref="TreeNodeCollection"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> to iterate the collection of nodes.</returns>
        public IEnumerator GetEnumerator()
        {
            return this.nodes.GetEnumerator();
        }

        /// <summary>
        /// Provides a simple "foreach" style iteration over the collection of nodes in the <see cref="TreeNodeCollection"/>.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> to iterate the collection of nodes.</returns>
        IEnumerator<TreeNode> IEnumerable<TreeNode>.GetEnumerator()
        {
            return this.nodes.GetEnumerator();
        }

        /// <summary>
        /// Adds the <paramref name="node"/> to the collection.
        /// </summary>
        /// <param name="node"><see cref="TreeNode"/> to add.</param>
        internal void Add(TreeNode node)
        {
            this.nodes.Add(node);
        }

        /// <summary>
        /// Inserts the <paramref name="node"/> at the specified position.
        /// </summary>
        /// <param name="index">Position to insert the node.</param>
        /// <param name="node"><see cref="TreeNode"/> to add.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the <see cref="Count"/>.</exception>
        internal void Insert(int index, TreeNode node)
        {
            this.nodes.Insert(index, node);
        }

        /// <summary>
        /// Removes the <paramref name="node"/> from the collection.
        /// </summary>
        /// <param name="node"><see cref="TreeNode"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="node"/> is null.</exception>
        internal bool Remove(TreeNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return this.nodes.Remove(node);
        }
    }
}
