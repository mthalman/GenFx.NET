using GenFx.ComponentLibrary.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestCommon.Helpers;

namespace GenFx.ComponentLibrary.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="TreeHelper"/> class.
    /// </summary>
    [TestClass]
    public class TreeHelperTest
    {
        /// <summary>
        /// Tests that the <see cref="TreeHelper.ReplaceNodeInTree"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeHelper_ReplaceNodeInTree()
        {
            TestTree tree1 = CreateTree1();
            TestTree tree2 = CreateTree2();
            TreeNode movingNode = tree1.RootNode.ChildNodes[0]; // node2
            TreeNode locationNode = tree2.RootNode.ChildNodes[2]; // node4
            TreeNode locationParentNode = locationNode.ParentNode;

            TreeHelper.ReplaceNodeInTree(movingNode, tree2, locationNode, locationParentNode);

            Assert.AreEqual(3, locationParentNode.ChildNodes.Count);
            Assert.IsFalse(locationParentNode.ChildNodes.Contains(locationNode));
            Assert.AreSame(movingNode, locationParentNode.ChildNodes[2]);
            Assert.AreEqual(1, movingNode.ChildNodes.Count);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null moving node.
        /// </summary>
        [TestMethod]
        public void TreeHelper_ReplaceNodeInTree_NullMovingNode()
        {
            AssertEx.Throws<ArgumentNullException>(() => TreeHelper.ReplaceNodeInTree(null, new TestTree(), new TreeNode(), new TreeNode()));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null location tree.
        /// </summary>
        [TestMethod]
        public void TreeHelper_ReplaceNodeInTree_NullLocationTree()
        {
            AssertEx.Throws<ArgumentNullException>(() => TreeHelper.ReplaceNodeInTree(new TreeNode(), null, new TreeNode(), new TreeNode()));
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null location node.
        /// </summary>
        [TestMethod]
        public void TreeHelper_ReplaceNodeInTree_NullLocationNode()
        {
            AssertEx.Throws<ArgumentNullException>(() => TreeHelper.ReplaceNodeInTree(new TreeNode(), new TestTree(), null, new TreeNode()));
        }

        /// <summary>
        /// Tests that the <see cref="TreeHelper.Swap"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeHelper_Swap()
        {
            TestTree entity1 = new TestTree();
            TreeNode node1 = new TreeNode();
            entity1.SetRootNode(node1);
            TreeNode childNode1 = new TreeNode();
            TreeNode grandChildNode1 = new TreeNode();
            node1.AppendChild(childNode1);
            childNode1.AppendChild(grandChildNode1);

            TestTree entity2 = new TestTree();
            TreeNode node2 = new TreeNode();
            entity2.SetRootNode(node2);
            TreeNode childNode2 = new TreeNode();
            TreeNode grandChildNode2 = new TreeNode();
            node2.AppendChild(childNode2);
            childNode2.AppendChild(grandChildNode2);

            TreeHelper.Swap(entity1.RootNode, entity2.RootNode.ChildNodes[0]);

            Assert.AreSame(childNode2, entity1.RootNode, "Nodes not swapped correctly.");
            Assert.IsNull(childNode2.ParentNode, "ParentNode should be null.");
            Assert.AreSame(entity1, childNode2.Tree, "Tree not set correctly.");
            Assert.AreSame(entity1, grandChildNode2.Tree, "Tree not set correctly.");

            Assert.AreSame(node2, entity2.RootNode, "RootNode should remain unchanged.");
            Assert.AreSame(node1, entity2.RootNode.ChildNodes[0], "Nodes not swapped correctly.");
            Assert.AreSame(node2, node1.ParentNode, "ParentNode should be net to the root node.");
            Assert.AreSame(entity2, node1.Tree, "Tree not set correctly.");
            Assert.AreSame(entity2, childNode1.Tree, "Tree not set correctly.");
            Assert.AreSame(entity2, grandChildNode1.Tree, "Tree not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null node.
        /// </summary>
        [TestMethod]
        public void TreeHelper_Swap_NullNode()
        {
            AssertEx.Throws<ArgumentNullException>(() => TreeHelper.Swap(null, new TreeNode()));
            AssertEx.Throws<ArgumentNullException>(() => TreeHelper.Swap(new TreeNode(), null));
        }

        /// <summary>
        /// Tests that the <see cref="TreeHelper.SetTreeForChildNodes"/> method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeHelper_SetTreeForChildNodes()
        {
            TreeNode root = new TreeNode();
            TreeNode node1 = new TreeNode();
            TreeNode node2 = new TreeNode();
            TreeNode node3 = new TreeNode();
            TreeNode node4 = new TreeNode();

            root.ChildNodes.Add(node1);
            root.ChildNodes.Add(node2);
            node1.ChildNodes.Add(node3);
            node2.ChildNodes.Add(node4);

            TestTree tree = new TestTree();
            root.Tree = tree;

            TreeHelper.SetTreeForChildNodes(root);

            Assert.AreSame(tree, root.Tree);
            Assert.AreSame(tree, node1.Tree);
            Assert.AreSame(tree, node2.Tree);
            Assert.AreSame(tree, node3.Tree);
            Assert.AreSame(tree, node4.Tree);
        }

        /// <summary>
        /// Tests that an exception is thrown when passing a null node.
        /// </summary>
        [TestMethod]
        public void TreeHelper_SetTreeForChildNodes_NullNode()
        {
            AssertEx.Throws<ArgumentNullException>(() => TreeHelper.SetTreeForChildNodes(null));
        }

        private static TestTree CreateTree1()
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
            return tree;
        }

        private static TestTree CreateTree2()
        {
            TestTree tree = new TestTree();
            TreeNode node1 = new TreeNode();
            tree.SetRootNode(node1);

            TreeNode node2 = new TreeNode();
            TreeNode node3 = new TreeNode();
            TreeNode node4 = new TreeNode();
            node1.AppendChild(node2);
            node1.AppendChild(node3);
            node1.AppendChild(node4);

            TreeNode node5 = new TreeNode();
            TreeNode node6 = new TreeNode();
            node2.AppendChild(node5);
            node4.AppendChild(node6);
            return tree;
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
