using GenFx.Components.Trees;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// This is a test class for GenFx.Components.Trees.BinaryTreeNode{T}.
    /// </summary>
    public class BinaryTreeNodeTest
    {
        /// <summary>
        /// Tests that the constructor initializes the state correctly.
        /// </summary>
        [Fact]
        public void BinaryTreeNode_Ctor()
        {
            BinaryTreeNode<int> node = new BinaryTreeNode<int>();
            Assert.Equal(2, node.ChildNodes.Count);
            Assert.Null(node.ChildNodes[0]);
            Assert.Null(node.ChildNodes[1]);
            Assert.Null(node.LeftChildNode);
            Assert.Null(node.RightChildNode);
        }

        /// <summary>
        /// Tests that the LeftChildNode and RightChildNode properties works correctly.
        /// </summary>
        [Fact]
        public void BinaryTreeNode_LeftAndRightChildNodes()
        {
            BinaryTreeNode<int> node = new BinaryTreeNode<int>();
            BinaryTreeNode<int> leftNode = new BinaryTreeNode<int>();
            BinaryTreeNode<int> rightNode = new BinaryTreeNode<int>();

            node.LeftChildNode = leftNode;
            Assert.Same(leftNode, node.LeftChildNode);
            Assert.Same(leftNode, node.ChildNodes[0]);

            node.RightChildNode = rightNode;
            Assert.Same(rightNode, node.RightChildNode);
            Assert.Same(rightNode, node.ChildNodes[1]);
        }
    }
}
