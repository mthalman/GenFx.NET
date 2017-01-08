using GenFx;
using GenFx.ComponentLibrary.Trees;
using GenFx.Contracts;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenFxTests
{
    /// <summary>
    ///This is a test class for GenFx.ComponentLibrary.Trees.TreeNode&lt;T&gt; and is intended
    ///to contain all GenFx.ComponentLibrary.Trees.TreeNode&lt;T&gt; Unit Tests
    ///</summary>
    [TestClass()]
    public class TreeNodeTest
    {
        /// <summary>
        /// Tests that the AppendChild method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeNode_AppendChild()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity(algorithm);
            TreeNode node = new TreeNode();
            entity.SetRootNode(node);

            TreeNode child = new TreeNode();
            node.AppendChild(child);
            Assert.AreSame(child, node.ChildNodes[0], "Child not appended correctly.");
            Assert.AreSame(node, child.ParentNode, "Parent not set correctly.");
            Assert.AreSame(entity, child.Tree, "Tree not set correctly.");
        }

        /// <summary>
        /// Tests that the InsertChild method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeNode_InsertChild()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity(algorithm);
            TreeNode node = new TreeNode();
            entity.SetRootNode(node);

            TreeNode child1 = new TreeNode();
            TreeNode child2 = new TreeNode();
            node.InsertChild(0, child1);
            node.InsertChild(0, child2);

            Assert.AreSame(child2, node.ChildNodes[0], "Child not inserted correctly.");
            Assert.AreSame(node, child2.ParentNode, "Parent not set correctly.");
            Assert.AreSame(entity, child2.Tree, "Tree not set correctly.");
            Assert.AreSame(child1, node.ChildNodes[1], "Child not inserted correctly.");
            Assert.AreSame(node, child1.ParentNode, "Parent not set correctly.");
            Assert.AreSame(entity, child1.Tree, "Tree not set correctly.");
        }

        /// <summary>
        /// Tests that an exception is thrown when a child is inserted with a parent not assigned to a tree.
        /// </summary>
        [TestMethod]
        public void TreeNode_InsertChild_NoTree()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity(algorithm);
            TreeNode node = new TreeNode();

            TreeNode child1 = new TreeNode();
            AssertEx.Throws<InvalidOperationException>(() => node.InsertChild(0, child1));
        }

        /// <summary>
        /// Tests that the Clone method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeNode_Clone()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity(algorithm);
            TreeNode node = new TreeNode();
            entity.SetRootNode(node);
            node.AppendChild(new TreeNode());
            node.Value = 10;
            TestTreeEntity newEntity = new TestTreeEntity(algorithm);
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
        /// Tests that the CopyTo method works correctly.
        /// </summary>
        [TestMethod]
        public void TreeNode_CopyTo()
        {
            IGeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity(algorithm);
            TreeNode node = new TreeNode();
            entity.SetRootNode(node);
            node.AppendChild(new TreeNode());
            node.Value = 10;
            TestTreeEntity newEntity = new TestTreeEntity(algorithm);
            TreeNode newParent = new TreeNode();
            newEntity.SetRootNode(newParent);
            TreeNode newNode = new TreeNode();
            node.CopyTo(newNode, newEntity, newParent);

            Assert.AreSame(newEntity, newNode.Tree, "Tree not set correctly.");
            Assert.AreSame(newParent, newNode.ParentNode, "ParentNode not set correctly.");
            Assert.AreNotSame(node.ChildNodes[0], newNode.ChildNodes[0], "Nodes should not be same instance.");
            Assert.AreEqual(node.Value, newNode.Value, "Value not set correctly.");
        }

        private static IGeneticAlgorithm GetAlgorithm()
        {
            IGeneticAlgorithm algorithm = new MockGeneticAlgorithm(new ComponentFactoryConfigSet
            {
                GeneticAlgorithm = new MockGeneticAlgorithmFactoryConfig(),
                Population = new MockPopulationFactoryConfig(),
                SelectionOperator = new MockSelectionOperatorFactoryConfig(),
                FitnessEvaluator = new MockFitnessEvaluatorFactoryConfig(),
                Entity = new TestTreeEntityFactoryConfig()
            });
            return algorithm;
        }

        private class TestTreeEntity : TreeEntity<TestTreeEntity, TestTreeEntityFactoryConfig>
        {
            public TestTreeEntity(IGeneticAlgorithm algorithm)
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
        }

        private class TestTreeEntityFactoryConfig : TreeEntityFactoryConfig<TestTreeEntityFactoryConfig, TestTreeEntity>
        {
        }
    }
}
