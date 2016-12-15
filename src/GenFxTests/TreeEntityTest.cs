using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Collections.Generic;
using GenFx.ComponentLibrary.Trees;
using GenFx;
using GenFxTests.Mocks;
using GenFxTests.Helpers;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Trees.TreeEntity&lt;TNode&gt; and is intended
    ///to contain all GenFx.ComponentLibrary.Trees.TreeEntity&lt;TNode&gt; Unit Tests
    ///</summary>
    [TestClass()]
    public class TreeEntityTest
    {
        /// <summary>
        /// Tests that the CopyTo method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntity_CopyTo()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new TestTreeEntityConfiguration();
            TreeEntity entity = new TestTreeEntity(algorithm);
            PrivateObject accessor = new PrivateObject(entity, new PrivateType(typeof(GeneticEntity)));
            accessor.SetField("age", 10);
            entity.SetRootNode(new TreeNode());
            TreeEntity newEntity = new TestTreeEntity(algorithm);
            entity.CopyTo(newEntity);

            Assert.AreEqual(entity.Age, newEntity.Age, "Entity class members not copied correctly.");
            Assert.IsNotNull(entity.RootNode, "RootNode not copied.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a null entity is passed.
        /// </summary>
        [TestMethod]
        public void TreeEntity_CopyToNullEntity()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new TestTreeEntityConfiguration();
            TreeEntity entity = new TestTreeEntity(algorithm);
            AssertEx.Throws<ArgumentNullException>(() => entity.CopyTo(null));
        }

        /// <summary>
        /// Tests that the GetSize method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntity_GetSize()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new TestTreeEntityConfiguration();
            TreeEntity entity = new TestTreeEntity(algorithm);

            int size = entity.GetSize();
            Assert.AreEqual(0, size, "Incorrect size.");

            entity.SetRootNode(new TreeNode());
            entity.RootNode.AppendChild(new TreeNode());
            entity.RootNode.ChildNodes[0].AppendChild(new TreeNode());
            entity.RootNode.AppendChild(new TreeNode());

            size = entity.GetSize();
            Assert.AreEqual(4, size, "Incorrect size.");
        }

        /// <summary>
        /// Tests that the GetPrefixTree method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntity_GetPrefixTree()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new TestTreeEntityConfiguration();
            TreeEntity entity = new TestTreeEntity(algorithm);

            entity.SetRootNode(new TreeNode());
            entity.RootNode.AppendChild(new TreeNode());
            entity.RootNode.ChildNodes[0].AppendChild(new TreeNode());
            entity.RootNode.AppendChild(new TreeNode());

            int loopCount = 0;
            IEnumerable<TreeNode> nodes = entity.GetPrefixTree();
            foreach (TreeNode node in nodes)
            {
                switch (loopCount)
                {
                    case 0:
                        Assert.AreSame(entity.RootNode, node, "Incorrect node.");
                        break;
                    case 1:
                        Assert.AreSame(entity.RootNode.ChildNodes[0], node, "Incorrect node.");
                        break;
                    case 2:
                        Assert.AreSame(entity.RootNode.ChildNodes[0].ChildNodes[0], node, "Incorrect node.");
                        break;
                    case 3:
                        Assert.AreSame(entity.RootNode.ChildNodes[1], node, "Incorrect node.");
                        break;
                    default:
                        Assert.Fail("More nodes encountered than expected.");
                        break;
                }
                loopCount++;
            }
        }

        /// <summary>
        /// Tests that the GetPostfixTree method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntity_GetPostfixTree()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new TestTreeEntityConfiguration();
            TreeEntity entity = new TestTreeEntity(algorithm);

            entity.SetRootNode(new TreeNode());
            entity.RootNode.AppendChild(new TreeNode());
            entity.RootNode.ChildNodes[0].AppendChild(new TreeNode());
            entity.RootNode.AppendChild(new TreeNode());

            int loopCount = 0;
            IEnumerable<TreeNode> nodes = entity.GetPostfixTree();
            foreach (TreeNode node in nodes)
            {
                switch (loopCount)
                {
                    case 0:
                        Assert.AreSame(entity.RootNode.ChildNodes[0].ChildNodes[0], node, "Incorrect node.");
                        break;
                    case 1:
                        Assert.AreSame(entity.RootNode.ChildNodes[0], node, "Incorrect node.");
                        break;
                    case 2:
                        Assert.AreSame(entity.RootNode.ChildNodes[1], node, "Incorrect node.");
                        break;
                    case 3:
                        Assert.AreSame(entity.RootNode, node, "Incorrect node.");
                        break;
                    default:
                        Assert.Fail("More nodes encountered than expected.");
                        break;
                }
                loopCount++;
            }
        }

        /// <summary>
        /// Tests that the SetRootNode method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntity_SetRootNode()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new TestTreeEntityConfiguration();
            TreeEntity entity = new TestTreeEntity(algorithm);
            TreeNode node = new TreeNode();
            TreeNode childNode = new TreeNode();
            PrivateObject nodeAccessor = new PrivateObject(childNode);
            nodeAccessor.SetField("parentNode", node);

            TreeNode grandChildNode = new TreeNode();

            node.ChildNodes.Add(childNode);
            childNode.ChildNodes.Add(grandChildNode);

            entity.SetRootNode(childNode);

            Assert.AreSame(childNode, entity.RootNode, "RootNode not set correctly.");
            Assert.IsNull(childNode.ParentNode, "ParentNode should be null.");
            Assert.AreSame(entity, childNode.Tree, "Tree not set correctly.");
            Assert.AreSame(entity, grandChildNode.Tree, "Tree not set correctly.");
        }

        /// <summary>
        /// Tests that the Swap method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeEntity_Swap()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm();
            algorithm.ConfigurationSet.Entity = new TestTreeEntityConfiguration();

            TreeEntity entity1 = new TestTreeEntity(algorithm);
            TreeNode node1 = new TreeNode();
            entity1.SetRootNode(node1);
            TreeNode childNode1 = new TreeNode();
            TreeNode grandChildNode1 = new TreeNode();
            node1.AppendChild(childNode1);
            childNode1.AppendChild(grandChildNode1);

            TreeEntity entity2 = new TestTreeEntity(algorithm);
            TreeNode node2 = new TreeNode();
            entity2.SetRootNode(node2);
            TreeNode childNode2 = new TreeNode();
            TreeNode grandChildNode2 = new TreeNode();
            node2.AppendChild(childNode2);
            childNode2.AppendChild(grandChildNode2);

            entity1.Swap(entity1.RootNode, entity2.RootNode.ChildNodes[0]);

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

        private class TestTreeEntity : TreeEntity
        {
            public TestTreeEntity(GeneticAlgorithm algorithm)
                : base(algorithm)
            {

            }

            public override string Representation
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }

            protected override void InitializeCore()
            {
                throw new Exception("The method or operation is not implemented.");
            }

            public override GeneticEntity Clone()
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        [Component(typeof(TestTreeEntity))]
        private class TestTreeEntityConfiguration : TreeEntityConfiguration
        {
        }
    }


}
