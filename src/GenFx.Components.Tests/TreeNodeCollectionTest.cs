using GenFx.Components.Trees;
using System;
using System.Collections;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="TreeNodeCollection"/> class.
    /// </summary>
    public class TreeNodeCollectionTest
    {
        /// <summary>
        /// Tests that the ctor initializes the state correctly.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_Ctor()
        {
            TreeNodeCollection collection = new TreeNodeCollection();
            Assert.Empty(collection);
            Assert.False(collection.IsReadOnly);

            collection = new TreeNodeCollection(3);
            Assert.Equal(3, collection.Count);
            Assert.False(collection.IsReadOnly);
        }

        /// <summary>
        /// Tests that the <see cref="TreeNodeCollection.this"/>. property works
        /// </summary>
        [Fact]
        public void TreeNodeCollection_Indexer()
        {
            TreeNodeCollection collection = new TreeNodeCollection(2);
            Assert.Null(collection[0]);
            Assert.Null(collection[1]);

            TreeNode node1 = new TreeNode();
            collection[0] = node1;

            TreeNode node2 = new TreeNode();
            collection[1] = node2;

            Assert.Same(node1, collection[0]);
            Assert.Same(node2, collection[1]);
        }

        /// <summary>
        /// Tests that the <see cref="TreeNodeCollection.Add"/>. method works correctly.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_Add()
        {
            TreeNodeCollection collection = new TreeNodeCollection();

            TreeNode node1 = new TreeNode();
            collection.Add(node1);
            TreeNode node2 = new TreeNode();
            collection.Add(node2);

            Assert.Equal(2, collection.Count);
            Assert.Same(node1, collection[0]);
            Assert.Same(node2, collection[1]);
        }

        /// <summary>
        /// Tests that an exception is thrown when attempting to add an item to a fixed size collection.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_Add_FixedSize()
        {
            TreeNodeCollection collection = new TreeNodeCollection(2);

            TreeNode node1 = new TreeNode();
            Assert.Throws<InvalidOperationException>(() => collection.Add(node1));
        }

        /// <summary>
        /// Tests that the <see cref="TreeNodeCollection.Clear"/>. method works correctly.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_Clear()
        {
            TreeNodeCollection collection = new TreeNodeCollection();

            TreeNode node1 = new TreeNode();
            collection.Add(node1);
            TreeNode node2 = new TreeNode();
            collection.Add(node2);

            Assert.Equal(2, collection.Count);

            collection.Clear();
            Assert.Empty(collection);
        }

        /// <summary>
        /// Tests that an exception is thrown when attempting to clear a fixed size collection.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_Clear_FixedSize()
        {
            TreeNodeCollection collection = new TreeNodeCollection(2);

            Assert.Throws<InvalidOperationException>(() => collection.Clear());
        }

        /// <summary>
        /// Tests that the <see cref="TreeNodeCollection.Contains"/>. method works correctly.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_Contains()
        {
            TreeNodeCollection collection = new TreeNodeCollection();

            TreeNode node1 = new TreeNode();
            collection.Add(node1);

            Assert.Contains(node1, collection);
            Assert.DoesNotContain(new TreeNode(), collection);
        }

        /// <summary>
        /// Tests that the <see cref="TreeNodeCollection.CopyTo"/>. method works correctly.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_CopyTo()
        {
            TreeNodeCollection collection = new TreeNodeCollection();

            TreeNode node1 = new TreeNode();
            collection.Add(node1);
            TreeNode node2 = new TreeNode();
            collection.Add(node2);

            TreeNode[] nodes = new TreeNode[2];
            collection.CopyTo(nodes, 0);
            Assert.Same(node1, nodes[0]);
            Assert.Same(node2, nodes[1]);

            nodes = new TreeNode[3];
            collection.CopyTo(nodes, 1);
            Assert.Null(nodes[0]);
            Assert.Same(node1, nodes[1]);
            Assert.Same(node2, nodes[2]);
        }

        /// <summary>
        /// Tests that the <see cref="TreeNodeCollection.GetEnumerator"/>. method works correctly.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_GetEnumerator()
        {
            TreeNodeCollection collection = new TreeNodeCollection();

            TreeNode node1 = new TreeNode();
            collection.Add(node1);
            TreeNode node2 = new TreeNode();
            collection.Add(node2);

            int index = 0;
            foreach (TreeNode node in (IEnumerable)collection)
            {
                switch (index)
                {
                    case 0:
                        Assert.Same(node1, node);
                        break;
                    case 1:
                        Assert.Same(node2, node);
                        break;
                    default:
                        break;
                }
                index++;
            }

            Assert.Equal(2, index);
        }

        /// <summary>
        /// Tests that the <see cref="TreeNodeCollection.GetEnumerator"/>. method works correctly.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_GetEnumeratorT()
        {
            TreeNodeCollection collection = new TreeNodeCollection();

            TreeNode node1 = new TreeNode();
            collection.Add(node1);
            TreeNode node2 = new TreeNode();
            collection.Add(node2);

            int index = 0;
            foreach (TreeNode node in collection)
            {
                switch (index)
                {
                    case 0:
                        Assert.Same(node1, node);
                        break;
                    case 1:
                        Assert.Same(node2, node);
                        break;
                    default:
                        break;
                }
                index++;
            }

            Assert.Equal(2, index);
        }

        /// <summary>
        /// Tests that the <see cref="TreeNodeCollection.IndexOf"/>. method works correctly.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_IndexOf()
        {
            TreeNodeCollection collection = new TreeNodeCollection();

            TreeNode node1 = new TreeNode();
            collection.Add(node1);
            TreeNode node2 = new TreeNode();
            collection.Add(node2);

            Assert.Equal(0, collection.IndexOf(node1));
            Assert.Equal(1, collection.IndexOf(node2));
        }

        /// <summary>
        /// Tests that the <see cref="TreeNodeCollection.Insert"/>. method works correctly.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_Insert()
        {
            TreeNodeCollection collection = new TreeNodeCollection();

            TreeNode node1 = new TreeNode();
            collection.Insert(0, node1);
            Assert.Same(node1, collection[0]);

            TreeNode node2 = new TreeNode();
            collection.Insert(0, node2);
            Assert.Same(node2, collection[0]);
            Assert.Same(node1, collection[1]);
        }

        /// <summary>
        /// Tests that an exception is thrown when inserting a node into a fixed size collection.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_Insert_FixedSize()
        {
            TreeNodeCollection collection = new TreeNodeCollection(2);

            TreeNode node1 = new TreeNode();
            Assert.Throws<InvalidOperationException>(() => collection.Insert(0, node1));
        }

        /// <summary>
        /// Tests that the <see cref="TreeNodeCollection.Remove"/>. method works correctly.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_Remove()
        {
            TreeNodeCollection collection = new TreeNodeCollection();

            TreeNode node1 = new TreeNode();
            collection.Add(node1);

            TreeNode node2 = new TreeNode();
            collection.Add(node2);

            collection.Remove(node1);
            Assert.Single(collection);
            Assert.Same(node2, collection[0]);

            collection.Remove(node2);
            Assert.Empty(collection);
        }

        /// <summary>
        /// Tests that an exception is thrown when removing a node in a fixed size collection.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_Remove_FixedSize()
        {
            TreeNodeCollection collection = new TreeNodeCollection(2);

            TreeNode node1 = new TreeNode();
            collection[0] = node1;
            Assert.Throws<InvalidOperationException>(() => collection.Remove(node1));
        }

        /// <summary>
        /// Tests that the <see cref="TreeNodeCollection.RemoveAt"/>. method works correctly.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_RemoveAt()
        {
            TreeNodeCollection collection = new TreeNodeCollection();

            TreeNode node1 = new TreeNode();
            collection.Add(node1);

            TreeNode node2 = new TreeNode();
            collection.Add(node2);

            collection.RemoveAt(1);
            Assert.Single(collection);
            Assert.Same(node1, collection[0]);

            collection.RemoveAt(0);
            Assert.Empty(collection);
        }

        /// <summary>
        /// Tests that an exception is thrown when removing a node in a fixed size collection.
        /// </summary>
        [Fact]
        public void TreeNodeCollection_RemoveAt_FixedSize()
        {
            TreeNodeCollection collection = new TreeNodeCollection(2);

            Assert.Throws<InvalidOperationException>(() => collection.RemoveAt(0));
        }
    }
}
