using GenFx.Components.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="TreeEntityExtensions"/> class.
    /// </summary>
    public class TreeEntityExtensionsTest
    {
        /// <summary>
        /// Tests that the <see cref="TreeEntityExtensions.GetPostfixTree"/> method works correctly.
        /// </summary>
        [Fact]
        public void TreeEntityExtensions_GetPostfixTree()
        {
            TestTree tree = new TestTree();
            TreeNode node1 = new TreeNode();
            tree.SetRootNode(node1);

            TreeNode node2 = new TreeNode();
            TreeNode node3 = new TreeNode();
            node1.AppendChild(node2);
            node1.AppendChild(node3);

            TreeNode node4 = new TreeNode();
            node2.AppendChild(node4);
            TreeNode node5 = new TreeNode();
            TreeNode node6 = new TreeNode();
            node3.AppendChild(node5);
            node3.AppendChild(node6);

            List<TreeNode> list = TreeEntityExtensions.GetPostfixTree(tree).ToList();
            Assert.Equal(new TreeNode[]
            {
                node4,
                node2,
                node5,
                node6,
                node3,
                node1
            }, list);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null tree.
        /// </summary>
        [Fact]
        public void TreeEntityExtensions_GetPostfixTree_NullTree()
        {
            Assert.Throws<ArgumentNullException>(() => TreeEntityExtensions.GetPostfixTree(null));
        }

        /// <summary>
        /// Tests that the <see cref="TreeEntityExtensions.GetPrefixTree"/> method works correctly.
        /// </summary>
        [Fact]
        public void TreeEntityExtensions_GetPrefixTree()
        {
            TestTree tree = new TestTree();
            TreeNode node1 = new TreeNode();
            tree.SetRootNode(node1);

            TreeNode node2 = new TreeNode();
            TreeNode node3 = new TreeNode();
            node1.AppendChild(node2);
            node1.AppendChild(node3);

            TreeNode node4 = new TreeNode();
            node2.AppendChild(node4);
            TreeNode node5 = new TreeNode();
            TreeNode node6 = new TreeNode();
            node3.AppendChild(node5);
            node3.AppendChild(node6);

            List<TreeNode> list = TreeEntityExtensions.GetPrefixTree(tree).ToList();
            Assert.Equal(new TreeNode[]
            {
                node1,
                node2,
                node4,
                node3,
                node5,
                node6
            }, list);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null tree.
        /// </summary>
        [Fact]
        public void TreeEntityExtensions_GetPrefixTree_NullTree()
        {
            Assert.Throws<ArgumentNullException>(() => TreeEntityExtensions.GetPrefixTree(null));
        }

        /// <summary>
        /// Tests that the <see cref="TreeEntityExtensions.GetSize"/> method works correctly.
        /// </summary>
        [Fact]
        public void TreeEntityExtensions_GetSize()
        {
            TestTree tree = new TestTree();
            TreeNode node1 = new TreeNode();
            tree.SetRootNode(node1);

            TreeNode node2 = new TreeNode();
            TreeNode node3 = new TreeNode();
            node1.AppendChild(node2);
            node1.AppendChild(node3);

            TreeNode node4 = new TreeNode();
            node2.AppendChild(node4);
            TreeNode node5 = new TreeNode();
            TreeNode node6 = new TreeNode();
            node3.AppendChild(node5);
            node3.AppendChild(node6);

            int size = TreeEntityExtensions.GetSize(tree);
            Assert.Equal(6, size);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null tree.
        /// </summary>
        [Fact]
        public void TreeEntityExtensions_GetSize_NullTree()
        {
            Assert.Throws<ArgumentNullException>(() => TreeEntityExtensions.GetSize(null));
        }

        private class TestTree : TreeEntityBase
        {
            public override string Representation
            {
                get
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
