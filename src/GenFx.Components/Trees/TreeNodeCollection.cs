using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GenFx.Components.Trees
{
    /// <summary>
    /// Represents an ordered collection of tree nodes.
    /// </summary>
    /// <remarks><b>TreeNodeCollection</b> supports iteration and indexed access.</remarks>
    [DataContract]
    public sealed class TreeNodeCollection : IList<TreeNode>
    {
        [DataMember]
        private readonly List<TreeNode> nodes = new List<TreeNode>();

        [DataMember]
        private readonly int? fixedSizeCount;

        /// <summary>
        /// Initializes a new instance of this class that has a fixed number of items.
        /// </summary>
        /// <param name="fixedSizeCount">Number of items this collection contains.</param>
        public TreeNodeCollection(int fixedSizeCount)
        {
            this.fixedSizeCount = fixedSizeCount;

            for (int i = 0; i < fixedSizeCount; i++)
            {
                this.nodes.Add(null);
            }
        }

        /// <summary>
        /// Initializes a new instance of this class.
        /// </summary>
        public TreeNodeCollection()
        {
        }

        /// <summary>
        /// Gets or sets the node at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the node to get or set.</param>
        /// <returns>The node at the specified index.</returns>
        public TreeNode this[int index]
        {
            get { return this.nodes[index]; }
            set { this.nodes[index] = value; }
        }

        /// <summary>
        /// Gets the number of nodes contained in the collection.
        /// </summary>
        public int Count
        {
            get { return this.nodes.Count; }
        }

        /// <summary>
        /// Gets a value indicating whether the collection is read-only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Adds a node to the collection.
        /// </summary>
        /// <param name="item">The object to add to the collection.</param>
        public void Add(TreeNode item)
        {
            this.EnsureNotFixedSize();
            this.nodes.Add(item);
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        public void Clear()
        {
            this.EnsureNotFixedSize();
            this.nodes.Clear();
        }

        /// <summary>
        /// Determines whether the collection contains a specific node.
        /// </summary>
        /// <param name="item">The node to locate in the colletion.</param>
        /// <returns>true if item is found in the collection; otherwise false.</returns>
        public bool Contains(TreeNode item)
        {
            return this.nodes.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the collection to an array starting at a particular array index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional array that is the destination of the elements copied
        /// from the collection. The array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
        public void CopyTo(TreeNode[] array, int arrayIndex)
        {
            this.nodes.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that iterates through the collection.</returns>
        public IEnumerator<TreeNode> GetEnumerator()
        {
            return this.nodes.GetEnumerator();
        }

        /// <summary>
        /// Determines the index of a specific node in the collection.
        /// </summary>
        /// <param name="item">The node to locate in the collection.</param>
        /// <returns>The index of node if found in the list; otherwise, -1.</returns>
        public int IndexOf(TreeNode item)
        {
            return this.nodes.IndexOf(item);
        }

        /// <summary>
        /// Inserts a node in the collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which the node should be inserted.</param>
        /// <param name="item">The node to insert into the collection.</param>
        public void Insert(int index, TreeNode item)
        {
            this.EnsureNotFixedSize();
            this.nodes.Insert(index, item);
        }

        /// <summary>
        /// Removes the specified node from the collection.
        /// </summary>
        /// <param name="item">The node to remove from the collection.</param>
        /// <returns>true if the item was found and removed; otherwise, false.</returns>
        public bool Remove(TreeNode item)
        {
            this.EnsureNotFixedSize();
            return this.nodes.Remove(item);
        }

        /// <summary>
        /// Removes the node at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the node to remove.</param>
        public void RemoveAt(int index)
        {
            this.EnsureNotFixedSize();
            this.nodes.RemoveAt(index);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that iterates through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.nodes.GetEnumerator();
        }

        private void EnsureNotFixedSize()
        {
            if (this.fixedSizeCount.HasValue)
            {
                throw new InvalidOperationException(Resources.ErrorMsg_TreeNodeCollectionCountError);
            }
        }
    }
}
