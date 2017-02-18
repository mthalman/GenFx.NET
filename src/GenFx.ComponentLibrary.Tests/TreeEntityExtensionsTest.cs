using GenFx.ComponentLibrary.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCommon.Helpers;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="TreeEntityExtensions"/> class.
    /// </summary>
    [TestClass]
    public class TreeEntityExtensionsTest
    {
        /// <summary>
        /// Tests that the <see cref="TreeEntityExtensions.GetPostfixTree"/> method works correctly.
        /// </summary>
        [TestMethod]
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
            CollectionAssert.AreEqual(new TreeNode[]
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
        [TestMethod]
        public void TreeEntityExtensions_GetPostfixTree_NullTree()
        {
            AssertEx.Throws<ArgumentNullException>(() => TreeEntityExtensions.GetPostfixTree(null));
        }

        /// <summary>
        /// Tests that the <see cref="TreeEntityExtensions.GetPrefixTree"/> method works correctly.
        /// </summary>
        [TestMethod]
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
            CollectionAssert.AreEqual(new TreeNode[]
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
        [TestMethod]
        public void TreeEntityExtensions_GetPrefixTree_NullTree()
        {
            AssertEx.Throws<ArgumentNullException>(() => TreeEntityExtensions.GetPrefixTree(null));
        }

        /// <summary>
        /// Tests that the <see cref="TreeEntityExtensions.GetSize"/> method works correctly.
        /// </summary>
        [TestMethod]
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
            Assert.AreEqual(6, size);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null tree.
        /// </summary>
        [TestMethod]
        public void TreeEntityExtensions_GetSize_NullTree()
        {
            AssertEx.Throws<ArgumentNullException>(() => TreeEntityExtensions.GetSize(null));
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
