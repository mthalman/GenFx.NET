using GenFx;
using GenFx.ComponentLibrary.Trees;
using GenFxTests.Helpers;
using GenFxTests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
            GeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);
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
            GeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);
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
            GeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);
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
            GeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);
            TreeNode node = new TreeNode();
            entity.SetRootNode(node);
            node.AppendChild(new TreeNode());
            node.Value = 10;
            TestTreeEntity newEntity = new TestTreeEntity();
            newEntity.Initialize(algorithm);
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
            GeneticAlgorithm algorithm = GetAlgorithm();
            TestTreeEntity entity = new TestTreeEntity();
            entity.Initialize(algorithm);
            TreeNode node = new TreeNode();
            entity.SetRootNode(node);
            node.AppendChild(new TreeNode());
            node.Value = 10;
            TestTreeEntity newEntity = new TestTreeEntity();
            newEntity.Initialize(algorithm);
            TreeNode newParent = new TreeNode();
            newEntity.SetRootNode(newParent);
            TreeNode newNode = new TreeNode();
            node.CopyTo(newNode, newEntity, newParent);

            Assert.AreSame(newEntity, newNode.Tree, "Tree not set correctly.");
            Assert.AreSame(newParent, newNode.ParentNode, "ParentNode not set correctly.");
            Assert.AreNotSame(node.ChildNodes[0], newNode.ChildNodes[0], "Nodes should not be same instance.");
            Assert.AreEqual(node.Value, newNode.Value, "Value not set correctly.");
        }

        /// <summary>
        /// Tests that the object can be serialized and deserialized.
        /// </summary>
        [TestMethod]
        public void Serialization()
        {
            TreeNode node = new TreeNode();
            node.Value = 3;
            node.ParentNode = new TreeNode();
            node.Tree = new TestTreeEntity();
            node.ChildNodes.Add(new TreeNode());

            TreeNode result = (TreeNode)SerializationHelper.TestSerialization(node, new Type[] {
                typeof(TestTreeEntity)
            });

            Assert.AreEqual(node.Value, result.Value);
            Assert.IsInstanceOfType(node.ParentNode, typeof(TreeNode));
            Assert.IsInstanceOfType(node.Tree, typeof(TestTreeEntity));
            Assert.IsInstanceOfType(node.ChildNodes[0], typeof(TreeNode));
        }

        private static GeneticAlgorithm GetAlgorithm()
        {
            GeneticAlgorithm algorithm = new MockGeneticAlgorithm
            {
                PopulationSeed = new MockPopulation(),
                SelectionOperator = new MockSelectionOperator(),
                FitnessEvaluator = new MockFitnessEvaluator(),
                GeneticEntitySeed = new TestTreeEntity()
            };
            return algorithm;
        }

        [DataContract]
        private class TestTreeEntity : TreeEntityBase
        {
            public override string Representation
            {
                get { throw new Exception("The method or operation is not implemented."); }
            }
        }
    }
}
