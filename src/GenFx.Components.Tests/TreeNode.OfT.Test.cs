using GenFx.Components.Trees;
using System;
using Xunit;

namespace GenFx.Components.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="TreeNode{T}"/> class.
    /// </summary>
    public class TreeNodeOfTTest
    {
        /// <summary>
        /// Tests that the <see cref="TreeNode{T}.Clone"/> method works correctly.
        /// </summary>
        [Fact]
        public void TreeNodeOfT_Clone()
        {
            TestTreeEntity entity = new TestTreeEntity();
            TreeNode<int> node = new TreeNode<int>();
            entity.SetRootNode(node);
            node.AppendChild(new TreeNode());
            node.Value = 10;
            TestTreeEntity newEntity = new TestTreeEntity();
            TreeNode newParent = new TreeNode();
            newEntity.SetRootNode(newParent);
            TreeNode clone = node.Clone(newEntity, newParent);

            Assert.NotSame(node, clone);
            Assert.NotSame(node.ChildNodes[0], clone.ChildNodes[0]);
            Assert.Same(newEntity, clone.Tree);
            Assert.Same(newParent, clone.ParentNode);
            Assert.Equal(node.Value, clone.Value);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null tree.
        /// </summary>
        [Fact]
        public void TreeNodeOfT_Clone_NullTree()
        {
            TreeNode<int> node = new TreeNode<int>();
            Assert.Throws<ArgumentNullException>(() => node.Clone(null, new TreeNode<int>()));
        }

        private class TestTreeEntity : TreeEntityBase
        {
            public override string Representation
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }
        }
    }
}
