using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using GenFx.ComponentLibrary.Trees;
using GenFx;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// This is a test class for GenFx.ComponentLibrary.Trees.BinaryTreeNode{T}.
    /// </summary>
    [TestClass()]
    public class BinaryTreeNodeTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [TestMethod]
        public void BinaryTreeNode_Ctor()
        {
            BinaryTreeNode<int> node = new BinaryTreeNode<int>();
            Assert.AreEqual(2, node.ChildNodes.Count, "Incorrect number of child nodes created.");
            Assert.IsNull(node.ChildNodes[0], "Child node should be null.");
            Assert.IsNull(node.ChildNodes[1], "Child node should be null.");
            Assert.IsNull(node.LeftChildNode, "Child node should be null.");
            Assert.IsNull(node.RightChildNode, "Child node should be null.");
        }

        /// <summary>
        /// Tests that the LeftChildNode and RightChildNode properties works correctly.
        /// </summary>
        [TestMethod]
        public void BinaryTreeNode_LeftAndRightChildNodes()
        {
            BinaryTreeNode<int> node = new BinaryTreeNode<int>();
            BinaryTreeNode<int> leftNode = new BinaryTreeNode<int>();
            BinaryTreeNode<int> rightNode = new BinaryTreeNode<int>();

            node.LeftChildNode = leftNode;
            Assert.AreSame(leftNode, node.LeftChildNode, "Nodes should be same instance.");
            Assert.AreSame(leftNode, node.ChildNodes[0], "Nodes should be same instance.");

            node.RightChildNode = rightNode;
            Assert.AreSame(rightNode, node.RightChildNode, "Nodes should be same instance.");
            Assert.AreSame(rightNode, node.ChildNodes[1], "Nodes should be same instance.");
        }
    }
}
