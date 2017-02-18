using GenFx.ComponentLibrary.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestCommon.Helpers;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="TreeNode{T}"/> class.
    /// </summary>
    [TestClass]
    public class TreeNodeOfTTest
    {
        /// <summary>
        /// Tests that the <see cref="TreeNode{T}.Clone"/> method works correctly.
        /// </summary>
        [TestMethod]
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

            Assert.AreNotSame(node, clone, "Nodes should not be same instance.");
            Assert.AreNotSame(node.ChildNodes[0], clone.ChildNodes[0], "Nodes should not be same instance.");
            Assert.AreSame(newEntity, clone.Tree, "Tree not set correctly.");
            Assert.AreSame(newParent, clone.ParentNode, "Parent node not set correctly.");
            Assert.AreEqual(node.Value, clone.Value, "Value not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null tree.
        /// </summary>
        [TestMethod]
        public void TreeNodeOfT_Clone_NullTree()
        {
            TreeNode<int> node = new TreeNode<int>();
            AssertEx.Throws<ArgumentNullException>(() => node.Clone(null, new TreeNode<int>()));
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
